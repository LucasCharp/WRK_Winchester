using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ClientController : MonoBehaviour
{
    private bool inBarZone = false;
    public string commande = "";
    private bool commandeFulfilled = false;
    private BarmanController barmanController;
    private bool satisfait = false;
    private float tempsInitial;
    public GameManager gameManager;
    private bool tropAttendu = false;
    private Animator animator;
    public Image bulleCommande;
    public Image boisson;
    public Image[] boissonImages;
    private Dictionary<string, Image> boissonsImages = new Dictionary<string, Image>();
    private bool unParUn = true;
    public RandomNavMeshMovement maBool;
    private void Update()
    {
        if(commandeFulfilled == true)
        {
            satisfait = true;
            animator.SetBool("willDrink", false);
            animator.SetBool("isWalking", true);
        }
    }
    private void Start()
    {
        bulleCommande.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        GameObject moneyManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = moneyManagerObject.GetComponent<GameManager>();
        }

        boissonsImages.Add("Absynthe", boissonImages[0]);
        boissonsImages.Add("Bière", boissonImages[1]);
        boissonsImages.Add("Champagne", boissonImages[2]);
        boissonsImages.Add("Cognac", boissonImages[3]);
        boissonsImages.Add("Vin", boissonImages[4]);

        barmanController = FindObjectOfType<BarmanController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BarmanZone"))
        {
            animator.SetBool("willDrink", true);
            animator.SetBool("isWalking", false);
            inBarZone = true;
            if (barmanController != null)
            {
                DemanderBoisson();
                barmanController.AjouterCommande(this, commande);
            }
        }
    }
    public void QuitterZoneBarman()
    {
        unParUn = true;
        bulleCommande.gameObject.SetActive(false);
        animator.SetBool("willDrink", false);
        animator.SetBool("isWalking", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BarmanZone"))
        {
            inBarZone = false;
            unParUn = true;
            bulleCommande.gameObject.SetActive(false);
            animator.SetBool("willDrink", false);
            animator.SetBool("isWalking", true);
        }
    }

    private void DemanderBoisson()
    {
        if (unParUn == true)
        {
            unParUn = false;
            if (!commandeFulfilled)
            {
                string[] boissons = { "Biere", "Vin", "Champagne", "Cognac", "Absynthe" };
                commande = boissons[Random.Range(0, boissons.Length)];
                AfficherCommande();
                //barmanController.TraiterCommandes();
                Debug.Log("Commande reçue : " + commande);
                tempsInitial = Time.time; // Stocker le temps initial
                Invoke("SimulerAttente", 15f);
            }
        }
    }

    private void AfficherCommande()
    {
        bulleCommande.gameObject.SetActive(true);
        Image imageBoisson = boissonsImages[commande];
        imageBoisson.gameObject.SetActive(true);
    }

    private void CacherCommande()
    {
        bulleCommande.gameObject.SetActive(false);
        Image imageBoisson = boissonsImages[commande];
        imageBoisson.gameObject.SetActive(false);
    }
    private void SimulerAttente()
    {
        if (!commandeFulfilled)
        {
            if (satisfait == false)
            {
                Debug.Log("Client insatisfait.");
                tropAttendu = true;
                LivrerBoisson(false); // Passer false pour indiquer que la boisson n'a pas été correctement servie
            }
        }
    }

    // Modifier la signature pour accepter un booléen
    public void LivrerBoisson(bool reponse)
    {
        if (inBarZone && !commandeFulfilled)
        {
            if (reponse)
            {
                float duree = Time.time - tempsInitial; // Calculer la durée écoulée
                float TimePoint = 15 - (Time.time - tempsInitial);
                Debug.Log(TimePoint + " s restante");
                int multiplicateur = Mathf.FloorToInt(TimePoint / 6f); // Utiliser la durée comme multiplicateur (par exemple, 1 point pour chaque tranche de 10 secondes)
                int scoreGagne = 10 * (multiplicateur + 1); // Calculer le score gagné en fonction du multiplicateur
                gameManager.AugmenterScore(scoreGagne); // Accéder à la méthode AugmenterScore à partir de l'instance de GameManager
                Debug.Log("Merci ! Score gagné : ");
                gameManager.AugmenterArgent(20);
                animator.SetBool("willDrink", false);
                animator.SetBool("isWalking", true);
                maBool.isDrunk = true;
            }
            else
            {
                if (tropAttendu == true)
                {
                    int scoreGagne = -20;
                    gameManager.AugmenterScore(scoreGagne); // Accéder à la méthode AugmenterScore à partir de l'instance de GameManager
                    Debug.Log("Trop Lent");
                    animator.SetBool("willDrink", false);
                    animator.SetBool("isWalking", true);
                }
                else 
                {
                    int scoreGagne = -8;
                    gameManager.AugmenterScore(scoreGagne); // Accéder à la méthode AugmenterScore à partir de l'instance de GameManager
                    Debug.Log("t'es con?");
                    animator.SetBool("willDrink", false);
                    animator.SetBool("isWalking", true);
                    maBool.isDrunk = true;
                }
            }
            commandeFulfilled = true;
        }
        CacherCommande();
    }

    public bool EstCommandeFulfilled()
    {
        return commandeFulfilled;
    }
}
