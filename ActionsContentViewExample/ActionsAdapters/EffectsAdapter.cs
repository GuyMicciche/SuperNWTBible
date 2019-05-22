using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using Android.Graphics.Drawables;

namespace ActionsContentViewExample.ActionsAdapters
{
    public class EffectsAdapter : BaseAdapter
    {
        private readonly LayoutInflater MInflater;

        private readonly string[] MTitles;
        private readonly int[] MLayouts;
        private readonly string[][] MHtmls;

        public EffectsAdapter(Context context)
        {
            MInflater = LayoutInflater.From(context);

            Resources res = context.Resources;

            MTitles = res.GetStringArray(Resource.Array.effects_name);

            string[] actionsHtml = res.GetStringArray(Resource.Array.effects_actions_html);
            string[] contentHtml = res.GetStringArray(Resource.Array.effects_content_html);

            MHtmls = new string[][] { actionsHtml, contentHtml };

            TypedArray layoutsArray = res.ObtainTypedArray(Resource.Array.effect_layouts);

            int count = layoutsArray.Length();
            MLayouts = new int[count];

            for (int i = 0; i < count; ++i)
            {
                MLayouts[i] = layoutsArray.GetResourceId(i, 0);
            }

            layoutsArray.Recycle();
        }

        public virtual string GetActionsHtml(int position)
        {
            return MHtmls[0][position];
        }

        public virtual string GetContentHtml(int position)
        {
            return MHtmls[1][position];
        }

        public virtual string GetEffectTitle(int position)
        {
            return MTitles[position];
        }

        public override int Count
        {
            get
            {
                return MLayouts.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public int GetItemAtPosition(int position)
        {
            return MLayouts[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder;
            if (convertView == null)
            {
                convertView = MInflater.Inflate(Resource.Layout.action_list_item, parent, false);

                holder = new ViewHolder();
                holder.Text = (TextView)convertView.FindViewById(Android.Resource.Id.Text1);

                Drawable icon = convertView.Context.Resources.GetDrawable(Resource.Drawable.ic_action_effects);
                icon.SetBounds(0, 0, icon.IntrinsicWidth, icon.IntrinsicHeight);
                holder.Text.SetCompoundDrawables(icon, null, null, null);
                holder.Text.Text = MTitles[position];

                convertView.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

            holder.Text.Text = MTitles[position];

            return convertView;
        }

        private class ViewHolder : Java.Lang.Object
        {
            internal TextView Text;
        }
    }

}