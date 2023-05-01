using BlackRefactory.BuildPipeline.Steps;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BlackRefactory.BuildPipeline
{
    public abstract class BuildTemplate : ScriptableObject, IBuildTemplate
    {
        [TextArea(2,4)] public string Description = "";
        
        [SerializeReference]
        public List<CustomBuildStep> PrePipelineSteps;
        [SerializeReference]
        public List<CustomBuildStep> PreBuildSteps;
        [SerializeReference]
        public List<CustomBuildStep> PostBuildSteps;


        public virtual void DrawBuildTemplate()
        {
            if(!string.IsNullOrEmpty(Description))
                GUILayout.Label(Description);

        }

        public abstract void SetupSettings(BuildPipelineInformation options);
        public virtual List<CustomBuildStep> GetPipelinePreProcessor() => PrePipelineSteps;
        public virtual List<CustomBuildStep> GetBuildPreProcessor() => PreBuildSteps;
        public virtual List<CustomBuildStep> GetBuildPostProcessor() => PostBuildSteps;
    }
}