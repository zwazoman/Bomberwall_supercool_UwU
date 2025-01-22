using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefabEditor : EditorWindow //Custom Window qui reprends beaucoup de principe de UIManager (Manager qui g�re le jeu lorsque c'est du PVP)
{
    #region Variable
    private GameObject _iaPrefab;
    [SerializeField] private int _numberIA;
    [SerializeField] private List<Transform> _iaSpawn = new List<Transform>();
    [SerializeField] private GameObject[] _uiPrefabs; // Tableau de pr�fab UI (J1, J2, J3, J4)
    [SerializeField] private Transform _canvas; // R�f�rence au panelPlayerLife dans la sc�ne (Canvas parent des UIs)
    #endregion
    #region Cr�ationWindow
    [MenuItem("Window/Player Prefab Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefabEditor>("Player Prefab Editor");
    }
    #endregion

    private void OnGUI()
    {
        // Styles
        GUIStyle VisuelTitre = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 20,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.white }
        };

        GUILayout.Label("Player Prefab Editor", VisuelTitre);
        GUILayout.Space(20);

//------------------------------------------------------------Cr�ation de variable dans la window--------------------------------------------------------------------------

        _iaPrefab = (GameObject)EditorGUILayout.ObjectField("IA Prefab", _iaPrefab, typeof(GameObject), false);

        _numberIA = EditorGUILayout.IntField("Nombre d'IA sur le terrain", _numberIA);


        SerializedObject serializedObject = new SerializedObject(this); //D�claration pour les futurs listes

        //Liste
        SerializedProperty spawnList = serializedObject.FindProperty("_iaSpawn");
        EditorGUILayout.PropertyField(spawnList, true);

        EditorGUILayout.Space();

        _canvas = (Transform)EditorGUILayout.ObjectField("Canvas (UI Parent)", _canvas, typeof(Transform), true);

        //Liste
        SerializedProperty uiArray = serializedObject.FindProperty("_uiPrefabs");
        EditorGUILayout.PropertyField(uiArray, true);

        serializedObject.ApplyModifiedProperties();//Save

        GUILayout.Space(20);

//---------------------------------------------------Contents (Cas o� les variables sont mal set)---------------------------------------------------------------------------------

        if (_iaPrefab == null)
        {
            EditorGUILayout.HelpBox("Un prefab d'IA est n�cessaire pour continuer.", MessageType.Warning);
            return;
        }

        if (_numberIA <= 0 && _numberIA >= 4|| _iaSpawn.Count == 0)
        {
            EditorGUILayout.HelpBox("Pas ou trop d'IA, le max c'est 4, ils leurs faut �galement un transform pour spawn", MessageType.Warning);
            return;
        }

        if (_canvas == null)
        {
            EditorGUILayout.HelpBox("R�f�rence au Canvas manquante.", MessageType.Warning);
            return;
        }
//--------------------------------------------------Contents (Systeme d'instance)----------------------------------------------------------------------

        // Bouton pour entrer en mode jeu
        if (GUILayout.Button("Jouer la Sc�ne"))
        {
            InstantiateIA();
            EditorApplication.EnterPlaymode();
        }
    }

    private void InstantiateIA()
    {
        //Script UIManager qui fait pareil pour la scene PVP

        int count = Mathf.Min(_numberIA, _iaSpawn.Count); //count sert � ne pas cr�e trop d'ia (ex : 2 IA demand� pour 1 SpawnPoint)

        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = _iaSpawn[i];

            GameObject iaInstance = Instantiate(_iaPrefab.gameObject, spawnPoint.position, spawnPoint.rotation); //?

            if (i < _uiPrefabs.Length && _uiPrefabs[i] != null)
            {
                GameObject uiInstance = Instantiate(_uiPrefabs[i], _canvas);
                PlayerAttributeUI playerUI = iaInstance.GetComponent<PlayerAttributeUI>();
                if (playerUI != null)
                {
                    playerUI.AssignUI(uiInstance);
                }
            }
        }
    }
}
