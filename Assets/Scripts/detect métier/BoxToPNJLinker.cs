using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToPNJLinker : MonoBehaviour
{
    public GameObject linkedPNJ; // PNJ lié à cette box
    public GameObject[] pnjCanvases; // Liste des canvas liés au PNJ
    public int boxNumber; // Ajout de la variable boxNumber
    public string PlayerPrefsKey; // Clé unique pour ce PNJ dans PlayerPrefs

    public void Start()
    {
        // Générer une clé unique pour ce PNJ
        PlayerPrefsKey = "BoxNumber_" + linkedPNJ.name;

        // Charger la valeur de boxNumber depuis PlayerPrefs
        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            boxNumber = PlayerPrefs.GetInt(PlayerPrefsKey);
        }
        Debug.Log("boxNumberboxtopnj");
    }

    // Méthode pour lier le PNJ à sa boîte respective en utilisant la valeur de boxNumber
    public void LinkPNJToBox(int boxNumber)
    {
        this.boxNumber = boxNumber; // Assigner le numéro de boîte donné au PNJ

        // Sauvegarder la valeur de boxNumber dans PlayerPrefs
        PlayerPrefs.SetInt(PlayerPrefsKey, boxNumber);
        PlayerPrefs.Save(); // S'assurer que les données sont sauvegardées immédiatement
        Debug.Log("linkint");
    }
}