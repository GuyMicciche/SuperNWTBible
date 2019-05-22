using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;

using StickyGridHeaders;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;

using Fragment = Android.Support.V4.App.Fragment;
using ListFragment = Android.Support.V4.App.ListFragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

using NWTBible;

namespace NWTBible
{
    public class CanonHeaderGridFragment : SherlockFragment
    {
        public View view;

        public TextView hebrewTextView;
        public TextView greekTextView;
        public LibraryGridView hebrewLibraryGridView;
        public LibraryGridView greekLibraryGridView;

        public CanonHeaderGridFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.BibleBookGridFragment, container, false);
            this.view = view;

            hebrewTextView = view.FindViewById<TextView>(Resource.Id.hebrewBibleTitle);
            greekTextView = view.FindViewById<TextView>(Resource.Id.greekBibleTitle);

            hebrewLibraryGridView = view.FindViewById<LibraryGridView>(Resource.Id.hebrewBibleBooks);
            greekLibraryGridView = view.FindViewById<LibraryGridView>(Resource.Id.greekBibleBooks);
                        
            return view;
        }

        public override void OnActivityCreated(Bundle bundle)
        {
            base.OnActivityCreated(bundle);

            PopulateGrid(false);

            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
        }

        public override void OnDestroy()
        {
            base.OnPause();

            ThisApp.LanguageChanged -= ThisApp_LanguageChanged;
        }

        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            PopulateGrid(true);            
        }

        void PopulateGrid(bool runOnUIThread)
        {
            if (runOnUIThread)
            {
                if (!String.IsNullOrEmpty(ThisApp.Language))
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var hebrewAdapter = new ArrayAdapter(Activity, Resource.Layout.GridItem, ThisApp.GetCanonBooks("hebrew").Select(s => s.Title.ToTitleCase()).ToList());
                        var greekAdapter = new ArrayAdapter(Activity, Resource.Layout.GridItem, ThisApp.GetCanonBooks("greek").Select(s => s.Title.ToTitleCase()).ToList());

                        hebrewTextView.Text = "Hebrew-Aramaic Scriptures";
                        //hebrewTextView.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(ThisApp.context));

                        greekTextView.Text = "Christian Greek Scriptures";
                        //greekTextView.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(ThisApp.context));

                        hebrewLibraryGridView.SetAdapter(hebrewAdapter);
                        greekLibraryGridView.SetAdapter(greekAdapter);
                    });
                }
                else
                {
                    Activity.RunOnUiThread(() =>
                    {
                        hebrewTextView.Text = "";
                        greekTextView.Text = "";

                        hebrewLibraryGridView.SetAdapter(null);
                        greekLibraryGridView.SetAdapter(null);
                    });
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(ThisApp.Language))
                {
                    var hebrewAdapter = new ArrayAdapter(Activity, Resource.Layout.GridItem, ThisApp.GetCanonBooks("hebrew").Select(s => s.Title.ToTitleCase()).ToList());
                    var greekAdapter = new ArrayAdapter(Activity, Resource.Layout.GridItem, ThisApp.GetCanonBooks("greek").Select(s => s.Title.ToTitleCase()).ToList());

                    hebrewTextView.Text = "Hebrew-Aramaic Scriptures";
                    //hebrewTextView.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(ThisApp.context));

                    greekTextView.Text = "Christian Greek Scriptures";
                    //greekTextView.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(ThisApp.context));

                    hebrewLibraryGridView.SetAdapter(hebrewAdapter);
                    greekLibraryGridView.SetAdapter(greekAdapter);
                }
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
}