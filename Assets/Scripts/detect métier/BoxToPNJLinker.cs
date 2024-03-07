using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToPNJLinker : MonoBehaviour
{
    public GameObject linkedPNJ; // PNJ li� � cette box
    public GameObject[] pnjCanvases; // Liste des canvas li�s au PNJ
    public int boxNumber; // Ajout de la variable boxNumber
    public string PlayerPrefsKey; // Cl� unique pour ce PNJ dans PlayerPrefs

    public void Start()
    {
        // G�n�rer une cl� unique pour ce PNJ
        PlayerPrefsKey = "BoxNumber_" + linkedPNJ.name;

        // Charger la valeur de boxNumber depuis PlayerPrefs
        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            boxNumber = PlayerPrefs.GetInt(PlayerPrefsKey);
        }
        Debug.Log("boxNumberboxtopnj");
    }

    // M�thode pour lier le PNJ � sa bo�te respective en utilisant la valeur de boxNumber
    public void LinkPNJToBox(int boxNumber)
    {
        this.boxNumber = boxNumber; // Assigner le num�ro de bo�te donn� au PNJ

        // Sauvegarder la valeur de boxNumber dans PlayerPrefs
        PlayerPrefs.SetInt(PlayerPrefsKey, boxNumber);
        PlayerPrefs.Save(); // S'assurer que les donn�es sont sauvegard�es imm�diatement
        Debug.Log("linkint");
    }
}