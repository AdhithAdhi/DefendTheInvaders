using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MenuConfig : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in Player)
        {
            if (child.GetComponent<SkinnedMeshRenderer>() != null)
            {

                child.GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetColor("_PrimaryColor", SaveAndLoad.GetPlayerPrimaryColor());

                child.GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetColor("_SecondaryColor", SaveAndLoad.GetPlayerSecondaryColor());

            }
        }
        highScore.text = "HighScore\n" + PlayerPrefs.GetInt("HighScore", 0);
        SoundManager.Instance.IntializeBgmSoundsAsState(!SettingsManager.bgmState());
        SoundManager.Instance.IntializeSfxSoundsAsState(!SettingsManager.sfxState());
        QualitySettings.SetQualityLevel(SettingsManager.QualityValue());
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnStoreClick()
    {
        SceneManager.LoadScene(3);
    }
    public void OnSettingsClick()
    {
        SceneManager.LoadScene(2);
    }
}
