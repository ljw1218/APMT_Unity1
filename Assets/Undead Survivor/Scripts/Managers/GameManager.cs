using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public End uiClear;
    public End uiDead;

    [Header("# Player Info")]
    public bool bisLive;

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
        uiLevelUp.Hide();
        uiClear.Hide();
        bisClear = false;
    }
    void Update()
    {
        gameTime += Time.deltaTime;

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
