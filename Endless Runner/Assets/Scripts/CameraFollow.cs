using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    private void Update()
    {
        if(playerTransform != null)
        {
            transform.SetPositionAndRotation(new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z), playerTransform.rotation);
        }
    }
}
