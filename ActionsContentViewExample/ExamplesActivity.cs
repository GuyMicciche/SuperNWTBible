using ActionsContentViewExample.ActionFragment;
using ActionsContentViewExample.ActionsAdapters;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using UI.ActionsContentView;
//using ActionsContentViewLibrary;

namespace ActionsContentViewExample
{
    [Activity(Label = "Actions Content View Example", MainLauncher = true)]
    public class ExamplesActivity : FragmentActivity
    {

        private const string STATE_URI = "state:uri";
        private const string STATE_FRAGMENT_TAG = "state:fragment_tag";

        private SettingsChangedListener mSettingsChangedListener;

        private ActionsContentView viewActionsContentView;

        private Uri CurrentUri = AboutFragment.ABOUT_URI;
        private string CurrentContentFragmentTag = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            mSettingsChangedListener = new SettingsChangedListener(this);

            SetContentView(Resource.Layout.example);

            viewActionsContentView = FindViewById<ActionsContentView>(Resource.Id.actionsContentView);
            viewActionsContentView.SwipingType = ActionsContentView.SwipingEdge;

            ListView viewActionsList = FindViewById<ListView>(Resource.Id.actions);
            ActionsAdapter actionsAdapter = new ActionsAdapter(this);
            viewActionsList.Adapter = actionsAdapter;
            viewActionsList.OnItemClickListener = new OnItemClickListenerAnonymousInnerClassHelper(this, actionsAdapter);

            ImageButton mainButton = FindViewById<ImageButton>(Resource.Id.mainButton);
            mainButton.Click += mainButton_Click;

            Button sourceCodeButton = FindViewById<Button>(Resource.Id.sourceCodeButton);
            sourceCodeButton.Click += sourceCodeButton_Click;

            if (savedInstanceState != null)
            {
                CurrentUri = Uri.Parse(savedInstanceState.GetString(STATE_URI));
                CurrentContentFragmentTag = savedInstanceState.GetString(STATE_FRAGMENT_TAG);
            }

            UpdateContent(CurrentUri);
        }

        void mainButton_Click(object sender, System.EventArgs e)
        {
            if (viewActionsContentView.IsActionsShown)
            {
                viewActionsContentView.ShowContent();
            }
            else
            {
                viewActionsContentView.ShowActions();
            }
        }

        void sourceCodeButton_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(Intent.ActionView);
            i.SetData(Uri.Parse(GetString(Resource.String.sources_link)));

