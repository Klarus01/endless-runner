using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyPrefab;
    private float knockbackForce = 10000f;

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
