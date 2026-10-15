using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private Color selected_Color = new Color(255, 197, 61);
    private Color Free_Color = Color.white;

    [Header("옵션 패널")]
    [SerializeField] private Button optionExitButton;
    [SerializeField] private Button optionGraphicButton;
    [SerializeField] private Button optionSoundButton;

    [Header("그래픽 패널")]
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Button[] resolutionButtons;
    [SerializeField] private Toggle enemyDamageToggle;
    [SerializeField] private Toggle playerDamageToggle;
    [SerializeField] private Button applyGraphicButton;

    [Header("사운드 패널")]
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider ESSlider;
    [SerializeField] private Button applySoundSlider;

    public void OnClick_Exit()
    {

    }

    public void OnClick_SelectGraphic()
    {

    }

    public void OnClick_SelectSound()
    {

    }

    public void OnClick_ApplyGraphic()
    {

    }

    public void OnClick_ApplySound()
    {

    }
}
