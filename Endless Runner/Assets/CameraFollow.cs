using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] 
    Transform playerTransform;

    private void Update()
    {
        if(playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - .5f);
            transform.rotation = playerTransform.rotation;
        }
    }
}
