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

    [SerializeField]
    private Animation BombAnim;

    private BombHandler _bombHandler;
    private PlayerHealth _playerHealth;
    private int _tourboucle = 1;

    private float _interpolateColor = 0f;
    private bool _fadingToRed = true; 
    
    public GameObject Player;

    private void Start()
    {
        Player.TryGetComponent<BombHandler>(out _bombHandler);
        Player.TryGetComponent<PlayerHealth>(out _playerHealth);
        _bombHandler.OnBombPickUp += SetBombe;
        _bombHandler.OnBombEquipped += SetBombe;
        _playerHealth.OnDamageTook += SetHealth;
        SetBombe();
    }

    /// <summary>
    /// Fonction qui modiefie l'UI du joueur(bombe)
    /// </summary>
    public void SetBombe()
    {
        BombAnim.Play();
        _textBombe.text = "X" + _bombHandler.BombsPossessedCount.ToString();
    }

    // Si le joueur n'a plus de bombes...
    public void Update()
    {
        if (_textBombe.text == "X" + "0")
        {
            _interpolateColor += (_fadingToRed ? 1 : -1) * 1.7f * Time.deltaTime;

            _interpolateColor = Mathf.Clamp01(_interpolateColor);

            _textBombe.color = Color.Lerp(Color.white, new Color(1f, 0.5f, 0.5f), _interpolateColor);

            if (_interpolateColor == 1f) _fadingToRed = false;
            if (_interpolateColor == 0f) _fadingToRed = true;
        }
        else { _textBombe.color = Color.white; }

    }
    /// <summary>
    /// Fonction qui modiefie l'UI du joueur (vie)
    /// </summary>
    public void SetHealth()
    {
        _heartImage[_heartImage.Count - _tourboucle].color = Color.black;
        _tourboucle++;
    }
}
