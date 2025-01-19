using UnityEditor;
using UnityEngine;

public class PlayerPrefabEditor : EditorWindow
{
    private GameObject _playerPrefab;
    private Vector2 _scrollPosition;
    private bool[] _widowsStates; //Chaque entrée du tableau foldoutStates est un booléen qui peut être soit true (fenêtre dépliée) soit false (fenêtre repliée).

    [MenuItem("Window/Player Prefab Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefabEditor>("Player Prefab Editor");
    }

    private void OnGUI()
    {
        // Styles
        GUIStyle VisuelTitre = new GUIStyle(EditorStyles.boldLabel) //Mise en forme titre
        {
            fontSize = 20,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.white }
        };

        GUIStyle VisuelBox = new GUIStyle(GUI.skin.box) //Mise en forme des Box
        {
            normal = { background = null }
        };

//----------------------------------------Contents (Cas ou 0 préfab choisi)-------------------------------------------------------------------------------------------
        GUILayout.Label("Player Prefab Editor", VisuelTitre);
        GUILayout.Space(20);

        _playerPrefab = (GameObject)EditorGUILayout.ObjectField("Player Prefab", _playerPrefab, typeof(GameObject), false);
        GUILayout.Space(10);

        if (_playerPrefab == null)
        {
            EditorGUILayout.HelpBox("Un préfab est néccéssaire pour pouvoir continuer", MessageType.Info);
            return;
        }
//-----------------------------------------Contents (Préfab choisi)---------------------------------------------------------------------------------------------------

        MonoBehaviour[] scripts = _playerPrefab.GetComponents<MonoBehaviour>(); //Je ne veux afficher que les scripts

        if (_widowsStates == null || _widowsStates.Length != scripts.Length)
            _widowsStates = new bool[scripts.Length];

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(400)); //On fait des windows

        for (int i = 0; i < scripts.Length; i++) // Trie parmi les Components
        {
            MonoBehaviour script = scripts[i];
            if (script == null) continue;

            _widowsStates[i] = EditorGUILayout.Foldout(_widowsStates[i], script.GetType().Name);

            if (_widowsStates[i])
            {
                SerializedObject serializedScript = new SerializedObject(script); // Sérialisation du script
                SerializedProperty property = serializedScript.GetIterator();
                property.NextVisible(true);

                while (property.NextVisible(false))
                {
                    if (property.name != "m_Script")
                        EditorGUILayout.PropertyField(property, true);
                }
                serializedScript.ApplyModifiedProperties();
            }
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Sauvegarder les changements")) //Si on appuie
            Debug.Log("C'est save");
    }
}
