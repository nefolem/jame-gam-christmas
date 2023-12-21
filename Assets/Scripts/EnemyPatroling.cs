using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatroling : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float _flyingHeight = 10f;

    private Enemy enemy;
    private int currentPatrolIndex;
    private NavMeshAgent agent;
    Vector3 destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        destination = new Vector3(patrolPoints[Random.Range(0, patrolPoints.Length)].position.x, 0, patrolPoints[Random.Range(0, patrolPoints.Length)].position.z);
        SetDestination(destination);
    }

    void Update()
    {
        if(enemy._isPatroling)
        {            
            if (agent.remainingDistance < 0.5f)
            {
                destination = new Vector3(patrolPoints[Random.Range(0, patrolPoints.Length)].position.x, 0, patrolPoints[Random.Range(0, patrolPoints.Length)].position.z);
                SetDestination(destination);
            }
        }
        else
        {                      
            SetDestination(enemy.Target.position);
        }
    }

    void SetDestination(Vector3 target)
    {
        
        agent.SetDestination(target);
    }
}
