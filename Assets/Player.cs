using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    public float gravity = 20.0f;
    public float moveSpeed = 10f;
    public float changeRowSpeed = 5f;
    public float horizontalInput;
    public int health = 30;
    public int points = 1;
    public float getMoveSpeed = 10;

    public TMP_Text pointsText;
    Rigidbody rb;
    RoadManager rm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rm = FindObjectOfType<RoadManager>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rb.freezeRotation = true;
        rb.useGravity = false;
        InvokeRepeating("SpawnPoints", 1f, 1f);
    }


    void Update()
    {
        
        transform.Translate(new Vector3(horizontalInput * changeRowSpeed, 0, moveSpeed) * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -1f);
        }
        else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 1f);
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }

    }

    void SpawnPoints()
    {
        //points = points + rm.roadsCount - 2;
        pointsText.text = "" + points;
        if (points > getMoveSpeed)
        {
            moveSpeed *= 1.1f;
            getMoveSpeed += getMoveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            health--;
            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
