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
using Xamarin.Parse;
using Xamarin.ActionbarSherlockBinding.App;
using Android.Preferences;

namespace NWTBible
{
    [Activity(Label = "Log In", Theme = "@style/Theme.Sherlock")]
    public class LogInActivity : SherlockActivity
    {
        Button Loginbutton;
        Button Signup;
        string Usernametxt;
        string Passwordtxt;
        EditText Password;
        EditText Username;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(PreferenceManager.GetDefaultSharedPreferences(ApplicationContext).GetInt("ThemeStyle", Resource.Style.Theme_Sherlock));

            SetContentView(Resource.Layout.LogIn);

            Username = (EditText)FindViewById(Resource.Id.username);
            Password = (EditText)FindViewById(Resource.Id.password);

            Loginbutton = (Button)FindViewById(Resource.Id.login);
            Signup = (Button)FindViewById(Resource.Id.signup);

            Loginbutton.Click += Loginbutton_Click;
            Signup.Click += Signup_Click;

            Parse.Initialize(this, "scBTJphDK8yVGGtNhcL9cYee89GbEKuRkygGYXKa", "wlXg6dWeJBCxD3uNbnoCTnnZlpSvvZWOdfyoeREZ");
        }

        void Signup_Click(object sender, EventArgs e)
        {
            // Retrieve the text entered from the EditText
            Usernametxt = Username.Text;
            Passwordtxt = Password.Text;

            // Force user to fill up the form
            if (string.IsNullOrEmpty(Usernametxt) || string.IsNullOrEmpty(Passwordtxt))
            {
                ThisApp.AlertBox(this, "REMINDER!", "Please complete the sign up form.");
            }
            else
            {
                // Save new user data into Parse.com Data Storage
                ParseUser user = new ParseUser();
                user.Username = Usernametxt;
                user.SetPassword(Passwordtxt);
                user.SignUpInBackground(new MySignUpCallback(this));
            }
        }

        void Loginbutton_Click(object sender, EventArgs e)
        {
            Usernametxt = Username.Text;
            Passwordtxt = Password.Text;

            if (string.IsNullOrEmpty(Usernametxt) || string.IsNullOrEmpty(Passwordtxt))
            {
                ThisApp.AlertBox(this, "REMINDER!", "Please enter your credentials.");
            }
            else
            {
                ParseUser.LogInInBackground(Usernametxt, Passwordtxt, new MyLogInCallback(this));
            }
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class MySignUpCallback : SignUpCallback
    {
        private Activity _context;

        public MySignUpCallback(Activity context)
		{
            this._context = context;
		}

        public override void Done(ParseException e)
        {
            if (e == null)
            {
                // Show a simple Toast message upon successful registration
                ThisApp.AlertBox(_context, "SUCCESS!", "Successfully signed up. Please log in.");
            }
            else if (e.Code == ParseException.UsernameTaken)
            {
                ThisApp.AlertBox(_context, "REMINDER!", "Username taken.");
            }
            else
            {
                ThisApp.AlertBox(_context, "REMINDER!", "There was an error.\n\n" + e.Message);
            }
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }

    public class MyLogInCallback : LogInCallback
    {
        private Activity _context;

        public MyLogInCallback(Activity context)
		{
            this._context = context;
		}

        public override void Done(ParseUser user, ParseException e)
        {
            if (e == null)
            {
                // If user exist and authenticated
                //ThisApp.AlertBox(_context, "SUCCESS!", "Successfully logged in!");
                Toast.MakeText(_context, "Successfully logged in as " + ParseUser.CurrentUser.Username + "! You can now sync your notes!", ToastLength.Long).Show();
                _context.Finish();
            }
            else if (e.Code == ParseException.ObjectNotFound)
            {
                ThisApp.AlertBox(_context, "REMINDER!", "Incorrect username or password. Please try again.");
            }
            else
            {
                ThisApp.AlertBox(_context, "REMINDER!", "Unknown error. Check your connections.\n\n" + e.Message);
            }
        }

        public App ThisApp
        {
            get
            {
                return App.Instance;
            }
        }
    }
}