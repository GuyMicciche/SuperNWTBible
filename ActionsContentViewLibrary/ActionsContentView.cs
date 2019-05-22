using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Interop;
using Java.Lang;
using System;

namespace ActionsContentView
{
    public class ActionsContentView : ViewGroup
    {
        private static readonly string TAG = Java.Lang.Class.FromType(typeof(ActionsContentView)).SimpleName;
        private static bool DEBUG = false;

        public interface IOnActionsContentListener
        {
            void OnContentStateChanged(ActionsContentView v, bool isContentShown);
            void OnContentStateInAction(ActionsContentView v, bool isContentShowing);
        }

        private const int FLING_MIN = 1000;

        /// <summary>
        /// Spacing will be calculated as offset from right bound of view.
        /// </summary>
        public const int SPACING_RIGHT_OFFSET = 0;
        /// <summary>
        /// Spacing will be calculated as right bound of actions bar container.
        /// </summary>
        public const int SPACING_ACTIONS_WIDTH = 1;

        /// <summary>
        /// Fade is disabled.
        /// </summary>
        public const int FADE_NONE = 0;
        /// <summary>
        /// Fade applies to actions container.
        /// </summary>
        public const int FADE_ACTIONS = 1;
        /// <summary>
        /// Fade applies to content container.
        /// </summary>
        public const int FADE_CONTENT = 2;
        /// <summary>
        /// Fade applies to every container.
        /// </summary>
        public const int FADE_BOTH = 3;

        /// <summary>
        /// Swiping will be handled at any point of screen.
        /// </summary>
        public const int SWIPING_ALL = 0;
        /// <summary>
        /// Swiping will be handled starting from screen edge only.
        /// </summary>
        public const int SWIPING_EDGE = 1;

        public const int EFFECTS_NONE = 0;
        public static readonly int EFFECTS_SCROLL_OPENING = 1 << 0;
        public static readonly int EFFECTS_SCROLL_CLOSING = 1 << 1;
        public static readonly int EFFECTS_SCROLL = EFFECTS_SCROLL_OPENING | EFFECTS_SCROLL_CLOSING;
        public static readonly int EFFECTS_FLING_OPENING = 1 << 2;
        public static readonly int EFFECTS_FLING_CLOSING = 1 << 3;
        public static readonly int EFFECTS_FLING = EFFECTS_FLING_OPENING | EFFECTS_FLING_CLOSING;
        public static readonly int EFFECTS_ALL = EFFECTS_SCROLL | EFFECTS_FLING;

        private readonly ContentScrollController mScrollController;
        private readonly GestureDetector mGestureDetector;

        private readonly View viewShadow;
        private readonly ActionsLayout viewActionsContainer;
        private readonly ContentLayout viewContentContainer;

        /// <summary>
        /// Spacing type.
        /// </summary>
        private int mSpacingType = SPACING_RIGHT_OFFSET;
        /// <summary>
        /// Value of spacing to use.
        /// </summary>
        private int mSpacing;

        /// <summary>
        /// Value of actions container spacing to use.
        /// </summary>
        private int mActionsSpacing;

        /// <summary>
        /// Value of shadow width.
        /// </summary>
        private int mShadowWidth = 0;

        /// <summary>
        /// Indicates how long flinging will take time in milliseconds.
        /// </summary>
        private int mFlingDuration = 250;

        /// <summary>
        /// Fade type.
        /// </summary>
        private int mFadeType = FADE_NONE;
        /// <summary>
        /// Max fade value.
        /// </summary>
        private int mFadeValue;

        /// <summary>
        /// Indicates whether swiping is enabled or not.
        /// </summary>
        private bool isSwipingEnabled = true;
        /// <summary>
        /// Swiping type.
        /// </summary>
        private int mSwipeType = FADE_NONE;
        /// <summary>
        /// Swiping edge width.
        /// </summary>
        private int mSwipeEdgeWidth;

        /// <summary>
        /// Indicates whether refresh of content position should be done on next layout calculation.
        /// </summary>
        private bool mForceRefresh = false;

        private int mEffects = EFFECTS_ALL;

        private IOnActionsContentListener MOnActionsContentListener;

        public ActionsContentView(Context context)
            : this(context, null)
        {
        }

