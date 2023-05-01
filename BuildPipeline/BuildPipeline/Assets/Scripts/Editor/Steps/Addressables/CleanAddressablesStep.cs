using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class CleanAddressablesStep : CustomBuildStep
    {
        public override void ExecuteStep(BuildPipelineInformation options)
        {
            AddressableAssetSettings.CleanPlayerContent();
        }
    }
}