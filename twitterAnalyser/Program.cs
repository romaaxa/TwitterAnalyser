using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;

namespace twitterAnalyser
{
    class Program : mapDrawing
    {
        static void Main(string[] args)
        {
            #region check correct work
            coordsParse cd = new coordsParse();

            jsonParse jp = new jsonParse();

            tweetMaker tw = new tweetMaker();

            Application.EnableVisualStyles();
            mapDrawing mp = new mapDrawing();
            mp.file = "texas";
            Application.Run(mp);
            //cd.OutPut("my_job");

            Console.ReadKey();
            #endregion
        }
    }
}