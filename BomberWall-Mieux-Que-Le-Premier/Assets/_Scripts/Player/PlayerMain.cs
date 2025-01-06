using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [field : SerializeField]
    public PlayerInputs Inputs { get; private set; }

    [field : SerializeField]
    public PlayerMove Move { get; private set; }

    [field : SerializeField]
    public BombHandler Bomb { get; private set; }
}
