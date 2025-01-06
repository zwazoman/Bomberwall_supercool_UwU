using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _inputSmoothDamp = 0.3f;
    [SerializeField] private float _smoothSpeed = 0.2f;
    [SerializeField] private PlayerInputs _playerVector;
    [SerializeField] private CharacterController _character;

    private Vector3 _playerVelocity;
    private Vector2 _currentInputVector;
    private Vector2 _smoothInputValue;

    private void Update()
    {
        if (_playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector2 inputVector = _playerVector.MoveVector;

        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, inputVector, ref _smoothInputValue, _smoothSpeed, _inputSmoothDamp);

        Vector3 move = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);

        _character.Move(move * Time.deltaTime * _playerSpeed);

        if (inputVector != Vector2.zero)
        {
            float TargetAngle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, TargetAngle, 0);
        }
    }
}
