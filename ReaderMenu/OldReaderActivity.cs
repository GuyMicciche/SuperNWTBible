using System;
using System.Collections.Generic;
using System.Linq;
using Java.Interop;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using System.Xml.Linq;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;

using IMenu = global::Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = global::Xamarin.ActionbarSherlockBinding.Views.IMenuItem;
using ActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using Newtonsoft.Json;
using Android.Preferences;

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible", Icon = "@drawable/icon", Theme = "@style/Theme.Sherlock")]
    public class OldReaderActivity : SherlockActivity
    {
        private GestureDetector gestureDetector;
        private GestureListener gestureListener;

        private List<NavPoint> navPointList;

        private static string dateFormat = @"yyyy-MM-dd";
        private DailyTextDay dailyText = new DailyTextDay();

        private int offset = 0;

        private int numOfChapters = 0;

        private WebView webview;
        private WebView webview2;
        private string html;

        private List<ChapterItem> content;
        private List<BibleChapterItem> biblecontent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Set the view
                SetContentView(Resource.Layout.Reader);

                // Set the title of the ActionBar
                Title = ThisApp.selectedChapter.BookAndChapter;
                LoadBibleReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                SetContentView(Resource.Layout.Reader);
                Title = ThisApp.selectedPublication.ShortTitle;
                LoadPublicationReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                SetContentView(Resource.Layout.DailyText);
                Title = "Daily Text";
                LoadDailyTextReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {
                SetContentView(Resource.Layout.Reader);
                LoadEPUBReader(0);
            }

            //var bar = SupportActionBar;
            //var change = bar.DisplayOptions ^ 1;
            //SupportActionBar.SetDisplayOptions(change, 1);

            //var bar = SupportActionBar;
            //var change = bar.DisplayOptions ^ ActionBarDisplayOptions.HomeAsUp;
            //SupportActionBar.SetDisplayOptions(change, ActionBarDisplayOptions.HomeAsUp);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                if (keyCode == Keycode.Back && webview.CanGoBack())
                {
                    webview.GoBack();

                    if (ThisApp.preferences.GetBoolean("dualWebviews", false))
                    {
                        webview2.GoBack();
                    }
                }
                else
                {
                    Finish();
                }
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
            var actionitem1 = menu.Add(0, 6, 6, "Previous");
            actionitem1.SetShowAsAction(1);
            actionitem1.SetIcon(Android.Resource.Drawable.IcMediaRew);

            var actionitem2 = menu.Add(0, 7, 7, "Next");
            actionitem2.SetShowAsAction(1);
            actionitem2.SetIcon(Android.Resource.Drawable.IcMediaFf);

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                var actionitem3 = menu.Add(0, 8, 8, "Note");
                actionitem3.SetShowAsAction(1);
                actionitem3.SetIcon(Android.Resource.Drawable.IcMenuAdd);
            }

            if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                var actionitem4 = menu.Add(0, 9, 9, "Today");
                actionitem4.SetShowAsAction(1);
                actionitem4.SetIcon(Android.Resource.Drawable.IcMenuToday);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case (6):
                    ThisApp.selectedPublication.CurrentPage--;
                    offset--;
                    if (ThisApp.ReaderKind == ReaderKind.EPUBReader) { ReloadEPUBPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.PublicationReader) { ReloadPublicationPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.BibleReader) { ReloadBiblePage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader) { LoadDailyTextReader(); }
                    return true;
                case (7):
                    ThisApp.selectedPublication.CurrentPage++;
                    offset++;
                    if (ThisApp.ReaderKind == ReaderKind.EPUBReader) { ReloadEPUBPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.PublicationReader) { ReloadPublicationPage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.BibleReader) { ReloadBiblePage(); }
                    else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader) { LoadDailyTextReader(); }
                    return true;
                case (9):
                    offset = 0;
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

            ThisApp.selectedDailyText = dailyText;

            dtDate.SetTypeface(ThisApp.Font(this, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            dtDate.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(this));
            dtScripture.SetTypeface(ThisApp.Font(this, "ionitalic"), Android.Graphics.TypefaceStyle.Normal);
            dtScripture.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(this));
            dtContent.SetTypeface(ThisApp.Font(this, "ionbook"), Android.Graphics.TypefaceStyle.Normal);
            dtContent.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(this));

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
            webview2 = FindViewById<WebView>(Resource.Id.readerWebView2);

            webview.SetWebViewClient(new NWTBibleWebView(this, ThisApp.Language));
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
            webview2 = FindViewById<WebView>(Resource.Id.readerWebView2);

            webview.SetWebViewClient(new NWTBibleWebView(this, ThisApp.Language, ThisApp.doHighlight, navPointList));
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

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                webview2.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
            }

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
            webview2 = FindViewById<WebView>(Resource.Id.readerWebView2);

            webview.SetWebViewClient(new NWTBibleWebView(this, ThisApp.Language, ThisApp.doHighlight));
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            //webview.Settings.DisplayZoomControls = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.AddJavascriptInterface(new ReaderMenu.VerseSelection(this, webview, webview2), "VerseSelection");
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                XDocument doc2 = ThisApp.GetNWTFile(ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language), ThisApp.selectedBook.Name.ToUpper());

                string html2 = "<html><body><h1>" + offset.ToString() + "</h1></body></html>";
                html2 += doc2.Descendants("c").ElementAt(offset - 1);

                webview2 = FindViewById<WebView>(Resource.Id.readerWebView);
                webview2.SetWebViewClient(new NWTBibleWebView(this, ThisApp.Language, ThisApp.doHighlight));
                webview2.Settings.JavaScriptEnabled = true;
                webview2.Settings.PluginsEnabled = true;
                //webview2.Settings.DisplayZoomControls = true;
                webview2.Settings.BuiltInZoomControls = true;
                webview2.AddJavascriptInterface(new ReaderMenu.VerseSelection(this, webview, webview2), "VerseSelection");
                webview2.LoadDataWithBaseURL("file:///android_asset/", html2, "text/html", "utf-8", null);
            }
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

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                webview2.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
            }

            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = offset.ToString()
            };
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
            if (e1.GetX() > e2.GetX())
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