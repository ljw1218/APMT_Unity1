using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioClip startClip;
    [SerializeField] public AudioMixer mixer;
    [SerializeField] public AudioClip openClip;
    [SerializeField] public AudioClip closeClip;
    [SerializeField] public AudioClip buttonClip;
    [SerializeField] public AudioClip applyClip;

    [Header("BGM_Clip")]
    [SerializeField] public AudioClip noramlBGMClip;
    [SerializeField] public AudioClip bossBGMClip;

    [Header("Effect_Sound")]
    [SerializeField] public AudioClip pipeClip;
    [SerializeField] public AudioClip SealClip;
    [SerializeField] public AudioClip SealPunchClip;
    public AudioSource bgmSource;
    private void Awake()
    {
        instance = this;
    }
    public IEnumerator PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        yield return null;
    }

    public IEnumerator PlaySoundThenLoad()
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

    public void ChangeClip(bool isBoss)
    {
        bgmSource.Stop();
        if (isBoss)
            bgmSource.generator = bossBGMClip;
        else
            bgmSource.generator = noramlBGMClip;
        bgmSource.Play();
    }
}
