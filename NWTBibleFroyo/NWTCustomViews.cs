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
using Android.Util;
using Android.Graphics;
using Android.Webkit;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Android.Preferences;
using Android.Content.Res;

namespace NWTBible
{
    public class IonTextView : TextView
    {
        private Context context;
        private string ttfName;

        public IonTextView(Context context, IAttributeSet attrs) 
            : base(context, attrs)
        {
            this.context = context;

            for (int i = 0; i < attrs.AttributeCount; i++) 
            {
                this.ttfName = attrs.GetAttributeValue("http://schemas.android.com/apk/res/com.gem.nwtbiblefree", "ttf");

                init();
            }
        }

        private void init() 
        {
            Typeface font = Typeface.CreateFromAsset(context.Assets, ttfName);
            SetTypeface(font, Android.Graphics.TypefaceStyle.Normal);
        }

        public override void SetTypeface(Typeface tf, TypefaceStyle style)
        {
 	         base.SetTypeface(tf, style);
        }
    }
    
    public class DonateDialogPreference : DialogPreference
    {
        private Context context;

        public DonateDialogPreference(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.context = context;
        }

        protected override void OnDialogClosed(bool positiveResult)
        {
            base.OnDialogClosed(positiveResult);
        }

        protected override void OnPrepareDialogBuilder(AlertDialog.Builder builder)
        {
            base.OnPrepareDialogBuilder(builder);

            builder.SetIcon(Resource.Drawable.Icon);
            builder.SetPositiveButton("Google Wallet", pos_Click);
            builder.SetNegativeButton("Pay Pal", neg_Click);
        }

        void pos_Click(object sender, EventArgs e)
        {
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(App.STOREHOUSE + "donate.html"));
            context.StartActivity(browserIntent);
        }

