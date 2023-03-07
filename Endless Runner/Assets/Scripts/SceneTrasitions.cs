using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrasitions : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (sceneName.Equals("Game"))
        {
            GameManager.instance.points = 0;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void doExitGame()
    {
        Application.Quit();
        Debug.Log("XD");
    }
}
