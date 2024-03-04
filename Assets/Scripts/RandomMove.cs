using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public float minMoveDelay = 1f; // Délai minimum entre chaque déplacement
    public float maxMoveDelay = 3f; // Délai maximum entre chaque déplacement

    private NavMeshAgent navMeshAgent;
    private float moveDelayTimer;
    private Vector3 randomDestination;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        // Si le NavMeshAgent a atteint sa destination ou s'il est bloqué, choisir une nouvelle destination aléatoire
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            if (moveDelayTimer <= 0f)
            {
                SetRandomDestination();
                moveDelayTimer = Random.Range(minMoveDelay, maxMoveDelay);
            }
            else
            {
                moveDelayTimer -= Time.deltaTime;
            }
        }
    }

    void SetRandomDestination()
    {
        // Générer une destination aléatoire à l'intérieur du NavMesh
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
