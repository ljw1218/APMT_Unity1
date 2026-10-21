using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;
    [SerializeField] private int mixerIndex;

    void Awake()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.onValueChanged.AddListener(v =>
            PrefsManager.SetVolume(mixer, PrefsManager.names[mixerIndex], v,false));
    }

    public void OnPointerDown(PointerEventData e)
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClip);
    }
    public void OnPointerUp(PointerEventData e)
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClip);
    }
}
