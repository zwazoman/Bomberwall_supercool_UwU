using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    [SerializeField] private float _bombThrowForce;

    private bool _bombsEquipped = false;
    private int _BombsPossessedCount = 0;

    /// <summary>
    /// bombe ramassée
    /// </summary>
    public void Pickup()
    {
        _BombsPossessedCount++;
        //updateUi
        //Juice
    }

    /// <summary>
    /// la bombe est équipée au dessus de la tête du joueur et commence a ticker
    /// </summary>
    public void Equip()
    {
        if (_bombsEquipped)
        {
            Drop();
            return;
        }
        if(_BombsPossessedCount > 0)
        {
            _BombsPossessedCount--;
            _bombsEquipped = true;
            //visuels et tout
        }
    }

    /// <summary>
    /// envoie un spherecast devant le joueur et pousse une bombe si elle est touchée
    /// </summary>
    public void Kick()
    {
        if (_bombsEquipped)
        {
            Throw();
            return;
        }
        //visuels et tout
        //overlapsphere pour taper la bombe avec
    }

    /// <summary>
    /// dépose la bombe aux pieds du joueur
    /// </summary>
    public void Drop()
    {
        _bombsEquipped = false;
        //visuels et tout
    }

    /// <summary>
    /// lance la bombe a une distance donnée
    /// </summary>
    public void Throw()
    {
        _bombsEquipped = false;
        //visuels et tout
    }
}
