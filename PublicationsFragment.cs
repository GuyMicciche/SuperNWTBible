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
using Xamarin.ActionbarSherlockBinding.App;
using NWTBible.ReaderMenu;
using Android.Graphics;

namespace NWTBible
{
    public class PublicationsFragment : SherlockFragment
    {
        public GridView gridView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle bundle)
        {
            base.OnActivityCreated(bundle);

            PopulateGridView();

            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
        }        

        public override void OnDestroy()
        {
            base.OnPause();

            ThisApp.LanguageChanged -= ThisApp_LanguageChanged;
        }

        private void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                Activity.RunOnUiThread(() =>
                {
                    gridView.Adapter = new PublicationAdapter(Activity, ThisApp.allPublications);
                });
            }
            else
            {
                Activity.RunOnUiThread(() =>
                {
                    gridView.Adapter = null;
                });
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            base.OnCreateView(inflater, container, bundle);

            View view = inflater.Inflate(Resource.Layout.Publications, container, false);

            gridView = view.FindViewById<GridView>(Resource.Id.gridview2);
            gridView.SetColumnWidth(192);
            gridView.SetNumColumns(-1);
            gridView.StretchMode = StretchMode.StretchColumnWidth;
            gridView.SetGravity(GravityFlags.Center); 

            gridView.ItemClick += gridView_ItemClick;

            return view;
        }

        private void PopulateGridView()
        {
            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                gridView.Adapter = new PublicationAdapter(Activity, ThisApp.allPublications);
            }
        }

        void gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var pub = ThisApp.allPublications[e.Position];
            ThisApp.selectedPublication = pub;

            ThisApp.selectedPublication.CurrentPage = 1;

            ThisApp.allPublicationArticles = ThisApp.GetAllPublicationArticles();

            Console.WriteLine(ThisApp.selectedPublication.Image);

            if (ThisApp.selectedPublication.Image == "dailytext")
            {
                ThisApp.ReaderKind = ReaderKind.DailyTextReader;
                var intent = new Intent(Activity, typeof(ReaderActivity));
                StartActivity(intent);  
            }
            else
            {
                ThisApp.ReaderKind = ReaderKind.PublicationReader;
                Intent intent = new Intent(Activity, ThisApp.MainReader.Class);
                StartActivity(intent);
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

    class PublicationAdapter : BaseAdapter
    {
        Activity context;
        public List<Publication> pubs;

        public PublicationAdapter(Activity context, List<Publication> pubs)
            : base()
        {
            this.context = context;
            this.pubs = pubs;
        }

        public override int Count
        {
            get { return this.pubs.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this.pubs[position];
            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.PublicationGridLayout, parent, false)) as RelativeLayout;

            var pubItem = view.FindViewById<TextView>(Resource.Id.pubTitleItem);
            pubItem.SetText(item.ShortTitle, TextView.BufferType.Normal);
            pubItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            //pubItem.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(context));

            int resourceId = (int)typeof(Resource.Drawable).GetField(item.Image).GetValue(null);

            Console.WriteLine("Resource: " + resourceId);

            //Bitmap jpg = BitmapFactory.DecodeFile(ThisApp.GetLanguageFile(ThisApp.Language, "jr.jpg").Path);
            
            var pubImage = view.FindViewById<ImageView>(Resource.Id.pubImageItem);
            ViewGroup.LayoutParams lp = (ViewGroup.LayoutParams)pubImage.LayoutParameters;
            lp.Width = 96;
            lp.Height = 96;
            pubImage.LayoutParameters = lp;
            pubImage.SetImageResource(resourceId);
            //pubImage.SetImageBitmap(jpg);

            return view;
        }

        public Publication GetItemAtPosition(int position)
        {
            return pubs[position];
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