using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth;
    public float TempSpeed => maxMoveSpeed / 4;
    public float tempMaxMoveSpeed;
    public float maxMoveSpeed = 10f;
    public float maxRotateSpeed = 100f;
    public int coinValue = 1;

    public Material selectedMaterial;
}
