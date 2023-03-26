using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] colorsLocked;
    public GameObject[] buyButtons;
    public Material[] materials;
    [SerializeField]
    private TMP_Text coinsText;
    public int[] priceOfColors;

    public void BuyColor(int input)
    {
        if (GameManager.instance.coins >= priceOfColors[input])
        {
            colorsLocked[input].SetActive(false);
            buyButtons[input].SetActive(false);

        }
    }

    public void SetColor(int input)
    {
        GameManager.instance.selectedMaterial = materials[input];
    }
}
