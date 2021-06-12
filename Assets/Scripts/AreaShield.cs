using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaShield : MonoBehaviour
{
    int durability = 5;
    //int hit = 1;
    public void SetDurability(int val)
    {
        durability = val;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("EnemyBullet"))
        {
            other.GetComponent<IPooledObject>().ReturnToPool();
            other.gameObject.SetActive(false);
            SoundManager.Instance.PlaySound("Explosion");

            durability--;
            //PlayerScore.Instance.AddToPlayerScore(GameManager.Instance.expForEnemy);
            //GameManager.Instance.SpwanPowerUp();

            //EnemyHolder.Instance.DecreasePlayerCount();
            //Need to change to pool
        }
        if (durability <= 0)
        {

        }
    }
}
