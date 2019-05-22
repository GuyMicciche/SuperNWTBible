using Android.App;
using Android.Preferences;
using System;
using System.Collections.Generic;
using System.Text;

namespace NWTBible
{
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
