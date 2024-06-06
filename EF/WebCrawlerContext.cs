﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Models;

namespace WebCrawler.EF
{
    public class WebCrawlerContext : DbContext
    {
        private string ConnectionString { get; set; }

        public DbSet<Site> Sites { get; set; }
        public WebCrawlerContext()
        {

        }

        public WebCrawlerContext(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnString"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }
    }
}
