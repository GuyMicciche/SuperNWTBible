using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using Android.Graphics;

namespace NWTBible
{
    [Activity(Label = "Magazines")]
    public class MagazinesActivity : Activity
    {
        private ListView grid;

        public List<Publication> pubsList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Magazines);

            grid = FindViewById<ListView>(Resource.Id.gridview2);
            //grid.ItemClick += grid_ItemClick;

            PopulateGridView();

            ThisApp.LanguageChanged += (sender, e) =>
            {
                RunOnUiThread(() =>
                {
                    PopulateGridView();
                });
            };
        }

        void grid_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // TODO 
        }

        void PopulateGridView()
        {
            if (!String.IsNullOrEmpty(ThisApp.Language))
            {
                var doc = ThisApp.GetNWTFile(ThisApp.Language, "epubs");

                pubsList = new List<Publication>();

                foreach (var query in doc.Descendants("item"))
                {
                    Publication pub = new Publication()
                    {
                        Category = query.Attribute("cat").Value,
                        ShortTitle = query.Element("name").Value,
                        Title = query.Attribute("cat").Value + " " + query.Element("name").Value,
                        Code = query.Element("code").Value,
                        Image = query.Element("img").Value,
                        EPUBLink = query.Element("url").Value,
                        Year = int.Parse(query.Element("year").Value),
                        Month = int.Parse(query.Element("month").Value)
                    };

                    pubsList.Add(pub); 
                }

                pubsList.OrderByDescending(s => s.Year).ThenByDescending(s => s.Month);

                grid.SetAdapter(new MagazineAdapter(this, pubsList));
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

    class MagazineAdapter : BaseAdapter<Publication>
    {
        Activity context;
        public List<Publication> pubs;
        public Dictionary<string, List<Publication>> publications;
        public List<string> categories = new List<string>();
        private readonly IList<object> rows;

        public MagazineAdapter(Activity context, List<Publication> pubs)
            : base()
        {
            this.context = context;
            this.pubs = pubs;

            publications = new Dictionary<string, List<Publication>>();

            // This foreach create categoroes by moonth
            foreach (var pub in pubs)
            {
                if (!categories.Any(c => c == pub.ShortTitle))
                {
                    categories.Add(pub.ShortTitle);
                }
            }

            Console.WriteLine(categories.Count() + " categories");

            // This foreach 
            foreach (var header in categories)
            {
                List<Publication> tempList = new List<Publication>();

                var items = pubs.Where(t => t.ShortTitle.ToString().Equals(header.ToString())).ToList();

                Console.WriteLine(items.Count() + " pub(s) in the category of " + header);

                foreach (var pub in items)
                {
                    tempList.Add(pub);
                }

                publications.Add(header, tempList);
            }

            rows = new List<object>();
            foreach (var section in publications.Keys)
            {
                rows.Add(section);
                
                foreach (var item in publications[section])
                {
                    rows.Add(item);                    
                }
            }
        }

        public Publication GetPublication(int position)
        {
            return (Publication)rows[position];
        }

        public override Publication this[int position]
        {
            get
            {
                return (Publication)rows[position];
            }
        }

        public override int Count
        {
            get { return this.rows.Count; }
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
            try
            {
                var item = this.rows[position];
                View view = null;

                // If a header
                if (item is string)
                {
                    view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.SectionHeader, parent, false)) as LinearLayout;
                    view.Clickable = false;
                    view.LongClickable = false;
                    view.SetOnClickListener(null);

                    var pubItem = view.FindViewById<TextView>(Resource.Id.headerText);
                    pubItem.Text = (string)item;
                    pubItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

                }
                // If not 
                else
                {
                    view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.MagazineItem, parent, false)) as LinearLayout;
                    //view.SetOnClickListener(

                    var pubItem = view.FindViewById<TextView>(Resource.Id.pubTitleItem);
                    pubItem.Text = ((Publication)item).Category;
                    pubItem.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

                    var pubImage = view.FindViewById<ImageView>(Resource.Id.pubImageItem);
                    this.context.RunOnUiThread(() => pubImage.SetImageURI(ThisApp.GetRemoteImage(((Publication)item).Image, ((Publication)item).Code + ".jpg")));

                    view.Click += delegate
                    {
                        var pub = ((Publication)item);
                        ThisApp.selectedPublication = pub;

                        // If file not there, download it
                        if (!ThisApp.EpubExists(pub.Code + ".epub"))
                        {
                            // TODO Download epub
                        }
                        // If file is there unzip it and go to the pub
                        else
                        {
                            ThisApp.UnzipEpub(pub.Code, pub.Code + ".epub");

                            ThisApp.ReaderKind = ReaderKind.EPUBReader;
                            var intent = new Intent(context, typeof(ReaderActivity));
                            context.StartActivity(intent);
                        }
                    };
                }

                return view;
            }
            catch (Exception ex)
            {

            }

            return null;
            
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