using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using NWTBible.ReaderMenu;
using Android.Webkit;
using System.Xml.Linq;
using SlidingMenuBinding.Lib.App;
using Android.Preferences;
using ActionbarSherlock.View;
using NWTBible.NotesMenu;

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible")]
    public class ReaderViewPagerActivity : SlidingFragmentActivity
    {
        private ViewPager viewPager;
        private Android.Support.V4.App.Fragment menuFragment;

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight));

            RequestWindowFeature(WindowFeatures.ActionBar);
            SetSlidingActionBarEnabled(true);

            // Left menu
            if (PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetBoolean("notesReplace", false))
            {
                menuFragment = new NotesFragment();
            }
            else
            {
                menuFragment = new MenuFragment();
            }

            SetContentView(Resource.Layout.ViewPagerLayout);

            SetBehindContentView(Resource.Layout.MenuFrame);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.MenuFrame, menuFragment).Commit();

            // Setup the sliding menu
            SlidingMenu.SetShadowWidthRes(Resource.Dimension.SlidingMenuShadowWidth);
            SlidingMenu.SetShadowDrawable(Resource.Drawable.SlidingMenuShadow);
            SlidingMenu.SetBehindOffsetRes(Resource.Dimension.SlidingmenuOffset);
            SlidingMenu.SetFadeDegree(0.35f);
            SlidingMenu.TouchModeAbove = SlidingMenuBinding.Lib.SlidingMenu.TouchmodeMargin;

            // Enable this so that we can toggle the menu when clicking the action bar home button in `OnOptionsItemSelected`
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
            InitializeView();
        }

        private void InitializeView()
        {
            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
            List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
            ThisApp.allBookChapters = collection;

            var actionBar = this.ActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            var pagerAdapter = new PagerAdapter(SupportFragmentManager);

            viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = pagerAdapter;
            viewPager.SetOnPageChangeListener(new PageChangeListener(viewPager));
            //viewPager.SetPageTransformer(true, new DepthPageTransformer());
            viewPager.CurrentItem = (int)(int.Parse(ThisApp.selectedChapter.ChapterNumber) - 1);

            Title = "New World Translation Bible — " + ThisApp.selectedBook.Title;
        }

        public void UpdatePager()
        {
            string[] args = ThisApp.selectedNote.ScriptureForHighlight.Split(',');

            RunOnUiThread(() =>
            {
                SetContentView(Resource.Layout.ViewPagerLayout);

                XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
                List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
                ThisApp.allBookChapters = collection;

                var pagerAdapter = new PagerAdapter(SupportFragmentManager);

                viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
                viewPager.Adapter = pagerAdapter;
                viewPager.SetOnPageChangeListener(new PageChangeListener(viewPager));
                viewPager.CurrentItem = int.Parse(args[1]) - 1;

                Title = "New World Translation Bible — " + ThisApp.selectedBook.Title;

                SlidingMenu.ShowContent();
            });         
        }
                
        public override bool OnCreateOptionsMenu(ActionbarSherlock.View.IMenu menu)
        {
            var actionitem3 = menu.Add(0, 1, 1, "+Note");
            actionitem3.SetShowAsAction(1);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(ActionbarSherlock.View.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    Toggle();
                    return true;
                case (1):
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

                var adapter = pager.Adapter as PagerAdapter;
                var frag = adapter.GetItem(position) as BibleReaderFragment;
                frag.client._currentChapter = (position + 1).ToString();
                //frag.client.RefreshView(frag.webview);
                //frag.ReHighlight();

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

    public class PagerAdapter : FragmentStatePagerAdapter
    {

        public PagerAdapter(Android.Support.V4.App.FragmentManager fm)
            : base(fm)
        {

        }

        public override Android.Support.V4.App.Fragment GetItem(int i)
        {
            BibleReaderFragment fragment = null;

            string chapter = !string.IsNullOrEmpty(ThisApp.selectedNote.ScriptureForHighlight) ? ThisApp.selectedNote.ScriptureForHighlight.Split(',')[1] : null;

            Console.WriteLine(chapter);

            if (!string.IsNullOrEmpty(chapter) && chapter == (i + 1).ToString())
            {
                fragment = new BibleReaderFragment(ThisApp.allBookChapters[i], true, (i + 1).ToString());
            }
            else
            {
                fragment = new BibleReaderFragment(ThisApp.allBookChapters[i], false, (i + 1).ToString());
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

    public class BibleReaderFragment : Android.Support.V4.App.Fragment
    {
        public WebView webview;
        public string html;
        public bool highlight;

        public ReaderActivity.MyWebViewClient client;

        public BibleReaderFragment(string html, bool highlight, string chapter)
        {
            this.html = html;
            this.highlight = highlight;

            client = new ReaderActivity.MyWebViewClient(Activity, highlight, null, chapter);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.Reader, container, false);

            return rootView;
        }

        public override void OnViewCreated(View view, Bundle bundle)
        {
            base.OnViewCreated(view, bundle);

            LoadBibleReader();
        }

        private void LoadBibleReader()
        {
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