using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace NWTBible
{
    public class CanonFragment : Fragment
    {
        string canon;

        public CanonFragment(String canon)
        {
            this.canon = canon;
        }

        private ListView list;
        private LayoutInflater inflater;
        private ViewGroup container;

        public List<BibleBook> booksList = new List<BibleBook>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            this.inflater = inflater;
            this.container = container;

            var view = inflater.Inflate(Resource.Layout.Canon, container, false);

            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                var doc = ThisApp.GetNWTFile(ThisApp.Language, canon.ToLower());

                booksList = new List<BibleBook>();

                foreach (var query in doc.Descendants("book"))
                {
                    BibleBook book = new BibleBook()
                    {
                        Number = query.Attribute("number").Value,
                        Title = query.Attribute("title").Value.ToUpper(),
                        Name = query.Attribute("n").Value,
                        Abbreviation = query.Attribute("abbr").Value,
                        Chapters = query.Attribute("chapters").Value,
                        Writer = query.Attribute("writer").Value,
                        Place = query.Attribute("place").Value,
                        Completed = query.Attribute("completed").Value,
                        Time = query.Attribute("time").Value
                    };

                    booksList.Add(book);
                }

                list = view.FindViewById<ListView>(Resource.Id.listView);
                //list.SetAdapter(new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, books.ToArray()));
                list.SetAdapter(new BibleBookFragmentListAdapter(Activity, booksList));
                list.ItemClick += list_ItemClick;
            }

            return view;
        }

        void list_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var book = booksList[e.Position];
            ThisApp.selectedBook = book;

            var intent = new Intent(Activity, typeof(ChaptersActivity));
            StartActivity(intent);

            //this.Activity.StartActivity(typeof(ChaptersActivity));
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }        
    }

    class BibleBookFragmentListAdapter : BaseAdapter<BibleBook>
    {
        Activity context;
        public List<BibleBook> books;

        public BibleBookFragmentListAdapter(Activity context, List<BibleBook> books)
            : base()
        {
            this.context = context;
            this.books = books;
        }

        public override int Count
        {
            get { return this.books.Count(); }
        }

        public override BibleBook this[int position]
        {
            get { return this.books[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this.books[position];
            var view = convertView;

            if (convertView == null || !(convertView is LinearLayout))
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.BibleBooksListLayout, parent, false);
            }

            var bookItem = view.FindViewById<TextView>(Resource.Id.bookTitleItem);
            bookItem.SetText(item.Title, TextView.BufferType.Normal);
            bookItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

            var detailsItem = view.FindViewById<TextView>(Resource.Id.bookDetailsItem);
            detailsItem.SetText(item.WriterAndPlace, TextView.BufferType.Normal);
            //detailsItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

            return view;
        }

        public override long GetItemId(int position)
        {
            return position;
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