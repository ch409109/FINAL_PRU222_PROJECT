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

    [Header("Attack Point")]
    [SerializeField] float attackPointRadius;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask player2Mask;
    [SerializeField] float attackdame = 10;
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
        float move = Input.GetAxisRaw("Horizontal");

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
            isJumping = true;
            rb.AddForce(new Vector2(0f, 1000f), ForceMode2D.Impulse);
        }
    }
    private void DealDame()
    {
        Collider2D[] Player2 = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackPointRadius, player2Mask);
        foreach (Collider2D player in Player2)
        {
            player.GetComponent<Player2Script>().TakeDamage(attackdame);
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.J)) // Ví dụ kích hoạt ComboAttack
        {
            animator.SetTrigger("ComboAttack");
        }

        if (Input.GetKeyDown(KeyCode.U)) // Kích hoạt Skill1
        {
            animator.SetTrigger("Skill1");
        }

        if (Input.GetKeyDown(KeyCode.I)) // Kích hoạt Skill2
        {
            animator.SetTrigger("Skill2");
        }

        if (Input.GetKeyDown(KeyCode.O)) // Kích hoạt Skill3
        {
            animator.SetTrigger("Skill3");
        }
    }
    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackPointRadius);
    }
}
