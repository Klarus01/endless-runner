using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float knockbackForce = 10000f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * knockbackForce);
            player.GiveDamege(1);
            Destroy(gameObject);
        }
    }
}
