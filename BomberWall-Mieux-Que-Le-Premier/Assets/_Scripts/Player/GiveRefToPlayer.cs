using Cinemachine;
using TMPro;
using UnityEngine;

public class GiveRefToPlayer : MonoBehaviour
{
    public static GiveRefToPlayer Instance;

    public TextMeshProUGUI _text; //Text de victoire utlisé dans EndGame
    public CinemachineVirtualCamera _camera; //Anim de la caméra utlisé dans EndGame

    private void Awake()
    {
        Instance = this;
    }
}
