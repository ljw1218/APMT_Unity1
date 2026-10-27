using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private Color selected_Color = new Color32(255, 197, 61,255);
    private Color Free_Color = Color.white;

    [Header("큰 패널")]
    [SerializeField] private Image mainPanel;

    [Header("옵션탭 패널")]
    [SerializeField] private Image graphicPanel;
    [SerializeField] private Image soundPanel;
    [SerializeField] private Button optionExitButton;
    [SerializeField] private Button optionGraphicButton;
    [SerializeField] private Button optionSoundButton;

    [Header("그래픽 패널")]
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Button[] displayButtons;
    [SerializeField] private Toggle enemyDamageToggle;
    [SerializeField] private Toggle playerDamageToggle;
    [SerializeField] private Button applyGraphicButton;

    [Header("사운드 패널")]
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider ESSlider;
    [SerializeField] private Button applySoundButton;

    private int graphicTemp;
    
    public void Start()
    {
        
    }

    public void OnOpen()
    {
        OnClick_SelectGraphic();
        SetSliderPosition();
        if(mainPanel == null)
            GameManager.instance.Stop();
    }
    public void OnDisable()
    {
        displayButtons[graphicTemp].GetComponent<Image>().color = Free_Color;
        displayButtons[GameData.DisplayIndex].GetComponent<Image>().color = selected_Color;
    }

    public void SetSliderPosition()
    {
        effectSlider.value = GameData.EffectRatio;
        BGMSlider.value = GameData.BGMRatio;
        ESSlider.value = GameData.ESRatio;
    }
    public void OnClick_CloseOption()
    {
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.closeClip));
        
        if(mainPanel != null)
            mainPanel.gameObject.SetActive(true);
        else
        {
            GameManager.instance.Resume();
        }
        SetSliderPosition();
        gameObject.SetActive(false);
    }

    public void OnClick_SelectGraphic()
    {
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.buttonClip));
        graphicPanel.gameObject.SetActive(true);
        soundPanel.gameObject.SetActive(false);
        optionGraphicButton.GetComponent<Image>().color = selected_Color;
        optionSoundButton.GetComponent<Image>().color = Free_Color;
        SetSliderPosition();
    }

    public void OnClick_SelectSound()
    {
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.buttonClip));
        graphicPanel.gameObject.SetActive(false);
        soundPanel.gameObject.SetActive(true);
        optionGraphicButton.GetComponent<Image>().color = Free_Color;
        optionSoundButton.GetComponent<Image>().color = selected_Color;
        SetSliderPosition();
    }

    public void OnClick_DisplayButton(int index)
    {
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.buttonClip));
        displayButtons[graphicTemp].GetComponent<Image>().color = Free_Color;
        graphicTemp = index;
        displayButtons[index].GetComponent<Image>().color = selected_Color;

    }
    public void OnClick_ApplyGraphic()
    {
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.applyClip));
        int prevIndex = GameData.DisplayIndex;
        //그래픽 저장은 매니저에서 따로
        PrefsManager.displayTemp = graphicTemp;
        GameData.DisplayIndex = graphicTemp;
        
        GameData.EffectRatio = effectSlider.value;
        PlayerPrefs.SetFloat(Define.strEffectRatio, effectSlider.value);
        GameData.showED = enemyDamageToggle.isOn;
        PlayerPrefs.SetInt(Define.strED, enemyDamageToggle.isOn ? 1 : 0);
        GameData.showPD = playerDamageToggle.isOn;
        PlayerPrefs.SetInt(Define.strPD, playerDamageToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
        if (prevIndex != GameData.DisplayIndex)
            PrefsManager.SetDisplay();

        if (GameData.isGameScene)
            WeaponManager.instance.ChangeAlpha();
    }

    public void OnClick_ApplySound()
    {
        StartCoroutine(SoundManager.instance.PlaySound(SoundManager.instance.applyClip));
        GameData.BGMRatio = BGMSlider.value;
        GameData.ESRatio = ESSlider.value;
        PrefsManager.SetVolume(SoundManager.instance.mixer, PrefsManager.names[0], GameData.BGMRatio, true);
        PrefsManager.SetVolume(SoundManager.instance.mixer, PrefsManager.names[1], GameData.ESRatio, true);
    }


}
