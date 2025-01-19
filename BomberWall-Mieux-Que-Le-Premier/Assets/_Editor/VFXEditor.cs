using UnityEditor;
using UnityEngine;

public class VFXEditor : EditorWindow
{
    private ParticleSystem _particleSystem;
    private Vector2 _scrollPosition;
    private bool[] _windowStates;  // Pour g�rer l'�tat de chaque fen�tre foldout (repli�e/d�pli�e)
    private GameObject _instantiatedVFX;  // Instance du ParticleSystem dans la sc�ne

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

        //----------------------------------------Contents (Cas o� aucun syst�me de particules n'est s�lectionn�)-------------------------------------------
        GUILayout.Label("VFX Editor", titleStyle);
        GUILayout.Space(20);

        _particleSystem = (ParticleSystem)EditorGUILayout.ObjectField("Particle System", _particleSystem, typeof(ParticleSystem), false);
        GUILayout.Space(10);

        if (_particleSystem == null)
        {
            EditorGUILayout.HelpBox("Un particule system est n�cc�ssaire pour pouvoir continuer", MessageType.Info);
            return;
        }

        //-----------------------------------------Contents (Syst�me de particules s�lectionn�)------------------------------------------

        // Cr�e un tableau de bool�ens pour chaque script attach� au syst�me de particules
        MonoBehaviour[] scripts = _particleSystem.GetComponents<MonoBehaviour>();

        if (_windowStates == null || _windowStates.Length != scripts.Length)
            _windowStates = new bool[scripts.Length];

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(400));  // On cr�e la fen�tre d�filable

        for (int i = 0; i < scripts.Length; i++)
        {
            MonoBehaviour script = scripts[i];
            if (script == null) continue;

            _windowStates[i] = EditorGUILayout.Foldout(_windowStates[i], script.GetType().Name);  // On affiche le nom du script avec la possibilit� de replier/ouvrir

            if (_windowStates[i])
            {
                SerializedObject serializedScript = new SerializedObject(script);  // S�rialise le script
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

        //---------------------------------------- Boutons et actions suppl�mentaires ---------------------------------------
        GUILayout.Space(10);

        if (GUILayout.Button("Instancier le Particle System"))
        {
            if (_particleSystem != null)
            {
                if (_instantiatedVFX == null)
                {
                    _instantiatedVFX = Instantiate(_particleSystem.gameObject, Vector3.zero, Quaternion.identity);
                    _instantiatedVFX.transform.position = new Vector3(1, 3, 0);
                    _instantiatedVFX.SetActive(true);  // On active l'objet instanci�
                    Selection.activeGameObject = _instantiatedVFX;  // S�lectionner l'instance dans la sc�ne
                    Debug.Log("Particle System instanci�.");
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
                    Debug.Log("Particle System jou�.");
                }
            }
        }
    }
}
