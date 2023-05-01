using BlackRefactory.BuildPipeline.Steps;
using System.Collections.Generic;

namespace BlackRefactory.BuildPipeline
{
    public interface IBuildTemplate : IBuildTemplateDrawer
    {
        void SetupSettings(BuildPipelineInformation options);
        public List<CustomBuildStep> GetPipelinePreProcessor();
        public List<CustomBuildStep> GetBuildPreProcessor();
        public List<CustomBuildStep>  GetBuildPostProcessor();
    }
}

