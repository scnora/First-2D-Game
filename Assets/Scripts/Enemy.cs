using UnityEngine;

public class Enemy : Entity

{
    private bool playerDetected;
    [Header("Movement details")]
    [SerializeField] protected float speed = 3.5f;




    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }


    protected override void HandleAttack()
    {
        //if player detected, attack 
        if (playerDetected)
        {
            anim.SetTrigger("attack");
        }

    }

    protected override void HandleMovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(facingDirection * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, isTarget);
    }

    protected override void Die()
    {
        base.Die();
        UI.Instance.AddKillCount();
    }

}
