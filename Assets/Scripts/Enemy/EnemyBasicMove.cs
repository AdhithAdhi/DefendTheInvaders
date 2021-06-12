using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
    public MovementType movementType;
    public float totalEnemyRows = 0f;
    public float totalEnemyInLines = 0f;
    public float minBound = 0f;
    public float maxBound = 0f;
    public float endPoint = 0f;
    public float enemyStepRight = 0f;
    public float enemyStepDown = 0f;
    float enemyMovementSpeed = 0f;
    public float enemyCount = 0;
    bool canMove = true;
    bool walkRight = true;
    ObjectPoolController objectPoolController;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartMovement()
    {
        objectPoolController = ObjectPoolController.Instance;
        canMove = true;
        walkRight = true;
        enemyCount = 0;
        enemyMovementSpeed = 1;

        transform.position = startPos;
        int currentPos = (int)movementType;
        currentPos++;
        if (currentPos >= System.Enum.GetValues(typeof(MovementType)).Length)
        {
            currentPos = 0;
        }
        movementType = (MovementType)currentPos;

        foreach (Transform child in EnemyMovement.Instance.holder)
        {
            Destroy(child.gameObject);
        }
        StopAllCoroutines();

        StartCoroutine(CreateNewEnemy());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CreateNewEnemy()
    {
        transform.position -= new Vector3((totalEnemyInLines * EnemyMovement.Instance.padHorizontal) / 2f - (EnemyMovement.Instance.padHorizontal / 2f), 0, totalEnemyRows * EnemyMovement.Instance.padVertical);

        EnemyMovement.Instance.holder.position = transform.position;

        if (movementType == MovementType.ZigZag)
        {

            for (int i = 0; i < totalEnemyRows; i++)
            {
                float posZ = transform.position.z + (EnemyMovement.Instance.padVertical * i);
                for (int n = 0; n < totalEnemyInLines; n++)
                {
                    Vector3 pos = new Vector3(transform.position.x + (EnemyMovement.Instance.padHorizontal * n), transform.position.y, posZ);
                    GameObject enemyHolder = Instantiate(EnemyMovement.Instance.EmptyObject, pos, EnemyMovement.Instance.EnemyPrefab.transform.rotation, EnemyMovement.Instance.holder);
                    enemyHolder.name = "EnemyHolder_" + (i + 1) + "_" + (n + 1);
                    enemyCount++;

                    objectPoolController.SpwanFromPool("Effect", startPos, Quaternion.identity);
                    yield return new WaitForSeconds(.5f);


                    EnemyController enemy = ObjectPoolController.Instance.SpwanFromPool("Enemy").GetComponent<EnemyController>();
                    enemy.transform.position = startPos;
                    enemy.transform.parent = transform;

                    enemy.speed = EnemyMovement.Instance.enemySpeed;
                    enemy.name = "Enemy_" + (i + 1) + "_" + (n + 1);
                    enemy.GetComponent<EnemyController>().holderTransform = enemyHolder.transform;
                    //EnemyController enemy = Instantiate(EnemyMovement.Instance.EnemyPrefab, new Vector3(0, 0, 21), EnemyMovement.Instance.EnemyPrefab.transform.rotation, transform).GetComponent<EnemyController>();
                    //enemys.Add(enemy.transform);

                }

                yield return new WaitForSeconds(.2f);

            }

            yield return new WaitForSeconds(1f);

            foreach (Transform enemy in transform)
            {
                enemy.GetComponent<EnemyController>().StopFollw();
            }
        }
        else if (movementType == MovementType.Horizontal)
        {
            for (int i = 0; i < totalEnemyRows; i++)
            {
                float posZ = transform.position.z + (EnemyMovement.Instance.padVertical * i);
                float posX = i % 2 == 0 ? 10 : -10;
                for (int n = 0; n < totalEnemyInLines; n++)
                {
                    Vector3 pos = new Vector3(transform.position.x + (EnemyMovement.Instance.padHorizontal * n), transform.position.y, posZ);
                    GameObject enemyHolder = Instantiate(EnemyMovement.Instance.EmptyObject, pos, EnemyMovement.Instance.EnemyPrefab.transform.rotation, EnemyMovement.Instance.holder);
                    enemyHolder.name = "EnemyHolder_" + (i + 1) + "_" + (n + 1);
                    enemyCount++;

                    yield return new WaitForSeconds(.5f);

                    EnemyController enemy = ObjectPoolController.Instance.SpwanFromPool("Enemy").GetComponent<EnemyController>();
                    enemy.transform.position = new Vector3(posX, transform.position.y, posZ);
                    enemy.transform.parent = transform;
                    //EnemyController enemy = Instantiate(EnemyMovement.Instance.EnemyPrefab, new Vector3(posX, 0, posZ), EnemyMovement.Instance.EnemyPrefab.transform.rotation, transform).GetComponent<EnemyController>();

                    objectPoolController.SpwanFromPool("Effect", enemy.transform.position, Quaternion.identity);
                    enemy.name = "Enemy_" + (i + 1) + "_" + (n + 1);
                    enemy.holderTransform = enemyHolder.transform;

                }


            }

            yield return new WaitForSeconds(2f);

            foreach (Transform enemy in transform)
            {
                enemy.GetComponent<EnemyController>().StopFollw();
            }
        }

        yield return new WaitForSeconds(0f);
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
                //StopAllCoroutines();
            }

            enemyMovementSpeed = (transform.childCount / enemyCount);
            //EnemyMovement.Instance.fireRate = (enemySpeed / 2f);
            if (enemyMovementSpeed > 1)
            {
                enemyMovementSpeed = 1;
            }

            Vector3 dir = walkRight ? Vector3.right : Vector3.left;
            transform.position += (dir * enemyStepRight);

            SoundManager.Instance.PlaySound("Blip");

            foreach (Transform enemy in transform)
            {
                //Debug.LogError(enemy.position.x);
                if (enemy.position.x < minBound || enemy.position.x > maxBound)
                {
                    walkRight = !walkRight;
                    transform.position += (Vector3.back * enemyStepDown);
                    if (enemy.position.z <= -endPoint)
                    {
                        canMove = false;
                        //Debug.LogError(enemy.name + " " + enemy.position.z);
                        GameManager.Instance.GameOver();
                    }
                    break;
                }
            }

            yield return new WaitForSeconds(enemyMovementSpeed);
        }
    }

    public void StopMove()
    {
        canMove = false;

    }
    public void Continue()
    {
        canMove = true;
        int currentPos = (int)movementType;
        currentPos++;
        if (currentPos >= System.Enum.GetValues(typeof(MovementType)).Length)
        {
            currentPos = 0;
        }
        movementType = (MovementType)currentPos;
        foreach (Transform child in EnemyMovement.Instance.holder)
        {
            Destroy(child.gameObject);
        }
    }
}
