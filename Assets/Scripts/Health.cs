using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public List<Transform> LifeSlots;
    public GameObject showCaseObject;
    public int currentHealth=0;

    public static Health Instance { get; private set; }

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
        for(int i = 0; i < LifeSlots.Count; i++)
        {
            GameObject go = Instantiate(showCaseObject, LifeSlots[i]);
            go.name = "Health_" + (i + 1);
        }
        currentHealth = LifeSlots.Count;
    }

    public void HealthDecreased()
    {
        if (currentHealth > 0)
        {
            Destroy(LifeSlots[currentHealth - 1].GetChild(0).gameObject);
            currentHealth -= 1;
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }
   
    public void HealthIncreased()
    {
        if (currentHealth < LifeSlots.Count)
        {
            currentHealth += 1;
            GameObject go = Instantiate(showCaseObject, LifeSlots[currentHealth - 1]);
            go.name = "Health_" + (currentHealth + 1);
        }
        for (int i = 0; i < LifeSlots.Count; i++)
        {
            if (LifeSlots[i].transform.childCount == 1)
            {
                Animator anim = LifeSlots[i].transform.GetChild(0).GetComponent<Animator>();
                if (anim != null)
                {
                    anim.gameObject.SetActive(false);
                    anim.enabled = false;
                    //anim.StopPlayback();
                    anim.gameObject.SetActive(true);
                    anim.enabled = true;
                    //anim.StartPlayback();
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
