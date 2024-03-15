using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testrandom : MonoBehaviour
{
    public float minMoveDelay = 1f; // Délai minimum entre chaque déplacement
    public float maxMoveDelay = 3f; // Délai maximum entre chaque déplacement
    public float placeInQueue = 0f;
    public Vector3 queueStart;
    public Vector3 queueSecond;
    public Vector3 queueThird;
    public Vector3 queueLast;

    private NavMeshAgent navMeshAgent;
    private float moveDelayTimer;
    private Vector3 randomDestination;
    public Vector3 EndZonePosition;

    private GameObject[] doors;

    private void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Invoke("SetRandomDestination()", 5f);
    }
    void SetRandomDestination()

    {
        {
            Vector3 randomDirection = Random.insideUnitSphere * 5f; // Rayon de 10 unités
            randomDirection += transform.position;
            randomDirection.y = 0;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);
            randomDestination = hit.position;

            // Définir la destination pour le NavMeshAgent
            navMeshAgent.SetDestination(randomDestination);
        }

    }
    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            if (moveDelayTimer <= 0f)
            {
                Invoke("SetRandomDestination()", 5f);
                moveDelayTimer = Random.Range(minMoveDelay, maxMoveDelay);
            }
        }
    }
}