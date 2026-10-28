using UnityEngine;
using UnityEngine.Audio;

public static class PrefsManager
{
    private static readonly Vector2Int[] displayPresets =
    {
        new Vector2Int(1280,720),
        new Vector2Int(1600,900),
        new Vector2Int(1920,1080),
        new Vector2Int(2560,1440),
    };

    private const string SaveKey = "DisplayIndex";

    public static readonly string[] names = { "BGMVolume", "SFXVolume" };
    public static int displayTemp;
    //Display 관련
    public static bool SetDisplay()
    {
        if (!IsAvailable())
            return false;

        if (GameData.DisplayIndex == Define.displayWindowFull_Index)
            Screen.SetResolution(GameData.monitorW, GameData.monitorH, FullScreenMode.FullScreenWindow);
        else if (GameData.DisplayIndex == Define.displayFull_Index)
            Screen.SetResolution(GameData.monitorW, GameData.monitorH, FullScreenMode.ExclusiveFullScreen);
        else
            Screen.SetResolution(displayPresets[GameData.DisplayIndex].x, displayPresets[GameData.DisplayIndex].y, FullScreenMode.Windowed);

        PlayerPrefs.SetInt(SaveKey, GameData.DisplayIndex);
        GameData.DisplayIndex = displayTemp;
        PlayerPrefs.Save();
        return true;
    }

    public static void DisplayLoad()
    {
        GameData.DisplayIndex = PlayerPrefs.GetInt(SaveKey, Define.Default_Index);
        if (!SetDisplay())
        {
            GameData.DisplayIndex = Define.Default_Index;
            SetDisplay();
        }
    }
    public static bool IsAvailable()
    {
        if (GameData.DisplayIndex > 3)
            return true;

        Vector2Int p = displayPresets[GameData.DisplayIndex];

        return p.x < Display.main.systemWidth && p.y < Display.main.systemHeight;
    }

    // 사운드 관련
    public static float ToDb(float value)
    {
        if (value <= 0.001f)
            return -80f;
        return Mathf.Log10(Mathf.Max(value,0.001f)) * 20f;
    }

    public static void SetVolume(AudioMixer mixer, string name, float value,bool isConfirm)
    {
        float db = ToDb(value);
        if (name == names[0])
            mixer.SetFloat(name, db);
        else
            mixer.SetFloat(name, db);
        if (isConfirm)
        {
            if (name == names[0])
                GameData.BGMRatio = value;
            else
                GameData.ESRatio = value;
            PlayerPrefs.SetFloat(name, value);
            PlayerPrefs.Save();
        }
        
    }
    public static void LoadSavedVolume(AudioMixer mixer)
    {
        foreach (string p in names)
        {
            float volume = PlayerPrefs.GetFloat(p, 0.4f);

            if (p == names[0])
            {
                GameData.BGMRatio = volume;
                mixer.SetFloat(p, ToDb(volume));
            }
            else
            {
                GameData.ESRatio = volume;
                mixer.SetFloat(p, ToDb(volume));
            }
        }
    }

    public static void LoadOption()
    {
        GameData.EffectRatio = PlayerPrefs.GetFloat(Define.strEffectRatio, 1f);
        bool bED;
        if (PlayerPrefs.GetInt(Define.strED, 1) == 1)
            bED = true;
        else
            bED = false;

        bool bPD;
        if (PlayerPrefs.GetInt(Define.strPD, 1) == 1)
            bPD = true;
        else
            bPD = false;

        GameData.showED = bED;
        GameData.showPD = bPD;
    }
}
