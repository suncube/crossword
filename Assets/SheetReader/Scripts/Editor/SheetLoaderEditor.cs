using UnityEditor;
using UnityEngine;

namespace SC.SheetReader
{
    [CustomEditor(typeof(SheetLoader))]
    public class SheetLoaderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var loader = (SheetLoader)target;

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.Space();
            if (GUILayout.Button("Open Sheet"))
            {
                Application.OpenURL($"https://docs.google.com/spreadsheets/d/{loader.SheetInfo.SpreadsheetId}");
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Load & Parce"))
            {
                loader.LoadAndParse();
            }
        }

    }


}