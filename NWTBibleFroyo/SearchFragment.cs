using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;
using NWTBible.ReaderMenu;
using Android.Text;

namespace NWTBible
{
    public class SearchFragment : SherlockFragment
    {
        private View view;

        private ListView list;
        private Spinner searchSpinner;
        private EditText searchQuery;
        private ProgressBar progress;
        private SearchResultListAdapter adapter;
        private List<BibleVerse> verseResults;

        private bool fromReader = true;

        private BackgroundWorker worker;

        public SearchFragment(bool fromReader)
        {
            this.fromReader = fromReader;
        }

        public SearchFragment()
        {
            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
        }

        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            if (searchSpinner == null)
            {
                return;
            }

            List<string> searchList = new List<string>();
            searchList.Add("Entire Bible");
            searchList.Add("Hebrew Scriptures");
            searchList.Add("Greek Scriptures");
            foreach (var b in ThisApp.GetAllBookTitles())
            {
                searchList.Add(b);
            }
            searchSpinner.Prompt = "Search";
            ArrayAdapter searchAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem, searchList);
            searchAdapter.SetDropDownViewResource(Resource.Layout.ListItem);
            searchSpinner.Adapter = searchAdapter;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            worker = new BackgroundWorker();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.SearchFragment, container, false);

            List<string> searchList = new List<string>();
            searchList.Add("Entire Bible");
            searchList.Add("Hebrew Scriptures");
            searchList.Add("Greek Scriptures");
            foreach (var b in ThisApp.GetAllBookTitles())
            {
                searchList.Add(b);
            }
            searchSpinner = view.FindViewById<Spinner>(Resource.Id.searchSpinner);
            searchSpinner.Prompt = "Search";
            ArrayAdapter searchAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem, searchList);
            searchAdapter.SetDropDownViewResource(Resource.Layout.ListItem);
            searchSpinner.Adapter = searchAdapter;

            verseResults = new List<BibleVerse>();
            adapter = new SearchResultListAdapter(Activity, verseResults);

            list = view.FindViewById<ListView>(Resource.Id.searchResults);
            list.ItemClick += list_ItemClick;
            list.Adapter = adapter;

            searchQuery = view.FindViewById<EditText>(Resource.Id.searchQuery);
            progress = view.FindViewById<ProgressBar>(Resource.Id.resultsProgressBar);

            Button search = view.FindViewById<Button>(Resource.Id.searchButton);
            search.Click += search_Click;

            //Button stop = view.FindViewById<Button>(Resource.Id.searchStopButton);
            //search.Click += stop_Click;
            
            return view;
        }

        //private void stop_Click(object sender, EventArgs e)
        //{
        //    worker.CancelAsync();
        //}

        void list_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            BibleVerse verse = adapter.verses[e.Position];

            ThisApp.doHighlight = true;
            ThisApp.selectedNote = new NoteScripture() { ScriptureForHighlight = "0,0," + verse.VerseNumber.ToString() };

            // Set book
            ThisApp.selectedBook = ThisApp.allBibleBooks.Find(x => x.Name.ToUpper() == verse.Book.Name.ToUpper());

            // Set chapter
            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = verse.Chapter.ChapterNumber.ToString()
            };

            ThisApp.ReaderKind = ReaderKind.BibleReader;

            if (fromReader)
            {
                if (ReaderNavigationType.IsSelectingNavigation)
                {
                    var act = Activity as ReaderActivity;
                    act.ReloadActionBar();

                    Android.Support.V4.App.Fragment newContent = null;
                    newContent = new ReaderFragment();
                    if (newContent != null)
                    {
                        act.SwitchContent(newContent);
                    }
                }
                else if (ReaderNavigationType.IsSwipingNavigation)
                {
                    var act = Activity as ReaderViewPagerActivity;
                    act.UpdatePager(int.Parse(verse.Chapter.ChapterNumber.ToString()));
                }

                XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
                List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
                ThisApp.allBookChapters = collection;
            }
            else
            {
                Intent intent = new Intent(Activity, ThisApp.MainReader.Class);
                StartActivity(intent);
            }
            
        }

        void search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ThisApp.Language))
            {
                ThisApp.AlertBox(Activity, "REMINDER", "Please download a language pack.");
            }
            else
            {
                progress.Progress = 0;

                worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.WorkerReportsProgress = true;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync();
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //worker.ReportProgress(100);

            if (e.Error != null)
            {
                Activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(Activity, e.Error.Message, ToastLength.Long).Show();
                    progress.Progress = 0;
                });
            }
            else if (e.Cancelled)
            {
                Activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(Activity, adapter.Count.ToString() + " results found.", ToastLength.Long).Show();
                    progress.Progress = 0;
                });
            }
            else
            {
                Activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(Activity, adapter.Count.ToString() + " results found.", ToastLength.Long).Show();
                    progress.Progress = 0;
                });
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
                       
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Activity.RunOnUiThread(() =>
            {
                verseResults = new List<BibleVerse>();
                adapter = new SearchResultListAdapter(Activity, verseResults);
                list.Adapter = adapter;

                ((BaseAdapter)adapter).NotifyDataSetChanged();
            });

            ExecuteSearch();
        }

        private void ExecuteSearch()
        {
            // Search entire Bible
            if (searchSpinner.SelectedItemPosition == 0)
            {
                progress.Max = 1189;

                foreach (var book in ThisApp.GetAllBookNames())
                {
                    SearchBook(book);
                }
            }
            // Search Hebrew Scriptures
            else if (searchSpinner.SelectedItemPosition == 1)
            {
                progress.Max = 929;

                foreach (var book in ThisApp.GetCanonBookNames("hebrew"))
                {
                    SearchBook(book);
                }
            }
            // Search Greek Scriptures
            else if (searchSpinner.SelectedItemPosition == 2)
            {
                progress.Max = 260;
                
                foreach (var book in ThisApp.GetCanonBookNames("greek"))
                {
                    SearchBook(book);
                }
            }
            // Search single book
            else
            {
                string book = ThisApp.GetAllBookNames()[searchSpinner.SelectedItemPosition - 3];

                SearchBook(book, true);                
            } 
        }

        private void SearchBook(string book, bool setProgressMax = false)
        {
            XDocument doc;
            int chapterNumber = 0;
            string query = searchQuery.Text;

            Console.WriteLine("Book of " + book);
            BibleBook bibleBook = new BibleBook() { Name = book.ToString(), Title = ThisApp.allBibleBooks.Single(x => x.Name.ToLower().Equals(book.ToLower())).Title };

            doc = ThisApp.GetNWTFile(ThisApp.Language, book);

            List<string> chapters = doc.Descendants("c").Select((element) => element.Value).ToList();

            if (setProgressMax)
            {
                progress.Max = chapters.Count;
            }

            chapterNumber = 0;

            foreach (var chapter in chapters)
            {
                // Remove footnotes
                string html = chapter.Replace("*", "").Replace("+", "").Replace("′", "").Replace("·", "");

                // Add span nodes       
                html = Regex.Replace(html, @"(\d+.)(</a></span>)(.*?)((?=<span)|(?=\d+.</a></span>)|(?=</p>))", delegate(Match match)
                {
                    string m = match.Groups[1].Value + "</a>" + match.Groups[3].Value + "</span>";

                    return m;
                });

                html = Regex.Replace(html, @"dcv_\d+_", "");

                progress.Progress++;

                chapterNumber++;

                BibleChapter bibleChapter = new BibleChapter() { ChapterNumber = chapterNumber.ToString(), Book = bibleBook };

                XDocument cdocs = XDocument.Parse(html.Replace("<!DOCTYPE html>", ""));

                IEnumerable<XElement> spans = cdocs.Descendants("span");
                for (var i = 0; i < spans.Count(); i++)
                {
                    string scripture = spans.ElementAt(i).Value;
                    string match = MatchBoundry(query, scripture);
                    if (!string.IsNullOrEmpty(match))
                    {
                        BibleVerse verse = new BibleVerse
                        {
                            Book = bibleBook,
                            Chapter = bibleChapter,
                            VerseNumber = (i + 1).ToString(),
                            Scripture = match
                        };

                        Activity.RunOnUiThread(() =>
                        {
                            adapter.Add(verse);
                            ((BaseAdapter)adapter).NotifyDataSetChanged();
                        });
                    }
                }
            }
        }

        private string MatchBoundry(string query, string scripture)
        {
            // Match words excluding tags      \b(\w+(?![^<>]*>))\b
            // Match whole sentences           (?sx-m)[^\r\n].*?(?:(?:\.|\?|!|\:)\s)
            // Match whole sentences           \b\(?((?>\w+),?\s?)+[\.!?]\)?

            string result = string.Empty;

            bool exactMatch = false;

            // Search book for other languages
            if (!ThisApp.Language.Contains("English"))
            {
                query = ThisApp.GBToUnicode(query).Replace("\\u", "\\\\u");
                scripture = ThisApp.GBToUnicode(scripture);

                Console.WriteLine(query);
                Console.WriteLine(scripture);

                Regex regex = new Regex(query, RegexOptions.Compiled);

                if (regex.IsMatch(scripture))
                {
                    result = Regex.Replace(scripture, @"(?i)(.*)(" + query + @")(.*)", delegate(Match match)
                    {
                        string m = ThisApp.UnicodeToGB(match.Groups[1].Value) + "<b><font color=\"#0481D4\">" + ThisApp.UnicodeToGB(match.Groups[2].Value) + "</font></b>" + ThisApp.UnicodeToGB(match.Groups[3].Value);

                        return m;
                    });
                    
                }

                return result;
            }

            if (exactMatch)
            {
                Regex regex = new Regex(@"(?i)\b" + query + @"\b", RegexOptions.Compiled);

                if (regex.IsMatch(scripture))
                {
                    result = Regex.Replace(scripture, @"(?i)(.*)(\b" + query + @"\b)(.*)", delegate(Match match)
                    {
                        string m = match.Groups[1].Value + "<b><font color=\"#0481D4\">" + match.Groups[2].Value + "</font></b>" + match.Groups[3].Value;

                        return m;
                    });
                }

                return result;
            }
            else
            {
                string temp = scripture;

                string[] strArray = query.Split(new Char[] { ' ' });
                string[] pattern = new string[strArray.Length];

                for (var j = 0; j < strArray.Length; j++)
                {
                    pattern[j] = @"(?i)\b" + strArray[j] + @"\b";
                }

                bool matchAll = true;
                for (var r = 0; r < pattern.Length; r++)
                {
                    Regex regex = new Regex(pattern[r]);
                    if (!regex.IsMatch(scripture))
                    {
                        matchAll = false;
                    }
                    else
                    {
                        result = Regex.Replace(temp, pattern[r], delegate(Match match)
                        {
                            return "<b><font color=\"#0481D4\">" + match.Value + "</font></b>";
                        });
                        temp = result;
                    }
                }
                if (matchAll)
                {
                    return result;
                }
            }

            return result;
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class SearchResultListAdapter : BaseAdapter<BibleVerse>
    {
        Activity context;
        public List<BibleVerse> verses;

        public SearchResultListAdapter(Activity context, List<BibleVerse> verses)
            : base()
        {
            this.context = context;
            this.verses = verses;
        }

        public void Add(BibleVerse verse)
        {
            verses.Add(verse);
            NotifyDataSetChanged();
        }

        public override int Count
        {
            get { return this.verses.Count(); }
        }

        public override BibleVerse this[int position]
        {
            get { return this.verses[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this.verses[position];
            var view = convertView;

            if (convertView == null || !(convertView is LinearLayout))
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.SearchRow, parent, false);
            }

            LinearLayout.LayoutParams lay = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            lay.Gravity = GravityFlags.Right;

            var bookItem = view.FindViewById<TextView>(Resource.Id.textSearchTitle);
            bookItem.SetText(item.BookChapterVerse, TextView.BufferType.Normal);
            bookItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

            var detailsItem = view.FindViewById<TextView>(Resource.Id.textSearchContent);
            detailsItem.SetText(Html.FromHtml(item.Scripture), TextView.BufferType.Normal);

            if (ThisApp.Language == "Arabic")
            {
                bookItem.LayoutParameters = lay;
                detailsItem.LayoutParameters = lay;
            }

            return view;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }
}