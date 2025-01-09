using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Variables
    [Header("Speed Settings")]
    [Tooltip("Vitesse de base d'un joueur, vitesse conseillée 4")]
    [SerializeField] private float _initialSpeed;

    [Tooltip("Vitesse maximun d'un joueur, vitesse conseillée 7")]
    [SerializeField] private float _maxSpeed;

    [Tooltip("La vitesse maximum sera atteinte après ce delay, vitesse conseillée 3")]
    [SerializeField] private float _timeToReachMaxSpeed;

    [Tooltip("Temps pour que le joueur perde sa vitesse (frein), plus le chiffre est élevée plus il ralentira vite, vitesse conseillée 30")]
    [SerializeField] private float _decelerationDelay;

    [Header("Rotation Settings")]
    [Tooltip("Vitesse pour que le joueur tourne la tête, vitesse conseillée 10")]
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("Physics")]
    [Tooltip("C'est juste un RigidBody mdr")]
    [SerializeField] private Rigidbody _rb;

    private Vector3 _moveDirection;
    private float _currentSpeed;
    private float _accelerationTimer;

    [HideInInspector] public Vector2 MoveInput;
    #endregion

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    /// <summary>
    /// Fonction qui doit gérer le déplacement du joueur, elle s'occupe également d'appliquer une accélération ou une décélération sur la vitesse du joueur
    /// </summary>
    private void Movement()
    {
        if (MoveInput.sqrMagnitude > 0.01f) //accélération
        {
            _moveDirection = new Vector3(MoveInput.x, 0, MoveInput.y).normalized;
            _accelerationTimer += Time.fixedDeltaTime;
            float accelerationDelay = Mathf.Clamp01(_accelerationTimer / _timeToReachMaxSpeed);
            _currentSpeed = Mathf.Lerp(_initialSpeed, _maxSpeed, accelerationDelay);
        }
        else //décélération
        {
            _accelerationTimer = 0f;
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0f, _decelerationDelay * Time.fixedDeltaTime);
        }

        Vector3 vitesse = _moveDirection * _currentSpeed;
        _rb.velocity = new Vector3(vitesse.x, _rb.velocity.y, vitesse.z);
    }

    /// <summary>
    /// Fonction qui modifie la rotation du joueur selon l'input du joueur
    /// </summary>
    private void Rotation()
    {
        if (MoveInput.sqrMagnitude < 0.01f) { return; }
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);

    }
}
