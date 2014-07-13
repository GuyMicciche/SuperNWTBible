using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;

using NWTBible.NotesMenu;

using SlidingMenuBinding.Lib.App;
using SlidingMenuBinding.Lib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Xamarin.ActionbarSherlockBinding.Views;

using Fragment = Android.Support.V4.App.Fragment;
using ActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using ActionMode = Xamarin.ActionbarSherlockBinding.Views.ActionMode;
using IMenu = Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = Xamarin.ActionbarSherlockBinding.Views.IMenuItem;
using System.IO;
using Android.Webkit;
using Newtonsoft.Json;
using Java.Interop;

namespace NWTBible.ReaderMenu
{
    [Activity(Label = "JW", Theme = "@style/Theme.Sherlock")]
    public class ReaderActivity : SlidingFragmentActivity, ActionBar.IOnNavigationListener, SlidingMenu.IOnOpenedListener
    {
        private Fragment leftMenuFragment;
        private Fragment rightMenuFragment;
        private Fragment contentFragment;

        private ReaderFragment reader;

        public int offset = 0;

        public int dailyTextToday = 0;

        private bool loadNav;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            RequestWindowFeature(WindowFeatures.ActionBar);
            SetSlidingActionBarEnabled(true);

            // Left menu
            if (PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetBoolean("notesReplace", false))
            {
                leftMenuFragment = new NotesFragmentAdvanced();
                rightMenuFragment = new MenuFragmentAdvanced();
            }
            else
            {
                leftMenuFragment = new MenuFragmentAdvanced();
                rightMenuFragment = new NotesFragmentAdvanced();
            }

            contentFragment = new ReaderFragment();
            reader = contentFragment as ReaderFragment;

            // Setup the left menu
            SetBehindContentView(Resource.Layout.MenuFrame);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.MenuFrame, leftMenuFragment).Commit();
            
