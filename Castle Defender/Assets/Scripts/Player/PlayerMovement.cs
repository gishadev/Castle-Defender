using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    Vector2 moveInput;

    Rigidbody2D rb;
    Animator animator;


    enum States
    {
        Idle,
        Run
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        MovementAnimation();
        rb.velocity += new Vector2(HorizontalVel(), VerticalVel());
    }

    private float HorizontalVel()
    {
        if (IsInHorizontalBounds(moveInput.x + transform.position.x))
            return moveInput.x * moveSpeed;
        else return 0f;
    }
    private float VerticalVel()
    {
        if (IsInVerticalBounds(moveInput.y + transform.position.y))
            return moveInput.y * moveSpeed;
        else return 0f;

    }

    private bool IsInVerticalBounds(float y)
    {
        if (!WorldBounds.Instance.castleBounds)
            return y > WorldBounds.Instance.b_defaultMin.position.y && y < WorldBounds.Instance.b_defaultMax.position.y;
        else
            return y > WorldBounds.Instance.b_castleMin.position.y && y < WorldBounds.Instance.b_castleMax.position.y;
    }
    private bool IsInHorizontalBounds(float x)
    {
        if (!WorldBounds.Instance.castleBounds)
            return x > WorldBounds.Instance.b_defaultMin.position.x && x < WorldBounds.Instance.b_defaultMax.position.x;
        else
            return x > WorldBounds.Instance.b_castleMin.position.x && x < WorldBounds.Instance.b_castleMax.position.x;
    }

    void MovementAnimation()
    {
        if (Mathf.Abs(moveInput.x) > 0f || Mathf.Abs(moveInput.y) > 0f)
            animator.SetInteger("State", (int)States.Run);
        else
            animator.SetInteger("State", (int)States.Idle);
    }

}
