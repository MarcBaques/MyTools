using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    [System.Serializable]
    public abstract class CustomBuildStep
    {
        //public string Name = this.Type;
        [SerializeField]
        public string ID { get => this.GetType().Name; }
        public abstract void ExecuteStep(BuildPipelineInformation options);
    }
}

