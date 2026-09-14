using UnityEngine;
using System.Collections;

public class Pattern_SquareDash : MonoBehaviour
{

    [Header("Refs")]
    public Transform player;            // 인스펙터에 Player 드래그 (또는 Awake에서 FindWithTag)
    public GameObject warningPrefab;    // 길쭉한 빨간 네모(경고용)

    [Header("Dash Values")]
    public float aimTime = 0.8f;        // 조준/경고 시간
    public float dashDistance = 6f;     // 돌진 거리
    public float dashDuration = 0.25f;  // 돌진 시간(짧을수록 빠름)

    [Header("Warning Shape")]
    public float warningWidth = 1.2f;   // 경고 네모의 두께(폭)
    public float warningThinZ = 0.1f;   // 3D 프리미티브면 납작하게
    public float groundZ = 0f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // player를 인스펙터에서 안 넣었을 때 자동 탐색(선택)
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Execute(Transform bossTransform)
    {
        if (player == null) yield break;

        // 1) 방향 고정(이 순간 플레이어 위치를 기준으로 “조준”)
        Vector2 startPos = bossTransform.position;
        Vector2 dir = ((Vector2)player.position - startPos).normalized;
        if (dir == Vector2.zero) dir = Vector2.right;

        // 보스가 바라보게(너가 transform.right를 전방으로 쓰는 느낌이면)
        bossTransform.right = new Vector3(dir.x, dir.y, 0f);

        // 2) 경고(길쭉한 네모를 돌진 방향으로 깔기)
        GameObject warn = null;
        if (warningPrefab != null)
        {
            // 경고 네모는 “돌진 경로”에 깔리면 보기 좋음(중앙에 배치)
            Vector2 center = startPos + dir * (dashDistance * 0.5f);
            Vector3 warnPos = new Vector3(center.x, center.y, groundZ);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            warn = Instantiate(warningPrefab, warnPos, Quaternion.Euler(0f, 0f, angle));

            // 길이 = dashDistance, 두께 = warningWidth
            warn.transform.localScale = new Vector3(dashDistance, warningWidth, warningThinZ);
        }

        yield return new WaitForSeconds(aimTime);

        if (warn != null) Destroy(warn);

        // 3) 돌진 (정해진 거리만큼, 정해진 시간에)
        float dashSpeed = dashDistance / dashDuration;
        float moved = 0f;

        while (moved < dashDistance)
        {
            float step = dashSpeed * Time.fixedDeltaTime;
            Vector2 next = rb != null ? (rb.position + dir * step) : ((Vector2)bossTransform.position + dir * step);

            // Rigidbody2D가 있으면 MovePosition으로(충돌 고려)
            if (rb != null) rb.MovePosition(next);
            else bossTransform.position = next;

            moved += step;
            yield return new WaitForFixedUpdate();
        }
    }
}
