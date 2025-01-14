using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerInputManager _playerInputManager;
    [SerializeField] private List<GameObject> _playersBar = new List<GameObject>();
    [SerializeField] private List<Transform> _playerSpawn = new List<Transform>();

    public Dictionary<GameObject, GameObject> UIPlayer;

    private void Awake()
    {
        if (_playersBar.Count != 4 || _playerSpawn.Count != 4 || _playerInputManager == null) { Debug.LogError("Il manque 1 ou plusieurs ref dans le script UIManager"); return; }
        foreach (GameObject PlayerUI in _playersBar) { PlayerUI.SetActive(false); }
        UIPlayer = new Dictionary<GameObject, GameObject>();
    }

    /// <summary>
    /// Fonction qui se joue au spawn d'un joueur
    /// </summary>
    public void PlayerSpawn()
    {
        for (int i = 0; i < _playerInputManager.playerCount; i++)
        {
            GameObject playerUI = _playersBar[i];
            Transform spawnPosition = _playerSpawn[i];
            playerUI.SetActive(true);
            GameObject newPlayer = _playerInputManager.playerPrefab;
            newPlayer.transform.position = spawnPosition.position;

            if (!UIPlayer.ContainsKey(newPlayer))
            {
                UIPlayer.Add(newPlayer, playerUI);
            }
        }
    }
}
