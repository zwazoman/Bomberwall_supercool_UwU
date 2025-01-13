using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ScenePVE()
    {
        SceneManager.LoadScene("PVE");
    }

    public void ScenePVP()
    {
        SceneManager.LoadScene("PVP");
    }

    public void QuitGame()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}
