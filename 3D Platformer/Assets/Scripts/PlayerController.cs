using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDashing) return;

        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0f;

        if (direction.magnitude >= 0.1f)
        {
            transform.forward = direction;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            canDoubleJump = false;
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashDirection = transform.forward;
        
        float startTime = Time.time;
        while (Time.time > startTime + dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