            StartActivity(i);
        }

        private class OnItemClickListenerAnonymousInnerClassHelper : Java.Lang.Object, AdapterView.IOnItemClickListener
        {
            private ExamplesActivity OuterInstance;

            private ActionsAdapter ActionsAdapter;

            public OnItemClickListenerAnonymousInnerClassHelper(ExamplesActivity outerInstance, ActionsAdapter actionsAdapter)
            {
                this.OuterInstance = outerInstance;
                this.ActionsAdapter = actionsAdapter;
            }

            public void OnItemClick(AdapterView adapter, View v, int position, long flags)
            {
                Uri uri = (Uri)ActionsAdapter.GetItem(position);

                if (EffectsExampleActivity.URI.Equals(uri))
                {
                    OuterInstance.StartActivity(new Intent(OuterInstance.BaseContext, typeof(EffectsExampleActivity)));

                    return;
                }

                OuterInstance.UpdateContent(uri);
                OuterInstance.viewActionsContentView.ShowContent();
            }
        }

        public override void OnBackPressed()
        {
            Fragment currentFragment = SupportFragmentManager.FindFragmentByTag(CurrentContentFragmentTag);
            if (currentFragment is WebViewFragment)
            {
                WebViewFragment webFragment = (WebViewFragment)currentFragment;
                if (webFragment.OnBackPressed())
                {
                    return;
                }
            }

            base.OnBackPressed();
        }

        public void OnActionsButtonClick(View view)
        {
            if (viewActionsContentView.IsActionsShown)
            {
                viewActionsContentView.ShowContent();
            }
            else
            {
                viewActionsContentView.ShowActions();
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString(STATE_URI, CurrentUri.ToString());
            outState.PutString(STATE_FRAGMENT_TAG, CurrentContentFragmentTag);
        }

        public void OnSourceCodeClick(View view)
        {
            Intent i = new Intent(Intent.ActionView);
            i.SetData(Uri.Parse(GetString(Resource.String.sources_link)));

            StartActivity(i);
        }

        public void UpdateContent(Uri uri)
        {
            Fragment fragment;
            string tag;

            FragmentManager fm = SupportFragmentManager;
            FragmentTransaction tr = fm.BeginTransaction();

            if (CurrentContentFragmentTag != null)
            {
                Fragment currentFragment = fm.FindFragmentByTag(CurrentContentFragmentTag);
                if (currentFragment != null)
                {
                    tr.Hide(currentFragment);
                }
            }

            if (AboutFragment.ABOUT_URI.Equals(uri))
            {
                tag = AboutFragment.TAG;
                Fragment foundFragment = fm.FindFragmentByTag(tag);
                if (foundFragment != null)
                {
                    fragment = foundFragment;
                }
                else
                {
                    fragment = new AboutFragment();
                }
            }
            else if (SandboxFragment.SETTINGS_URI.Equals(uri))
            {
                tag = SandboxFragment.TAG;
                SandboxFragment foundFragment = (SandboxFragment)fm.FindFragmentByTag(tag);
                if (foundFragment != null)
                {
                    foundFragment.onSettingsChangedListener = mSettingsChangedListener;
                    fragment = foundFragment;
                }
                else
                {
                    SandboxFragment settingsFragment = new SandboxFragment();
                    settingsFragment.onSettingsChangedListener = mSettingsChangedListener;
                    fragment = settingsFragment;
                }
            }
            else if (uri != null)
            {
                tag = WebViewFragment.TAG;
                WebViewFragment webViewFragment;
                Fragment foundFragment = fm.FindFragmentByTag(tag);
                if (foundFragment != null)
                {
                    fragment = foundFragment;
                    webViewFragment = (WebViewFragment)fragment;
                }
                else
                {
                    webViewFragment = new WebViewFragment();
                    fragment = webViewFragment;
                }
                webViewFragment.Url = uri.ToString();
            }
            else
            {
                return;
            }

            if (fragment.IsAdded)
            {
                tr.Show(fragment);
            }
            else
            {
                tr.Add(Resource.Id.content, fragment, tag);
            }
            tr.Commit();

            CurrentUri = uri;
            CurrentContentFragmentTag = tag;
        }

        private class SettingsChangedListener : SandboxFragment.OnSettingsChangedListener
        {
            bool InstanceFieldsInitialized = false;

            private ExamplesActivity OuterInstance;

            float MDensity;
            int MAdditionaSpacingWidth;

            void InitializeInstanceFields()
            {
                MAdditionaSpacingWidth = (int)(100 * MDensity);
            }

            public SettingsChangedListener(ExamplesActivity outerInstance)
            {
                this.OuterInstance = outerInstance;

                if (!InstanceFieldsInitialized)
                {
                    MDensity = OuterInstance.Resources.DisplayMetrics.Density;

                    InitializeInstanceFields();
                    InstanceFieldsInitialized = true;
                }
            }

            public void OnSettingChanged(int prefId, int value)
            {
                switch (prefId)
                {
                    case SandboxFragment.PREF_SPACING_TYPE:
                        int currentType = OuterInstance.viewActionsContentView.SpacingType;

                        if (currentType == value)
                        {
                            return;
                        }

                        int spacingWidth = OuterInstance.viewActionsContentView.SpacingWidth;

                        if (value == ActionsContentView.SpacingActionsWidth)
                        {
                            OuterInstance.viewActionsContentView.SpacingWidth = spacingWidth + MAdditionaSpacingWidth;
                        }
                        else if (value == ActionsContentView.SpacingRightOffset)
                        {
                            OuterInstance.viewActionsContentView.SpacingWidth = spacingWidth - MAdditionaSpacingWidth;
                        }

                        OuterInstance.viewActionsContentView.SpacingType = value;

                        return;
                    case SandboxFragment.PREF_SPACING_WIDTH:
                        int width;
                        if (OuterInstance.viewActionsContentView.SpacingType == ActionsContentView.SpacingActionsWidth)
                        {
                            width = (int)(value * MDensity) + MAdditionaSpacingWidth;
                        }
                        else
                        {
                            width = (int)(value * MDensity);
                        }
                        OuterInstance.viewActionsContentView.SpacingWidth = width;
                        return;
                    case SandboxFragment.PREF_SPACING_ACTIONS_WIDTH:
                        OuterInstance.viewActionsContentView.ActionsSpacingWidth = (int)(value * MDensity);
                        return;
                    case SandboxFragment.PREF_SHOW_SHADOW:
                        OuterInstance.viewActionsContentView.ShadowVisible = value == 1;
                        return;
                    case SandboxFragment.PREF_FADE_TYPE:
                        OuterInstance.viewActionsContentView.FadeType = value;
                        return;
                    case SandboxFragment.PREF_FADE_MAX_VALUE:
                        OuterInstance.viewActionsContentView.FadeValue = value;
                        return;
                    case SandboxFragment.PREF_SWIPING_TYPE:
                        OuterInstance.viewActionsContentView.SwipingType = value;
                        return;
                    case SandboxFragment.PREF_SWIPING_EDGE_WIDTH:
                        OuterInstance.viewActionsContentView.SwipingEdgeWidth = value;
                        return;
                    case SandboxFragment.PREF_FLING_DURATION:
                        OuterInstance.viewActionsContentView.FlingDuration = value;
                        return;
                    default:
                        return;
                }
            }
        }
    }
}