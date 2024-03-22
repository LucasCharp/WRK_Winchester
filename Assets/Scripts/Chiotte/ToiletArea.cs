using System.Collections.Generic;
using UnityEngine;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;
    private Coroutine waitingCoroutine;
    public bool inChiotteZone = false;
    public GameObject PNJ;
    private ClientChiotte lesClientsDesChiottes = new ClientChiotte();
    public static ToiletArea instance;

    private void OnTriggerEnter(Collider PNJ)
    {
        if (PNJ.CompareTag("PNJ"))
        {
            Debug.Log("Jesuila");
            inChiotteZone = true;
            // R�cup�rer le script ClientChiotte attach� au PNJ
            ClientChiotte clientChiotte = PNJ.GetComponent<ClientChiotte>();
            Debug.Log(clientChiotte+ "je l'ai");
            if (clientChiotte != null)
            {
                Debug.Log("la");
                lesClientsDesChiottes.UseToilet();
                Debug.Log("la2");
            }
            else
            {
                Debug.LogWarning("Script ClientChiotte non trouv� sur le PNJ !");
                // Si aucun toilette n'est disponible imm�diatement, d�marrer la coroutine d'attente
                waitingCoroutine = StartCoroutine(lesClientsDesChiottes.WaitInToiletZone());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            inChiotteZone = false;
            // Arr�ter la coroutine si le PNJ quitte la zone des toilettes
            if (waitingCoroutine != null)
            {
                StopCoroutine(waitingCoroutine);
            }
        }
    }
    public bool IsToiletAvailable()
    {
        // V�rifier si au moins un toilette est disponible
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
        // R�cup�rer un toilette disponible au hasard
        foreach (var toilet in toilets)
        {
            print("ouiouiuiuiui");
            print(toilet.isOccupied + "e"); // Utilisez toilet au lieu de Toilet.instance
            print(toilet.isDirty + "o"); // Utilisez toilet au lieu de Toilet.instance
            if (!toilet.isOccupied && !toilet.isDirty) // Utilisez toilet au lieu de Toilet.instance
            {
                Debug.Log("il est libre");
                return toilet;
            }
        }
        return null;
    }
}