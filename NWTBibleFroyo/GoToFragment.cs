using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;

using Fragment = Android.Support.V4.App.Fragment;
using ListFragment = Android.Support.V4.App.ListFragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using NWTBible.ReaderMenu;
using System.Xml.Linq;

namespace NWTBible
{
    public class GoToFragment : SherlockFragment
    {
        private Spinner book;
        private Spinner chapter;
        private Spinner verse;
        private Button lookup;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            base.OnCreateView(inflater, container, bundle);

            View view = inflater.Inflate(Resource.Layout.GoToFragment, container, false);

            book = view.FindViewById<Spinner>(Resource.Id.spinnerB);
            chapter = view.FindViewById<Spinner>(Resource.Id.spinnerC);
            verse = view.FindViewById<Spinner>(Resource.Id.spinnerV);
            lookup = view.FindViewById<Button>(Resource.Id.gotoConfirm);

            lookup.Click += lookup_Click;

            book.ItemSelected += book_ItemSelected;
            chapter.ItemSelected += chapter_ItemSelected;

            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                PopulateFields();
            }

            return view;
        }

        void book_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // Set book
            ThisApp.selectedBook = ThisApp.allBibleBooks[book.SelectedItemPosition];
        }

        void chapter_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // Set chapter
            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = (chapter.SelectedItemPosition + 1).ToString()
            };
        }

        void lookup_Click(object sender, EventArgs e)
        {
            ThisApp.doHighlight = true;

            ThisApp.ReaderKind = ReaderKind.BibleReader;     

            NoteScripture n = new NoteScripture()
            {
                Id = 0,
                ScriptureForHighlight = ThisApp.selectedBook.Name + "," + (chapter.SelectedItemPosition + 1).ToString() + "," + (verse.SelectedItemPosition + 1).ToString()
            };
            ThisApp.selectedNote = n;

            //Intent intent = new Intent(Activity, ThisApp.MainReader.Class);
            //Activity.StartActivity(intent);
        }

        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);

            // Useful when you have a fragment in actionbar tabs
            RetainInstance = true;

            ThisApp.LanguageChanged += (sender, e) =>
            {
                Activity.RunOnUiThread(() =>
                {
                    PopulateFields();
                });
            };
        }

        private void PopulateFields()
        {
            book.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem, ThisApp.GetAllBookTitles());
            book.Prompt = "Select a Book:";
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