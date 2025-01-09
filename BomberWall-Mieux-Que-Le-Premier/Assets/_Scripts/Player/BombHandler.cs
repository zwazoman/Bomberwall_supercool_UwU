using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float _bombThrowVerticalForce = 1;
    [SerializeField] float _bombThrowHorizontalForce = 1;
    [SerializeField] float _kickForce = 1;
    [SerializeField] float _kickZoneSize = 1;

    [Header("References")]
    [SerializeField] Transform _bombEquipSocket;
    [SerializeField] Transform _bombDropSocket;
    [SerializeField] Transform _kickSocket;

    private bool _bombsEquipped = false;
    public int _BombsPossessedCount = 0;
    GameObject _currentBombEquipped;

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
            GameObject bomb = PoolManager.Instance.AccessPool(Pools.Bomb).TakeFromPool(transform);
            _currentBombEquipped = bomb;
            _currentBombEquipped.GetComponent<Rigidbody>().isKinematic = true;
            _currentBombEquipped.GetComponent<Collider>().enabled = false;
            _currentBombEquipped.transform.position = _bombEquipSocket.position;
            //visuels et tout
        }
        else
        {
            print("No Bombs");
            //juice
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
        Collider[] hits = Physics.OverlapSphere(_kickSocket.position, _kickZoneSize, LayerMask.GetMask("Bomb"));
        foreach (Collider coll in hits)
        {
            coll.GetComponent<Bomb>().Push(transform.forward * _kickForce);
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
        _currentBombEquipped.transform.position = _bombDropSocket.position;
        _currentBombEquipped.GetComponent<Rigidbody>().isKinematic = false;
        _currentBombEquipped.GetComponent<Collider>().enabled = true;
        _currentBombEquipped.transform.parent = null;
        _currentBombEquipped = null;
        //visuels et tout
    }

    /// <summary>
    /// lance la bombe a une distance donnée
    /// </summary>
    public void Throw()
    {
        _bombsEquipped = false;
        _currentBombEquipped.GetComponent<Rigidbody>().isKinematic = false;
        _currentBombEquipped.GetComponent<Collider>().enabled = true;
        _currentBombEquipped.transform.parent = null;
        Vector3 throwVector = (transform.forward * _bombThrowHorizontalForce) + (Vector3.up * _bombThrowVerticalForce);
        _currentBombEquipped.GetComponent<Bomb>().Push(throwVector);
        _currentBombEquipped = null;
        //visuels et tout
    }
}
