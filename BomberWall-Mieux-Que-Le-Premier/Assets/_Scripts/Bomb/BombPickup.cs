using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<BombHandler>(out BombHandler bombHandler))
        {
            bombHandler.Pickup();
            //BacktoPool
        }
    }
}
