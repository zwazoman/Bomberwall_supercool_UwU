using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public bool BombEquipped = false;
    public int BombsInInventory = 0;

    public void Pickup()
    {
        BombsInInventory++;
        //updateUi
        //Juice
    }

    public void Equip()
    {
        if (BombEquipped)
        {
            Drop();
            return;
        }
        if(BombsInInventory > 0)
        {
            BombEquipped = true;
            //visuels et tout
        }
    }

    public void Kick()
    {
        if (BombEquipped)
        {
            Throw();
            return;
        }
        //visuels et tout
        //overlapsphere pour taper la bombe avec
    }

    public void Drop()
    {
        BombEquipped = false;
        //visuels et tout
    }

    public void Throw()
    {
        BombEquipped = false;
        //visuels et tout
    }
}
