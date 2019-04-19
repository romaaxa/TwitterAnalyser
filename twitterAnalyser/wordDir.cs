using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitterAnalyser
{
    class wordDir
    {
        public Dictionary<string, double> wordsValue = new Dictionary<string, double>();
        bool create = false;

        public void CreatingDictionary()
        {
            create = true;
            StreamReader reader = new StreamReader("sentiments.csv");
            string line;

            //string[] st = new string[n];
            string[] st = new string[2];
            while ((line = reader.ReadLine()) != null)
            {
                st = line.Split(',');
                st[1] = st[1].Replace('.', ',');
                wordsValue.Add(st[0].ToLower(), double.Parse(st[1]));
            }
            create = true;
        }

        double sentiments(string word)
        {
            if (create == false) { CreatingDictionary(); }
            return wordsValue[word.ToLower()];
        }

        bool checkword(string word)
        {
            if (create == false) { CreatingDictionary(); }
            if (wordsValue.ContainsKey(word.ToLower())) return true;
            return false;
        }

        Tweet t = new Tweet(); //add construct
        public double AverageMood(Tweet t, tweetMaker tm)
        {
            //check tm
            List<string> words = t.splitWords();
            double mood = 0; int wordscount = 0;

            bool exp = false;
            for (int i = 0; i < words.Count; i++)
            {
                double value = 0;
                int count = 1;
                string str = words[i];
                for (int y = i; y < words.Count - 1; y++)
                {
                    if (checkword(str))
                    {
                        exp = true;
                        value = sentiments(str);
                        count = str.Split(' ').Length;
                    }
                    str += " " + words[y + 1];
                }
                mood += value;

                if (exp == true) { wordscount++; exp = false; }
                for (int z = 1; z < count; z++)
                {
                    i++;
                }
            }
            var res = (mood / wordscount);
            return res;
        }
    }
}
