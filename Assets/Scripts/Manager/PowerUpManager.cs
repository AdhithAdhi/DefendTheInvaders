using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public List<PowerUpUi> powerUpUis = new List<PowerUpUi>();
    public GameObject powerUpItem;
    public Transform powerUpSpwan;
    public Transform powerUpIconsHolder;
    public float minDistance=0,maxDistance=0;
    public GameObject powerUpEffect;
    public float destructAt;
    public static PowerUpManager Instance { get; private set; }
    public float randomValue = .997f;
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
        
    }

    public void SpwanPowerUpBy(PowerUpType powerUpType)
    {
        if (powerUpType == PowerUpType.Health)
        {
            //Instantiate(powerUpEffect, transform);

            SoundManager.Instance.PlaySound("Powerup");
            Health.Instance.HealthIncreased();
            return;
        }
        foreach (Transform child in powerUpIconsHolder)
        {
            if (child.GetComponent<PowerUpUi>().Type== powerUpType)
            {
                child.GetComponent<PowerUpUi>().IncreaseCount();
                return;
            }
        }
        for (int i = 0; i < powerUpUis.Count; i++)
        {

            if(powerUpUis[i].Type== powerUpType)
            {

                Instantiate(powerUpUis[i].gameObject, powerUpIconsHolder);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpwanPowerUp()
    {
        if (Random.value > randomValue)
        {
            if (Random.value > randomValue)
            {

                GameObject powerup = Instantiate(powerUpItem, powerUpSpwan);
                //Debug.LogError(new Vector3(Random.Range(minDistance, maxDistance), 0, 0));
                powerup.transform.position += new Vector3(Random.Range(minDistance, maxDistance), 0, 0);
                //Debug.LogError("PowerUp Spwaned");
            }
        }
    }
    public void ResetPowerUps()
    {
        foreach (Transform child in powerUpIconsHolder)
        {
            if (child.GetComponent<IPowerUp>() != null)
            {
                child.GetComponent<IPowerUp>().ResetPowerUp();
            }
        }
    }
}
public enum PowerUpType
{
    Health,
    AreaBlast,
    _2x,
    Shield,
}
