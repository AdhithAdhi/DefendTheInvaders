using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject GameOverScreen;
    public GameObject LevelScreen;
    public GameObject PauseScreen;
    public int currentLevel = 0;
    public int expForEnemy = 0;
    bool isGameOver = false;

    public TextMeshProUGUI BulletCountText;
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
        PauseScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        currentLevel = 0;
        EnemyMovement.Instance.StartLevel();
    }
    public void GameOver()
    {
        if (isGameOver == true)
        {
            return;
        }
        PlayerController.Instance.PlayerDead();
        PowerUpManager.Instance.ResetPowerUps();
        ObjectPoolController.Instance.ReturnToPool("Bullet");
        ObjectPoolController.Instance.ReturnToPool("EnemyBullet");
        GameOverScreen.SetActive(true);
        if (PlayerScore.Instance.HaveNewHighScore())
        {
            GameOverScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "High Score\n" + PlayerScore.Instance.GetPlayerScore();
        }
        else
        {
            GameOverScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score\n" + PlayerScore.Instance.GetPlayerScore();
        }
        StopAllCoroutines();
        Invoke("StopTime", 1.5f);
        isGameOver = true;
    }
    void StopTime()
    {
        CancelInvoke();
        Time.timeScale = 0;
        Joystick.Instance.ExcludeInput = true;
    }
    public void LoadNextLevel()
    {
        EnemyMovement.Instance.StartLevel();
    }
    public IEnumerator PlayLevelAnimation()
    {
        LevelScreen.SetActive(true);
        LevelScreen.GetComponentInChildren<TextMeshPro>().text = "Level " + currentLevel;
        LevelScreen.GetComponent<Animator>().Play(Animator.StringToHash("NewLevel"));
        yield return new WaitForSeconds(3f);
        LevelScreen.GetComponent<Animator>().StopPlayback();
        LevelScreen.SetActive(false);
    }
    public void SpwanPowerUp()
    {
        PowerUpManager.Instance.SpwanPowerUp();
    }
    public void LevelIncrease()
    {
        currentLevel++;
    }
    public void LevelDecrease()
    {
        currentLevel--;
    }
    public void Restart()
    {

        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadSettings()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    //private void Update()
    //{
    //}
    public void PauseBtnClick()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        PauseScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score\n" + PlayerScore.Instance.GetPlayerScore();
    }
    public void ResumeBtnClick()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }

    public void SetBulletCount(int bulletCount, int magCapacity)
    {
        BulletCountText.text = (magCapacity - bulletCount) + "/" + magCapacity;
    }
    public void ReloadBullet()
    {
        BulletCountText.transform.parent.GetChild(0).GetComponent<Animator>().SetTrigger("Reload");

    }

    public void ContinueGame()
    {
        CancelInvoke();
        isGameOver = false;
        Time.timeScale = 1;
        Joystick.Instance.ExcludeInput = false;
        PlayerController.Instance.PlayerRevive();
        PauseScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        LevelDecrease();
        EnemyMovement.Instance.Continue();
    }
}
