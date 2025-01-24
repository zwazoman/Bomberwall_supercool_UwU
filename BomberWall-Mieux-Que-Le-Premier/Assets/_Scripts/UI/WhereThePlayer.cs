using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhereThePlayer : MonoBehaviour //Script qui affiche une icon lorsque le joueur n'est plus visible à l'écran
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private GameObject _iconPrefab;
    [SerializeField] private Transform _uiParent;
    [SerializeField] private bool _swapFirstTwoColors;

    private Dictionary<GameObject, GameObject> _playerIcons = new();

    private void Start()
    {
        GameStart.Instance.GameStarted += InitializePlayerIcons;
    }

    private void InitializePlayerIcons() //On fait spawn des Icon pour chaque joueur 
    {
        for (int i = 0; i < UIManager.Instance.Players.Count; i++)
        {
            var player = UIManager.Instance.Players[i];
            var icon = Instantiate(_iconPrefab, _uiParent); //icon = Prefab Icon
            icon.SetActive(false);

            Color color = _swapFirstTwoColors switch //Cas de la scene IA, comme l'IA est présente de base elle est l'index 1 et donc bleu
            {
                true when i == 0 => Color.red,
                true when i == 1 => Color.blue,
                _ => GetColorForPlayer(i + 1),
            };

            SetIconColor(icon, color);
            _playerIcons[player] = icon;
        }
    }

    private void SetIconColor(GameObject icon, Color color) //On veut colorier les enfants du prefab (les images) /// Il aurait fallut séparé en 2 scripts pour être plus propres
    {
        foreach (var image in icon.GetComponentsInChildren<Image>())
        {
            image.color = color;
        }
    }

    private Color GetColorForPlayer(int index) => index switch //On applique la couleur de l'icon
    {
        1 => Color.blue,
        2 => Color.red,
        3 => Color.green,
        4 => Color.yellow,
        _ => throw new NotImplementedException() //cas autres
    };

    private void Update()
    {
        if (_playerIcons.Count == 0) return;

        var cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var (player, icon) in _playerIcons)
        {
            if (player == null) continue;

            var playerCollider = player.GetComponent<Collider>();
            if (playerCollider == null) continue;

            bool isVisible = GeometryUtility.TestPlanesAABB(cameraFrustum, playerCollider.bounds) && !IsObstructed(playerCollider);

            icon.SetActive(!isVisible);
            if (!isVisible)
            {
                UpdateIconPosition(icon, player.transform.position);
            }
        }
    }

    private bool IsObstructed(Collider player) //On affiche l'icon si les joueurs ne sont plus visible par la cam
    {
        Vector3 direction = (player.bounds.center - _camera.transform.position).normalized;
        float distance = Vector3.Distance(_camera.transform.position, player.bounds.center);
        return Physics.Raycast(_camera.transform.position, direction, distance, _obstacleLayer);
    }

    private void UpdateIconPosition(GameObject icon, Vector3 worldPosition)  //Update de la position de l'icon dans le monde
    {
        Vector3 screenPos = _camera.WorldToScreenPoint(worldPosition);
        screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
        screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

        icon.transform.position = new Vector3(screenPos.x - 15, screenPos.y + 50, screenPos.z);
    }
}
