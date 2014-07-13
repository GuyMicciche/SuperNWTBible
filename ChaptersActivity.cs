using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Preferences;

using Xamarin.ActionbarSherlockBinding.App;
using Android.Content.Res;

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible", Icon = "@drawable/icon", Theme = "@style/Theme.Sherlock")]
    public class ChaptersActivity : SherlockActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            Title = ThisApp.selectedBook.Title;

            SetContentView(Resource.Layout.Chapters);

            int num = int.Parse(ThisApp.selectedBook.Chapters);
            string[] chapters = new string[num];
            for (var i = 0; i < chapters.Length; i++)
            {
                chapters[i] = (i + 1).ToString();
            }

            var chaptersTitle = FindViewById<TextView>(Resource.Id.chaptersTitle).Text = ThisApp.selectedBook.Title;

            var gridView = FindViewById<GridView>(Resource.Id.chaptersGridView);
            gridView.Adapter = new ChapterButtonAdapter(this, chapters);
            //gridView.LayoutParameters = new GridView.LayoutParams(GridLayout.LayoutParams.FillParent, GridLayout.LayoutParams.FillParent);
            gridView.SetColumnWidth(84);
            gridView.SetClipToPadding(false);
            gridView.SetNumColumns(-1);
            gridView.SetVerticalSpacing(4);
            gridView.SetHorizontalSpacing(4);
            gridView.SetPadding(8, 8, 8, 8);
            gridView.StretchMode = StretchMode.NoStretch;
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
        public ChapterButtonAdapter(Activity context, string[] chapters)
            : base()
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
            TextView button = new TextView(context);
            button.SetText(chapters[position], TextView.BufferType.Normal);
            button.TextSize = 32;
            button.SetTypeface(ThisApp.Font(context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            button.SetHeight(84);
            button.SetBackgroundResource(Resource.Drawable.metro_button_style);
            button.Gravity = GravityFlags.Center;
            //button.SetPadding(0, 0, 0, 0);
            //button.SetTextColor(Resources.System.GetColorStateList(Resource.Color.metro_button_text_style));
            //button.SetTextColor(Resources.System.GetColor(Resource.Color.metro_button_text_style));
            button.Id = position;

            button.Click += button_Click;

            return button;
        }

        void button_Click(object sender, EventArgs e)
        {
            var chapter = (sender as TextView).Text;
            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = chapter
            };
            ThisApp.ReaderKind = ReaderKind.BibleReader;

            context.Finish();

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