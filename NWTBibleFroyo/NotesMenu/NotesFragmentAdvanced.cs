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
using Android.Support.V4.App;
using Android.Support.V4;

using Xamarin.ActionbarSherlockBinding.App;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentTabHost = Android.Support.V4.App.FragmentTabHost;

using Java.Lang;
using Android.Graphics.Drawables;
using NWTBible.NotesMenu;

namespace NWTBible.ReaderMenu
{
    public class NotesFragmentAdvanced : SherlockFragment
    {
        private FragmentTabHost tabHost;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            tabHost = new FragmentTabHost(Activity);
            tabHost.Setup(Activity, ChildFragmentManager, Resource.Layout.FragmentTabHost);

            View tabIndicator1 = LayoutInflater.From(Activity).Inflate(Resource.Layout.TabIndicator, tabHost.TabWidget, false);
            ((TextView)tabIndicator1.FindViewById(Resource.Id.TabTitle)).Text = "Notes";
            ((ImageView)tabIndicator1.FindViewById(Resource.Id.TabIcon)).SetImageResource(Resource.Drawable.ic_menu_archive);

            View tabIndicator2 = LayoutInflater.From(Activity).Inflate(Resource.Layout.TabIndicator, tabHost.TabWidget, false);
            ((TextView)tabIndicator2.FindViewById(Resource.Id.TabTitle)).Text = "Favorites";
            ((ImageView)tabIndicator2.FindViewById(Resource.Id.TabIcon)).SetImageResource(Resource.Drawable.ic_menu_star);

            View tabIndicator3 = LayoutInflater.From(Activity).Inflate(Resource.Layout.TabIndicator, tabHost.TabWidget, false);
            ((TextView)tabIndicator3.FindViewById(Resource.Id.TabTitle)).Text = "Highlights";
            ((ImageView)tabIndicator3.FindViewById(Resource.Id.TabIcon)).SetImageResource(Android.Resource.Drawable.IcMenuEdit);

            tabHost.AddTab(tabHost.NewTabSpec("notes").SetIndicator(tabIndicator1), (new NotesFragment()).Class, null);
            tabHost.AddTab(tabHost.NewTabSpec("favorites").SetIndicator(tabIndicator2), (new FavoritesFragment()).Class, null);
            tabHost.AddTab(tabHost.NewTabSpec("highlights").SetIndicator(tabIndicator3), (new HighlightsFragment()).Class, null);

            //tabHost.AddTab(tabHost.NewTabSpec("notes").SetIndicator("", Resources.GetDrawable(Resource.Drawable.ic_menu_archive)), (new NotesFragment()).Class, null);
            //tabHost.AddTab(tabHost.NewTabSpec("favorites").SetIndicator("", Resources.GetDrawable(Resource.Drawable.ic_menu_star)), (new FavoritesFragment()).Class, null);
            //tabHost.AddTab(tabHost.NewTabSpec("highlights").SetIndicator("", Resources.GetDrawable(Android.Resource.Drawable.IcMenuEdit)), (new HighlightsFragment()).Class, null);

            //TabWidget tw = (TabWidget)tabHost.FindViewById(Android.Resource.Id.Tabs);
            //for (var i = 0; i < 3; i++)
            //{
            //    View tabView = tw.GetChildTabViewAt(i);
            //    TextView tv = (TextView)tabView.FindViewById(Android.Resource.Id.Title);
            //    ImageView iv = (ImageView)tabView.FindViewById(Android.Resource.Id.Icon);
            //    //tabView.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.ic_menu_star));
            //    iv.SetImageResource(Resource.Drawable.ic_menu_star);
            //}

 	        return tabHost;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            tabHost = null;
        }
    }
}