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

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible", Icon = "@drawable/icon")]
    public class MainActivity : TabActivity
    {
        private bool reload = false;

        public WifiReceiver receiver;
        public IntentFilter filter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RegisterBroadcastReceiver();

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight));

            SetContentView(Resource.Layout.Main);
            
            TabHost.TabSpec spec;
            Intent hebrewIntent;
            Intent greekIntent;
            Intent pubsIntent;

            // Hebrew Tab

            // Create an Intent to launch an Activity for the tab (to be reused)
            hebrewIntent = new Intent(this, typeof(CanonActivity));
            hebrewIntent.AddFlags(ActivityFlags.NewTask);
            hebrewIntent.PutExtra("Canon", "Hebrew");

            // Initialize a TabSpec for each tab and add it to the TabHost
            spec = TabHost.NewTabSpec("hebrew");
            spec.SetIndicator("Hebrew", Resources.GetDrawable(Resource.Drawable.hebrew));
            spec.SetContent(hebrewIntent);  
            TabHost.AddTab(spec);

            // Greek Tab

            greekIntent = new Intent(this, typeof(CanonActivity));
            greekIntent.AddFlags(ActivityFlags.NewTask);
            greekIntent.PutExtra("Canon", "Greek");

            spec = TabHost.NewTabSpec("greek");
            spec.SetIndicator("Greek", Resources.GetDrawable(Resource.Drawable.greek));
            spec.SetContent(greekIntent);
            TabHost.AddTab(spec);

            // Magazines Tab

            pubsIntent = new Intent(this, typeof(MagazinesActivity));
            pubsIntent.AddFlags(ActivityFlags.NewTask);

            spec = TabHost.NewTabSpec("magazines");
            spec.SetIndicator("Magazines", Resources.GetDrawable(Resource.Drawable.greek));
            spec.SetContent(pubsIntent);
            //TabHost.AddTab(spec);

            // Publications Tab

            pubsIntent = new Intent(this, typeof(PublicationsActivity));
            pubsIntent.AddFlags(ActivityFlags.NewTask);

            spec = TabHost.NewTabSpec("publications");
            spec.SetIndicator("Publications", Resources.GetDrawable(Resource.Drawable.greek));
            spec.SetContent(pubsIntent);
            //TabHost.AddTab(spec);

            TabHost.CurrentTab = 0;
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

            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
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

        void LoadPreferences()
        {
            if (ThisApp.availableDownloadedLanguages == null && ConnectedToNetwork())
            {
                ThisApp.availableDownloadedLanguages = ThisApp.GetAvailableLanguages();
            }

            ThisApp.context = this;
            ThisApp.activity = this;

            var prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);

            ThisApp.preferences = prefs;

            // Handle first launch
            if ( prefs.Contains("FirstLaunch"))
            {
                
            }
            else
            {
                prefs.Edit().PutBoolean("FirstLaunch", true).Commit();

                var d = ThisApp.SuperDialog(this,
                    "Welcome.",
                    "Because of your donations this past year, I used my time to make a super-awesome version of the old version. With holy spirit, research, and consideration of all your emails, I put Jehovah into this one.\n\nMake sure you check out all the options in the Settings menu.\n\nWithout any further aniticipation, here we are!",
                    "Continue",
                    "Donate",
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
                                Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://dl.dropbox.com/u/826238/donate.html"));
                                StartActivity(browserIntent);
                            }
                        );
                    });
                d.Show();
            }

            // Handle language
            if (prefs.Contains("Language"))
            {
                ThisApp.Language = prefs.GetString("Language", "null");
            }
            else
            {
                if (ConnectedToNetwork())
                {
                    ShowDialog(1);
                }
                else
                {
                    ThisApp.AlertBox(this, "WARNING", "Please confirm your connection settings before you download a language pack.");
                }
            }
        }

        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Languaged changed to: " + e.Language);

                var prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
                prefs.Edit().PutString("Language", e.Language).Commit();
            });

            ThisApp.allBibleBooks = ThisApp.GetAllBooks(e.Language);
            ThisApp.allDailyTexts = ThisApp.GetAllDailyTexts(e.Language);
            ThisApp.selectedDailyText = ThisApp.GetDailyText((DateTime.Now).ToString(@"yyyy-MM-dd"));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            int group = 0;

            IMenuItem item1 = menu.Add(group, 1, 1, "My Notes");
            IMenuItem item2 = menu.Add(group, 2, 2, "Daily Text");
            IMenuItem item4 = menu.Add(group, 3, 3, "Translate");
            IMenuItem item5 = menu.Add(group, 4, 4, "Language Packs");
            IMenuItem item6 = menu.Add(group, 5, 5, "Settings");

            // Submenu
            //ISubMenu sub = menu.AddSubMenu("Main");
            //sub.Add(0, 0, 0, "Light (Dark Action Bar)");
            //sub.Item.SetShowAsAction(ShowAsAction.Always | ShowAsAction.WithText);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case (1):
                    var intent3 = new Intent(this, typeof(NotesActivity));
                    StartActivity(intent3);
                    return true;
                case (2):
                    if (ThisApp.preferences.Contains("Language"))
                    {
                        ThisApp.ReaderKind = ReaderKind.DailyTextReader;
                        var intent = new Intent(this, typeof(ReaderSliderActivity));
                        StartActivity(intent);                    
                    }
                    else
                    {
                        ThisApp.AlertBox(this, "WARNING", "Please download a language pack.");
                    }
                    return true;
                case (3):
                    ShowDialog(2);                    
                    return true;
                case (4):
                    //menu id 4 was selected
                    //ThisApp.ReaderKind = ReaderKind.EPUBReader;
                    //var intent2 = new Intent(this, typeof(ReaderActivity));
                    //StartActivity(intent2);

                    if (ConnectedToNetwork())
                    {
                        ShowDialog(1);
                    }
                    else
                    {
                        ThisApp.AlertBox(this, "WARNING", "Please confirm your connection settings before you download a language pack.");
                    }
                    return true;
                case (5):
                    //menu id 5 was selected
                    var intent2 = new Intent(this, typeof(PreferencesActivity));
                    StartActivity(intent2);
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

