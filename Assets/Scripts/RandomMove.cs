using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public float minMoveDelay = 1f; // D�lai minimum entre chaque d�placement
    public float maxMoveDelay = 3f; // D�lai maximum entre chaque d�placement


    private NavMeshAgent navMeshAgent;
    private float moveDelayTimer;
    private Vector3 randomDestination;
    private Vector3 lastDestination;
    private Animator animator;
    public Vector3 EndZonePosition;
    private Vector3 PubOpposite;
    bool hasManagedDoor = false;

    private GameObject[] doors;

    private void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        SetRandomDestination();
    }

    void Update()
    {
        // Si le NavMeshAgent a atteint sa destination ou s'il est bloqu�, choisir une nouvelle destination al�atoire
            CheckCollisionWithOtherPNJs();
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

            //Si le PNJ a r�alis� 3 actions, il s'en va du pub
            if (animator.GetInteger("actions") == 3)
            {
                animator.SetBool("isWalking", true);
                if (!hasManagedDoor)
                {
                    foreach (GameObject door in doors)
                    {
                        DoorLeft doorScript = door.GetComponent<DoorLeft>();
                        doorScript.OpenDoor();
                    }
                    // Mettez � jour la variable pour indiquer que ManageDoor() a �t� appel�
                    hasManagedDoor = true;
                }
                navMeshAgent.SetDestination(EndZonePosition);
            }

            //Si le PNJ est devant la porte d'entr�e mais qu'il n'a pas r�alis� ses 3 actions, il rerentre dans le pub
            if (animator.GetBool("shouldGoIn") == true)
            {
                animator.SetBool("isWalking", true);
                if (animator.GetBool("isOutOfWall") == true) 
                {
                    PubOpposite = new Vector3(Random.Range(0f, 2.3f), 0.08950949f, Random.Range(2.5f, 4f));
                    navMeshAgent.SetDestination(PubOpposite);
                }
                else
                {
                    PubOpposite = new Vector3(Random.Range(-3f, -0.8f), 0.08950949f, Random.Range(-2f, 1.5f));
                    navMeshAgent.SetDestination(PubOpposite);
                }
                animator.SetBool("shouldGoIn", false);
            }
    }

    void CheckCollisionWithOtherPNJs()
    {
        if (animator.GetBool("isDancing") == false)
        {
            // R�cup�rer tous les PNJs avec le tag "PNJ"
            GameObject[] otherPNJs = GameObject.FindGameObjectsWithTag("PNJ");

            foreach (GameObject otherPNJ in otherPNJs)
            {
                // V�rifier si c'est un autre PNJ et non le PNJ actuel
                if (otherPNJ != gameObject)
                {
                    // Obtenir le NavMeshAgent de l'autre PNJ
                    NavMeshAgent otherNavMeshAgent = otherPNJ.GetComponent<NavMeshAgent>();

                    // Calculer la distance entre les deux agents
                    float distance = Vector3.Distance(transform.position, otherPNJ.transform.position);

                    // D�finir une distance de d�clenchement (ajustez-la selon vos besoins)
                    float triggerDistance = navMeshAgent.radius + otherNavMeshAgent.radius;

                    // Si les NavMeshAgents sont suffisamment proches
                    if (distance < triggerDistance)
                    {
                        // D�clencher votre code ici
                        animator.SetBool("isWalking", true);
                        navMeshAgent.SetDestination(lastDestination);
                    }
                }
            }
        }
    }

    void SetRandomDestination()
    {
        // G�n�rer une destination al�atoire � l'int�rieur du NavMesh
        
        if (animator.GetBool("isDancing") == false)
        {
            lastDestination = randomDestination;
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
}
