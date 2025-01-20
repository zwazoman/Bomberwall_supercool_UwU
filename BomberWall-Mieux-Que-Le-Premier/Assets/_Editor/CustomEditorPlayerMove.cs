using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerMove))]
public class CustomEditorPlayerMove : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Explode Bomb"))
        {
        }
    }
}