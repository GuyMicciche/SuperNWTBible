using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using System.Collections.Generic;
using Android.Webkit;
using System.Xml.Linq;
using System.Linq;
using ActionbarSherlock.App;
using NWTBibleFree.NotesMenu;
using Android.Preferences;

namespace NWTBibleFree.ReaderMenu
{
    public class ReaderFragment : Fragment
    {
        private DailyTextDay dailyText = new DailyTextDay();

        private int numOfChapters = 0;

        public WebView webview;
        private string html;  
      
        public ReaderActivity.MyWebViewClient client;

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetHasOptionsMenu(true);
            SetMenuVisibility(true);
        }

        public override View OnCreateView(LayoutInflater intInflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Default view is Bible reader
            View view = intInflater.Inflate(Resource.Layout.Reader, container, false);

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                view = intInflater.Inflate(Resource.Layout.Reader, container, false);
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                //LoadPublicationReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                view = intInflater.Inflate(Resource.Layout.DailyText, container, false);
            }
            else if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {
                //LoadEPUBReader(0);
            }

            return view;
        }

        public override void OnActivityCreated(Bundle bundle)
        {
            base.OnActivityCreated(bundle);

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                LoadBibleReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                //LoadPublicationReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                LoadDailyTextReader(0);
            }
            else if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {
                //LoadEPUBReader(0);
            } 
        }

        private void LoadBibleReader()
        {
            int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber);

            // Load xml document of the book
            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());

            // Load each chapter
            //var query = from node in doc.Descendants("c")
            //            select new
            //            {
            //                Data = node.Value
            //            };

            List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();

            ThisApp.allBookChapters = collection;

            numOfChapters = collection.Count();

            // Get the CDATA of the selected chapter
            html = collection.ElementAt(chapterNum - 1);

            client = new ReaderActivity.MyWebViewClient(Activity, ThisApp.doHighlight, null, ThisApp.selectedChapter.ChapterNumber);

            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview.SetWebViewClient(client);
            webview.Settings.JavaScriptEnabled = true; 
            webview.Settings.PluginsEnabled = true;
            webview.Settings.DisplayZoomControls = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
            webview.AddJavascriptInterface(new ReaderActivity.VerseSelection(Activity, webview), "VerseSelection");
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
        }

        public void ReHighlight()
        {
            RunJs(webview, "ClearSelection();");

            // Highlight user highlights
            var chapterVerses = ThisApp.highlightedScriptures;
            List<string> verseNumbersList = new List<string>();
            for (int cv = 0; cv < chapterVerses.Count; cv++)
            {
                if (chapterVerses.ElementAt(cv).Chapter.ChapterNumber.Equals(ThisApp.selectedChapter.ChapterNumber))
                {
                    verseNumbersList.Add(chapterVerses.ElementAt(cv).VerseNumber);
                }
            }

            foreach (string vs in verseNumbersList)
            {
                RunJs(webview, "HighlightVerse('" + EscapeJsString(vs) + "');");
            }
        }

        public void ReScrollToHighlight()
        {
            RunJs(webview, "ScrollToHighlight('" + EscapeJsString("2") + "');");
        }        

        public void RunJs(WebView webview, String js)
        {
            webview.LoadUrl("javascript:" + js);
        }

        /**
        * Helper method to escape JavaScript strings. Useful when passing strings to a WebView via
        * "javascript:" calls.
        */
        public string EscapeJsString(String s)
        {
            if (s == null)
            {
                return "";
            }


            return s.Replace("'", "\\'").Replace("\"", "\\\"");
        }

        public void ReloadBiblePage(int offset)
        {
            if (offset > numOfChapters)
            {
                offset = 1;
            }
            else if (offset < 1)
            {
                offset = numOfChapters;
            }

            // Load xml document of the book
            //XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());

            // Load each chapter
            //var query = from node in doc.Descendants("c")
            //            select new
            //            {
            //                Data = node.Value
            //            };


            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = offset.ToString()
            };

            client = new ReaderActivity.MyWebViewClient(Activity, ThisApp.doHighlight, null, ThisApp.selectedChapter.ChapterNumber);

            html = ThisApp.allBookChapters.ElementAt(offset - 1);
            webview.SetWebViewClient(client);
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            // No selected verses
            ThisApp.selectedVerses = new List<BibleVerse>();

            //ReHighlight();
        }

        public void LoadDailyTextReader(int offset)
        {
            var dtDate = View.FindViewById<TextView>(Resource.Id.dtDate);
            var dtScripture = View.FindViewById<TextView>(Resource.Id.dtScripture);
            var dtContent = View.FindViewById<TextView>(Resource.Id.dtContent);

            dailyText = ThisApp.GetDailyText(FormatDateTime(DateTime.Now.AddDays(offset)));

            ThisApp.selectedDailyText = dailyText;

            dtDate.SetTypeface(ThisApp.Font(Activity, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            dtScripture.SetTypeface(ThisApp.Font(Activity, "ionitalic"), Android.Graphics.TypefaceStyle.Normal);
            dtContent.SetTypeface(ThisApp.Font(Activity, "ionbook"), Android.Graphics.TypefaceStyle.Normal);

            dtDate.Text = dailyText.Date;
            dtScripture.Text = dailyText.Scripture;
            dtContent.Text = dailyText.Content;
        }

        public void LoadDailyTextReaderFromActionBar(int position)
        {
            var dtDate = View.FindViewById<TextView>(Resource.Id.dtDate);
            var dtScripture = View.FindViewById<TextView>(Resource.Id.dtScripture);
            var dtContent = View.FindViewById<TextView>(Resource.Id.dtContent);

            dailyText = ThisApp.GetDailyTextByPosition(position);

            ThisApp.selectedDailyText = dailyText;

            dtDate.SetTypeface(ThisApp.Font(Activity, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            dtScripture.SetTypeface(ThisApp.Font(Activity, "ionitalic"), Android.Graphics.TypefaceStyle.Normal);
            dtContent.SetTypeface(ThisApp.Font(Activity, "ionbook"), Android.Graphics.TypefaceStyle.Normal);

            dtDate.Text = dailyText.Date;
            dtScripture.Text = dailyText.Scripture;
            dtContent.Text = dailyText.Content;
        }

        public void InitializeNoteParadigm()
        {
            string[] verses = new string[ThisApp.selectedVerses.Count];

            if (ThisApp.selectedVerses.Count == 0)
            {
                return;
            }

            foreach (var i in ThisApp.selectedVerses)
            {
                Console.WriteLine(i.ScriptureFormatted);
            }

            for (int j = 0; j < verses.Length; j++)
            {
                verses[j] = ThisApp.selectedVerses[j].VerseNumber;
            }

            var intent = new Intent(Activity, typeof(NoteEditActivity));
            StartActivity(intent);
        }

        public string FormatDateTime(DateTime input)
        {
            return input.ToString(@"yyyy-MM-dd");
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