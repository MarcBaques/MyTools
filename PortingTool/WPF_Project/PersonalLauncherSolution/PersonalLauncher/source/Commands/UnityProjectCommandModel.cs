namespace PersonalLauncher.Commands
{
    public class UnityProjectCommandModel
    {
        public string ProjectPath;
        public string UnityVersionPath;
        public string BuildTarget; 

        public UnityProjectCommandModel(string projectPath, string unityVersionPath, string buildTarget)
        {
            ProjectPath = projectPath;
            UnityVersionPath = unityVersionPath;
            BuildTarget = buildTarget;
        }
    }
}
