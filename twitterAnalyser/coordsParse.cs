using GMap.NET;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitterAnalyser
{
    class coordsParse : jsonParse
    {
        public class Polygon
        {
            public string name;
            public GMapPolygon polygon;
            public double mood;
            public Polygon(string name, GMapPolygon polygon, double mood)
            {
                this.name = name;
                this.polygon = polygon;
                this.mood = mood;
            }
        }

        public Dictionary<Tweet, Polygon> polyMood(string file)
        {
            wordDir wd = new wordDir();
            Dictionary<string, List<GMapPolygon>> states = Polygons();

            tweetMaker tm = new tweetMaker();
            List<Tweet> tweets = tm.MakeTweet(file);
            Dictionary<Tweet, Polygon> moodstates = new Dictionary<Tweet, Polygon>();
            foreach (Tweet t in tweets)
            {
                foreach (var s in states)
                {
                    for (int i = 0; i < s.Value.Count; i++)
                    {
                        if (s.Value[i].IsInside(t.latLng))
                        {
                          
                            if (wd.AverageMood(t, tm) >= -1 && wd.AverageMood(t, tm) <= 1)
                            {
                                moodstates.Add(t, new Polygon(s.Key, s.Value[i], wd.AverageMood(t, tm))); break;
                            }
                        }
                    }
                }
                continue;
            }
            return moodstates;
        }

        public Dictionary<string, List<GMapPolygon>> Polygons()
        {
            Dictionary<string, List<List<List<double>>>> states = jsonparse("states.json");
            Dictionary<string, List<GMapPolygon>> z = new Dictionary<string, List<GMapPolygon>>();

            foreach (var v in states)
            {
                List<GMapPolygon> poly = new List<GMapPolygon>();
                for (int i = 0; i < v.Value.Count; i++)
                {
                    List<PointLatLng> points = new List<PointLatLng>();
                    for (int y = 0; y < v.Value[i].Count; y++)
                    {
                        PointLatLng point = new PointLatLng(v.Value[i][y][1], v.Value[i][y][0]);
                        points.Add(point);
                    }
                    GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
                    poly.Add(polygon);
                }
                z.Add(v.Key, poly);
            }
            return z;
        }

        public Dictionary<GMapPolygon, double> averageMoodst(string file)
        {
            Dictionary<string, List<GMapPolygon>> polygons = Polygons();
            int polynum = 0;
            foreach (var v in polygons)
            {
                polynum += v.Value.Count;
            }

            Dictionary<GMapPolygon, double> states = new Dictionary<GMapPolygon, double>();
            Dictionary<GMapPolygon, int> count_tweets = new Dictionary<GMapPolygon, int>();

            Dictionary<Tweet, Polygon> tweets = polyMood(file);

            foreach (var v in tweets)
            {

                if (states.ContainsKey(v.Value.polygon)) { states[v.Value.polygon] += v.Value.mood; count_tweets[v.Value.polygon]++; }

                else { states.Add(v.Value.polygon, v.Value.mood); count_tweets.Add(v.Value.polygon, 1); }

            }

            foreach (var v in count_tweets)
            {
                states[v.Key] /= v.Value;
            }
            return states;

        }
    }
}

