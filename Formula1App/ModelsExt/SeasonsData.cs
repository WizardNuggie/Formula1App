﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class SeasonsApi
    {
        public SeasonsData SeasonsData { get; set; }
    }

    public class SeasonsData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public Seasontable SeasonTable { get; set; }
    }

    public class Seasontable
    {
        public List<Season> Seasons { get; set; }
    }

    public class Season
    {
        public string season { get; set; }
        public string url { get; set; }
    }

}
