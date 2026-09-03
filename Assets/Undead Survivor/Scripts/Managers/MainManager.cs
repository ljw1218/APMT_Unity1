using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    void Awake()
    {

    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitButton()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
