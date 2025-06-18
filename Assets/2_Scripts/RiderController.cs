using UnityEngine;

public class RiderController : MonoBehaviour
{
    public float moveSpeed = 5f;   //�̵��ӵ�
    public float jumpForce = 5f;   //������
    public float airTorque = 100f;    //ȸ����
    public float airMoveForce = 5f;    //���� �̵��ӵ�
    public float groundMoveForce = 10f;   //������ ���� ���� ũ��
    public float maxSpeed = 8f;  //�ִ�ӵ�

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
        if (!controlEnabled) return; // ���� ��Ȱ��ȭ �� �ƹ� �Էµ� �� ����

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
