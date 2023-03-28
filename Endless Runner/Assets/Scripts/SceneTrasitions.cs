using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrasitions : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (sceneName.Equals("Game"))
        {
            GameManager.instance.points = 0;
            GameManager.instance.pointsNeededToGetMoveSpeed = 10;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void DoExitGame()
    {
        Application.Quit();
    }
}
