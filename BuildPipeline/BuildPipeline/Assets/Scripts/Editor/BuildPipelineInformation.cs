using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace BlackRefactory.BuildPipeline
{
    public class BuildPipelineInformation
    {
        public BuildPipelineInformation(BuildPlayerOptions options)
        {
            PlayerOptions = options;
            BuildOutputDirectoryPath = Path.GetDirectoryName(options.locationPathName);
        }

        public bool BatchMode = false;
        public BuildPlayerOptions PlayerOptions;
        public BuildReport Report;
        public string BuildOutputDirectoryPath;
    }
}

