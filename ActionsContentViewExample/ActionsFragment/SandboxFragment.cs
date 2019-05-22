using Android.Content.Res;
using Android.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;

using System;

using UI.ActionsContentView;
//using ActionsContentViewLibrary;

namespace ActionsContentViewExample.ActionFragment
{
    public class SandboxFragment : Fragment, View.IOnClickListener
    {
        public static string TAG = typeof(SandboxFragment).FullName;
        private const bool DEBUG = false;

        public interface OnSettingsChangedListener
        {
            void OnSettingChanged(int prefId, int value);
        }

        private static string SETTINGS_SCHEME = "settings";
        private static string SETTINGS_AUTHORITY = "sandbox";
        public static Android.Net.Uri SETTINGS_URI = new Android.Net.Uri.Builder().Scheme(SETTINGS_SCHEME).Authority(SETTINGS_AUTHORITY).Build();

        public const int PREF_SPACING_TYPE = Resource.Id.prefSpacingType;
        public const int PREF_SPACING_WIDTH = Resource.Id.prefSpacingWidth;
        public const int PREF_SPACING_ACTIONS_WIDTH = Resource.Id.prefSpacingActionsWidth;
        public const int PREF_SHOW_SHADOW = Resource.Id.prefShowShadow;
        public const int PREF_SHADOW_WIDTH = Resource.Id.prefShadowWidth;
        public const int PREF_FADE_TYPE = Resource.Id.prefFadeType;
        public const int PREF_FADE_MAX_VALUE = Resource.Id.prefFadeMaxValue;
        public const int PREF_SWIPING_TYPE = Resource.Id.prefSwipingType;
        public const int PREF_SWIPING_EDGE_WIDTH = Resource.Id.prefSwipingEdgeWidth;
        public const int PREF_FLING_DURATION = Resource.Id.prefFlingDuration;

        private const int PREF_SPACING_TYPE_VALUE = Resource.Id.prefSpacingTypeValue;
        private const int PREF_SPACING_WIDTH_VALUE = Resource.Id.prefSpacingWidthValue;
        private const int PREF_SPACING_ACTIONS_WIDTH_VALUE = Resource.Id.prefSpacingActionsWidthValue;
        private const int PREF_SHOW_SHADOW_VALUE = Resource.Id.prefShowShadowValue;
        private const int PREF_SHADOW_WIDTH_VALUE = Resource.Id.prefShadowWidthValue;
        private const int PREF_FADE_TYPE_VALUE = Resource.Id.prefFadeTypeValue;
        private const int PREF_FADE_MAX_VALUE_VALUE = Resource.Id.prefFadeMaxValueValue;
        private const int PREF_SWIPING_TYPE_VALUE = Resource.Id.prefSwipingTypeValue;
        private const int PREF_SWIPING_EDGE_WIDTH_VALUE = Resource.Id.prefSwipingEdgeWidthValue;
        private const int PREF_FLING_DURATION_VALUE = Resource.Id.prefFlingDurationValue;

        private View ViewRoot;
        private OnSettingsChangedListener mSettingsChangedListener;

