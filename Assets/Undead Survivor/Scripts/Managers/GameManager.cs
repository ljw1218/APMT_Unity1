using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    [Header("# Player Info")]
    public int health;
    public int maxhealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Control")]
    public bool bGameLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxhealth;
        uiLevelUp.Hide();
        uiLevelUp.Select(1);
    }
    void Update()
    {
        gameTime += Time.deltaTime;

    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        bGameLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        bGameLive = true;
        Time.timeScale = 1;
    }

}
