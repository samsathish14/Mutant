using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public Rigidbody2D myBody;
    public Joystick joystick;
    public float moveSpeed;

    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheckingPos;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(joystick.Horizontal * moveSpeed, myBody.velocity.y);

        // If grounded and jump button (e.g., space) is pressed, then jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply vertical force to make the player jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    bool IsGrounded()
    {
        // Check if the player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckingPos.position, groundCheckRadius, groundLayer);

        // Visualize the ground check area in the editor
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(groundCheckingPos.position, groundCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider != null && collider.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }

    // Visualize the ground check area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckingPos.position, groundCheckRadius);
    }


}
