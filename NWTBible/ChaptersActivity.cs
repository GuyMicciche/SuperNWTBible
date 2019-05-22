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
using Android.Preferences;

using NWTBible.ReaderMenu;
using SlidingMenuBinding.Lib.App;

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible", Icon = "@drawable/icon")]
    public class ChaptersActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight));

            Title = "New World Translation Bible — " + ThisApp.selectedBook.Title;

            SetContentView(Resource.Layout.Chapters);  

            //GridView gridView = new GridView(this);
            //gridView.LayoutParameters = new GridView.LayoutParams(GridLayout.LayoutParams.FillParent, GridLayout.LayoutParams.FillParent);
            //gridView.SetColumnWidth(90);
            //gridView.SetNumColumns(-1);
            //gridView.SetVerticalSpacing(10);
            //gridView.SetHorizontalSpacing(10);
            //gridView.StretchMode = StretchMode.StretchColumnWidth;
            //gridView.SetGravity(GravityFlags.Center);            

            int num = int.Parse(ThisApp.selectedBook.Chapters);
            string[] chapters = new string[num];
            for (var i = 0; i < chapters.Length; i++)
            {
                chapters[i] = (i + 1).ToString();
            }            

            var grid = FindViewById<GridView>(Resource.Id.gridview);
            grid.SetAdapter(new ChapterButtonAdapter(this, chapters));
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }  
    }

    public class ChapterButtonAdapter : BaseAdapter
    {
        private Activity context;
        private string[] chapters;

        // Gets the context so it can be used later
        public ChapterButtonAdapter(Activity context, string[] chapters) : base()
        {
            this.context = context;
            this.chapters = chapters;
        }

        // Total number of things contained within the adapter
        public override int Count
        {
            get { return chapters.Length; }
        }

        // Require for structure, not really used in my code.
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        // Require for structure, not really used in my code. Can be used to get the id of an item in the adapter for manual control.
        public override long GetItemId(int position) 
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent) 
        {
            Button button = new Button(context);
            button.SetText(chapters[position], TextView.BufferType.Normal);
            //button.SetTextColor(Android.Graphics.Color.White);
            button.SetPadding(4, 4, 4, 4);
            button.TextSize = 36;
            button.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            button.LayoutParameters = new GridView.LayoutParams(96, 96);
            //button.BackgroundResource(R.drawable.button);
            button.Id = position;

            button.Click += button_Click;

            return button;
        }

        void button_Click(object sender, EventArgs e)
        {
            var chapter = (sender as Button).Text;
            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = chapter
            };
            ThisApp.ReaderKind = ReaderKind.BibleReader;
            
            Intent intent = new Intent(context, ThisApp.MainReader.Class);
            context.StartActivity(intent);
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