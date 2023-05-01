using BlackRefactory.Utils;
using PlasticGui.WorkspaceWindow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BlackRefactory.BuildPipeline
{
    public class BuildPipelineEditorWindow : ExtendedEditorWindow
    {
        private VisualElement m_RightPane;

        private void Awake()
        {
            Debug.Log("Awake  window");
        }

        [MenuItem("Pipeline/My Window")]
        private static void OpenPipelinesWindow()
        {
            BuildPipelineEditorWindow window = (BuildPipelineEditorWindow)GetWindow(typeof(BuildPipelineEditorWindow));
            window.titleContent = new GUIContent("Build Pipeline Editor");
            window.Show();
        }

        public void CreateGUI()
        {
            // Get a list of all sprites in the project
            var allObjectGuids = AssetDatabase.FindAssets("t:BuildTemplate");
            var allObjects = new List<BuildTemplate>();
            foreach (var guid in allObjectGuids)
            {
                allObjects.Add(AssetDatabase.LoadAssetAtPath<BuildTemplate>(AssetDatabase.GUIDToAssetPath(guid)));
            }

            // Create a two-pane view with the left pane being fixed with
            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

            // Add the view to the visual tree by adding it as a child to the root element
            rootVisualElement.Add(splitView);

            // A TwoPaneSplitView always needs exactly two child elements
            Label listLable = new Label();
            listLable.text = "Pipelines";
            listLable.focusable = false;
            
            
            var leftPane = new ListView();
            splitView.Add(leftPane);
            // Initialize the list view with all sprites' names
            leftPane.makeItem = () => new Label();
            leftPane.bindItem = (item, index) => { 
                (item as Label).text = allObjects[index].name; 
            };
            leftPane.itemsSource = allObjects;
            leftPane.style.alignContent = Align.FlexEnd;
            leftPane.style.alignSelf = Align.FlexStart;
            leftPane.onSelectionChange += OnPipelineSelectionChange;

            m_RightPane = new VisualElement();
            splitView.Add(m_RightPane);

        }

        private void OnPipelineSelectionChange(IEnumerable<object> selectedItems)
        {
            m_RightPane.Clear();

            var scroll = new ScrollView();
            // Get the selected Pipeline
            var selectedPipeline = selectedItems.First() as BuildTemplate;
            if (selectedPipeline == null)
                return;

            var pipelineElement = new InspectorElement(selectedPipeline);
            scroll.Add(pipelineElement);

            m_RightPane.Add(scroll);
        }

        private void DrawListSide()
        {
            
        }
    }

}
