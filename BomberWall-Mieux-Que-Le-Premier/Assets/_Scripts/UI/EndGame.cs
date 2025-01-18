using Cinemachine;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    //La caméra de fin
    private CinemachineVirtualCamera _virtualCamera;
    //-------------------
    private TextMeshProUGUI _textVictoire;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        TryGetComponent<PlayerHealth>(out _playerHealth);
        _playerHealth.OnDeath += VerificationVictory;
    }

    private void Start()
    {
        _textVictoire = GiveRefToPlayer.Instance._text;
        _virtualCamera = GiveRefToPlayer.Instance._camera;
    }

    public async void VerificationVictory()
    {
        if (UIManager.Instance.Players.Count <= 1)
        {
            _textVictoire.gameObject.SetActive(true);
            _virtualCamera.gameObject.SetActive(true);
            _virtualCamera.Follow = UIManager.Instance.Players[0].transform;
            _virtualCamera.LookAt = UIManager.Instance.Players[0].transform;
            await Task.Delay(6000);
            SceneManager.LoadScene("Menu");
        }
    }
}
