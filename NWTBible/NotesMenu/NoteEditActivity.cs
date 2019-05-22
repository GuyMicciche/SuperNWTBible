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

namespace NWTBible.NotesMenu
{
    using Android.App;
    using Android.Database;
    using Android.OS;
    using Android.Widget;
    using Java.Lang;
    using Android.Preferences;

    [Activity(Label = "NoteEdit")]
    public class NoteEditActivity : Activity
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

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight));

            this.dbHelper = new NotesDbAdapter(this);
            this.dbHelper.Open();

            SetContentView(Resource.Layout.NoteEdit);

            this.scriptureTitle = (TextView)FindViewById(Resource.Id.scriptureTitle);
            this.scriptureText = (TextView)FindViewById(Resource.Id.scriptureTextContent);
            this.titleText = (EditText)FindViewById(Resource.Id.title);
            this.bodyText = (EditText)FindViewById(Resource.Id.body);

            var confirmButton = (Button)FindViewById(Resource.Id.confirm);

            this.rowId = ((savedInstanceState == null) ? null : savedInstanceState.GetSerializable(NotesDbAdapter.KeyRowId)) as Long;

            if (this.rowId == null)
            {
                var extras = Intent.Extras;
                this.rowId = extras != null ? new Long(extras.GetLong(NotesDbAdapter.KeyRowId))
                                        : null;
            }

            this.PopulateFields();
            confirmButton.Click += delegate
            {
                this.SaveState();
                SetResult(Result.Ok);
                this.Finish();
            };
        }

        private void PopulateFields()
        {
            var confirmButton = (Button)FindViewById(Resource.Id.confirm);

            string verses = "";
            for (var c = 0; c < ThisApp.selectedVerses.Count(); c++)
            {
                if (c == ThisApp.selectedVerses.Count()-1)
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
                scriptureTitle.Text = "(" + scripture.Title + ") ";

                confirmButton.Text = "Create Note";
                return;
            }

            // Edit mode
            ICursor note = this.dbHelper.FetchNote(this.rowId.LongValue());
            this.StartManagingCursor(note);
            this.scriptureText.SetText(note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyScriptureContent)), TextView.BufferType.Normal);
            this.scriptureTitle.SetText("(" + note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyScriptureTitle)) + ") ", TextView.BufferType.Normal);
            this.titleText.SetText(note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyTitle)), TextView.BufferType.Normal);
            this.bodyText.SetText(note.GetString(note.GetColumnIndexOrThrow(NotesDbAdapter.KeyBody)), TextView.BufferType.Normal);
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
            string title = this.titleText.Text;
            string body = this.bodyText.Text;

            if (this.rowId == null)
            {
                long id = this.dbHelper.CreateNote(title, body, scripture.Title, scripture.Scripture, scriptureForHighlight);
                if (id > 0)
                {
                    this.rowId = new Long(id);
                }
            }
            else
            {
                this.dbHelper.UpdateNote(this.rowId.LongValue(), title, body);
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