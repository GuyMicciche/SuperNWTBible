using Xamarin.ActionbarSherlockBinding.App;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Java.Lang;
using NWTBibleFree.ReaderMenu;
using System;
using System.Collections.Generic;

namespace NWTBibleFree.NotesMenu
{
    [Activity(Label = "My Notes")]
    public class NotesActivity : ListActivity
    {
        private const int ActivityCreate = 0;
        private const int ActivityEdit = 1;

        private const int InsertId = Menu.First;
        private const int DeleteId = Menu.First + 1;

        private NotesDbAdapter dbHelper;

        private SimpleCursorAdapter notesAdapter;

        private List<NoteScripture> notesList = new List<NoteScripture>();

        // Called when the activity is first created.
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight));

            SetContentView(Resource.Layout.NotesList);

            this.dbHelper = new NotesDbAdapter(this);
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

            ThisApp.selectedNote = new NoteScripture();
        }
        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.search_options_menu, menu);

            var searchManager = (SearchManager)GetSystemService(Context.SearchService);
            var search = (SearchView)menu.FindItem(Resource.Id.search).ActionView;
            var searchableInfo = searchManager.GetSearchableInfo(ComponentName);

            search.SetQueryHint("Search Notes");
            search.SetIconifiedByDefault(true);
            search.QueryTextChange += (sender, e) =>
                {
                    
                    //string val = search.Query.ToString();

                    //Dictionary<string, int> dict = new Dictionary<string, int>();
                    //foreach (var n in notesList)
                    //{
                    //    var all = n.NoteBody + " " + n.NoteTitle + " " + n.Scripture + " " + n.Title;
                    //    dict.Add(all, n.Id);
                    //}

                    //List<int> ids = new List<int>();
                    //foreach (var pair in dict)
                    //{
                    //    string value = pair.Value.ToString();

                    //    if (value.IndexOf(val, StringComparison.CurrentCultureIgnoreCase) != -1)
                    //    {
                    //        ids.Add(int.Parse(pair.Key));
                    //    }
                    //}

                    //FillDataWhileSearching(ids.ToArray());
                    //FillDataWhileSearching(search.Query.ToString());

                    if (notesAdapter != null)
                    {
                        Console.WriteLine(search.Query);
                        notesAdapter.Filter.InvokeFilter(search.Query);
                        ListAdapter = notesAdapter;
                    }
                };

            return base.OnCreateOptionsMenu(menu);
        }

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    base.OnCreateOptionsMenu(menu);

        //    //menu.Add(0, InsertId, 0, "+Note").SetShowAsAction(ShowAsAction.IfRoom);
        //    SearchView search = new SearchView(SupportActionBar.ThemedContext);
        //    search.SetQueryHint("Search Friends");
        //    search.SetIconifiedByDefault(true);

        //    return true;
        //}

        //public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        //{
        //    switch (item.ItemId)
        //    {
        //        case InsertId:
        //            this.CreateNote();
        //            return true;
        //    }

        //    return base.OnMenuItemSelected(featureId, item);
        //}

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            menu.Add(0, DeleteId, 0, "Edit").SetShowAsAction(ShowAsAction.IfRoom);

            base.OnCreateContextMenu(menu, v, menuInfo);
            menu.Add(0, DeleteId+1, 0, "Delete").SetShowAsAction(ShowAsAction.IfRoom);
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
                    var i = new Intent(this, typeof(NoteEditActivity));
                    i.PutExtra(NotesDbAdapter.KeyRowId, info.Id);
                    StartActivityForResult(i, ActivityEdit);
                    return true;
            }
            return base.OnContextItemSelected(item);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            ThisApp.doHighlight = true;

            ThisApp.ReaderKind = ReaderKind.BibleReader;

            ICursor note = this.dbHelper.FetchNote(id);
            this.StartManagingCursor(note);

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

            string[] args = ThisApp.selectedNote.ScriptureForHighlight.Split(',');

            // Set book
            var book = ThisApp.allBibleBooks.Find(x => x.Name == args[0]);
            ThisApp.selectedBook = book;

            // Set chapter
            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = book,
                ChapterNumber = args[1]
            };

            Finish();

            Intent intent = new Intent(this, ThisApp.MainReader.Class);
            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            this.FillData();
        }

        private void FillData()
        {
            ICursor notesCursor = this.dbHelper.FetchAllNotes();
            this.StartManagingCursor(notesCursor);

            // Create an array to specify the fields we want to display in the list (only TITLE)
            var from = new[] { NotesDbAdapter.KeyTitle, NotesDbAdapter.KeyBody, NotesDbAdapter.KeyScriptureTitle };

            // and an array of the fields we want to bind those fields to (in this case just text1)
            var to = new[] { Resource.Id.textNoteTitle, Resource.Id.textNoteContent, Resource.Id.textNoteScriptureTitle };

            // Now create a simple cursor adapter and set it to display
            notesAdapter = new SimpleCursorAdapter(this, Resource.Layout.NoteRow, notesCursor, from, to);
            notesAdapter.FilterQueryProvider = new FilterQueryProvider(c => notesCursor, dbHelper);

            this.ListAdapter = notesAdapter;
        }

        private void CreateNote()
        {
            var i = new Intent(this, typeof(NoteEditActivity));
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

    public class FilterQueryProvider : Java.Lang.Object, IFilterQueryProvider
    {
        public static readonly string TAG = "FilterQueryProvider";

        private readonly Func<ICharSequence, ICursor> _query;
        private readonly NotesDbAdapter _db;

        public FilterQueryProvider(Func<ICharSequence, ICursor> query, NotesDbAdapter db)
        {
            _query = query;
            _db = db;
        }

        public ICursor RunQuery(string constraint)
        {
            return RunQuery(new Java.Lang.String(constraint));
        }

        public ICursor RunQuery(ICharSequence constraint)
        {
            Console.WriteLine(constraint);
            ICursor cursor = _db.FetchNote(constraint.ToString());
            return cursor;
        }
    }
}