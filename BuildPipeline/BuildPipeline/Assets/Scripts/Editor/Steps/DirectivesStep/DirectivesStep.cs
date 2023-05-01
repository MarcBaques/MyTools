using BlackRefactory.Utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline.Steps
{
    public class DirectivesStep : CustomBuildStep
    {
        public List<string> Directives = new List<string>();
        public bool ResetDirectivesOnFinish = true;

        private string DefaultDirectives = "";
        public override void ExecuteStep(BuildPipelineInformation info)
        {
            DefaultDirectives = DirectiveUtils.GetCurrentDirectives();

            foreach (var directive in Directives)
            {
                if (DirectiveUtils.IsDirective(directive))
                {
                    continue;
                }

                DirectiveUtils.AddDirective(directive);
            }

            if (ResetDirectivesOnFinish)
                BuildPipeline.OnPostProcess += ResetDirectives;
        }

        private void ResetDirectives(BuildPipelineInformation info)
        {
            BuildPipeline.OnPostProcess -= ResetDirectives;

            DirectiveUtils.SetDirectives(DefaultDirectives);
        }
    }
}