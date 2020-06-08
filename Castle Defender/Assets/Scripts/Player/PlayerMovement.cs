using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    Vector2 moveInput;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

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
        return y > WorldBounds.Instance.b_Bottom.position.y && y < WorldBounds.Instance.b_Top.position.y;
    }
    private bool IsInHorizontalBounds(float x)
    {
        return x < WorldBounds.Instance.b_Right.position.x && x > WorldBounds.Instance.b_Left.position.x;
    }
}
