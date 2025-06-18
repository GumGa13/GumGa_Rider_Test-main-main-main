using UnityEngine;

public class RiderController : MonoBehaviour
{
    public float moveSpeed = 5f;   //이동속도
    public float jumpForce = 5f;   //점프력
    public float airTorque = 100f;    //회전력
    public float airMoveForce = 5f;    //공중 이동속도
    public float groundMoveForce = 10f;   //가속을 위한 힘의 크기
    public float maxSpeed = 8f;  //최대속도

    public AudioSource jumpSound;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool controlEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        if (!controlEnabled) return; // 조작 비활성화 시 아무 입력도 안 받음

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (jumpSound != null)
        {
            jumpSound.Play();
        }
    }

    void FixedUpdate()
    {
        if (!controlEnabled) return;

        float moveInput = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
            moveInput = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            moveInput = -1f;

        if (isGrounded)
        {
            if (moveInput != 0)
            {
                if (Mathf.Abs(rb.linearVelocity.x) < maxSpeed)
                {
                    rb.AddForce(Vector2.right * moveInput * groundMoveForce, ForceMode2D.Force);
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddTorque(airTorque * Time.fixedDeltaTime, ForceMode2D.Force);
                if (Mathf.Abs(rb.linearVelocity.x) < maxSpeed)
                    rb.AddForce(Vector2.right * airMoveForce, ForceMode2D.Force);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddTorque(-airTorque * Time.fixedDeltaTime, ForceMode2D.Force);
                if (Mathf.Abs(rb.linearVelocity.x) < maxSpeed)
                    rb.AddForce(Vector2.left * airMoveForce, ForceMode2D.Force);
            }
        }
    }
    public void DisableControl()
    {
        controlEnabled = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            isGrounded = false;
    }
}
