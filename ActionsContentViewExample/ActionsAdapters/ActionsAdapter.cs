using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Net;
using Android.Views;
using Android.Widget;

namespace ActionsContentViewExample.ActionsAdapters
{
    public class ActionsAdapter : BaseAdapter
    {

        private const int VIEW_TYPE_CATEGORY = 0;
        private const int VIEW_TYPE_SETTINGS = 1;
        private const int VIEW_TYPE_SITES = 2;
        private const int VIEW_TYPES_COUNT = 3;

        private readonly LayoutInflater MInflater;

        private readonly string[] MTitles;
        private readonly string[] MUrls;
        private readonly int[] MIcons;

        public ActionsAdapter(Context context)
        {
            MInflater = LayoutInflater.From(context);

            Resources res = context.Resources;
            MTitles = res.GetStringArray(Resource.Array.actions_names);
            MUrls = res.GetStringArray(Resource.Array.actions_links);

            TypedArray iconsArray = res.ObtainTypedArray(Resource.Array.actions_icons);
            int count = iconsArray.Length();
            MIcons = new int[count];
            for (int i = 0; i < count; ++i)
            {
                MIcons[i] = iconsArray.GetResourceId(i, 0);
            }
            iconsArray.Recycle();
        }

        public override int Count
        {
            get
            {
                return MUrls.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return Uri.Parse(MUrls[position]);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @SuppressLint("DefaultLocale") @Override public Android.Views.View getView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            int type = GetItemViewType(position);

            ViewHolder holder;
            if (convertView == null)
            {
                if (type == VIEW_TYPE_CATEGORY)
                {
                    convertView = MInflater.Inflate(Resource.Layout.category_list_item, parent, false);
                }
                else
                {
                    convertView = MInflater.Inflate(Resource.Layout.action_list_item, parent, false);
                }

                holder = new ViewHolder();
                holder.Text = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
                convertView.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

            if (type != VIEW_TYPE_CATEGORY)
            {
                Drawable icon = convertView.Context.Resources.GetDrawable(MIcons[position]);
                icon.SetBounds(0, 0, icon.IntrinsicWidth, icon.IntrinsicHeight);
                holder.Text.SetCompoundDrawables(icon, null, null, null);
                holder.Text.Text = MTitles[position];
            }
            else
            {
                holder.Text.Text = MTitles[position].ToUpper();
            }

            return convertView;
        }

        public override int ViewTypeCount
        {
            get
            {
                return VIEW_TYPES_COUNT;
            }
        }

        public override int GetItemViewType(int position)
        {
            Uri uri = Uri.Parse(MUrls[position]);
            string scheme = uri.Scheme;
            if ("category".Equals(scheme))
            {
                return VIEW_TYPE_CATEGORY;
            }
            else if ("settings".Equals(scheme))
            {
                return VIEW_TYPE_SETTINGS;
            }
            return VIEW_TYPE_SITES;
        }

        public override bool IsEnabled(int position)
        {
            return GetItemViewType(position) != VIEW_TYPE_CATEGORY;
        }

        private class ViewHolder : Java.Lang.Object
        {
            internal TextView Text;
        }
    }

}