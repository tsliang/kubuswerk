using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using BuildMonitor.Models;
using TeamCitySharp.DomainEntities;

namespace BuildMonitor{
    internal class Program
    {
        private static readonly string BuildID =
            ConfigurationSettings.AppSettings["BuildId"].ToString(CultureInfo.InvariantCulture);

        private static Thread _musicThread;

        private static void Main(string[] args){
            var teamCityCube = new TeamCityCube();
            _musicThread = new Thread(PlaySong);
            var teamCityCubeDriver = new TeamCityCubeDriver();
            teamCityCube.Connect();
            var builds = teamCityCube.GetBuildConfigurations();
            while (true)
            {
                var chosenBuild = ShowMenuAndGetChosenBuild(builds);
                RunProgram(teamCityCube, chosenBuild, teamCityCubeDriver);
            }
        }

        private static void RunProgram(TeamCityCube teamCityCube, BuildConfig chosenBuild, TeamCityCubeDriver teamCityCubeDriver)
        {
            do
            {
                while (!Console.KeyAvailable)
                {
                    var status = teamCityCube.GetStatus(chosenBuild);
                    teamCityCubeDriver.SendUpdate(status);
                    ShowBuildStatus(status);
                    Thread.Sleep(1000);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static BuildConfig ShowMenuAndGetChosenBuild(List<BuildConfig> builds)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            var buildsByIndex = builds.ToDictionary(x => builds.FindIndex(m => m == x), y => y);
            foreach (var buildConfig in buildsByIndex)
            {
                Console.WriteLine("{0}. {1}", buildConfig.Key + 1, buildConfig.Value);
            }
            Console.WriteLine("Enter the Id of a configuration. Press ESC to start over. :)");
            var buildIndexStr = Console.ReadLine();
            while (true)
            {
                var buildIndex = 0;
                if (!int.TryParse(buildIndexStr, out buildIndex))
                {
                    Console.WriteLine("Invalid number.");
                }
                else if (!buildsByIndex.ContainsKey(buildIndex - 1))
                {
                    Console.WriteLine("Invalid selection.");
                }
                else
                {
                    return builds[buildIndex - 1];
                }
            }
        }        

        private static void ShowBuildStatus(BuildStatus status){
            Console.BackgroundColor = status.Result == BuildResult.Success ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
            Console.ForegroundColor = status.IsBuilding ? ConsoleColor.White : ConsoleColor.Black;
            if (status.Result == BuildResult.Failure)
            {
                if (!_musicThread.IsAlive)
                {
                    _musicThread.Start();
                }
            }
            else
            {
                if (_musicThread.IsAlive)
                {
                    _musicThread.Abort();
                }
            }
            Console.WriteLine(status.IsBuilding ? "Building" : "Done");
        }

        private static void PlaySong()
        {
            Console.Beep(658, 125); Console.Beep(1320, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 250); Console.Beep(1320, 125); Console.Beep(1188, 125); Console.Beep(1056, 250); Console.Beep(990, 250); Console.Beep(880, 500); Console.Beep(880, 250); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 750); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(250); Console.Beep(1188, 500); Console.Beep(1408, 250); Console.Beep(1760, 500); Console.Beep(1584, 250); Console.Beep(1408, 250); Console.Beep(1320, 750); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(500); Console.Beep(1320, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 250); Console.Beep(1320, 125); Console.Beep(1188, 125); Console.Beep(1056, 250); Console.Beep(990, 250); Console.Beep(880, 500); Console.Beep(880, 250); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 750); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(250); Console.Beep(1188, 500); Console.Beep(1408, 250); Console.Beep(1760, 500); Console.Beep(1584, 250); Console.Beep(1408, 250); Console.Beep(1320, 750); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(500); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 1000); Console.Beep(440, 1000); Console.Beep(419, 1000); Console.Beep(495, 1000); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 500); Console.Beep(660, 500); Console.Beep(880, 1000); Console.Beep(838, 2000); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 1000); Console.Beep(440, 1000); Console.Beep(419, 1000); Console.Beep(495, 1000); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 500); Console.Beep(660, 500); Console.Beep(880, 1000); Console.Beep(838, 2000);
        }
    }
}