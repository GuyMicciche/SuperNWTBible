using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;
using System.Collections.Generic;

namespace NWTBible
{
    [Activity(Label = "New World Translation Bible — Settings", Icon = "@drawable/icon")]
    public class PreferencesActivity : PreferenceActivity, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.AddPreferencesFromResource(Resource.Layout.PreferencesLayout);            
        }

        protected override void OnResume()
        {
            base.OnResume();

            PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
        }

        protected override void OnPause()
        {
            base.OnPause();

            PreferenceScreen.SharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
        }

        public void OnSharedPreferenceChanged(ISharedPreferences prefs, string key)
        {
            if (key == "listThemeStyle")
            {
                ListPreference lp = (ListPreference)FindPreference(key);
                String lpVal = lp.Value;
                if(lpVal.Contains("Dark"))
                {
                    prefs.Edit().PutInt("ThemeStyle", Android.Resource.Style.ThemeHolo).Commit();
                    ThisApp.StyleTheme = Android.Resource.Style.ThemeHolo;
                }
                else if(lpVal.Contains("Light"))
                {
                    prefs.Edit().PutInt("ThemeStyle", Android.Resource.Style.ThemeHoloLight).Commit();
                    ThisApp.StyleTheme = Android.Resource.Style.ThemeHoloLight;
                }                
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
}