using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;

using Java.Lang;
using Java.Lang.Reflect;

using System;
using System.Collections.Generic;

namespace ActionsContentView
{
    public class EffectsController
    {
        private static string TAG = Java.Lang.Class.FromType(typeof(EffectsController)).SimpleName;
        private static bool DEBUG = false;

        private static Method APPLY_TRANSFORMATION;

        public const int EFFECT_OPEN = 0;
        public const int EFFECT_CLOSE = 1;

        private const int EFFECTS_COUNT = 2;

        static EffectsController()
        {
            APPLY_TRANSFORMATION = ApplyTransformation;
        }

        private Transformation mTransformation = new Transformation();
        private Matrix mMatrix = new Matrix();
        private float mEffectsAlpha = 1f;

        private Effect[] mEffects = new Effect[EFFECTS_COUNT];

        public void SetEffects(Context context, int resId)
        {
            string resourceType = context.Resources.GetResourceTypeName(resId);

            if (!resourceType.Equals("array"))
            {
                Animation anim = AnimationUtils.LoadAnimation(context, resId);
                SetEffects(anim);

                return;
            }

            TypedArray effects = context.Resources.ObtainTypedArray(resId);

            int count = effects.Length();
            int size = System.Math.Min(EFFECTS_COUNT, count);
            for (int i = 0; i < size; ++i)
            {
                int id = effects.GetResourceId(i, -1);
                if (id > 0)
                {
                    Animation anim = AnimationUtils.LoadAnimation(context, id);
                    mEffects[i] = new Effect(anim);
                }
            }
            effects.Recycle();
        }

        public void SetEffects(Animation animation)
        {
            mEffects[0] = mEffects[1] = new Effect(animation);
        }

        public Effect[] GetEffects()
        {
            return mEffects;
        }

        public void Initialize(View v)
        {
            if (mEffects == null)
            {
                return;
            }

            ViewGroup parent = (ViewGroup)v.Parent;
            if (parent != null)
            {
                foreach (Effect effect in mEffects)
                {
                    if (effect == null)
                    {
                        continue;
                    }
                    effect.Anim.Initialize(v.Width, v.Height, parent.Width, parent.Height);
                }
            }
        }

        public Matrix EffectsMatrix
        {
            get
            {
                return mMatrix;
            }
        }

        public float EffectsAlpha
        {
            get
            {
                return mEffectsAlpha;
            }
        }

        public void Reset()
        {
            mMatrix.Reset();
            mEffectsAlpha = 1f;
        }

        public virtual bool Apply(float factor, int effectType)
        {
            if (mEffects == null)
            {
                return false;
            }

            Reset();

            Effect effect = mEffects[effectType];
            if (effect == null)
            {
                return false;
            }

            Animation anim = effect.Anim;
            long totalTime = effect.TotalTime;

            if (anim is AnimationSet)
            {
                return Apply(factor, (AnimationSet)anim, totalTime);
            }
            else
            {
                return Apply(factor, anim, totalTime);
            }
        }

        private bool Apply(float factor, Animation animation, long totalTime)
        {
            float animationFactor;

            long animationDuration = animation.Duration;
            if (animationDuration == 0 || totalTime == 0)
            {
                animationFactor = factor;
            }
            else
            {
                long effectTime = (int)(totalTime * factor);

                long animationStartOffset = animation.StartOffset;
                long animationEndTime = animationStartOffset + animationDuration;

                if (effectTime < animationStartOffset || effectTime > animationEndTime)
                {
                    return true;
                }

                animationFactor = (float)(effectTime - animationStartOffset) / (float)animationDuration;
            }

            try
            {
                // We need reset transformation for every animation
                mTransformation.Clear();

                float interpolatedFactor = animation.Interpolator.GetInterpolation(animationFactor);
                Java.Lang.Object[] args = new Java.Lang.Object[] { interpolatedFactor, mTransformation };
                APPLY_TRANSFORMATION.Invoke(animation, args);
                if ((mTransformation.TransformationType & TransformationTypes.Matrix) == TransformationTypes.Matrix)
                {
                    mMatrix.PostConcat(mTransformation.Matrix);
                }
                if ((mTransformation.TransformationType & TransformationTypes.Alpha) == TransformationTypes.Alpha)
                {
                    mEffectsAlpha *= mTransformation.Alpha;
                }

                if (DEBUG)
                {
                    Console.WriteLine(TAG, "Transformation: " + animation);
                    Console.WriteLine(TAG, " - " + mTransformation.ToShortString());
                }

                return true;
            }
            catch (IllegalArgumentException e)
            {
                // we don't care because this exception should never happen
            }
            catch (IllegalAccessException e)
            {
                // we don't care because this exception should never happen
            }
            catch (InvocationTargetException e)
            {
                // we don't care because this exception should never happen
            }

            return false;
        }

        private bool Apply(float factor, AnimationSet set, long totalTime)
        {
            IList<Animation> animations = set.Animations;
            foreach (Animation a in animations)
            {
                if (a is AnimationSet)
                {
                    if (!Apply(factor, (AnimationSet)a, totalTime))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!Apply(factor, a, totalTime))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static Method ApplyTransformation
        {
            get
            {
                try
                {
                    Method m = Java.Lang.Class.FromType(typeof(Animation)).GetMethod("applyTransformation", Java.Lang.Class.FromType(typeof(Transformation)).Class);
                    m.Accessible = true;

                    return m;
                }
                catch (NoSuchMethodException e)
                {
                    // This exception should never happen
                }
                return null;
            }
        }

        public class Effect
        {
            public Animation Anim;
            public long TotalTime;

            public Effect(Animation anim)
            {
                this.Anim = anim;
                TotalTime = anim.ComputeDurationHint();
            }
        }
    }
}