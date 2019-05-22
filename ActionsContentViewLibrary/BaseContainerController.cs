using Android.Views;
using Android.Views.Animations;
using Android.Graphics;

using Effect = ActionsContentView.EffectsController.Effect;

namespace ActionsContentView
{
    public class BaseContainerController : IContainerController
    {

        private readonly View view;
        private readonly EffectsController mEffectsController = new EffectsController();

        private bool mIgnoreTouchEvents = false;
        private int mFadeFactor = 0;

        public BaseContainerController(View view)
        {
            this.view = view;
        }

        public bool IgnoreTouchEvents
        {
            set
            {
                mIgnoreTouchEvents = value;
            }
        }

        public bool IsIgnoringTouchEvents
        {
            get
            {
                return mIgnoreTouchEvents;
            }
        }

        public void InitializeEffects()
        {
            mEffectsController.Initialize(view);
        }

        public Matrix EffectsMatrix
        {
            get
            {
                return mEffectsController.EffectsMatrix;
            }
        }

        public float EffectsAlpha
        {
            get
            {
                return mEffectsController.EffectsAlpha;
            }
        }

        public void SetEffects(Animation effects)
        {
            mEffectsController.SetEffects(effects);
        }

        public void SetEffects(int resId)
        {
            mEffectsController.SetEffects(view.Context, resId);
        }

        public Effect[] GetEffects()
        {
            return mEffectsController.GetEffects();
        }

        public int FadeFactor
        {
            get
            {
                return mFadeFactor;
            }
        }

        public virtual void OnScroll(float factor, int fadeFactor, bool isOpening, bool enableEffects)
        {
            mFadeFactor = fadeFactor;

            bool updateEffects;
            if (enableEffects)
            {
                updateEffects = mEffectsController.Apply(factor, isOpening ? EffectsController.EFFECT_OPEN : EffectsController.EFFECT_CLOSE);
            }
            else
            {
                mEffectsController.Reset();
                updateEffects = false;
            }

            if (updateEffects || mFadeFactor > 0)
            {
                view.PostInvalidate();
            }
        }
    }
}