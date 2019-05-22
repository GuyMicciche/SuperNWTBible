using Android.Graphics;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Content;

namespace ActionsContentView
{
    public class ActionsLayout : FrameLayout
    {
        private BaseContainerController mController;

        private readonly Paint mFadePaint = new Paint();

        public ActionsLayout(Context context)
            : this(context, null)
        {
            if (mController == null)
            {
                mController = new BaseContainerController(this);
            }
        }

        public ActionsLayout(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
            if (mController == null)
            {
                mController = new BaseContainerController(this);
            }
        }

        public ActionsLayout(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            if (mController == null)
            {
                mController = new BaseContainerController(this);
            }
        }

        public virtual BaseContainerController Controller
        {
            get
            {
                return mController;
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            return !mController.IsIgnoringTouchEvents && base.OnTouchEvent(e);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            mController.InitializeEffects();
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
            if (fadeFactor > 0)
            {
                mFadePaint.Color = Color.Argb(fadeFactor, 0, 0, 0);
                canvas.DrawRect(0, 0, Width, Height, mFadePaint);
            }

            canvas.RestoreToCount(saveCount);
        }
    }
}