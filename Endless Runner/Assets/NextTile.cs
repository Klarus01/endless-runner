using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTile : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NextTile")
            Destroy(this.gameObject);
    }
    
    

}
