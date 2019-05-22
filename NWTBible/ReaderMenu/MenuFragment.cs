using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using System.Linq;

namespace NWTBible.ReaderMenu
{
    public class MenuFragment : ListFragment
    {
        private MenuAdapter books;
        //private BibleBookActivityListAdapter books;
        //private ArrayAdapter books;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.List, null);
        }


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            
            RegisterForContextMenu(ListView);

            var booksList = ThisApp.allBibleBooks;

            //books = new BibleBookActivityListAdapter(Activity, booksList);
            books = new MenuAdapter(Activity);
            //books = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, ThisApp.GetAllBookTitles());

            ListAdapter = books;
        }

        public override void OnListItemClick(ListView lv, View v, int position, long id)
        {
            //Fragment newContent = null;

            //newContent = new ReaderFragment();

            //if (newContent != null)
            //{
            //    SwitchFragment(newContent);
            //}

            Activity.Finish();

            // Use this if using BibleBookActivityListAdapter
            //var book = ThisApp.allBibleBooks[position];

            // Use this if using MenuAdapter
            int i = position - 1;
            while (i > 0 && books.rows[i] is string)
            {
                i--;
            }
            Header h = (Header)books.rows[i];
            string bookRow = (string)books.sections[h.Name].GetItem(position - i - 1);
            var book = ThisApp.allBibleBooks.Find(x => x.Title == bookRow);

            ThisApp.selectedBook = book;

            var intent = new Intent(Activity, typeof(ChaptersActivity));
            intent.AddFlags(ActivityFlags.NoHistory);
            Activity.StartActivity(intent);
        }

        private void SwitchFragment(Fragment newContent)
        {
            // todo change this to use an interface
            var baseActivity = Activity as ReaderSliderActivity;
            if (baseActivity != null)
            {
                baseActivity.SwitchContent(newContent);
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