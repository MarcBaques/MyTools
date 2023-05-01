using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class IncludeScenesStep : CustomBuildStep
    {
        public List<SceneAsset> ExcludeScenes = new List<SceneAsset>();

        private List<EditorBuildSettingsScene> m_DefaultScenesList = new List<EditorBuildSettingsScene>();

        public override void ExecuteStep(BuildPipelineInformation options)
        {
            List<string> buildScenes = new List<string>();

            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!InExcludedScenes(scene.path))
                {
                    buildScenes.Add(scene.path);
                }
            }

            options.PlayerOptions.scenes = buildScenes.ToArray();
        }

        private bool InExcludedScenes(string scenePath)
        {
            foreach (var scene in ExcludeScenes)
            {
                var path = AssetDatabase.GetAssetOrScenePath(scene);
                if (scenePath == path)
                    return true;
            }
            return false;
        }
    }
}

