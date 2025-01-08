using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerMain _main;

    private void Awake()
    {
        TryGetComponent<PlayerMain>(out _main);
    }

    /// <summary>
    /// inputs de déplacement du joueur
    /// </summary>
    /// <param name="ctx"></param>
    public void OnMove(InputAction.CallbackContext ctx)
    {
        _main.Move.MoveInput = ctx.ReadValue<Vector2>();
    }

    /// <summary>
    /// inputs de coup de pied / de lancé du joueur
    /// </summary>
    /// <param name="ctx"></param>
    public void OnKick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _main.Bomb.Kick();
        }
    }

    /// <summary>
    /// input d'équipement / de déposage du joueur
    /// </summary>
    /// <param name="ctx"></param>
    public void OnEquip(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _main.Bomb.Equip();
        }
    }

}
