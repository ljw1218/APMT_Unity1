using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip startClip;

    bool isLoading = false;

    void Awake()
    {
        
    }

    public void OnStartButton()
    {
        if (isLoading)
            return;
        isLoading = true;
        StartCoroutine(PlaySoundThenLoad());
    }

    private IEnumerator PlaySoundThenLoad()
    {
        float wait = 0f;

        if (sfxSource != null && startClip != null)
        {
            sfxSource.PlayOneShot(startClip);
            wait = startClip.length + 0.3f;
        }

        // timeScale이 0이어도 기다릴 수 있게 Realtime 사용
        yield return new WaitForSecondsRealtime(wait);

        SceneManager.LoadScene(Define.Scene.Load);
    }

    public void OnExitButton()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
