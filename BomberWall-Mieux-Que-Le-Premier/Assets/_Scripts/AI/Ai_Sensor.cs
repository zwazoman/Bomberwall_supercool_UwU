using System;
using UnityEngine;

public class Ai_Sensor : MonoBehaviour
{
    public event Action<GameObject> OnBombNear;
    public event Action<GameObject> OnBombVeryNear;
    public event Action OnBombFar;
    public event Action OnPlayerInRange;

    [Header("Parameters")]
    [SerializeField] float _bombDetectionrange = 1;

    PlayerHealth _health;
    BombHandler _bombHandler;

    private void Awake()
    {
        TryGetComponent(out _health);
        TryGetComponent(out _bombHandler);
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _bombDetectionrange, LayerMask.GetMask("Bomb"));
        foreach (var item in hits)
        {
            if(item.gameObject.TryGetComponent<Bomb>(out Bomb bomb))
            {
                float bombDistance = (item.transform.position - transform.position).magnitude;
                if (bombDistance <= _bombDetectionrange / 2) OnBombVeryNear?.Invoke(bomb.gameObject); else OnBombNear?.Invoke(bomb.gameObject);
            }
            else
            {
                OnBombFar?.Invoke();
            }
        }
    }



}
