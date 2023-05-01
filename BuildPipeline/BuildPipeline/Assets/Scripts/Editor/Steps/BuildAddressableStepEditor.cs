using BlackRefactory.BuildPipeline.Steps;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace BlackRefactory.BuildPipeline
{

    [CustomPropertyDrawer(typeof(BuildAddressablesStep))]
    public class BuildAddressableStepEditor : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Rect tittleRect = new Rect(position.x, position.y, position.width, position.height);
            EditorGUI.LabelField(tittleRect,  new GUIContent(GetClassName(property.managedReferenceFullTypename)), EditorStyles.largeLabel);
            EditorGUI.EndProperty();
            //base.OnGUI(position, property, label);
        }

        private string GetClassName(string fulltypeName)
        {
            var splits = fulltypeName.Split('.');
            return splits[splits.Length - 1];
        }
    }
}
