using Android.Views.Animations;

using Effect = ActionsContentView.EffectsController.Effect;

namespace ActionsContentView
{
    public interface IContainerController
    {
        /// <summary>
        /// Setter for ignore touch events key.
        /// </summary>
        bool IgnoreTouchEvents { set; }

        /// <summary>
        /// Getter for ignore touch events key.
        /// </summary>
        bool IsIgnoringTouchEvents { get; }

        /// <summary>
        /// Setter for effects.
        /// </summary>
        void SetEffects(int resId);

        /// <summary>
        /// Setter for effects.
        /// </summary>
        void SetEffects(Animation effects);

        /// <summary>
        /// Getter for effects.
        /// </summary>
        Effect[] GetEffects();
    }
}