using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonBoisson : MonoBehaviour
{
    public string nomBoisson; // Nom de la boisson associ�e � ce bouton
    public BarmanController barmanController;

    public void ServirBoisson()
    {
        // Appel de la m�thode pour servir la boisson dans BarmanController
        barmanController.ServirBoisson(nomBoisson);
    }
}