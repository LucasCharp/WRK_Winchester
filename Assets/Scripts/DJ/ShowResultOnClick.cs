using UnityEngine;

public class ShowResultOnClick : MonoBehaviour
{
    public GameObject resultCanvas; // Référence au canvas contenant le texte résultat

    void Start()
    {
        // Assurez-vous que le canvas est désactivé au démarrage du jeu
        resultCanvas.SetActive(false);
    }

    private void OnMouseDown()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        // Affiche le canvas avec le résultat
        resultCanvas.SetActive(true);
        if (resultCanvas != null) {
            foreach (Canvas canvas in canvases)
            {
                canvas.gameObject.SetActive(false);
            }
        }

     }
}