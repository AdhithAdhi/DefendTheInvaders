using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMeshMovement : MonoBehaviour
{
    public Transform FollowTransform;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (agent.remainingDistance == 0)
        {

            //SetDestination(new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15)));
        }
    }
    public void SetDestination(Vector3 Follow)
    {
        if(agent==null)
            agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(Follow);
    }
}
