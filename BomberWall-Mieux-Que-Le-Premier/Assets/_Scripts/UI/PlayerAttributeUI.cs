using UnityEngine;

public class PlayerAttributeUI : MonoBehaviour
{
    private GameObject playerUI;
    private SetUI _setUI;

    public void AssignUI(GameObject ui)
    {
        ui.TryGetComponent<SetUI>(out _setUI);
        playerUI = ui;
        _setUI.Player = gameObject;
        playerUI.SetActive(true);
    }
}
