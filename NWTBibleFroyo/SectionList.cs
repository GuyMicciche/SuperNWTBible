using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NWTBible
{
    class SectionList
    {

    }

    interface IHasLabel
    {
        string Label { get; }
    }

    class ListItemValue : Java.Lang.Object, IHasLabel, IComparable<ListItemValue>
    {
        public ListItemValue(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        int IComparable<ListItemValue>.CompareTo(ListItemValue value)
        {
            return Name.CompareTo(value.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public string Label
        {
            get { return Name[0].ToString(); }
        }
    }

    class ListItemCollection<T> : IEnumerable<T>
        where T : IHasLabel, IComparable<T>
    {
        public ListItemCollection()
        {
        }

        List<T> values = new List<T>();

        public void Add(T value)
        {
            values.Add(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Dictionary<string, List<T>> GetSortedData()
        {
            var results = new Dictionary<string, List<T>>();
            List<T> contacts = null;
            string cur = null;

            foreach (var e in values)
            {
                if (e.Label != cur)
                {
                    contacts = new List<T>();
                    cur = e.Label;
                    results.Add(cur, contacts);
                }
                contacts.Add(e);
            }

            foreach (var v in results.Values)
            {
                v.Sort();
            }

            return results;
        }
    }

    class SeparatedListAdapter : BaseAdapter
    {
        public SeparatedListAdapter(Context context)
        {
            headers = new ArrayAdapter<string>(context, Resource.Layout.ListHeader);
        }

        Dictionary<string, IAdapter> sections = new Dictionary<string, IAdapter>();
        ArrayAdapter<string> headers;
        const int TypeSectionHeader = 0;

        public void AddSection(string section, IAdapter adapter)
        {
            headers.Add(section);
            sections.Add(section, adapter);
        }

        public override Java.Lang.Object GetItem(int position)
        {
            int op = position;
            foreach (var section in sections.Keys)
            {
                var adapter = sections[section];
                int size = adapter.Count + 1;
                if (position == 0)
                {
                    return section;
                }
                if (position < size)
                {
                    return adapter.GetItem(position - 1);
                }
                position -= size;
            }
            return null;
        }

        public override int Count
        {
            get { return sections.Values.Sum(adapter => adapter.Count + 1); }
        }

        public override int ViewTypeCount
        {
            get { return 1 + sections.Values.Sum(adapter => adapter.ViewTypeCount); }
        }

        public override int GetItemViewType(int position)
        {
            int type = 1;
            foreach (var section in sections.Keys)
            {
                var adapter = sections[section];
                int size = adapter.Count + 1;

                // check if position inside this section
                if (position == 0)
                {
                    return TypeSectionHeader;
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

        public override bool AreAllItemsEnabled()
        {
            return false;
        }

        public override bool IsEnabled(int position)
        {
            return (GetItemViewType(position) != TypeSectionHeader);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            int sectionnum = 0;
            foreach (var section in sections.Keys)
            {
                var adapter = sections[section];
                int size = adapter.Count + 1;

                // check if position inside this section
                if (position == 0)
                {
                    return headers.GetView(sectionnum, convertView, parent);
                }
                if (position < size)
                {
                    return adapter.GetView(position - 1, convertView, parent);
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
    }
}
