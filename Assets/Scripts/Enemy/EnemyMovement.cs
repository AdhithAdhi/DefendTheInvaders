using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Levels;
    public GameObject[] BonusLevels;
    public GameObject EmptyObject;
    public GameObject EnemyPrefab;

    public Transform holder;

    public float padHorizontal = 0f;
    public float padVertical = 0f;

    public ParticleSystem Effect;
    public GameObject enemyBullet;
    public bool canFire = false;
    public float fireRate = 0f;
    public LevelType currentLevelType;
    public float enemySpeed = 1f;
    int bonusCurrentLevel = -1;
    // Start is called before the first frame update
    public static EnemyMovement Instance { get; private set; }
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
    void Start()
    {

    }



    void Update()
    {

    }

    public IEnumerator Pause()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
    public void Continue()
    {
        if (bonusCurrentLevel >= 0)
            bonusCurrentLevel -= 1;
        for (int i = 0; i < BonusLevels.Length; i++)
        {
            IMovement MoveObject = BonusLevels[i].GetComponent<IMovement>();
            if (MoveObject != null)
            {
                MoveObject.OnContinue();
            }
        }
        Levels.GetComponent<EnemyBasicMove>().Continue();
        currentLevelType = LevelType.Normal;
        StartLevel();
    }
    public void StartLevel()
    {
        ObjectPoolController.Instance.ReturnToPool("Enemy");
        GameManager.Instance.LevelIncrease();
        StartCoroutine(GameManager.Instance.PlayLevelAnimation());
        for (int i = 0; i < BonusLevels.Length; i++)
        {
            BonusLevels[i].SetActive(false);
        }
        if ((GameManager.Instance.currentLevel - 1) % System.Enum.GetValues(typeof(MovementType)).Length != 0)
        {
            currentLevelType = LevelType.Bonus;
            bonusCurrentLevel++;
            if(bonusCurrentLevel>= BonusLevels.Length)
            {
                bonusCurrentLevel = 0;
            }
            Levels.SetActive(false);
            for(int i=0;i< BonusLevels.Length; i++)
            {
                if (i == bonusCurrentLevel)
                {
                    BonusLevels[i].SetActive(true);
                    IMovement MoveObject = BonusLevels[i].GetComponent<IMovement>();
                    if (MoveObject != null)
                    {
                        MoveObject.OnStart();
                    }
                }
                else
                {
                    BonusLevels[i].SetActive(false);
                    IMovement MoveObject = BonusLevels[i].GetComponent<IMovement>();
                    if (MoveObject != null)
                    {
                        MoveObject.OnDisable();
                    }
                }
            }
            //Debug.LogError("Level " + GameManager.Instance.currentLevel + " is bonus");
        }
        else
        {
            Levels.SetActive(true);
            currentLevelType = LevelType.Normal;
            Levels.GetComponent<EnemyBasicMove>().StartMovement();
            //Debug.LogError("Level " + GameManager.Instance.currentLevel + " is normal");
        }
        //Debug.LogError("Level " + GameManager.Instance.currentLevel + " started");
    }

    public void ClearHolder()
    {
        foreach(Transform child in holder)
        {
            Destroy(child.gameObject);
        }
    }
}
public enum MovementType
{
    ZigZag = 0,
    Horizontal = 1
    //Vertical = 2
}
public enum LevelType
{
    Normal,
    Bonus
}

