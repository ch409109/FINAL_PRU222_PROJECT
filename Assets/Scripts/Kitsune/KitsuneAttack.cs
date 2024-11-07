using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitsuneAttack : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;
    private bool isShooting = false;
    private bool facingRight = true;

    [Header("Attack 2")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject secondSkillBulletPrefab;
    [SerializeField] private float secondSkillBulletSpeed;

    [Header("Attack 3")]
    [SerializeField] private GameObject thirdSkillBulletPrefab;
    [SerializeField] private float thirdSkillBulletSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.U)) Attack_2();
        if (Input.GetKeyDown(KeyCode.I)) Attack_3();
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
        GameObject newSecondSkillBullet = GameObject.Instantiate(secondSkillBulletPrefab, shootPoint.position, Quaternion.identity);

        if (facingRight)
        {
            newSecondSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * 1, 0);
        }
        else
        {
            newSecondSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * -1, 0);
        }

        Physics2D.IgnoreCollision(newSecondSkillBullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

        Destroy(newSecondSkillBullet, 10);

        isShooting = false;
    }

    private void ThirdSkillBulletFly()
    {
        GameObject newThirdSkillBullet = GameObject.Instantiate(secondSkillBulletPrefab, shootPoint.position, Quaternion.identity);

        if (facingRight)
        {
            newThirdSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * 1, 0);
        }
        else
        {
            newThirdSkillBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(secondSkillBulletSpeed * -1, 0);
        }

        Physics2D.IgnoreCollision(newThirdSkillBullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

        Destroy(newThirdSkillBullet, 10);

        isShooting = false;
    }
}
