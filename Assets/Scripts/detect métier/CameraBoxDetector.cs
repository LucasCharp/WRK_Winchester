using UnityEngine;

public class CameraBoxDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si la caméra entre en collision avec une box
        if (other.gameObject.layer == LayerMask.NameToLayer("Box"))
            Debug.Log("Lacaméracollide");
        {
            // Ferme tous les canvas lors du changement de box
            CloseAllCanvases();

            // Réinitialise les états des canvas des PNJ après le changement de box
            ResetPNJCanvasStates();
        }
    }

    private void CloseAllCanvases()
    {
        // Récupère tous les canvas de la scène
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();

        // Désactive tous les canvas
        foreach (Canvas canvas in allCanvases)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void ResetPNJCanvasStates()
    {
        // Récupère tous les PNJ de la scène
        PNJCanvasController[] allPNJs = FindObjectsOfType<PNJCanvasController>();

        // Réinitialise les états des canvas liés à chaque PNJ
        foreach (PNJCanvasController pnj in allPNJs)
        {
            // Désactive tous les canvas liés au PNJ
            pnj.DisableAllCanvases();
        }
    }
}