using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class GenerateAddressablesReportStep : CustomBuildStep
    {
        public bool GenerateBuildLayout = true;
        public bool ExportLayoutToBuildFolder = true;
        public bool ResetValueOnFinish = true;

        private bool m_GenerateBuildLayoutDefaultValue = false;

        private const string BUILD_LAYOUT_FILE_APPLICATION_PATH = @"\Library\com.unity.addressables\buildlayout.txt";
        private const string BUILD_LAYOUT_FILE_PATH = @"buildlayout.txt";
        public override void ExecuteStep(BuildPipelineInformation options)
        {
            m_GenerateBuildLayoutDefaultValue = ProjectConfigData.GenerateBuildLayout;
            ProjectConfigData.GenerateBuildLayout = GenerateBuildLayout;

            if(ResetValueOnFinish)
                BuildPipeline.OnPostProcess += ResetValue;

            if (ExportLayoutToBuildFolder)
                BuildPipeline.OnPostProcess += MoveLayoutFile;
        }

        private void ResetValue(BuildPipelineInformation info)
        {
            BuildPipeline.OnPostProcess -= ResetValue;
            ProjectConfigData.GenerateBuildLayout = m_GenerateBuildLayoutDefaultValue;
        }

        private void MoveLayoutFile(BuildPipelineInformation info)
        {
            BuildPipeline.OnPostProcess -= MoveLayoutFile;

            if (Directory.Exists(info.BuildOutputDirectoryPath))
            {
                var reportFilePath = Directory.GetParent(Application.dataPath) + BUILD_LAYOUT_FILE_APPLICATION_PATH;
                if (File.Exists(reportFilePath))
                {
                    FileInfo buildReport = new FileInfo(reportFilePath);
                    var destinyFilePath = Path.Combine(info.BuildOutputDirectoryPath, BUILD_LAYOUT_FILE_PATH);

                    if (File.Exists(destinyFilePath))
                    {
                        File.Delete(destinyFilePath);
                    }
                    buildReport.CopyTo(Path.Combine(info.BuildOutputDirectoryPath, BUILD_LAYOUT_FILE_PATH), true);
                }
            }


        }
    }
}
