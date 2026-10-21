using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    [SerializeField] private Image mainPanel;
    [SerializeField] private Image optionPanel;
    
    bool isLoading = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainPanel.gameObject.SetActive(true);
        optionPanel.gameObject.SetActive(false);
        PrefsManager.DisplayLoad();
        PrefsManager.LoadSavedVolume(SoundManager.instance.mixer);
        PrefsManager.LoadOption();
    }

    public void OnStartButton()
    {
        if (isLoading)
            return;
        isLoading = true;
        StartCoroutine(SoundManager.instance.PlaySoundThenLoad());
    }

    public void OnOptionButton()
    {
        mainPanel.gameObject.SetActive(false);
        optionPanel.gameObject.SetActive(true);
        optionPanel.GetComponent<Option>().OnOpen();
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.openClip));
    }

    public void OnExitButton()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    
}
