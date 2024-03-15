using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;
using UnityEngine.InputSystem.Controls;

public class RandomNavMeshMovement : MonoBehaviour
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
        //SetRandomDestination();
        InQueue();
    }

    void Update()
    {
        if (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("oui");
            QuitQueue();
        }

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
                if (animator.GetBool("willDance") == true)
                {
                    animator.SetBool("isDancing", true);
                }
            }
        }



        if (animator.GetBool("hasEnterPub") == true)
        {
            CheckCollisionWithOtherPNJs();
            //Si le PNJ a réalisé 3 actions, il s'en va du pub
            if (animator.GetInteger("actions") == 3)
            {
                animator.SetBool("isWalking", true);
                if (!hasManagedDoor)
                {
                    foreach (GameObject door in doors)
                    {
                        DoorLeft doorScript = door.GetComponent<DoorLeft>();
                        doorScript.howManyPnjUseDoors += 1;
                        if (doorScript.howManyPnjUseDoors == 1)
                        {
                            doorScript.OpenDoor();
                        }
                    }
                    // Mettez à jour la variable pour indiquer que ManageDoor() a été appelé
                    hasManagedDoor = true;
                }
                navMeshAgent.SetDestination(EndZonePosition);
            }

            //Si le PNJ est devant la porte d'entrée mais qu'il n'a pas réalisé ses 3 actions, il rerentre dans le pub
            if (animator.GetBool("shouldGoIn") == true)
            {
                animator.SetBool("isWalking", true);
                if (animator.GetBool("isOutOfWall") == true)
                {
                    PubOpposite = new Vector3(Random.Range(0f, 2.3f), 0f, Random.Range(2.5f, 4f));
                    navMeshAgent.SetDestination(PubOpposite);
                    animator.SetBool("isOutOfWall", false);
                }
                else
                {
                    PubOpposite = new Vector3(Random.Range(-3f, -0.8f), 0f, Random.Range(-2f, 1.5f));
                    navMeshAgent.SetDestination(PubOpposite);
                }
                animator.SetBool("shouldGoIn", false);
            }
        }
    }

    void CheckCollisionWithOtherPNJs()
    {
        if (animator.GetBool("isDancing") == false)
        {
            // Récupérer tous les PNJs avec le tag "PNJ"
            GameObject[] otherPNJs = GameObject.FindGameObjectsWithTag("PNJ");

            foreach (GameObject otherPNJ in otherPNJs)
            {
                // Vérifier si c'est un autre PNJ et non le PNJ actuel
                if (otherPNJ != gameObject)
                {
                    // Obtenir le NavMeshAgent de l'autre PNJ
                    NavMeshAgent otherNavMeshAgent = otherPNJ.GetComponent<NavMeshAgent>();

                    // Calculer la distance entre les deux agents
                    float distance = Vector3.Distance(transform.position, otherPNJ.transform.position);

                    // Définir une distance de déclenchement (ajustez-la selon vos besoins)
                    float triggerDistance = navMeshAgent.radius + otherNavMeshAgent.radius;

                    // Si les NavMeshAgents sont suffisamment proches
                    if (distance < triggerDistance)
                    {
                        // Déclencher votre code ici
                        animator.SetBool("isWalking", true);
                        SetRandomDestination();
                    }
                }
            }
        }
    }

    void SetRandomDestination()
    {
        if (animator.GetBool("hasEnterPub") == true)
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

    public void InQueue()
    {
        GameObject spawner = GameObject.Find("Spawner");
        SpawnManager spawnManager = spawner.GetComponent<SpawnManager>();
        if (spawnManager.isFull == false)
        {
            spawnManager.StartQueue();
            if (spawnManager.numberOfPeopleInQueue == 1)
            {
                navMeshAgent.SetDestination(queueStart);
                placeInQueue = 1;
            }
            else if (spawnManager.numberOfPeopleInQueue == 2)
            {
                navMeshAgent.SetDestination(queueSecond);
                placeInQueue = 2;
            }
            else if (spawnManager.numberOfPeopleInQueue == 3)
            {
                navMeshAgent.SetDestination(queueThird);
                placeInQueue = 3;
            }
            else if (spawnManager.numberOfPeopleInQueue == 4)
            {
                navMeshAgent.SetDestination(queueLast);
                placeInQueue = 4;
            }
            else if (spawnManager.numberOfPeopleInQueue == 5)
            {
                placeInQueue = 5;
            }
        }           
    }

    public void QuitQueue()
    {
        GameObject spawner = GameObject.Find("Spawner");
        SpawnManager spawnManager = spawner.GetComponent<SpawnManager>();
        Debug.Log("Il quitte la queue");
        animator.SetBool("isWalking", true);
        if (placeInQueue == 1)
        {
            spawnManager.numberOfPeopleInQueue -= 1;
            foreach (GameObject door in doors)
            {
                DoorLeft doorScript = door.GetComponent<DoorLeft>();
                doorScript.howManyPnjUseDoors += 1;
                if (doorScript.howManyPnjUseDoors == 1)
                {
                    doorScript.OpenDoor();
                }
            }
            PubOpposite = new Vector3(Random.Range(0f, 2.3f), 0f, Random.Range(2.5f, 4f));
            navMeshAgent.SetDestination(PubOpposite);
            placeInQueue = 0;
        }
        else if (placeInQueue == 2)
        {
            navMeshAgent.SetDestination(queueStart);
            placeInQueue = 1;
        }
        else if (placeInQueue == 3)
        {
            navMeshAgent.SetDestination(queueSecond);
            placeInQueue = 2;
        }
        else if (placeInQueue == 4)
        {
            navMeshAgent.SetDestination(queueThird);
            placeInQueue = 3;
        }
        else if (placeInQueue == 5)
        {
            navMeshAgent.SetDestination(queueLast);
            placeInQueue = 4;
        }



        // Réinitialisez la condition de file d'attente pleine
        if (spawnManager.numberOfPeopleInQueue < spawnManager.maxQueueSize)
        {
            spawnManager.isFull = false;
        }
    }
}
