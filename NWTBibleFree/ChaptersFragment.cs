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
using Fragment = Android.Support.V4.App.Fragment;

namespace NWTBibleFree
{
    public class ChaptersFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Chapters, container, false);
            var grid = view.FindViewById<GridView>(Resource.Id.gridview);

            Button buyButton = new Button(container.Context);
            buyButton.Text = "Hello";

            grid.AddView(buyButton);

            return view;
        }
    }
}