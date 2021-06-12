using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public GameObject Empty;
    public GameObject EnemyPreab;
    public Transform Holder;
    public Transform EnemyHolder;
    public float totalEnemyRows = 0f;
    public float totalEnemyInLines = 0f;
    public float minBound = 0f;
    public float maxBound = 0f;
    public float endPoint = 0f;
    public float enemyStepRight = 0f;
    public float enemyStepDown = 0f;
    public float enemyCount = 0;
    public float padHorizontal = 0f;
    public float padVertical = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();

        StartCoroutine(CreateNewEnemy());

    }

    IEnumerator CreateNewEnemy()
    {
        //transform.position -= new Vector3((totalEnemyInLines * EnemyMovement.Instance.padHorizontal) / 2f - (EnemyMovement.Instance.padHorizontal / 2f), 0, totalEnemyRows * EnemyMovement.Instance.padVertical);

        //EnemyMovement.Instance.holder.position = transform.position;

        for (int i = 0; i < totalEnemyRows; i++)
        {
            float posZ = transform.position.z + (padHorizontal * i);
            for (int n = 0; n < totalEnemyInLines; n++)
            {
                Vector3 pos = new Vector3(transform.position.x + (padHorizontal * n), transform.position.y, posZ);
                GameObject enemyHolder = Instantiate(Empty, pos, Empty.transform.rotation, Holder);
                enemyHolder.name = "EnemyHolder_" + (i + 1) + "_" + (n + 1);
                enemyCount++;

                yield return new WaitForSeconds(.5f);

                EnemyNavMeshMovement enemy = Instantiate(EnemyPreab).GetComponent<EnemyNavMeshMovement>();
                enemy.transform.parent = EnemyHolder;
                enemy.FollowTransform = enemyHolder.transform;
                //enemy.SetDestination(new Vector3(RandomXvalue(), 0, RandomYvalue()));
                enemy.name = "Enemy_" + (i + 1) + "_" + (n + 1);
                enemy.transform.localPosition = new Vector3(RandomXvalue(), 0, RandomYvalue());
                enemy.SetDestination(enemy.FollowTransform.position);

                //ObjectPoolController.Instance.SpwanFromPool("Effect", enemy.transform.position, Quaternion.identity);

            }

            yield return new WaitForSeconds(.2f);

        }

        yield return new WaitForSeconds(1f);

        //foreach (Transform enemy in transform)
        //{
        //    enemy.GetComponent<EnemyController>().StopFollw();
        //}

    }

    float RandomXvalue()
    {
        float value = 0f;
        value = Random.Range(9f, 14f);
        if (Random.value < .5f)
        {
            value = -value;
        }
        return value;
    }
    float RandomYvalue()
    {
        float value = 0f;
        value = Random.Range(0f, -25f);
        if (Random.value < .5f)
        {
            value = -value;
        }
        return value;
    }
}
