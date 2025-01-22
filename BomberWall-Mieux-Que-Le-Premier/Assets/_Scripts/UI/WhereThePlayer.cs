using System.Collections.Generic;
using UnityEngine;

public class WhereThePlayer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _detectionLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private GameObject _iconPrefab;
    [SerializeField] private Transform _uiParent;

    private Dictionary<Collider, GameObject> _playerIcons = new();

    private void Start()
    {
        GameStart.Instance.GameStarted += InitializePlayerIcons;
    }

    private void InitializePlayerIcons() //Création des fleches
    {
        foreach (var player in Physics.OverlapSphere(_camera.transform.position, 100f, _detectionLayer))
        {
            var icon = Instantiate(_iconPrefab, _uiParent);
            icon.SetActive(false);
            _playerIcons[player] = icon;
        }
    }

    private void Update()
    {
        if (_playerIcons.Count == 0) return;

        Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var entry in _playerIcons)
        {
            var player = entry.Key;
            var icon = entry.Value;

            if (player == null) continue;

            bool isVisible = GeometryUtility.TestPlanesAABB(cameraFrustum, player.bounds) && !IsObstructed(player); // Vérifie la visibilité du joueur

            icon.SetActive(!isVisible);
            if (!isVisible)
            {
                UpdateIconPosition(icon, player.transform.position);
            }
        }
    }

    private bool IsObstructed(Collider player)//Si il est derriere un mur je veux afficher le joueur
    {
        Vector3 direction = (player.bounds.center - _camera.transform.position).normalized;
        float distance = Vector3.Distance(_camera.transform.position, player.bounds.center);
        return Physics.Raycast(_camera.transform.position, direction, distance, _obstacleLayer);
    }

    private void UpdateIconPosition(GameObject icon, Vector3 worldPosition)
    {
        Vector3 screenPos = _camera.WorldToScreenPoint(worldPosition);

        screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
        screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

        icon.transform.position = screenPos;
    }
}
