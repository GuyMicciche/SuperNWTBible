using Android.App;
using Android.Content.PM;
using Android.OS;
using NWTBible.ReaderMenu;

namespace NWTBible
{
    [Activity(MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, ConfigurationChanges = ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            StartActivity(typeof(MainActivity));

            //if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Honeycomb)
            //{
            //    StartActivity(typeof(MainActivity));
            //}
            //else
            //{
            //    StartActivity(typeof(HomeActivity));
            //}
        }        
    }
}