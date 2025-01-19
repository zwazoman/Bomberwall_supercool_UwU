using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    [SerializeField] GameObject _panel;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _panel.SetActive(false);
        }
    }
}
