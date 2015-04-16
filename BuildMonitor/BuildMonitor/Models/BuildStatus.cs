namespace BuildMonitor.Models
{
    internal class BuildStatus {
        public BuildResult Result { get; set; }
        public bool IsBuilding { get; set; }

        public CubeConfiguration ToCubeConfiguration(){
            return new CubeConfiguration{
                Channel = Result == BuildResult.Success ? Channel.Green : Channel.Red,
                Mode = IsBuilding ? Mode.Pulse : Mode.On,
                Repetitions = 0
            };
        }
    }
}