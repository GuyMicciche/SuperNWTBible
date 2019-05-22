using Android.Annotation;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ActionsContentView
{
    internal class ContentLayout : LinearLayout
    {
        public interface IOnSwipeListener
        {
            void OnSwipe(int scrollPosition);
        }
        
        private BaseContainerController mController;

        private readonly Rect mHitRect = new Rect();
        private readonly RectF mEffectedHitRect = new RectF();
        private readonly Paint mFadePaint = new Paint();

        private IOnSwipeListener mOnSwipeListener;

        public ContentLayout(Context context)
            : base(context)
        {
        }

        public ContentLayout(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            if(mController == null)
            {
                mController = new BaseContainerController(this);
            }

            // We need to be sure we have horizontal layout to add shadow to left border
            Orientation = Orientation.Horizontal;
        }

        [TargetApi(Value = (int)BuildVersionCodes.Honeycomb)]
        public ContentLayout(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            if (mController == null)
            {
                mController = new BaseContainerController(this);
            }

            // We need to be sure we have horizontal layout to add shadow to left border
            Orientation = Orientation.Horizontal;
        }

        public virtual BaseContainerController Controller
        {
            get
            {
                return mController;
            }
        }

        public virtual IOnSwipeListener OnSwipeListener
        {
            set
            {
                mOnSwipeListener = value;
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            mController.InitializeEffects();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (mController.IsIgnoringTouchEvents)
            {
                return false;
            }

            // Prevent ray cast of touch events to actions container
            GetHitRect(mHitRect);
            mHitRect.Offset(-ScrollX, -ScrollY);

            // Applying effects
            mEffectedHitRect.Set(mHitRect);
            mController.EffectsMatrix.MapRect(mEffectedHitRect);

            if (mEffectedHitRect.Contains((int)e.GetX(), (int)e.GetY()))
            {
                return true;
            }

            return base.OnTouchEvent(e);
        }

        protected override void OnScrollChanged(int l, int t, int oldl, int oldt)
        {
            base.OnScrollChanged(l, t, oldl, oldt);
            if (mOnSwipeListener != null)
            {
                mOnSwipeListener.OnSwipe(-ScrollX);
            }
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            int saveCount = canvas.Save();

            Matrix m = mController.EffectsMatrix;
            if (!m.IsIdentity)
            {
                canvas.Concat(m);
            }

            float alpha = mController.EffectsAlpha;
            if (alpha != 1f)
            {
                canvas.SaveLayerAlpha(0, 0, canvas.Width, canvas.Height, (int)(255 * alpha), SaveFlags.HasAlphaLayer);
            }

            base.DispatchDraw(canvas);

            int fadeFactor = mController.FadeFactor;
            if (fadeFactor > 0f)
            {
                mFadePaint.Color = Color.Argb(fadeFactor, 0, 0, 0);
                canvas.DrawRect(0, 0, Width, Height, mFadePaint);
            }

            canvas.RestoreToCount(saveCount);
        }
    }
}