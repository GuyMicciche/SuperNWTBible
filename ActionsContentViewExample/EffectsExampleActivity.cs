using Android.App;
using Android.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Text;
using Android.Views;
using Android.Widget;

using UI.ActionsContentView;
//using ActionsContentViewLibrary;

using ActionsContentViewExample.ActionsAdapters;

namespace ActionsContentViewExample
{
    [Activity(Label = "Effects Example")]
    public class EffectsExampleActivity : FragmentActivity
    {
        private const string STATE_POSITION = "state:layout_id";

        private const string SCHEME = "settings";
        private const string AUTHORITY = "effects";
        public static Uri URI = new Uri.Builder().Scheme(SCHEME).Authority(AUTHORITY).Build();

        private EffectsAdapter MAdapter;

        private ListView ViewList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MAdapter = new EffectsAdapter(this);

            int selectedPosition;
            if (savedInstanceState != null)
            {
                selectedPosition = savedInstanceState.GetInt(STATE_POSITION, 0);
            }
            else
            {
                selectedPosition = 0;
            }

            Init(selectedPosition);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            int position = ViewList.SelectedItemPosition;
            if (position != ListView.InvalidPosition)
            {
                outState.PutInt(STATE_POSITION, position);
            }
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @SuppressLint("DefaultLocale") private void init(int position)
        private void Init(int position)
        {
            int layoutId = MAdapter.GetItemAtPosition(position);
            SetContentView(layoutId);

            string titleText = GetString(Resource.String.action_effects);
            TextView title = FindViewById<TextView>(Android.Resource.Id.Text1);
            title.Text = titleText.ToUpper();

            ActionsContentView viewActionsContentView = FindViewById<ActionsContentView>(Resource.Id.actionsContentView);
            viewActionsContentView.OnActionsContentListener = new OnActionsContentListenerAnonymousInnerClassHelper(this);

            TextView name = FindViewById<TextView>(Resource.Id.effect_name);
            name.Text = MAdapter.GetEffectTitle(position);

            TextView actions = FindViewById<TextView>(Resource.Id.actions_html);
            string actionsHtml = MAdapter.GetActionsHtml(position);
            if (!TextUtils.IsEmpty(actionsHtml))
            {
                FindViewById(Resource.Id.effect_actions_layout).Visibility = ViewStates.Visible;
                actions.Text = Html.FromHtml(actionsHtml).ToString();
            }

            TextView content = FindViewById<TextView>(Resource.Id.content_html);
            string contentHtml = MAdapter.GetContentHtml(position);
            if (!TextUtils.IsEmpty(contentHtml))
            {
                FindViewById(Resource.Id.effect_content_layout).Visibility = ViewStates.Visible;
                content.Text = Html.FromHtml(contentHtml).ToString();
            }

            ViewList = FindViewById<ListView>(Resource.Id.actions);
            ViewList.Adapter = MAdapter;
            ViewList.OnItemClickListener = new OnItemClickListenerAnonymousInnerClassHelper(this, position);
        }

        private class OnActionsContentListenerAnonymousInnerClassHelper : Java.Lang.Object, ActionsContentView.IOnActionsContentListener
        {
            private EffectsExampleActivity OuterInstance;

            public OnActionsContentListenerAnonymousInnerClassHelper(EffectsExampleActivity outerInstance)
            {
                this.OuterInstance = outerInstance;
            }

            public void OnContentStateChanged(ActionsContentView v, bool isContentShown)
            {
                //v.ContentController.IgnoreTouchEvents = !isContentShown;
            }
        }

        private class OnItemClickListenerAnonymousInnerClassHelper : Java.Lang.Object, AdapterView.IOnItemClickListener
        {
            private EffectsExampleActivity OuterInstance;

            private int Position;

            public OnItemClickListenerAnonymousInnerClassHelper(EffectsExampleActivity outerInstance, int position)
            {
                this.OuterInstance = outerInstance;
                this.Position = position;
            }

            public void OnItemClick(AdapterView adapter, View v, int position, long flags)
            {
                OuterInstance.Init(position);
            }
        }

    }

}