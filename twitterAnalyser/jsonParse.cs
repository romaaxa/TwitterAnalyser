using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace twitterAnalyser
{
    public class jsonParse
    {
        public Dictionary<string, List<List<List<double>>>> jsonparse(string json)
        {
            string jsonstring = new StreamReader(json).ReadToEnd();
            return JsonConvert.DeserializeObject<Dictionary<string, List<List<List<double>>>>>(jsonstring);
        }
    }
}
