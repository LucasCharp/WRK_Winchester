using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public float minMoveDelay = 1f; // Délai minimum entre chaque déplacement
    public float maxMoveDelay = 3f; // Délai maximum entre chaque déplacement

    public Transform secondNavMeshDestination; // Destination dans le deuxième NavMesh

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
        // Générer une destination aléatoire à l'intérieur du NavMesh
        
        if (animator.GetBool("isDancing") == false)
        {
            animator.SetBool("isWalking", true);
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

    void MoveToSecondNavMesh()
    {
        // Déplacer l'agent vers la destination dans le deuxième NavMesh
        navMeshAgent.SetDestination(secondNavMeshDestination.position);
    }
}
