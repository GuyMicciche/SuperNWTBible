using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NWTBible.ReaderMenu;
using NWTBible.NotesMenu;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Java.Net;
using Android.Net;

using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;

using IMenu = global::Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = global::Xamarin.ActionbarSherlockBinding.Views.IMenuItem;

using ActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Android.Content.PM;
using Newtonsoft.Json;

using Xamarin.Parse;
using Android.Text;
using Android.Text.Style;
using Android.Graphics;

namespace NWTBible
{
    [Activity(Label = "JW", Icon = "@drawable/icon", Theme = "@style/Theme.Sherlock", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden)]
    public class MainActivity : SherlockFragmentActivity, ActionBar.ITabListener
    {
        private bool reload = false;

        public WifiReceiver receiver;
        public IntentFilter filter;

        public Fragment fragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);            

            RegisterBroadcastReceiver();

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            //SetContentView(Resource.Layout.MainFragmentsContainer);

            SupportActionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;
            SupportActionBar.SetDisplayShowTitleEnabled(true);            
           
            ThisApp.LanguageChanged += ThisApp_LanguageChanged;
            ThisApp.ThemeChanged += (sender, e) =>
            {
                Console.WriteLine("Theme changed to: " + e.StyleTheme.ToString());

                reload = true;

                RunOnUiThread(() =>
                {
                    //Recreate();
                });
            };
            
            LoadPreferences();

            //ActionBar.Tab hebrewTab = SupportActionBar.NewTab();
            //hebrewTab.SetText("Hebrew");
            //hebrewTab.SetTag("hebrew");
            //hebrewTab.SetTabListener(this);
            //hebrewTab.SetIcon(Resource.Drawable.hebrew);
            //SupportActionBar.AddTab(hebrewTab);

            //ActionBar.Tab greekTab = SupportActionBar.NewTab();
            //greekTab.SetText("Greek");
            //greekTab.SetTag("greek");
            //greekTab.SetTabListener(this);
            //greekTab.SetIcon(Resource.Drawable.greek);
            //SupportActionBar.AddTab(greekTab);

            ActionBar.Tab bibleTab = SupportActionBar.NewTab();
            bibleTab.SetText("Bible");
            bibleTab.SetTag("bible");
            bibleTab.SetTabListener(this);
            bibleTab.SetIcon(Resource.Drawable.bible);
            SupportActionBar.AddTab(bibleTab);

            ActionBar.Tab pubsTab = SupportActionBar.NewTab();
            pubsTab.SetText("Publications");
            pubsTab.SetTag("publications");
            pubsTab.SetTabListener(this);
            pubsTab.SetIcon(Resource.Drawable.pubs);
            SupportActionBar.AddTab(pubsTab);

            ActionBar.Tab searchTab = SupportActionBar.NewTab();
            searchTab.SetText("Search");
            searchTab.SetTag("search");
            searchTab.SetTabListener(this);
            searchTab.SetIcon(Android.Resource.Drawable.IcMenuSearch);
            SupportActionBar.AddTab(searchTab);    

            if (bundle != null)
            {
                // Select the tab that was selected before orientation change
                int index = bundle.GetInt("tabIndex");
                SupportActionBar.SetSelectedNavigationItem(index);
            }

            // Initialize Parse push notifications
            Parse.Initialize(this, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");
            PushService.SetDefaultPushCallback(this, this.Class);
            ParseInstallation.CurrentInstallation.SaveInBackground();
            ParseAnalytics.TrackAppOpened(this.Intent);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutInt("tabIndex", SupportActionBar.SelectedTab.Position);   
        }
        
        public void OnTabSelected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            string tag = tab.Tag.ToString();

            Fragment f = SupportFragmentManager.FindFragmentByTag(tag);

