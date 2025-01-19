using UnityEditor;
using UnityEngine;

public class VFXEditor : EditorWindow
{
    private ParticleSystem _particleSystem;
    private Vector2 _scrollPosition;
    private bool[] _windowStates;  // Pour gérer l'état de chaque fenêtre foldout (repliée/dépliée)
    private GameObject _instantiatedVFX;  // Instance du ParticleSystem dans la scène

    [MenuItem("Window/VFX Editor")]
    public static void ShowWindow()
    {
        GetWindow<VFXEditor>("VFX Editor");
    }

    private void OnGUI()
    {
        // Styles
        GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel)  // Mise en forme du titre
        {
            fontSize = 20,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.white }
        };

        //----------------------------------------Contents (Cas où aucun système de particules n'est sélectionné)-------------------------------------------
        GUILayout.Label("VFX Editor", titleStyle);
        GUILayout.Space(20);

        _particleSystem = (ParticleSystem)EditorGUILayout.ObjectField("Particle System", _particleSystem, typeof(ParticleSystem), false);
        GUILayout.Space(10);

        if (_particleSystem == null)
        {
            EditorGUILayout.HelpBox("Un particule system est néccéssaire pour pouvoir continuer", MessageType.Info);
            return;
        }

        //-----------------------------------------Contents (Système de particules sélectionné)------------------------------------------

        // Crée un tableau de booléens pour chaque script attaché au système de particules
        MonoBehaviour[] scripts = _particleSystem.GetComponents<MonoBehaviour>();

        if (_windowStates == null || _windowStates.Length != scripts.Length)
            _windowStates = new bool[scripts.Length];

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(400));  // On crée la fenêtre défilable

        for (int i = 0; i < scripts.Length; i++)
        {
            MonoBehaviour script = scripts[i];
            if (script == null) continue;

            _windowStates[i] = EditorGUILayout.Foldout(_windowStates[i], script.GetType().Name);  // On affiche le nom du script avec la possibilité de replier/ouvrir

            if (_windowStates[i])
            {
                SerializedObject serializedScript = new SerializedObject(script);  // Sérialise le script
                SerializedProperty property = serializedScript.GetIterator();
                property.NextVisible(true);

                while (property.NextVisible(false))
                {
                    if (property.name != "m_Script")
                        EditorGUILayout.PropertyField(property, true);
                }

                serializedScript.ApplyModifiedProperties();  // Applique les modifications
            }
        }

        EditorGUILayout.EndScrollView();

        //---------------------------------------- Boutons et actions supplémentaires ---------------------------------------
        GUILayout.Space(10);

        if (GUILayout.Button("Instancier le Particle System"))
        {
            if (_particleSystem != null)
            {
                if (_instantiatedVFX == null)
                {
                    _instantiatedVFX = Instantiate(_particleSystem.gameObject, Vector3.zero, Quaternion.identity);
                    _instantiatedVFX.transform.position = new Vector3(1, 3, 0);
                    _instantiatedVFX.SetActive(true);  // On active l'objet instancié
                    Selection.activeGameObject = _instantiatedVFX;  // Sélectionner l'instance dans la scène
                    Debug.Log("Particle System instancié.");
                }
            }
        }

        if (GUILayout.Button("Jouer le Particle System"))
        {
            if (_instantiatedVFX != null)
            {
                ParticleSystem instantiatedParticle = _instantiatedVFX.GetComponent<ParticleSystem>();
                if (instantiatedParticle != null)
                {
                    instantiatedParticle.Play();  // Jouer les particules
                    Debug.Log("Particle System joué.");
                }
            }
        }
    }
}
