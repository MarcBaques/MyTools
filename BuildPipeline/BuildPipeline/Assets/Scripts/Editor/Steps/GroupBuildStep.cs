using System.Collections.Generic;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class GroupBuildStep : CustomBuildStep
    {
        [SerializeField] private List<CustomBuildStep> m_Steps = new List<CustomBuildStep>();
        public override void ExecuteStep(BuildPipelineInformation options)
        {
            for (int i = 0; i < m_Steps.Count; i++)
            {
                m_Steps[i].ExecuteStep(options);
            }
        }
    }
}
