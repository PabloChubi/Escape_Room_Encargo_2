using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[DisallowMultipleComponent]

public class EnemyController : MonoBehaviour
{
   NavMeshAgent agent;
   [SerializeField] Transform[] waypoint;
   [SerializeField] int nextWaypoint;
   [SerializeField] float idleTimer = 2f;
   [SerializeField] bool hasDetectedPlayer;
   [SerializeField] Transform player;

   public enum EnemyPatrolState
   {
    Idle,
    Walking,
    Chase,
   }

 EnemyPatrolState currentState;

 private void Awake()
 {
    agent = GetComponent<NavMeshAgent>();
 }

 private void Start()
 {
    ChangeEnemyState(EnemyPatrolState.Walking);
 }

 private void Update()
 {
    switch (currentState)
    {
        case EnemyPatrolState.Idle:
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                idleTimer = 2;
                ChangeEnemyState(EnemyPatrolState.Walking);
                
            }
            break;

        case EnemyPatrolState.Walking:
        
            if (hasDetectedPlayer)
        {
            ChangeEnemyState(EnemyPatrolState.Chase);            
        }
        
        

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Llegué al waypoint " + nextWaypoint);
            nextWaypoint++;
            if (nextWaypoint > waypoint.Length)
            {
                nextWaypoint = 0;
            }
            Debug.Log("El siguiente waypoint es el " + nextWaypoint);
            ChangeEnemyState(EnemyPatrolState.Idle);      
        }
        break;

        case EnemyPatrolState.Chase:
            if (hasDetectedPlayer)
         agent.SetDestination(player.position);
            else 
         ChangeEnemyState(EnemyPatrolState.Idle);
            break;

        }
        if (hasDetectedPlayer)
        {
            ChangeEnemyState(EnemyPatrolState.Chase);
        }
    
    }


void ChangeEnemyState(EnemyPatrolState newState)
{
    currentState = newState;
    if (currentState == EnemyPatrolState.Walking)
    {
        agent.SetDestination(waypoint[nextWaypoint].position);
    }
}

void OnTriggerEnter(Collider other)
{
    if (other.name == "Player")
    {
        hasDetectedPlayer = true;
        player = other.transform;
    }
}

void OnTriggerExit(Collider other)
{
    if (other.name == "Player")
    {
        hasDetectedPlayer = false;
        player = null;
    }
}

}



