using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Xml.Linq;

namespace NWTBible.ReaderMenu
{
    public class Sample
    {
        public Sample(string name, Type screen)
        {
            Name = name;
            Screen = screen;
        }
        public string Name;
        public Type Screen;
    }

    public class Header
    {
        public string Name;
        public int SectionIndex;
    }

    public class MenuAdapter : BaseAdapter<string>
    {
        private List<string> hebrewBooks;
        private List<string> greekBooks;
        Dictionary<string, List<string>> samples;

        const int TypeSectionHeader = 0;
        const int TypeSectionSample = 1;

        readonly Activity context;
        public IList<object> rows = new List<object>();

        public ArrayAdapter<string> headers;
        public Dictionary<string, IAdapter> sections = new Dictionary<string, IAdapter>();

        public MenuAdapter(Activity context)
            : base()
        {
            this.context = context;
            headers = new ArrayAdapter<string>(context, Resource.Layout.SectionHeader, Resource.Id.headerText);


            hebrewBooks = ThisApp.GetCanonBookNames("hebrew");
            greekBooks = ThisApp.GetCanonBookNames("greek");

            samples = new Dictionary<string, List<string>>() {
                { "Hebrew-Aramaic Scriptures", hebrewBooks },
                { "Christian Greek Scriptures", greekBooks}
            };

            rows = new List<object>();
            foreach (var section in samples.Keys)
            {
                headers.Add(section);
                sections.Add(section, new ArrayAdapter<string>(context, Resource.Layout.SectionItem, samples[section]));
                rows.Add(new Header { Name = section, SectionIndex = sections.Count - 1 });
                foreach (var session in samples[section])
                {
                    rows.Add(session);
                }
            }
        }
        public string GetSample(int position)
        {
            return (string)rows[position];
        }
        public override string this[int position]
        {
            get
            { // this'll break if called with a 'header' position
                return (string)rows[position];
            }
        }

        public override int ViewTypeCount
        {
            get
            {
                return 1 + sections.Values.Sum(adapter => adapter.ViewTypeCount);
            }
        }

        public override int GetItemViewType(int position)
        {
            return rows[position] is Header
                ? TypeSectionHeader
                : TypeSectionSample;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return rows.Count; }
        }
        public override bool AreAllItemsEnabled()
        {
            return true;
        }
        public override bool IsEnabled(int position)
        {
            return !(rows[position] is Header);
        }

        /// <summary>
        /// Grouped list: view could be a 'section heading' or a 'data row'
        /// </summary>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for this position
            var item = this.rows[position];

            View view = convertView;

            if (item is Header)
            {
                view = headers.GetView(((Header)item).SectionIndex, convertView, parent);
                //view.FindViewById<TextView>(Resource.Id.headerText).SetTypeface(Typeface.CreateFromAsset(context.Assets, "fonts/americana-bt.ttf"), Android.Graphics.TypefaceStyle.Normal);
                view.Clickable = false;
                view.LongClickable = false;
                return view;
            }

            int i = position - 1;
            while (i > 0 && rows[i] is string)
            {
                i--;
            }
            Header h = (Header)rows[i];
            view = sections[h.Name].GetView(position - i - 1, convertView, parent);
            //view.FindViewById<TextView>(Resource.Id.itemText).Text = ((string)item);
            //view.FindViewById<TextView>(Android.Resource.Id.Text1).SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
            //view.FindViewById<TextView>(Android.Resource.Id.Text1).SetTextSize(Android.Util.ComplexUnitType.Sp, 36);
            //view.FindViewById<TextView>(Android.Resource.Id.Text1).SetPadding(16, 8, 8, 8);

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
