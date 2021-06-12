using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
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
        transform.position += Vector3.forward * speed;
        if (transform.position.z > maxPosition)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IPooledObject>().ReturnToPool();
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound("Explosion");
            PlayerScore.Instance.AddToPlayerScore(GameManager.Instance.expForEnemy);
            GameManager.Instance.SpwanPowerUp();

            //EnemyHolder.Instance.DecreasePlayerCount();
            //Need to change to pool
        }
        if (other.CompareTag("EnemyBullet"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound("Explosion");
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
public enum BulletType
{
    Bullet,
    Bomb,
}
