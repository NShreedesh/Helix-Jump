using Editor.Helix_Scripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EditorHelixGenerate))]
    public class HelixGenerateEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var editorHelixGenerate = (EditorHelixGenerate)target;
            
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Generate And Rotate"))
            {
                editorHelixGenerate.GenerateAndRotate();
            }

            if (GUILayout.Button("Generate Random Helixes"))
            {
                editorHelixGenerate.GenerateRandomHelix();
            }
            
            if (GUILayout.Button("Clear all Helixes"))
            {
                editorHelixGenerate.ClearAllHelixes();
            }
        }      
    }
}
