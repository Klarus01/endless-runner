using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TMP_Text pointsText;

    private Rigidbody rb;
    private SceneTrasitions sceneTransition;

    public int health = 3;
    public float moveSpeed = 10f;
    [SerializeField]
    private float maxMoveSpeed;
    private float restorationSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        sceneTransition = FindObjectOfType<SceneTrasitions>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -100f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 100f * Time.deltaTime);
        }

        if (moveSpeed < maxMoveSpeed)
        {
            moveSpeed += restorationSpeed * Time.deltaTime;
        }

        rb.velocity = transform.forward * moveSpeed;
    }

    public void GiveDamege(int damage)
    {
        health -= damage;
        maxMoveSpeed = moveSpeed;
        moveSpeed = maxMoveSpeed / 2;
        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransition.LoadScene("Menu");
        }
    }
}
