using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    TMP_Text myText;
    Slider expSlider;
    Slider hpSlider;

    void Awake()
    {
        myText = GetComponentInChildren<TMP_Text>();
        expSlider = GetComponentInChildren<Slider>();
        hpSlider = GetComponent<Slider>();
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
                myText.text = $"Lv. {GameManager.instance.level + 1}";
                break;
            case InfoType.Kill:

                break;
            case InfoType.Time:

                break;
            case InfoType.Health:
                float curHp = GameManager.instance.health;
                float maxHp = GameManager.instance.maxhealth;
                hpSlider.value = curHp / maxHp;
                break;
        }
    }
}
