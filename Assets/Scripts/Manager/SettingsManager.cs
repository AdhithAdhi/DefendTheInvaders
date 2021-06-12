using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    public CustomToggle bgmToggle;
    public CustomToggle sfxToggle;
    public TMP_Dropdown qualityDropdown;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
             Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        qualityDropdown.value = QualityValue();
        bgmToggle.SetIsOn(bgmState());
        sfxToggle.SetIsOn(sfxState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityLevel", qualityIndex);
        PlayerPrefs.Save();
    }
    public void MuteBgm(bool bgmCheckbox)
    {
        PlayerPrefs.SetString("bgmSound", bgmCheckbox.ToString());
        PlayerPrefs.Save();
        if (SoundManager.Instance != null)
            SoundManager.Instance.IntializeBgmSoundsAsState(!bgmCheckbox);
        Debug.LogError("bgm state is "+ bgmCheckbox.ToString());
    }
    public void MuteSfx(bool sfxCheckbox)
    {
        PlayerPrefs.SetString("sfxSound", sfxCheckbox.ToString());
        PlayerPrefs.Save();
        if (SoundManager.Instance != null)
            SoundManager.Instance.IntializeSfxSoundsAsState(!sfxCheckbox);
        Debug.LogError("sfx state is " + sfxCheckbox.ToString());
    }
    public static bool bgmState()
    {
        return bool.Parse(PlayerPrefs.GetString("bgmSound", "true"));
    }
    public static bool sfxState()
    {
        return bool.Parse(PlayerPrefs.GetString("sfxSound", "true"));
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public static int QualityValue()
    {
        return PlayerPrefs.GetInt("qualityLevel", 0);
    }
}
