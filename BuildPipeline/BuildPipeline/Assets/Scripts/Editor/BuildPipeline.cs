using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline
{
    public partial class BuildPipeline : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public static BuildTemplate BuildPipelineTemplate;
        public static BuildPipelineInformation BuildPipelineInformation;

        public static Action<BuildPipelineInformation> OnPostProcess;
        public static Action OnPreProcess;

        public static void StartBuildPipeline()
        {
            PreBuildSteps();
            Build();
        }

        public static void StartBuildPipeline(BuildTemplate buildTemplate, BuildPipelineInformation options)
        {
            BuildPipelineTemplate = buildTemplate;
            BuildPipelineInformation = options;
            StartBuildPipeline();
        }

        private static void PreBuildSteps()
        {
            Debug.Log("[BP] Pre pipeline processors");
            SetupSettings(BuildPipelineInformation);

            if (BuildPipelineTemplate != null && BuildPipelineTemplate.GetPipelinePreProcessor() != null)
            {
                Debug.Log("[BP] Starting pre pipelines preprocessors");
                for (int i = 0; i < BuildPipelineTemplate.GetPipelinePreProcessor().Count; i++)
                {
                    //TODO: How to treat async steps or need to reset editor as Defines
                    BuildPipelineTemplate.GetPipelinePreProcessor()[i].ExecuteStep(BuildPipelineInformation);
                }
            }
        }

        private static void Build()
        {
            Debug.Log("[BP] Build");
            
            DefaultBuild(BuildPipelineInformation.PlayerOptions);
        }

        public static void SetupBuildInformation(BuildPlayerOptions obj)
        {
            BuildPipelineInformation = new BuildPipelineInformation(obj);
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (BuildPipelineTemplate != null && BuildPipelineTemplate.GetBuildPreProcessor() != null)
            {
                BuildPipelineInformation.Report = report;
                Debug.Log("[BP] Starting PreProcessors");
                for (int i = 0; i < BuildPipelineTemplate.GetBuildPreProcessor().Count; i++)
                {
                    BuildPipelineTemplate.GetBuildPreProcessor()[i].ExecuteStep(BuildPipelineInformation);
                }
            }

            OnPreProcess?.Invoke();
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            if (BuildPipelineTemplate != null && BuildPipelineTemplate.GetBuildPostProcessor() != null)
            {
                Debug.Log("[BP] Starting PostProcessors");
                for (int i = 0; i < BuildPipelineTemplate.GetBuildPostProcessor().Count; i++)
                {
                    BuildPipelineTemplate.GetBuildPostProcessor()[i].ExecuteStep(BuildPipelineInformation);
                }
            }

            OnPostProcess?.Invoke(BuildPipelineInformation);
        }
        
        private static void SetupSettings(BuildPipelineInformation options)
        {
            if (BuildPipelineTemplate != null)
            {
                IBuildTemplate buildPipeline = BuildPipelineTemplate;
                Debug.Log("[BP] Setup build pipeline");
                buildPipeline.SetupSettings(options);
            }
        }

        private static void DefaultBuild(BuildPlayerOptions options)
        {
            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
        }

        //TODO: Find buildPipeline name in assets
        private static BuildTemplate FindBuildTemplate(string buildTemplateName)
        {
            return null;
        }
    }
}

