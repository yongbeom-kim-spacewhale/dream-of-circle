using UnityEngine;

public class DamageZone2D : MonoBehaviour
{

    public int damage = 1;          // 오버라이트 될것
    public float lifeTime = 0.2f;   // 오버라이트 될것

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) //플레이어와 충돌
    {
        Debug.Log("DamageZone hit: " + other.name);
        // 플레이어가 닿으면 체력 감소
        var player = other.GetComponentInParent<PlayerStats>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

}

