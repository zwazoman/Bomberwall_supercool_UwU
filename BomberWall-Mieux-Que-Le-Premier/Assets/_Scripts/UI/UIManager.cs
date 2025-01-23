using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script qui g�re "l'individualisme" de l'UI selon le joueur
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private List<Transform> _playerSpawn = new List<Transform>();
    [SerializeField] private GameObject[] _uiPrefabs; // Tableau de pr�fab UI (J1, J2, J3, J4)
    [SerializeField] private CanvasRenderer _canvas; // R�f�rence au panelPlayerLife dans la sc�ne
    [SerializeField] private List<Material> _materialsList = new List<Material>();

    public List<GameObject> Players = new List<GameObject>();

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        if (_playerInputManager == null || _playerSpawn.Count == 0 || _uiPrefabs.Length != 4) { return; }
    }

    private void Start()
    {
        _playerInputManager.onPlayerJoined += OnPlayerJoined;
    }

    /// <summary>
    /// A chaque fois que un joueur appara�t
    /// </summary>
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        int playerIndex = playerInput.playerIndex;
        print(Players.Count);
        Players.Add(playerInput.gameObject);

        if (playerIndex >= _playerSpawn.Count) { return; }

        Transform spawnPosition = _playerSpawn[playerIndex];
        playerInput.transform.position = spawnPosition.position;

        if (playerIndex < _uiPrefabs.Length)
        {
            GameObject uiInstance = Instantiate(_uiPrefabs[playerIndex], _canvas.transform); //On fait spawn l'ui du joueur lorsque il apparait
            PlayerAttributeUI playerUIComponent = playerInput.GetComponent<PlayerAttributeUI>();
            if (playerUIComponent != null)
            {
                playerUIComponent.AssignUI(uiInstance);
            }
        }
        playerInput.gameObject.GetComponent<MeshRenderer>().material = _materialsList[Players.Count - 1];
    }
}
