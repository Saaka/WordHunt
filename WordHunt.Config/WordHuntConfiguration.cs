using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Config
{
    public class WordHuntConfiguration : IAppConfiguration, IAuthConfiguration, ISeedConfiguration
    {
        private IConfigurationRoot config;

        public WordHuntConfiguration(IConfigurationRoot config)
        {
            this.config = config;
        }

        public string DbConnectionString => config["data:connectionString"].ToString();

        public string TokenKey => config["data:secretKey"].ToString();
        public string Issuer => config["data:issuer"].ToString();
        public string Audience => config["data:audience"].ToString();
        public int TokenValidInMinutes => Convert.ToInt32(config["data:tokenValidInMinutes"].ToString());

        public string AdminEmail => config["data:adminEmail"].ToString();
        public string AdminName => config["data:adminName"].ToString();
        public string AdminPassword => config["data:adminPassword"].ToString();

        public string UserEmail => config["data:userEmail"].ToString();
        public string UserName => config["data:userName"].ToString();
        public string UserPassword => config["data:userPassword"].ToString();
    }
}
