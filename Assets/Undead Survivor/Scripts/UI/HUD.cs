using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    TMP_Text LvText;
    TMP_Text TimeText;
    Slider expSlider;
    Slider hpSlider;

    void Awake()
    {
        LvText = GetComponent<TMP_Text>();
        expSlider = GetComponent<Slider>();
        hpSlider = GetComponent<Slider>();
        TimeText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        if (type == InfoType.Level)
        {
            LvText.text = $"Lv. {GameManager.instance.level + 1}";
            LayoutRebuilder.ForceRebuildLayoutImmediate(LvText.transform.parent.GetComponent<RectTransform>());
        }
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                expSlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                LvText.text = $"Lv. {GameManager.instance.level + 1}";
                break;
            case InfoType.Kill:
                
                break;
            case InfoType.Time:
                float timeNow = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(timeNow / 60);
                int sec = Mathf.FloorToInt(timeNow % 60);
                TimeText.text = $"{min:D2}:{sec:D2}";
                break;
            case InfoType.Health:
                float curHp = GameManager.instance.health;
                float maxHp = GameManager.instance.maxhealth;
                hpSlider.value = curHp / maxHp;
                break;
        }
    }
}
