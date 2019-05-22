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

namespace NWTBible
{
    public class CanonFragment : SherlockListFragment
    {
        public string canon;

        public List<BibleBook> booksList = new List<BibleBook>();

        public CanonFragment(string canon)
        {
            this.canon = canon;
        }
        
        public override void OnAttach(Activity activity)
        {
            base.OnAttach(activity);
        }

        public override void OnActivityCreated(Bundle bundle)
        {
            base.OnActivityCreated(bundle);

            PopulateList(canon);

            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
        }

        public override void OnDestroy()
        {
            base.OnPause();

            ThisApp.LanguageChanged -= ThisApp_LanguageChanged;
        }

        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                booksList = ThisApp.GetCanonBooks(canon.ToLower());

                Activity.RunOnUiThread(() =>
                {
                    ListAdapter = new BibleBookFragmentListAdapter(Activity, booksList);
                });
            }
            else
            {
                Activity.RunOnUiThread(() =>
                {
                    ListAdapter = null;
                });
            }
        }

        public override void OnListItemClick(ListView p0, View p1, int p2, long p3)
        {
            base.OnListItemClick(p0, p1, p2, p3);

            var book = booksList[p2];
            ThisApp.selectedBook = book;

            var intent = new Intent(Activity, typeof(ChaptersActivity));
            StartActivity(intent);
        }
            
        void PopulateList(string canon)
        {
            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                booksList = ThisApp.GetCanonBooks(canon.ToLower());

                ListAdapter = new BibleBookFragmentListAdapter(Activity, booksList);
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

    public class BibleBookFragmentListAdapter : BaseAdapter<BibleBook>
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