        public ActionsContentView(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public ActionsContentView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {

            SetClipChildren(false);
            SetClipToPadding(false);

            // Reading attributes
            TypedArray a = Context.ObtainStyledAttributes(attrs, Resource.Styleable.ActionsContentView);
            mSpacingType = a.GetInteger(Resource.Styleable.ActionsContentView_spacing_type, SPACING_RIGHT_OFFSET);
            int spacingDefault = context.Resources.GetDimensionPixelSize(Resource.Dimension.default_actionscontentview_spacing);
            mSpacing = a.GetDimensionPixelSize(Resource.Styleable.ActionsContentView_spacing, spacingDefault);
            int actionsSpacingDefault = context.Resources.GetDimensionPixelSize(Resource.Dimension.default_actionscontentview_actions_spacing);
            mActionsSpacing = a.GetDimensionPixelSize(Resource.Styleable.ActionsContentView_actions_spacing, actionsSpacingDefault);

            int actionsLayout = a.GetResourceId(Resource.Styleable.ActionsContentView_actions_layout, 0);
            int contentLayout = a.GetResourceId(Resource.Styleable.ActionsContentView_content_layout, 0);

            mShadowWidth = a.GetDimensionPixelSize(Resource.Styleable.ActionsContentView_shadow_width, 0);
            int shadowDrawableRes = a.GetResourceId(Resource.Styleable.ActionsContentView_shadow_drawable, 0);

            mFadeType = a.GetInteger(Resource.Styleable.ActionsContentView_fade_type, FADE_NONE);
            int fadeValueDefault = context.Resources.GetInteger(Resource.Integer.default_actionscontentview_fade_max_value);
            mFadeValue = (int)a.GetInt(Resource.Styleable.ActionsContentView_fade_max_value, fadeValueDefault);

            FadeValue = mFadeValue;

            int flingDurationDefault = context.Resources.GetInteger(Resource.Integer.default_actionscontentview_fling_duration);
            mFlingDuration = a.GetInteger(Resource.Styleable.ActionsContentView_fling_duration, flingDurationDefault);

            mSwipeType = a.GetInteger(Resource.Styleable.ActionsContentView_swiping_type, SWIPING_EDGE);
            int swipingEdgeWidthDefault = context.Resources.GetDimensionPixelSize(Resource.Dimension.default_actionscontentview_swiping_edge_width);
            mSwipeEdgeWidth = a.GetDimensionPixelSize(Resource.Styleable.ActionsContentView_swiping_edge_width, swipingEdgeWidthDefault);

            int effectActionsRes = a.GetResourceId(Resource.Styleable.ActionsContentView_effect_actions, 0);
            int effectContentRes = a.GetResourceId(Resource.Styleable.ActionsContentView_effect_content, 0);
            mEffects = a.GetInt(Resource.Styleable.ActionsContentView_effects, EFFECTS_ALL);

            int effectsInterpolatorRes = a.GetResourceId(Resource.Styleable.ActionsContentView_effects_interpolator, 0);

            a.Recycle();

            if (DEBUG)
            {
                Console.WriteLine(TAG, "Values from layout");
                Console.WriteLine(TAG, "  spacing type: " + mSpacingType);
                Console.WriteLine(TAG, "  spacing value: " + mSpacing);
                Console.WriteLine(TAG, "  actions spacing value: " + mActionsSpacing);
                Console.WriteLine(TAG, "  actions layout id: " + actionsLayout);
                Console.WriteLine(TAG, "  content layout id: " + contentLayout);
                Console.WriteLine(TAG, "  shadow drawable: " + shadowDrawableRes);
                Console.WriteLine(TAG, "  shadow width: " + mShadowWidth);
                Console.WriteLine(TAG, "  fade type: " + mFadeType);
                Console.WriteLine(TAG, "  fade max value: " + mFadeValue);
                Console.WriteLine(TAG, "  fling duration: " + mFlingDuration);
                Console.WriteLine(TAG, "  swiping type: " + mSwipeType);
                Console.WriteLine(TAG, "  swiping edge width: " + mSwipeEdgeWidth);
                Console.WriteLine(TAG, "  swiping enabled: " + isSwipingEnabled);
                Console.WriteLine(TAG, "  effects: " + mEffects);
                Console.WriteLine(TAG, "  effect actions: " + effectActionsRes);
                Console.WriteLine(TAG, "  effect content: " + effectContentRes);
                Console.WriteLine(TAG, "  effects interpolator: " + effectsInterpolatorRes);
            }

            Scroller effectsScroller;

            if (effectsInterpolatorRes > 0)
            {
                IInterpolator interpolator = AnimationUtils.LoadInterpolator(Context, effectsInterpolatorRes);
                effectsScroller = new Scroller(context, interpolator);
            }
            else
            {
                effectsScroller = new Scroller(context);
            }

            mScrollController = new ContentScrollController(this, new Scroller(context), effectsScroller);

            mGestureDetector = new GestureDetector(context, mScrollController);
            mGestureDetector.IsLongpressEnabled = true;

            LayoutInflater inflater = LayoutInflater.From(context);
            viewActionsContainer = new ActionsLayout(context);
            if (actionsLayout != 0)
            {
                inflater.Inflate(actionsLayout, viewActionsContainer, true);
            }

            base.AddView(viewActionsContainer, 0, new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent));

            viewContentContainer = new ContentLayout(context);
            viewContentContainer.OnSwipeListener = new MyOnSwipeListener(this);

            viewShadow = new View(context);
            viewShadow.SetBackgroundResource(shadowDrawableRes);
            LinearLayout.LayoutParams shadowParams = new LinearLayout.LayoutParams(mShadowWidth, LinearLayout.LayoutParams.MatchParent);
            viewShadow.LayoutParameters = shadowParams;
            viewContentContainer.AddView(viewShadow);

            if (mShadowWidth <= 0 || shadowDrawableRes == 0)
            {
                viewShadow.Visibility = ViewStates.Gone;
            }

            if (contentLayout != 0)
            {
                inflater.Inflate(contentLayout, viewContentContainer, true);
            }

            base.AddView(viewContentContainer, 1, new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent));

