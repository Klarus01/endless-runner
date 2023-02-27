using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    Vector3 backToRoad;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (transform.position.x < 0)
                backToRoad = new Vector3(other.transform.position.x + .2f, other.transform.position.y, other.transform.position.z);
            else
                backToRoad = new Vector3(other.transform.position.x - .2f, other.transform.position.y, other.transform.position.z);

            other.transform.position = backToRoad;
        }
    }
}
