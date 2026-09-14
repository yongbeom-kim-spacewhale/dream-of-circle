using UnityEngine;

/// <summary>
/// 플레이어의 체력, 스태미나, 공격력 등 기본 스탯을 관리하는 클래스
/// </summary>
public class PlayerStats : MonoBehaviour
{
    #region 기본 스탯
    [Header("기본 스탯")]
    [Tooltip("플레이어의 현재 체력")]
    public int hp = 3;

    [Tooltip("플레이어의 현재 스태미나 (대시 사용)")]
    public int stamina = 3;

    [Tooltip("플레이어의 공격력")]
    public int attack = 1;

    [Tooltip("스태미나 1 회복에 걸리는 시간(초)")]
    [SerializeField] private float staminaRegenTime = 1.0f;
    #endregion

    #region 최대 스탯
    [Header("최대 스탯")]
    [Tooltip("플레이어의 최대 체력")]
    public int maxHp = 3;

    [Tooltip("플레이어의 최대 스태미나")]
    public int maxStamina = 3;
    #endregion

    #region 내부 변수
    // 스태미나 회복을 위한 타이머
    private float staminaTimer = 0f;

    // 게임오버 상태 플래그
    public bool isGameOver = false;
    #endregion

    #region Unity 생명주기
    /// <summary>
    /// 게임 시작 시 스탯 초기화
    /// </summary>
    void Start()
    {
        // 현재 스탯을 최대값으로 설정
        hp = maxHp;
        stamina = maxStamina;
        attack = 1;
    }

    /// <summary>
    /// 매 프레임마다 게임오버 체크 및 스태미나 회복 처리
    /// </summary>
    void Update()
    {
        // 게임오버 체크 - 체력이 0 이하가 되면 게임오버
        CheckGameOver();

        // 스태미나가 최대치 미만이면 자동 회복
        RegenerateStamina();
    }
    #endregion

    #region 게임오버 처리
    /// <summary>
    /// 플레이어의 체력을 확인하여 게임오버 여부 판단
    /// </summary>
    private void CheckGameOver()
    {
        if (!isGameOver && hp <= 0)
        {
            isGameOver = true;
            Debug.Log("GAME OVER");
            // TODO: 게임오버 UI 표시 또는 씬 전환 로직 추가
        }
    }
    #endregion

    #region 스태미나 관리
    /// <summary>
    /// 시간에 따라 스태미나를 자동으로 회복
    /// </summary>
    private void RegenerateStamina()
    {
        // 스태미나가 최대치보다 적을 때만 회복
        if (stamina < maxStamina)
        {
            staminaTimer += Time.deltaTime;

            // 설정된 회복 시간이 지나면 스태미나 1 회복
            if (staminaTimer >= staminaRegenTime)
            {
                stamina += 1;
                staminaTimer = 0f;
            }
        }
        else
        {
            // 최대 스태미나일 경우 타이머 초기화
            staminaTimer = 0f;
        }
    }

    /// <summary>
    /// 스태미나 소모 시도 (대시 등에 사용)
    /// </summary>
    /// <param name="amount">소모할 스태미나 양</param>
    /// <returns>스태미나가 충분하면 true, 부족하면 false</returns>
    public bool TrySpendStamina(int amount)
    {
        // 스태미나가 부족하면 소모 실패
        if (stamina < amount)
            return false;

        // 스태미나 소모 성공
        stamina -= amount;
        return true;
    }
    #endregion

    #region 데미지 처리
    /// <summary>
    /// 플레이어가 데미지를 받는 처리
    /// </summary>
    /// <param name="damage">받을 데미지 양</param>
    public void TakeDamage(int damage)
    {
        // 이미 체력이 0 이하면 추가 데미지 무시
        if (hp <= 0)
            return;

        // 체력 감소 (최소값 0)
        hp = Mathf.Max(hp - damage, 0);
    }
    #endregion
}