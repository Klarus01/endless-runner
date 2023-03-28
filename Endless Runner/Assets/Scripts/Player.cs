using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TMP_Text pointsText;

    private Rigidbody rb;
    private Animator anim;
    private GameManager gm;
    private SceneTrasitions sceneTransition;

    private int health = 10;
    private float rortateSpeed = 100f;
    private float maxMoveSpeed;
    private readonly float moveRestorationSpeed = 10f;
    private float resistanceToDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gm = GameManager.instance;
        maxMoveSpeed = gm.moveSpeed;
        GetComponent<MeshRenderer>().material = gm.selectedMaterial;
        rb.freezeRotation = true;
        sceneTransition = FindObjectOfType<SceneTrasitions>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rortateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rortateSpeed * Time.deltaTime);
        }

        if (gm.moveSpeed < maxMoveSpeed)
        {
            gm.moveSpeed += moveRestorationSpeed * Time.deltaTime;
        }
        if (resistanceToDamage > 0)
        {
            resistanceToDamage -= Time.deltaTime;
        }

        rb.velocity = transform.forward * gm.moveSpeed;
    }

    public void GiveDamege(int damage)
    {
        if (resistanceToDamage > 0)
            return;

        health -= damage;
        maxMoveSpeed = gm.moveSpeed;
        gm.moveSpeed = maxMoveSpeed / 4;
        resistanceToDamage = 1;
        anim.SetTrigger("isInviolable");
        if (health <= 0)
        {
            if (gm.points > gm.highscore)
            {
                gm.highscore = gm.points;
            }
            gm.moveSpeed = maxMoveSpeed;
            gm.Save();
            Destroy(gameObject);
            sceneTransition.LoadScene("Menu");
        }
    }
}
