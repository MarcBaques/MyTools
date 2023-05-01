using BlackRefactory.BuildPipeline.Steps;
using BlackRefactory.Utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BlackRefactory.BuildPipeline
{
    [InitializeOnLoad]
    public class BuildWindow : ExtendedEditorWindow
    {
        private Vector2 m_ScrollPosition;

        static BuildWindow()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(OpenCustomBuildPipelineWindow);
        }

        private static void OpenCustomBuildPipelineWindow(BuildPlayerOptions obj)
        {
            BuildWindow window = (BuildWindow)GetWindow(typeof(BuildWindow));
            window.titleContent = new GUIContent("Build Pipeline");
            window.Show();

            if(BuildPipeline.BuildPipelineTemplate != null)
            {
                m_SerializedObject = new SerializedObject(BuildPipeline.BuildPipelineTemplate);
            }

            BuildPipeline.SetupBuildInformation(obj);
        }

        private void OnGUI()
        {
            DrawBuildPipelineOptions();

            EditorGUILayout.Space(10f);
            if (GUILayout.Button("Build"))
            {
                Build();
            }
            if (GUILayout.Button("Cancel"))
            {
                CancelBuild();
            }
        }

        private void DrawBuildPipelineOptions()
        {
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition,  GUILayout.ExpandHeight(false));
;
            EditorGUILayout.LabelField("Template:");
            EditorGUI.BeginChangeCheck();
            BuildPipeline.BuildPipelineTemplate = (BuildTemplate)EditorGUILayout.ObjectField(BuildPipeline.BuildPipelineTemplate, typeof(BuildTemplate), false);
            if (EditorGUI.EndChangeCheck())
            {
                if(BuildPipeline.BuildPipelineTemplate != null)
                {
                    m_SerializedObject = new SerializedObject(BuildPipeline.BuildPipelineTemplate);
                }
            }

            if(BuildPipeline.BuildPipelineTemplate != null && m_SerializedObject != null)
            {
                m_CurrentProperty = m_SerializedObject.FindProperty("PrePipelineSteps");
                DrawPropierties(m_CurrentProperty, true);

                m_CurrentProperty = m_SerializedObject.FindProperty("PreBuildSteps");
                DrawPropierties(m_CurrentProperty, true);

                m_CurrentProperty = m_SerializedObject.FindProperty("PostBuildSteps");
                DrawPropierties(m_CurrentProperty, true);
                //TODO: Draw as Tree View
                //DrawBuildProcessor<CustomBuildStep>(BuildPipeline.BuildPipelineTemplate.GetPipelinePreProcessor(), "Pre Pipeline");

                //EditorGUILayout.Space(5f);

                //DrawBuildProcessor<CustomBuildStep>(BuildPipeline.BuildPipelineTemplate.GetBuildPreProcessor(), "Pre Processor");

                //EditorGUILayout.Space(5f);

                //DrawBuildProcessor<CustomBuildStep>(BuildPipeline.BuildPipelineTemplate.GetBuildPostProcessor(), "Post Processor");

                EditorGUILayout.Space(10f);

                DrawBuildTemplateSummary();
            }

            EditorGUILayout.EndScrollView();
        }

        private static void DrawBuildTemplateSummary()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Summary", EditorStyles.boldLabel);
            GUI.enabled = false;
            IBuildTemplate buildPipeline = BuildPipeline.BuildPipelineTemplate;
            buildPipeline.DrawBuildTemplate();
            GUI.enabled = true;
            EditorGUILayout.EndVertical();
        }

        private void CancelBuild()
        {
            this.Close();
        }

        private void Build()
        {
            BuildPipeline.StartBuildPipeline();

            this.Close();
        }
    }
}

