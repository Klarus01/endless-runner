using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TMP_Text pointsText;

    private int hp;
    private float rotate;
    private float speed;
    private readonly float moveRestorationSpeed = 10f;
    private float resistanceToDamage;

    private Rigidbody rb;
    private Animator anim;
    public Action OnDeath;

    [SerializeField] private PlayerStats playerStats;

    private void Awake()
    {
        SetupStartingStats();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotate * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotate * Time.deltaTime);
        }

        IsPlayerResistanceToDamage();
        IsPlayerSlowed();

        rb.velocity = transform.forward * speed;
    }

    private void SetupStartingStats()
    {
        hp = playerStats.maxHealth;
        playerStats.tempMaxMoveSpeed = playerStats.maxMoveSpeed;
        speed = playerStats.maxMoveSpeed;
        rotate = playerStats.maxRotateSpeed;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
        GetComponent<MeshRenderer>().material = playerStats.selectedMaterial;
    }

    public void IsPlayerResistanceToDamage()
    {
        if (resistanceToDamage <= 0)
        {
            return;
        }

        resistanceToDamage -= Time.deltaTime;
    }

    public void IsPlayerSlowed()
    {
        if (speed >= playerStats.tempMaxMoveSpeed)
        {
            return;
        }
        speed += moveRestorationSpeed * Time.deltaTime;
    }

    public void GiveDamege(int damage)
    {
        IsPlayerResistanceToDamage();

        hp -= damage;
        speed = playerStats.TempSpeed;
        resistanceToDamage = 1;
        anim.SetTrigger("isInviolable");
        if (hp <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
