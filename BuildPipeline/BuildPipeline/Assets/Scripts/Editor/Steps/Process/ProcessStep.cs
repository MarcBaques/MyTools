using System.Diagnostics;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class ProcessStep : CustomBuildStep
    {
        public bool CreateNoWindow = false; 
        public bool UseShellExecute = false;
        public bool WaitForExit = false;
        public string Filename = "";
        [TextArea(2,5)]
        public string Args = "";
        public override void ExecuteStep(BuildPipelineInformation options)
        {
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = Filename;
            info.Arguments = Args;

            process.StartInfo = info;
            process.Start();
            if(WaitForExit)
                process.WaitForExit();
            process.Close();
        }
    }
}