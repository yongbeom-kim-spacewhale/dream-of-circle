using UnityEngine;

public class BossStats : MonoBehaviour
{
    public int bossHp = 10;
    public int bossDamage = 1;

    public bool IsDead => bossHp <= 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어와 충돌시 플레이어의 체력 1감소

    }
    public void TakeDamage(int amount)
    {
        bossHp = Mathf.Max(bossHp - amount, 0);
        if (bossHp == 0)
            Debug.Log("Boss Dead");
    }
}
