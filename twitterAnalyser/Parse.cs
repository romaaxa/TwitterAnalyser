using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace twitterAnalyser
{
    class Parse
    {
        string[] daysofWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public string DayofWeek(int n)
        {
            return daysofWeek[n];
        }
        public string[] info(string file)
        {
            //StreamReader reader = new StreamReader(@"");
            StreamReader reader = new StreamReader(file + ".txt");
            string text = reader.ReadToEnd();
            reader.Close();
            string[] tweets = text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return tweets;
        }
    }
}