            if (f == null)
            {
                if (tag == "goto")
                {
                    fragment = new GoToFragment();
                }
                else if (tag == "search")
                {
                    fragment = new SearchFragment(false); 
                }
                else if (tag == "publications")
                {
                    fragment = new PublicationsFragment();                    
                }
                else if (tag == "bible")
                {
                    fragment = new CanonHeaderGridFragment();
                }
                else
                {
                    //fragment = new CanonFragment(tag);
                }

                fragment.RetainInstance = true;

                ft.Add(Android.Resource.Id.Content, fragment, tag);
            }
            else
            {
                ft.Attach(f);
            }            
        }

        public void OnTabUnselected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            string tag = tab.Tag.ToString();

            Fragment f = SupportFragmentManager.FindFragmentByTag(tag);
            if (f != null)
            {
                ft.Detach(f);
                return;
            }
            else if (fragment != null)
            {
                ft.Detach(fragment);
            }
        }

        public void OnTabReselected(ActionBar.Tab tab, FragmentTransaction ft)
        {

        }

        protected override void OnPause()
        {
            base.OnPause();

            UnregisterReceiver(receiver);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (reload)
            {
                Finish();
                StartActivity(new Intent(this, typeof(MainActivity)));
                reload = false;
            }

            RegisterReceiver(receiver, filter);
        }

        private void RegisterBroadcastReceiver()
        {
            receiver = new WifiReceiver();

            filter = new IntentFilter();
            filter.AddAction("android.net.wifi.STATE_CHANGE");
            filter.AddAction("android.net.conn.CONNECTIVITY_CHANGE");            

            Intent i = new Intent(this, typeof(WifiIntentService));
            StartService(Intent);
        }

        private bool ConnectedToNetwork()
        {
            bool connected = false;

            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
            if (connectivityManager != null)
            {
                bool mobileNetwork = false;
                bool wifiNetwork = false;
                bool wimaxNetwork = false;

                bool mobileNetworkConnected = false;
                bool wifiNetworkConnected = false;
                bool wimaxNetworkConnected = false;

                NetworkInfo mobileInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
                NetworkInfo wifiInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
                NetworkInfo wimaxInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wimax);

                if (mobileInfo != null)
                {
                    mobileNetwork = mobileInfo.IsAvailable;
                    Console.WriteLine("Is mobile available?  " + mobileNetwork);
                }

                if (wifiInfo != null)
                {
                    wifiNetwork = wifiInfo.IsAvailable;
                    Console.WriteLine("Is WiFi available?  " + wifiNetwork);
                }

                if (wimaxInfo != null)
                {
                    wimaxNetwork = wimaxInfo.IsAvailable;
                    Console.WriteLine("Is WiMAX available?  " + wimaxNetwork);
                }

                if (wifiNetwork || mobileNetwork || wimaxNetwork)
                {
                    mobileNetworkConnected = (mobileInfo != null) ? mobileInfo.IsConnectedOrConnecting : false;
                    wifiNetworkConnected = (wifiInfo != null) ? wifiInfo.IsConnectedOrConnecting : false;
                    wimaxNetworkConnected = (wimaxInfo != null) ? wimaxInfo.IsConnectedOrConnecting : false;
                }

                connected = (mobileNetworkConnected || wifiNetworkConnected || wimaxNetworkConnected);

                Console.WriteLine("Is mobile connected?  " + mobileNetworkConnected);
                Console.WriteLine("Is WiFi connected?  " + wifiNetworkConnected);
                Console.WriteLine("Is WiMAX connected?  " + wimaxNetworkConnected);
            }

            Console.WriteLine("Is this device connected?  " + connected);

            return connected;
        }

        public void LoadPreferences()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            ThisApp.preferences = prefs;

            ThisApp.context = this;
            ThisApp.activity = this;

            if (ThisApp.AvailableDownloadedLanguages == null)
            {
                ThisApp.AvailableDownloadedLanguages = ThisApp.GetAvailableLanguages();
            }            

            // Handle first launch
            if (prefs.Contains("FirstLaunch"))
            {
                
            }
            else
            {
                prefs.Edit().PutBoolean("FirstLaunch", true).Commit();

                prefs.Edit().PutBoolean("bibleReferences", false).Commit();
                prefs.Edit().PutBoolean("allowHighlighting", true).Commit();
                prefs.Edit().PutBoolean("recordEveryHighlight", true).Commit();

                var d = ThisApp.SuperDialog(this,
                    "100 Years.",
                    "Jehovah's Kingdom reached it's 100th year in power. We are living in exciting times. Jesus is king and is drawing millions of people out of all nations to Jehovah. It is now more than ever important to remain close to Jehovah's organization. May this tool serve as a helper to anyone. Glory be to Jehovah forever.",
                    "Enter",
                    "Contribute",
                    null,
                    delegate
                    {
                        ThisApp.QueryBox(this,
                            "How would you like to donate to the NWT Bible project?",
                            "PayPal",
                            "Google",
                            delegate
                            {
                                Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=J3WJ7MMYPV8R8"));
                                StartActivity(browserIntent);
                            },
                            delegate
                            {
                                Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(App.STOREHOUSE + "donate.html"));
                                StartActivity(browserIntent);
                            }
                        );
                    });
                d.Show();
            }

            // Handle language
            if (prefs.Contains("Language"))
            {
                if (!string.IsNullOrEmpty(prefs.GetString("Language", "")))
                {
                    ThisApp.Language = prefs.GetString("Language", "");
                }
            }
            else
            {
                if (ConnectedToNetwork())
                {
                    ShowDialog(1);
                }
                else
                {
                    ThisApp.AlertBox(this, "REMINDER", "Please confirm your connection settings before you download a language pack.");
                }
            }

            // Handle highlights
            if (prefs.Contains("HighlightList"))
            {
                string json = prefs.GetString("HighlightList", "null");
                List<BibleVerse> highlightedScriptures = JsonConvert.DeserializeObject<List<BibleVerse>>(json);

                ThisApp.highlightedScriptures = highlightedScriptures;
            }
        }

        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);

            if (string.IsNullOrEmpty(ThisApp.Language))
            {
                prefs.Edit().PutString("Language", "").Commit();

                return;
            }

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Languaged changed to: " + e.Language);

                prefs.Edit().PutString("Language", e.Language).Commit();
            });

            ThisApp.allBibleBooks = ThisApp.GetAllBooks(e.Language);
            ThisApp.allPublications = ThisApp.GetAllPublications(e.Language);
            ThisApp.allDailyTexts = ThisApp.GetAllDailyTexts(e.Language);
            ThisApp.selectedDailyText = ThisApp.GetDailyText((DateTime.Now).ToString(@"yyyy/M/d"));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            int group = 0;

            IMenuItem item1 = menu.Add(group, MYNOTES_MENU, MYNOTES_MENU, "My Notes");
            item1.SetIcon(Android.Resource.Drawable.IcMenuEdit);

            IMenuItem item2 = menu.Add(group, GOTO_MENU, GOTO_MENU, "Look Up");
            item2.SetIcon(Android.Resource.Drawable.IcMenuUpload);
            item2.SetShowAsAction((int)ShowAsAction.IfRoom);

            IMenuItem item3 = menu.Add(group, DAILYTEXT_MENU, DAILYTEXT_MENU, "Daily Text");
            item3.SetIcon(Android.Resource.Drawable.IcMenuDay);
            item3.SetShowAsAction((int)ShowAsAction.IfRoom);

            IMenuItem item4 = menu.Add(group, TRANSLATE_MENU, TRANSLATE_MENU, "Translate");
            item4.SetIcon(Android.Resource.Drawable.IcMenuRotate);
            item4.SetShowAsAction((int)ShowAsAction.IfRoom );

            IMenuItem item5 = menu.Add(group, LANGUAGEPACKS_MENU, LANGUAGEPACKS_MENU, "Language Packs");
            item5.SetIcon(Android.Resource.Drawable.IcMenuMapmode);
            item5.SetShowAsAction((int)ShowAsAction.CollapseActionView | (int)ShowAsAction.WithText);

            IMenuItem item6 = menu.Add(group, SETTINGS_MENU, SETTINGS_MENU, "Settings");
            item6.SetIcon(Android.Resource.Drawable.IcMenuPreferences);
            item6.SetShowAsAction((int)ShowAsAction.CollapseActionView | (int)ShowAsAction.WithText);

            // Submenu
            //ISubMenu sub = menu.AddSubMenu("Main");
            //sub.Add(0, 0, 0, "Light (Dark Action Bar)");
            //sub.Item.SetShowAsAction(ShowAsAction.Always | ShowAsAction.WithText);

            return base.OnCreateOptionsMenu(menu);
        }

        private const int MYNOTES_MENU = 2;
        private const int GOTO_MENU = 3;
        private const int DAILYTEXT_MENU = 1;
        private const int TRANSLATE_MENU = 4;
        private const int LANGUAGEPACKS_MENU = 5;
        private const int SETTINGS_MENU = 6;

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case (MYNOTES_MENU):
                    var intent3 = new Intent(this, typeof(NotesActivity));
                    StartActivity(intent3);
                    return true;
                case (GOTO_MENU):
                    if (!string.IsNullOrEmpty(ThisApp.Language))
                    {
                        ShowDialog(3);
                    }
                    else
                    {
                        ThisApp.AlertBox(this, "REMINDER", "Please download a language pack.");
                    }
                    return true;
                case (DAILYTEXT_MENU):
                    if (!string.IsNullOrEmpty(ThisApp.Language))
                    {
                        ThisApp.ReaderKind = ReaderKind.DailyTextReader;
                        var intent = new Intent(this, typeof(ReaderActivity));
                        StartActivity(intent);                    
                    }
                    else
                    {
                        ThisApp.AlertBox(this, "REMINDER", "Please download a language pack.");
                    }
                    return true;
                case (TRANSLATE_MENU):
                    ShowDialog(2);                    
                    return true;
                case (LANGUAGEPACKS_MENU):
                    if (ConnectedToNetwork())
                    {
                        ShowDialog(1);
                    }
                    else
                    {
                        ThisApp.AlertBox(this, "REMINDER", "Please confirm your connection settings before you download a language pack.");
                    }
                    return true;
                case (SETTINGS_MENU):
                    //menu id 5 was selected
                    var intent4 = new Intent(this, typeof(PreferencesActivity));
                    StartActivity(intent4);
                    return true;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        // Instantiate dialogs to be used with this activity
        protected override void OnPrepareDialog(int id, Dialog dialog)
        {
            base.OnPrepareDialog(id, dialog);

            switch (id)
            {
                case (1):
                    RemoveDialog(id);
                    dialog = ThisApp.DownloadLanguagePackDialog(this);
                    break;
                case (2):
                    RemoveDialog(id);
                    dialog = ThisApp.TranslateLanguageDialog(this);
                    break;
                case (3):
                    RemoveDialog(id);
                    dialog = ThisApp.GoToArticleDialog(this);
                    break;
            }
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case (1):
                    return ThisApp.DownloadLanguagePackDialog(this);
                case (2):
                    return ThisApp.TranslateLanguageDialog(this);
                case (3):
                    return ThisApp.GoToArticleDialog(this);
            }

            return base.OnCreateDialog(id);
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class WifiReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // If not connected to a network
            if (!ConnectedToNetwork(context))
            {
                Console.WriteLine("SOMETHING HAPPENED!!!!");

                // If there is a running WebClient
                if (ThisApp.client != null)
                {
                    // Cancel the download
                    ThisApp.client.CancelAsync();

                    // If there are items to be downloaded
                    if (ThisApp.downloadQueue.Count > 0)
                    {
                        // For each item that was going to be downloaded
                        for (var i = 0; i < ThisApp.downloadQueue.Count; i++)
                        {
                            // Delete that item and remove any downloaded files
                            ThisApp.DeleteLanguagePack(ThisApp.downloadQueue[i]);
                        }
                    }

                    // No downloads to be downloaded
                    ThisApp.downloadQueue = new List<string>();
                    // No files to be download
                    ThisApp.FilesCoutner = 0;
                    // They didn't all download, but set to true anyways
                    ThisApp.allDownloaded = true;

                    if (ThisApp.DownloadedLanguages.Count > 0)
                    {
                        ThisApp.Language = ThisApp.Language;
                    }
                }
            }
        }

        private bool ConnectedToNetwork(Context context)
        {
            //Console.WriteLine("Connectivity Service is: " + ConnectivityService);
            //// Check if you are connected to a network
            //bool isAvailable = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            //ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            //NetworkInfo networkInfo = connectivityManager.GetNetworkInfo(isAvailable ? ConnectivityType.Wifi : ConnectivityType.Mobile);

            //// Check if you have a connection from that network
            //if (networkInfo.IsConnected || networkInfo.IsConnectedOrConnecting)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            bool connected = false;

            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            if (connectivityManager != null)
            {
                bool mobileNetwork = false;
                bool wifiNetwork = false;
                bool wimaxNetwork = false;

                bool mobileNetworkConnected = false;
                bool wifiNetworkConnected = false;
                bool wimaxNetworkConnected = false;

                NetworkInfo mobileInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
                NetworkInfo wifiInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
                NetworkInfo wimaxInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wimax);

                if (mobileInfo != null)
                {
                    mobileNetwork = mobileInfo.IsAvailable;
                    Console.WriteLine("Is mobile available?  " + mobileNetwork);
                }

                if (wifiInfo != null)
                {
                    wifiNetwork = wifiInfo.IsAvailable;
                    Console.WriteLine("Is WiFi available?  " + wifiNetwork);
                }

                if (wimaxInfo != null)
                {
                    wimaxNetwork = wimaxInfo.IsAvailable;
                    Console.WriteLine("Is WiMAX available?  " + wimaxNetwork);
                }

                if (wifiNetwork || mobileNetwork || wimaxNetwork)
                {
                    mobileNetworkConnected = (mobileInfo != null) ? mobileInfo.IsConnectedOrConnecting : false;
                    wifiNetworkConnected = (wifiInfo != null) ? wifiInfo.IsConnectedOrConnecting : false;
                    wimaxNetworkConnected = (wimaxInfo != null) ? wimaxInfo.IsConnectedOrConnecting : false;
                }

                connected = (mobileNetworkConnected || wifiNetworkConnected || wimaxNetworkConnected);

                Console.WriteLine("Is mobile connected?  " + mobileNetworkConnected);
                Console.WriteLine("Is WiFi connected?  " + wifiNetworkConnected);
                Console.WriteLine("Is WiMAX connected?  " + wimaxNetworkConnected);
            }

            Console.WriteLine("Is this device connected?  " + connected);

            return connected;
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class WifiIntentService : IntentService
    {
        public WifiIntentService()
            : base("WifiIntentService")
        {

        }

        protected override void OnHandleIntent(Intent intent)
        {
            Intent newIntent = new Intent();
            newIntent.SetAction("android.net.wifi.STATE_CHANGE");
            newIntent.SetAction("android.net.conn.CONNECTIVITY_CHANGE");
            SendBroadcast(newIntent);
        }
    }


}

