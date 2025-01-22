using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BombHandler))]
public class CustomEditorBomb : Editor
{
    private EntityTakeDamage _entityTakeDamage;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawLine();

        if (GUILayout.Button("BOOM"))
        {
            foreach(GameObject obj in UIManager.Instance.Players)
            {
                PlayerHealth health = obj.GetComponent<PlayerHealth>();
                int _numberOfPv = health.CurrentHealth;
                if (_numberOfPv > 0)
                {
                    health.TakeDamage(obj);
                    BombHandler Target = (BombHandler)target;
                    Target.TryGetComponent<EntityTakeDamage>(out _entityTakeDamage);
                    _entityTakeDamage.ParticleSystem.Play();
                }
            }
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