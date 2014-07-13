using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Preferences;

namespace NWTBible
{
    [Activity(Label = "Canon")]
    public class CanonActivity : Activity
    {
        private ListView list;
        public List<BibleBook> booksList = new List<BibleBook>();

        private ISharedPreferences _preferences;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            SetContentView(Resource.Layout.Canon);

            string canon = Intent.GetStringExtra("Canon") ?? "Data not available";

            list = FindViewById<ListView>(Resource.Id.listView);
            list.ItemClick += list_ItemClick;

            PopulateList(canon);

            ThisApp.LanguageChanged += (sender, e) =>
            {
                RunOnUiThread(() =>
                {
                    PopulateList(canon);
                });
            };

            _preferences = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
        }

        protected override void OnResume()
        {
            base.OnResume();

            string activityStyle = _preferences.GetString("mainOption", "null");
            Console.WriteLine("WOOOOOOOOOOOOOORK: " + activityStyle);
        }

        void PopulateList(string canon)
        {
            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                booksList = ThisApp.GetCanonBooks(canon.ToLower());

                list.Adapter = new BibleBookActivityListAdapter(this, booksList);
            }
        }

        void list_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var book = booksList[e.Position];
            ThisApp.selectedBook = book;

            //var intent = new Intent(this, typeof(ChaptersActivity));
            //intent.AddFlags(ActivityFlags.NoHistory);
            //StartActivity(intent);
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class BibleBookActivityListAdapter : BaseAdapter
    {
        Activity context;
        public List<BibleBook> books;

        public BibleBookActivityListAdapter(Activity context, List<BibleBook> books)
            : base()
        {
            this.context = context;
            this.books = books;
        }

        public override int Count
        {
            get { return this.books.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this.books[position];
            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.BibleBooksListLayout, parent, false)) as LinearLayout;

            LinearLayout.LayoutParams lay = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            lay.Gravity = GravityFlags.Right;

            var bookItem = view.FindViewById<TextView>(Resource.Id.bookTitleItem);
            bookItem.SetText(item.Title, TextView.BufferType.Normal);
            bookItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            bookItem.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(context));
                
            var detailsItem = view.FindViewById<TextView>(Resource.Id.bookDetailsItem);
            detailsItem.SetText(item.WriterAndPlace, TextView.BufferType.Normal);
            detailsItem.SetTextSize(Android.Util.ComplexUnitType.Sp, ((float)ThisApp.UserFontSize(context) / 2));
            //detailsItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

            if (ThisApp.Language == "Arabic")
            {
                bookItem.LayoutParameters = lay;
                detailsItem.LayoutParameters = lay;
            }

            return view;
        }

        public BibleBook GetItemAtPosition(int position)
        {
            return books[position];
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class BibleBookActivityListAdapter_Alternate : ArrayAdapter<BibleBook>
    {
        Context context;
        public List<BibleBook> books;

        public BibleBookActivityListAdapter_Alternate(Context context, List<BibleBook> books)
            : base(context, Resource.Layout.BibleBooksListLayout)
        {
            this.context = context;
            this.books = books;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this.books[position];
            var view = base.GetView(position, convertView, parent);

            var bookItem = view.FindViewById<TextView>(Resource.Id.bookTitleItem);
            bookItem.SetText(item.Title, TextView.BufferType.Normal);
            bookItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

            var detailsItem = view.FindViewById<TextView>(Resource.Id.bookDetailsItem);
            detailsItem.SetText(item.WriterAndPlace, TextView.BufferType.Normal);
            //detailsItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

            return view;
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