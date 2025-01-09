using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public event Action OnTakeDamage;

    public void ApplyDamage()
    {
        OnTakeDamage?.Invoke();
    }
}
