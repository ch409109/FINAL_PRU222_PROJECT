using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Yamabushi_TenguAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool isShooting = false;
    private Player1Script playerScript;
    private bool facingRight;

    [Header("Attack 1")]
    [SerializeField] float attackPointRadius;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask player2Mask;
    [SerializeField] float attack1dame = 3;


    [Header("Attack 2")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject secondSkillBulletPrefab;
    [SerializeField] private float secondSkillBulletSpeed;
    [SerializeField] private float attack2Cooldown = 6f;
    private float nextAttack2Time = 0f;
    [SerializeField] TextMeshProUGUI CDAttack2;

    [Header("Attack 3")]
    [SerializeField] private GameObject thirdSkillBulletPrefab;
    [SerializeField] private float thirdSkillBulletSpeed;
    [SerializeField] private float attack3Cooldown = 12f;
    private float nextAttack3Time = 0f;
    [SerializeField] TextMeshProUGUI CDAttack3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerScript = GetComponent<Player1Script>();
    }

    void Update()
    {
        if (playerScript != null)
        {
            facingRight = playerScript.FacingRight; // Lấy giá trị facingRight từ Player1Script
        }
        HandleInput();
        UpdateCooldownAttack();
    }

    private void UpdateCooldownAttack()
    {
        if (Time.time < nextAttack2Time)
        {
            float remainingCooldown2 = nextAttack2Time - Time.time;
            CDAttack2.text = $"Attack 2: {remainingCooldown2:F1}s";
            Debug.Log("Cooldown Attack 2: " + remainingCooldown2); // In ra giá trị thời gian hồi chiêu
        }
        else 
        {
            CDAttack2.text = "Attack 2: Ready";
            Debug.Log("Attack 2: Ready"); // In ra khi cooldown đã sẵn sàng
        }

        if (Time.time < nextAttack3Time)
        {
            float remainingCooldown3 = nextAttack3Time - Time.time;
            CDAttack3.text = $"Attack 3: {remainingCooldown3:F1}s";
        }
        else
        {
            CDAttack3.text = "Attack 3: Ready";
        }
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack_1();
        }

        if (Input.GetKeyDown(KeyCode.U) && Time.time >= nextAttack2Time)
        {
            Attack_2();
            nextAttack2Time = Time.time + attack2Cooldown;
        }

        if (Input.GetKeyDown(KeyCode.I) && Time.time >= nextAttack3Time)
        {
            Attack_3();
            nextAttack3Time = Time.time + attack3Cooldown;
        }
    }

    private void Attack_1()
    {
        animator.SetTrigger("Attack_1");
        Collider2D[] Player2 = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackPointRadius, player2Mask);
        foreach (Collider2D player in Player2)
        {
            player.GetComponent<Player2Script>().TakeDamage(attack1dame);
        }
    }
    private void DealDame()
    {
        Collider2D[] Player2 = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRadius, player2Mask);
        foreach (Collider2D player in Player2)
        {
            player.GetComponent<Player2Script>().TakeDamage(attack1dame);
        }
    }
    private void Attack_2()
    {
        animator.SetTrigger("Attack_2");
    }

    private void Attack_3()
    {
        animator.SetTrigger("Attack_3");
    }

    private void SecondSkillBulletFly()
    {
        GameObject newSecondSkillBullet = GameObject.Instantiate(secondSkillBulletPrefab, firePoint.position, Quaternion.identity);

        if (facingRight)
        {
            newSecondSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * 1, 0);
        }
        else
        {
            newSecondSkillBullet.transform.rotation = Quaternion.Euler(0, 180, 0);
            newSecondSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * -1, 0);
        }

        Physics2D.IgnoreCollision(newSecondSkillBullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        IgnoreCollisionsWithTags(newSecondSkillBullet.GetComponent<Collider2D>(), new string[] { "SuperBullet1", "SuperBullet2", "Bullet2", "Heart", "Poop"});

        Destroy(newSecondSkillBullet, 10);

        isShooting = false;
    }

    private void ThirdSkillBulletFly()
    {
        GameObject newThirdSkillBullet = GameObject.Instantiate(thirdSkillBulletPrefab, firePoint.position, Quaternion.identity);

        if (facingRight)
        {
            newThirdSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * 1, 0);
        }
        else
        {
            newThirdSkillBullet.transform.rotation = Quaternion.Euler(0, 180, 0);
            newThirdSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * -1, 0);
        }

        Physics2D.IgnoreCollision(newThirdSkillBullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        IgnoreCollisionsWithTags(newThirdSkillBullet.GetComponent<Collider2D>(), new string[] { "Bullet1", "SuperBullet2", "Bullet2", "Heart", "Poop" });

        Destroy(newThirdSkillBullet, 10);

        isShooting = false;
    }

    private void IgnoreCollisionsWithTags(Collider2D collider, string[] tags)
    {
        foreach (string tag in tags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject taggedObject in taggedObjects)
            {
                Collider2D taggedCollider = taggedObject.GetComponent<Collider2D>();
                if (taggedCollider != null)
                {
                    Physics2D.IgnoreCollision(collider, taggedCollider);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackPointRadius);
    }
}
