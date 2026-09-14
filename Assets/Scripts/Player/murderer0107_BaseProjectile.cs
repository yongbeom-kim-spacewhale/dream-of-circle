using UnityEngine;

public class BaseProjectile : MonoBehaviour
{

    public float speed = 12f;
    public float lifeTime = 1f;

    Vector2 dir;
    int damage;

    public void Init(Vector2 direction, int dmg)
  {
        dir = direction.normalized;
        damage = dmg;
        Destroy(gameObject, lifeTime);
    }

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
        var boss = other.GetComponentInParent<BossStats>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
