using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] Slider slider;
    
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation Aop = SceneManager.LoadSceneAsync(Define.Scene.Game);
        Aop.allowSceneActivation = false;

        while (!Aop.isDone)
        {
            float LoadProgress = Mathf.Clamp01(Aop.progress / 0.9f);
            slider.value = LoadProgress;

            if(Aop.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                Aop.allowSceneActivation = true;
            }
        }

        yield return null;
    }
}