            if (effectActionsRes > 0)
            {
                viewActionsContainer.Controller.SetEffects(effectActionsRes);
            }
            if (effectContentRes > 0)
            {
                viewContentContainer.Controller.SetEffects(effectContentRes);
            }
        }

        private class MyOnSwipeListener : ContentLayout.IOnSwipeListener
        {
            private ActionsContentView view;

            public MyOnSwipeListener(ActionsContentView view)
            {
                this.view = view;
            }

            public void OnSwipe(int scrollPosition)
            {
                view.UpdateScrollFactor();
            }
        }

        public IOnActionsContentListener ActionsContentListener
        {
            set
            {
                MOnActionsContentListener = value;
            }
            get
            {
                return MOnActionsContentListener;
            }
        }


        /// <summary>
        /// This method is not supported and throws an UnsupportedOperationException when called.
        /// </summary>
        /// <param name="child"> Ignored.
        /// </param>
        /// <exception cref="UnsupportedOperationException"> Every time this method is invoked. </exception>
        public override void AddView(View child)
        {
            throw new UnsupportedOperationException("addView(View) is not supported in " + TAG);
        }

        /// <summary>
        /// This method is not supported and throws an UnsupportedOperationException when called.
        /// </summary>
        /// <param name="child"> Ignored. </param>
        /// <param name="index"> Ignored.
        /// </param>
        /// <exception cref="UnsupportedOperationException"> Every time this method is invoked. </exception>
        public override void AddView(View child, int index)
        {
            throw new UnsupportedOperationException("addView(View, int) is not supported in " + TAG);
        }

        /// <summary>
        /// This method is not supported and throws an UnsupportedOperationException when called.
        /// </summary>
        /// <param name="child"> Ignored. </param>
        /// <param name="params"> Ignored.
        /// </param>
        /// <exception cref="UnsupportedOperationException"> Every time this method is invoked. </exception>
        public override void AddView(View child, LayoutParams @params)
        {
            throw new UnsupportedOperationException("addView(View, LayoutParams) is not supported in " + TAG);
        }

        /// <summary>
        /// This method is not supported and throws an UnsupportedOperationException when called.
        /// </summary>
        /// <param name="child"> Ignored. </param>
        /// <param name="index"> Ignored. </param>
        /// <param name="params"> Ignored.
        /// </param>
        /// <exception cref="UnsupportedOperationException"> Every time this method is invoked. </exception>
        public override void AddView(View child, int index, LayoutParams @params)
        {
            throw new UnsupportedOperationException("addView(View, int, LayoutParams) is not supported in " + TAG);
        }

        /// <summary>
        /// This method is not supported and throws an UnsupportedOperationException when called.
        /// </summary>
        /// <param name="child"> Ignored.
        /// </param>
        /// <exception cref="UnsupportedOperationException"> Every time this method is invoked. </exception>
        public override void RemoveView(View child)
        {
            throw new UnsupportedOperationException("removeView(View) is not supported in " + TAG);
        }

        /// <summary>
        /// This method is not supported and throws an UnsupportedOperationException when called.
        /// </summary>
        /// <param name="index"> Ignored.
        /// </param>
        /// <exception cref="UnsupportedOperationException"> Every time this method is invoked. </exception>
        public override void RemoveViewAt(int index)
        {
            throw new UnsupportedOperationException("removeViewAt(int) is not supported in " + TAG);
        }

        /// <summary>
        /// This method is not supported and throws an UnsupportedOperationException when called.
        /// </summary>
        /// <exception cref="UnsupportedOperationException"> Every time this method is invoked. </exception>
        public override void RemoveAllViews()
        {
            throw new UnsupportedOperationException("removeAllViews() is not supported in " + TAG);
        }

        public IParcelable OnSaveInstanceState()
        {
            IParcelable superState = base.OnSaveInstanceState();
            SavedState ss = new SavedState(superState);
            ss.isContentShown = IsContentShown;
            ss.mSpacingType = SpacingType;
            ss.mSpacing = SpacingWidth;
            ss.mActionsSpacing = ActionsSpacingWidth;
            ss.isShadowVisible = ShadowVisible;
            ss.mShadowWidth = ShadowWidth;
            ss.isSwipingEnabled = SwipingEnabled;
            ss.mFlingDuration = FlingDuration;
            ss.mFadeType = FadeType;
            ss.mFadeValue = FadeValue;
            ss.mSwipeType = SwipingType;
            ss.mSwipeEdgeWidth = SwipingEdgeWidth;
            return ss;
        }

