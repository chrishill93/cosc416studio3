using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.4f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (isDashing) return;

        Move();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.collider.CompareTag("Ground") && hit.normal == Vector3.up)
            {
                isGrounded = true;
                canDoubleJump = true;
            }
        }
        else
        {
            isGrounded = false;
        }

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
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            canDoubleJump = false;
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashDirection = transform.forward;
        Vector3 originalVelocity = rb.velocity;
        
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        rb.velocity = originalVelocity;
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
