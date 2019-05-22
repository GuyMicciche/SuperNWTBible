//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.Content.Res;

//namespace NWTBible
//{
//    [Activity(Label = "Publications")]
//    public class PublicationsActivity : Activity
//    {
//        private GridView grid;
//        public List<Publication> pubsList;

//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);

//            SetContentView(Resource.Layout.Publications);

//            grid = FindViewById<GridView>(Resource.Id.gridview2);
//            grid.ItemClick += grid_ItemClick;

//            PopulateGridView();

//            ThisApp.LanguageChanged += (sender, e) =>
//            {
//                RunOnUiThread(() =>
//                {
//                    PopulateGridView();
//                });
//            };
//        }

//        void grid_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
//        {
//            var pub = pubsList[e.Position];
//            ThisApp.selectedPublication = pub;

//            ThisApp.ReaderKind = ReaderKind.PublicationReader;
//            var intent = new Intent(this, typeof(ReaderActivity));
//            StartActivity(intent);
//        }

//        void PopulateGridView()
//        {
//            if (!String.IsNullOrEmpty(ThisApp.Language))
//            {
//                var doc = ThisApp.GetNWTFile(ThisApp.Language, "publications");

//                pubsList = new List<Publication>();

//                foreach (var query in doc.Descendants("publication"))
//                {
//                    Publication pub = new Publication()
//                    {
//                        Title = query.Attribute("fulltitle").Value,
//                        ShortTitle = query.Attribute("title").Value.ToUpper(),
//                        Code = query.Attribute("abbr").Value,
//                        Image = query.Attribute("img").Value,
//                        CurrentPage = 0
//                    };

//                    pubsList.Add(pub);
//                }

//                grid.SetAdapter(new PublicationAdapter(this, pubsList));
//            }
//        }

//        public App ThisApp
//        {
//            get
//            {
//                return App.Instance;
//            }
//        }
//    }

//    class PublicationAdapter : BaseAdapter
//    {
//        Activity context;
//        public List<Publication> pubs;

//        public PublicationAdapter(Activity context, List<Publication> pubs)
//            : base()
//        {
//            this.context = context;
//            this.pubs = pubs;
//        }

//        public override int Count
//        {
//            get { return this.pubs.Count; }
//        }

//        public override Java.Lang.Object GetItem(int position)
//        {
//            return position;
//        }

//        public override long GetItemId(int position)
//        {
//            return position;
//        }

//        public override View GetView(int position, View convertView, ViewGroup parent)
//        {
//            var item = this.pubs[position];
//            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.PublicationGridLayout, parent, false)) as RelativeLayout;

//            var pubItem = view.FindViewById<TextView>(Resource.Id.pubTitleItem);
//            pubItem.SetText(item.Title, TextView.BufferType.Normal);
//            pubItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

//            int resourceId = (int)typeof(Resource.Drawable).GetField(item.Image).GetValue(null);

//            Console.WriteLine("Resource: " + resourceId);

//            var pubImage = view.FindViewById<ImageView>(Resource.Id.pubImageItem);
//            pubImage.SetImageResource(resourceId);

//            return view;
//        }

//        public Publication GetItemAtPosition(int position)
//        {
//            return pubs[position];
//        }

//        public App ThisApp
//        {
//            get
//            {
//                return App.Instance;
//            }
//        }
//    }
//}