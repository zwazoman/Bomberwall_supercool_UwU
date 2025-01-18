using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private PlayerInputManager _playerManager;

    public static GameStart Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Lance le compte à rebours de démarrage du jeu.
    /// </summary>
    public async void Demarrage()
    {
        _playerManager.DisableJoining(); // Empêche les nouveaux joueurs de rejoindre
        _infoText.gameObject.SetActive(false);
        _timerText.gameObject.SetActive(true);
        string[] countdownMessages = { "3", "2", "1", "GO !" };
        const int delay = 800;
        foreach (string message in countdownMessages)
        {
            _timerText.text = message;
            await Task.Delay(delay);
        }
        _timerText.gameObject.SetActive(false);
    }
}
