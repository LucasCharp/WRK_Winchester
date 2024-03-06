using UnityEngine;

public class PNJCanvasController : MonoBehaviour
{
    public GameObject[] canvases; // Liste des canvas li�s au PNJ
    private bool isCanvasActive = false; // Indique si les canvas sont actuellement activ�s

    private void Start()
    {
        // D�sactive tous les canvas au d�marrage
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        // Inverse l'�tat des canvas (actif / inactif) lorsque le PNJ est cliqu�
        isCanvasActive = !isCanvasActive;

        // Active ou d�sactive tous les canvas li�s au PNJ en cons�quence
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(isCanvasActive);
        }
        // Active ou d�sactive tous les canvas li�s au PNJ lorsqu'il est cliqu�
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    public void DisableAllCanvases()
    {
        // D�sactive tous les canvas li�s au PNJ
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }
    }
}