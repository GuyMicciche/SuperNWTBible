using Android.App;
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.Preferences;

using Java.Lang;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

using NWTBible.NotesMenu;
using NWTBible.ReaderMenu;

using Xamarin.Parse;

using SlidingMenuBinding.Lib.App;
using System.Xml;
using Android.Views;
using Android.Widget;
using Android.Text.Method;
using Android.Text;
using Android.Webkit;
using Android.Util;
using System.Text.RegularExpressions;
using System.Text;

namespace NWTBible
{
    public class App : Application
    {
        //
        // LANGUAGE CHANGE EVENT HANDLER
        //
        public delegate void LanguageChangedEventHandler(object sender, LanguageChangedArgs e);
        public class LanguageChangedArgs : EventArgs
        {
            private string language;

            public LanguageChangedArgs(string language)
            {
                this.language = language;
            }

            public string Language
            {
                get
                {
                    return language;
                }
            }
        }

        public event LanguageChangedEventHandler LanguageChanged;
        protected virtual void OnLanguageChanged(LanguageChangedArgs e)
        {
            LanguageChanged(this, e);
        }

        private string language;
        public string Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;

                LanguageChangedArgs args = new LanguageChangedArgs(value);
                this.OnLanguageChanged(args);
            }
        }

        //
        // THEME CHANGE EVENT HANDLER
        //
        public delegate void ThemeChangedEventHandler(object sender, ThemeChangedArgs e);
        public class ThemeChangedArgs : EventArgs
        {
            private int styleTheme;

            public ThemeChangedArgs(int styleTheme)
            {
                this.styleTheme = styleTheme;
            }

            public int StyleTheme
            {
                get
                {
                    return styleTheme;
                }
            }
        }

        public event ThemeChangedEventHandler ThemeChanged;
        protected virtual void OnThemeChanged(ThemeChangedArgs e)
        {
            ThemeChanged(this, e);
        }

        private int styleTheme;
        public int StyleTheme
        {
            get
            {
                return this.styleTheme;
            }
            set
            {
                this.styleTheme = value;

                ThemeChangedArgs args = new ThemeChangedArgs(value);
                this.OnThemeChanged(args);
            }
        }

        static readonly App instance = new App();
        public static App Instance
        {
            get
            {
                return instance;
            }
        }

        public SlidingFragmentActivity MainReader
        {
            get
            {
                SlidingFragmentActivity act = new SlidingFragmentActivity();

                if (ReaderNavigationType.IsSelectingNavigation)
                {
                    act = new ReaderActivity();
                }
                else if (ReaderNavigationType.IsSwipingNavigation)
                {
                    act = new ReaderViewPagerActivity();
                }

                return act;
            }
        }

        public const string STOREHOUSE = "http://www.kingdomnotes.com/WT_LIB/";

        public bool LoggedIn = false;

        public WebClient client;
        public Context context;
        public Activity activity;
        private int NumberOfFiles;
        public int FilesCoutner = 0;
        private ProgressDialog progress;
        public List<string> downloadQueue;
        public bool allDownloaded = false;
        private List<NWTFile> files;
        private XDocument doc;
        public string[] AvailableDownloadedLanguages;

        public string JWName = "Anonymous";

        public bool doHighlight = false;

        public string WolArticle;

        public BibleBook selectedBook = new BibleBook();
        public BibleChapter selectedChapter = new BibleChapter();
        public Publication selectedPublication = new Publication();
        public PublicationArticle selectedPublicationArticle = new PublicationArticle();
        public DailyTextDay selectedDailyText = new DailyTextDay();
        public NoteScripture selectedNote = new NoteScripture();

        public List<BibleBook> allBibleBooks = new List<BibleBook>();
        public List<string> allBookChapters = new List<string>();
        public List<DailyTextDay> allDailyTexts = new List<DailyTextDay>();
        public List<Publication> allPublications = new List<Publication>();
        public List<PublicationArticle> allPublicationArticles = new List<PublicationArticle>();

        public List<BibleVerse> selectedVerses = new List<BibleVerse>();

        public List<BibleVerse> highlightedScriptures = new List<BibleVerse>();

        public string ReaderKind;

        public bool PinyinToggle = true;

        public ISharedPreferences preferences;

        public App()
        {
            // Dummy code to add I18N and West to the apk
            var ignore = new I18N.West.CP437();
        }

        public string[] GetAllBookNames()
        {
            string[] books = { "GENESIS", "EXODUS", "LEVITICUS", "NUMBERS", "DEUTERONOMY", "JOSHUA", "JUDGES", "RUTH", "1 SAMUEL", "2 SAMUEL", "1 KINGS", "2 KINGS", "1 CHRONICLES", "2 CHRONICLES", "EZRA", "NEHEMIAH", "ESTHER", "JOB", "PSALMS", "PROVERBS", "ECCLESIASTES", "SONG OF SOLOMON", "ISAIAH", "JEREMIAH", "LAMENTATIONS", "EZEKIEL", "DANIEL", "HOSEA", "JOEL", "AMOS", "OBADIAH", "JONAH", "MICAH", "NAHUM", "HABAKKUK", "ZEPHANIAH", "HAGGAI", "ZECHARIAH", "MALACHI", "MATTHEW", "MARK", "LUKE", "JOHN", "ACTS", "ROMANS", "1 CORINTHIANS", "2 CORINTHIANS", "GALATIANS", "EPHESIANS", "PHILIPPIANS", "COLOSSIANS", "1 THESSALONIANS", "2 THESSALONIANS", "1 TIMOTHY", "2 TIMOTHY", "TITUS", "PHILEMON", "HEBREWS", "JAMES", "1 PETER", "2 PETER", "1 JOHN", "2 JOHN", "3 JOHN", "JUDE", "REVELATION" };

            return books;
        }

        public string[] GetAllBookTitles()
        {
            List<string> books = new List<string>();

            foreach (var b in allBibleBooks)
            {
                books.Add(b.Title);
            }

            string[] final = books.ToArray();

            return final;
        }

        public List<BibleBook> GetAllBooks(string lang)
        {
            List<BibleBook> booksList = new List<BibleBook>();

            // Hebrew books
            var doc = GetNWTFile(Language, "hebrew");

            foreach (var query in doc.Descendants("book"))
            {
                BibleBook book = new BibleBook()
                {
                    Number = query.Attribute("number").Value,
                    //Title = query.Attribute("title").Value.ToUpper(),
                    Title = query.Attribute("title").Value,
                    Name = query.Attribute("n").Value,
                    Abbreviation = query.Attribute("abbr").Value,
                    Chapters = query.Attribute("chapters").Value,
                    Writer = query.Attribute("writer").Value,
                    Place = query.Attribute("place").Value,
                    Completed = query.Attribute("completed").Value,
                    Time = query.Attribute("time").Value
                };

                booksList.Add(book);
            }

            // Greek books
            doc = GetNWTFile(Language, "greek");

            foreach (var query in doc.Descendants("book"))
            {
                BibleBook book = new BibleBook()
                {
                    Number = query.Attribute("number").Value,
                    //Title = query.Attribute("title").Value.ToUpper(),
                    Title = query.Attribute("title").Value,
                    Name = query.Attribute("n").Value,
                    Abbreviation = query.Attribute("abbr").Value,
                    Chapters = query.Attribute("chapters").Value,
                    Writer = query.Attribute("writer").Value,
                    Place = query.Attribute("place").Value,
                    Completed = query.Attribute("completed").Value,
                    Time = query.Attribute("time").Value
                };

                booksList.Add(book);
            }

            return booksList;
        }

        public List<BibleBook> GetCanonBooks(string canon)
        {
            List<BibleBook> booksList = new List<BibleBook>();

            var doc = GetNWTFile(Language, canon);

            foreach (var query in doc.Descendants("book"))
            {
                BibleBook book = new BibleBook()
                {
                    Number = query.Attribute("number").Value,
                    //Title = query.Attribute("title").Value.ToUpper(),
                    Title = query.Attribute("title").Value,
                    Name = query.Attribute("n").Value,
                    Abbreviation = query.Attribute("abbr").Value,
                    Chapters = query.Attribute("chapters").Value,
                    Writer = query.Attribute("writer").Value,
                    Place = query.Attribute("place").Value,
                    Completed = query.Attribute("completed").Value,
                    Time = query.Attribute("time").Value
                };

                booksList.Add(book);
            }

            return booksList;
        }

        public List<string> GetCanonBookNames(string canon)
        {
            List<string> booksList = new List<string>();

            var doc = GetNWTFile(Language, canon);

            foreach (var query in doc.Descendants("book"))
            {
                //booksList.Add(query.Attribute("title").Value.ToUpper());
                booksList.Add(query.Attribute("title").Value);
            }

            return booksList;
        }

        public List<Publication> GetAllPublications(string lang)
        {
            var doc = GetNWTFile(lang, "publications");

            List<Publication> pubsList = new List<Publication>();

            foreach (var query in doc.Descendants("publication"))
            {
                Publication pub = new Publication()
                {
                    Title = query.Attribute("fulltitle").Value,
                    //ShortTitle = query.Attribute("title").Value.ToUpper(),
                    ShortTitle = query.Attribute("title").Value,
                    Code = query.Attribute("abbr").Value,
                    Image = query.Attribute("img").Value,
                    CurrentPage = 0
                };

                pubsList.Add(pub);
            }

            return pubsList;
        }

        public List<PublicationArticle> GetAllPublicationArticles()
        {
            XDocument doc = GetNWTFile(Language, selectedPublication.Image);
            List<PublicationArticle> collection = doc.Descendants("article").Select(article => new PublicationArticle
            {
                ArticleTitle = article.Element("title").Value,
                Content = article.Element("content").Value,
                DocumentLocation = article.Element("documentLocation").Value,
                PublicationTitle = selectedPublication.Image
            }).ToList();


            return collection;
        }

        public List<DailyTextDay> GetAllDailyTexts(string lang)
        {
            XDocument doc = GetNWTFile(Language, "dailytext");

            List<DailyTextDay> collection = doc.Descendants("day").Select((element, index) => new DailyTextDay
            {
                Index = (index + 1),
                DateShort = element.Element("code").Value,
                Date = element.Element("date").Value,
                Scripture = element.Element("scripture").Value,
                Content = element.Element("content").Value
            }).ToList();

            // Sort list by sysdate and convert it back to list
            //collection = collection.OrderBy(x => x.DateShort.ToString()).ToList();

            return collection;
        }

        public DailyTextDay GetDailyText(string date)
        {
            DailyTextDay item = allDailyTexts.First(e => e.DateShort.Equals(date));

            return item;
        }

        public DailyTextDay GetDailyTextByPosition(int position)
        {
            DailyTextDay item = allDailyTexts.First(e => e.Index == position);

            return item;
        }

        public DailyTextDay GetSecondLanguageDailyText(string language, string date, int position)
        {
            DailyTextDay item;

            XDocument doc = GetNWTFile(language, "dailytext");

            List<DailyTextDay> collection = doc.Descendants("day").Select((element, index) => new DailyTextDay
            {
                Index = (index + 1),
                DateShort = element.Element("code").Value,
                Date = element.Element("date").Value,
                Scripture = element.Element("scripture").Value,
                Content = element.Element("content").Value
            }).ToList();

            if (position == 0)
            {
                item = allDailyTexts.First(e => e.DateShort.Equals(date));
            }
            else
            {
                item = collection.First(e => e.Index == position);
            }

            return item;
        }

        public string[] GetAvailableLanguages()
        {
            List<string> languages = new List<string>();

            string url = STOREHOUSE + "languages.xml";

            //if (ConnectedToNetwork(context))
            //{
            //    XDocument doc = XDocument.Load(url);
            //    foreach (string lang in doc.Descendants("language"))
            //    {
            //        languages.Add(lang);
            //    }
            //}
            //else
            //{
            //    string xml;

            //    var input = context.Assets.Open("languages.xml");

            //    using (StreamReader reader = new StreamReader(input))
            //    {
            //        xml = reader.ReadToEnd();
            //    }

            //    XDocument doc = XDocument.Parse(xml);

            //    foreach (string lang in doc.Descendants("language"))
            //    {
            //        languages.Add(lang);
            //    }
            //}    

            string xml;

            var input = context.Assets.Open("languages.xml");

            using (StreamReader reader = new StreamReader(input))
            {
                xml = reader.ReadToEnd();
            }

            XDocument doc = XDocument.Parse(xml);

            foreach (string lang in doc.Descendants("language"))
            {
                languages.Add(lang);
            } 

            return languages.ToArray();
        }

        public string GetLanguageCode(string lang)
        {
            string code = "E";

            string xml;

            var input = context.Assets.Open("languages.xml");

            using (StreamReader reader = new StreamReader(input))
            {
                xml = reader.ReadToEnd();
            }

            XDocument doc = XDocument.Parse(xml);

            XElement language = doc.Descendants("language").Single(x => x.Value == lang);

            code = language.Attribute("code").Value;

            return code;
        }

        public string GetLanguageR(string lang)
        {
            string code = "E";

            string xml;

            var input = context.Assets.Open("languages.xml");

            using (StreamReader reader = new StreamReader(input))
            {
                xml = reader.ReadToEnd();
            }

            XDocument doc = XDocument.Parse(xml);

            XElement language = doc.Descendants("language").Single(x => x.Value == lang);

            code = language.Attribute("r").Value;

            return code;
        }

        public bool[] IsLanguagesDownloaded(string[] available)
        {
            bool[] downloaded = new bool[available.Length];
            string[] languages = available;

            for (var i = 0; i < languages.Length; i++)
            {
                var dir = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
                var folder = new Java.IO.File(dir, languages[i]);
                downloaded[i] = folder.IsDirectory;
            }

            return downloaded;
        }

        private List<string> downloadedLanguages = new List<string>();
        public List<string> DownloadedLanguages
        {
            get
            {
                this.downloadedLanguages = new List<string>();

                bool[] downloaded = IsLanguagesDownloaded(AvailableDownloadedLanguages);
                string[] languages = AvailableDownloadedLanguages.ToArray();



                for (var i = 0; i < languages.Length; i++)
                {
                    if (downloaded[i])
                    {
                        this.downloadedLanguages.Add(languages[i]);
                    }
                }

                return this.downloadedLanguages;
            }
            set
            {
                this.downloadedLanguages = value;
            }
        }

        public Dialog TranslateLanguageDialog(Context context, bool fromReader = false)
        {
            // All downloaded languages
            string[] languages = DownloadedLanguages.ToArray();

            var builder = new AlertDialog.Builder(context);

            // No downloaded languages
            if (languages == null || languages.Length < 1)
            {
                builder.SetIcon(Resource.Drawable.Icon);
                builder.SetTitle("REMINDER");
                builder.SetMessage("No Language Packs are downloaded. Please download a Language Pack.");
                builder.SetPositiveButton("Ok", (sender, e) => { });
            }
            else
            {
                // The current selected language
                int index = Array.IndexOf(languages, Language);
                int selected = index;

                builder.SetIcon(Resource.Drawable.Icon);
                builder.SetTitle("Translate Language");
                builder.SetCancelable(false);
                builder.SetSingleChoiceItems(languages, index, (sender, e) =>
                {
                    selected = e.Which;
                });
                builder.SetPositiveButton("Ok", (sender, e) =>
                {
                    this.Language = languages[selected];

                    if (fromReader)
                    {
                        if (ReaderKind == NWTBible.ReaderKind.PublicationReader)
                        {
                            if (!allPublications.Any(x => x.Image == selectedPublication.Image))
                            {
                                return;
                            }
                        }

                        Activity act = context as Activity;
                        act.Finish();

                        Intent intent = new Intent(context, MainReader.Class);
                        context.StartActivity(intent);
                    }
                });
                builder.SetNegativeButton("Cancel", (sender, e) =>
                {
                    selected = index;
                });
            }

            return builder.Create();
        }

        public Dialog DownloadLanguagePackDialog(Context context)
        {
            if (AvailableDownloadedLanguages == null)
            {
                AvailableDownloadedLanguages = GetAvailableLanguages().ToArray();
            }

            // Clear array, reset the queue
            downloadQueue = new List<string>();

            string[] available = AvailableDownloadedLanguages;

            bool[] toDownload = IsLanguagesDownloaded(available);
            string[] availableToDownload = available;

            var builder = new AlertDialog.Builder(context);
            builder.SetIcon(Resource.Drawable.Icon);
            builder.SetTitle("Download Language Packs");
            builder.SetCancelable(false);
            builder.SetMultiChoiceItems(availableToDownload, toDownload, (sender, e) =>
            {
                int index = e.Which;

                toDownload[index] = e.IsChecked;
            });

            builder.SetPositiveButton("Ok", (sender, e) =>
            {
                DownloadLanguagesFromDialog(AvailableDownloadedLanguages, toDownload);
            });
            builder.SetNegativeButton("Cancel", delegate
            {

            });

            return builder.Create();
        }

        public ProgressDialog DownloadingProgressDialog(string title, string message)
        {
            ProgressDialog progress = new ProgressDialog(context);
            progress.SetMessage(message);
            progress.SetTitle(title);
            progress.SetIcon(Resource.Drawable.Icon);
            progress.SetButton("Cancel", (sender, args) =>
            {
                // Close progress dialog
                progress.Dismiss();

                // Stop all downloads
                client.CancelAsync();

                // Remove any language folders that were to be downloaded
                for (var i = 0; i < downloadQueue.Count; i++)
                {
                    DeleteLanguagePack(downloadQueue[i]);
                }

                // No downloads to be downloaded
                downloadQueue = new List<string>();
                // No files to be download
                FilesCoutner = 0;
                // They didn't all download, but set to true anyways
                allDownloaded = true;

                if (DownloadedLanguages.Count > 0)
                {
                    Language = Language;
                }
            });
            progress.SetProgressStyle(ProgressDialogStyle.Horizontal);
            progress.SetCancelable(false);
            progress.SetCanceledOnTouchOutside(false);

            return progress;
        }

        class MyClickListener : Java.Lang.Object, IDialogInterfaceOnClickListener
        {
            Context Context { get; set; }

            public MyClickListener(Context c)
            {
                Context = c;
            }

            public void OnClick(IDialogInterface dialog, int which)
            {

            }
        }

        public void DownloadLanguagesFromDialog(string[] items, bool[] selections)
        {
            allDownloaded = false;
            downloadQueue = new List<string>();

            // Languages already downloaded
            bool[] downloaded = IsLanguagesDownloaded(AvailableDownloadedLanguages);

            for (int i = 0; i < items.Length; i++)
            {
                //// If it's the last item;
                //if (i == items.Length - 1)
                //{
                //    allDownloaded = true;
                //}

                // If selection is not downloaded and checked, then download
                if ((downloaded[i] != selections[i]) && selections[i])
                {
                    downloadQueue.Add(items[i]);
                }
                // Delete all languages not selected
                else if ((downloaded[i] != selections[i]) && !selections[i])
                {
                    DeleteLanguagePack(items[i]);
                }
            }
            
            if (downloadQueue.Count > 0)
            {
                DownloadLanguagePack(downloadQueue[0]);
            }
        }

        public string UnicodeToString(string unicode)
        {
            System.Text.UnicodeEncoding encodingUNICODE = new System.Text.UnicodeEncoding();
            byte[] unicodeToBytes = encodingUNICODE.GetBytes(unicode);

            return encodingUNICODE.GetString(unicodeToBytes);
        }

        public XDocument GetNWTFile(string language, string fileName)
        {
            //fileName = fileName.Replace(" ", "%20");

            Console.WriteLine(language + "   " + fileName);

            XDocument doc = new XDocument();

            var file = GetLanguageFile(language, fileName + ".xml");

            if (File.Exists(file.Path))
            {
                using (var stream = new StreamReader(file.Path))
                {
                    doc = XDocument.Parse(stream.ReadToEnd());
                }
            }

            return doc;
        }

        public Java.IO.File GetLanguageDirectory(string language)
        {
            var dir = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            var folder = new Java.IO.File(dir, language);  // Folder sorted by language
            if (folder.Exists())
            {
                return folder;
            }

            return null;
        }

        public Java.IO.File GetLanguageFile(string language, string fileName)
        {
            var folder = GetLanguageDirectory(language);
            var file = new Java.IO.File(folder, fileName);

            if (File.Exists(file.Path))
            {
                return file;
            }

            return null;
        }

        public Java.IO.File GetEPUBPage(string language, string dir, string fileName)
        {
            var root = GetLanguageDirectory(language);
            var directory = new Java.IO.File(root, dir);
            var maindir = new Java.IO.File(directory, "OEBPS");
            var file = new Java.IO.File(maindir, fileName);

            //string[] children = maindir.List();
            //for (int i = 0; i < children.Length; i++)
            //{
            //    string child = children[i];
            //    Console.WriteLine(child);
            //}

            //Console.WriteLine(file.Path);

            if (File.Exists(file.Path))
            {
                Console.WriteLine("Exists: " + file.Path);
                return file;
            }

            return null;
        }

        public void DeleteLanguagePack(string language)
        {
            if (language == Language)
            {
                //return;
                Language = "";
            }

            var dir = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            var folder = new Java.IO.File(dir, language);

            if (folder.IsDirectory)
            {
                Console.WriteLine(folder.Path + " is a directory and exists!");
                string[] children = folder.List();
                for (int i = 0; i < children.Length; i++)
                {
                    //string child = children[i].Replace(" ", "%20");
                    string child = children[i];

                    var file = new Java.IO.File(folder, child);
                    file.Delete();
                    Console.WriteLine(file.Path + " is deleted!");
                }
                folder.Delete();
                Console.WriteLine(folder.Path + " is deleted!");
            }

            DownloadedLanguages.Remove(language);
        }

        public void DownloadLanguagePack(string language)
        {
            Console.WriteLine("ATTEMPTING TO DOWNLOAD " + language);

            files = new List<NWTFile>();

            string[] others = { "hebrew", "greek", "publications"};
            for (var i = 0; i < others.Length; i++)
            {
                NWTFile file = new NWTFile(others[i] + ".xml", STOREHOUSE + language + "/" + others[i] + ".xml");
                files.Add(file);
            }

            //string[] publications = { "dailytext", "biblestories" };
            string[] publications = { "dailytext", "allscripture", "knowledge", "bearingwitness", "biblestories", "bibleteach", "closetojehovah", "godslove", "isaiah1", "isaiah2", "jehovahsday", "jehovahswill", "jeremiah", "ministryschool", "myfollower", "reasoning", "revelation", "singtojehovah", "teacher", "worshipgod", "youngpeople1", "youngpeople2" };
            //string[] publications = { "dailytext" };
            for (var i = 0; i < publications.Length; i++)
            {
                NWTFile file = new NWTFile(publications[i] + ".xml", STOREHOUSE + language + "/books/" + publications[i] + ".xml");
                files.Add(file);
            }

            //string[] pi = { "jr" };
            //for (var i = 0; i < pi.Length; i++)
            //{
            //    NWTFile file = new NWTFile(pi[i] + ".jpg", "http://www.jw.org/assets/a/" + pi[i] + "/" + pi[i] + "_" + GetPubCode(language) + "/" + pi[i] + "_" + GetPubCode(language) + "_xs.jpg");
            //    files.Add(file);
            //}

            string[] books = GetAllBookNames();
            for (var i = 0; i < books.Length; i++)
            {
                //string book = books[i].Replace(" ", "%20");
                string book = books[i];

                NWTFile file = new NWTFile(book + ".xml", STOREHOUSE + language + "/bible/" + book + ".xml");
                files.Add(file);
            }

            string[] watchtower = { "ws_E_20130315", "g_E_201303", "wp_E_20130301", "w_E_20130315", "w_E_20130215", "w_E_20130115", "w_E_20121215", "w_E_20121115", "w_E_20121015", "w_E_20120915", "w_E_20120815", "w_E_20120715", "w_E_20120615", "w_E_20120515", "w_E_20120415", "w_E_20120315", "w_E_20120215", "w_E_20120115" };
            for (var i = 0; i < watchtower.Length; i++)
            {
                NWTFile file = new NWTFile(watchtower[i] + ".epub", STOREHOUSE + language + "/Watchtower/" + watchtower[i] + ".epub", watchtower[i]);
                //files.Add(file);
            }

            //string[] test = { "English" };
            //for (var i = 0; i < test.Length; i++)
            //{
            //    NWTFile file = new NWTFile(test[i] + ".zip", "http://dl.dropbox.com/u/826238/nwtreader/" + language + "/" + test[i] + ".zip");
            //    files.Add(file);
            //}

            NumberOfFiles = files.Count();

            Console.WriteLine(NumberOfFiles + " files to download in " + language);

            progress = DownloadingProgressDialog("Language Pack — " + language, "Parsing language pack...");
            progress.Max = NumberOfFiles;
            progress.Progress = 0;

            for (var i = 0; i < NumberOfFiles; i++)
            {
                string[] args = { language, files[i].FileName, files[i].FileTitle };

                Console.WriteLine("File name: " + files[i].FileName);
                Console.WriteLine("URL: " + files[i].FileURL);

                // This way...
                //wc = new WebClient();
                //wc.OpenReadCompleted += wc_OpenReadCompleted;
                //wc.OpenReadAsync(new System.Uri(files[i].FileURL), args);

                // OR this way
                //HttpWebRequest request = WebRequest.Create(files[i].FileURL) as HttpWebRequest;
                //request.Method = "GET";
                //request.ContentType = "application/xml";
                //request.BeginGetResponse(result => ReadCallback(result, args, request), null);

                var dir = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
                var folder = new Java.IO.File(dir, language);  // Folder sorted by language
                var file = new Java.IO.File(folder, files[i].FileName);  // Represents each file that was downloaded
                if (!folder.Exists())
                {
                    // Create the folder
                    folder.Mkdirs();
                    Console.WriteLine("Creating folder: " + folder.Path);
                }

                Console.WriteLine("Attempting to run WebClient");

                client = new WebClient();
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                client.DownloadFileAsync(new System.Uri(files[i].FileURL), file.Path, args);
                client.DownloadStringCompleted += client_DownloadStringCompleted;
            }

            progress.Show();

            DownloadedLanguages.Add(language);
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // If the user canceled a download, override download paradigm and cancel all
            if (allDownloaded == true)
            {
                return;
            }

            string[] args = (string[])e.UserState;
            string language = args[0];
            string filename = args[1];
            string filetitle = args[2];

            Console.WriteLine("Saved! " + filename);

            FilesCoutner++;

            progress.Progress = FilesCoutner;

            // Do this when all files are downloaded
            if (NumberOfFiles == FilesCoutner)
            {
                progress.Dismiss();
                progress.Progress = 0;
                FilesCoutner = 0;

                downloadQueue.Remove(language);                

                if (downloadQueue.Count > 0)
                {
                    activity.RunOnUiThread(() =>
                        {
                            if (ConnectedToNetwork(context))
                            {
                                DownloadLanguagePack(downloadQueue[0]);
                            }
                            else
                            {
                                return;
                            }
                        });
                }
                else
                {
                    allDownloaded = true;
                }
            }

            // Do this when all checked languages are downloaded
            if (allDownloaded)
            {
                activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(context, "Language packs successfully configured.", ToastLength.Long).Show();
                    //MessageBox(context, "Success!", "All Language Packs you selected have been downloaded!");
                });
                
                Language = language;
            }
        }

        private void ReadCallback(IAsyncResult result, string[] args, HttpWebRequest request)
        {
            string language = args[0];
            string filename = args[1];

            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
            StreamReader reader = new StreamReader(response.GetResponseStream());

            Console.WriteLine("OpenReadCompleted: " + filename);

            if (Android.OS.Environment.ExternalStorageState == Android.OS.Environment.MediaMounted)
            {
                var dir = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
                var folder = new Java.IO.File(dir, language);
                var file = new Java.IO.File(folder, filename);
                if (!folder.Exists())
                {
                    folder.Mkdirs();
                }

                //XDocument doc = XDocument.Load(reader);

                Console.WriteLine("Saved: " + filename);

                FilesCoutner++;
                progress.Progress = FilesCoutner;

                if (NumberOfFiles == FilesCoutner)
                {
                    Console.WriteLine("Language Pack Complete: " + language);
                    progress.Dismiss();
                    FilesCoutner = 0;
                }
            }

            response.Close();
        }

        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            string[] args = (string[])e.UserState;
            string language = args[0];
            string filename = args[1];

            Console.WriteLine("OpenReadCompleted: " + filename);

            if (Android.OS.Environment.ExternalStorageState == Android.OS.Environment.MediaMounted)
            {
                var dir = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
                var folder = new Java.IO.File(dir, language);
                var file = new Java.IO.File(folder, filename);
                if (!folder.Exists())
                {
                    folder.Mkdirs();
                }

                // Old way
                //using (var stream = new FileStream(file.Path, FileMode.Create, FileAccess.Write, FileShare.Write))
                //{
                //    UTF8Encoding enc = new UTF8Encoding();
                //    byte[] data = enc.GetBytes(doc.ToString());
                //    stream.Write(data, 0, data.Length);
                //    stream.Flush();
                //    stream.Close();

                //    FilesCoutner++;
                //}

                doc = new XDocument();
                using (Stream s = e.Result)
                {
                    // Load xml into document
                    doc = XDocument.Load(s);

                    using (var stream = new StreamWriter(file.Path, false))
                    {
                        // Save document
                        doc.Save(stream);
                    }
                }

                //var stream = new StreamWriter(file.Path, false);
                //XmlDocument xmldoc = new XmlDocument();
                //xmldoc.Load(e.Result);
                //xmldoc.Save(stream);


                //Load response stream into XMLReader
                // XmlTextReader reader = new XmlTextReader(e.Result);
                //XmlDocument xmldoc = new XmlDocument();
                //xmldoc.Load(reader);
                //xmldoc.Save(new StreamWriter(file.Path, false));
                //reader.Close();



                Console.WriteLine("Saved: " + filename);

                FilesCoutner++;
                progress.Progress = FilesCoutner;

                if (NumberOfFiles == FilesCoutner)
                {
                    Console.WriteLine("Language Pack Complete: " + language);
                    progress.Dismiss();
                    FilesCoutner = 0;
                    Language = language;
                }
            }
        }

        public void UnzipEpub(string upZipToFolder, string filename)
        {
            //Unzip zip = new Unzip(language + ".zip", language);
            //zip.unzip();                

            var root = GetLanguageDirectory(language);
            var folder = new Java.IO.File(root, upZipToFolder);  // Represents each file that was downloaded
            if (!EpubExists(folder.Path))
            {
                // Create the folder
                folder.Mkdirs();
            }

            string zipToUnpack = GetLanguageFile(language, filename).Path;
            string unpackDirectory = folder.Path;

            Console.WriteLine("Unzipping... " + filename);

            Zip.ExtractAll(zipToUnpack, unpackDirectory);

            Console.WriteLine("UNZIPPED!");
        }

        public bool EpubExists(string filename)
        {
            var root = GetLanguageDirectory(language);
            var file = new Java.IO.File(root, filename);  // Represents each file that was downloaded
            if (!file.Exists())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ConnectedToNetwork(Context context)
        {
            //Console.WriteLine("Connectivity Service is: " + ConnectivityService);
            //// Check if you are connected to a network
            //bool isAvailable = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            //ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            //NetworkInfo networkInfo = connectivityManager.GetNetworkInfo(isAvailable ? ConnectivityType.Wifi : ConnectivityType.Mobile);

            //// Check if you have a connection from that network
            //if (networkInfo.IsConnected || networkInfo.IsConnectedOrConnecting)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            bool connected = false;

            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            if (connectivityManager != null)
            {
                bool mobileNetwork = false;
                bool wifiNetwork = false;
                bool wimaxNetwork = false;

                bool mobileNetworkConnected = false;
                bool wifiNetworkConnected = false;
                bool wimaxNetworkConnected = false;

                NetworkInfo mobileInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
                NetworkInfo wifiInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
                NetworkInfo wimaxInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wimax);

                if (mobileInfo != null)
                {
                    mobileNetwork = mobileInfo.IsAvailable;
                    Console.WriteLine("Is mobile available?  " + mobileNetwork);
                }

                if (wifiInfo != null)
                {
                    wifiNetwork = wifiInfo.IsAvailable;
                    Console.WriteLine("Is WiFi available?  " + wifiNetwork);
                }

                if (wimaxInfo != null)
                {
                    wimaxNetwork = wimaxInfo.IsAvailable;
                    Console.WriteLine("Is WiMAX available?  " + wimaxNetwork);
                }

                if (wifiNetwork || mobileNetwork || wimaxNetwork)
                {
                    mobileNetworkConnected = (mobileInfo != null) ? mobileInfo.IsConnectedOrConnecting : false;
                    wifiNetworkConnected = (wifiInfo != null) ? wifiInfo.IsConnectedOrConnecting : false;
                    wimaxNetworkConnected = (wimaxInfo != null) ? wimaxInfo.IsConnectedOrConnecting : false;
                }

                connected = (mobileNetworkConnected || wifiNetworkConnected || wimaxNetworkConnected);

                Console.WriteLine("Is mobile connected?  " + mobileNetworkConnected);
                Console.WriteLine("Is WiFi connected?  " + wifiNetworkConnected);
                Console.WriteLine("Is WiMAX connected?  " + wimaxNetworkConnected);
            }

            Console.WriteLine("Is this device connected?  " + connected);

            return connected;
        }

        public Android.Net.Uri GetRemoteImage(string url, string filename)
        {
            // My special folder
            var dir = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            var folder = new Java.IO.File(dir, language);
            // Create a temp folder if there is not already
            if (!folder.Exists())
            {
                folder.Mkdirs();
            }

            // Reference to the image on the device
            string file = new Java.IO.File(dir, filename).Path;

            if (System.IO.File.Exists(file))
            {
                return Android.Net.Uri.Parse(file);
            }
            else
            {
                // Make the connection and execute download immediately
                WebClient wc = new WebClient();
                wc.DownloadFile(url, file);

                return Android.Net.Uri.Parse(file);
            }
        }

        public Typeface Font(Context context, string font)
        {
            Typeface tf = null;

            if (Language == "English")
            {
                if (font.Equals("ionbold"))
                {
                    tf = Typeface.CreateFromAsset(context.Assets, "fonts/Ion-Bold.ttf");
                }
                else if (font.Equals("ionbook"))
                {
                    tf = Typeface.CreateFromAsset(context.Assets, "fonts/Ion-Book.ttf");
                }
                else if (font.Equals("ionduel"))
                {
                    tf = Typeface.CreateFromAsset(context.Assets, "fonts/Ion-Duel.ttf");
                }
                else if (font.Equals("ionitalic"))
                {
                    tf = Typeface.CreateFromAsset(context.Assets, "fonts/Ion-Italic.ttf");
                }
            }
            else if (Language.Contains("Chinese"))
            {
                tf = Typeface.Serif;
            }
            else
            {
                tf = Typeface.Serif;
            }

            return tf;
        }

        public int ToDIP(int value)
        {
            float density = Application.Context.Resources.DisplayMetrics.Density;

            return (int)(value * density);
        }

        public int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Application.Context.Resources.DisplayMetrics.Density);
            return dp;
        }

        public DisplayMetricsDensity GetScreenDensity(Display display)
        {
            Android.Util.DisplayMetrics metrics = new Android.Util.DisplayMetrics();
            display.GetMetrics(metrics);

            switch (metrics.DensityDpi)
            {
                case DisplayMetricsDensity.Low:
                    return DisplayMetricsDensity.Low;
                case DisplayMetricsDensity.Medium:
                    return DisplayMetricsDensity.Medium;
                case DisplayMetricsDensity.High:
                    return DisplayMetricsDensity.High;
                case DisplayMetricsDensity.Xhigh:
                    return DisplayMetricsDensity.Xhigh;
                case DisplayMetricsDensity.Tv:
                    return DisplayMetricsDensity.Tv;
            }

            return DisplayMetricsDensity.Default;
        }

        public void AlertBox(Context context, string title, string message = null)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(context);
            dialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            dialog.SetPositiveButton("Ok",
                (o, args) =>
                {
                    // Do something
                });
            dialog.Show();
        }

        public void MessageBox(Context context, string title, string message = null)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(context);
            dialog.SetIcon(Resource.Drawable.Icon);
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            dialog.SetPositiveButton("Ok",
                (o, args) =>
                {
                    // Do something
                });
            dialog.Show();
        }

        public Dialog GoToArticleDialog(Context context)
        {
            var alert = new AlertDialog.Builder(context);
            EditText input = new EditText(context);
            input.Hint = "Enter Watchtower Article Number";

            alert.SetTitle("Go To Watchtower Article");
            alert.SetMessage(Html.FromHtml("<i>Article Number:</i>"));
            alert.SetView(input);

            alert.SetPositiveButton("Go", delegate
                    {
                        WolArticle = input.Text;
                        ReaderKind = NWTBible.ReaderKind.WOLReader;
                        var intent = new Intent(context, typeof(ReaderActivity));
                        context.StartActivity(intent);
                    });

            alert.SetNegativeButton("X",
                (o2, args2) =>
                {

                });

            return alert.Create();
        }

        public void QueryBox(Context context, string query, string positiveTitle, string negativeTitle, EventHandler<DialogClickEventArgs> ok = null, EventHandler<DialogClickEventArgs> cancel = null)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(context);
            dialog.SetTitle(query);
            dialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            dialog.SetPositiveButton(positiveTitle, ok);
            dialog.SetNegativeButton(negativeTitle, cancel);
            dialog.Show();
        }

        public Dialog PresentationDialog(Activity activity, string title, string message = "", bool save = true)
        {
            LayoutInflater inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
            View layout = inflater.Inflate(Resource.Layout.HTMLWebView, null);

            AlertDialog.Builder dialog = new AlertDialog.Builder(activity, Android.Resource.Style.ThemeDeviceDefaultNoActionBarFullscreen);
            dialog.SetTitle(title);
            dialog.SetIcon(Resource.Drawable.Icon);
            dialog.SetNegativeButton("X",
                (o, args) =>
                {
                    // Do something
                });

            NotesDbAdapter dbHelper = new NotesDbAdapter(activity);
            dbHelper.Open();

            if (save)
            {
                dialog.SetPositiveButton("Save",
                    (o, args) =>
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(activity, Android.Resource.Style.ThemeDeviceDefaultNoActionBarFullscreen);
                        EditText input = new EditText(activity);
                        input.Hint = "favorite title";

                        alert.SetTitle("Add Favorite - " + title);
                        alert.SetMessage(Html.FromHtml("<i>Favorite Title:</i>"));
                        alert.SetView(input);

                        alert.SetPositiveButton("Save",
                            (o2, args2) =>
                            {
                                long id = dbHelper.CreateNote(title, message, input.Text, " ", "GEMFAVORITE");
                            });

                        alert.SetNegativeButton("X",
                           (o2, args2) =>
                           {

                           });
                        alert.Show();
                    });
            }

            //TextView textview = layout.FindViewById<TextView>(Resource.Id.htmltextbox);
            //textview.MovementMethod = LinkMovementMethod.Instance;
            //textview.SetText(Html.FromHtml(message), TextView.BufferType.Normal);

            WebView PresentationWebView = layout.FindViewById<WebView>(Resource.Id.htmlWebView);
            var client = new NWTBibleWebView(activity, Language);
            PresentationWebView.SetWebViewClient(client);
            PresentationWebView.Settings.JavaScriptEnabled = true;
            PresentationWebView.Settings.PluginsEnabled = true;
            PresentationWebView.Settings.BuiltInZoomControls = true;
            PresentationWebView.Settings.DefaultFontSize = UserFontSize(activity);
            PresentationWebView.LoadDataWithBaseURL("file:///android_asset/", message, "text/html", "utf-8", null);

            dialog.SetView(layout);

            return dialog.Create();

        }        

        public Dialog SuperDialog(Context context, string query, string message = "", string positiveTitle = "Ok", string negativeTitle = "Cancel", EventHandler<DialogClickEventArgs> ok = null, EventHandler<DialogClickEventArgs> cancel = null, bool cancelable = false, string neutralTitle = "", EventHandler<DialogClickEventArgs> neutral = null)
        {
            var builder = new AlertDialog.Builder(context);
            builder.SetIcon(Resource.Drawable.Icon);
            builder.SetTitle(query);
            builder.SetCancelable(cancelable);

            if (!string.IsNullOrEmpty(message))
            {
                builder.SetMessage(message);
            }

            builder.SetPositiveButton(positiveTitle, ok);
            builder.SetNegativeButton(negativeTitle, cancel);

            if (!string.IsNullOrEmpty(neutralTitle))
            {
                builder.SetNeutralButton(neutralTitle, neutral);
            }            

            return builder.Create();
        }

        public string Generate3LinesHTML(int index)
        {
            string html;

            html = "<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" /><meta name=\"viewport\" content=\"width=320\" /><link rel=\"stylesheet\" href=\"ui/web/Site.css\" type=\"text/css\"/><script src=\"ui/web/verseselection.jsf\"></script></head><body><div class=\"page\">\n";

            XDocument doc = GetNWTFile("Chinese Simplified Pinyin", selectedBook.Name.ToUpper());
            var chapterVerses = doc.Descendants("c").ElementAt(index - 1);

            int numVerses = chapterVerses.Descendants("v").Count();

            Console.WriteLine(chapterVerses);
            Console.WriteLine(doc.Descendants("c").Descendants("v"));

            string[] hanziVersesArray = new string[numVerses];
            string[] pinyinVersesArray = new string[numVerses];

            for (var i = 0; i < numVerses; i++)
            {
                hanziVersesArray[i] = chapterVerses.Descendants("v").ElementAt(i).Element("hanzi").Value;
                pinyinVersesArray[i] = chapterVerses.Descendants("v").ElementAt(i).Element("pinyin").Value;
            }

            for (var j = 0; j < numVerses; j++)
            {
                html += "<table class=\"main\"><tr><td>\n";
                html += "<table class=\"ggc\"><tr><td class=\"cgc\"><span class=\"simsun\"><b>" + (j + 1).ToString() + "</b></span></td></tr></table>";

                string[] hanziArray = hanziVersesArray[j].Split('|');
                string[] pinyinArray = pinyinVersesArray[j].Split('|');

                for (var k = 0; k < hanziArray.Length; k++)
                {

                    string[] formattedMaterial = CorrectHanziPinyin(hanziArray[k], pinyinArray[k]);

                    string character = formattedMaterial[0];
                    string pinyin = formattedMaterial[1];

                    html += "<table class=\"ggc\"><tr><td class=\"cgc\"><b>" + pinyin + "</b><br><span class=\"simsun\">" + character + "</span></td></tr></table>";
                }

                html += "\n</td></tr></table>\n";
            }

            html += "</div>\n</body>\n</html>";

            return html;
        }

        private string[] CorrectHanziPinyin(string hanzi, string pinyin)
        {
            string[] output = new string[2];

            pinyin = (hanzi == "的" && pinyin == "dí") ? "de" : pinyin;
            pinyin = (hanzi == "了" && pinyin == "liǎo") ? "le" : pinyin;
            pinyin = (hanzi == "说" && pinyin == "shuì") ? "shuō" : pinyin;
            pinyin = (hanzi == "好" && pinyin == "hào") ? "hǎo" : pinyin;
            pinyin = (hanzi == "和" && pinyin == "huò") ? "hé" : pinyin;
            pinyin = (hanzi == "从" && pinyin == "zòng") ? "cóng" : pinyin;
            pinyin = (hanzi == "要" && pinyin == "yāo") ? "yào" : pinyin;
            pinyin = (hanzi == "把" && pinyin == "bà") ? "bǎ" : pinyin;
            pinyin = (hanzi == "都" && pinyin == "dū") ? "dōu" : pinyin;
            pinyin = (hanzi == "呢" && pinyin == "ní") ? "ne" : pinyin;
            pinyin = (hanzi == "还" && pinyin == "huán") ? "hái" : pinyin;

            output[0] = hanzi;
            output[1] = pinyin;

            return output;
        }

        public NoteScripture FormatScriptureHeading(List<BibleVerse> verses)
        {
            NoteScripture scripture = new NoteScripture();

            string tstring = "";

            string scriptureHeading = "";
            string scriptureText = "";

            List<string> temp = new List<string>();

            // Number of selected verses				
            int num = verses.Count;

            if (num > 0)
            {
                foreach (var i in verses)
                {
                    temp.Add(i.VerseNumber);
                    scriptureText += i.VerseNumber + " " + i.Scripture + " ";
                }

                if (num >= 1)
                {
                    tstring = "";

                    List<string> group = new List<string>();

                    for (var j = 0; j < temp.Count; j++)
                    {
                        int current = int.Parse(temp[j]);
                        int next;
                        int space;

                        if (temp.Count == 1)
                        {
                            space = 0;
                        }
                        else
                        {
                            // If there is a next
                            next = j + 1;
                            if (next < temp.Count)
                            {
                                space = System.Math.Abs(int.Parse(temp[j + 1]) - current);
                            }
                            else
                            {
                                space = 0;
                            }

                        }

                        // If there is a break between selection
                        if (space >= 2)
                        {
                            group.Add(temp[j]);

                            if (group.Count == 2)
                            {
                                tstring += group[0] + ", " + group[group.Count - 1] + ", ";
                            }
                            else if (group.Count >= 3)
                            {
                                tstring += group[0] + "-" + group[group.Count - 1] + ", ";
                            }
                            else
                            {
                                tstring += temp[j] + ", ";
                            }
                            group = new List<string>();
                        }
                        // If there is not a break between selection
                        else if (space == 1)
                        {
                            group.Add(temp[j]);
                        }
                        // If there are no verses after the current selection
                        else
                        {
                            if (num == 1)
                            {
                                tstring += temp[j];
                            }
                            else
                            {
                                group.Add(temp[j]);

                                if (group.Count == 2)
                                {
                                    tstring += group[0] + ", " + group[group.Count - 1];
                                }
                                else if (group.Count >= 3)
                                {
                                    tstring += group[0] + "-" + group[group.Count - 1];
                                }
                                else
                                {
                                    tstring += temp[j];
                                }
                                group = new List<string>();
                            }
                        }
                    }
                }

                //scriptureHeading = selectedBook.Title.ToUpper() + " " + selectedChapter.ChapterNumber + ":" + tstring;
                scriptureHeading = selectedBook.Title + " " + selectedChapter.ChapterNumber + ":" + tstring;

                scripture.Title = scriptureHeading;
                scripture.Scripture = scriptureText;
            }
            else
            {
                scripture = null;
            }

            return scripture;
        }

        public string FormatScriptureHeading(string book, string chapter, string[] verses)
        {
            string scriptureHeading = "";

            string tstring = "";

            List<string> temp = new List<string>();

            // Number of verses				
            int num = verses.Length;

            if (num > 0)
            {
                foreach (string verse in verses)
                {
                    temp.Add(verse);
                }

                if (num >= 1)
                {
                    tstring = "";

                    List<string> group = new List<string>();

                    for (var j = 0; j < temp.Count; j++)
                    {
                        int current = int.Parse(temp[j]);
                        int next;
                        int space;

                        if (temp.Count == 1)
                        {
                            space = 0;
                        }
                        else
                        {
                            // If there is a next
                            next = j + 1;
                            if (next < temp.Count)
                            {
                                space = System.Math.Abs(int.Parse(temp[j + 1]) - current);
                            }
                            else
                            {
                                space = 0;
                            }

                        }

                        // If there is a break between selection
                        if (space >= 2)
                        {
                            group.Add(temp[j]);

                            if (group.Count == 2)
                            {
                                tstring += group[0] + ", " + group[group.Count - 1] + ", ";
                            }
                            else if (group.Count >= 3)
                            {
                                tstring += group[0] + "-" + group[group.Count - 1] + ", ";
                            }
                            else
                            {
                                tstring += temp[j] + ", ";
                            }
                            group = new List<string>();
                        }
                        // If there is not a break between selection
                        else if (space == 1)
                        {
                            group.Add(temp[j]);
                        }
                        // If there are no verses after the current selection
                        else
                        {
                            if (num == 1)
                            {
                                tstring += temp[j];
                            }
                            else
                            {
                                group.Add(temp[j]);

                                if (group.Count == 2)
                                {
                                    tstring += group[0] + ", " + group[group.Count - 1];
                                }
                                else if (group.Count >= 3)
                                {
                                    tstring += group[0] + "-" + group[group.Count - 1];
                                }
                                else
                                {
                                    tstring += temp[j];
                                }
                                group = new List<string>();
                            }
                        }
                    }
                }

                scriptureHeading = book + " " + chapter + ":" + tstring;
            }

            return scriptureHeading;
        }

        public int UserFontSize(Context context)
        {
            int size = 0;
            string fontsize = PreferenceManager.GetDefaultSharedPreferences(context).GetString("listFontSize", "Standard");
            if (fontsize != "Standard")
            {
                if (fontsize == "Particle of a Letter")
                {
                    size = 128;
                }
                if (fontsize == "Superman Vision")
                {
                    size = 48;
                }
                else if (fontsize == "Large")
                {
                    size = 32;
                }
                else if (fontsize == "Medium")
                {
                    size = 24;
                }
                else if (fontsize == "Small")
                {
                    size = 16;
                }
                else if (fontsize == "Almost Impossible")
                {
                    size = 8;
                }
            }
            else
            {
                size = 20;
            }

            return size;
        }

        static public Drawable GetAndroidDrawable(string pDrawableName)
        {
            int resourceId = Application.Context.Resources.GetIdentifier(pDrawableName, "drawable", "android");
            if (resourceId == 0)
            {
                return null;
            }
            else
            {
                return Application.Context.Resources.GetDrawable(resourceId);
            }
        }

        public void SaveNoteToParse(long rowId, string noteTitle, string noteBody, string noteScripture, string noteScriptureText, string noteScriptureForHighlight)
        {
            if (!ConnectedToNetwork(context))
            {
                return;
            }

            selectedNote.Id = int.Parse(rowId.ToString());
            
            // Get all selected verses    
            string[] verses = noteScriptureForHighlight.Split(',');
            string verseList = "";
            int pos = 2;
            while (pos < verses.Length)
            {
                string v = verses[pos].ToString();
                verseList += v;

                if (pos < (verses.Length - 1))
                {
                    verseList += ",";
                }

                pos++;
            }

            // Initialize Parse
            Parse.Initialize(context, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");

            ParseObject testObject = new ParseObject("Note");
            testObject.Put("book", selectedBook.Title);
            testObject.Put("bookName", selectedBook.Name);
            testObject.Put("chapter", selectedChapter.ChapterNumber);
            testObject.Put("language", Language);
            testObject.Put("name", JWName);
            testObject.Put("title", noteTitle);
            testObject.Put("note", noteBody);
            testObject.Put("text", noteScriptureText);
            testObject.Put("verse", verseList);
            testObject.SaveInBackground(new MySaveCallback(testObject, selectedNote, context));
        }

        public void UpdateNoteToParse(long rowId, string noteTitle, string noteBody)
        {
            if (!ConnectedToNetwork(context))
            {
                return;
            }

            NoteScripture n = GetNoteByParseId(rowId);

            // Initialize Parse
            Parse.Initialize(context, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");

            ParseQuery notes = new ParseQuery("Note");
            notes.GetInBackground(n.NWTId, new MyGetCallback(n, "update"));
        }

        public void DeleteNoteFromParse(NoteScripture note)
        {
            if (!ConnectedToNetwork(context))
            {
                return;
            }

            // Initialize Parse
            Parse.Initialize(context, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");

            ParseQuery notes = new ParseQuery("Note");
            notes.GetInBackground(note.NWTId, new MyGetCallback(note, "delete"));
        }

        public NoteScripture GetNoteByParseId(long rowId)
        {
            NotesDbAdapter dbHelper;
            dbHelper = new NotesDbAdapter(context);
            dbHelper.Open();

            ICursor note = dbHelper.FetchNote(rowId);

            NoteScripture n = new NoteScripture()
            {
                Id = int.Parse(note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyRowId))),
                Title = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyScriptureTitle)),
                Scripture = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyScriptureContent)),
                ScriptureForHighlight = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyScriptureForHighlight)),
                NoteTitle = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyTitle)),
                NoteBody = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyBody)),
                NWTId = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyNWTId))
            };

            return n;
        }

        public void SyncUserNotesFromParse(NotesActivity notesActivity)
        {
            if (!ConnectedToNetwork(context))
            {
                return;
            }

            Parse.Initialize(context, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");

            ParseQuery query = new ParseQuery("Note");
            query.WhereEqualTo("name", ParseUser.CurrentUser.Username);
            query.FindInBackground(new MyFindCallback(notesActivity));
        }

        public void SyncUserNotesToParse(NotesActivity notesActivity, List<NoteScripture> notes)
        {
            if (!ConnectedToNetwork(context))
            {
                return;
            }

            Parse.Initialize(context, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");

            List<ParseObject> parseItems = new List<ParseObject>();
            List<NoteScripture> dbNotes = new List<NoteScripture>();

            foreach (NoteScripture note in notes)
            {
                ParseQuery query = new ParseQuery("Note");
                query.WhereEqualTo("objectId", note.NWTId);
                IEnumerable<ParseObject> existedObjects = query.Find();

                // Does not exist in Parse
                if (existedObjects.Count() == 0)
                {
                    ParseObject testObject = new ParseObject("Note");
                    testObject.Put("book", note.Book);
                    testObject.Put("bookName", note.Book);
                    testObject.Put("chapter", note.Chapter);
                    testObject.Put("name", ParseUser.CurrentUser.Username);
                    testObject.Put("title", note.NoteTitle);
                    testObject.Put("note", note.NoteBody);
                    testObject.Put("text", note.Scripture);
                    testObject.Put("verse", note.Verses);
                    testObject.Put("nwtId", note.NWTId);          

                    parseItems.Add(testObject);
                    dbNotes.Add(note);
                }
                // Exists in Parse
                else if (existedObjects.Count() == 1)
                {
                    query.GetInBackground(note.NWTId, new MyGetCallback(note, "update"));
                }
            }

            ParseObject.SaveAllInBackground(parseItems, new MySyncUploadSaveCallback(notesActivity, parseItems, dbNotes));
        }

        /// <summary>  
        ///  The Vice- Unicode  
        /// </summary>  
        /// <param name="text"> The string to convert </param>  
        /// <returns></returns>  
        public string GBToUnicode(string text)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(text);
            string lowCode = "", temp = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    temp = System.Convert.ToString(bytes[i], 16);// Remove element 4 encoding content ( Two-digit hexadecimal )  
                    if (temp.Length < 2) temp = "0" + temp;
                }
                else
                {
                    string mytemp = Convert.ToString(bytes[i], 16);
                    if (mytemp.Length < 2) mytemp = "0" + mytemp; lowCode = lowCode + @"\u" + mytemp + temp;// Remove element 4 encoding content ( Two-digit hexadecimal )  
                }
            }
            return lowCode;
        }

        /// <summary>  
        ///  Converts a Unicode character   
        /// </summary>  
        /// <param name="name"> The string to convert </param>  
        /// <returns></returns>  
        public string UnicodeToGB(string text)
        {
            MatchCollection mc = Regex.Matches(text, "([\\w]+)|(\\\\u([\\w]{4}))");
            if (mc != null && mc.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (Match m2 in mc)
                {
                    string v = m2.Value;
                    if (v.StartsWith("\\u"))
                    {
                        string word = v.Substring(2);
                        byte[] codes = new byte[2];
                        int code = Convert.ToInt32(word.Substring(0, 2), 16);
                        int code2 = Convert.ToInt32(word.Substring(2), 16);
                        codes[0] = (byte)code2;
                        codes[1] = (byte)code;
                        sb.Append(Encoding.Unicode.GetString(codes));
                    }
                    else
                    {
                        sb.Append(v);
                    }
                }
                return sb.ToString();
            }
            else
            {
                return text;
            }
        } 
    }

    public class MySyncUploadSaveCallback : SaveCallback
    {
        NotesActivity _notesActivity;
        List<ParseObject> _parseItems;
        List<NoteScripture> _dbNotes;

        public MySyncUploadSaveCallback(NotesActivity notesActivity, List<ParseObject> parseItems, List<NoteScripture> dbNotes)
        {
            _notesActivity = notesActivity;
            _parseItems = parseItems;
            _dbNotes = dbNotes;
        }

        public override void Done(Xamarin.Parse.ParseException e)
        {
            for (var i = 0; i < _dbNotes.Count; i++)
            {
                _parseItems[i].Put("nwtId", _parseItems[i].ObjectId);
                _parseItems[i].SaveInBackground();

                // Update the note in the database to have the same id as in parse
                _notesActivity.dbHelper.UpdateNoteNWTId(new Long(_dbNotes[i].Id.ToString()).LongValue(), _parseItems[i].ObjectId);
            }

            ThisApp.AlertBox(_notesActivity, "SUCCESS", "Your notes are synced!");
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class MySaveCallback : SaveCallback
    {
        private NoteScripture _n;
        private ParseObject _testObject;

        private NotesDbAdapter dbHelper;
        
        public MySaveCallback(ParseObject testObject, NoteScripture n, Context context)
        {
            _testObject = testObject;
            _n = n;

            dbHelper = new NotesDbAdapter(context);
            dbHelper.Open();
        }

        public override void Done(Xamarin.Parse.ParseException e)
        {
            _n.NWTId = _testObject.ObjectId;

            _testObject.Put("nwtId", _testObject.ObjectId);
            _testObject.SaveInBackground();

            // Update the note in the database to have the same id as in parse
            dbHelper.UpdateNoteNWTId(new Long(_n.Id.ToString()).LongValue(), _testObject.ObjectId);            
        }
    }

    public class MyGetCallback : GetCallback
    {
        private NoteScripture _n;
        private string _operation = "";


        public MyGetCallback(NoteScripture n, string operation)
        {
            _n = n;
            _operation = operation;            
        }

        public override void Done(ParseObject testObject, Xamarin.Parse.ParseException e)
        {
            if (testObject != null)
            {
                switch (_operation)
                {
                    case "delete":
                        testObject.DeleteInBackground();
                        return;
                    case "update":
                        testObject.Put("title", _n.NoteTitle);
                        testObject.Put("note", _n.NoteBody);
                        testObject.SaveInBackground();
                        return;
                }   
            }
        }
    }

    public class MyFindCallback : FindCallback
    {
        NotesActivity _notesActivity;

        public MyFindCallback(NotesActivity notesActivity)
        {
            _notesActivity = notesActivity;
        }

        public override void Done(IList<ParseObject> list, Xamarin.Parse.ParseException e)
        {
            _notesActivity.dbHelper.DeleteAllNotes();

            for (var i = 0; i < list.Count; i++)
            {
                ParseObject item = list[i];

                NoteScripture note = new NoteScripture()
                {
                    NoteBody = item.GetString("note"),
                    NoteTitle = item.GetString("title") ?? "",
                    NWTId = item.ObjectId,
                    Scripture = item.GetString("text"),
                    ScriptureForHighlight = item.GetString("bookName") + "," + item.GetString("chapter") + "," + item.GetString("verse"),
                    Title = ThisApp.FormatScriptureHeading(item.GetString("bookName"), item.GetString("chapter"), item.GetString("verse").Split(','))
                };

                long id = _notesActivity.dbHelper.CreateNote(note);
            }

            // Update my notes list
            _notesActivity.FillData();

            ThisApp.AlertBox(_notesActivity, "SUCCESS", "Your notes are synced!\n\n" + list.Count + " notes downloaded.");
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