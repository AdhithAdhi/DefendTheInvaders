using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBlaster : MonoBehaviour,IPooledObject
{
    public float speed = 0f;
    public float maxPosition = 0f;
    bool stopMove = false;
    public float destroyAfter = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stopMove)
        {
            transform.position += Vector3.forward * speed;
            if (transform.position.z > maxPosition)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            stopMove = true;
            other.GetComponent<IPooledObject>().ReturnToPool();
            other.gameObject.SetActive(false);
            StartCoroutine(DestroyAfter());
            PlayerScore.Instance.AddToPlayerScore(GameManager.Instance.expForEnemy);
            //GameManager.Instance.SpwanPowerUp();

            //EnemyHolder.Instance.DecreasePlayerCount();
            //Need to change to pool
        }
        if (other.CompareTag("EnemyBullet"))
        {
            other.gameObject.SetActive(false);
            //Need to change to pool
        }
    }
    IEnumerator DestroyAfter()
    {
        GetComponent<Animator>().SetTrigger("Explode");
        SoundManager.Instance.PlaySound("Explosion");
        yield return new WaitForSeconds(destroyAfter);
        gameObject.SetActive(false);
    }

    public void OnObjectSpwan()
    {

        stopMove = false;
        GetComponent<Animator>().Play(Animator.StringToHash("Idle"));
    }

    public void ReturnToPool()
    {

    }
}
