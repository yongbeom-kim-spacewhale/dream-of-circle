using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 lastMoveDir = Vector3.right;
    private bool isDashing = false;
    private PlayerStats stats;

    private Rigidbody2D rb;
    private Vector2 moveDir;

    public float speed = 20.0f;
    public float dashDistance = 10.0f;
    public float dashDuration = 0.12f;
    public Vector3 LastMoveDir => lastMoveDir;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 입력만 받기
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(horizontalInput, verticalInput).normalized;

        if (moveDir != Vector2.zero)
            lastMoveDir = new Vector3(moveDir.x, moveDir.y, 0f);

        // 대쉬 입력
        if (!isDashing && Input.GetKeyDown(KeyCode.Space))
        {
            if (stats != null && stats.TrySpendStamina(1))
                StartCoroutine(DashRoutine(lastMoveDir));
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    IEnumerator DashRoutine(Vector3 dashDir)
    {
        isDashing = true;

        Vector2 d = new Vector2(dashDir.x, dashDir.y).normalized;
        float dashSpeed = dashDistance / dashDuration;
        float t = 0f;

        while (t < dashDuration)
        {
            rb.MovePosition(rb.position + d * dashSpeed * Time.fixedDeltaTime);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
    }
}