using UnityEngine;
using System.Collections;

public class Pattern_SquareMultiShot : MonoBehaviour
{

    public GameObject projectilePrefab;

    public int damage = 1;               // 패턴 데미지(원하는 값)
    public float projectileSpeed = 8f;   // 탄속
    public float projectileLifeTime = 4f;// 탄 생존시간
    public int directions = 8;           // 몇 방향? (8이면 8방향)
    public float projectileScale = 0.5f; // 탄 크기(보이는 정도)
    public int burstCount = 5; // 연사횟수
    public float burstInterval = 0.3f; //연사 간격
    public float angleOffsetPerBurst = 10f; // 예: 10f면 연사마다 회전

    public float groundZ = 0f;

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
        if (projectilePrefab == null || directions <= 0)
            yield break;

        Vector3 pos3 = new Vector3(bossTransform.position.x, bossTransform.position.y, groundZ);
        Vector2 origin = new Vector2(pos3.x, pos3.y);

        for (int b = 0; b < burstCount; b++)
        {
            float offset = angleOffsetPerBurst * b;

            // 360도를 directions로 나눠서 균등 발사
            for (int i = 0; i < directions; i++)
            {
                float angle = (360f / directions) * i + offset;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                GameObject obj = Instantiate(projectilePrefab, (Vector3)origin + new Vector3(0, 0, groundZ), Quaternion.identity);
                obj.transform.localScale = new Vector3(projectileScale, projectileScale, 0.1f);

                var proj = obj.GetComponent<BossProjectile2D>();
                if (proj != null)
                    proj.SquareInit(dir, projectileSpeed, damage, projectileLifeTime);
            }
            yield return new WaitForSeconds(burstInterval);
        }
        // 패턴이 “완료”됐다고 판단하는 최소 대기
        yield return null;
    }
}
