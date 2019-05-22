using ActionbarSherlock.View;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using NWTBible.NotesMenu;
using NWTBible.ReaderMenu;
using SlidingMenuBinding.Lib.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Fragment = Android.Support.V4.App.Fragment;

namespace NWTBible.ReaderMenu
{
    [Activity(Label = "New World Translation Bible")]
    public class ReaderSliderActivity : SlidingFragmentActivity, ActionbarSherlock.App.ActionBar.IOnNavigationListener
    {
        private Fragment menuFragment;
        private Fragment contentFragment;

        private ReaderFragment reader;

        public int offset = 0;

        private bool loadNav;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
            
            // Right content
            contentFragment = new ReaderFragment();
            reader = contentFragment as ReaderFragment;

            // Setup the menu
            SetBehindContentView(Resource.Layout.MenuFrame);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.MenuFrame, menuFragment).Commit();

            // Setup the content
            SetContentView(Resource.Layout.ContentFrame);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.ContentFrame, contentFragment).Commit();

            // Setup the sliding menu
            SlidingMenu.SetShadowWidthRes(Resource.Dimension.SlidingMenuShadowWidth);
            SlidingMenu.SetShadowDrawable(Resource.Drawable.SlidingMenuShadow);
            SlidingMenu.SetBehindOffsetRes(Resource.Dimension.SlidingmenuOffset);
            SlidingMenu.SetFadeDegree(0.35f);
            SlidingMenu.TouchModeAbove = SlidingMenuBinding.Lib.SlidingMenu.TouchmodeFullscreen;

            // Enable this so that we can toggle the menu when clicking the action bar home button in `OnOptionsItemSelected`
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);            

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Number of chapters in the selected book
                int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber);
                offset = chapterNum;

                Title = "New World Translation Bible — " + ThisApp.selectedChapter.BookAndChapter;
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                ThisApp.selectedDailyText = ThisApp.GetDailyText(reader.FormatDateTime(DateTime.Now));

                Title = "New World Translation Bible — " + ThisApp.selectedDailyText.Date;
            }

            // Add the menu if user preferences
            loadNav = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetBoolean("topNavController", true);
            if (loadNav)
            {
                InitializeActionBarItemList();
                Title = "New World Translation Bible";
            }
        }

        public void InitializeActionBarItemList()
        {
            string[] chapters = new string[0];
            int navItem = 0;

            if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {

            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {

            }
            else if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Set action bar mode to ActionBarNavigationMode.List
                SupportActionBar.NavigationMode = (int)ActionBarNavigationMode.List;
                // Add list to menu
                //SupportActionBar.SetListNavigationCallbacks(chapterAdapter, chapterListener);

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
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                DailyTextDay currentDailyText = ThisApp.GetDailyText(reader.FormatDateTime(DateTime.Now));

                // Set action bar mode to ActionBarNavigationMode.List
                SupportActionBar.NavigationMode = (int)ActionBarNavigationMode.List;
                // Add list to menu
                //SupportActionBar.SetListNavigationCallbacks(chapterAdapter, chapterListener);

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
                Title = "New World Translation Bible — " + ThisApp.selectedChapter.BookAndChapter;
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

        public override bool OnCreateOptionsMenu(ActionbarSherlock.View.IMenu menu)
        {
            var actionitem1 = menu.Add(0, 8, 8, "<< Prev");
            actionitem1.SetShowAsAction(1);

            var actionitem2 = menu.Add(0, 9, 9, "Next >>");
            actionitem2.SetShowAsAction(1);

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                var actionitem3 = menu.Add(0, 1, 1, "+Note");
                actionitem3.SetShowAsAction(1);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(ActionbarSherlock.View.IMenuItem item)
        {
            ThisApp.doHighlight = false;

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                // Number of chapters in the selected book
                int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber);
                offset = chapterNum;
            }

            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    Toggle();
                    return true;
                case (1):
                    reader.InitializeNoteParadigm();
                    return true;
                case (8):
                    ThisApp.selectedPublication.CurrentPage--;
                    offset--;
                    UpdateBar();
                    return true;
                case (9):
                    ThisApp.selectedPublication.CurrentPage++;
                    offset++;
                    UpdateBar();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void UpdateBar(int numberOfItems = 0)
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
                    Title = "New World Translation Bible";
                }
                else
                {
                    reader.ReloadBiblePage(offset);
                    Title = "New World Translation Bible — " + ThisApp.selectedChapter.BookAndChapter;
                }                
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {                
                if (loadNav)
                {
                    SupportActionBar.SetSelectedNavigationItem(offset - 1);
                    Title = "New World Translation Bible";
                }
                else
                {
                    reader.LoadDailyTextReader(offset);
                    Title = "New World Translation Bible — " + ThisApp.selectedDailyText.Date;
                }                
            }                      
        }

        public bool OnNavigationItemSelected(int itemPosition, long itemId)
        {
            if (ThisApp.ReaderKind == ReaderKind.EPUBReader) { }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader) { }
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

            return false;
        }

        protected override void OnDestroy()
        {
            ThisApp.doHighlight = false;

            base.OnDestroy();
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