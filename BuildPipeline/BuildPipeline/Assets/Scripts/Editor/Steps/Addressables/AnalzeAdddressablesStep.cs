using BlackRefactory.Utils;
using System.Linq;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build.AnalyzeRules;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class AnalzeAdddressablesStep : CustomBuildStep
    {
        public bool AnalyzeNonFixable = false;
        public bool ClearAnalisis = false;
        public bool Fix = false;

        public override void ExecuteStep(BuildPipelineInformation options)
        {
            var rules = AssembliesUtils.GetSubclassesOfBaseClass<AnalyzeRule>().ToList();
            for (int i = 0; i < rules.Count; i++)
            {
                if (!rules[i].CanFix && !AnalyzeNonFixable)
                    continue;

                if (ClearAnalisis)
                    rules[i].ClearAnalysis();

                var results = rules[i].RefreshAnalysis(AddressableAssetSettingsDefaultObject.Settings);
                foreach (var result in results)
                {
                    Debug.Log(result.resultName);
                }

                if (Fix && rules[i].CanFix)
                    rules[i].FixIssues(AddressableAssetSettingsDefaultObject.Settings);
            }
        }
    }
}