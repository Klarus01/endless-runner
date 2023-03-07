using TMPro;
using UnityEngine;

public class SetupMenuPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text coinsText;
    [SerializeField]
    private TMP_Text highscoreText;

    private void Awake()
    {
        coinsText.SetText(GameManager.instance.coins.ToString());
        if (GameManager.instance.highscore < GameManager.instance.points)
        {
            GameManager.instance.highscore = GameManager.instance.points;
        }
        highscoreText.SetText("Highscore: " + GameManager.instance.highscore);
    }
}
