using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IPooledObject
{
    public float speed = 0f;
    public float maxPosition = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.back * speed;
        if (transform.position.z < -maxPosition)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            Health.Instance.HealthDecreased();
            SoundManager.Instance.PlaySound("Dead");
            //Destroy(gameObject);
            //PlayerScore.Instance.SetGameOverText(true);
            //Time.timeScale = 0;
            //Need to change to pool
        }
    }

    public void OnObjectSpwan()
    {

    }

    public void ReturnToPool()
    {

    }
}
