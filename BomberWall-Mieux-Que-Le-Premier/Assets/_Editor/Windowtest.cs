using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Windowtest : EditorWindow
{
    public string newName;

    [MenuItem("Windowtest")]
    public static void  ShowWindow()
    {
        GetWindow<Windowtest>("test");
    }

    private void OnGUI()
    {
        GUILayout.Label("titre");
        EditorGUILayout.Space();

        newName = EditorGUILayout.TextField("test", newName);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Click me"))
        {

        }
    }
}
