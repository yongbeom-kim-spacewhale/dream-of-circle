using UnityEngine;

public class BossContactDamage : MonoBehaviour
{

    BossStats stats;
    PlayerStats player;

    float interval = 1f; // 1초마다 데미지
    float t = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        stats = GetComponent<BossStats>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        t += Time.deltaTime;
        if (t >= interval)
        {
            int damage = (stats != null) ? stats.bossDamage : 1;
            player.TakeDamage(damage);
            t = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.collider.GetComponentInParent<PlayerStats>();
        if (player != null)
        {
            t = interval; // 닿자마자 1번 깎고 싶으면 interval, 1초 뒤부터면 0f
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var p = collision.collider.GetComponentInParent<PlayerStats>();
        if (p == player)
        {
            player = null;
            t = 0f;
        }
    }
}
