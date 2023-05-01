using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlackRefactory.BuildPipeline
{
    [CreateAssetMenu(fileName = "Android_BuildPipeline_Template", menuName = "BlackRefactory/Build Pipeline/Template/Android")]
    public class AndroidBuildTemplate : BuildTemplate
    {
        public bool apk = false;

        public override void DrawBuildTemplate()
        {
            base.DrawBuildTemplate();

            apk = EditorGUILayout.Toggle("Mode", apk);
        }

        public override void SetupSettings(BuildPipelineInformation options)
        {
            Debug.Log("Setup android");

        }
    }
}
