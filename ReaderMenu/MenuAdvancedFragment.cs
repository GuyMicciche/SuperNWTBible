//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using Android.Support.V4.App;
//using Android.Support.V4;

//using Xamarin.ActionbarSherlockBinding.App;

//using Fragment = Android.Support.V4.App.Fragment;
//using Java.Lang;
//using Android.Graphics.Drawables;

//namespace NWTBible.ReaderMenu
//{
//    public class MenuAdvancedFragment : SherlockFragment
//    {
//        private TabHost tabHost;
//        private TabManager tabManager;

//        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//        {
//            View fragmentView = inflater.Inflate(Resource.Layout.FragmentTabHost, container);

//            tabHost = fragmentView.FindViewById<TabHost>(Android.Resource.Id.TabHost);

//            LocalActivityManager mLocalActivityManager = new LocalActivityManager(Activity, false);
//            mLocalActivityManager.DispatchCreate(savedInstanceState);
//            tabHost.Setup(mLocalActivityManager);

//            tabManager = new TabManager(Activity, tabHost, Resource.Id.realtabcontent);

//            tabManager.AddTab(tabHost.NewTabSpec("hebrew").SetIndicator("Hebrew"), (new MenuFragment()).Class, null);

//            return fragmentView;
//        }

//        public override void OnDestroyView()
//        {
//            base.OnDestroyView();
//            tabHost = null;
//        }
//    }
//    public class TabManager : Java.Lang.Object, TabHost.IOnTabChangeListener
//    {
//        private FragmentActivity _activity;
//        private TabHost _tabHost;
//        private int _containerId;
//        private Dictionary<string, TabInfo> _tabs = new Dictionary<string, TabInfo>();
//        TabInfo _lastTab;

//        public class TabInfo
//        {
//            public string tag;
//            public Class clss;
//            public Bundle args;
//            public Fragment fragment { get; set; }

//            public TabInfo(string _tag, Class _class, Bundle _args)
//            {
//                tag = _tag;
//                clss = _class;
//                args = _args;
//            }
//        }

//        public class DummyTabFactory : Java.Lang.Object, TabHost.ITabContentFactory
//        {
//            private Context _context;

//            public DummyTabFactory(Context context)
//            {
//                _context = context;
//            }

//            public View CreateTabContent(string tag)
//            {
//                var v = new View(_context);
//                v.SetMinimumHeight(0);
//                v.SetMinimumWidth(0);
//                return v;
//            }
//        }

//        public TabManager(FragmentActivity activity, TabHost tabHost, int containerId)
//        {
//            _activity = activity;
//            _tabHost = tabHost;
//            _containerId = containerId;
//            _tabHost.SetOnTabChangedListener(this);
//        }

//        public void AddTab(TabHost.TabSpec tabSpec, Class clss, Bundle args)
//        {
//            tabSpec.SetContent(new DummyTabFactory(_activity));
//            var tag = tabSpec.Tag;

//            var info = new TabInfo(tag, clss, args);

//            // Check to see if we already have a fragment for this tab, probably
//            // from a previously saved state.  If so, deactivate it, because our
//            // initial state is that a tab isn't shown.
//            info.fragment = _activity.SupportFragmentManager.FindFragmentByTag(tag);
//            if (info.fragment != null && !info.fragment.IsDetached)
//            {
//                var ft = _activity.SupportFragmentManager.BeginTransaction();
//                ft.Detach(info.fragment);
//                ft.Commit();
//            }

//            _tabs.Add(tag, info);
//            _tabHost.AddTab(tabSpec);
//        }

//        public void OnTabChanged(string tabId)
//        {
//            var newTab = _tabs[tabId];
//            if (_lastTab != newTab)
//            {
//                var ft = _activity.SupportFragmentManager.BeginTransaction();
//                if (_lastTab != null)
//                {
//                    if (_lastTab.fragment != null)
//                    {
//                        ft.Detach(_lastTab.fragment);
//                    }
//                }
//                if (newTab != null)
//                {
//                    if (newTab.fragment == null)
//                    {
//                        newTab.fragment = Fragment.Instantiate(_activity, newTab.clss.Name, newTab.args);
//                        ft.Add(_containerId, newTab.fragment, newTab.tag);
//                    }
//                    else
//                    {
//                        ft.Attach(newTab.fragment);
//                    }
//                }

//                _lastTab = newTab;
//                ft.CommitAllowingStateLoss();
//                _activity.SupportFragmentManager.ExecutePendingTransactions();
//            }
//        }
//    }
//}