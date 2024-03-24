using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;
    private Coroutine waitingCoroutine;
    public bool inChiotteZone = false;
    public GameObject PNJ;
    private ClientChiotte lesClientsDesChiottes = new ClientChiotte();
    public static ToiletArea instance;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    private void OnTriggerEnter(Collider PNJ)
    {
        if (PNJ.CompareTag("PNJ"))
        {
            inChiotteZone = true;
            // Récupérer le script ClientChiotte attaché au PNJ
            ClientChiotte clientChiotte = PNJ.GetComponent<ClientChiotte>();
            navMeshAgent = PNJ.GetComponent<NavMeshAgent>();
            animator = PNJ.GetComponent<Animator>();
            if (clientChiotte != null&& animator.GetBool("hasAlreadyShit") == false)
            {
                lesClientsDesChiottes.UseToilet();
                Debug.Log("la2");
            }
            else
            {
                Debug.LogWarning("Script ClientChiotte non trouvé sur le PNJ !");
                // Si aucun toilette n'est disponible immédiatement, démarrer la coroutine d'attente
                waitingCoroutine = StartCoroutine(lesClientsDesChiottes.WaitInToiletZone());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            inChiotteZone = false;
            // Arrêter la coroutine si le PNJ quitte la zone des toilettes
            if (waitingCoroutine != null)
            {
                StopCoroutine(waitingCoroutine);
            }
        }
    }
    public bool IsToiletAvailable()
    {
        // Vérifier si au moins un toilette est disponible
        foreach (var toilet in toilets)
        {
            if (!Toilet.instance.isOccupied && !Toilet.instance.isDirty)
            {
                return true;
            }
        }
        return false;
    }

    public Toilet GetAvailableToilet()
    {
        Debug.Log("oui3");
        // Récupérer un toilette disponible au hasard
        foreach (var toilet in toilets)
        {
            print(toilet.isOccupied + "e"); // Utilisez toilet au lieu de Toilet.instance
            print(toilet.isDirty + "o"); // Utilisez toilet au lieu de Toilet.instance
            if (!toilet.isOccupied && !toilet.isDirty) // Utilisez toilet au lieu de Toilet.instance
            {
                return toilet;
            }
        }
        return null;
    }

    public void LanceInfo()
    {
        gameManager.AugmenterArgent(15);
    }
}