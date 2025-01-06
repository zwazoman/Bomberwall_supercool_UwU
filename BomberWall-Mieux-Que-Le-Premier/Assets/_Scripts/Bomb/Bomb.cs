using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _lifeTime;

     float _timer = 0;

    /// <summary>
    /// gère l'explosion
    /// </summary>
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _lifeTime)
        {
            Explode();
        }
    }

    /// <summary>
    /// pousse la bombe dans la direction donnée
    /// </summary>
    /// <param name="direction"></param>
    public void Push(Vector3 direction)
    {

    }

    /// <summary>
    /// envoie des spherecasts autour de la bombe ingfligeant des dégats aux joueurs touchés
    /// </summary>
    public void Explode()
    {

    }

    private void OnDisable()
    {
        _timer = 0;
    }
}
