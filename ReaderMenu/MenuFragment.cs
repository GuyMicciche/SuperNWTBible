using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;

using Fragment = Android.Support.V4.App.Fragment;
using ListFragment = Android.Support.V4.App.ListFragment;

namespace NWTBible.ReaderMenu
{
    public class MenuFragment : SherlockListFragment
    {
        //private MenuAdapter books;
        //private BibleBookActivityListAdapter books;
        //private ArrayAdapter books;
        //private BibleBookAdapter bookAdapter;

        private SeparatedListAdapter adapter;
        private ArrayAdapter hebrewBooks;
        private ArrayAdapter greekBooks;
        
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            var booksList = ThisApp.allBibleBooks;

            // Add Sections
            adapter = new SeparatedListAdapter(Activity);
            ArrayAdapter<string> hebrewBooks = new ArrayAdapter<string>(Activity, Resource.Layout.ListItem, ThisApp.GetCanonBookNames("hebrew"));
            adapter.AddSection("HEBREW", hebrewBooks);

            ArrayAdapter<string> greekBooks = new ArrayAdapter<string>(Activity, Resource.Layout.ListItem, ThisApp.GetCanonBookNames("greek"));
            adapter.AddSection("GREEK", greekBooks);

            ListAdapter = adapter;




            //books = new BibleBookActivityListAdapter(Activity, booksList);
            //books = new MenuAdapter(Activity);
            //books = new ArrayAdapter(Activity, Resource.Layout.ListItem, ThisApp.GetAllBookTitles());
            //ListAdapter = books;




            //bookAdapter = new BibleBookAdapter(Activity);
            //bookAdapter.AddSeparatorItem("Hebrew-Aramaic Scriptures");
            //foreach (string item in ThisApp.GetCanonBookNames("hebrew"))
            //{
            //    bookAdapter.AddItem(item);
            //}
            //bookAdapter.AddSeparatorItem("Christian Greek Scriptures");
            //foreach (string item in ThisApp.GetCanonBookNames("greek"))
            //{
            //    bookAdapter.AddItem(item);
            //}
            //ListAdapter = bookAdapter;



            // Cool way
            //var data = new ListItemCollection<ListItemValue>()
            //{
            //    new ListItemValue ("Babbage"),
            //    new ListItemValue ("Boole"),
            //    new ListItemValue ("Berners-Lee"),
            //    new ListItemValue ("Atanasoff"),
            //    new ListItemValue ("Allen"),
            //    new ListItemValue ("Cormack"),
            //    new ListItemValue ("Cray"),
            //    new ListItemValue ("Dijkstra"),
            //    new ListItemValue ("Dix"),
            //    new ListItemValue ("Dewey"),
            //    new ListItemValue ("Erdos"),
            //};
            //var sortedContacts = data.GetSortedData();
            //var adapter = CreateAdapter(sortedContacts);
            //ListAdapter = adapter;		
        }

        SeparatedListAdapter CreateAdapter<T>(Dictionary<string, List<T>> sortedObjects)
            where T : IHasLabel, IComparable<T>
        {
            var adapter = new SeparatedListAdapter(Activity);
            foreach (var e in sortedObjects.OrderBy(de => de.Key))
            {
                var label = e.Key;
                var section = e.Value;
                adapter.AddSection(label, new ArrayAdapter<T>(Activity, Resource.Layout.ListItem, section));
            }

            return adapter;
        }

        public override void OnListItemClick(ListView lv, View v, int position, long id)
        {
            //Fragment newContent = null;

            //newContent = new ReaderFragment();

            //if (newContent != null)
            //{
            //    SwitchFragment(newContent);
            //}



            // Use this if using BibleBookActivityListAdapter or ArrayAdapter
            //var book = ThisApp.allBibleBooks[position];



            // Use this if using MenuAdapter
            //int i = position - 1;
            //while (i > 0 && books.rows[i] is string)
            //{
            //    i--;
            //}
            //Header h = (Header)books.rows[i];
            //string bookRow = (string)books.sections[h.Name].GetItem(position - i - 1);
            //var book = ThisApp.allBibleBooks.Find(x => x.Title == bookRow);



            // Use this if using BibleBookAdapter
            //int i = position - 1;
            //if (i > 39)
            //{
            //    i = position - 2;
            //}
            //var book = ThisApp.allBibleBooks[i];



            // Use this if using SeparatedListAdapter
            string bookname = (string)adapter.GetItem(position);
            BibleBook book = ThisApp.allBibleBooks.Single(x => x.Title.ToLower().Equals(bookname.ToLower()));

            ThisApp.selectedBook = book;

            Activity.Finish();

            var intent = new Intent(Activity, typeof(ChaptersActivity));
            intent.AddFlags(ActivityFlags.NoHistory);
            Activity.StartActivity(intent);
        }

