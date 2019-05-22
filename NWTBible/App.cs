using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.Preferences;
using Android.Util;
using Android.Views;
using NWTBible.NotesMenu;
using NWTBible.ReaderMenu;
using SlidingMenuBinding.Lib.App;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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
                    act = new ReaderSliderActivity();
                }
                else if (ReaderNavigationType.IsSwipingNavigation)
                {
                    act = new ReaderViewPagerActivity();
                }

                return act;
            }
        }

        public WebClient client;
        public Context context;
        public Activity activity;
        private int NumberOfFiles;
        public int FilesCoutner = 0;
        private ProgressDialog progress;
        public List<string> downloadQueue;
        private List<WebClient> allClients = new List<WebClient>();
        public bool allDownloaded = false;
        private List<NWTFile> files;
        private XDocument doc;
        public string[] availableDownloadedLanguages;

        public bool doHighlight = false;

        public BibleBook selectedBook = new BibleBook();
        public BibleChapter selectedChapter = new BibleChapter();
        public Publication selectedPublication = new Publication();
        public DailyTextDay selectedDailyText = new DailyTextDay();
        public NoteScripture selectedNote = new NoteScripture();

        public List<BibleBook> allBibleBooks = new List<BibleBook>();
        public List<string> allBookChapters = new List<string>();
        public List<DailyTextDay> allDailyTexts = new List<DailyTextDay>();

        public List<BibleVerse> selectedVerses = new List<BibleVerse>();

        public List<BibleVerse> highlightedScriptures = new List<BibleVerse>();

        public string ReaderKind;

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
                    Title = query.Attribute("title").Value.ToUpper(),
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
                    Title = query.Attribute("title").Value.ToUpper(),
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
                    Title = query.Attribute("title").Value.ToUpper(),
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
                booksList.Add(query.Attribute("title").Value.ToUpper());
            }

            return booksList;
        }

        public List<DailyTextDay> GetAllDailyTexts(string lang)
        {
            XDocument doc = GetNWTFile(Language, "dailytext");

            List<DailyTextDay> collection = doc.Descendants("daily").Select((element, index) => new DailyTextDay
            {
                Index = (index + 1),
                DateShort = element.Element("sysdate").Value,
                Date = element.Element("textdate").Value,
                Scripture = element.Element("scripture").Value,
                Content = "    " + element.Element("content").Value
            }).ToList();

            // Sort list by sysdate and convert it back to list
            collection = collection.OrderBy(x => x.DateShort.ToString()).ToList();

            return collection;
        }

        public DailyTextDay GetDailyText(string date)
        {
            DailyTextDay item = allDailyTexts.First(e => e.DateShort == date);

            return item;
        }

        public DailyTextDay GetDailyTextByPosition(int position)
        {
            DailyTextDay item = allDailyTexts.First(e => e.Index == position);

            return item;
        }

        public string[] GetAvailableLanguages()
        {
            List<String> languages = new List<String>();
            string url = "http://dl.dropbox.com/u/826238/kingdomtools/languages.xml";

            // This way
            //try
            //{
            //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //    request.Method = "GET";
            //    request.ContentType = "application/xml";
            //    request.BeginGetResponse(new AsyncCallback((iar) =>
            //        {
            //            try
            //            {
            //                HttpWebRequest req = (HttpWebRequest)iar.AsyncState;
            //                HttpWebResponse response = (HttpWebResponse)req.EndGetResponse(iar);
            //                StreamReader reader = new StreamReader(response.GetResponseStream());
            //                XDocument doc = XDocument.Load(reader);

            //                foreach (String lang in doc.Descendants("language"))
            //                {
            //                    languages.Add(lang);
            //                }

            //                response.Close();
            //            }
            //            catch (System.Exception ex)
            //            {

            //            }
            //        }), request);
            //}
            //catch (WebException we)
            //{

            //}

            // Or this way
            //WebClient wc = new WebClient();
            //wc.OpenReadCompleted += (sender, e) =>
            //{
            //    XDocument doc = new XDocument();
            //    using(Stream stream = e.Result)
            //    {
            //        doc = XDocument.Load(stream);
            //    }

            //    foreach(String lang in doc.Descendants("language"))
            //    {
            //        languages.Add(lang);
            //    }
            //};
            //wc.OpenReadAsync(new Uri(url));

            // Or this way

            XDocument doc = XDocument.Load(url);
            foreach (String lang in doc.Descendants("language"))
            {
                languages.Add(lang);
            }

            return languages.ToArray();
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

        private List<String> downloadedLanguages = new List<String>();
        public List<String> DownloadedLanguages
        {
            get
            {
                this.downloadedLanguages = new List<String>();

                bool[] downloaded = IsLanguagesDownloaded(availableDownloadedLanguages);
                string[] languages = availableDownloadedLanguages.ToArray();

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

        public Dialog TranslateLanguageDialog(Context context)
        {
            if (String.IsNullOrEmpty(Language))
            {
                return null;
            }

            // All downloaded languages
            string[] languages = DownloadedLanguages.ToArray();
            // The current selected language
            int index = Array.IndexOf(languages, Language);
            int selected = index;

            var builder = new AlertDialog.Builder(context);
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
            });
            builder.SetNegativeButton("Cancel", (sender, e) =>
            {
                selected = index;
            });

            return builder.Create();
        }

        public Dialog DownloadLanguagePackDialog(Context context)
        {
            if (availableDownloadedLanguages == null)
            {
                availableDownloadedLanguages = GetAvailableLanguages().ToArray();
            }

            //_context = BaseContext;

            // Clear array, reset the queue
            downloadQueue = new List<string>();

            string[] available = availableDownloadedLanguages;

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
                DownloadLanguagesFromDialog(availableDownloadedLanguages, toDownload);
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
            allClients = new List<WebClient>();

            // Languages already downloaded
            bool[] downloaded = IsLanguagesDownloaded(availableDownloadedLanguages);

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
                return;
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

            string[] others = { "hebrew", "greek", "publications", "melodies", "epubs" };
            for (var i = 0; i < others.Length; i++)
            {
                NWTFile file = new NWTFile(others[i] + ".xml", "https://dl.dropbox.com/u/826238/kingdomtools/" + language + "/" + others[i] + ".xml");
                files.Add(file);
            }

            //string[] publications = { "dailytext", "allscripture", "knowledge", "bearingwitness", "biblestories", "bibleteach", "closetojehovah", "godslove", "isaiah1", "isaiah2", "jehovahsday", "jeremiah", "ministryschool", "myfollower", "reasoning", "revelation", "singtojehovah", "teacher", "worshipgod", "youngpeople1", "youngpeople2" };
            string[] publications = { "dailytext" };
            for (var i = 0; i < publications.Length; i++)
            {
                NWTFile file = new NWTFile(publications[i] + ".xml", "https://dl.dropbox.com/u/826238/kingdomtools/" + language + "/books/" + publications[i] + ".xml");
                files.Add(file);
            }

            string[] books = GetAllBookNames();
            for (var i = 0; i < books.Length; i++)
            {
                //string book = books[i].Replace(" ", "%20");
                string book = books[i];

                NWTFile file = new NWTFile(book + ".xml", "https://dl.dropbox.com/u/826238/kingdomtools/" + language + "/bible/" + book + ".xml");
                files.Add(file);
            }

            string[] watchtower = { "ws_E_20130315", "g_E_201303", "wp_E_20130301", "w_E_20130315", "w_E_20130215", "w_E_20130115", "w_E_20121215", "w_E_20121115", "w_E_20121015", "w_E_20120915", "w_E_20120815", "w_E_20120715", "w_E_20120615", "w_E_20120515", "w_E_20120415", "w_E_20120315", "w_E_20120215", "w_E_20120115" };
            for (var i = 0; i < watchtower.Length; i++)
            {
                NWTFile file = new NWTFile(watchtower[i] + ".epub", "https://dl.dropbox.com/u/826238/nwtreader/" + language + "/Watchtower/" + watchtower[i] + ".epub", watchtower[i]);
                //files.Add(file);
            }

            //string[] test = { "English" };
            //for (var i = 0; i < test.Length; i++)
            //{
            //    NWTFile file = new NWTFile(test[i] + ".zip", "https://dl.dropbox.com/u/826238/nwtreader/" + language + "/" + test[i] + ".zip");
            //    files.Add(file);
            //}

            NumberOfFiles = files.Count();
            
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
                }

                client = new WebClient();
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                client.DownloadFileAsync(new System.Uri(files[i].FileURL), file.Path, args);
            }

            progress.Show();

            DownloadedLanguages.Add(language);
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
                    MessageBox(context, "Success!", "All Language Packs you selected have been downloaded!");
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
            float density = Resources.DisplayMetrics.Density;

            return (int)(value * density);
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

        public void QueryBox(Context context, string query, string positiveTitle, string negativeTitle, EventHandler<DialogClickEventArgs> ok = null, EventHandler<DialogClickEventArgs> cancel = null)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(context);
            dialog.SetTitle(query);
            dialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            dialog.SetPositiveButton(positiveTitle, ok);
            dialog.SetNegativeButton(negativeTitle, cancel);
            dialog.Show();
        }

        public Dialog SuperDialog(Context context, string query, string message = "", string positiveTitle = "Ok", string negativeTitle = "Cancel", EventHandler<DialogClickEventArgs> ok = null, EventHandler<DialogClickEventArgs> cancel = null)
        {
            var builder = new AlertDialog.Builder(context);
            builder.SetIcon(Resource.Drawable.Icon);
            builder.SetTitle(query);
            builder.SetCancelable(false);

            if (!String.IsNullOrEmpty(message))
            {
                builder.SetMessage(message);
            }

            builder.SetPositiveButton(positiveTitle, ok);
            builder.SetNegativeButton(negativeTitle, cancel);

            return builder.Create();
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
                                space = Math.Abs(int.Parse(temp[j + 1]) - current);
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

                scriptureHeading = selectedBook.Title.ToUpper() + " " + selectedChapter.ChapterNumber + ":" + tstring;

                scripture.Title = scriptureHeading;
                scripture.Scripture = scriptureText;
            }
            else
            {
                scripture = null;
            }

            return scripture;
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
                    size = 26;
                }
                else if (fontsize == "Small")
                {
                    size = 12;
                }
                else if (fontsize == "Almost Impossible")
                {
                    size = 0;
                }
            }
            else
            {
                size = 18;
            }

            return size;
        }
    }
}