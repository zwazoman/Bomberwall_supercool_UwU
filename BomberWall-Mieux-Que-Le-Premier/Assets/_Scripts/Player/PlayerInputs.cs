using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 MoveVector;

    private PlayerMain _main;

    private void Awake()
    {
        TryGetComponent<PlayerMain>(out _main);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            MoveVector = ctx.ReadValue<Vector2>();
        }
    }

    public void OnKick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _main.Bomb.Kick();
        }
    }

    public void OnEquip(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _main.Bomb.Equip();
        }
    }

}
