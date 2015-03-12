using System;
using System.Collections.Generic;
using System.Text;
using BuildMonitor.Models;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;

namespace BuildMonitor
{
    internal class TeamCityCube {
        private TeamCityClient client;

        public BuildStatus GetStatus(BuildConfig build){
            var client = Connect();
            var latestBuild = new LatestBuild(client, build.Id);
            return latestBuild.GetStatus();            
        }

        public TeamCityClient Connect() {
            client = new TeamCityClient("usb-gatteamcity.ef.com");
            client.Connect("peter.lazzarino", Encoding.UTF8.GetString(Convert.FromBase64String("R3Vhbm8zMTE=")));
            return client;
        }

        public List<BuildConfig> GetBuildConfigurations(){
            return client.BuildConfigs.All();
        }
    }
}