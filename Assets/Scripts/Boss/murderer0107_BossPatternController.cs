using UnityEngine;
using System.Collections;

public class BossPatternController : MonoBehaviour
{

    public float patternInterval = 5f; // 몇 초마다 패턴을 시도할지
   // public Pattern_SquareExplosion pattern1; // 인스펙터에서 보스에 붙은 패턴 컴포넌트를 연결
    //public Pattern_SquareMultiShot pattern2;

    public MonoBehaviour[] patterns; // 인스펙터에서 [0]=패턴1, [1]=패턴2 ... 순서대로 넣기

    int currentIndex = 0;

    BossController bossController;

    void Awake()
    {
        bossController = GetComponent<BossController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PatternLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PatternLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(patternInterval);

            if (patterns == null || patterns.Length == 0)
                continue;

            // 이동 멈춤
            if (bossController != null) bossController.enabled = false;

            // 현재 패턴 실행 (패턴 스크립트가 Execute 코루틴을 갖고 있다는 전제)
            var p = patterns[currentIndex];

            // 예: Pattern_SquareExplosion / Pattern_SquareMultiShot 둘 다 Execute가 있어야 함
            if (p is Pattern_SquareExplosion p1) yield return StartCoroutine(p1.Execute(transform));
            else if (p is Pattern_SquareMultiShot p2) yield return StartCoroutine(p2.Execute(transform));
            else if (p is Pattern_SquareDash p3) yield return StartCoroutine(p3.Execute(transform));
            // 이동 재개
            if (bossController != null) bossController.enabled = true;

            // 다음 패턴으로(끝이면 처음으로)
            currentIndex++;
            if (currentIndex >= patterns.Length) currentIndex = 0;
        }
    }


}
