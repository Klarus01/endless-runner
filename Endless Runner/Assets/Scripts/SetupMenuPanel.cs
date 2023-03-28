using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupMenuPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private GameObject playerColor;

    private void Awake()
    {
        coinsText.SetText(GameManager.instance.coins.ToString());
        highscoreText.SetText("Highscore: " + GameManager.instance.highscore);
        playerColor.GetComponent<Image>().color = GameManager.instance.selectedMaterial.color;
    }
}
