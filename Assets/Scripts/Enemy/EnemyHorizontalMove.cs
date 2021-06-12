using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMove : MonoBehaviour, IMovement
{
    public GameObject triggerObject;
    public float totalEnemyRows = 0f;
    public float totalEnemyInLines = 0f;
    public float minBound = 0f;
    public float maxBound = 0f;
    public float endPoint = 0f;
    public float enemyStepRight = 0f;
    public float enemyStepDown = 0f;
    public float enemySpeed = 0f;
    public float enemyCount = 0;
    ObjectPoolController objectPoolController;
    bool started = false;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        //objectPoolController = ObjectPoolController.Instance;
    }
    public void OnStart()
    {
        foreach (Transform child in EnemyMovement.Instance.holder)
        {
            Destroy(child.gameObject);
        }
        started = false;
        enemyCount = 0;
        //enemySpeed = ;
        objectPoolController = ObjectPoolController.Instance;
        triggerObject.SetActive(true);


        transform.position = startPos;
        StopAllCoroutines();
        StartCoroutine(CreateNewEnemy()); 
    }
    IEnumerator CreateNewEnemy()
    {
        //Debug.LogError("tetsing");
        transform.position -= new Vector3((totalEnemyInLines * EnemyMovement.Instance.padHorizontal) / 2f - (EnemyMovement.Instance.padHorizontal / 2f), 0, totalEnemyRows * EnemyMovement.Instance.padVertical);

        EnemyMovement.Instance.ClearHolder();
        EnemyMovement.Instance.holder.position = transform.position;
        for (int i = 0; i < totalEnemyRows; i++)
        {
            float posZ = transform.position.z + (EnemyMovement.Instance.padVertical * i);
            float posX = i % 2 == 0 ? 10 : -10;

            Vector3 pos;
            if (posX < 0)
            {
                pos = new Vector3(-(posX - 5f), transform.position.y, posZ);

            }
            else
            {

                pos = new Vector3(-(posX + 5f), transform.position.y, posZ);
            }
            //Debug.LogError("Position is " + pos);
            GameObject enemyHolder = Instantiate(EnemyMovement.Instance.EmptyObject, pos, EnemyMovement.Instance.EnemyPrefab.transform.rotation, EnemyMovement.Instance.holder);
            enemyHolder.name = "EnemyHolder_" + (i + 1);

          
            for (int n = 0; n < totalEnemyInLines; n++)
            {
                enemyCount++;

                EnemyController enemy = ObjectPoolController.Instance.SpwanFromPool("Enemy").GetComponent<EnemyController>();
                enemy.transform.parent = transform;
                //enemy.transform.position = new Vector3(posX, 0, posZ);
                enemy.transform.localPosition = new Vector3(posX, 0, posZ);
                //need to reset the local position y value
                //Debug.LogError(enemy.transform.position);
                //Debug.LogError(enemy.transform.localPosition);
                enemy.holderTransform = enemyHolder.transform;
                enemy.transform.position = new Vector3(posX, transform.position.y, posZ);
                //EnemyController enemy = Instantiate(EnemyMovement.Instance.EnemyPrefab, new Vector3(posX, 0, posZ), EnemyMovement.Instance.EnemyPrefab.transform.rotation, transform).GetComponent<EnemyController>();
                //enemys.Add(enemy.transform);
                enemy.speed = enemySpeed;
                enemy.ForceFireIs(true);
                objectPoolController.SpwanFromPool("Effect", enemy.transform.position, Quaternion.identity);
                enemy.name = "Enemy_" + (i + 1) + "_" + (n + 1);

                yield return new WaitForSeconds(.5f);
            }

            //yield return new WaitForSeconds(1f);

        }

        yield return new WaitForSeconds(2f);
        started = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (started)
        {

            if (transform.childCount == 0)
            {
                started = false;
                GameManager.Instance.LoadNextLevel();
            }
        }
    }

    public void OnDisable()
    {
        triggerObject.SetActive(false);
    }

    public void OnContinue()
    {
        started = false;
        enemyCount = 0;
        //enemySpeed = ;
        triggerObject.SetActive(true);

        foreach (Transform child in EnemyMovement.Instance.holder)
        {
            Destroy(child.gameObject);
        }
    }
}
