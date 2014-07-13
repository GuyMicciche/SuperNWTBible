using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Webkit;
using Android.Widget;

using NWTBible.NotesMenu;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;

using Fragment = Android.Support.V4.App.Fragment;
using IMenu = Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = Xamarin.ActionbarSherlockBinding.Views.IMenuItem;
using ActionMode = Xamarin.ActionbarSherlockBinding.Views.ActionMode;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace NWTBible.ReaderMenu
{
    public class ReaderFragment : SherlockFragment
    {
        View view;

        private DailyTextDay dailyText = new DailyTextDay();

        private int numOfChapters = 0;

        private ActionMode currentActionMode;
        private ActionMode.ICallback contentSelectionActionModeCallback;

        public int mTouchX;
        public int mTouchY;

        public WebView webview;
        public WebView webview2;
        private string html;

        public string Title;

        public NWTBibleWebView client;

        public ReaderFragment()
        {
            contentSelectionActionModeCallback = new CreateActionModeImpl(delegate(ActionMode actionMode, IMenu menu)
            {
                actionMode.Title = "Highlighting";

                SherlockActivity.SupportMenuInflater.Inflate(Resource.Menu.highlighting_menu, menu);
                return true;
            }, delegate(ActionMode actionMode, IMenu menu)
            {
                return false;
            }, delegate(ActionMode actionMode, IMenuItem menuItem)
            {
                switch (menuItem.ItemId)
                {
                    case Resource.Id.highlight:
                        actionMode.Finish();
                        return true;
                }
                return false;
            }, delegate(ActionMode actionMode)
            {
                view.Selected = false;
                currentActionMode = null;
            });
        }     

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetHasOptionsMenu(true);
            SetMenuVisibility(true);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Default view is Bible reader
            view = inflater.Inflate(Resource.Layout.Reader, container, false);

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                if (ThisApp.preferences.GetBoolean("dualWebviews", false))
                {
                    view = inflater.Inflate(Resource.Layout.DuelReader, container, false);
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.Reader, container, false);
                }
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                if (ThisApp.preferences.GetBoolean("dualWebviews", false))
                {
                    view = inflater.Inflate(Resource.Layout.DuelReader, container, false);
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.Reader, container, false);
                }
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                // If have daily text xml files
                //view = inflater.Inflate(Resource.Layout.DailyText, container, false);

                // If loading from WOL
                if (ThisApp.preferences.GetBoolean("dualWebviews", false))
                {
                    view = inflater.Inflate(Resource.Layout.DuelReader, container, false);
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.Reader, container, false);
                }
            }
            else if (ThisApp.ReaderKind == ReaderKind.WOLReader)
            {
                if (ThisApp.preferences.GetBoolean("dualWebviews", false))
                {
                    view = inflater.Inflate(Resource.Layout.DuelReader, container, false);
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.Reader, container, false);
                }
            }
            else if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {
                //LoadEPUBReader(0);
            }

            return view;
        }

        public override void OnActivityCreated(Bundle bundle)
        {
            base.OnActivityCreated(bundle);

            if (ThisApp.ReaderKind == ReaderKind.BibleReader)
            {
                LoadBibleReader();
            }
            else if (ThisApp.ReaderKind == ReaderKind.PublicationReader)
            {
                int page = (ThisApp.selectedPublication.CurrentPage > 0) ? ThisApp.selectedPublication.CurrentPage : 1;
                LoadPublicationReader(ThisApp.selectedPublication.CurrentPage);
            }
            else if (ThisApp.ReaderKind == ReaderKind.DailyTextReader)
            {
                LoadDailyTextReader(0);
            }
            else if (ThisApp.ReaderKind == ReaderKind.WOLReader)
            {
                LoadWOLArticle();
            }
            else if (ThisApp.ReaderKind == ReaderKind.EPUBReader)
            {
                //LoadEPUBReader(0);
            }
        }

        public void ShowHighlightingMenu()
        {
            if (currentActionMode != null)
            {
                return;
            }

            currentActionMode = SherlockActivity.StartActionMode(contentSelectionActionModeCallback);
        }

        private void LoadBibleReader()
        {
            // Load xml document of the book
            XDocument doc = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedBook.Name.ToUpper());

            int chapterNum = int.Parse(ThisApp.selectedChapter.ChapterNumber);

            List<string> collection = doc.Descendants("c").Select((element) => element.Value).ToList();
            ThisApp.allBookChapters = collection;

            numOfChapters = collection.Count();

            // Get the CDATA of the selected chapter
            html = "<html><body><h1>" + chapterNum + "</h1></body></html>";
            html += collection.ElementAt(chapterNum - 1);

            // References
            if (ThisApp.preferences.GetBoolean("bibleReferences", true))
            {
                html = html.Replace("display: none", "display: inline");
            }

            // Add highlight nodes          
            html = Regex.Replace(html, @"(\d+.)(</a></span>|</span>)(.*?)((?=<span)|(?=\d+.</a></span>)|(?=</p>))", delegate(Match match)
            {
                string m = "";

                if (match.Groups[2].Value == "</a></span>")
                {
                    m = match.Groups[1].Value + "</a>" + match.Groups[3].Value + "</span>";
                }
                else if (match.Groups[2].Value == "</span>")
                {
                    m = match.Groups[1].Value + match.Groups[3].Value + "</span>";
                }

                return m;
            });
            html = Regex.Replace(html, @"dcv_\d+_", "");

            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview2 = View.FindViewById<WebView>(Resource.Id.readerWebView2);

            client = new NWTBibleWebView(Activity, ThisApp.Language, ThisApp.doHighlight, null, ThisApp.selectedChapter.ChapterNumber);
            webview.SetWebViewClient(client);
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
            webview.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);

                string html2 = "<html><body><h1>" + chapterNum + "</h1></body></html>";

                XDocument doc2 = ThisApp.GetNWTFile(lang, ThisApp.selectedBook.Name.ToUpper());
                html2 += doc2.Descendants("c").ElementAt(chapterNum - 1);

                // References
                if (ThisApp.preferences.GetBoolean("bibleReferences", true))
                {
                    html2 = html2.Replace("display: none", "display: inline");
                }

                // Add highlight nodes          
                html2 = Regex.Replace(html2, @"(\d+.)(</a></span>|</span>)(.*?)((?=<span)|(?=\d+.</a></span>)|(?=</p>))", delegate(Match match)
                {
                    string m = "";

                    if (match.Groups[2].Value == "</a></span>")
                    {
                        m = match.Groups[1].Value + "</a>" + match.Groups[3].Value + "</span>";
                    }
                    else if (match.Groups[2].Value == "</span>")
                    {
                        m = match.Groups[1].Value + match.Groups[3].Value + "</span>";
                    }

                    return m;
                });
                html2 = Regex.Replace(html2, @"dcv_\d+_", "");

                webview2.SetWebViewClient(new NWTBibleWebView(Activity, lang, ThisApp.doHighlight, null, ThisApp.selectedChapter.ChapterNumber));
                webview2.Settings.JavaScriptEnabled = true;
                webview2.Settings.PluginsEnabled = true;
                webview2.Settings.BuiltInZoomControls = true;
                webview2.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                webview2.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
                webview2.LoadDataWithBaseURL("file:///android_asset/", html2, "text/html", "utf-8", null);
            }
        }

        public void ReloadBiblePage(int offset)
        {
            if (offset > numOfChapters)
            {
                offset = 1;
            }
            else if (offset < 1)
            {
                offset = numOfChapters;
            }

            ThisApp.selectedChapter = new BibleChapter()
            {
                Book = ThisApp.selectedBook,
                ChapterNumber = offset.ToString()
            };

            html = "<html><body><h1>" + offset.ToString() + "</h1></body></html>";
            html += ThisApp.allBookChapters.ElementAt(offset - 1);

            // References
            if (ThisApp.preferences.GetBoolean("bibleReferences", true))
            {
                html = html.Replace("display: none", "display: inline");
            }

            // Add highlight nodes          
            html = Regex.Replace(html, @"(\d+.)(</a></span>|</span>)(.*?)((?=<span)|(?=\d+.</a></span>)|(?=</p>))", delegate(Match match)
            {
                string m = "";

                if (match.Groups[2].Value == "</a></span>")
                {
                    m = match.Groups[1].Value + "</a>" + match.Groups[3].Value + "</span>";
                }
                else if (match.Groups[2].Value == "</span>")
                {
                    m = match.Groups[1].Value + match.Groups[3].Value + "</span>";
                }

                return m;
            });
            html = Regex.Replace(html, @"dcv_\d+_", "");

            client = new NWTBibleWebView(Activity, ThisApp.Language, ThisApp.doHighlight, null, ThisApp.selectedChapter.ChapterNumber);
            webview.SetWebViewClient(client);
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);
                XDocument doc = ThisApp.GetNWTFile(lang, ThisApp.selectedBook.Name.ToUpper());

                string html2 = "<html><body><h1>" + offset.ToString() + "</h1></body></html>";
                html2 += doc.Descendants("c").ElementAt(offset - 1);

                // References
                if (ThisApp.preferences.GetBoolean("bibleReferences", true))
                {
                    html2 = html2.Replace("display: none", "display: inline");
                }

                // Add highlight nodes          
                html2 = Regex.Replace(html2, @"(\d+.)(</a></span>|</span>)(.*?)((?=<span)|(?=\d+.</a></span>)|(?=</p>))", delegate(Match match)
                {
                    string m = "";

                    if (match.Groups[2].Value == "</a></span>")
                    {
                        m = match.Groups[1].Value + "</a>" + match.Groups[3].Value + "</span>";
                    }
                    else if (match.Groups[2].Value == "</span>")
                    {
                        m = match.Groups[1].Value + match.Groups[3].Value + "</span>";
                    }

                    return m;
                });
                html2 = Regex.Replace(html2, @"dcv_\d+_", "");

                webview2.SetWebViewClient(new NWTBibleWebView(Activity, lang, ThisApp.doHighlight, null, ThisApp.selectedChapter.ChapterNumber));
                webview2.LoadDataWithBaseURL("file:///android_asset/", html2, "text/html", "utf-8", null);
            }

            // No selected verses
            ThisApp.selectedVerses = new List<BibleVerse>();
        }

        public void ReHighlight()
        {
            RunJs(webview, "ClearSelection();");

            // Highlight user highlights
            var chapterVerses = ThisApp.highlightedScriptures;
            List<string> verseNumbersList = new List<string>();
            for (int cv = 0; cv < chapterVerses.Count; cv++)
            {
                if (chapterVerses.ElementAt(cv).Chapter.ChapterNumber.Equals(ThisApp.selectedChapter.ChapterNumber))
                {
                    verseNumbersList.Add(chapterVerses.ElementAt(cv).VerseNumber);
                }
            }

            foreach (string vs in verseNumbersList)
            {
                RunJs(webview, "HighlightVerse('" + EscapeJsString(vs) + "', false);");
            }
        }

        public void ReScrollToHighlight()
        {
            RunJs(webview, "ScrollToHighlight('" + EscapeJsString("2") + "');");
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

        public void LoadPublicationReader(int chapter = 1)
        {
            ThisApp.allPublicationArticles = ThisApp.GetAllPublicationArticles();

            ThisApp.selectedPublicationArticle = ThisApp.allPublicationArticles.ElementAt(chapter - 1);

            numOfChapters = ThisApp.allPublicationArticles.Count();

            // Get the CDATA of the selected chapter
            html = "<html><body><h1>" + ThisApp.selectedPublicationArticle.ArticleTitle + "</h1></body></html>";
            html += ThisApp.allPublicationArticles.ElementAt(chapter - 1).Content.Replace("PageOnLoad", "PubOnLoad");

            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview2 = View.FindViewById<WebView>(Resource.Id.readerWebView2);

            client = new NWTBibleWebView(Activity, ThisApp.Language, ThisApp.doHighlight, null, ThisApp.selectedPublicationArticle.ArticleTitle);
            webview.SetWebViewClient(client);
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
            webview.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);

                XDocument doc2;
                if (ThisApp.GetAllPublications(lang).Any(p => p.Image == ThisApp.selectedPublication.Image))
                {
                    doc2 = ThisApp.GetNWTFile(lang, ThisApp.selectedPublication.Image);
                }
                else
                {
                    doc2 = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedPublication.Image);
                    ThisApp.AlertBox(Activity, "REMINDER", "This document does not exist in " + lang + ".");
                }

                List<PublicationArticle> collection2 = doc2.Descendants("article")
                .Select(article => new PublicationArticle
                {
                    ArticleTitle = article.Element("title").Value,
                    Content = article.Element("content").Value,
                    DocumentLocation = article.Element("documentLocation").Value,
                    PublicationTitle = ThisApp.selectedPublication.Image
                }).ToList();

                string html2 = "<html><body><h1>" + collection2.ElementAt(chapter - 1).ArticleTitle + "</h1></body></html>";
                html2 += collection2.ElementAt(chapter - 1).Content.Replace("PageOnLoad", "PubOnLoad");

                webview2.SetWebViewClient(new NWTBibleWebView(Activity, lang, ThisApp.doHighlight, null, ThisApp.selectedPublicationArticle.ArticleTitle));
                webview2.Settings.JavaScriptEnabled = true;
                webview2.Settings.PluginsEnabled = true;
                webview2.Settings.BuiltInZoomControls = true;
                webview2.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                webview2.AddJavascriptInterface(new VerseSelection(Activity, webview, webview2), "VerseSelection");
                webview2.LoadDataWithBaseURL("file:///android_asset/", html2, "text/html", "utf-8", null);
            }

            // Highlighting a publication paragraph.
            //webview.SetOnLongClickListener(new PublicationLongClickListener(this));            
        }

        public void LoadPublicationFromActionBar(int position)
        {
            if (position > numOfChapters)
            {
                position = 1;
            }
            else if (position < 1)
            {
                position = numOfChapters;
            }

            ThisApp.selectedPublicationArticle = ThisApp.allPublicationArticles.ElementAt(position - 1);

            html = "<html><body><h1>" + ThisApp.selectedPublicationArticle.ArticleTitle + "</h1></body></html>";
            html += ThisApp.selectedPublicationArticle.Content.Replace("PageOnLoad", "PubOnLoad"); ;

            client = new NWTBibleWebView(Activity, ThisApp.Language, ThisApp.doHighlight, null, ThisApp.selectedPublicationArticle.ArticleTitle);
            webview.SetWebViewClient(client);
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);

                XDocument doc2;
                if (ThisApp.GetAllPublications(lang).Any(p => p.Image == ThisApp.selectedPublication.Image))
                {
                    doc2 = ThisApp.GetNWTFile(lang, ThisApp.selectedPublication.Image);
                }
                else
                {
                    doc2 = ThisApp.GetNWTFile(ThisApp.Language, ThisApp.selectedPublication.Image);
                    ThisApp.AlertBox(Activity, "REMINDER", "This document does not exist in " + lang + ".");
                }

                List<PublicationArticle> collection2 = doc2.Descendants("article")
                .Select(article => new PublicationArticle
                {
                    ArticleTitle = article.Element("title").Value,
                    Content = article.Element("content").Value,
                    DocumentLocation = article.Element("documentLocation").Value,
                    PublicationTitle = ThisApp.selectedPublication.Image
                }).ToList();

                string html2 = "<html><body><h1>" + collection2.ElementAt(position - 1).ArticleTitle + "</h1></body></html>";
                html2 += collection2.ElementAt(position - 1).Content.Replace("PageOnLoad", "PubOnLoad"); ;

                webview2.SetWebViewClient(new NWTBibleWebView(Activity, lang, ThisApp.doHighlight, null, ThisApp.selectedPublicationArticle.ArticleTitle));
                webview2.LoadDataWithBaseURL("file:///android_asset/", html2, "text/html", "utf-8", null);
            }

            // No selected verses
            ThisApp.selectedVerses = new List<BibleVerse>();
        }

        public void LoadDailyTextReader(int offset)
        {
            // Either this
            //LoadWOLDailyText(offset);
            //return;

            // Or this
            var dtDate = View.FindViewById<TextView>(Resource.Id.dtDate);
            var dtScripture = View.FindViewById<TextView>(Resource.Id.dtScripture);
            var dtContent = View.FindViewById<TextView>(Resource.Id.dtContent);

            dailyText = ThisApp.GetDailyText(FormatDateTime(DateTime.Now.AddDays(offset)));

            ThisApp.selectedDailyText = dailyText;            

            if (ThisApp.Language == "Chinese Simplified Pinyin")
            {
                html = dailyText.Content;
            }
            else
            {
                html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                    "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" +
                    "<link href=\"ui/web/dt.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
                    "<body class=\"calibre\">\n" +
                    "<div class=\"body\">" + dailyText.Content + "</div>\n" +
                    "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
            }

            client = new NWTBibleWebView(Activity, ThisApp.Language);
            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview.SetWebViewClient(client);
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);

                dailyText = ThisApp.GetSecondLanguageDailyText(lang, FormatDateTime(DateTime.Now.AddDays(offset)), 0);

                if (lang == "Chinese Simplified Pinyin")
                {
                    html = dailyText.Content;
                }
                else
                {
                    html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                        "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" +
                        "<link href=\"ui/web/dt.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
                        "<body class=\"calibre\">\n" +
                        "<div class=\"body\">" + dailyText.Content + "</div>\n" +
                        "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
                }

                client = new NWTBibleWebView(Activity, lang);
                webview2 = View.FindViewById<WebView>(Resource.Id.readerWebView2);
                webview2.SetWebViewClient(client);
                webview2.Settings.JavaScriptEnabled = true;
                webview2.Settings.PluginsEnabled = true;
                webview2.Settings.BuiltInZoomControls = true;
                webview2.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                webview2.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
            }

            //dtDate.SetTypeface(ThisApp.Font(Activity, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            //dtDate.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(Activity));
            //dtScripture.SetTypeface(ThisApp.Font(Activity, "ionitalic"), Android.Graphics.TypefaceStyle.Normal);
            //dtScripture.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(Activity));
            //dtContent.SetTypeface(ThisApp.Font(Activity, "ionbook"), Android.Graphics.TypefaceStyle.Normal);
            //dtContent.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(Activity));

            //dtDate.Text = dailyText.Date;
            //dtScripture.Text = dailyText.Scripture;
            //dtContent.Text = dailyText.Content;
        }      

        public void LoadDailyTextReaderFromActionBar(int position)
        {
            // Either this
            //LoadWOLDailyText(position);
            //return;
            
            // Or this
            var dtDate = View.FindViewById<TextView>(Resource.Id.dtDate);
            var dtScripture = View.FindViewById<TextView>(Resource.Id.dtScripture);
            var dtContent = View.FindViewById<TextView>(Resource.Id.dtContent);

            dailyText = ThisApp.GetDailyTextByPosition(position);

            ThisApp.selectedDailyText = dailyText;

            if (ThisApp.Language == "Chinese Simplified Pinyin")
            {
                html = dailyText.Content;
            }
            else
            {
                html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                    "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" +
                    "<link href=\"ui/web/dt.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
                    "<body class=\"calibre\">\n" +
                    "<div class=\"body\">" + dailyText.Content + "</div>\n" +
                    "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                    "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
            }

            client = new NWTBibleWebView(Activity, ThisApp.Language);
            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview.SetWebViewClient(client);
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.PluginsEnabled = true;
            webview.Settings.BuiltInZoomControls = true;
            webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
            webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);

                dailyText = ThisApp.GetSecondLanguageDailyText(lang, FormatDateTime(DateTime.Now.AddDays(position)), position);

                if (lang == "Chinese Simplified Pinyin")
                {
                    html = dailyText.Content;
                }
                else
                {
                    html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                        "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" +
                        "<link href=\"ui/web/dt.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
                        "<body class=\"calibre\">\n" +
                        "<div class=\"body\">" + dailyText.Content + "</div>\n" +
                        "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                        "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
                }

                client = new NWTBibleWebView(Activity, lang);
                webview2 = View.FindViewById<WebView>(Resource.Id.readerWebView2);
                webview2.SetWebViewClient(client);
                webview2.Settings.JavaScriptEnabled = true;
                webview2.Settings.PluginsEnabled = true;
                webview2.Settings.BuiltInZoomControls = true;
                webview2.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                webview2.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
            }

            //dtDate.SetTypeface(ThisApp.Font(Activity, "ionbold"), Android.Graphics.TypefaceStyle.Normal);
            //dtDate.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(Activity));
            //dtScripture.SetTypeface(ThisApp.Font(Activity, "ionitalic"), Android.Graphics.TypefaceStyle.Normal);
            //dtScripture.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(Activity));
            //dtContent.SetTypeface(ThisApp.Font(Activity, "ionbook"), Android.Graphics.TypefaceStyle.Normal);
            //dtContent.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)ThisApp.UserFontSize(Activity));

            //dtDate.Text = dailyText.Date;
            //dtScripture.Text = dailyText.Scripture;
            //dtContent.Text = dailyText.Content;
        }

        private void LoadWOLArticle()
        {
            string url = "http://m.wol.jw.org/en/wol/d/r" + ThisApp.GetLanguageR(ThisApp.Language) + "/lp-" + ThisApp.GetLanguageCode(ThisApp.Language).ToLower() + "/" + ThisApp.WolArticle;

            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            webview2 = View.FindViewById<WebView>(Resource.Id.readerWebView2);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 8000;  //if it's an aspx give it a few to warm up

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    html = reader.ReadToEnd();

                    Console.WriteLine(html);

                    string[] beginning1 = Regex.Split(html, "<title>");
                    string removedBeginning1 = beginning1[beginning1.Length - 1];
                    html = removedBeginning1;
                    string[] end1 = Regex.Split(html, "</title>");
                    string pagetitle = end1[end1.Length - 2];
                    pagetitle = pagetitle.Replace("&mdash;", "—");

                    Title = pagetitle;
                    Activity.Title = pagetitle;

                    string[] beginning = Regex.Split(html, "<div id=\"main\">");
                    string removedBeginning = beginning[beginning.Length - 1];
                    html = "<div id=\"content\"  >" + removedBeginning;
                    string[] end = Regex.Split(html, "<div id=\"contentFooter\">");
                    string content = end[end.Length - 2];

                    html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                            "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" +
                            "<link href=\"ui/web/dt.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
                            "<body class=\"calibre\">\n" +
                            "<div class=\"body\">" + content + "</div>\n" +
                            "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
                    html = html.Replace("/en", "http://m.wol.jw.org/en");

                    client = new NWTBibleWebView(Activity, ThisApp.Language);
                    webview.SetWebViewClient(client);
                    webview.Settings.JavaScriptEnabled = true;
                    webview.Settings.PluginsEnabled = true;
                    webview.Settings.BuiltInZoomControls = true;
                    webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                    webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                Toast.MakeText(Activity, "Article does not exist, or no connection.", ToastLength.Long).Show();

                return;
            }

            if (ThisApp.preferences.GetBoolean("dualWebviews", false))
            {
                string lang = ThisApp.preferences.GetString("listSecondLanguage", ThisApp.Language);
                url = "http://m.wol.jw.org/en/wol/d/r" + ThisApp.GetLanguageR(lang) + "/lp-" + ThisApp.GetLanguageCode(lang).ToLower() + "/" + ThisApp.WolArticle;

                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 8000;  //if it's an aspx give it a few to warm up

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Stream stream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        html = reader.ReadToEnd();

                        Console.WriteLine(html);

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

                        html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" +
                                "<link href=\"ui/web/dt.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
                                "<body class=\"calibre\">\n" +
                                "<div class=\"body\">" + content + "</div>\n" +
                                "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                                "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
                        html = html.Replace("/en", "http://m.wol.jw.org/en");

                        client = new NWTBibleWebView(Activity, lang);
                        webview2.SetWebViewClient(client);
                        webview2.Settings.JavaScriptEnabled = true;
                        webview2.Settings.PluginsEnabled = true;
                        webview2.Settings.BuiltInZoomControls = true;
                        webview2.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                        webview2.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                    Toast.MakeText(Activity, "Article does not exist, or no connection.", ToastLength.Long).Show();

                    return;
                }
            }
        }

        private void LoadWOLDailyText(int offset)
        {
            string[] date = FormatWOLDateTime(DateTime.Now.AddDays(offset)).Split('-');
            string url = "http://wol.jw.org/en/wol/dt/r" + ThisApp.GetLanguageR(ThisApp.Language) + "/lp-" + ThisApp.GetLanguageCode(ThisApp.Language).ToLower() + "/" + date[0] + "/" + date[1] + "/" + date[2];

            webview = View.FindViewById<WebView>(Resource.Id.readerWebView);
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 8000;  //if it's an aspx give it a few to warm up

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    html = reader.ReadToEnd();

                    Console.WriteLine(html);

                    string[] beginning1 = Regex.Split(html, "<title>");
                    string removedBeginning1 = beginning1[beginning1.Length - 1];
                    html = removedBeginning1;
                    string[] end1 = Regex.Split(html, "</title>");
                    string pagetitle = end1[end1.Length - 2];
                    pagetitle = pagetitle.Replace("&mdash;", "—");

                    string[] beginning = Regex.Split(html, "<div class=\"results\">");
                    string removedBeginning = beginning[beginning.Length - 1];
                    html = "<div id=\"content\"  >" + removedBeginning;
                    string[] end = Regex.Split(html, "<div id=\"contentFooter\">");
                    string content = end[end.Length - 2];

                    html = "<html><meta name=\"viewport\" content=\"width=320\" />\n" +
                            "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n" +
                            "<link href=\"ui/web/dt.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
                            "<body class=\"calibre\">\n" +
                            "<div class=\"body\">" + content + "</div>\n" +
                            "<script src=\"http://m.wol.jw.org/js/jquery.min.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/underscore-min.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/wol.modernizr.min.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/startup.js\"></script>\n" +
                            "<script src=\"http://m.wol.jw.org/js/wol.mobile.min.js\"></script>\n" + "</body></html>";
                    html = html.Replace("/en", "http://m.wol.jw.org/en");

                    client = new NWTBibleWebView(Activity, ThisApp.Language);
                    webview.SetWebViewClient(client);
                    webview.Settings.JavaScriptEnabled = true;
                    webview.Settings.PluginsEnabled = true;
                    webview.Settings.BuiltInZoomControls = true;
                    webview.Settings.DefaultFontSize = ThisApp.UserFontSize(Activity);
                    webview.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "utf-8", null);   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                Toast.MakeText(Activity, "Please check connections.", ToastLength.Long).Show();

                return;
            }                     
        }

        public void InitializeNoteParadigm()
        {
            string[] verses = new string[ThisApp.selectedVerses.Count];

            if (ThisApp.selectedVerses.Count == 0)
            {
                return;
            }

            foreach (var i in ThisApp.selectedVerses)
            {
                Console.WriteLine(i.ScriptureFormatted);
            }

            for (int j = 0; j < verses.Length; j++)
            {
                verses[j] = ThisApp.selectedVerses[j].VerseNumber;
            }

            var intent = new Intent(Activity, typeof(NoteEditActivity));
            StartActivity(intent);
        }

        public string FormatDateTime(DateTime input)
        {
            return input.ToString(@"yyyy/M/d");
        }

        public string FormatWOLDateTime(DateTime input)
        {
            return input.ToString(@"yyyy-M-d");
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }   
    }

    public class PublicationTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        ReaderFragment view;

        public PublicationTouchListener(ReaderFragment view)
        {
            this.view = view;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            view.mTouchX = (int)e.GetX();
            view.mTouchY = (int)e.GetY();

            return false;
        }
    }

    public class PublicationLongClickListener : Java.Lang.Object, View.IOnLongClickListener
    {
        ReaderFragment view;

        public PublicationLongClickListener(ReaderFragment view)
        {
            this.view = view;
        }

        public bool OnLongClick(View v)
        {
            view.ShowHighlightingMenu();

            return true;
        }
    }

    public class CreateActionModeImpl : Java.Lang.Object, ActionMode.ICallback
    {
        Func<ActionMode, IMenu, bool> create;
        Func<ActionMode, IMenu, bool> prepare;
        Func<ActionMode, IMenuItem, bool> clicked;
        Action<ActionMode> destroy;

        public CreateActionModeImpl(Func<ActionMode, IMenu, bool> create,
            Func<ActionMode, IMenu, bool> prepare,
            Func<ActionMode, IMenuItem, bool> clicked,
            Action<ActionMode> destroy)
        {
            this.create = create;
            this.prepare = prepare;
            this.clicked = clicked;
            this.destroy = destroy;
        }

        public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
        {
            return clicked(mode, item);
        }

        public bool OnCreateActionMode(ActionMode mode, IMenu menu)
        {
            return create(mode, menu);
        }

        public void OnDestroyActionMode(ActionMode mode)
        {
            destroy(mode);
        }

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            return prepare(mode, menu);
        }
    }
}