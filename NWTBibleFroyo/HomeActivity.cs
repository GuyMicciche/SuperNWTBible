//using System;

//using Android.App;
//using Android.Content;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.OS;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using System.Linq;
//using System.Collections.Generic;
//using Android.Preferences;
//using System.Collections;

//namespace NWTBible
//{
//    [Activity(Label = "New World Translation Bible", Icon = "@drawable/icon")]
//    public class HomeActivity : Activity
//    {
//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);            

//            // Set our view from the "main" layout resource
//            SetContentView(Resource.Layout.Home);

//            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

//            AddTab("Hebrew", Resource.Drawable.greek);
//            AddTab("Greek", Resource.Drawable.hebrew);
//            AddTab("Search", Resource.Drawable.search);

//            ThisApp.LanguageChanged += ThisApp_LanguageChanged;

//            LoadPreferences();
//        }

//        private void LoadPreferences()
//        {
//            var prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
//            if (prefs.Contains("Language"))
//            {
//                ThisApp.Language = prefs.GetString("Language", "null");
//            }
//            else
//            {
//                ShowDialog(1);
//            }
//        }

//        void ThisApp_LanguageChanged(object sender, App.LanguageChangedArgs e)
//        {
//            Task.Factory.StartNew(() => 
//            {
//                Console.WriteLine("Languaged changed to: " + e.Language);

//                var prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
//                prefs.Edit().PutString("Language", e.Language).Commit();
//            });
//        }

//        public override bool OnCreateOptionsMenu(IMenu menu)
//        {
//            int group = 0;

//            IMenuItem item1 = menu.Add(group, 1, 1, "Translate");
//            IMenuItem item2 = menu.Add(group, 2, 2, "Daily Text");
//            IMenuItem item3 = menu.Add(group, 3, 3, "Publications");
//            IMenuItem item4 = menu.Add(group, 4, 4, "Language Packs");
//            IMenuItem item5 = menu.Add(group, 5, 5, "Settings");
//            IMenuItem item6 = menu.Add(group, 6, 6, "About");

//            // Submenu
//            //ISubMenu sub = menu.AddSubMenu("Main");
//            //sub.Add(0, 0, 0, "Light (Dark Action Bar)");
//            //sub.Item.SetShowAsAction(ShowAsAction.Always | ShowAsAction.WithText);

//            return base.OnCreateOptionsMenu(menu);
//        }

//        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
//        {
//            switch (item.ItemId)
//            {
//                case (1):
//                    ShowDialog(2);
//                    return true;
//                case (2):
//                    //menu id 2 was selected
//                    return true;
//                case (3):
//                    //menu id 3 was selected
//                    return true;
//                case (4):
//                    ShowDialog(1); 
//                    return true;
//                case (5):
//                    //menu id 5 was selected
//                    return true;
//                case (6):
//                    //menu id 6 was selected
//                    return true;
//            }

//            return base.OnMenuItemSelected(featureId, item);
//        }

//        protected override void OnPrepareDialog(int id, Dialog dialog)
//        {
//            base.OnPrepareDialog(id, dialog);

//            switch (id)
//            {
//                case (1):
//                    RemoveDialog(id);
//                    dialog = ThisApp.DownloadLanguagePackDialog(this);
//                    break;
//                case (2):
//                    RemoveDialog(id);
//                    dialog = ThisApp.TranslateLanguageDialog(this);
//                    break;
//            }
//        }

//        protected override Dialog OnCreateDialog(int id)
//        {
//            switch (id)
//            {
//                case (1):
//                    return ThisApp.DownloadLanguagePackDialog(this);
//                case (2):
//                    return ThisApp.TranslateLanguageDialog(this);
//            }

//            return base.OnCreateDialog(id);
//        }

//        void AddTab(string tabTitle, int iconResourceId)
//        {
//            var tab = ActionBar.NewTab();
//            tab.SetText(tabTitle);
//            tab.SetIcon(iconResourceId);

//            bool tabSelected = false;

//            Fragment fragment = new CanonFragment(tabTitle);
//            FragmentTransaction transaction = FragmentManager.BeginTransaction();
//            transaction.Add(fragment, tabTitle).Commit();

//            // Must set event handler before adding tab
//            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
//            {
//                transaction = e.FragmentTransaction;
//                transaction.Replace(Resource.Id.fragmentContainer, new CanonFragment(tabTitle));
//                transaction.SetTransition(FragmentTransit.FragmentFade);

//                tabSelected = true;
                
//            };
//            tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e)
//            {
//                tabSelected = false;
//            };
//            tab.TabReselected += delegate(object sender, ActionBar.TabEventArgs e)
//            {
//                transaction = e.FragmentTransaction;
//                transaction.Replace(Resource.Id.fragmentContainer, new CanonFragment(tabTitle));
//                transaction.SetTransition(FragmentTransit.FragmentFade);

//                tabSelected = true;
//            };

//            ThisApp.LanguageChanged += (sender, e) =>
//            {
//                if(tabSelected)
//                {
//                    ActionBar.SetSelectedNavigationItem(ActionBar.SelectedNavigationIndex);
//                }
//            };

//            ActionBar.AddTab(tab);
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