        public void OnRestoreInstanceState(IParcelable state)
        {
            if (!(state is SavedState))
            {
                base.OnRestoreInstanceState(state);
                return;
            }

            SavedState ss = (SavedState)state;
            base.OnRestoreInstanceState(ss.SuperState);

            mScrollController.isContentShown = ss.isContentShown;

            mSpacingType = ss.mSpacingType;
            mSpacing = ss.mSpacing;
            mActionsSpacing = ss.mActionsSpacing;
            isSwipingEnabled = ss.isSwipingEnabled;
            mSwipeType = ss.mSwipeType;
            mSwipeEdgeWidth = ss.mSwipeEdgeWidth;
            mFlingDuration = ss.mFlingDuration;
            mFadeType = ss.mFadeType;
            mFadeValue = ss.mFadeValue;

            viewShadow.Visibility = ss.isShadowVisible ? ViewStates.Visible : ViewStates.Gone;

            // This will call requestLayout() to calculate layout according to values
            ShadowWidth = ss.mShadowWidth;
        }

        public ViewGroup ActionsContainer
        {
            get
            {
                return viewActionsContainer;
            }
        }

        public ViewGroup ContentContainer
        {
            get
            {
                return viewContentContainer;
            }
        }

        public IContainerController ActionsController
        {
            get
            {
                return viewActionsContainer.Controller;
            }
        }

        public IContainerController ContentController
        {
            get
            {
                return viewContentContainer.Controller;
            }
        }

        public bool IsActionsShown
        {
            get
            {
                return !mScrollController.IsContentShown;
            }
        }

        public void ShowActions()
        {
            mScrollController.HideContent(mFlingDuration);
        }

        public bool IsContentShown
        {
            get
            {
                return mScrollController.IsContentShown;
            }
        }

        public virtual void ShowContent()
        {
            mScrollController.ShowContent(mFlingDuration);
        }

        public void ToggleActions()
        {
            if (IsActionsShown)
            {
                ShowContent();
            }
            else
            {
                ShowActions();
            }
        }

        public int SpacingType
        {
            set
            {
                if (mSpacingType == value)
                {
                    return;
                }

                if (value != SPACING_RIGHT_OFFSET && value != SPACING_ACTIONS_WIDTH)
                {
                    return;
                }

                mSpacingType = value;
                mForceRefresh = true;
                RequestLayout();
            }
            get
            {
                return mSpacingType;
            }
        }


        public int SpacingWidth
        {
            set
            {
                if (mSpacing == value)
                {
                    return;
                }

                mSpacing = value;
                mForceRefresh = true;
                RequestLayout();
            }
            get
            {
                return mSpacing;
            }
        }


        public int ActionsSpacingWidth
        {
            set
            {
                if (mActionsSpacing == value)
                {
                    return;
                }

                mActionsSpacing = value;
                mForceRefresh = true;
                RequestLayout();
            }
            get
            {
                return mActionsSpacing;
            }
        }


        public virtual bool ShadowVisible
        {
            set
            {
                viewShadow.Visibility = value ? ViewStates.Visible : ViewStates.Gone;
                mForceRefresh = true;
                RequestLayout();
            }
            get
            {
                return viewShadow.Visibility == ViewStates.Visible;
            }
        }


        public virtual int ShadowWidth
        {
            set
            {
                if (mShadowWidth == value)
                {
                    return;
                }

                mShadowWidth = value;
                viewShadow.LayoutParameters.Width = mShadowWidth;
                mForceRefresh = true;
                RequestLayout();
            }
            get
            {
                return mShadowWidth;
            }
        }


        public virtual int FlingDuration
        {
            set
            {
                mFlingDuration = value;
            }
            get
            {
                return mFlingDuration;
            }
        }


        public virtual int FadeType
        {
            set
            {
                if (value != FADE_NONE && value != FADE_ACTIONS && value != FADE_CONTENT && value != FADE_BOTH)
                {
                    return;
                }

                mFadeType = value;
                UpdateScrollFactor();
            }
            get
            {
                return mFadeType;
            }
        }


        public virtual int FadeValue
        {
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 255)
                {
                    value = 255;
                }

