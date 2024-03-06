using UnityEngine;

public class PNJCanvasController : MonoBehaviour
{
    public GameObject[] canvases; // Liste des canvas liés au PNJ
    private bool isCanvasActive = false; // Indique si les canvas sont actuellement activés

    private void Start()
    {
        // Désactive tous les canvas au démarrage
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        // Inverse l'état des canvas (actif / inactif) lorsque le PNJ est cliqué
        isCanvasActive = !isCanvasActive;

        // Active ou désactive tous les canvas liés au PNJ en conséquence
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(isCanvasActive);
        }
        // Active ou désactive tous les canvas liés au PNJ lorsqu'il est cliqué
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    public void DisableAllCanvases()
    {
        // Désactive tous les canvas liés au PNJ
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }
    }
}