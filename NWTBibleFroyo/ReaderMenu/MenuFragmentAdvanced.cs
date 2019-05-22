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
    public class MenuFragmentAdvanced : SherlockFragment
    {
        private FragmentTabHost tabHost;
        private Intent intent; 

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            intent = new Intent(Activity, typeof(MenuFragment));            

            tabHost = new FragmentTabHost(Activity);
            tabHost.Setup(Activity, ChildFragmentManager, Resource.Layout.FragmentTabHost);

            View tabIndicator1 = LayoutInflater.From(Activity).Inflate(Resource.Layout.TabIndicator, tabHost.TabWidget, false);
            ((TextView)tabIndicator1.FindViewById(Resource.Id.TabTitle)).Text = "Bible";
            ((ImageView)tabIndicator1.FindViewById(Resource.Id.TabIcon)).SetImageResource(Resource.Drawable.bible);

            View tabIndicator2 = LayoutInflater.From(Activity).Inflate(Resource.Layout.TabIndicator, tabHost.TabWidget, false);
            ((TextView)tabIndicator2.FindViewById(Resource.Id.TabTitle)).Text = "Pubs";
            ((ImageView)tabIndicator2.FindViewById(Resource.Id.TabIcon)).SetImageResource(Resource.Drawable.pubs);

            View tabIndicator3 = LayoutInflater.From(Activity).Inflate(Resource.Layout.TabIndicator, tabHost.TabWidget, false);
            ((TextView)tabIndicator3.FindViewById(Resource.Id.TabTitle)).Text = "Search";
            ((ImageView)tabIndicator3.FindViewById(Resource.Id.TabIcon)).SetImageResource(Resource.Drawable.search);

            tabHost.AddTab(tabHost.NewTabSpec("bible").SetIndicator(tabIndicator1), (new CanonHeaderGridFragment()).Class, null);
            tabHost.AddTab(tabHost.NewTabSpec("publications").SetIndicator(tabIndicator2), (new PublicationsFragment()).Class, null);
            tabHost.AddTab(tabHost.NewTabSpec("search").SetIndicator(tabIndicator3), (new SearchFragment()).Class, null);

            //tabHost.AddTab(tabHost.NewTabSpec("bible").SetIndicator("Bible", Resources.GetDrawable(Resource.Drawable.bible)), (new CanonHeaderGridFragment()).Class, null);
            //tabHost.AddTab(tabHost.NewTabSpec("publications").SetIndicator("Pubs", Resources.GetDrawable(Resource.Drawable.pubs)), (new PublicationsFragment()).Class, null);
            //tabHost.AddTab(tabHost.NewTabSpec("search").SetIndicator("Search", Resources.GetDrawable(Resource.Drawable.search)), (new SearchFragment()).Class, null);

 	        return tabHost;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            tabHost = null;
        }
    }
}