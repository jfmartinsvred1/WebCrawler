﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Services
{
    public class LogService
    {
        private readonly static ILog log = LogManager.GetLogger(typeof(Crawler));
    }
}
