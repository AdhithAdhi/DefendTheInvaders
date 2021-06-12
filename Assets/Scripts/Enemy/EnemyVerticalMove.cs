using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalMove : MonoBehaviour, IMovement
{
    public float totalEnemyRows = 0f;
    public float totalEnemyInLines = 0f;
    public float endPoint = 0f;
    public float enemyStepDown = 0f;
    public float enemySpeed = 0f;
    public float enemyCount = 0;
    bool canMove = true;
    ObjectPoolController objectPoolController;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnStart()
    {
        objectPoolController = ObjectPoolController.Instance;
        canMove = true;
        enemyCount = 0;
        enemySpeed = 1;
        transform.position = startPos;
        foreach (Transform child in EnemyMovement.Instance.holder)
        {
            Destroy(child.gameObject);
        }
        StopAllCoroutines();
        StartCoroutine(CreateNewEnemy());

    }
    IEnumerator CreateNewEnemy()
    {
        transform.position -= new Vector3((totalEnemyInLines* EnemyMovement.Instance.padHorizontal)/2f-(EnemyMovement.Instance.padHorizontal /2f), 0, totalEnemyRows * EnemyMovement.Instance.padVertical);
        EnemyMovement.Instance.holder.position = transform.position;
        for (int i = 0; i < totalEnemyRows; i++)
        {
            float posZ = transform.position.z + (EnemyMovement.Instance.padVertical * i);
            for (int n = 0; n < totalEnemyInLines; n++)
            {
                Vector3 pos = new Vector3(transform.position.x + (EnemyMovement.Instance.padHorizontal * n), transform.position.y, posZ);
                GameObject enemyHolder = Instantiate(EnemyMovement.Instance.EmptyObject, pos, EnemyMovement.Instance.EnemyPrefab.transform.rotation, EnemyMovement.Instance.holder);
                enemyHolder.name = "EnemyHolder_" + (i + 1) + "_" + (n + 1);
                enemyCount++;
                EnemyController enemy = ObjectPoolController.Instance.SpwanFromPool("Enemy").GetComponent<EnemyController>();
                enemy.transform.position = pos;
                enemy.transform.parent = transform;
                //EnemyController enemy = Instantiate(EnemyMovement.Instance.EnemyPrefab, pos, EnemyMovement.Instance.EnemyPrefab.transform.rotation, transform).GetComponent<EnemyController>();
                //enemys.Add(enemy.transform);
                objectPoolController.SpwanFromPool("Effect", pos, Quaternion.identity);
                enemy.name = "Enemy_" + (i + 1) + "_" + (n + 1);
                enemy.holderTransform = enemyHolder.transform;

            }

            enemySpeed = 1;
            yield return new WaitForSeconds(.5f);

        }


        //foreach (Transform enemyHolder in transform)
        //{
        //    Destroy(enemyHolder.gameObject);
        //}
        foreach (Transform enemy in transform)
        {
            enemy.GetComponent<EnemyController>().StopFollw();
            //enemy.parent = transform;
        }
        enemySpeed = (transform.childCount / enemyCount);
        StartCoroutine(MoveEnemy());
    }
    IEnumerator MoveEnemy()
    {
        //cyield return new WaitForSeconds(5f);

        while (canMove)
        {
            if (PlayerController.Instance.IsDead())
            {

                canMove = false;
            }
            if (transform.childCount == 0)
            {
                canMove = false;
                GameManager.Instance.LoadNextLevel();
            }

            //enemySpeed = (transform.childCount / enemyCount);
            //EnemyMovement.Instance.fireRate = (enemySpeed / 2f);

            enemySpeed = ((transform.childCount*2) / enemyCount);
            if (enemySpeed > 1)
            {
                enemySpeed = 1;
            }

            transform.position += (Vector3.back * enemyStepDown);

            SoundManager.Instance.PlaySound("Blip");

            foreach (Transform enemy in transform)
            {
                if (enemy.position.z <= -endPoint)
                {
                    //Debug.LogError(enemy.name + " " + enemy.position.z);
                    GameManager.Instance.GameOver();
                    break;
                }
            }

            yield return new WaitForSeconds(enemySpeed);
        }
    }
    public void OnDisable()
    {

    }

    public void OnContinue()
    {
        objectPoolController = ObjectPoolController.Instance;
        canMove = true;
        enemyCount = 0;
        enemySpeed = 1;
        transform.position = startPos;

        foreach (Transform child in EnemyMovement.Instance.holder)
        {
            Destroy(child.gameObject);
        }
    }
}
