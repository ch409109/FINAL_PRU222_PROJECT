using System.Collections;
using UnityEngine;

public class KitsuneAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool isShooting = false;
    private Player2Script playerScript;
    private bool facingRight;

    [Header("Attack 1")]
    [SerializeField] private float attackPointRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask player1Mask;
    [SerializeField] private float attack1dame;

    [Header("Attack 2")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject secondSkillBulletPrefab;
    [SerializeField] private float secondSkillBulletSpeed;
    [SerializeField] private float attack2Cooldown = 5f; 
    private float nextAttack2Time = 0f;

    [Header("Attack 3")]
    [SerializeField] private GameObject thirdSkillBulletPrefab;
    [SerializeField] private float thirdSkillBulletSpeed;
    [SerializeField] private float attack3Cooldown = 10f; 
    private float nextAttack3Time = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerScript = GetComponent<Player2Script>();
    }

    void Update()
    {
        if (playerScript != null)
        {
            facingRight = playerScript.FacingRight;
        }
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Attack_1();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4) && Time.time >= nextAttack2Time)
        {
            Attack_2();
            nextAttack2Time = Time.time + attack2Cooldown; // Cập nhật thời điểm có thể sử dụng lại Attack_2
        }

        if (Input.GetKeyDown(KeyCode.Keypad5) && Time.time >= nextAttack3Time)
        {
            Attack_3();
            nextAttack3Time = Time.time + attack3Cooldown; // Cập nhật thời điểm có thể sử dụng lại Attack_3
        }
    }

    private void Attack_1()
    {
        animator.SetTrigger("Attack_1");
    }

    private void DealDame()
    {
        Collider2D[] Player2 = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRadius, player1Mask);
        foreach (Collider2D player in Player2)
        {
            player.GetComponent<Player1Script>().TakeDamage(attack1dame);
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
        GameObject newSecondSkillBullet = Instantiate(secondSkillBulletPrefab, firePoint.position, Quaternion.identity);
        newSecondSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(facingRight ? secondSkillBulletSpeed : -secondSkillBulletSpeed, 0);

        if (!facingRight)
        {
            newSecondSkillBullet.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        Physics2D.IgnoreCollision(newSecondSkillBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Destroy(newSecondSkillBullet, 5);
        isShooting = false;
    }

    private void ThirdSkillBulletFly()
    {
        GameObject newThirdSkillBullet = Instantiate(thirdSkillBulletPrefab, firePoint.position, Quaternion.identity);
        newThirdSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(facingRight ? thirdSkillBulletSpeed : -thirdSkillBulletSpeed, 0);

        if (!facingRight)
        {
            newThirdSkillBullet.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        Physics2D.IgnoreCollision(newThirdSkillBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Destroy(newThirdSkillBullet, 10);
        isShooting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRadius);
    }
}
