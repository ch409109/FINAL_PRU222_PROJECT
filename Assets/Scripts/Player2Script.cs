using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Script : MonoBehaviour
{
    [SerializeField] float forceX = 5, forceY = 5;
    float xInput;

    [Header("Ground check")]
    [SerializeField] float groundCheckRadius;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [Header("Health Management")]
    public UIController UIController;
    public float currentHP;
    public float maxHP = 50;
    public float heartHeal = 5;
    public float poopDamage = 5;

    public bool isGround;
    bool facingRight = false;
    public bool FacingRight
    {
        get { return facingRight; }
    }
    //bool isDead = false;
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] private float skill2Damage = 7;
    [SerializeField] private float skill3Damage = 12;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.transform.rotation = Quaternion.Euler(0, 180, 0);
        animator = gameObject.GetComponent<Animator>();

        currentHP = maxHP;
        UIController.UpdateSecondPlayerHealthBar(currentHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        xInput = Input.GetAxisRaw("Horizontal_P2");
        Movement();
        Flip();
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Jump();
        }
        AnimationController();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet1"))

        {
            Destroy(collision.gameObject);
            animator.SetTrigger("Hurt");
            currentHP -= skill2Damage;
            UIController.UpdateSecondPlayerHealthBar(currentHP, maxHP);

            if (currentHP <= 0)
            {
                animator.SetTrigger("Dead");
                SceneManager.LoadSceneAsync(3);
                GameResult.ResultText = "Tengu Win";
            }
        }
        if (collision.gameObject.CompareTag("SuperBullet1"))
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("Hurt");
            currentHP -= skill3Damage;
            UIController.UpdateSecondPlayerHealthBar(currentHP, maxHP);

            if (currentHP <= 0)
            {
                animator.SetTrigger("Dead");
                SceneManager.LoadSceneAsync(3);
                GameResult.ResultText = "Tengu Win";
            }
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            currentHP += heartHeal;
            UIController.UpdateSecondPlayerHealthBar(currentHP, maxHP);
        }
        if (collision.gameObject.CompareTag("Poop"))
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("Hurt");
            currentHP -= poopDamage;
            UIController.UpdateSecondPlayerHealthBar(currentHP, maxHP);

            if (currentHP <= 0)
            {
                animator.SetTrigger("Dead");
                SceneManager.LoadSceneAsync(3);
                GameResult.ResultText = "Tengu Win";
            }
        }
    }

    private void AnimationController()
    {
        animator.SetFloat("xVelocity", rb.velocity.x);
        animator.SetBool("isGround", isGround);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void Jump()
    {
        if (isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, forceY);
        }
    }
    private void Flip()
    {
        if (rb.velocity.x < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }

    }
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * forceX, rb.velocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
    private void CollisionCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundMask);
    }
    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hurt");
        currentHP -= damage;
        UIController.UpdateSecondPlayerHealthBar(currentHP, maxHP);

        if (currentHP <= 0)
        {
            animator.SetTrigger("Dead");
            SceneManager.LoadSceneAsync(3);
            GameResult.ResultText = "Tengu Win";
        }
    }
}
