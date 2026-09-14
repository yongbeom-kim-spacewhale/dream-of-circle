using UnityEngine;
using System.Collections;

public class Pattern_SquareExplosion : MonoBehaviour
{
    public GameObject warningPrefab; // 경고 프리팹
    public GameObject damagePrefab;  // 데미지 존 프리팹

    public Vector2 size = new Vector2(6f, 6f); // 네모 범위(가로, 세로)
    public float warnTime = 3f;                // 패턴 시작 3초 전부터 경고 → 3초 후 발동
    public int damage = 2;                     // 이 패턴의 데미지
    public float lifeTime = 0.2f;              // 데미지 존이 유지되는 시간(짧게)


    public float groundZ = 0f;                 // XY가 바닥인 경우 z 고정
    public float thinZ = 0.1f;                 // 3D 프리미티브/쿼드면 납작하게 보이게


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
        // 보스 위치에 깔기 (z를 고정해서 “상공에 뜨는 문제” 방지)
        Vector3 pos = new Vector3(bossTransform.position.x, bossTransform.position.y, groundZ);

        // 1) 경고 프리팹 생성
        GameObject warning = null;
        if (warningPrefab != null)
        {
            warning = Instantiate(warningPrefab, pos, Quaternion.identity);

            // (요구사항) XY 범위는 크고, Z는 얇게 (보이기만 하는 정도)
            warning.transform.localScale = new Vector3(size.x, size.y, thinZ);
        }

        // 2) 경고 유지 시간(3초) 대기
        yield return new WaitForSeconds(warnTime);

        // 3) 경고 제거
        if (warning != null)
            Destroy(warning);

        // 4) 데미지 존 생성
        if (damagePrefab != null)
        {
            GameObject dmg = Instantiate(damagePrefab, pos, Quaternion.identity);
            dmg.transform.localScale = new Vector3(size.x, size.y, thinZ);

            // 핵심: DamageZone2D는 “공통 판정 로직”만 담당하고,
            // 패턴이 자신의 값(damage, lifeTime)을 생성 직후 덮어씌움
            DamageZone2D zone = dmg.GetComponent<DamageZone2D>();
            if (zone != null)
            {
                zone.damage = damage;
                zone.lifeTime = lifeTime;
            }
        }
    }
}
