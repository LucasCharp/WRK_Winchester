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
    public MoneyManager moneyManager;
    private bool tropAttendu = false;
    private int drinkCost = 25;

    private Animator animator;

    public Image bulleCommande;
    //public Image boisson;
    public Image[] boissonImages;
    private Dictionary<string, Image> boissonsImages = new Dictionary<string, Image>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        GameObject moneyManagerObject = GameObject.FindWithTag("MoneyManager");
        if (gameManagerObject != null)
        {
            moneyManager = moneyManagerObject.GetComponent<MoneyManager>();
        }

        boissonsImages.Add("Absynthe", boissonImages[0]);
        boissonsImages.Add("Bière", boissonImages[1]);
        boissonsImages.Add("Champagne", boissonImages[2]);
        boissonsImages.Add("Cognac", boissonImages[3]);
        boissonsImages.Add("Vin", boissonImages[4]);

        bulleCommande.gameObject.SetActive(false);
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
                barmanController.AjouterCommande(this, commande);
                DemanderBoisson();
            }
        }
    }
    public void QuitterZoneBarman()
    {
        satisfait = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BarmanZone"))
        {
            inBarZone = false;
            satisfait = true;
            bulleCommande.gameObject.SetActive(false);
        }
    }

    private void DemanderBoisson()
    {
        if (!commandeFulfilled)
        {
            string[] boissons = { "Bière", "Vin", "Champagne", "Cognac", "Absynthe" };
            commande = boissons[Random.Range(0, boissons.Length)];
            AfficherCommande();
            Debug.Log("Commande reçue : " + commande);
            tempsInitial = Time.time; // Stocker le temps initial
            Invoke("SimulerAttente", 7f);
        }
    }

    private void AfficherCommande()
    {
        bulleCommande.gameObject.SetActive(true);
        Image imageBoisson = boissonsImages[commande];
        imageBoisson.gameObject.SetActive(true);
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
                float TimePoint = 40 - (Time.time - tempsInitial);
                Debug.Log(TimePoint + " s restante");
                int multiplicateur = Mathf.FloorToInt(TimePoint / 10f); // Utiliser la durée comme multiplicateur (par exemple, 1 point pour chaque tranche de 10 secondes)
                int scoreGagne = 10 * (multiplicateur + 1); // Calculer le score gagné en fonction du multiplicateur
                GameManager.instance.AugmenterScore(scoreGagne); // Accéder à la méthode AugmenterScore à partir de l'instance de GameManager
                Debug.Log("Merci ! Score gagné : ");
                moneyManager.moneyChange = drinkCost;
                moneyManager.OnMoneyChange();
                animator.SetBool("willDrink", false);
                animator.SetBool("isWalking", true);
            }
            else
            {
                if (tropAttendu == true)
                {
                    int scoreGagne = -20;
                    GameManager.instance.AugmenterScore(scoreGagne); // Accéder à la méthode AugmenterScore à partir de l'instance de GameManager
                    Debug.Log("Trop Lent");
                    animator.SetBool("willDrink", false);
                    animator.SetBool("isWalking", true);
                }
                else 
                {
                    int scoreGagne = -8;
                    GameManager.instance.AugmenterScore(scoreGagne); // Accéder à la méthode AugmenterScore à partir de l'instance de GameManager
                    Debug.Log("t'es con?");
                }
            }
            commandeFulfilled = true;
        }
    }

    public bool EstCommandeFulfilled()
    {
        return commandeFulfilled;
    }
}
