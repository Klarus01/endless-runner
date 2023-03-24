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
        GameManager gm = GameManager.instance;

        coinsText.SetText(gm.coins.ToString());
        if (gm.highscore < gm.points)
        {
            gm.highscore = gm.points;
        }
        highscoreText.SetText("Highscore: " + gm.highscore);
    }
}
