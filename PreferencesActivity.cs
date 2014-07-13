using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;
using System.Collections.Generic;
using Xamarin.ActionbarSherlockBinding.App;
using Android.Text;

namespace NWTBible
{
    [Activity(Label = "Settings", Icon = "@drawable/icon", Theme = "@style/Theme.Sherlock")]
    public class PreferencesActivity : SherlockPreferenceActivity, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        private ListPreference slp;
        private CheckBoxPreference cp;

        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            base.OnCreate(bundle);

            this.AddPreferencesFromResource(Resource.Layout.PreferencesLayout);

            cp = (CheckBoxPreference)FindPreference("dualWebviews");
            slp = (ListPreference)FindPreference("listSecondLanguage");

            CheckBoxPreference notesReplace = (CheckBoxPreference)FindPreference("notesReplace");
            CheckBoxPreference allowHighlighting = (CheckBoxPreference)FindPreference("allowHighlighting");
            CheckBoxPreference recordEveryHighlight = (CheckBoxPreference)FindPreference("recordEveryHighlight");

            //notesReplace.Enabled = false;
            //notesReplace.Selectable = false;
            //notesReplace.SetDefaultValue(false);
            //allowHighlighting.Enabled = false;
            //allowHighlighting.Selectable = false;
            //allowHighlighting.SetDefaultValue(false);
            //recordEveryHighlight.Enabled = false;
            //recordEveryHighlight.Selectable = false;
            //recordEveryHighlight.SetDefaultValue(false);
        }

        protected override void OnResume()
        {
            base.OnResume();

            PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);

            if (String.IsNullOrEmpty(ThisApp.Language))
            {
                cp.Enabled = false;
                cp.Selectable = false;

                slp.Enabled = false;
                slp.Selectable = false;
            }
            else
            {
                cp.Enabled = true;
                cp.Selectable = true;

                if (cp.Checked && cp.Enabled)
                {
                    slp.Enabled = true;
                    slp.Selectable = true;
                    slp.SetEntries(ThisApp.DownloadedLanguages.ToArray());
                    slp.SetEntryValues(ThisApp.DownloadedLanguages.ToArray());
                    if (string.IsNullOrEmpty(slp.Value))
                    {
                        slp.SetValueIndex(ThisApp.DownloadedLanguages.IndexOf(ThisApp.Language));
                    }
                }
                else
                {
                    slp.Enabled = false;
                    slp.Selectable = false;
                }
            }
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
                if (lpVal.Contains("Dark"))
                {
                    prefs.Edit().PutInt("ThemeStyle", Resource.Style.Theme_Sherlock).Commit();
                    ThisApp.StyleTheme = Resource.Style.Theme_Sherlock;
                }
                else if (lpVal.Contains("Light"))
                {
                    prefs.Edit().PutInt("ThemeStyle", Resource.Style.Theme_Sherlock_Light).Commit();
                    ThisApp.StyleTheme = Resource.Style.Theme_Sherlock_Light;
                }

                Finish();
                StartActivity(Intent);
            }
            else if (key == "listFontSize")
            {
                ThisApp.Language = ThisApp.Language;
            }
            else if (key == "dualWebviews")
            {
                CheckBoxPreference cbp = (CheckBoxPreference)FindPreference(key);
                if (cbp.Checked)
                {
                    slp.Enabled = true;
                    slp.Selectable = true;
                    slp.SetEntries(ThisApp.DownloadedLanguages.ToArray());
                    slp.SetEntryValues(ThisApp.DownloadedLanguages.ToArray());
                    if (string.IsNullOrEmpty(slp.Value))
                    {
                        slp.SetValueIndex(ThisApp.DownloadedLanguages.IndexOf(ThisApp.Language));
                    }
                }
                else
                {
                    slp.Enabled = false;
                    slp.Selectable = false;
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