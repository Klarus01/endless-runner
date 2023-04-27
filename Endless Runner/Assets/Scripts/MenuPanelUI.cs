using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private Image playerColor;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() => LoadScene(0));
        shopButton.onClick.AddListener(() => LoadScene(2));
        exitButton.onClick.AddListener(ExitGame);

        coinsText.SetText(GameManager.Instance.coins.ToString());
        highscoreText.SetText("Highscore: " + GameManager.Instance.highscore);
        playerColor.color = GameManager.Instance.playerStats.selectedMaterial.color;
    }

    private void LoadScene(int sceneID)
    {
        GameManager.Instance.OnSceneTransition(sceneID);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