        private void SwitchFragment(Android.Support.V4.App.Fragment newContent)
        {
            // TODO change this to use an interface
            var baseActivity = Activity as ReaderActivity;
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

    //Adapter Class
    public class BibleBookAdapter : BaseAdapter
    {
        private const int TYPE_ITEM = 0;
        private const int TYPE_SEPARATOR = 1;
        private const int TYPE_MAX_COUNT = TYPE_SEPARATOR + 1;

        public List<string> mData = new List<string>();
        private LayoutInflater mInflater;

        public List<string> mItemsSet = new List<string>();
        private SortedSet<int> mSeparatorsSet = new SortedSet<int>();

        private Activity activity;

        public BibleBookAdapter(Activity activity)
        {
            this.activity = activity;
            mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
        }

        public void AddItem(string item)
        {
            mData.Add(item);

            mItemsSet.Add(item);
            NotifyDataSetChanged();
        }

        public void AddSeparatorItem(string item)
        {
            mData.Add(item);

            mSeparatorsSet.Add(mData.Count - 1);
            NotifyDataSetChanged();
        }

        public override int GetItemViewType(int position)
        {
            return mSeparatorsSet.Contains(position) ? TYPE_SEPARATOR : TYPE_ITEM;
        }

        public override int ViewTypeCount
        {
            get { return TYPE_MAX_COUNT; }
        }

        public override int Count
        {
            get { return mData.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return mData[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        // Disable if the row is a separator
        public override bool IsEnabled(int position)
        {
            return !GetItemViewType(position).Equals(TYPE_SEPARATOR);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder = null;
            TextView textview = null;

            int type = GetItemViewType(position);

            if (convertView == null)
            {
                holder = new ViewHolder();
                switch (type)
                {
                    case TYPE_ITEM:
                        convertView = mInflater.Inflate(Resource.Layout.SectionItem, parent, false);
                        
                        textview = (TextView)convertView.FindViewById(Resource.Id.itemText);
                        textview.SetTypeface(ThisApp.Font(activity, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
                        textview.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(activity));

                        holder.TextV = textview;
                        break;
                    case TYPE_SEPARATOR:
                        convertView = mInflater.Inflate(Resource.Layout.SectionHeader, parent, false);

                        textview = (TextView)convertView.FindViewById(Resource.Id.headerText);
                        textview.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(activity)/2);

                        holder.TextV = textview;
                        break;
                }
                convertView.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

            holder.TextV.Text = mData[position];

            return convertView;
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    class ViewHolder : Java.Lang.Object
    {
        public TextView TextV { get; set; }
        public ImageView IconV { get; set; }
    }

    public class SeparatedListAdapter : BaseAdapter
    {
        public Context context;
        public Dictionary<string, ArrayAdapter> sections = new Dictionary<string, ArrayAdapter>();
        public ArrayAdapter<string> headers;
        public static int TYPE_SECTION_HEADER = 0;
 
        public SeparatedListAdapter(Context context)
        {
            this.context = context;
            this.headers = new ArrayAdapter<string>(context, Resource.Layout.ListHeader);
        }

        public void AddSection(string section, ArrayAdapter adapter)
        {
            this.headers.Add(section);
            this.sections.Add(section, adapter);
        }

        public override Java.Lang.Object GetItem(int position)
        {
            foreach (var section in sections.Keys)
            {
                ArrayAdapter adapter = sections[section];
                int size = adapter.Count + 1;
 
                // check if position inside this section
                if (position == 0)
                { 
                    return section; 
                }
                if (position < size)
                {
                    return adapter.GetItem(position - 1);
                }
 
                // otherwise jump into next section
                position -= size;
            }
            return null;
        }
 
        public override int Count
        {
            get
            {
                // total together all sections, plus one for each section header
                int total = 0;
                foreach (var adapter in sections.Values)
                {
                     total += adapter.Count + 1;
                }
                return total;
            }
        }
 
        public override int ViewTypeCount
        {
            get
            {

                // assume that headers count as one, then total all sections
                int total = 1;
                foreach (var adapter in sections.Values)
                {
                    total += adapter.ViewTypeCount;
                }
                return total;
            }
        }
 
        public override int GetItemViewType(int position)
        {
            int type = 1;
            foreach (var section in sections.Keys)
            {
                ArrayAdapter adapter = sections[section];
                int size = adapter.Count + 1;
 
                // check if position inside this section
                if (position == 0)
                {
                    return TYPE_SECTION_HEADER;
                }
                if (position < size)
                {
                    return type + adapter.GetItemViewType(position - 1);
                }

                // otherwise jump into next section
                position -= size;
                type += adapter.ViewTypeCount;
            }
            return -1;
        }
 
        public bool AreAllItemsSelectable()
        {
            return false;
        }
 
        public override bool IsEnabled(int position)
        {
            return (GetItemViewType(position) != TYPE_SECTION_HEADER);
        }
 
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view;

            int sectionnum = 0;
            foreach (var section in sections.Keys)
            {
                ArrayAdapter adapter = sections[section];
                int size = adapter.Count + 1;
 
                // check if position inside this section
                if (position == 0)
                {
                    view = headers.GetView(sectionnum, convertView, parent);

                    TextView tv = view.FindViewById<TextView>(Resource.Id.list_header_title);
                    tv.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(context));

                    return view;
                }
                if (position < size)
                {
                    view = adapter.GetView(position - 1, convertView, parent);

                    TextView tv = view.FindViewById<TextView>(Resource.Id.list_item_title);
                    tv.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
                    tv.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(context));

                    return view;
                }
 
                // otherwise jump into next section
                position -= size;
                sectionnum++;
            }
            return null;
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