using System;
using System.Collections.Generic;
using System.Text;
using BuildMonitor.Models;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;

namespace BuildMonitor
{
    internal class TeamCityAgent {
        private TeamCityClient client;

        public BuildStatus GetStatus(BuildConfig build){
            var client = ConnectToTeamCity();
            var latestBuild = new LatestBuild(client, build.Id);
            return latestBuild.GetStatus();            
        }

        public TeamCityClient ConnectToTeamCity() {
            client = new TeamCityClient("usb-gatteamcity.ef.com");
            client.Connect("tai.sassen-liang", Encoding.UTF8.GetString(Convert.FromBase64String("R0FUREVW")));
            return client;
        }

        public List<BuildConfig> GetBuildConfigurations(){
            return client.BuildConfigs.All();
        }
    }
}