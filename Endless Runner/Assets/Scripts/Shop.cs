using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] items;
    public Material[] materials;
    [SerializeField] private TMP_Text coinsText;

    public void SetColor(int input)
    {
        GameManager.instance.selectedMaterial = materials[input];
    }
}