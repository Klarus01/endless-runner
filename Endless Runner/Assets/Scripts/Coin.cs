using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            GameManager.instance.coins++;
            GameManager.instance.SpawnPoints();
            Destroy(gameObject);
        }
    }
}
