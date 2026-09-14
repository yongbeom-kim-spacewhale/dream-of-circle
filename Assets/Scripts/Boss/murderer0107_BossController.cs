using UnityEngine;

public class BossController : MonoBehaviour
{

    public Transform target; //대상 추적
    public float moveSpeed = 3f; //보스 이동 속도


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = target.position - transform.position;
        toTarget.z = 0f; // XY 평면 이동, Z 고정

        Vector3 dir = toTarget.normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

        transform.right = dir; // 보스 방향 플레이어로 고정

    }
}
