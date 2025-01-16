using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textBombe;

    [SerializeField]
    private List<Image> _heartImage = new List<Image>();

    private  int _nombrePV = 3;
    private BombHandler _bombHandler;
    private Damageable _damageable;

    [HideInInspector] public GameObject Player;

    private void Start()
    {
        Player.TryGetComponent<BombHandler>(out _bombHandler);
        Player.TryGetComponent<Damageable>(out _damageable);
        _bombHandler.OnBombPickUp += SetBombe;
        _bombHandler.OnBombDropped += SetBombe;
        _bombHandler.OnThrow += SetBombe;
        _damageable.OnTakeDamage += SetHealth;
        SetBombe();
    }

    /// <summary>
    /// Fonction qui modiefie l'UI du joueur(bombe)
    /// </summary>
    public void SetBombe()
    {
        _textBombe.text = "X" + _bombHandler.BombsPossessedCount.ToString();
    }

    /// <summary>
    /// Fonction qui modiefie l'UI du joueur (vie)
    /// </summary>
    public void SetHealth(GameObject Apolish)
    {
        _heartImage[_heartImage.Count - 1].color = Color.black;
        _nombrePV--;
    }
}
