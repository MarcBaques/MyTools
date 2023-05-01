using BlackRefactory.BuildPipeline.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BlackRefactory.BuildPipeline
{
    [CreateAssetMenu(fileName = "PC_BuildPipeline_Template", menuName = "BlackRefactory/Build Pipeline/Template/PC")]
    public class PCBuildTemplate : BuildTemplate
    {
        public bool Development = false;

        public override void DrawBuildTemplate()
        {
            base.DrawBuildTemplate();

            Development = EditorGUILayout.Toggle("Development", Development);
        }

        public override void SetupSettings(BuildPipelineInformation options)
        {
            Debug.Log("Setup PC");
            EditorUserBuildSettings.development = Development;
        }

        [ContextMenu("Create")]
        public void CreateAllInherited()
        {
            var clases = GetAllInherited(typeof(CustomBuildStep));
            foreach (var stepClass in clases)
            {
                var a = (CustomBuildStep)Activator.CreateInstance(stepClass);
                PrePipelineSteps.Add(a);
            }
        }

        IEnumerable<Type> GetAllInherited(Type t)
        {
            return Assembly.GetAssembly(t).GetTypes().Where(assemblyType => assemblyType.IsClass && !assemblyType.IsAbstract && assemblyType.IsSubclassOf(t));
        }
    }
}