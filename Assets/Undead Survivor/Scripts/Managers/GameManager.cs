using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

    [Header("# Player Info")]
    public int health;
    public int maxhealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxhealth;
    }
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
