using System;
using System.Linq;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace BuildMonitor.Models
{
    internal class LatestBuild
    {
        private readonly ITeamCityClient client;
        private readonly string buildId;
        private Build currentBuild;

        public LatestBuild(ITeamCityClient client, string buildId)
        {
            this.client = client;
            this.buildId = buildId;
        }

        public BuildStatus GetStatus()
        {
            var build = client.Builds.ByBuildConfigId(buildId).OrderByDescending(m => m.StartDate).First();
            var buildLocator = BuildLocator.RunningBuilds();
            var runningBuild = client.Builds.ByBuildLocator(buildLocator).FirstOrDefault(m => m.BuildTypeId == buildId);
            currentBuild = runningBuild ?? build;
            return new BuildStatus {
                Result = currentBuild.Status.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase) ? BuildResult.Success : BuildResult.Failure,
                IsBuilding = runningBuild != null
            };
        }
    }
}