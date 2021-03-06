using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Database;
using Java.Lang;
using Android.Preferences;
using NWTBibleFree.ReaderMenu;

using Android.Support.V4.App;
using System.Xml.Linq;

namespace NWTBibleFree.NotesMenu
{
    public class NotesFragment : ListFragment
    {
        private const int ActivityCreate = 0;
        private const int ActivityEdit = 1;

        private const int InsertId = Menu.First;
        private const int DeleteId = Menu.First + 1;

        private NotesDbAdapter dbHelper;

        private SimpleCursorAdapter notesAdapter;

        private List<NoteScripture> notesList = new List<NoteScripture>();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.NotesList, null);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetTheme(PreferenceManager.GetDefaultSharedPreferences(Activity.ApplicationContext).GetInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight));

            //SetContentView(Resource.Layout.NotesList);

            this.dbHelper = new NotesDbAdapter(Activity);
            this.dbHelper.Open();
            this.FillData();

            RegisterForContextMenu(ListView);

            ICursor cursor = ((SimpleCursorAdapter)ListView.Adapter).Cursor;
            while (cursor.MoveToNext())
            {
                NoteScripture n = new NoteScripture()
                {
                    Id = int.Parse(cursor.GetString(cursor.GetColumnIndex(NotesDbAdapter.KeyRowId))),
                    Title = cursor.GetString(cursor.GetColumnIndex(NotesDbAdapter.KeyScriptureTitle)),
                    Scripture = cursor.GetString(cursor.GetColumnIndex(NotesDbAdapter.KeyScriptureContent)),
                    ScriptureForHighlight = cursor.GetString(cursor.GetColumnIndex(NotesDbAdapter.KeyScriptureForHighlight)),
                    NoteTitle = cursor.GetString(cursor.GetColumnIndex(NotesDbAdapter.KeyTitle)),
                    NoteBody = cursor.GetString(cursor.GetColumnIndex(NotesDbAdapter.KeyBody))
                };

                notesList.Add(n);
            }

            this.ListView.TextFilterEnabled = true;

        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.search_options_menu, menu);

            //var searchManager = (SearchManager)GetSystemService(Context.SearchService);
            var search = (SearchView)menu.FindItem(Resource.Id.search).ActionView;
            //var searchableInfo = searchManager.GetSearchableInfo(ComponentName);

            search.SetQueryHint("Search Notes");
            search.SetIconifiedByDefault(true);
            search.QueryTextChange += (sender, e) =>
                {
                    if (notesAdapter != null)
                    {
                        Console.WriteLine(search.Query);
                        notesAdapter.Filter.InvokeFilter(search.Query);
                        ListAdapter = notesAdapter;
                    }
                };

 	         base.OnCreateOptionsMenu(menu, inflater);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            menu.Add(0, DeleteId, 0, "Edit").SetShowAsAction(ShowAsAction.IfRoom);

            base.OnCreateContextMenu(menu, v, menuInfo);
            menu.Add(0, DeleteId+1, 0, "Delete").SetShowAsAction(ShowAsAction.IfRoom);
        }

        public override void OnResume()
        {
            base.OnResume();

            ThisApp.selectedNote = new NoteScripture();
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            AdapterView.AdapterContextMenuInfo info;

            switch (item.ItemId)
            {
                case DeleteId+1:
                    info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
                    this.dbHelper.DeleteNote(info.Id);
                    this.FillData();
                    return true;
                case DeleteId:
                    info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
                    var i = new Intent(Activity, typeof(NoteEditActivity));
                    i.PutExtra(NotesDbAdapter.KeyRowId, info.Id);
                    StartActivityForResult(i, ActivityEdit);
                    return true;
            }
            return base.OnContextItemSelected(item);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());
            List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
            ThisApp.allBookChapters = collection;

            ThisApp.doHighlight = true;

            ThisApp.ReaderKind = ReaderKind.BibleReader;  

            ICursor note = this.dbHelper.FetchNote(id);
            Activity.StartManagingCursor(note);

            NoteScripture n = new NoteScripture()
            {
                Id = int.Parse(note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyRowId))),
                Title = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyScriptureTitle)),
                Scripture = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyScriptureContent)),
                ScriptureForHighlight = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyScriptureForHighlight)),
                NoteTitle = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyTitle)),
                NoteBody = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyBody))
            };

            ThisApp.selectedNote = n;

            string[] args = n.ScriptureForHighlight.Split(',');

            // Set book
            var book = ThisApp.allBibleBooks.Find(x => x.Name == args[0]);
            ThisApp.selectedBook = book;

            // Set chapter
            var chapter = new BibleChapter()
            {
                Book = book,
                ChapterNumber = args[1].ToString()
            };
            ThisApp.selectedChapter = chapter;

            if (ReaderNavigationType.IsSelectingNavigation)
            {
                var act = Activity as ReaderSliderActivity;
                act.ReloadActionBar();

                Fragment newContent = null;
                newContent = new ReaderFragment();
                if (newContent != null)
                {
                    act.SwitchContent(newContent);
                }
            }
            else if (ReaderNavigationType.IsSwipingNavigation)
            {
                var act = Activity as ReaderViewPagerActivity;
                act.UpdatePager();
            }     
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            FillData();
        }

        private void FillData()
        {
            ICursor notesCursor = this.dbHelper.FetchAllNotes();
            Activity.StartManagingCursor(notesCursor);

            // Create an array to specify the fields we want to display in the list (only TITLE)
            var from = new[] { NotesDbAdapter.KeyTitle, NotesDbAdapter.KeyBody, NotesDbAdapter.KeyScriptureTitle };

            // and an array of the fields we want to bind those fields to (in this case just text1)
            var to = new[] { Resource.Id.textNoteTitle, Resource.Id.textNoteContent, Resource.Id.textNoteScriptureTitle };

            // Now create a simple cursor adapter and set it to display
            notesAdapter = new SimpleCursorAdapter(Activity, Resource.Layout.NoteRow, notesCursor, from, to);
            //notesAdapter.StringConversionColumn = notesCursor.GetColumnIndexOrThrow(NotesDbAdapter.KeyBody);
            //notesAdapter.CursorToStringConverter = notesCursor.GetColumnIndexOrThrow(NotesDbAdapter.KeyBody);
            notesAdapter.FilterQueryProvider = new FilterQueryProvider(c => notesCursor, dbHelper);

            this.ListAdapter = notesAdapter;
        }

        private void CreateNote()
        {
            var i = new Intent(Activity, typeof(NoteEditActivity));
            this.StartActivityForResult(i, ActivityCreate);
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