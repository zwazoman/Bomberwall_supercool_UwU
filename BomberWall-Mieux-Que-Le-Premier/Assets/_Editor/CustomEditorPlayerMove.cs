using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerMove))]
public class CustomEditorMove : Editor
{
    private MeshRenderer m_Renderer;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawLine();

        if (GUILayout.Button("Surprise"))
        {
            PlayerMove Target = (PlayerMove)target;
            Target.TryGetComponent<MeshRenderer>(out m_Renderer);
            m_Renderer.sharedMaterial.color = Random.ColorHSV();
        }
        DrawLine();
    }
    void DrawLine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}