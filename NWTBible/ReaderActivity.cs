using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Interop;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using System.Xml.Linq;
using System.Xml;
using Android.Preferences;

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible", Icon = "@drawable/icon")]
    public class ReaderActivity : Activity
    {
        private GestureDetector gestureDetector;
        private GestureListener gestureListener;

        private List<NavPoint> navPointList;

        private static string dateFormat = @"yyyy-MM-dd";
        private DailyTextDay dailyText = new DailyTextDay();

        private int offset = 0;

        private int numOfChapters = 0;

        private WebView webview;
        private string html;

        private List<ChapterItem> content;
        private List<BibleChapterItem> biblecontent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight));

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Set the view
                SetContentView(Resource.Layout.Reader);

                // Set the title of the ActionBar
                Title = "New World Translation Bible — " + ThisApp.selectedChapter.BookAndChapter;
                LoadBibleReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                SetContentView(Resource.Layout.Reader);
                Title = ThisApp.selectedPublication.Title;
                LoadPublicationReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                SetContentView(Resource.Layout.DailyText);
                Title = "Examining the Scriptures Daily 2013";
                LoadDailyTextReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {
                SetContentView(Resource.Layout.Reader);
                LoadEPUBReader(0);
            }
  
            var bar = ActionBar;
            var change = bar.DisplayOptions ^ ActionBarDisplayOptions.HomeAsUp;
            ActionBar.SetDisplayOptions(change, ActionBarDisplayOptions.HomeAsUp);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                if (keyCode == Keycode.Back && webview.CanGoBack())
                {
                    webview.GoBack();
                }
            }

            else
            {
                Finish();
            }

            return base.OnKeyDown(keyCode, e);
        }

        //public override bool DispatchTouchEvent(MotionEvent ev)
        //{
        //    base.DispatchTouchEvent(ev);
        //    return gestureDetector.OnTouchEvent(ev);
        //}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var actionitem1 = menu.Add(0, 1, 1, "<< Prev");
            actionitem1.SetShowAsAction(ShowAsAction.IfRoom);

            var actionitem2 = menu.Add(0, 2, 2, "Next >>");
            actionitem2.SetShowAsAction(ShowAsAction.IfRoom);
 
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case (1):
                    ThisApp.selectedPublication.CurrentPage--;
                    offset--;
                    if (ThisApp.ReaderKind == ReaderKind.EPUBReader) { ReloadEPUBPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.PublicationReader) { ReloadPublicationPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.BibleReader) { ReloadBiblePage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader) { LoadDailyTextReader(); }
                    return true;
                case (2):
                    ThisApp.selectedPublication.CurrentPage++;
                    offset++;
                    if (ThisApp.ReaderKind == ReaderKind.EPUBReader) { ReloadEPUBPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.PublicationReader) { ReloadPublicationPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.BibleReader) { ReloadBiblePage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader) { LoadDailyTextReader(); }
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (Android.Resource.Id.Home == item.ItemId)
            {
                var top = (new MainActivity()).Class;
                if (this.Class != top)
                {
                    Intent intent = new Intent(this, top);
                    intent.AddFlags(ActivityFlags.ClearTop);
                    StartActivity(intent);
                }
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private void LoadDailyTextReader()
        {
            var dtDate = FindViewById<TextView>(Resource.Id.dtDate);
            var dtScripture = FindViewById<TextView>(Resource.Id.dtScripture);
            var dtContent = FindViewById<TextView>(Resource.Id.dtContent);

            dailyText = ThisApp.GetDailyText(FormatDateTime(DateTime.Now.AddDays(offset)));

            dtDate.SetTypeface(ThisApp.Font(this, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            dtScripture.SetTypeface(ThisApp.Font(this, "ionitalic"), Android.Graphics.TypefaceStyle.Normal);
            dtContent.SetTypeface(ThisApp.Font(this, "ionbook"), Android.Graphics.TypefaceStyle.Normal);

            dtDate.Text = dailyText.Date;
            dtScripture.Text = dailyText.Scripture;
            dtContent.Text = dailyText.Content; 
        }

        private void LoadPublicationReader(int chapter = 0)
        {
            content = new List<ChapterItem>();

            int chapterNum = 0;

            if (chapter != 0)
            {
                // Number of chapters in the selected book
                chapterNum = chapter;
            }
            else
            {
                chapterNum = ThisApp.selectedPublication.CurrentPage;                
            }

            // Load xml document of the book
            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedPublication.Image);

            // Load each chapter
            var query = from node in doc.Descendants("article")
                        select new 
                        {
                            Title = node.Element("title").Value,
                            DocumentLocation = node.Element("documentLocation").Value,
                            Content = node.Element("content").Value
                        };

            foreach (var chap in query)
            {
                content.Add(new ChapterItem
                    {
                        Title = chap.Title,
                        DocumentLocation = chap.DocumentLocation,
                        Content = chap.Content,
                    });
            }

            numOfChapters = content.Count();

            // Get the CDATA of the selected chapter
            html = content.ElementAt(ThisApp.selectedPublication.CurrentPage).Content;
            ThisApp.selectedPublication.ShortTitle = query.ElementAt(ThisApp.selectedPublication.CurrentPage).Title;

            webview = FindViewById<WebView>(Resource.Id.readerWebView);
            webview.SetWebViewClient(new MyWebViewClient(this));
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            webview.Settings.DisplayZoomControls = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);            
        }

        private void LoadEPUBReader(int page)
        {
            XDocument doc = new XDocument();

            var toc = ThisApp.GetEPUBPage(ThisApp.Language, ThisApp.selectedPublication.Code, "toc.ncx");
            if (System.IO.File.Exists(toc.Path))
            {
                using (var stream = new System.IO.StreamReader(toc.Path))
                {
                    doc = XDocument.Parse(stream.ReadToEnd());
                }
            }

            navPointList = new List<NavPoint>();

            foreach (var name in doc.Root.DescendantNodes().OfType<XElement>().Select(x => x.Name))
            {
                //Console.WriteLine(name);
            }

            foreach (var name in doc.Root.Descendants(XName.Get("navPoint", @"http://www.daisy.org/z3986/2005/ncx/")))
            {
                //Console.WriteLine("Entire Element: " + name);
                var toctitle = name.Descendants(XName.Get("text", @"http://www.daisy.org/z3986/2005/ncx/")).First().Value;
                var tocsource = name.Descendants(XName.Get("content", @"http://www.daisy.org/z3986/2005/ncx/")).First().Attribute("src").Value;
                var tocorder = name.Attribute("playOrder").Value;

                NavPoint point = new NavPoint()
                {
                    Title = toctitle,
                    Source = tocsource,
                    Order = int.Parse(tocorder)
                };

                navPointList.Add(point);
            }

            ThisApp.selectedPublication.Title = doc.Root.Descendants(XName.Get("docTitle", @"http://www.daisy.org/z3986/2005/ncx/")).Descendants(XName.Get("text", @"http://www.daisy.org/z3986/2005/ncx/")).First().Value;
            ThisApp.selectedPublication.CurrentPage = page;

            webview = FindViewById<WebView>(Resource.Id.readerWebView);
            webview.SetWebViewClient(new MyWebViewClient(this, ThisApp.doHighlight, navPointList));
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            webview.Settings.DisplayZoomControls = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.LoadUrl("file://" + ThisApp.GetEPUBPage(ThisApp.Language, ThisApp.selectedPublication.Code, navPointList[ThisApp.selectedPublication.CurrentPage].Source).Path);

            //gestureListener = new GestureListener(webview, navPointList, 1);
            //gestureDetector = new GestureDetector(this, gestureListener);
        }

        private void ReloadEPUBPage()
        {            
            if (ThisApp.selectedPublication.CurrentPage == navPointList.Count)
            {
                ThisApp.selectedPublication.CurrentPage = 0;
            }
            else if (ThisApp.selectedPublication.CurrentPage < 0)
            {
                ThisApp.selectedPublication.CurrentPage = navPointList.Count - 1;
            }

            webview.LoadUrl("file://" + ThisApp.GetEPUBPage(ThisApp.Language, ThisApp.selectedPublication.Code, navPointList[ThisApp.selectedPublication.CurrentPage].Source).Path);
        }

        private void ReloadPublicationPage()
        {
            if (ThisApp.selectedPublication.CurrentPage == numOfChapters)
            {
                ThisApp.selectedPublication.CurrentPage = 0;   
            }
            else if (ThisApp.selectedPublication.CurrentPage < 0)
            {
                ThisApp.selectedPublication.CurrentPage = numOfChapters - 1;                 
            }

            html = content.ElementAt(ThisApp.selectedPublication.CurrentPage).Content;
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);  

            ThisApp.selectedPublication.ShortTitle = content.ElementAt(ThisApp.selectedPublication.CurrentPage).Title;
        }
        
        private void LoadBibleReader()
        {
            // Number of chapters in the selected book
            int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber);

            offset = chapterNum;            

            // Load xml document of the book
            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());

            // Load each chapter
            var query = from node in doc.Descendants("c")
                        select new
                        {
                            Data = node.Value
                        };

            numOfChapters = query.Count();

            // Get the CDATA of the selected chapter
            html = query.ElementAt(chapterNum - 1).Data;

            webview = FindViewById<WebView>(Resource.Id.readerWebView);
            webview.SetWebViewClient(new MyWebViewClient(this, ThisApp.doHighlight));
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            webview.Settings.DisplayZoomControls = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.AddJavascriptInterface(new VerseSelection(this, webview), "VerseSelection");
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
        }

        private void ReloadBiblePage()
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
            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());

            // Load each chapter
            var query = from node in doc.Descendants("c")
                        select new
                        {
                            Data = node.Value
                        };

            html = query.ElementAt(offset - 1).Data;
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = offset.ToString()
            };
        }

        public class VerseSelection : Java.Lang.Object
        {
            Context context;
            WebView webview;
            public List<BibleVerse> selectedVerses;
            public string formattedScripture = "";

            public VerseSelection(Context context, WebView webview)
            {
                this.context = context;
                this.webview = webview;
                selectedVerses = new List<BibleVerse>();
            }

            [Export]
            public void AddVerse(string verseNumber, string verseText)
            {
                if (!selectedVerses.Any(x => x.VerseNumber.Equals(verseNumber)))
                {
                    BibleVerse v = new BibleVerse()
                    {
                        Book = ThisApp.selectedBook,
                        Chapter = ThisApp.selectedChapter,
                        VerseNumber = verseNumber,
                        Scripture = verseText
                    };

                    selectedVerses.Add(v);
                }

                if (ThisApp.preferences.GetBoolean("recordEveryHighlight", false))
                {
                    BibleVerse vs = new BibleVerse()
                    {
                        Book = ThisApp.selectedBook,
                        Chapter = ThisApp.selectedChapter,
                        VerseNumber = verseNumber,
                        Scripture = verseText
                    };

                    ThisApp.highlightedScriptures.Add(vs);
                }

                foreach (var i in selectedVerses)
                {
                    //Console.WriteLine(i.ScriptureFormatted);
                }
                Console.WriteLine("There are now " + selectedVerses.Count() + " verses selected.");

                ThisApp.selectedVerses = selectedVerses;
            }

            [Export]
            public void SubtractVerse(string verseNumber)
            {
                if (selectedVerses.Any(x => x.VerseNumber.Equals(verseNumber)))
                {
                    var sel = selectedVerses.Single(s => s.VerseNumber.Equals(verseNumber));
                    selectedVerses.Remove(sel);
                }

                var selected = ThisApp.highlightedScriptures.Single(s => s.VerseNumber.Equals(verseNumber) && s.Chapter.ChapterNumber.Equals(ThisApp.selectedChapter.ChapterNumber));
                ThisApp.highlightedScriptures.Remove(selected);

                Console.WriteLine("There are now " + selectedVerses.Count() + " verses selected.");

                ThisApp.selectedVerses = selectedVerses;
            }

            [Export]
            public void ClearVerses()
            {
                selectedVerses = new List<BibleVerse>();
            }

            [Export]
            public void HighlightVerses()
            {
                if (ThisApp.doHighlight)
                {
                    string[] verses = ThisApp.selectedNote.ScriptureForHighlight.Split(',');
                    for (var i = 3; i < verses.Length; i++)
                    {
                        string v = verses[i];
                        RunJs("HighlightVerse('" + EscapeJsString("4") + "');");
                    }

                    ThisApp.doHighlight = false;
                }
            }

            public void RunJs(String js)
            {
                webview.LoadUrl("javascript:" + js);
                Console.WriteLine("javascript:" + js);
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

            public App ThisApp
            {
                get
                {
                    return App.Instance;
                }
            } 
        }

        public class MyWebViewClient : WebViewClient
        {
            private Activity _context;
            private List<NavPoint> _list;
            public bool _highlight;
            public string _currentChapter;
            public WebView _webview;

            public MyWebViewClient(Activity context, bool highlight = false, List<NavPoint> list = null, string currentChapter = null)
            {
                _context = context;
                _highlight = highlight;
                _list = list;
                _currentChapter = (currentChapter == null) ? ThisApp.selectedChapter.ChapterNumber : currentChapter;
            }

            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);

                return true;
            }

            public override void OnPageFinished(WebView webview, string url)
            {
                base.OnPageFinished(webview, url);

                // Always set the title of the Activity to the current Publication + the current Page
                if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
                {
                    _context.Title = ThisApp.selectedPublication.Title + " — " + webview.Title;
                }
                else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
                {
                    _context.Title = ThisApp.selectedPublication.Title + " — " + ThisApp.selectedPublication.ShortTitle;
                }
                else if (ThisApp.ReaderKind == ReaderKind.BibleReader)
                {
                    //_context.Title = "New World Translation Bible — " + ThisApp.selectedChapter.BookAndChapter;
                    //_context.Title = "New World Translation Bible"; 
                }

                if (_highlight)
                {
                    string[] verses = ThisApp.selectedNote.ScriptureForHighlight.Split(',');
                    int pos = 2;
                    string first = verses[2].ToString();
                    while (pos < verses.Length)
                    {
                        string v = verses[pos].ToString();
                        Console.WriteLine("Verse: " + v);

                        if (v.Equals(first))
                        {
                            RunJs(webview, "ScrollToHighlight('" + EscapeJsString(first) + "');");
                        }

                        if (ThisApp.preferences.GetBoolean("allowHighlighting", true))
                        {
                            RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "');");
                        }

                        pos++;
                    }

                    _highlight = false;
                }
                else
                {
                    Console.WriteLine("NO!!!");

                    RunJs(webview, "ClearSelection();");
                }

                // Highlight user highlights
                if (ThisApp.preferences.GetBoolean("recordEveryHighlight", false))
                {
                    var chapterVerses = ThisApp.highlightedScriptures;
                    List<string> verseNumbersList = new List<string>();
                    for (int cv = 0; cv < chapterVerses.Count; cv++)
                    {
                        var element = chapterVerses.ElementAt(cv);
                        if (element.Chapter.ChapterNumber.Equals(_currentChapter) && element.Book.Name.Equals(ThisApp.selectedBook.Name))
                        {
                            verseNumbersList.Add(chapterVerses.ElementAt(cv).VerseNumber);
                        }
                    }

                    foreach (string v in verseNumbersList)
                    {
                        RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "');");
                    }
                }

                if (!ThisApp.Language.Equals("English"))
                {
                    RunJs(webview, "SwitchFont();");
                }

                if (!ThisApp.preferences.GetBoolean("allowHighlighting", true))
                {
                    RunJs(webview, "DisconnectHighlighting();");
                }

                if (_list != null)
                {
                    // Make the page numbers match what document page is actually loaded in the webview
                    ThisApp.selectedPublication.CurrentPage = _list.First(e => e.Title == webview.Title).Order - 1;
                }

                ThisApp.doHighlight = false;
                ThisApp.selectedVerses = new List<BibleVerse>();
                //ThisApp.highlightedScriptures = new List<BibleVerse>();
                //ThisApp.selectedNote = new NoteScripture();
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

            public void RefreshView(WebView webview)
            {
                if (_highlight)
                {
                    string[] verses = ThisApp.selectedNote.ScriptureForHighlight.Split(',');
                    int pos = 2;
                    string first = verses[2].ToString();
                    while (pos < verses.Length)
                    {
                        string v = verses[pos].ToString();
                        Console.WriteLine("Verse: " + v);

                        if (v.Equals(first))
                        {
                            RunJs(webview, "ScrollToHighlight('" + EscapeJsString(first) + "');");
                        }

                        if (ThisApp.preferences.GetBoolean("allowHighlighting", true))
                        {
                            RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "');");
                        }

                        pos++;
                    }

                    _highlight = false;
                }
                else
                {
                    Console.WriteLine("NO!!!");

                    RunJs(webview, "ClearSelection();");
                }

                // Highlight user highlights
                if (ThisApp.preferences.GetBoolean("recordEveryHighlight", false))
                {
                    var chapterVerses = ThisApp.highlightedScriptures;
                    List<string> verseNumbersList = new List<string>();
                    for (int cv = 0; cv < chapterVerses.Count; cv++)
                    {
                        var element = chapterVerses.ElementAt(cv);
                        if (element.Chapter.ChapterNumber.Equals(_currentChapter) && element.Book.Name.Equals(ThisApp.selectedBook.Name))
                        {
                            verseNumbersList.Add(chapterVerses.ElementAt(cv).VerseNumber);
                        }
                    }

                    foreach (string v in verseNumbersList)
                    {
                        RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "');");
                    }
                }
            }

            public App ThisApp
            {
                get
                {
                    return App.Instance;
                }
            } 
        }

        private string FormatDateTime(DateTime input)
        {
            return input.ToString(dateFormat);
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }  
    }    

    public class GestureListener : GestureDetector.SimpleOnGestureListener
    {
        private WebView _webview;
        private List<NavPoint> _point;
        private int _id;

        public GestureListener(WebView webview, List<NavPoint> point, int id)
        {
            _webview = webview;
            _point = point;
            _id = id;
        }

        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            // Swipe left
            if(e1.GetX() > e2.GetX())
            {
                Console.WriteLine("Left");
                _id++;
                if (_id == _point.Count)
                {
                    _id = 0;
                }
            }
            // Swipe right
            else if (e2.GetX() > e1.GetX())
            {
                Console.WriteLine("Right");
                _id--;
                if (_id < 0)
                {
                    _id = _point.Count - 1;
                }
            }

            _webview.LoadUrl("file://" + ThisApp.GetEPUBPage(ThisApp.Language, "w_E_20130315", _point[_id].Source).Path);

            return base.OnFling(e1, e2, velocityX, velocityY);
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