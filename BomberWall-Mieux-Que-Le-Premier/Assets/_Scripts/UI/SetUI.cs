using TMPro;
using UnityEngine;

public class SetUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textBombe;

    [SerializeField]
    private  int _nombrePV;

    private BombHandler _bombHandler;

    [HideInInspector] public GameObject Player;

    private void Start()
    {
        Player.TryGetComponent<BombHandler>(out _bombHandler);
        _bombHandler.OnBombPickUp += SetBombe;
        _bombHandler.OnBombDropped += SetBombe;
        _bombHandler.OnThrow += SetBombe;
        SetBombe();
    }

    /// <summary>
    /// Fonction qui modiefi l'UI du joueur
    /// </summary>
    public void SetBombe()
    {
        _textBombe.text = "X" + _bombHandler.BombsPossessedCount.ToString();
    }
}
