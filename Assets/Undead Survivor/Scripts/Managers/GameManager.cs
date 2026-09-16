using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public End uiClear;
    public End uiDead;
    public PlayerStat playerStat;

    [Header("# Player Info")]
    public bool bisLive;
    public float health;
    //public float maxhealth = 100;
    public int level;
    public int kill;
    public int exp;
    //public int[] nextExp = { 3, 5, 10, 20, 150, 210, 280, 360, 450, 600 };
    public int nextExp;
    public float itemGetRange;

    [Header("# Game Control")]
    public bool bGameLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public int nBossSpawnTime;
    public bool bisClear;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bisLive = true;
        InitStat();
        uiLevelUp.Hide();
        //uiLevelUp.Select(Define.Default_Index);
        uiClear.Hide();
        bisClear = false;
    }
    void Update()
    {
        gameTime += Time.deltaTime;

    }
    void InitStat()
    {
        nextExp = playerStat.BaseData.ExpTerm;
        //RunStat.Init();
        playerStat.RecalculateStats();
        health = playerStat.MaxHealth;
    }
    public void GetExp(int ExpData)
    {
        exp += ExpData;

        if (exp >= nextExp)
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

    public void OnEndClicked()
    {
        //if 승리 이면 데이터 저장 로직 추가 필요
        SceneManager.LoadScene(Define.Scene.Main);
    }

}
