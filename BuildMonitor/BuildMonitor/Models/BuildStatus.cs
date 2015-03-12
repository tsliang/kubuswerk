namespace BuildMonitor.Models
{
    internal class BuildStatus {
        public BuildResult Result { get; set; }
        public bool IsBuilding { get; set; }
    }
}