        public virtual OnSettingsChangedListener onSettingsChangedListener
        {
            set
            {
                mSettingsChangedListener = value;
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            SaveStringPrefState(outState, PREF_SPACING_TYPE_VALUE);
            SaveStringPrefState(outState, PREF_SPACING_WIDTH_VALUE);
            SaveStringPrefState(outState, PREF_SPACING_ACTIONS_WIDTH_VALUE);
            SaveBooleanPrefState(outState, PREF_SHOW_SHADOW_VALUE);
            SaveStringPrefState(outState, PREF_SHADOW_WIDTH_VALUE);
            SaveStringPrefState(outState, PREF_FADE_TYPE_VALUE);
            SaveStringPrefState(outState, PREF_FADE_MAX_VALUE_VALUE);
            SaveStringPrefState(outState, PREF_SWIPING_TYPE_VALUE);
            SaveStringPrefState(outState, PREF_SWIPING_EDGE_WIDTH_VALUE);
            SaveStringPrefState(outState, PREF_FLING_DURATION_VALUE);

            base.OnSaveInstanceState(outState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
            {
                string spacingType = RestoreStringPrefState(savedInstanceState, PREF_SPACING_TYPE_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_SPACING_WIDTH_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_SPACING_ACTIONS_WIDTH_VALUE);
                RestoreBooleanPrefState(savedInstanceState, PREF_SHOW_SHADOW_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_SHADOW_WIDTH_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_FADE_TYPE_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_FADE_MAX_VALUE_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_SWIPING_TYPE_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_SWIPING_EDGE_WIDTH_VALUE);
                RestoreStringPrefState(savedInstanceState, PREF_FLING_DURATION_VALUE);

                int[] spacingTypes = Resources.GetIntArray(Resource.Array.spacing_types_values);
                string[] spacingTypeValues = Resources.GetStringArray(Resource.Array.spacing_types_short);
                int count = spacingTypeValues.Length;
                for (int i = 0; i < count; ++i)
                {
                    if (spacingType.Equals(spacingTypeValues[i]))
                    {
                        // showing additional value for spacing
                        if (spacingTypes[i] == ActionsContentView.SpacingActionsWidth)
                        {
                            ViewRoot.FindViewById(Resource.Id.prefSpacingWidthAdditionalValue).Visibility = ViewStates.Visible;
                        }
                        else
                        {
                            ViewRoot.FindViewById(Resource.Id.prefSpacingWidthAdditionalValue).Visibility = ViewStates.Gone;
                        }
                        break;
                    }
                }
            }

            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewRoot = inflater.Inflate(Resource.Layout.sandbox, container, false);

            ViewRoot.FindViewById(PREF_SPACING_TYPE).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_SPACING_WIDTH).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_SPACING_ACTIONS_WIDTH).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_SHOW_SHADOW).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_SHADOW_WIDTH).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_FADE_MAX_VALUE).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_FADE_TYPE).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_SWIPING_TYPE).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_SWIPING_EDGE_WIDTH).SetOnClickListener(this);
            ViewRoot.FindViewById(PREF_FLING_DURATION).SetOnClickListener(this);

            return ViewRoot;
        }

        public void OnClick(View v)
        {
            int id = v.Id;

            int titleId;
            int valueId;
            int itemsArrayId;
            int valuesArrayId;

            switch (id)
            {
                case PREF_SPACING_TYPE:
                    titleId = Resource.String.pref_spacing_type;
                    valueId = PREF_SPACING_TYPE_VALUE;
                    itemsArrayId = Resource.Array.spacing_types;
                    valuesArrayId = Resource.Array.spacing_types_values;
                    break;
                case PREF_SPACING_WIDTH:
                    titleId = Resource.String.pref_spacing_width;
                    valueId = PREF_SPACING_WIDTH_VALUE;
                    itemsArrayId = Resource.Array.width_strings;
                    valuesArrayId = Resource.Array.width_values;
                    break;
                case PREF_SPACING_ACTIONS_WIDTH:
                    titleId = Resource.String.pref_spacing_actions;
                    valueId = PREF_SPACING_ACTIONS_WIDTH_VALUE;
                    itemsArrayId = Resource.Array.width_strings;
                    valuesArrayId = Resource.Array.width_values;
                    break;
                case PREF_SHOW_SHADOW:
                    CheckBox viewValue = v.FindViewById<CheckBox>(PREF_SHOW_SHADOW_VALUE);
                    bool @checked = !viewValue.Checked;
                    viewValue.Checked = @checked;
                    if (mSettingsChangedListener != null)
                    {
                        mSettingsChangedListener.OnSettingChanged(id, @checked ? 1 : 0);
                    }
                    else if (DEBUG)
                    {
                        System.Console.WriteLine("OnSettingsChangedListener is not set");
                    }
                    return;
                case PREF_SHADOW_WIDTH:
                    valueId = PREF_SHADOW_WIDTH_VALUE;
                    titleId = Resource.String.pref_shadow_width;
                    itemsArrayId = Resource.Array.width_strings;
                    valuesArrayId = Resource.Array.width_values;
                    break;
                case PREF_FADE_TYPE:
                    titleId = Resource.String.pref_fade_type;
                    valueId = PREF_FADE_TYPE_VALUE;
                    itemsArrayId = Resource.Array.fade_types;
                    valuesArrayId = Resource.Array.fade_types_values;
                    break;
                case PREF_FADE_MAX_VALUE:
                    titleId = Resource.String.pref_fade_max_value;
                    valueId = PREF_FADE_MAX_VALUE_VALUE;
                    itemsArrayId = Resource.Array.fade_max_value_strings;
                    valuesArrayId = Resource.Array.fade_max_value_values;
                    break;
                case PREF_SWIPING_TYPE:
                    titleId = Resource.String.pref_swiping_type;
                    valueId = PREF_SWIPING_TYPE_VALUE;
                    itemsArrayId = Resource.Array.swiping_types;
                    valuesArrayId = Resource.Array.swiping_types_values;
                    break;
                case PREF_SWIPING_EDGE_WIDTH:
                    valueId = PREF_SWIPING_EDGE_WIDTH_VALUE;
                    titleId = Resource.String.pref_swiping_edge_width;
                    itemsArrayId = Resource.Array.width_strings;
                    valuesArrayId = Resource.Array.width_values;
                    break;
                case PREF_FLING_DURATION:
                    titleId = Resource.String.pref_other_fling_duration;
                    valueId = PREF_FLING_DURATION_VALUE;
                    itemsArrayId = Resource.Array.fling_duration_strings;
                    valuesArrayId = Resource.Array.fling_duration_values;
                    break;
                default:
                    return;
            }

            Fragment prev = FragmentManager.FindFragmentByTag(ValueChooserDialogFragment.TAG);
            if (prev != null)
            {
                FragmentManager.BeginTransaction().Remove(prev).Commit();
            }

            ValueChooserDialogFragment fragment = ValueChooserDialogFragment.NewInstance(id, titleId, itemsArrayId);
            fragment.onSettingsSelectedListener = new OnSettingSelectedListenerAnonymousInnerClassHelper(this, v, id, valueId, valuesArrayId);
            fragment.Show(FragmentManager, ValueChooserDialogFragment.TAG);
        }

        private class OnSettingSelectedListenerAnonymousInnerClassHelper : ValueChooserDialogFragment.OnSettingSelectedListener
        {
            private readonly SandboxFragment OuterInstance;

            private View v;
            private int Id;
            private int ValueId;
            private int ValuesArrayId;

            public OnSettingSelectedListenerAnonymousInnerClassHelper(SandboxFragment outerInstance, View v, int id, int valueId, int valuesArrayId)
            {
                this.OuterInstance = outerInstance;
                this.v = v;
                this.Id = id;
                this.ValueId = valueId;
                this.ValuesArrayId = valuesArrayId;
            }

            public void OnSettingSelected(int id, int item)
            {
                int[] values = OuterInstance.Resources.GetIntArray(ValuesArrayId);

                switch (id)
                {
                    case PREF_SPACING_TYPE:
                        {
                            TextView viewValue = v.FindViewById<TextView>(ValueId);
                            string value = OuterInstance.Resources.GetStringArray(Resource.Array.spacing_types_short)[item];
                            viewValue.Text = value;

                            // showing additional value for spacing
                            if (values[item] == ActionsContentView.SpacingActionsWidth)
                            {
                                OuterInstance.ViewRoot.FindViewById(Resource.Id.prefSpacingWidthAdditionalValue).Visibility = ViewStates.Visible;
                            }
                            else
                            {
                                OuterInstance.ViewRoot.FindViewById(Resource.Id.prefSpacingWidthAdditionalValue).Visibility = ViewStates.Gone;
                            }
                            break;
                        }
                    case PREF_FADE_TYPE:
                        {
                            TextView viewValue = v.FindViewById<TextView>(ValueId);
                            string value = OuterInstance.Resources.GetStringArray(Resource.Array.fade_types)[item];
                            viewValue.Text = value;
                            break;
                        }
                    case PREF_SWIPING_TYPE:
                        {
                            TextView viewValue = v.FindViewById<TextView>(ValueId);
                            string value = OuterInstance.Resources.GetStringArray(Resource.Array.swiping_types)[item];
                            viewValue.Text = value;
                            break;
                        }
                    case PREF_SWIPING_EDGE_WIDTH:
                    case PREF_SPACING_WIDTH:
                    case PREF_SHADOW_WIDTH:
                    case PREF_FADE_MAX_VALUE:
                    case PREF_FLING_DURATION:
                        {
                            TextView viewValue = v.FindViewById<TextView>(ValueId);
                            string value = Convert.ToString(values[item]);
                            viewValue.Text = value;
                            break;
                        }
                    case PREF_SHOW_SHADOW:
                        break;
                }

                if (OuterInstance.mSettingsChangedListener != null)
                {
                    OuterInstance.mSettingsChangedListener.OnSettingChanged(id, values[item]);
                }
                else if (DEBUG)
                {
                    System.Console.WriteLine("OnSettingsChangedListener is not set");
                }
            }
        }

        private void SaveStringPrefState(Bundle outState, int prefValue)
        {
            TextView viewValue = ViewRoot.FindViewById<TextView>(prefValue);
            outState.PutString(Convert.ToString(prefValue), viewValue.Text.ToString());
        }

        private void SaveBooleanPrefState(Bundle outState, int prefValue)
        {
            CompoundButton viewValue = ViewRoot.FindViewById<CompoundButton>(prefValue);
            outState.PutBoolean(Convert.ToString(prefValue), viewValue.Checked);
        }

        private string RestoreStringPrefState(Bundle savedInstanceState, int prefValue)
        {
            string value = savedInstanceState.GetString(Convert.ToString(prefValue));
            TextView viewValue = ViewRoot.FindViewById<TextView>(prefValue);
            viewValue.Text = value;

            return value;
        }

        private bool RestoreBooleanPrefState(Bundle savedInstanceState, int prefValue)
        {
            bool value = savedInstanceState.GetBoolean(Convert.ToString(prefValue));
            CompoundButton viewValue = ViewRoot.FindViewById<CompoundButton>(prefValue);
            viewValue.Checked = value;

            return value;
        }
    }

}