using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Image optionPanel;
    public Image OverPanel;
    public End uiClear;
    public End uiDead;

    [Header("# Player Info")]
    public bool bisLive;

    [Header("# Game Control")]
    public bool bGameLive;
    public float gameTime {  get; private set; }
    //public int nBossSpawnTime;
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
        OverPanel.gameObject.SetActive(false);
        bisClear = false;
    }
    void Update()
    {
        gameTime += Time.deltaTime;

    }
    public void GameEndSequnce()
    {
        bGameLive = false;
        Time.timeScale = 0;
        OverPanel.gameObject.SetActive(true);
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

    public void OnOptionClicked()
    {
        optionPanel.gameObject.SetActive(true);
        optionPanel.GetComponent<Option>().OnOpen();
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.openClip));
    }

}
