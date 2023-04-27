using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private readonly float knockbackForce = 10000f;
    [SerializeField] private GameObject destroyPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * knockbackForce);
            player.GiveDamege(1);
            Instantiate(destroyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
