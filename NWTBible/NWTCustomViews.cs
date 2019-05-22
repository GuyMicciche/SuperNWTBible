using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;

namespace NWTBible
{
    public class IonTextView : TextView
    {
        private Context context;
        private string ttfName;

        public IonTextView(Context context, IAttributeSet attrs) 
            : base(context, attrs)
        {
            this.context = context;

            for (int i = 0; i < attrs.AttributeCount; i++) 
            {
                this.ttfName = attrs.GetAttributeValue("http://schemas.android.com/apk/res/com.gem.nwtbible", "ttf");

                init();
            }
        }

        private void init() 
        {
            Typeface font = Typeface.CreateFromAsset(context.Assets, ttfName);
            SetTypeface(font, Android.Graphics.TypefaceStyle.Normal);
        }

        public override void SetTypeface(Typeface tf, TypefaceStyle style)
        {
 	         base.SetTypeface(tf, style);
        }
    }
}