        void neg_Click(object sender, EventArgs e)
        {
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=J3WJ7MMYPV8R8"));
            context.StartActivity(browserIntent);
        }
    }

    public class NWTBibleWebView : WebViewClient
    {
        private Activity _activity;
        private List<NavPoint> _list;
        public bool _highlight;
        public bool _externalLink;
        public string _currentChapter;
        public WebView _webview;
        public string _language;

        public Dialog dialog;

        public  NWTBibleWebView(Activity activity, string language, bool highlight = false, List<NavPoint> list = null, string currentChapter = null)
        {
            _activity = activity;
            _language = language;
            _highlight = highlight;
            _list = list;
            _currentChapter = (currentChapter == null) ? thisApp.selectedChapter.ChapterNumber : currentChapter;
            _externalLink = false;
        }

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            _externalLink = true;
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 8000;  //if it's an aspx give it a few to warm up

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    string html = reader.ReadToEnd();

                    string[] beginning1 = Regex.Split(html, "<title>");
                    string removedBeginning1 = beginning1[beginning1.Length - 1];
                    html = removedBeginning1;
                    string[] end1 = Regex.Split(html, "</title>");
                    string pagetitle = end1[end1.Length - 2];
                    pagetitle = pagetitle.Replace("&mdash;", "—");

                    string[] beginning = Regex.Split(html, "<div id=\"main\">");
                    string removedBeginning = beginning[beginning.Length - 1];
                    html = "<div id=\"content\"  >" + removedBeginning;
                    string[] end = Regex.Split(html, "<div id=\"contentFooter\">");
                    string content = end[end.Length - 2];

                    //html = "<html><body><h1>" + pagetitle + "</h1></body></html>\n" + "\n<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                    html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" + 
				                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" + 
				                "<link href=\"ui/web/wol.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" + 
				                "<script src=\"ui/web/verseselection.jsf\"></script>\n" +
				                "<body class=\"calibre\">\n" +
                                "<div class=\"body\">" + content + "</div>\n" +
                                "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
                    html = html.Replace("/en", "http://m.wol.jw.org/en");

                    //view.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

                    // This way is pretty cool!
                    var newdialog = thisApp.PresentationDialog(_activity, pagetitle, html);
                    dialog = newdialog;
                    dialog.Show();
                }

                _externalLink = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                Toast.MakeText(_activity, "Sorry, unable to retreive document.", ToastLength.Long).Show();

                _externalLink = false;
            }

            return true;
        }

        public override void OnPageFinished(WebView webview, string url)
        {
            base.OnPageFinished(webview, url);

            // Always set the title of the Activity to the current Publication + the current Page
            if (thisApp.ReaderKind == ReaderKind.EPUBReader)
            {
                _activity.Title = thisApp.selectedPublication.Title + " — " + webview.Title;
            }
            else if (thisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                //_context.Title = thisApp.selectedPublication.Title + " — " + thisApp.selectedPublication.ShortTitle;
            }
            else if (thisApp.ReaderKind == ReaderKind.BibleReader)
            {
                //_context.Title = thisApp.selectedChapter.BookAndChapter;
                //_context.Title = "New World Translation Bible"; 
            }

            //if (_externalLink)
            //{
            //    RunJs(webview, "window.VerseSelection.GetContentHTML('<html><head>'+document.getElementById('content').innerHTML+'</head></html>');");

            //    _externalLink = false;
            //}

            if (_highlight)
            {
                string[] verses = thisApp.selectedNote.ScriptureForHighlight.Split(',');
                int pos = 2;
                string first = verses[2].ToString();
                while (pos < verses.Length)
                {
                    string v = verses[pos].ToString();
                    if (v.Equals(first))
                    {
                        RunJs(webview, "ScrollToHighlight('" + EscapeJsString(first) + "');");
                    }

                    if (thisApp.preferences.GetBoolean("allowHighlighting", false))
                    {
                        RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "', false);");
                    }

                    pos++;
                }

                _highlight = false;
            }
            else
            {
                RunJs(webview, "ClearSelection();");
            }

            // Highlight user highlights
            if (thisApp.preferences.GetBoolean("recordEveryHighlight", false))
            {
                var chapterVerses = thisApp.highlightedScriptures;
                List<string> verseNumbersList = new List<string>();
                for (int cv = 0; cv < chapterVerses.Count; cv++)
                {
                    var element = chapterVerses.ElementAt(cv);
                    if (element.Chapter.ChapterNumber.Equals(_currentChapter) && element.Book.Name.Equals(thisApp.selectedBook.Name))
                    {
                        verseNumbersList.Add(chapterVerses.ElementAt(cv).VerseNumber);
                    }
                }

                foreach (string v in verseNumbersList)
                {
                    RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "', false);");
                }
            }

            if (!_language.Equals("English"))
            {
                RunJs(webview, "SwitchFont();");
            }

            if (!thisApp.preferences.GetBoolean("allowHighlighting", false))
            {
                RunJs(webview, "DisconnectHighlighting();");
            }

            if (_list != null)
            {
                // Make the page numbers match what document page is actually loaded in the webview
                thisApp.selectedPublication.CurrentPage = _list.First(e => e.Title == webview.Title).Order - 1;
            }

            thisApp.doHighlight = false;
            thisApp.selectedVerses = new List<BibleVerse>();
        }

        public void RunJs(WebView webview, String js)
        {
            webview.LoadUrl("javascript:" + js);
        }

        /**
        * Helper method to escape JavaScript strings. Useful when passing strings to a WebView via
        * "javascript:" calls.
        */
        public string EscapeJsString(String s)
        {
            if (s == null)
            {
                return "";
            }

            return s.Replace("'", "\\'").Replace("\"", "\\\"");
        }

        public void RefreshView(WebView webview)
        {
            if (_highlight)
            {
                string[] verses = thisApp.selectedNote.ScriptureForHighlight.Split(',');
                int pos = 2;
                string first = verses[2].ToString();
                while (pos < verses.Length)
                {
                    string v = verses[pos].ToString();
                    Console.WriteLine("Verse: " + v);

                    if (v.Equals(first))
                    {
                        RunJs(webview, "ScrollToHighlight('" + EscapeJsString(first) + "');");
                    }

                    if (thisApp.preferences.GetBoolean("allowHighlighting", true))
                    {
                        RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "', false);");
                    }

                    pos++;
                }

                _highlight = false;
            }
            else
            {
                RunJs(webview, "ClearSelection();");
            }

            // Highlight user highlights
            if (thisApp.preferences.GetBoolean("recordEveryHighlight", false))
            {
                var chapterVerses = thisApp.highlightedScriptures;
                List<string> verseNumbersList = new List<string>();
                for (int cv = 0; cv < chapterVerses.Count; cv++)
                {
                    var element = chapterVerses.ElementAt(cv);
                    if (element.Chapter.ChapterNumber.Equals(_currentChapter) && element.Book.Name.Equals(thisApp.selectedBook.Name))
                    {
                        verseNumbersList.Add(chapterVerses.ElementAt(cv).VerseNumber);
                    }
                }

                foreach (string v in verseNumbersList)
                {
                    RunJs(webview, "HighlightVerse('" + EscapeJsString(v) + "', false);");
                }
            }
        }

        public App thisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class LibraryGridView : AdapterView
    {
        public IListAdapter adapter = null;
        public int columnWidth = 48;
        public int numColumns = 10;
        public bool d = true;
        public int horizontalSpacing = 4;
        public int verticalSpacing = 4;
        public int measuredWidth = 0;
        public int measuredHeight = 0;

        public LibraryGridView(Context paramContext)
            : base(paramContext)
        {
        }

        public LibraryGridView(Context paramContext, IAttributeSet paramAttributeSet)
            : base(paramContext, paramAttributeSet)
        {
            TypedArray localTypedArray = paramContext.ObtainStyledAttributes(paramAttributeSet, Resource.Styleable.LibraryGridView);
            GridViewParams(localTypedArray);
            localTypedArray.Recycle();
        }

        public LibraryGridView(Context paramContext, IAttributeSet paramAttributeSet, int paramInt)
            : base(paramContext, paramAttributeSet, paramInt)
        {
            TypedArray localTypedArray = paramContext.ObtainStyledAttributes(paramAttributeSet, Resource.Styleable.LibraryGridView, paramInt, 0);
            GridViewParams(localTypedArray);
            localTypedArray.Recycle();
        }

        public void GridViewParams(TypedArray paramTypedArray)
        {
            this.HorizontalSpacing = paramTypedArray.GetDimensionPixelSize(0, 4);
            this.VerticalSpacing = paramTypedArray.GetDimensionPixelSize(1, 4);
            this.NumColumns = paramTypedArray.GetInt(3, 10);
            this.ColumnWidth = paramTypedArray.GetDimensionPixelSize(2, 48);
        }

        public void SetAdapter(ArrayAdapter adapter)
        {
            RawAdapter = adapter;   
        }
        
        protected override Java.Lang.Object RawAdapter
        {
            get
            {
                return this.adapter.JavaCast<Java.Lang.Object>();
            }
            set
            {
                this.adapter = value.JavaCast<global::Android.Widget.IListAdapter>();
                RemoveAllViewsInLayout();
                for (int i = 0; i < ((IListAdapter)this.adapter).Count; i++)
                {
                    View localView = ((IListAdapter)this.adapter).GetView(i, null, this);
                    //localView.FindViewById<TextView>(Android.Resource.Id.Text1).SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(ThisApp.context));
                    //localView.FindViewById<TextView>(Android.Resource.Id.Text1).SetTypeface(ThisApp.Font(ThisApp.context, "ionbold"), Android.Graphics.TypefaceStyle.Normal);

                    ViewGroup.LayoutParams localLayoutParams = localView.LayoutParameters;
                    if (localLayoutParams == null)
                    {
                        localLayoutParams = new ViewGroup.LayoutParams(-2, -2);
                    }
                    localView.Clickable = true;
                    localView.SetOnClickListener(new CustomLibraryGridViewListener(this, i));
                    AddViewInLayout(localView, i, localLayoutParams);
                    localView.Measure(GetChildMeasureSpec(View.MeasureSpec.MakeMeasureSpec(this.columnWidth, MeasureSpecMode.AtMost), 0, localLayoutParams.Width), GetChildMeasureSpec(View.MeasureSpec.MakeMeasureSpec(0, 0), 0, localLayoutParams.Height));
                }
                RequestLayout();
            }
        }

        public int NumColumns
        {
            get
            {
                return this.numColumns;
            }
            set
            {
                this.numColumns = value;
                RequestLayout();
            }
        }

        public int ColumnWidth
        {
            get
            {
                return this.columnWidth;
            }
            set
            {
                this.columnWidth = value;
                this.d = false;
            }
        }

        public int HorizontalSpacing
        {
            get
            {
                return this.horizontalSpacing;
            }
            set
            {
                this.horizontalSpacing = value;
                RequestLayout();
            }
        }

        public int VerticalSpacing
        {
            get
            {
                return this.verticalSpacing;
            }
            set
            {
                this.verticalSpacing = value;
                RequestLayout();
            }
        }

        public override View SelectedView
        {
            get
            {
                return null;
            }
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);

            if (this.adapter != null)
            {
                int i1 = PaddingLeft;
                int k = PaddingRight;
                int m = PaddingTop;
                int n = 0;
                int i3 = this.columnWidth + this.horizontalSpacing;
                int i2 = 0;
                int j = i1;
                while (i2 < ChildCount)
                {
                    View localView = GetChildAt(i2);
                    int i = localView.MeasuredHeight;
                    if (this.numColumns != 0)
                    {
                        if (n >= this.numColumns)
                        {
                            m += i + this.verticalSpacing;
                            n = 0;
                            j = i1;
                        }
                    }
                    else if (k + (j + i3) > right)
                    {
                        m += i + this.verticalSpacing;
                        j = i1;
                    }
                    localView.Layout(j, m, j + this.columnWidth, i + m);
                    j += this.columnWidth + this.horizontalSpacing;
                    n++;
                    i2++;
                }
                Invalidate();
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            int k = View.MeasureSpec.GetSize(widthMeasureSpec) - this.horizontalSpacing - PaddingLeft - PaddingRight;
            int i = this.columnWidth + this.horizontalSpacing;
            View localView = GetChildAt(0);
            int j;
            if (localView != null)
            {
                j = localView.MeasuredHeight;
            }
            else
            {
                j = 16;
            }
            int m = ChildCount;
            if (this.numColumns > 0)
            {
                k = Math.Max(this.numColumns, 1);
            }
            else
            {
                k = Math.Max(k / i, 1);
            }
            m = 1 + m / k;
            this.measuredWidth = (k * i + this.horizontalSpacing);
            this.measuredHeight = (m * (j + this.verticalSpacing) + this.verticalSpacing);
            SetMeasuredDimension(this.measuredWidth, this.measuredHeight);
        }

        public override void SetSelection(int position)
        {
            throw new NotImplementedException();
        }       

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class CustomLibraryGridViewListener : Java.Lang.Object, View.IOnClickListener
    {
        LibraryGridView paramLibraryGridView;
        int paramInt;

        public CustomLibraryGridViewListener(LibraryGridView paramLibraryGridView, int paramInt)
        {
            this.paramLibraryGridView = paramLibraryGridView;
            this.paramInt = paramInt;
        }

        public void OnClick(View paramView)
        {
            //paramLibraryGridView.PerformItemClick(paramView, paramInt, paramLibraryGridView.GetItemIdAtPosition(paramInt));

            string bookname = (string)paramLibraryGridView.GetItemAtPosition(paramInt);
            BibleBook book = ThisApp.allBibleBooks.Single(x => x.Title.ToLower().Equals(bookname.ToLower()));

            ThisApp.selectedBook = book;

            var intent = new Intent(ThisApp.activity, typeof(ChaptersActivity));
            ThisApp.activity.StartActivity(intent);
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