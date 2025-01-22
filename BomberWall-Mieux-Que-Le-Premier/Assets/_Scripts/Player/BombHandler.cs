using System;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public event Action OnBombEquipped;
    public event Action OnBombDropped;
    public event Action OnBombPickUp;

    public event Action OnThrow;

    public event Action OnHit;

    [Header("Parameters")]
    [SerializeField] float _bombThrowVerticalForce = 1;
    [SerializeField] float _bombThrowHorizontalForce = 1;
    [SerializeField] float _kickForce = 1;
    [SerializeField] float _kickZoneSize = 1;

    [Header("References")]
    [SerializeField] Transform _bombEquipSocket;
    [SerializeField] Transform _bombDropSocket;
    [SerializeField] Transform _kickSocket;

    GameObject _currentBombEquipped;

    public bool HasBombsEquipped = false;
    public int BombsPossessedCount { get; private set; }

    /// <summary>
    /// bombe ramassée
    /// </summary>
    public void Pickup()
    {
        print("Pickup");
        BombsPossessedCount++;
        OnBombPickUp?.Invoke();
    }

    /// <summary>
    /// la bombe est équipée au dessus de la tête du joueur et commence a ticker
    /// </summary>
    public void Equip()
    {
        print("hm?");
        if (HasBombsEquipped)
        {
            Drop();
            return;
        }
        if(BombsPossessedCount > 0)
        {
            BombsPossessedCount--;
            HasBombsEquipped = true;
            GameObject bomb = PoolManager.Instance.AccessPool(Pools.Bomb).TakeFromPool(transform);
            _currentBombEquipped = bomb;
            _currentBombEquipped.GetComponent<Rigidbody>().isKinematic = true;
            _currentBombEquipped.GetComponent<Collider>().enabled = false;
            _currentBombEquipped.transform.position = _bombEquipSocket.position;

            OnBombEquipped?.Invoke();
            //visuels et tout
        }
        else
        {
            print("No Bombs");
        }
    }

    /// <summary>
    /// envoie un spherecast devant le joueur et pousse une bombe si elle est touchée
    /// </summary>
    public void Hit()
    {
        if (HasBombsEquipped)
        {
            Throw();
            return;
        }
        Collider[] hits = Physics.OverlapSphere(_kickSocket.position, _kickZoneSize, LayerMask.GetMask("Bomb"));
        foreach (Collider coll in hits)
        {
            coll.GetComponent<Bomb>().Push(transform.forward * _kickForce);
        }
        OnHit?.Invoke();
        //visuels et tout
    }

    /// <summary>
    /// dépose la bombe aux pieds du joueur
    /// </summary>
    public void Drop()
    {
        BombDropped();
        _currentBombEquipped.transform.position = _bombDropSocket.position;
        _currentBombEquipped = null;

        //visuels et tout
    }

    /// <summary>
    /// lance la bombe a une distance donnée
    /// </summary>
    public void Throw()
    {
        if (!HasBombsEquipped) return;

        BombDropped();
        Vector3 throwVector = (transform.forward * _bombThrowHorizontalForce) + (Vector3.up * _bombThrowVerticalForce);
        _currentBombEquipped.GetComponent<Bomb>().Push(throwVector);
        _currentBombEquipped = null;

        OnThrow?.Invoke();
        //visuels et tout
    }

    public void BombDropped()
    {
        HasBombsEquipped = false;
        if(_currentBombEquipped != null)
        {
            _currentBombEquipped.GetComponent<Rigidbody>().isKinematic = false;
            _currentBombEquipped.GetComponent<Collider>().enabled = true;
            _currentBombEquipped.transform.parent = null;
        }

        OnBombDropped?.Invoke();
    }

}
