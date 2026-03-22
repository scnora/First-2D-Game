using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{


    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected SpriteRenderer sr;


    [Header("Health details")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration = 0.1f;
    private Coroutine damageFeedbackCoroutine;


    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask isTarget;



    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;



    protected int facingDirection = 1;
    protected bool isFacingRight = true;
    protected bool canMove = true;




    protected virtual void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHealth;
    }
    protected virtual void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }
    public void DamageTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, isTarget);
        foreach (Collider2D enemy in enemyColliders)

        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }



    private void TakeDamage()
    {
        currentHealth -= 1;
        PlayDamageFeedback();

        if (currentHealth <= 0)

        {
            Die();

        }
    }

    private void PlayDamageFeedback()
    {
        if (damageFeedbackCoroutine != null)
        {
            StopCoroutine(damageFeedbackCoroutine);
        }
        StartCoroutine(DamageFeedbackCo());
    }

    private IEnumerator DamageFeedbackCo()
    {
        Material originalMat = sr.material;
        sr.material = damageMaterial;
        yield return new WaitForSeconds(damageFeedbackDuration);
        sr.material = originalMat;

    }

    protected virtual void Die()
    {
        anim.enabled = false;
        coll.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        Destroy(gameObject, 3);
    }

    public virtual void EnableMovement(bool enable)
    {
        canMove = enable;
        //canJump = enable;

    }

    protected virtual void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }



    protected virtual void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
            //rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
    protected virtual void HandleMovement()
    {

    }


    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

    }
    protected virtual void HandleFlip()
    {
        if(rb.linearVelocity.x > 0 && isFacingRight==false)
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && isFacingRight==true)
        {
            Flip();
        }
    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
        facingDirection = facingDirection * -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


}
