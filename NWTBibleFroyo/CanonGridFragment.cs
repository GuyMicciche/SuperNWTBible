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


namespace NWTBible
{
    public class CanonGridFragment : SherlockFragment, StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener, StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener
    {
        public string canon;

        public List<BibleBook> booksList = new List<BibleBook>();

        public GridView grid;

        private Toast toast;

        public CanonGridFragment(string canon)
        {
            this.canon = canon;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.CanonGrid, container, false);
        }

        public void OnHeaderClick(AdapterView parent, Android.Views.View view, long id)
        {
            string text = "Header " + ((TextView)view.FindViewById(Android.Resource.Id.Text1)).Text + " was tapped.";
            if (toast == null)
            {
                toast = Toast.MakeText(Activity, text, ToastLength.Short);
            }
            else
            {
                toast.SetText(text);
            }
            toast.Show();
        }

        public bool OnHeaderLongClick(AdapterView parent, Android.Views.View view, long id)
        {
            string text = "Header " + ((TextView)view.FindViewById(Android.Resource.Id.Text1)).Text + " was long pressed.";
            if (toast == null)
            {
                toast = Toast.MakeText(Activity, text, ToastLength.Short);
            }
            else
            {
                toast.SetText(text);
            }
            toast.Show();

            return true;
        }

        public override void OnActivityCreated(Bundle bundle)
        {
            base.OnActivityCreated(bundle);

            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            grid = (GridView)view.FindViewById(Resource.Id.asset_grid);
            grid.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs args)
            {

            };

            string[] array = { "Afghanistan","Albania","Algeria","Andorra","Angola","Antigua &amp; Barbuda","Argentina","Armenia","Australia","Austria","Azerbaijan", 
"Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bhutan","Bolivia","Bosnia and Herzegovina","Botswana","Brazil","Brunei","Bulgaria","Burkina Faso","Burma","Burundi",
"Cambodia","Cameroon","Canada","Cape Verde","Central African Republic","Chad","Chile","China","Colombia","Comoros","Congo, Democratic Republic of the","Congo, Republic of the","Costa Rica","Cote d\'voire","Croatia","Cuba","Cyprus","Czech Republic",
"Denmark","Djibouti","Dominica","Dominican Republic",
"Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea","Estonia","Ethiopia",
"Fiji","Finland","France",
"Gabon","Gambia, The","Georgia","Ghana","Greece","Grenada","Guatemala","Guinea","Guinea-Bissau","Guyana",
"Haiti","Holy See","Honduras","Hong Kong","Hungary",
"Iceland","India","Indonesia","Iran","Iraq","Ireland","Israel","Italy",
"Jamaica","Japan","Jordan",
"Kazakhstan","Kenya","Kiribati","Korea, North","Korea, South","Kosovo","Kuwait","Kyrgyzstan",
"Laos","Latvia","Lebanon","Lesotho","Liberia","Libya","Liechtenstein","Lithuania","Luxembourg",
"Macau","Macedonia","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands","Mauritania","Mauritius","Mexico","Micronesia","Moldova","Monaco","Mongolia","Montenegro","Morocco","Mozambique",
"Namibia","Nauru","Nepal","Netherlands","Netherlands Antilles","New Zealand","Nicaragua","Niger","Nigeria","North Korea","Norway",
"Oman",
"Pakistan","Palau","Palestinian Territories","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Poland","Portugal",
"Qatar",
"Romania","Russia","Rwanda",
"Saint Kitts and Nevis","Saint Lucia","Saint Vincent and the Grenadines","Samoa","San Marino","Sao Tome and Principe","Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Solomon Islands","Somalia","South Africa","South Korea","South Sudan","Spain","Sri Lanka","Sudan","Suriname","Swaziland","Sweden","Switzerland","Syria",
"Taiwan","Tajikistan","Tanzania","Thailand ","Timor-Leste","Togo","Tonga","Trinidad and Tobago","Tunisia","Turkey","Turkmenistan","Tuvalu",
"Uganda","Ukraine","United Arab Emirates","United Kingdom","Uruguay","Uzbekistan",
"Vanuatu","Venezuela","Vietnam",
"Yemen",
"Zambia","Zimbabwe", };

            grid.SetAdapter(new StickyGridHeadersSimpleArrayAdapter(Activity, array, Resource.Layout.GridHeader, Resource.Layout.GridItem));

            ((StickyGridHeadersGridView)grid).SetOnHeaderClickListener(this);
            ((StickyGridHeadersGridView)grid).SetOnHeaderLongClickListener(this);

            SetHasOptionsMenu(true);
        }

        public override void OnDestroy()
        {
            base.OnPause();

            ThisApp.LanguageChanged -= ThisApp_LanguageChanged;
        }

        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            
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