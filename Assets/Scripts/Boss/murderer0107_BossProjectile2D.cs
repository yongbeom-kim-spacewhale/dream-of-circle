using UnityEngine;

public class BossProjectile2D : MonoBehaviour
{
    Vector2 dir;
    public float speed;
    public int damage;
    public float lifeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponentInParent<PlayerStats>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SquareInit(Vector2 direction, float moveSpeed, int dmg, float timeToLive)
    {
        dir = direction.normalized;
        speed = moveSpeed;
        damage = dmg;
        lifeTime = timeToLive;

        Destroy(gameObject, lifeTime);
    }


}
