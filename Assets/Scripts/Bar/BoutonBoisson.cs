using UnityEngine;

public class BoutonBoisson : MonoBehaviour
{
    public string nomBoisson; // Nom de la boisson associée à ce bouton
    public BarmanController barmanController;

    public void ServirBoisson()
    {
        barmanController.ServirBoisson(nomBoisson);
    }
}