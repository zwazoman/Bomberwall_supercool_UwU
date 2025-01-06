using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _lifeTime;

     float _timer = 0;

    /// <summary>
    /// g�re l'explosion
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
    /// pousse la bombe dans la direction donn�e
    /// </summary>
    /// <param name="direction"></param>
    public void Push(Vector3 direction)
    {

    }

    /// <summary>
    /// envoie des spherecasts autour de la bombe ingfligeant des d�gats aux joueurs touch�s
    /// </summary>
    public void Explode()
    {

    }

    private void OnDisable()
    {
        _timer = 0;
    }
}
