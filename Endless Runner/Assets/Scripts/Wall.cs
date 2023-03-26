using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.GiveDamege(100);
        }
    }
}
