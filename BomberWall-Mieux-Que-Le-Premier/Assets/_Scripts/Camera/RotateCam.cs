using UnityEngine;

public class RotateCam : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // Vitesse en degrés par seconde

    void Update()
    {
        // Ajoute de la rotation uniquement sur l'axe Y
        Vector3 currentEulerAngles = transform.eulerAngles;
        currentEulerAngles.y += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = currentEulerAngles;
    }
}
