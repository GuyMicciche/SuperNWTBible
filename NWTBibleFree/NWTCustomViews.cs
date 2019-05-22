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
using Android.Preferences;

namespace NWTBibleFree
{
    public class OptionDialogPreference : DialogPreference
    {
        private Context context;

        public OptionDialogPreference(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.context = context;
        }

        protected override void OnDialogClosed(bool positiveResult)
        {
            base.OnDialogClosed(positiveResult);
        }

        protected override void OnPrepareDialogBuilder(AlertDialog.Builder builder)
        {
            base.OnPrepareDialogBuilder(builder);

            builder.SetIcon(Resource.Drawable.Icon);
            builder.SetPositiveButton("Google Wallet", pos_Click);
            builder.SetNegativeButton("PayPal", neg_Click);
        }

        void neg_Click(object sender, EventArgs e)
        {
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=J3WJ7MMYPV8R8"));
            context.StartActivity(browserIntent);
        }

        void pos_Click(object sender, EventArgs e)
        {
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://dl.dropbox.com/u/826238/donate.html"));
            context.StartActivity(browserIntent);
        }
    }

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