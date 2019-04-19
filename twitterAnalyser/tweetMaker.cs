using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitterAnalyser
{
    public class tweetMaker
    {
        public List<Tweet> MakeTweet(string file)
        {
            Parse p = new Parse();
            List<Tweet> tweets = new List<Tweet>();
            string[] inform = p.info(file);

            for (int i = 0; i < inform.Length; i++)
            {
                string[] tparse = inform[i].Split(new string[] { "	" }, StringSplitOptions.RemoveEmptyEntries);
                if (tparse.Length == 4)
                {
                    string[] coords = tparse[0].Split(new string[] { "[", "]", "," }, StringSplitOptions.RemoveEmptyEntries);
                    coords[0] = coords[0].Replace('.', ','); coords[1] = coords[1].Replace('.', ',');

                    DateTime date = DateTime.ParseExact(tparse[2], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    PointLatLng latLng = new PointLatLng(double.Parse(coords[0]), double.Parse(coords[1]));

                    Tweet t = new Tweet(latLng, p.DayofWeek(int.Parse(tparse[1])), date, tparse[3]);
                    tweets.Add(t);
                }
            }
            return tweets;
        }
    }
}
