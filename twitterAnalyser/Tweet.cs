using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitterAnalyser
{
    public class Tweet
    {
        public PointLatLng latLng; //{get: set; }
        public string DayofWeek { get; set; }
        public DateTime time { get; set; }
        public string text { get; set; }
        public Tweet() { }
        public Tweet(PointLatLng latLng, string DayofWeek, DateTime time, string text)
        {
            this.latLng = latLng;
            this.DayofWeek = DayofWeek;
            this.time = time;
            this.text = text;
        }

        public List<string> splitWords()
        {
            string[] ar = text.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ar.Length; i++)
            {
                ar[i] = ar[i].Trim(' ', ',', '.');
            }
            List<string> words = ar.ToList<string>();
            return words;
        }
        //tweetMaker try to add in a class
        #region tweetMaker

        //correctly type maketweet function

        #endregion

    }
}