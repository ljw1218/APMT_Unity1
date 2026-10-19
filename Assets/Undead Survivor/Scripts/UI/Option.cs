using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private Color selected_Color = new Color32(255, 197, 61,255);
    private Color Free_Color = Color.white;

    [Header("큰 패널")]
    [SerializeField] private Image mainPanel;
    [SerializeField] private Image optionPanel;

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
    private readonly Vector2Int[] displayPresets =
    {
        new Vector2Int(1280,720),
        new Vector2Int(1600,900),
        new Vector2Int(1920,1080),
        new Vector2Int(2560,1440),
    };

    public void Start()
    {
        BGMSlider.value = GameData.BGMRatio;
        ESSlider.value = GameData.ESRatio;
        effectSlider.value = GameData.EffectRatio;
    }

    public void OnEnable()
    {
        OnClick_SelectGraphic();
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
    public void OnClick_ExitOption()
    {
        optionPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);
        SetSliderPosition();
    }

    public void OnClick_SelectGraphic()
    {
        graphicPanel.gameObject.SetActive(true);
        soundPanel.gameObject.SetActive(false);
        optionGraphicButton.GetComponent<Image>().color = selected_Color;
        optionSoundButton.GetComponent<Image>().color = Free_Color;
        SetSliderPosition();
    }

    public void OnClick_SelectSound()
    {
        graphicPanel.gameObject.SetActive(false);
        soundPanel.gameObject.SetActive(true);
        optionGraphicButton.GetComponent<Image>().color = Free_Color;
        optionSoundButton.GetComponent<Image>().color = selected_Color;
        SetSliderPosition();
    }

    public void OnClick_DisplayButton(int index)
    {
        displayButtons[graphicTemp].GetComponent<Image>().color = Free_Color;
        graphicTemp = index;
        displayButtons[index].GetComponent<Image>().color = selected_Color;

    }
    public void OnClick_ApplyGraphic()
    {
        GameData.DisplayIndex = graphicTemp;
        GameData.EffectRatio = effectSlider.value;
        GameData.showED = enemyDamageToggle.isOn;
        GameData.showPD = playerDamageToggle.isOn;
    }

    public void OnClick_ApplySound()
    {
        GameData.BGMRatio = BGMSlider.value;
        GameData.ESRatio = ESSlider.value;
    }

    public void SetDisplay()
    {

    }
}
