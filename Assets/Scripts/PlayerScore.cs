using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance { get; private set; }
    int playerScore = 0;
    public TextMeshProUGUI ScoreText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance!=this)
        {
             Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AddToPlayerScore(0);
    }

    public void AddToPlayerScore(int value)
    {
        playerScore += value;
        ScoreText.text = "Score : " + playerScore;
    }
    public int GetPlayerScore()
    {
        return playerScore;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    public void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", playerScore);
        PlayerPrefs.Save();
    }

    public bool HaveNewHighScore()
    {
        if (playerScore > GetHighScore())
        {
            SetHighScore();
            return true;
        }
        else
        {
            return false;
        }
    }

}