                mFadeValue = value;
                UpdateScrollFactor();
            }
            get
            {
                return mFadeValue;
            }
        }


        public virtual bool SwipingEnabled
        {
            get
            {
                return isSwipingEnabled;
            }
            set
            {
                isSwipingEnabled = value;
            }
        }


        public virtual int SwipingType
        {
            set
            {
                if (value != SWIPING_ALL && value != SWIPING_EDGE)
                {
                    return;
                }

                mSwipeType = value;
            }
            get
            {
                return mSwipeType;
            }
        }


        public virtual int SwipingEdgeWidth
        {
            set
            {
                mSwipeEdgeWidth = value;
            }
            get
            {
                return mSwipeEdgeWidth;
            }
        }


        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (!isSwipingEnabled)
            {
                return false;
            }

            mGestureDetector.OnTouchEvent(ev);

            MotionEventActions action = ev.Action;
            // if current touch event should be handled
            if (mScrollController.Handled == true)
            {
                if (action == MotionEventActions.Up)
                {
                    mScrollController.OnUp(ev);
                }
                return true;
            }

            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (!isSwipingEnabled)
            {
                return false;
            }

            mGestureDetector.OnTouchEvent(ev);

            // whether we should handle all following events by our view
            // and don't allow children to get them
            return mScrollController.Handled.Value;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int width = MeasureSpec.GetSize(widthMeasureSpec);
            int height = MeasureSpec.GetSize(heightMeasureSpec);
            
            int childrenCount = ChildCount;
            for (int i = 0; i < childrenCount; ++i)
            {
                View v = GetChildAt(i);
                if (v == viewActionsContainer)
                {
                    // Setting size of actions according to spacing parameters
                    if (mSpacingType == SPACING_ACTIONS_WIDTH)
                    {
                        viewActionsContainer.Measure(MeasureSpec.MakeMeasureSpec(mSpacing, MeasureSpecMode.Exactly), heightMeasureSpec);
                    }
                    // All other situations are handled as SPACING_RIGHT_OFFSET
                    else
                    {
                        viewActionsContainer.Measure(MeasureSpec.MakeMeasureSpec(width - mSpacing, MeasureSpecMode.Exactly), heightMeasureSpec);
                    }
                }
                else if (v == viewContentContainer)
                {
                    int shadowWidth = ShadowVisible ? mShadowWidth : 0;
                    int contentWidth = MeasureSpec.GetSize(widthMeasureSpec) - mActionsSpacing + shadowWidth;
                    v.Measure(MeasureSpec.MakeMeasureSpec(contentWidth, MeasureSpecMode.Exactly), heightMeasureSpec);
                }
                else
                {
                    v.Measure(widthMeasureSpec, heightMeasureSpec);
                }
            }

            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            // Putting every child view to top-left corner
            int childrenCount = ChildCount;
            for (int i = 0; i < childrenCount; ++i)
            {
                View v = GetChildAt(i);
                if (v == viewContentContainer)
                {
                    int shadowWidth = ShadowVisible ? mShadowWidth : 0;
                    v.Layout(mActionsSpacing - shadowWidth, 0, mActionsSpacing + v.MeasuredWidth, v.MeasuredHeight);
                }
                else
                {
                    v.Layout(0, 0, v.MeasuredWidth, v.MeasuredHeight);
                }
            }

            if (mForceRefresh)
            {
                mForceRefresh = false;
                mScrollController.Init();
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            // Set correct position of content view after view size was changed
            if (w != oldw || h != oldh)
            {
                mScrollController.Init();
            }
        }

        private void UpdateScrollFactor()
        {
            if (viewActionsContainer == null || viewContentContainer == null)
            {
                return;
            }

            float scrollFactor = mScrollController.ScrollFactor;
            bool isOpening = mScrollController.Opening;
            bool enableEffects = mScrollController.IsEffectsEnabled;

            int actionsFadeFactor;
            if ((mFadeType & FADE_ACTIONS) == FADE_ACTIONS)
            {
                actionsFadeFactor = (int)(scrollFactor * mFadeValue);
            }
            else
            {
                actionsFadeFactor = 0;
            }
            viewActionsContainer.Controller.OnScroll(scrollFactor, actionsFadeFactor, isOpening, enableEffects);

            int contentFadeFactor;
            if ((mFadeType & FADE_CONTENT) == FADE_CONTENT)
            {
                contentFadeFactor = (int)((1f - scrollFactor) * mFadeValue);
            }
            else
            {
                contentFadeFactor = 0;
            }
            viewContentContainer.Controller.OnScroll(1f - scrollFactor, contentFadeFactor, isOpening, enableEffects);
        }

        public class SavedState : BaseSavedState
        {
            /// <summary>
            /// Indicates whether content was shown while saving state.
            /// </summary>
            internal bool isContentShown;

            /// <summary>
            /// Spacing type.
            /// </summary>
            internal int mSpacingType = SPACING_RIGHT_OFFSET;
            /// <summary>
            /// Value of spacing to use.
            /// </summary>
            internal int mSpacing;

            /// <summary>
            /// Value of actions container spacing to use.
            /// </summary>
            internal int mActionsSpacing;

            /// <summary>
            /// Indicates whether shadow is visible.
            /// </summary>
            internal bool isShadowVisible;

            /// <summary>
            /// Value of shadow width.
            /// </summary>
            internal int mShadowWidth = 0;

            /// <summary>
            /// Indicates whether swiping is enabled or not.
            /// </summary>
            internal bool isSwipingEnabled = true;

            /// <summary>
            /// Indicates how long flinging will take time in milliseconds.
            /// </summary>
            internal int mFlingDuration = 250;

            /// <summary>
            /// Fade type.
            /// </summary>
            internal int mFadeType = FADE_NONE;
            /// <summary>
            /// Max fade value.
            /// </summary>
            internal int mFadeValue;

            /// <summary>
            /// Swiping type.
            /// </summary>
            internal int mSwipeType = FADE_NONE;
            /// <summary>
            /// Swiping edge width.
            /// </summary>
            internal int mSwipeEdgeWidth;
            
            public SavedState(IParcelable superState)
                : base(superState)
            {
            }

            public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
            {
                base.WriteToParcel(dest, flags);

                dest.WriteInt(isContentShown ? 1 : 0);
                dest.WriteInt(mSpacingType);
                dest.WriteInt(mSpacing);
                dest.WriteInt(mActionsSpacing);
                dest.WriteInt(isShadowVisible ? 1 : 0);
                dest.WriteInt(mShadowWidth);
                dest.WriteInt(isSwipingEnabled ? 1 : 0);
                dest.WriteInt(mFlingDuration);
                dest.WriteInt(mFadeType);
                dest.WriteInt(mFadeValue);
                dest.WriteInt(mSwipeType);
                dest.WriteInt(mSwipeEdgeWidth);
            }

            [ExportField("CREATOR")]
            static SavedStateCreator InitializeCreator()
            {
                return new SavedStateCreator();
            }

            class SavedStateCreator : Java.Lang.Object, IParcelableCreator
            {
                public Java.Lang.Object CreateFromParcel(Parcel source)
                {
                    return new SavedState(source);
                }

                public Java.Lang.Object[] NewArray(int size)
                {
                    return new SavedState[size];
                }
            }

            //// The creator creates an instance of the specified object
            //private static readonly GenericParcelableCreator<SavedState> _creator
            //    = new GenericParcelableCreator<SavedState>((parcel) => new SavedState(parcel));

            //[ExportField("CREATOR")]
            //public static GenericParcelableCreator<SavedState> GetCreator()
            //{
            //    return _creator;
            //}

            //public sealed class GenericParcelableCreator<T> : Java.Lang.Object, IParcelableCreator
            //    where T : Java.Lang.Object, new()
            //{
            //    private readonly Func<Parcel, T> _createFunc;

            //    /// <summary>
            //    /// Initializes a new instance of the <see cref="ParcelableDemo.GenericParcelableCreator`1"/> class.
            //    /// </summary>
            //    /// <param name='createFromParcelFunc'>
            //    /// Func that creates an instance of T, populated with the values from the parcel parameter
            //    /// </param>
            //    public GenericParcelableCreator(Func<Parcel, T> createFromParcelFunc)
            //    {
            //        _createFunc = createFromParcelFunc;
            //    }

            //    public Java.Lang.Object CreateFromParcel(Parcel source)
            //    {
            //        return _createFunc(source);
            //    }

            //    public Java.Lang.Object[] NewArray(int size)
            //    {
            //        return new T[size];
            //    }
            //} 

            internal SavedState(Parcel parcel)
                : base(parcel)
            {

                isContentShown = parcel.ReadInt() == 1;
                mSpacingType = parcel.ReadInt();
                mSpacing = parcel.ReadInt();
                mActionsSpacing = parcel.ReadInt();
                isShadowVisible = parcel.ReadInt() == 1;
                mShadowWidth = parcel.ReadInt();
                isSwipingEnabled = parcel.ReadInt() == 1;
                mFlingDuration = parcel.ReadInt();
                mFadeType = parcel.ReadInt();
                mFadeValue = parcel.ReadInt();
                mSwipeType = parcel.ReadInt();
                mSwipeEdgeWidth = parcel.ReadInt();
            }
        }        

        /// <summary>
        /// Used to handle scrolling events and scroll content container on top of actions one.
        /// </summary>
        private class ContentScrollController : Java.Lang.Object, GestureDetector.IOnGestureListener, IRunnable
        {
            private readonly ActionsContentView view;

            /// <summary>
            /// Used to auto-scroll to closest bound on touch up event.
            /// </summary>
            internal readonly Scroller mScroller;
            /// <summary>
            /// Used to fling to after fling touch event.
            /// </summary>
            internal readonly Scroller mEffectsScroller;

            // using Boolean object to initialize while first scroll event
            internal bool? mHandleEvent = null;

            /// <summary>
            /// Indicates whether we need initialize position of view after measuring is finished.
            /// </summary>
            internal bool isContentShown = true;

            internal bool isFlinging = false;

            internal bool isEffectsEnabled = false;

            public ContentScrollController(ActionsContentView outerInstance, Scroller scroller, Scroller effectsScroller)
            {
                this.view = outerInstance;
                mScroller = scroller;
                mEffectsScroller = effectsScroller;
            }

            /// <summary>
            /// Initializes visibility of content after views measuring is finished.
            /// </summary>
            public virtual void Init()
            {
                if (DEBUG)
                {
                    Console.WriteLine(TAG, "Scroller: init");
                }

                if (isContentShown)
                {
                    ShowContent(0);
                }
                else
                {
                    HideContent(0);
                }

                view.UpdateScrollFactor();
            }

            /// <summary>
            /// Returns handling lock value. It indicates whether all events
            /// should be marked as handled.
            /// @return
            /// </summary>
            public virtual bool? Handled
            {
                get
                {
                    return ((mHandleEvent != null) && (mHandleEvent == true));
                }
            }

            public bool IsSwipeFinished
            {
                get
                {
                    if (!mScroller.IsFinished)
                    {
                        return false;
                    }

                    if (!mEffectsScroller.IsFinished)
                    {
                        return false;
                    }

                    int x = view.viewContentContainer.ScrollX;
                    if (view.IsContentShown && x != 0)
                    {
                        return false;
                    }

                    if (!view.IsContentShown && x != -RightBound)
                    {
                        return false;
                    }

                    return true;
                }
            }
                
            public virtual bool Opening
            {
                get
                {
                    if (!mScroller.IsFinished)
                    {
                        return mScroller.StartX > mScroller.FinalX;
                    }

                    if (!mEffectsScroller.IsFinished)
                    {
                        return mEffectsScroller.StartX > mEffectsScroller.FinalX;
                    }

                    return !isContentShown;
                }
            }

            public virtual bool IsEffectsEnabled
            {
                get
                {
                    return isEffectsEnabled;
                }
            }

            public bool OnDown(MotionEvent e)
            {
                mHandleEvent = null;
                Reset();
                return false;
            }

            public virtual bool OnUp(MotionEvent e)
            {
                if (IsSwipeFinished)
                {
                    return false;
                }

                mHandleEvent = null;
                CompleteScrolling();
                return true;
            }

            public bool OnSingleTapUp(MotionEvent e)
            {
                return false;
            }

            public void OnShowPress(MotionEvent e)
            {
                // No-op
            }

            public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
            {

                isFlinging = false;
                isEffectsEnabled = false;

                Reset();

                // If there is first scroll event after touch down
                if (mHandleEvent == null)
                {
                    if (System.Math.Abs(distanceX) < System.Math.Abs(distanceY))
                    {
                        // If first event is more scroll by Y axis than X one
                        // Ignore all events until event up
                        mHandleEvent = false;
                    }
                    else
                    {
                        int contentLeftBound = view.viewContentContainer.Left - view.viewContentContainer.ScrollX + view.mShadowWidth;
                        int firstTouchX = (int)e1.RawX;

                        if (DEBUG)
                        {
                            Console.WriteLine(TAG, "Scroller: first touch: " + firstTouchX + ", " + e1.GetY());
                            Console.WriteLine(TAG, "Content left bound: " + contentLeftBound);
                        }

                        // If content is not shown we handle all horizontal swipes
                        // If content shown and there is edge mode we should check start swiping area first
                        if (view.mSwipeType == SWIPING_ALL || (IsContentShown && firstTouchX <= view.mSwipeEdgeWidth || (!IsContentShown && firstTouchX >= contentLeftBound)))
                        {
                            // handle all events of scrolling by X axis
                            mHandleEvent = true;
                            ScrollBy((int)distanceX);
                        }
                        else
                        {
                            mHandleEvent = false;
                        }
                    }
                }
                else if (mHandleEvent == true)
                {
                    // It is not first event we should handle as scrolling by X axis
                    ScrollBy((int)distanceX);
                }

                return mHandleEvent.Value;
            }

            public void OnLongPress(MotionEvent e)
            {
                // No-op
            }

            public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
            {

                if (mHandleEvent == false)
                {
                    return false;
                }

                float absVelocityX = System.Math.Abs(velocityX);
                if (absVelocityX <= System.Math.Abs(velocityY))
                {
                    return false;
                }

                if (absVelocityX < FLING_MIN)
                {
                    return false;
                }

                isFlinging = true;

                if (velocityX < 0)
                {
                    ShowContent(view.mFlingDuration);
                }
                else
                {
                    HideContent(view.mFlingDuration);
                }

                return true;
            }

            public virtual bool IsContentShown
            {
                get
                {
                    return isContentShown;
                }
            }

            public virtual void HideContent(int duration)
            {
                if (DEBUG)
                {
                    Console.WriteLine(TAG, "Scroller: hide content by " + duration + "ms");
                }

                isContentShown = false;
                if (view.viewContentContainer.MeasuredWidth == 0 || view.viewContentContainer.MeasuredHeight == 0)
                {
                    return;
                }

                Scroll(false, duration);
            }

            public virtual void ShowContent(int duration)
            {
                if (DEBUG)
                {
                    Console.WriteLine(TAG, "Scroller: show content by " + duration + "ms");
                }

                isContentShown = true;
                if (view.viewContentContainer.MeasuredWidth == 0 || view.viewContentContainer.MeasuredHeight == 0)
                {
                    return;
                }

                Scroll(true, duration);
            }

            public virtual float ScrollFactor
            {
                get
                {
                    return 1f + (float)view.viewContentContainer.ScrollX / (float)RightBound;
                }
            }

            /// <summary>
            /// Resets scroller controller. Stops flinging on current position.
            /// </summary>
            public virtual void Reset()
            {
                if (DEBUG)
                {
                    Console.WriteLine(TAG, "Scroller: reset");
                }

                if (!mScroller.IsFinished)
                {
                    mScroller.ForceFinished(true);
                }
                if (!mEffectsScroller.IsFinished)
                {
                    mEffectsScroller.ForceFinished(true);
                }
            }

            /// <summary>
            /// Starts auto-scrolling to bound which is closer to current position.
            /// </summary>
            internal virtual void CompleteScrolling()
            {
                // Preventing override of fling effect
                if (!mScroller.IsFinished || !mEffectsScroller.IsFinished)
                {
                    return;
                }

                int startX = view.viewContentContainer.ScrollX;

                int rightBound = RightBound;
                int middle = -rightBound / 2;
                if (startX > middle)
                {
                    ShowContent(view.mFlingDuration);
                }
                else
                {
                    HideContent(view.mFlingDuration);
                }
            }

            internal virtual void Scroll(bool showContent, int duration)
            {
                Reset();

                int startX = view.viewContentContainer.ScrollX;
                int dx = showContent ? -startX : -RightBound - startX;
                if (DEBUG)
                {
                    Console.WriteLine(TAG, "start scroller at " + startX + " for " + dx + " by " + duration);
                }

                if (duration <= 0)
                {
                    view.viewContentContainer.ScrollBy(dx, 0);
                    return;
                }

                isEffectsEnabled = StartEffects(dx < 0, isFlinging);
                if (isEffectsEnabled)
                {
                    mEffectsScroller.StartScroll(startX, 0, dx, 0, duration);
                }
                else
                {
                    mScroller.StartScroll(startX, 0, dx, 0, duration);
                }

                view.viewContentContainer.Post(this);
            }
            
            // Scrolling content view according by given value.
            internal virtual void ScrollBy(int dx)
            {
                int x = view.viewContentContainer.ScrollX;

                isEffectsEnabled = StartEffects(!isContentShown, false);

                int scrollBy;

                // Scrolling right
                if (dx < 0) 
                {
                    int rightBound = RightBound;
                    if (x + dx < -rightBound)
                    {
                        scrollBy = -rightBound - x;
                    }
                    else
                    {
                        scrollBy = dx;
                    }
                }
                // Scrolling left
                else
                {
                    // Don't scroll if we are at left bound
                    if (x == 0)
                    {
                        return;
                    }

                    if (x + dx > 0)
                    {
                        scrollBy = -x;
                    }
                    else
                    {
                        scrollBy = dx;
                    }
                }

                if (DEBUG)
                {
                    Console.WriteLine(TAG, "scroll from " + x + " by " + dx + " [" + scrollBy + "]");
                }

                view.viewContentContainer.ScrollBy(scrollBy, 0);
            }

            // Processes auto-scrolling to bound which is closer to current position.            
            public void Run()
            {
                Scroller scroller = IsEffectsEnabled ? mEffectsScroller : mScroller;
                if (scroller.IsFinished)
                {
                    if (DEBUG)
                    {
                        Console.WriteLine(TAG, "scroller is finished, done with fling");
                    }
                    if (view.MOnActionsContentListener != null)
                    {
                        view.MOnActionsContentListener.OnContentStateChanged(view, isContentShown);
                    }
                    return;
                }

                bool more = scroller.ComputeScrollOffset();
                int x = scroller.CurrX;
                view.viewContentContainer.ScrollTo(x, 0);

                if (more)
                {
                    view.viewContentContainer.Post(this);
                }
                else
                {
                    if (view.MOnActionsContentListener != null)
                    {
                        view.MOnActionsContentListener.OnContentStateChanged(view, isContentShown);
                    }
                }
            }

            internal virtual int RightBound
            {
                get
                {
                    if (view.mSpacingType == SPACING_ACTIONS_WIDTH)
                    {
                        return view.mSpacing - view.mActionsSpacing;
                    } // all other situations are handled as SPACING_RIGHT_OFFSET
                    else
                    {
                        return view.Width - view.mSpacing - view.mActionsSpacing;
                    }
                }
            }

            internal virtual bool StartEffects(bool isOpening, bool isFlinging)
            {
                bool enableEffects;

                if (view.mEffects == EFFECTS_NONE)
                {
                    enableEffects = false;
                }
                else if (!isFlinging && (view.mEffects & EFFECTS_SCROLL) > 0)
                {
                    if (isOpening && (view.mEffects & EFFECTS_SCROLL_OPENING) == EFFECTS_SCROLL_OPENING)
                    {
                        enableEffects = true;
                    }
                    else if (!isOpening && (view.mEffects & EFFECTS_SCROLL_CLOSING) == EFFECTS_SCROLL_CLOSING)
                    {
                        enableEffects = true;
                    }
                    else
                    {
                        enableEffects = false;
                    }
                }
                else if (isFlinging && (view.mEffects & EFFECTS_FLING) > 0)
                {
                    if (isOpening && (view.mEffects & EFFECTS_FLING_OPENING) == EFFECTS_FLING_OPENING)
                    {
                        enableEffects = true;
                    }
                    else if (!isOpening && (view.mEffects & EFFECTS_FLING_CLOSING) == EFFECTS_FLING_CLOSING)
                    {
                        enableEffects = true;
                    }
                    else
                    {
                        enableEffects = false;
                    }
                }
                else
                {
                    enableEffects = false;
                }

                return enableEffects;
            }
        }
    }
}