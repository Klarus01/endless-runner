using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    private bool isUnlocked = false;
    [SerializeField] private int currentTierUpgrade = 0;
    [SerializeField] private int maxTierUpgrade;
    [SerializeField] private int[] price;
    [SerializeField] private int id;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private TMP_Text priceText;

    private void Awake()
    {
        Load();
        CheckIfAlreadyBought();
    }

    private void CheckIfAlreadyBought()
    {
        if (isUnlocked)
        {
            GetComponent<Button>().enabled = true;
            buyButton.SetActive(false);
        }
        priceText.SetText(price[currentTierUpgrade].ToString());
    }

    public void BuyColor()
    {
        if (GameManager.instance.coins >= price[0])
        {
            GameManager.instance.coins -= price[0];
            isUnlocked = true;
            CheckIfAlreadyBought();
            Save();
        }
    }

    public void BuyUpgrade()
    {
        if (GameManager.instance.coins >= price[currentTierUpgrade])
        {
            GameManager.instance.coins -= price[currentTierUpgrade];
            Upgrades();
            if (currentTierUpgrade.Equals(maxTierUpgrade))
            {
                isUnlocked = true;
            }
            else
            {
                currentTierUpgrade++;
            }
            CheckIfAlreadyBought();
            Save();
        }
    }

    private void Upgrades()
    {
        if (id.Equals(4)) //speed
        {
            GameManager.instance.moveSpeed += 2;
        }
        else if (id.Equals(5)) //rotate
        {
            Debug.Log("rotate");
        }
        else if (id.Equals(6)) //health
        {
            Debug.Log("health");
        }
        else if (id.Equals(7)) //money
        {
            Debug.Log("money");
        }
    }

    public void Save()
    {
        SaveShopItem save = new()
        {
            isUnlocked = isUnlocked,
            currentTierUpgrade = currentTierUpgrade,
        };
        string json = JsonUtility.ToJson(save);

        File.WriteAllText(Application.dataPath + "/saveShopItem" + id + ".txt", json);
    }

    private void Load()
    {
        if (File.Exists(Application.dataPath + "/saveShopItem" + id + ".txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/saveShopItem" + id + ".txt");

            SaveShopItem saveObject = JsonUtility.FromJson<SaveShopItem>(saveString);

            isUnlocked = saveObject.isUnlocked;
            currentTierUpgrade = saveObject.currentTierUpgrade;
        }
    }
}


public class SaveShopItem
{
    public bool isUnlocked;
    public int currentTierUpgrade;
}
