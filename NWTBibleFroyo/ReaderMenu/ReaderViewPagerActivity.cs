using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Webkit;

using NWTBible.NotesMenu;
using NWTBible.ReaderMenu;

using SlidingMenuBinding.Lib.App;
using SlidingMenuBinding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Xamarin.ActionbarSherlockBinding.Views;

using IMenu = global::Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = global::Xamarin.ActionbarSherlockBinding.Views.IMenuItem;

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible", Theme = "@style/Theme.Sherlock")]
    public class ReaderViewPagerActivity : SlidingFragmentActivity
    {
        private ViewPager viewPager;
        private Android.Support.V4.App.Fragment leftMenuFragment;
        private Android.Support.V4.App.Fragment rightMenuFragment;

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            RequestWindowFeature(WindowFeatures.ActionBar);
            SetSlidingActionBarEnabled(true);

            // Left menu
            if (PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetBoolean("notesReplace", false))
            {
                leftMenuFragment = new NotesFragment();
                rightMenuFragment = new MenuFragmentAdvanced();
            }
            else
            {
                leftMenuFragment = new MenuFragmentAdvanced();
                rightMenuFragment = new NotesFragment();
            }

            SetContentView(Resource.Layout.ViewPagerLayout);

            // Setup the left menu
            SetBehindContentView(Resource.Layout.MenuFrame);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.MenuFrame, leftMenuFragment).Commit();

            // Setup the right menu
            SlidingMenu.SetSecondaryMenu(Resource.Layout.MenuFrameSecondary);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.MenuFrameSecondary, rightMenuFragment).Commit();

            // Get display dimensions of current oriented screen
            Display display = WindowManager.DefaultDisplay;
            Android.Util.DisplayMetrics metrics = new Android.Util.DisplayMetrics();
            display.GetMetrics(metrics);

            int width = (int)(metrics.WidthPixels * .333);

            // Setup the sliding menu
            SlidingMenu.Mode = SlidingMenuBinding.Lib.SlidingMenu.LeftRight;
            SlidingMenu.SetShadowWidthRes(Resource.Dimension.SlidingMenuShadowWidth);
            SlidingMenu.SetShadowDrawable(Resource.Drawable.SlidingMenuShadow);
            SlidingMenu.SetBehindWidth(width * 2);
            SlidingMenu.SetFadeDegree(0.35f);
            SlidingMenu.TouchModeAbove = SlidingMenuBinding.Lib.SlidingMenu.TouchmodeMargin;

            // Enable this so that we can toggle the menu when clicking the action bar home button in `OnOptionsItemSelected`
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            InitializeView();
            
            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ThisApp.doHighlight = false;

            ThisApp.LanguageChanged -= ThisApp_LanguageChanged;
        }

        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {

            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {

            }
            else if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Reset the global book
                int index = int.Parse(ThisApp.selectedBook.Number) - 1;
                ThisApp.selectedBook = ThisApp.allBibleBooks[index];

                // Reset the global chapter
                string chapter = ThisApp.selectedChapter.ChapterNumber;
                ThisApp.selectedChapter = new BibleChapter()
                {
                    Book = ThisApp.selectedBook,
                    ChapterNumber = chapter
                };

                // Reload chapter data
                XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
                List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
                ThisApp.allBookChapters = collection;
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {

            }

            this.Finish();

            Intent intent = new Intent(this, ThisApp.MainReader.Class);
            this.StartActivity(intent);
        }

        private void InitializeView()
        {
            if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {

            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {                
                XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedPublication.Image);
                List<PublicationArticle> collection = doc.Descendants("article")
                    .Select(article => new PublicationArticle
                    {
                        ArticleTitle = article.Element("title").Value,
                        Content = article.Element("content").Value,
                        DocumentLocation = article.Element("documentLocation").Value,
                        PublicationTitle = ThisApp.selectedPublication.Image
                    }).ToList();

                ThisApp.allPublicationArticles = collection;

                var actionBar = SupportActionBar;
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                var pagerAdapter = new PublicationPagerAdapter(SupportFragmentManager);

                viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
                viewPager.Adapter = pagerAdapter;
                viewPager.SetOnPageChangeListener(new PageChangeListener(viewPager));
                //viewPager.SetPageTransformer(true, new DepthPageTransformer());
                viewPager.CurrentItem = (int)(ThisApp.selectedPublication.CurrentPage - 1);

                Title = ThisApp.selectedPublication.ShortTitle;
            }
            else if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
                List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
                ThisApp.allBookChapters = collection;

                var actionBar = SupportActionBar;
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                var pagerAdapter = new BiblePagerAdapter(SupportFragmentManager);

                viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
                viewPager.Adapter = pagerAdapter;
                viewPager.SetOnPageChangeListener(new PageChangeListener(viewPager));
                //viewPager.SetPageTransformer(true, new DepthPageTransformer());
                viewPager.CurrentItem = (int)(int.Parse(ThisApp.selectedChapter.ChapterNumber) - 1);

                Title = ThisApp.selectedBook.Title;
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {

            }
            
        }

        public void UpdatePager(int index)
        {
            RunOnUiThread(() =>
            {
                SetContentView(Resource.Layout.ViewPagerLayout);

                XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
                List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
                ThisApp.allBookChapters = collection;

                var pagerAdapter = new BiblePagerAdapter(SupportFragmentManager);

                viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
                viewPager.Adapter = pagerAdapter;
                viewPager.SetOnPageChangeListener(new PageChangeListener(viewPager));
                viewPager.CurrentItem = index - 1;

                Title = ThisApp.selectedBook.Title;

                SlidingMenu.ShowContent();
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                var noteMenu = menu.Add(0, NOTE_MENU, NOTE_MENU, "Note");
                noteMenu.SetShowAsAction(1);
                noteMenu.SetIcon(Android.Resource.Drawable.IcMenuAdd);

                var translateMenu = menu.Add(0, TRANSLATE_MENU, TRANSLATE_MENU, "Translate");
                translateMenu.SetShowAsAction(1);
                translateMenu.SetIcon(Android.Resource.Drawable.IcMenuRotate);
            }

            if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                //var noteMenu = menu.Add(0, NOTE_MENU, NOTE_MENU, "Note");
                //noteMenu.SetShowAsAction(1);
                //noteMenu.SetIcon(Android.Resource.Drawable.IcMenuAdd);

                var translateMenu = menu.Add(0, TRANSLATE_MENU, TRANSLATE_MENU, "Translate");
                translateMenu.SetShowAsAction(1);
                translateMenu.SetIcon(Android.Resource.Drawable.IcMenuRotate);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        private const int TRANSLATE_MENU = 1;
        private const int NOTE_MENU = 9;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    Toggle();
                    return true;
                case (TRANSLATE_MENU):
                    ThisApp.TranslateLanguageDialog(this).Show();
                    return true;
                case (NOTE_MENU):
                    string[] verses = new string[ThisApp.selectedVerses.Count];
                    if (ThisApp.selectedVerses.Count == 0)
                    {
                        return true;
                    }
                    var intent = new Intent(this, typeof(NoteEditActivity));
                    StartActivity(intent);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class PageChangeListener : ViewPager.SimpleOnPageChangeListener
    {
        public ViewPager pager;

        public PageChangeListener(ViewPager pager)
        {
            this.pager = pager;
        }

        public override void OnPageSelected(int position)
        {
            base.OnPageSelected(position);
        }

        public override void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            base.OnPageScrolled(position, positionOffset, positionOffsetPixels);

            if (positionOffset == 0)
            {
                Console.WriteLine("This is UNofficially chapter " + (position + 1).ToString());

                if (ThisApp.ReaderKind == ReaderKind.BibleReader)
                {
                    var adapter = pager.Adapter as BiblePagerAdapter;
                    var frag = adapter.GetItem(position) as PagerReaderFragment;
                    frag.client._currentChapter = (position + 1).ToString();
                    //frag.client.RefreshView(frag.webview);
                    //frag.ReHighlight();
                }
                else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
                {
                    var adapter = pager.Adapter as PublicationPagerAdapter;
                    var frag = adapter.GetItem(position) as PagerReaderFragment;
                    frag.client._currentChapter = (position + 1).ToString();
                    //frag.client.RefreshView(frag.webview);
                    //frag.ReHighlight();
                }
                

                ThisApp.selectedChapter = new BibleChapter()
                {
                    Book = ThisApp.selectedBook,
                    ChapterNumber = (position + 1).ToString()
                };

                ThisApp.selectedNote = new NoteScripture();
            }
        }

        public override void OnPageScrollStateChanged(int state)
        {
            base.OnPageScrollStateChanged(state);
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class BiblePagerAdapter : FragmentStatePagerAdapter
    {

        public BiblePagerAdapter(Android.Support.V4.App.FragmentManager fm)
            : base(fm)
        {

        }

        public override Android.Support.V4.App.Fragment GetItem(int i)
        {
            PagerReaderFragment fragment = null;

            string chapter = !string.IsNullOrEmpty(ThisApp.selectedNote.ScriptureForHighlight) ? ThisApp.selectedNote.ScriptureForHighlight.Split(',')[1] : null;

            Console.WriteLine(chapter);

            if (!string.IsNullOrEmpty(chapter) && chapter == (i + 1).ToString())
            {
                fragment = new PagerReaderFragment(ThisApp.allBookChapters[i], true, (i + 1).ToString());
            }
            else
            {
                fragment = new PagerReaderFragment(ThisApp.allBookChapters[i], false, (i + 1).ToString());
            }

            return fragment;
        }

        // For this contrived example, we have a 100-object collection.
        public override int Count
        {
            get
            {
                return ThisApp.allBookChapters.Count;
            }
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int i)
        {
            return new Java.Lang.String(string.Format("CHAPTER {0}", i + 1));
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class PublicationPagerAdapter : FragmentStatePagerAdapter
    {

        public PublicationPagerAdapter(Android.Support.V4.App.FragmentManager fm)
            : base(fm)
        {

        }

        public override Android.Support.V4.App.Fragment GetItem(int i)
        {
            PagerReaderFragment fragment = null;

            fragment = new PagerReaderFragment(ThisApp.allPublicationArticles[i].Content, false, (i + 1).ToString());

            return fragment;
        }

        // For this contrived example, we have a 100-object collection.
        public override int Count
        {
            get
            {
                return ThisApp.allPublicationArticles.Count;
            }
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int i)
        {
            return new Java.Lang.String(string.Format("{0}", ThisApp.allPublicationArticles[i].ArticleTitle));
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class PagerReaderFragment : Android.Support.V4.App.Fragment
    {
        public WebView webview;
        public WebView webview2;

        public string html;
        public string chapter;
        public bool highlight;

        public NWTBibleWebView client;

        public PagerReaderFragment(string html, bool highlight, string chapter)
        {
            this.html = html;
            this.highlight = highlight;
            this.chapter = chapter;

            client = new NWTBibleWebView(Activity, ThisApp.Language, highlight, null, chapter);
        }

        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);

            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView;

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                rootView = inflater.Inflate(Resource.Layout.DuelReader, container, false);
            }
            else
            {
                rootView = inflater.Inflate(Resource.Layout.Reader, container, false);
            }

            return rootView;
        }

        public override void OnViewCreated(View view, Bundle bundle)
        {
            base.OnViewCreated(view, bundle);
            
            if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {

            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                LoadPublicationReader();  
            }
            else if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                LoadBibleReader();                
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {

            }
        }

        private void LoadPublicationReader()
        {
            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview2 = View.FindViewById<WebView>(Resource.Id.readerWebView2);

            webview.SetWebViewClient(client);
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            //webview.Settings.DisplayZoomControls = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
            webview.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);

                XDocument doc2 = ThisApp.GetNWTFile(lang, ThisApp.selectedPublication.Image);

                List<PublicationArticle> collection2 = doc2.Descendants("article")
                .Select(article => new PublicationArticle
                {
                    ArticleTitle = article.Element("title").Value,
                    Content = article.Element("content").Value,
                    DocumentLocation = article.Element("documentLocation").Value,
                    PublicationTitle = ThisApp.selectedPublication.Image
                }).ToList();

                string html2 = collection2.ElementAt(int.Parse(chapter) - 1).Content;

                ThisApp.selectedPublicationArticle = ThisApp.GetAllPublicationArticles().ElementAt(int.Parse(chapter) - 1);

                webview2.SetWebViewClient(new NWTBibleWebView(Activity, lang, ThisApp.doHighlight, null, ThisApp.selectedPublicationArticle.ArticleTitle));
                webview2.Settings.JavaScriptEnabled = true;
                webview2.Settings.PluginsEnabled = true;
                //webview2.Settings.DisplayZoomControls = true;
                webview2.Settings.BuiltInZoomControls = true;
                webview2.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                webview2.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
                webview2.LoadDataWithBaseURL("file:///android_asset/", html2, "text/html", "utf-8", null);
            } 
        }

        private void LoadBibleReader()
        {
            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview2 = View.FindViewById<WebView>(Resource.Id.readerWebView2);

            webview.SetWebViewClient(client);
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            //webview.Settings.DisplayZoomControls = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
            webview.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);
                                
                XDocument doc = ThisApp.GetNWTFile(ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language), ThisApp.selectedBook.Name.ToUpper());
                string html2 = doc.Descendants("c").ElementAt(int.Parse(chapter) - 1).Value;

                webview2.SetWebViewClient(new NWTBibleWebView(Activity, lang, ThisApp.doHighlight, null, ThisApp.selectedChapter.ChapterNumber));
                webview2.Settings.JavaScriptEnabled = true;
                webview2.Settings.PluginsEnabled = true;
                //webview2.Settings.DisplayZoomControls = true;
                webview2.Settings.BuiltInZoomControls = true;
                webview2.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                webview2.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
                webview2.LoadDataWithBaseURL("file:///android_asset/", html2, "text/html", "utf-8", null);
            }            
        }

        public void ReHighlight()
        {
            RunJs("ClearSelection();");

            // Highlight user highlights
            if (ThisApp.preferences.GetBoolean("recordEveryHighlight", false))
            {
                var chapterVerses = ThisApp.highlightedScriptures;
                List<string> verseNumbersList = new List<string>();
                for (int cv = 0; cv < chapterVerses.Count; cv++)
                {
                    var element = chapterVerses.ElementAt(cv);
                    if (element.Chapter.ChapterNumber.Equals(ThisApp.selectedChapter.ChapterNumber) && element.Book.Name.Equals(ThisApp.selectedBook.Name))
                    {
                        verseNumbersList.Add(chapterVerses.ElementAt(cv).VerseNumber);
                    }
                }

                foreach (string v in verseNumbersList)
                {
                    RunJs("HighlightVerse('" + EscapeJsString(v) + "');");
                }
            }
        }

        public void RunJs(String js)
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

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class ZoomOutPageTransformer : Java.Lang.Object, ViewPager.IPageTransformer
    {
        private static float MIN_SCALE = 0.85f;
        private static float MIN_ALPHA = 0.5f;

        public void TransformPage(View view, float position)
        {
            int pageWidth = view.Width;
            int pageHeight = view.Height;

            if (position < -1)
            {
                // [-Infinity,-1)
                // This page is way off-screen to the left.
                view.Alpha = 0;

            }
            else if (position <= 1)
            {
                // [-1,1]
                // Modify the default slide transition to shrink the page as well
                float scaleFactor = Math.Max(MIN_SCALE, 1 - Math.Abs(position));
                float vertMargin = pageHeight * (1 - scaleFactor) / 2;
                float horzMargin = pageWidth * (1 - scaleFactor) / 2;
                if (position < 0)
                {
                    //view.TranslationX = (horzMargin - vertMargin / 2);
                }
                else
                {
                    //view.TranslationY = (-horzMargin + vertMargin / 2);
                }

                // Scale the page down (between MIN_SCALE and 1)
                view.ScaleX = (scaleFactor);
                view.ScaleX = (scaleFactor);

                // Fade the page relative to its size.
                view.Alpha = (MIN_ALPHA + (scaleFactor - MIN_SCALE) / (1 - MIN_SCALE) * (1 - MIN_ALPHA));

            }
            else
            {
                // (1,+Infinity]
                // This page is way off-screen to the right.
                view.Alpha = 0;
            }
        }
    }

    public class DepthPageTransformer : Java.Lang.Object, ViewPager.IPageTransformer
    {
        private static float MIN_SCALE = 0.75f;

        public void TransformPage(View view, float position)
        {
            int pageWidth = view.Width;

            if (position < -1)
            { // [-Infinity,-1)
                // This page is way off-screen to the left.
                view.Alpha = 0;

            }
            else if (position <= 0)
            {
                // [-1,0]
                // Use the default slide transition when moving to the left page
                view.Alpha = (1);
                view.TranslationX = (0);
                view.ScaleX = (1);
                view.ScaleY = (1);

            }
            else if (position <= 1)
            {
                // (0,1]
                // Fade the page out.
                view.Alpha = (1 - position);

                // Counteract the default slide transition
                view.TranslationX = (pageWidth * -position);

                // Scale the page down (between MIN_SCALE and 1)
                float scaleFactor = MIN_SCALE + (1 - MIN_SCALE) * (1 - Math.Abs(position));
                view.ScaleX = (scaleFactor);
                view.ScaleY = (scaleFactor);

            }
            else
            { // (1,+Infinity]
                // This page is way off-screen to the right.
                view.Alpha = 0;
            }
        }
    }
}