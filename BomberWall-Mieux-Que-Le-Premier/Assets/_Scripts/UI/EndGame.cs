using UnityEngine;

public class EndGame : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        TryGetComponent<PlayerHealth>(out _playerHealth);
        _playerHealth.OnDeath += VerificationVictory;
    }

    private void Start()
    {
        
    }

    public void VerificationVictory()
    {
        if (UIManager.Instance.Players.Count <= 1)
        {
            print("victoire");
        }
    }
}
