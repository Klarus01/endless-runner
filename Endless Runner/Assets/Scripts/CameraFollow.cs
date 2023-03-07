using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
        }
    }
}
