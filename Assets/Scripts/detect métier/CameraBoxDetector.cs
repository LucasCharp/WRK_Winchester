using UnityEngine;

public class CameraBoxDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si la cam�ra entre en collision avec une box
        if (other.gameObject.layer == LayerMask.NameToLayer("Box"))
            Debug.Log("Lacam�racollide");
        {
            // Ferme tous les canvas lors du changement de box
            CloseAllCanvases();

            // R�initialise les �tats des canvas des PNJ apr�s le changement de box
            ResetPNJCanvasStates();
        }
    }

    private void CloseAllCanvases()
    {
        // R�cup�re tous les canvas de la sc�ne
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();

        // D�sactive tous les canvas
        foreach (Canvas canvas in allCanvases)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void ResetPNJCanvasStates()
    {
        // R�cup�re tous les PNJ de la sc�ne
        PNJCanvasController[] allPNJs = FindObjectsOfType<PNJCanvasController>();

        // R�initialise les �tats des canvas li�s � chaque PNJ
        foreach (PNJCanvasController pnj in allPNJs)
        {
            // D�sactive tous les canvas li�s au PNJ
            pnj.DisableAllCanvases();
        }
    }
}