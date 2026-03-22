using UnityEngine;

public class Player : Entity
{

    [Header("Movement details")]
    [SerializeField] protected float speed = 8;
    [SerializeField] private float jumpForce = 12;


    private float xInput;
    private bool canJump = true;


    protected override void Update()
    {
        base.Update();
        HandleInput();
    }
    protected override void HandleMovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void TryJump()
    {
        //if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleAttack();
        }
    }
    public override void EnableMovement(bool enable)
    {
        base.EnableMovement(enable);
        canJump = enable;
    }
    protected override void Die()
    {
        base.Die();
        UI.Instance.EnableGameOver();
    }
}
