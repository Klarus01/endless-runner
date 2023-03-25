using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TMP_Text pointsText;

    private Rigidbody rb;
    private Animator anim;
    private SceneTrasitions sceneTransition;

    private int health = 10;
    public float moveSpeed = 10f;
    private float maxMoveSpeed;
    private float moveRestorationSpeed = 10f;
    private float resistanceToDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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
            moveSpeed += moveRestorationSpeed * Time.deltaTime;
        }
        if (resistanceToDamage > 0)
        {
            resistanceToDamage -= Time.deltaTime;
        }

        rb.velocity = transform.forward * moveSpeed;
    }

    public void GiveDamege(int damage)
    {
        if (resistanceToDamage > 0)
            return;

        health -= damage;
        maxMoveSpeed = moveSpeed;
        moveSpeed = maxMoveSpeed / 4;
        resistanceToDamage = 1;
        anim.SetTrigger("isInviolable");
        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransition.LoadScene("Menu");
        }
    }
}