            // Setup the content
            SetContentView(Resource.Layout.ContentFrame);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.ContentFrame, contentFragment).Commit();

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
            //SlidingMenu.SetShadowWidthRes(Resource.Dimension.SlidingMenuShadowWidth);
            //SlidingMenu.SetShadowDrawable(Resource.Drawable.SlidingMenuShadow);
            SlidingMenu.SetBehindWidth(width * 2);
            //SlidingMenu.SetFadeDegree(0.35f);
            SlidingMenu.TouchModeAbove = SlidingMenuBinding.Lib.SlidingMenu.TouchmodeFullscreen;
            
            // Enable this so that we can toggle the menu when clicking the action bar home button in `OnOptionsItemSelected`
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                Console.WriteLine("Chapter " + ThisApp.selectedChapter.ChapterNumber.ToString());
                // Number of chapters in the selected book
                int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber.ToString());
                offset = chapterNum;

                Title = ThisApp.selectedChapter.BookAndChapter;
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                Title = ThisApp.selectedPublicationArticle.ArticleTitle;
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                ThisApp.selectedDailyText = ThisApp.GetDailyText(reader.FormatDateTime(DateTime.Now));

                Title = ThisApp.selectedDailyText.Date;
            }
            else if (ThisApp.ReaderKind == ReaderKind.WOLReader)
            {
                Title = "Watchtower Online Library";
            }

            // Add the menu if user preferences
            loadNav = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetBoolean("topNavController", true);
            if (loadNav && !ThisApp.ReaderKind.Equals(ReaderKind.WOLReader))
            {
                InitializeActionBarItemList();
                Title = "";
            }

            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
        }

        public void OnOpened()
        {
            Console.WriteLine("Reader Activity is opened!");
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
                if (ThisApp.allPublications.Any(x => x.Image == ThisApp.selectedPublication.Image))
                {
                    // Reload articles
                    ThisApp.allPublicationArticles = ThisApp.GetAllPublicationArticles();
                }
                else
                {
                    ThisApp.AlertBox(this, "REMINDER", "This document does not exist in " + ThisApp.Language + ".");

                    return;
                }
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
            this.StartActivity(Intent);
        }

        public void InitializeActionBarItemList()
        {
            string[] chapters = new string[0];
            int navItem = 0;

            if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {

            }            
            else if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Set action bar mode to ActionBarNavigationMode.List
                SupportActionBar.NavigationMode = (int)ActionBarNavigationMode.List;

                // Store all chapter numbers in string array
                int num = int.Parse(ThisApp.selectedBook.Chapters);
                chapters = new string[num];
                for (var i = 0; i < chapters.Length; i++)
                {
                    chapters[i] = ThisApp.selectedBook.Title.ToUpper() + " " + (i + 1).ToString();
                }

                navItem = int.Parse(ThisApp.selectedChapter.ChapterNumber) - 1;

                var chapterArrayAdapter = new ArrayAdapter(SupportActionBar.ThemedContext, Resource.Layout.ChapterSpinnerListLayout, Resource.Id.chapterTitleItem, chapters);
                SupportActionBar.SetListNavigationCallbacks(chapterArrayAdapter, this);
                SupportActionBar.SetSelectedNavigationItem(navItem);
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                // Set action bar mode to ActionBarNavigationMode.List
                SupportActionBar.NavigationMode = (int)ActionBarNavigationMode.List;

                var collection = ThisApp.allPublicationArticles;

                // Store all chapter numbers in string array
                int num = collection.Count();
                chapters = new string[num];
                for (var i = 0; i < num; i++)
                {
                    chapters[i] = collection.ElementAt(i).ArticleTitle + "\n" + collection.ElementAt(i).DocumentLocation;
                }

                navItem = ThisApp.selectedPublication.CurrentPage - 1;

                var chapterArrayAdapter = new ArrayAdapter(SupportActionBar.ThemedContext, Resource.Layout.ChapterSpinnerListLayout, Resource.Id.chapterTitleItem, chapters);
                SupportActionBar.SetListNavigationCallbacks(chapterArrayAdapter, this);
                SupportActionBar.SetSelectedNavigationItem(navItem);
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                DailyTextDay currentDailyText = ThisApp.GetDailyText(reader.FormatDateTime(DateTime.Now));

                // Set action bar mode to ActionBarNavigationMode.List
                SupportActionBar.NavigationMode = (int)ActionBarNavigationMode.List;

                var collection = ThisApp.allDailyTexts;

                // Store all chapter numbers in string array
                int num = collection.Count();
                chapters = new string[num];
                for (var i = 0; i < num; i++)
                {
                    chapters[i] = collection.ElementAt(i).Date + "\n" + collection.ElementAt(i).Scripture;
                    if (collection.ElementAt(i).DateShort == currentDailyText.DateShort)
                    {
                        navItem = i;
                        offset = i + 1;
                        dailyTextToday = i + 1;
                    }
                }

                var chapterArrayAdapter = new ArrayAdapter(SupportActionBar.ThemedContext, Resource.Layout.ChapterSpinnerListLayout, Resource.Id.chapterTitleItem, chapters);
                SupportActionBar.SetListNavigationCallbacks(chapterArrayAdapter, this);
                SupportActionBar.SetSelectedNavigationItem(navItem);
            }
        }

        public void ReloadActionBar()
        {
            if (loadNav)
            {
                Title = "";

                string[] chapters = new string[0];
                int navItem = 0;

                int num = int.Parse(ThisApp.selectedBook.Chapters);
                chapters = new string[num];
                for (var i = 0; i < chapters.Length; i++)
                {
                    chapters[i] = ThisApp.selectedBook.Title.ToUpper() + " " + (i + 1).ToString();
                }

                navItem = int.Parse(ThisApp.selectedChapter.ChapterNumber) - 1;

                var chapterArrayAdapter = new ArrayAdapter(SupportActionBar.ThemedContext, Resource.Layout.ChapterSpinnerListLayout, Resource.Id.chapterTitleItem, chapters);
                SupportActionBar.SetListNavigationCallbacks(chapterArrayAdapter, this);
                SupportActionBar.SetSelectedNavigationItem(navItem);
            }
            else
            {
                Title = ThisApp.selectedChapter.BookAndChapter;
            }
        }

        public void SwitchContent(Fragment content)
        {
            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Number of chapters in the selected book
                int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber);
                offset = chapterNum;
            }

            reader = content as ReaderFragment;

            contentFragment = content;
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.ContentFrame, content).Commit();

            SlidingMenu.ShowContent();
        }

        public override bool OnCreateOptionsMenu(Xamarin.ActionbarSherlockBinding.Views.IMenu menu)
        {
            if (ThisApp.ReaderKind != ReaderKind.WOLReader)
            {
                var previousMenu = menu.Add(0, PREVIOUS_MENU, PREVIOUS_MENU, "Previous");
                previousMenu.SetShowAsAction(1);
                previousMenu.SetIcon(Android.Resource.Drawable.IcMediaRew);

                var nextMenu = menu.Add(0, NEXT_MENU, NEXT_MENU, "Next");
                nextMenu.SetShowAsAction(1);
                nextMenu.SetIcon(Android.Resource.Drawable.IcMediaFf);
            }

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                var noteMenu = menu.Add(0, NOTE_MENU, NOTE_MENU, "Note");
                noteMenu.SetShowAsAction(1);
                noteMenu.SetIcon(Android.Resource.Drawable.IcMenuAdd);
            }

            if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                var todayMenu = menu.Add(0, TODAY_MENU, TODAY_MENU, "Today");
                todayMenu.SetShowAsAction(1);
                todayMenu.SetIcon(Android.Resource.Drawable.IcMenuToday);
            }

            if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                //var highlightMenu = menu.Add(0, HIGHLIGHT_MENU, HIGHLIGHT_MENU, "Highlighter");
                //highlightMenu.SetShowAsAction(1);
            }

            if (ThisApp.ReaderKind == ReaderKind.WOLReader)
            {
                var saveMenu = menu.Add(0, SAVE_MENU, SAVE_MENU, "Save");
                saveMenu.SetShowAsAction(1);
                saveMenu.SetIcon(Android.Resource.Drawable.IcMenuSave);
            }

            var translateMenu = menu.Add(0, TRANSLATE_MENU, TRANSLATE_MENU, "Translate");
            translateMenu.SetShowAsAction(1);
            translateMenu.SetIcon(Android.Resource.Drawable.IcMenuRotate);

            var gotoMenu = menu.Add(0, GOTO_MENU, GOTO_MENU, "Look Up");
            gotoMenu.SetShowAsAction(1);
            gotoMenu.SetIcon(Android.Resource.Drawable.IcMenuUpload);

            return base.OnCreateOptionsMenu(menu);
        }

        private const int PREVIOUS_MENU = 1;
        private const int NEXT_MENU = 2;
        private const int TRANSLATE_MENU = 3;
        private const int GOTO_MENU = 5;
        private const int SAVE_MENU = 6;
        private const int HIGHLIGHT_MENU = 7;
        private const int NOTE_MENU = 8;
        private const int TODAY_MENU = 9;

        public override bool OnOptionsItemSelected(Xamarin.ActionbarSherlockBinding.Views.IMenuItem item)
        {
            ThisApp.doHighlight = false;

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Number of chapters in the selected book
                int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber);
                offset = chapterNum;
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                // Number of chapters in the selected book
                int pageNum = ThisApp.selectedPublication.CurrentPage;
                offset = pageNum;
            }

            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    Toggle();
                    return true;
                case (PREVIOUS_MENU):
                    ThisApp.selectedPublication.CurrentPage--;
                    offset--;
                    ReloadReader();
                    return true;
                case (NEXT_MENU):
                    ThisApp.selectedPublication.CurrentPage++;
                    offset++;
                    ReloadReader();
                    return true;
                case (GOTO_MENU):
                    ThisApp.GoToArticleDialog(this).Show();
                    return true;
                case (TRANSLATE_MENU):
                    ThisApp.TranslateLanguageDialog(this).Show();
                    return true;
                case (NOTE_MENU):
                    reader.InitializeNoteParadigm();
                    return true;
                case (SAVE_MENU):
                    NotesDbAdapter dbHelper = new NotesDbAdapter(this);
                    dbHelper.Open();
                    long id = dbHelper.CreateNote(reader.Title, ThisApp.WolArticle, reader.Title, " ", "WOLFAVORITE");
                    return true;
                case (HIGHLIGHT_MENU):
                    return true;
                case (TODAY_MENU):
                    offset = dailyTextToday;
                    ReloadReader();
                    return true;                
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ReloadReader(int numberOfItems = 0)
        {
            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                numberOfItems = int.Parse(ThisApp.selectedBook.Chapters);
                if (offset > numberOfItems)
                {
                    offset = 1;
                }
                else if (offset < 1)
                {
                    offset = numberOfItems;
                }

                if (loadNav)
                {
                    SupportActionBar.SetSelectedNavigationItem(offset - 1);
                    Title = "";
                }
                else
                {
                    reader.ReloadBiblePage(offset);
                    Title = ThisApp.selectedChapter.BookAndChapter;
                }
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                numberOfItems = ThisApp.allPublicationArticles.Count();
                int page = ThisApp.selectedPublication.CurrentPage;

                if (page > numberOfItems)
                {
                    ThisApp.selectedPublication.CurrentPage = 1;
                }
                else if (page < 1)
                {
                    ThisApp.selectedPublication.CurrentPage = numberOfItems;
                }

                if (loadNav)
                {
                    SupportActionBar.SetSelectedNavigationItem(page - 1);
                    Title = "";
                }
                else
                {
                    reader.LoadPublicationReader(page);
                    Title = ThisApp.selectedPublicationArticle.ArticleTitle;
                }
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                // This
                //reader.LoadDailyTextReader(offset);

                // Or this
                if (loadNav)
                {
                    SupportActionBar.SetSelectedNavigationItem(offset - 1);
                    Title = "";
                }
                else
                {
                    reader.LoadDailyTextReader(offset);
                    Title = ThisApp.selectedDailyText.Date;
                }
            }
        }

        //public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        //{
        //    if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
        //    {
        //        if (keyCode == Keycode.Back && reader.webview.CanGoBack())
        //        {
        //            reader.webview.GoBack();

        //            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
        //            {
        //                reader.webview2.GoBack();
        //            }

        //            return true;
        //        }
        //        else
        //        {
        //            Finish();
        //        }
        //    }

        //    return base.OnKeyDown(keyCode, e);
        //}

        public bool OnNavigationItemSelected(int itemPosition, long itemId)
        {
            if (ThisApp.ReaderKind == ReaderKind.EPUBReader) { }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                reader.LoadPublicationFromActionBar(itemPosition + 1);
                ThisApp.selectedPublication.CurrentPage = itemPosition + 1;
            }
            else if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                if ((itemPosition + 1).ToString() != ThisApp.selectedChapter.ChapterNumber)
                {
                    reader.ReloadBiblePage(itemPosition + 1);
                }
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                reader.LoadDailyTextReaderFromActionBar(itemPosition + 1);
                offset = itemPosition + 1;
            }

            return true;
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }



    public class VerseSelection : Java.Lang.Object
    {
        Context context;
        WebView webview;
        WebView webview2;
        public List<BibleVerse> selectedVerses;
        public string formattedScripture = "";

        public VerseSelection(Context context, WebView webview, WebView webview2)
        {
            this.context = context;
            this.webview = webview;
            this.webview2 = webview2;
            selectedVerses = new List<BibleVerse>();
        }

        [Export]
        public bool IsPublication()
        {
            if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                return true;
            }

            return false;
        }

        [Export]
        public void AddVerse(string verseNumber, string verseText)
        {
            BibleVerse v = new BibleVerse()
            {
                Book = ThisApp.selectedBook,
                Chapter = ThisApp.selectedChapter,
                VerseNumber = verseNumber,
                Scripture = verseText
            };  
 
            // If the verse is not already selected, add it
            if (selectedVerses.IndexOf(v) < 0) 
            {
                selectedVerses.Add(v);

                ThisApp.selectedVerses = selectedVerses;
            }

            // If record every highlight is checked on, add it and save to preferences
            if (ThisApp.preferences.GetBoolean("recordEveryHighlight", false))
            {
                ThisApp.highlightedScriptures = selectedVerses;

                string json = JsonConvert.SerializeObject(ThisApp.highlightedScriptures);
                var prefs = PreferenceManager.GetDefaultSharedPreferences(context.ApplicationContext);
                prefs.Edit().PutString("HighlightList", json).Commit();
            }

            Console.WriteLine("There are now " + selectedVerses.Count() + " verses selected.");
        }

        [Export]
        public void SubtractVerse(string verseNumber)
        {
            // If the verse is already selected, delete it
            if (selectedVerses.Any(x => x.VerseNumber.Equals(verseNumber)))
            {
                var sel = selectedVerses.Single(s => s.VerseNumber.Equals(verseNumber));
                selectedVerses.Remove(sel);

                ThisApp.selectedVerses = selectedVerses;
            }

            // If record every highlight is checked on, remove it and save to preferences
            if (ThisApp.preferences.GetBoolean("recordEveryHighlight", false))
            {
                ThisApp.highlightedScriptures = selectedVerses;

                string json = JsonConvert.SerializeObject(ThisApp.highlightedScriptures);
                var prefs = PreferenceManager.GetDefaultSharedPreferences(context.ApplicationContext);
                prefs.Edit().PutString("HighlightList", json).Commit();
            }

            Console.WriteLine("There are now " + selectedVerses.Count() + " verses selected.");
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
                    RunJs("HighlightVerse('" + EscapeJsString("4") + "', true);");
                }

                ThisApp.doHighlight = false;
            }
        }

        [Export]
        public void GetContentHTML(string html)
        {
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
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

}