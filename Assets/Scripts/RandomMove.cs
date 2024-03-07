using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public float minMoveDelay = 1f; // D�lai minimum entre chaque d�placement
    public float maxMoveDelay = 3f; // D�lai maximum entre chaque d�placement

    public Transform secondNavMeshDestination; // Destination dans le deuxi�me NavMesh

    private NavMeshAgent navMeshAgent;
    private float moveDelayTimer;
    private Vector3 randomDestination;
    private Animator animator;
    public Vector3 EndZonePosition;
    private bool moveToSecondNavMesh;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        SetRandomDestination();
    }

    void Update()
    {
        // Si le NavMeshAgent a atteint sa destination ou s'il est bloqu�, choisir une nouvelle destination al�atoire
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
                    animator.SetBool("isWalking", false);
                    if (animator.GetBool("willDance") == true )
                    {
                        animator.SetBool("isDancing", true);
                    }
                }
            }
            if (animator.GetInteger("actions") == 3 && !moveToSecondNavMesh)
        {
                MoveToSecondNavMesh();
                moveToSecondNavMesh = true;
                navMeshAgent.SetDestination(EndZonePosition);
            }
    }

    void SetRandomDestination()
    {
        // G�n�rer une destination al�atoire � l'int�rieur du NavMesh
        
        if (animator.GetBool("isDancing") == false)
        {
            animator.SetBool("isWalking", true);
            Vector3 randomDirection = Random.insideUnitSphere * 5f; // Rayon de 10 unit�s
            randomDirection += transform.position;
            randomDirection.y = 0;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);
            randomDestination = hit.position;

            // D�finir la destination pour le NavMeshAgent
            navMeshAgent.SetDestination(randomDestination);
        }
    }

    void MoveToSecondNavMesh()
    {
        // D�placer l'agent vers la destination dans le deuxi�me NavMesh
        navMeshAgent.SetDestination(secondNavMeshDestination.position);
    }
}
