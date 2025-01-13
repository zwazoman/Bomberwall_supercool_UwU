using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStats : MonoBehaviour
{
    [SerializeField] int _life;
    [SerializeField] int _bomb;

    public void Start()
    {
        //BombHandler.OnBombPickUp += jsp;
    }

    public void jsp()
    {
        print("un truc");
    }
}
