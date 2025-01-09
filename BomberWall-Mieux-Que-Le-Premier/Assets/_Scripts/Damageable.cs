using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public event Action<GameObject> OnTakeDamage;

    public void ApplyDamage(GameObject killer)
    {
        OnTakeDamage?.Invoke(killer);
    }
}
