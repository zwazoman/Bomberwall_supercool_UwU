using Cinemachine;
using TMPro;
using UnityEngine;

public class GiveRefToPlayer : MonoBehaviour
{
    public static GiveRefToPlayer Instance;

    public TextMeshProUGUI _text; //Text de victoire utlis� dans EndGame
    public CinemachineVirtualCamera _camera; //Anim de la cam�ra utlis� dans EndGame

    private void Awake()
    {
        Instance = this;
    }
}
