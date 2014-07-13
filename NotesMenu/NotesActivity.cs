using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Java.Lang;
using NWTBible.ReaderMenu;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;

using SearchView = global::Xamarin.ActionbarSherlockBinding.Widget.SearchView;
using IMenu = global::Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = global::Xamarin.ActionbarSherlockBinding.Views.IMenuItem;
using Menu = global::Android.Views.Menu;
using Xamarin.Parse;

namespace NWTBible.NotesMenu
{
    [Activity(Label = "My Notes", Theme = "@style/Theme.Sherlock")]
    public class NotesActivity : SherlockListActivity
    {
        private const int ActivityCreate = 0;
        private const int ActivityEdit = 1;

        private const int InsertId = Menu.First;
        private const int DeleteId = Menu.First + 1;

        public NotesDbAdapter dbHelper;

        private SimpleCursorAdapter notesAdapter;

        private List<NoteScripture> notesList = new List<NoteScripture>();

        // Called when the activity is first created.
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            SetContentView(Resource.Layout.NotesList);

            this.dbHelper = new NotesDbAdapter(this);
            this.dbHelper.Open();
            this.FillData();

            RegisterForContextMenu(ListView);

            this.ListView.TextFilterEnabled = true;
        }

        private const int SYNC_MENU = 1;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            int group = 0;

            IMenuItem item = menu.Add(group, SYNC_MENU, SYNC_MENU, "Sync");
            item.SetIcon(Android.Resource.Drawable.IcMenuRotate);
            item.SetShowAsAction((int)ShowAsAction.IfRoom);

            SupportMenuInflater.Inflate(Resource.Menu.search_options_menu, menu);

            var searchManager = (SearchManager)GetSystemService(Context.SearchService);
            var search = (SearchView)menu.FindItem(Resource.Id.search).ActionView;
            var searchableInfo = searchManager.GetSearchableInfo(ComponentName);

            search.QueryHint = "Search Notes";
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

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case SYNC_MENU:
                    Parse.Initialize(this, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");

                    if (ParseUser.CurrentUser != null)
                    {
                        ThisApp.SuperDialog(this, "Sync My Notes", "Select a type of sync:", "Download", "Upload",
                            delegate
                            {
                                ThisApp.SyncUserNotesFromParse(this);
                            },
                            delegate
                            {
                                this.FillData();
                                ThisApp.SyncUserNotesToParse(this, notesList);
                            }, true, "Logout",
                            delegate
                            {
                                Toast.MakeText(this, ParseUser.CurrentUser.Username + " logged out.", ToastLength.Long).Show();
                                ParseUser.LogOut();
                            }).Show();
                    }
                    else
                    {
                        ThisApp.SuperDialog(this, "Sync My Notes", "You are not logged in.\n\nWhen you are logged in and sync your notes, they are stored in a safe parsed database online. Would you like to continue?", "Continue", "Cancel",
                            delegate
                            {
                                var intent = new Intent(this, typeof(LogInActivity));
                                StartActivity(intent);
                            },
                            delegate
                            {

                            }, true).Show();
                    }

                    return true;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            menu.Add(0, DeleteId, 0, "Edit");
            menu.Add(0, DeleteId + 1, 1, "Delete");
        }

        public override bool OnContextItemSelected(Android.Views.IMenuItem item)
        {
            AdapterView.AdapterContextMenuInfo info;

            switch (item.ItemId)
            {
                case DeleteId + 1:
                    info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;

                    Parse.Initialize(this, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");
                    if(ParseUser.CurrentUser != null)
                    {
                        this.FillData();
                        NoteScripture n = notesList.ElementAt(info.Position);
                        ThisApp.DeleteNoteFromParse(n);
                    }
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
                NoteBody = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyBody)),
                NWTId = note.GetString(note.GetColumnIndex(NotesDbAdapter.KeyNWTId))
            };

            ThisApp.selectedNote = n;

            Console.WriteLine(n.NWTId);


            string[] args = n.ScriptureForHighlight.Split(',');

            // Set book
            ThisApp.selectedBook = ThisApp.allBibleBooks.Find(x => x.Name == args[0]);

            // Set chapter
            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = args[1].ToString()
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

        public void FillData()
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

            notesList = new List<NoteScripture>();
            while (notesCursor.MoveToNext())
            {
                // Get all selected verses    
                string[] verses = notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyScriptureForHighlight)).Split(',');
                string verseList = "";
                int pos = 2;
                while (pos < verses.Length)
                {
                    string v = verses[pos].ToString();
                    verseList += v;

                    if (pos < (verses.Length - 1))
                    {
                        verseList += ",";
                    }

                    pos++;
                }

                NoteScripture n = new NoteScripture()
                {
                    Id = int.Parse(notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyRowId))),
                    Title = notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyScriptureTitle)),
                    Scripture = notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyScriptureContent)),
                    ScriptureForHighlight = notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyScriptureForHighlight)),
                    NoteTitle = notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyTitle)),
                    NoteBody = notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyBody)),
                    NWTId = notesCursor.GetString(notesCursor.GetColumnIndex(NotesDbAdapter.KeyNWTId)),
                    Book = verses[0],
                    Chapter = verses[1],
                    Verses = verseList
                };

                notesList.Add(n);
            }
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