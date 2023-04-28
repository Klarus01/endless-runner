using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private bool[] isUnlocked;
    [SerializeField] private int[] upgradeLevel;
    [SerializeField] private ShopItem[] shopItems;
    [SerializeField] private Material[] materials;
    [SerializeField] private TMP_Text coinsText;

    [SerializeField] private Button exitButton;
    [SerializeField] private Button[] buyButtons;
    [SerializeField] private Button[] changeColorButtons;

    private void Awake()
    {
        Load();
        SetUpButtons();

        for (int i = 0; i < shopItems.Length; i++)
        {
            CheckIfAlreadyBought(shopItems[i]);
        }
    }

    private void SetUpButtons()
    {
        exitButton.onClick.AddListener(BackToMenu);
        for (int i = 0; i < buyButtons.Length; i++)
        {
            int index = i;
            if (i < 4)
            {
                buyButtons[index].onClick.AddListener(() => BuyColor(shopItems[index]));
            }
            else
            {
                buyButtons[index].onClick.AddListener(() => BuyUpgrade(shopItems[index]));
            }
        }

        for (int j = 0; j < changeColorButtons.Length; j++)
        {
            int index = j;
            changeColorButtons[index].onClick.AddListener(() => SetColor(shopItems[index].id));
        }
    }

    private void BackToMenu()
    {
        GameManager.Instance.OnSceneTransition(1);
    }

    private void CheckIfAlreadyBought(ShopItem shopItem)
    {
        if (isUnlocked[shopItem.id])
        {
            shopItem.GetComponent<Button>().enabled = true;
            shopItem.buyButton.SetActive(false);
        }
        else
        {
            shopItem.priceText.SetText(shopItem.price[upgradeLevel[shopItem.id]].ToString());
        }
        coinsText.SetText(GameManager.Instance.coins.ToString());
    }

    private void BuyColor(ShopItem shopItem)
    {
        if (GameManager.Instance.coins >= shopItem.price[0])
        {
            GameManager.Instance.coins -= shopItem.price[0];
            isUnlocked[shopItem.id] = true;
            CheckIfAlreadyBought(shopItem);
            Save();
        }
    }

    private void SetColor(int input)
    {
        GameManager.Instance.playerStats.selectedMaterial = materials[input];
    }

    private void BuyUpgrade(ShopItem shopItem)
    {
        if (GameManager.Instance.coins >= shopItem.price[upgradeLevel[shopItem.id]])
        {
            GameManager.Instance.coins -= shopItem.price[upgradeLevel[shopItem.id]];
            upgradeLevel[shopItem.id]++;
            Upgrades(shopItem);
            if (upgradeLevel[shopItem.id].Equals(shopItem.maxTierUpgrade))
            {
                isUnlocked[shopItem.id] = true;
            }
            CheckIfAlreadyBought(shopItem);
            Save();
        }
    }

    private void Upgrades(ShopItem shopItem)
    {
        if (shopItem.id.Equals(4)) //Speed
        {
            GameManager.Instance.playerStats.maxMoveSpeed += .5f;
        }
        if (shopItem.id.Equals(5)) //Rotate
        {
            GameManager.Instance.playerStats.maxRotateSpeed += 5f;
        }
        if (shopItem.id.Equals(6)) //Health
        {
            GameManager.Instance.playerStats.maxHealth += 1;
        }
        if (shopItem.id.Equals(7)) //Coins
        {
            GameManager.Instance.playerStats.coinValue += 1;
        }
    }

    private void Save()
    {
        SaveShopItems save = new()
        {
            isUnlocked = isUnlocked,
            upgradeLevel = upgradeLevel,
        };
        string json = JsonUtility.ToJson(save);

        File.WriteAllText(Application.dataPath + "/saveShopItem.txt", json);
    }

    private void Load()
    {
        if (File.Exists(Application.dataPath + "/saveShopItem.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/saveShopItem.txt");

            SaveShopItems saveObject = JsonUtility.FromJson<SaveShopItems>(saveString);

            isUnlocked = saveObject.isUnlocked;
            upgradeLevel = saveObject.upgradeLevel;
        }
    }
}

public class SaveShopItems
{
    public bool[] isUnlocked;
    public int[] upgradeLevel;
}