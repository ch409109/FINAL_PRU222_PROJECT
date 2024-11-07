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

    [Header("Attack 2")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject secondSkillBulletPrefab;
    [SerializeField] private float secondSkillBulletSpeed;

    [Header("Attack 3")]
    [SerializeField] private GameObject thirdSkillBulletPrefab;
    [SerializeField] private float thirdSkillBulletSpeed;

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
        if (Input.GetKeyDown(KeyCode.Keypad1)) Attack_1();
        if (Input.GetKeyDown(KeyCode.Keypad4)) Attack_2();
        if (Input.GetKeyDown(KeyCode.Keypad5)) Attack_3();
    }

    private void Attack_1()
    {
        animator.SetTrigger("Attack_1");
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
}
