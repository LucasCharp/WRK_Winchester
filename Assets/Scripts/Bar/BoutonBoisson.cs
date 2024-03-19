using UnityEngine;

public class BoutonBoisson : MonoBehaviour
{
    public string nomBoisson; // Nom de la boisson associ�e � ce bouton
    public BarmanController barmanController;

    public void ServirBoisson()
    {
        barmanController.ServirBoisson(nomBoisson);
    }
}