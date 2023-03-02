using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    private float moveSpeed = 10f;
    private float horizontalInput;
    private int health = 3;
    private int points = 1;
    private float getMoveSpeed = 10;
    private int coins = 0;

    public TMP_Text pointsText;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rb.freezeRotation = true;
        rb.useGravity = false;

        InvokeRepeating("SpawnPoints", 1f, 1f);
    }


    void Update()
    {


        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -.2f);
        }
        else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, .2f);
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }

        transform.Translate(new Vector3(horizontalInput * 5f, 0, moveSpeed) * Time.deltaTime);

    }

    void SpawnPoints()
    {
        points++;
        pointsText.text = "" + points;
        if (points > getMoveSpeed)
        {
            moveSpeed *= 1.1f;
            getMoveSpeed += getMoveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            health--;
            if (health <= 0)
                Destroy(gameObject);
        }

        if(other.CompareTag("Coin"))
        {
            coins++;
            SpawnPoints();
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
