using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndController : MonoBehaviour
{
    public PlayerStats player;
    public GameObject gameOverPanel;
    public BossStats boss;
    public GameObject victoryPanel;

    bool done = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (done) return;
        if (player == null || gameOverPanel == null) return;

        if (player.isGameOver)
        {
            done = true;

            // UI ∂ÁøÏ±‚
            gameOverPanel.SetActive(true);

            // ∞‘¿” ∏ÿ√ﬂ±‚
            Time.timeScale = 0f;
        }

        if (boss != null && boss.bossHp <= 0)
        {
            done = true;

            // UI ∂ÁøÏ±‚
            victoryPanel.SetActive(true);

            // ∞‘¿” ∏ÿ√ﬂ±‚
            Time.timeScale = 0f;
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // ∏ÿ√Áµ– Ω√∞£ µ«µπ∏Æ±‚
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
