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
    public float errorMargin = 0.1f;

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

        if (AreEqual(transform.position, standPosition) && hasSeparate)
        {
            print("il est arrive");
            animator.SetBool("isFighting", false);
            navMeshAgent.isStopped = true;
            hasSeparate = false;
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
        print("go");
        navMeshAgent.isStopped = false;
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
        buttonBagarre.gameObject.SetActive(false);
        imgBagarre.gameObject.SetActive(true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(standPosition);
    }

    public bool AreEqual(Vector3 a, Vector3 b)
    {
        // Calculer la distance entre les composantes X, Y et Z des Vector3
        float distanceX = Mathf.Abs(a.x - b.x);
        float distanceY = Mathf.Abs(a.y - b.y);
        float distanceZ = Mathf.Abs(a.z - b.z);

        // Si toutes les distances sont inférieures ou égales à la marge d'erreur, les Vector3 sont considérés comme égaux
        return distanceX <= errorMargin && distanceY <= errorMargin && distanceZ <= errorMargin;
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
