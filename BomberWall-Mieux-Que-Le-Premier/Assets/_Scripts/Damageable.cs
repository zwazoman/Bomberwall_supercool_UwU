using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public event Action<GameObject> OnTakeDamage;

    public void ApplyDamage(GameObject killer)
    {
        OnTakeDamage?.Invoke(killer);
    }
}
