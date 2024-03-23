using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VideurPNJ : MonoBehaviour
{
    public float entryInterval;
    private Coroutine repeatingInvoke;
    public Button buttonBagarre;
    public Image imgBagarre;
    public GameObject Fighter;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool hasSeparate = false;
    private Vector3 standPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartRepeatingInvoke(entryInterval);
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        standPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isFighting") == true && hasSeparate == false)
        {
            SeparatePeople();
        }
    }
    void InvokeInQueue()
    {
        GameObject spawner = GameObject.Find("Spawner");
        SpawnManager spawnManager = spawner.GetComponent<SpawnManager>();
        if (spawnManager.numberOfPeopleInQueue > 0)
        {
            // Trouver tous les GameObjects avec le tag "PNJ" dans la scène
            GameObject[] pnjArray = GameObject.FindGameObjectsWithTag("PNJ");

            // Boucler sur tous les GameObjects trouvés
            foreach (GameObject pnj in pnjArray)
            {
                // Récupérer le composant RandomNavMeshMovement attaché à chaque GameObject
                RandomNavMeshMovement randomMove = pnj.GetComponent<RandomNavMeshMovement>();
                if (randomMove != null)
                {
                    // Appeler la fonction InQueue() du script RandomNavMeshMovement
                    randomMove.QuitQueue();
                }
                else
                {

                }
            }
        }
    }

    void StartRepeatingInvoke(float interval)
    {
        // Arrêter l'invocation répétée actuelle, s'il y en a une
        StopRepeatingInvoke();

        if (interval > 0)
        {
            // Démarrer une nouvelle invocation répétée avec l'intervalle donné
            repeatingInvoke = StartCoroutine(RepeatInvoke(interval));
        }
    }

    // Fonction pour arrêter l'invocation répétée
    void StopRepeatingInvoke()
    {
        if (repeatingInvoke != null)
        {
            StopCoroutine(repeatingInvoke);
            repeatingInvoke = null;
        }
    }

    public void ActivateButton(GameObject fighter)
    {
        buttonBagarre.gameObject.SetActive(true);
        imgBagarre.gameObject.SetActive(false);
        Fighter = fighter;
    }

    public void GoToFighter()
    {
        animator.SetBool("isFighting", true);
        navMeshAgent.SetDestination(Fighter.transform.position);
        
    }

    public void SeparatePeople()
    {
        NavMeshAgent otherNavMeshAgent = Fighter.GetComponent<NavMeshAgent>();
        RandomNavMeshMovement fighterScript = Fighter.GetComponent<RandomNavMeshMovement>();

        float distance = Vector3.Distance(transform.position, Fighter.transform.position);


        float triggerDistance = navMeshAgent.radius + otherNavMeshAgent.radius;

        if (distance < triggerDistance + 1f)
        {
            print("touche");
            animator.SetBool("isMenacing", true);
            print("Il le menance");
            fighterScript.StopFight();
            hasSeparate = true;
            navMeshAgent.isStopped = true;
        }
    }

    public void GoBack()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(standPosition);
    }

    // Coroutine pour l'invocation répétée
    IEnumerator RepeatInvoke(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Appeler la fonction InvokeInQueue
            InvokeInQueue();

            // Sortir de la coroutine si l'interval a changé
            if (interval != entryInterval)
            {
                break;
            }
        }

        // Redémarrer l'invocation répétée avec le nouvel intervalle
        StartRepeatingInvoke(entryInterval);
    }
}
