using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class BuildAddressablesStep : CustomBuildStep
    {
        public override void ExecuteStep(BuildPipelineInformation options)
        {
            AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult result);
        }
    }
}