using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using Android.Database;
using Java.Lang;
using Android.Preferences;
using Xamarin.ActionbarSherlockBinding.App;

namespace NWTBible.NotesMenu
{  
    [Activity(Label = "NoteEdit", Theme = "@style/Theme.Sherlock")]
    public class NoteEditActivity : SherlockActivity
    {
        private TextView scriptureTitle;
        private TextView scriptureText;
        private EditText titleText;
        private EditText bodyText;

        private Long rowId;
        private NotesDbAdapter dbHelper;

        private string scriptureForHighlight = "";

        private NoteScripture scripture;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            dbHelper = new NotesDbAdapter(this);
            dbHelper.Open();

            SetContentView(Resource.Layout.NoteEdit);

            scriptureTitle = (TextView)FindViewById(Resource.Id.scriptureTitle);
            scriptureText = (TextView)FindViewById(Resource.Id.scriptureTextContent);
            titleText = (EditText)FindViewById(Resource.Id.title);
            bodyText = (EditText)FindViewById(Resource.Id.body);

            var confirmButton = (Button)FindViewById(Resource.Id.confirm);

            rowId = ((savedInstanceState == null) ? null : savedInstanceState.GetSerializable(NotesDbAdapter.KeyRowId)) as Long;

            if (this.rowId == null)
            {
                var extras = Intent.Extras;
                this.rowId = extras != null ? new Long(extras.GetLong(NotesDbAdapter.KeyRowId)) : null;
            }

            PopulateFields();
            confirmButton.Click += delegate
            {
                SaveState();
                SetResult(Result.Ok);
                Finish();
            };
        }

        private void PopulateFields()
        {
            var confirmButton = (Button)FindViewById(Resource.Id.confirm);

            string verses = "";
            for (var c = 0; c < ThisApp.selectedVerses.Count(); c++)
            {
                if (c == ThisApp.selectedVerses.Count() - 1)
                {
                    verses += ThisApp.selectedVerses[c].VerseNumber;
                }
                else
                {
                    verses += ThisApp.selectedVerses[c].VerseNumber + ",";
                }

            }
            scriptureForHighlight = ThisApp.selectedBook.Name + "," + ThisApp.selectedChapter.ChapterNumber + "," + verses;

            // Create mode
            if (this.rowId == null)
            {
                scripture = ThisApp.FormatScriptureHeading(ThisApp.selectedVerses);
                Title = "New Note — " + scripture.Title;
                scriptureText.Text = scripture.Scripture;
                scriptureTitle.Text = scripture.Title;

                confirmButton.Text = "Create Note";
                return;
            }

            // Edit mode
            ICursor note = dbHelper.FetchNote(this.rowId.LongValue());
            StartManagingCursor(note);
            scriptureText.SetText(note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyScriptureContent)), TextView.BufferType.Normal);
            scriptureTitle.SetText(note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyScriptureTitle)), TextView.BufferType.Normal);
            titleText.SetText(note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyTitle)), TextView.BufferType.Normal);
            bodyText.SetText(note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyBody)), TextView.BufferType.Normal);
            Title = "Edit Note — " + note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyScriptureTitle));

            confirmButton.Text = "Save Note";
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            //this.SaveState();            
        }


        protected override void OnPause()
        {
            base.OnPause();
            //this.SaveState();
        }

        protected override void OnResume()
        {
            base.OnResume();
            //this.PopulateFields();
        }

        private void SaveState()
        {
            string title = titleText.Text;
            string body = bodyText.Text;

            if (rowId == null)
            {
                long id = dbHelper.CreateNote(title, body, scripture.Title, scripture.Scripture, scriptureForHighlight);
                if (id > 0)
                {
                    rowId = new Long(id);
                }

                // TESTING PURPOSES
                //ThisApp.SaveNoteToParse(id, title, body, scripture.Title, scripture.Scripture, scriptureForHighlight);
            }
            else
            {
                dbHelper.UpdateNote(rowId.LongValue(), title, body);

                // TESTING PURPOSES
                //ThisApp.UpdateNoteToParse(rowId.LongValue(), title, body);
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