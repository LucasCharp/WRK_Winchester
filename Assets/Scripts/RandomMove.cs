using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public float minMoveDelay = 1f; // Délai minimum entre chaque déplacement
    public float maxMoveDelay = 3f; // Délai maximum entre chaque déplacement


    private NavMeshAgent navMeshAgent;
    private float moveDelayTimer;
    private Vector3 randomDestination;
    private Animator animator;
    public Vector3 EndZonePosition;
    private Vector3 PubMiddle;

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
            if (animator.GetInteger("actions") == 3)
            {
                navMeshAgent.SetDestination(EndZonePosition);
            }

            if (animator.GetBool("shouldGoIn") == true)
            {
                Debug.Log("denr");
                Debug.Log(PubMiddle);
                PubMiddle = new Vector3 (Random.Range(-3f, - 0.8f), 0.08950949f, Random.Range(-2f, 1.5f));
                navMeshAgent.SetDestination(PubMiddle);
                animator.SetBool("shouldGoIn", false);
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
}
