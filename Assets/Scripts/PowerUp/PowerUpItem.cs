using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public PowerUpType Type;
    public float timeLimit;
    float time = 0;
    bool claimed = false;
    // Start is called before the first frame update
    void Start()
    {
        Type = (PowerUpType) Random.Range(0, System.Enum.GetNames(typeof(PowerUpType)).Length);

        StartCoroutine(SetUpDetails());
    }

    IEnumerator SetUpDetails()
    {

        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1f);


        GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(PowerUpManager.Instance.destructAt);

        Destroy(gameObject);
    }

    // Update is called once per frame

    private void Update()
    {
        if (claimed == false)
        {
            time += Time.deltaTime;
            if (time > PowerUpManager.Instance.destructAt)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            PowerUpManager.Instance.SpwanPowerUpBy(Type);
            //Destroy(gameObject);
        }
    }
}
