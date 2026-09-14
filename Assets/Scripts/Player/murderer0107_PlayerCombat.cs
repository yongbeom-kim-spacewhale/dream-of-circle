using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerCombat : MonoBehaviour
{
    private float nextAttackTime = 0f;
    public GameObject attackPrefab1;  // Attack 프리팹
    public float spawnOffset = 0.6f; // 플레이어 앞에 살짝 띄워서 생성
    public float attackCooldown = 0.25f;

    public float heavyCooldown = 5f;
    private float nextHeavyTime = 0f;

    public GameObject heavyAttackPrefab; 
    public int heavyDamage = 5;

    PlayerStats stats;
    PlayerController controller;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        controller = GetComponent<PlayerController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.X) && Time.time >= nextHeavyTime)
        {
            nextHeavyTime = Time.time + heavyCooldown;
            FireHeavy();
        }
    }

    void Fire()
    {
        if (attackPrefab1 == null) return;

        // 전방 방향 결정
        Vector2 forward = (controller != null)
            ? (Vector2)controller.LastMoveDir
            : (Vector2)transform.right;

        Vector3 spawnPos = transform.position + (Vector3)(forward * spawnOffset);

        GameObject obj = Instantiate(attackPrefab1, spawnPos, Quaternion.identity);

        var proj = obj.GetComponent<BaseProjectile>();
        if (proj != null)
        {
            int dmg = (stats != null) ? stats.attack : 1;
            proj.Init(forward, dmg);
        }
    }

    void FireHeavy()
    {
        if (heavyAttackPrefab == null) return;

        Vector2 forward = (Vector2)controller.LastMoveDir; // 또는 lastMoveDir
        Vector3 spawnPos = transform.position + (Vector3)(forward * spawnOffset);

        GameObject obj = Instantiate(heavyAttackPrefab, spawnPos, Quaternion.identity);

        var proj = obj.GetComponent<BaseProjectile>();
        if (proj != null)
        {
            proj.Init(forward, heavyDamage);
        }
    }


}
