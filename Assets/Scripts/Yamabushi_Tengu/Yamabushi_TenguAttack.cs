using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] float attack1dame;

    [Header("Attack 2")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject secondSkillBulletPrefab;
    [SerializeField] private float secondSkillBulletSpeed;
    [SerializeField] private float attack2Cooldown = 6f;
    private float nextAttack2Time = 0f;

    [Header("Attack 3")]
    [SerializeField] private GameObject thirdSkillBulletPrefab;
    [SerializeField] private float thirdSkillBulletSpeed;
    [SerializeField] private float attack3Cooldown = 12f;
    private float nextAttack3Time = 0f;

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

        Destroy(newThirdSkillBullet, 10);

        isShooting = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackPointRadius);
    }
}
