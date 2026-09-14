using TMPro;
using UnityEngine;

public class Uicon : MonoBehaviour
{

    public TMP_Text bossHpText;
    public TMP_Text playerHpText;
    public TMP_Text staminaText;

    public BossStats boss;
    public PlayerStats player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossHpText.text = $"BOSS HP : {boss.bossHp}";

        playerHpText.text = $"HP : {player.hp}/{player.maxHp}";
        staminaText.text = $"ST : {player.stamina}/{player.maxStamina}";
    }
}
