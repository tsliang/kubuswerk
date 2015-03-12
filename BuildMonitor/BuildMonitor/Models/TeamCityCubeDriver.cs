namespace BuildMonitor.Models
{
    internal class TeamCityCubeDriver
    {
        public void SendUpdate(BuildStatus status)
        {
            BlinkType = status.IsBuilding ? BlinkType.Pulse : BlinkType.Blink;
            IsSuccess = status.Result == BuildResult.Success;
        }

        public bool IsSuccess { get; private set; }
        public BlinkType BlinkType { get; private set; }
    }
}