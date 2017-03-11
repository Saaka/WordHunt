using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Config
{
    public class WordHuntConfiguration : IAppConfiguration
    {
        private IConfigurationRoot config;

        public WordHuntConfiguration(IConfigurationRoot config)
        {
            this.config = config;
        }

        public string DbConnectionString => config["data:connectionString"].ToString();
    }
}
