using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinGame : MonoBehaviour
{
    private bool _gameStarted;
    private PlayerMove _playerMove;

    public async void GameBegin(InputAction.CallbackContext _context) //Quand on appuie sur +
    {
        if (_context.performed)
        {
            if (_gameStarted) { return; }
            if (UIManager.Instance.Players.Count <= 1) { print("pas assez de joueur"); return; }
            _gameStarted = true;
            GameStart.Instance.Demarrage();
            await Task.Delay(3200); //Fin du 3,2,1,GO
            foreach (GameObject player in UIManager.Instance.Players)
            {
                player.TryGetComponent<PlayerMove>(out _playerMove);
                _playerMove.CanMove = true;
            }
        }
    }
}
