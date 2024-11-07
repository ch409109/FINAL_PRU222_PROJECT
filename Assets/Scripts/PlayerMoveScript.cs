using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private bool isMoving;
    private bool isJumping;
    private bool facingRight = true; // Biến để theo dõi hướng mặt của nhân vật

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimations();
    }

    void HandleMovement()
    {
        float move = Input.GetAxisRaw("Horizontal_P2");

        // Di chuyển
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
        isMoving = move != 0;

        // Kiểm tra và lật hướng nhân vật
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        // Kiểm tra nhảy
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void Flip()
    {
        // Đảo hướng nhân vật
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Lật trục X
        transform.localScale = scale;
    }

    void HandleAnimations()
    {
        // Set các trạng thái di chuyển và nhảy

        animator.SetBool("isMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJumping");
        }
        // Các hành động khác
        if (Input.GetKeyDown(KeyCode.F)) // Ví dụ kích hoạt TakeDamage
        {
            animator.SetTrigger("TakeDamage");
        }

        if (Input.GetKeyDown(KeyCode.G)) // Ví dụ kích hoạt ComboAttack
        {
            animator.SetTrigger("ComboAttack");
        }

        if (Input.GetKeyDown(KeyCode.H)) // Ví dụ kích hoạt Knockout
        {
            animator.SetTrigger("Knockout");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Kích hoạt Skill1
        {
            animator.SetTrigger("Skill1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // Kích hoạt Skill2
        {
            animator.SetTrigger("Skill2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) // Kích hoạt Skill3
        {
            animator.SetTrigger("Skill3");
        }
    }
}
