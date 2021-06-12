using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IPooledObject
{
    public EnemyState enemyState;
    public Transform holderTransform;
    public Transform bulletSpwanPos;

    public float speed = 1f;

    public float restTime = 1f;
    bool stopFollow = false;

    bool forceFire = false;

    private void Start()
    {

        //foreach (Transform child in transform)
        //{
        //    if (child.GetComponent<SkinnedMeshRenderer>() != null)
        //    {
        //        child.GetComponent<SkinnedMeshRenderer>().enabled = false;
        //    }
        //}

        //GetComponent<BoxCollider>().enabled = false;
        //StartCoroutine(ShowRender());
    }
    IEnumerator ShowRender()
    {
        yield return new WaitForSeconds(.5f);

        foreach (Transform child in transform)
        {
            if (child.GetComponent<SkinnedMeshRenderer>() != null)
            {
                child.GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }



        //yield return new WaitForSeconds(2.5f);

        //GetComponent<BoxCollider>().enabled = true;

    }
    public void Update()
    {
        if (stopFollow == false)
        {if (holderTransform != null)
                transform.position = Vector3.MoveTowards(transform.position, holderTransform.position, speed * Time.deltaTime);
            //transform.position = holderTransform.position;
        }

    }

    public void StopFollw()
    {
        stopFollow = true;
    }
    public void ForceFireIs(bool val)
    {
        forceFire = val;
    }
    public void FireBullet()
    {
        if (stopFollow == false && forceFire == false)
            return;

        GameObject bullet =ObjectPoolController.Instance.SpwanFromPool("EnemyBullet");
        bullet.transform.position = bulletSpwanPos.position;
        //Instantiate(EnemyMovement.Instance.enemyBullet, bulletSpwanPos.position, EnemyMovement.Instance.enemyBullet.transform.rotation);

        SoundManager.Instance.PlaySound("Fire");
    }

    public void OnObjectSpwan()
    {
        forceFire = false;
    }

    public void ReturnToPool()
    {
        speed = EnemyMovement.Instance.enemySpeed;
        //
        stopFollow = false;
        transform.position = Vector3.zero;
        transform.parent = ObjectPoolController.Instance.transform;
       // Debug.LogError(gameObject.name+":"+transform.parent.name+":"+transform.position);
    }
}
public enum EnemyState
{
    Attack,
    Parade,
    Rest
}