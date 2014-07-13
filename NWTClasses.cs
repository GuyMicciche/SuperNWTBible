using Android.App;
using Android.Preferences;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace NWTBible
{

    public static class StringExtensions
    {
        ///summary>
        /// Converts the string case to Title Case, where the first letter of each word is capitalized.
        /// /summary>
        public static string ToTitleCase(this string inputString)
        {
            System.Globalization.TextInfo txtInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            return txtInfo.ToTitleCase(inputString);
        }
    }

    public static class ReaderKind
    {
        public static string BibleReader
        {
            get
            {
                return "bible";
            }
        }
        public static string PublicationReader
        {
            get
            {
                return "publication";
            }
        }
        public static string DailyTextReader
        {
            get
            {
                return "dailytext";
            }
        }
        public static string WOLReader
        {
            get
            {
                return "wol";
            }
        }
        public static string EPUBReader
        {
            get
            {
                return "epub";
            }
        }
        public static string HighlighterMode
        {
            get
            {
                return "highlight";
            }
        }
    }

    public static class ReaderNavigationType
    {
        public static string SelectingNavigation
        {
            get
            {
                return "Selecting Navigation";
            }
        }
        public static string SwipingNavigation
        {
            get
            {
                return "Swiping Navigation";
            }
        }
        public static bool IsSelectingNavigation
        {
            get
            {
                string prefs = ThisApp.preferences.GetString("listReaderNavigation", SelectingNavigation);   
   
                return prefs.Equals(SelectingNavigation);
            }
        }
        public static bool IsSwipingNavigation
        {
            get
            {
                string prefs = ThisApp.preferences.GetString("listReaderNavigation", SelectingNavigation);

                return prefs.Equals(SwipingNavigation);
            }
        }

        public static App ThisApp
        {
            get
            {
                return App.Instance;
            }
        } 
    }
}
