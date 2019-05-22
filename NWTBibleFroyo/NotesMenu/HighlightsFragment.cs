using Android.Content;
using Android.Database;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using NWTBible.NotesMenu;
using NWTBible.ReaderMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.Parse;
using Android.App;

namespace NWTBible.NotesMenu
{
    public class HighlightsFragment : SherlockListFragment
    {
        private SimpleCursorAdapter highlightsAdapter;
        private List<BibleVerse> highlightsList = new List<BibleVerse>();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.NotesList, null);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PopulateListView();

            RegisterForContextMenu(ListView);            

            ListView.TextFilterEnabled = true;
        }

        public override void OnCreateOptionsMenu(Xamarin.ActionbarSherlockBinding.Views.IMenu menu, Xamarin.ActionbarSherlockBinding.Views.MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            inflater.Inflate(Resource.Menu.search_options_menu, menu);

            var search = (SearchView)menu.FindItem(Resource.Id.search).ActionView;

            search.SetQueryHint("Search Highlights");
            search.SetIconifiedByDefault(true);
            search.QueryTextChange += (sender, e) =>
            {
                if (highlightsAdapter != null)
                {
                    Console.WriteLine(search.Query);
                    highlightsAdapter.Filter.InvokeFilter(search.Query);
                    ListAdapter = highlightsAdapter;
                }
            };
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            menu.Add(0, Menu.First, 0, "Delete");
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            AdapterView.AdapterContextMenuInfo info;

            switch (item.ItemId)
            {
                case Menu.First:
                    info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
                    Console.WriteLine(info.Position.ToString());
                    BibleVerse v = highlightsList.ElementAt(info.Position);
                    ThisApp.highlightedScriptures.Remove(v);
                    PopulateListView();
                    return true;
            }
            return base.OnContextItemSelected(item);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            ThisApp.ReaderKind = ReaderKind.BibleReader;

            BibleVerse verse = highlightsList.ElementAt(position);

            // Set book
            ThisApp.selectedBook = ThisApp.allBibleBooks.Find(x => x.Name == verse.Book.Name);

            // Set chapter
            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = verse.Chapter.ChapterNumber
            };

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
                act.UpdatePager(int.Parse(ThisApp.selectedChapter.ChapterNumber));
            }

            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
            List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
            ThisApp.allBookChapters = collection;            
        }

        public void PopulateListView()
        {
            if (!System.String.IsNullOrEmpty(ThisApp.Language))
            {
                ListAdapter = new ArrayAdapter(Activity, Resource.Layout.ListItem, ThisApp.highlightedScriptures.Select(s=>s.BookChapterVerse).ToList());

                highlightsList = new List<BibleVerse>();
                foreach (var h in ThisApp.highlightedScriptures)
                {
                    highlightsList.Add(h);
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
}