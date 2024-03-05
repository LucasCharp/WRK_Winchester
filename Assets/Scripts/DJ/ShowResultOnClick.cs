using UnityEngine;

public class ShowResultOnClick : MonoBehaviour
{
    public GameObject resultCanvas; // R�f�rence au canvas contenant le texte r�sultat

    void Start()
    {
        // Assurez-vous que le canvas est d�sactiv� au d�marrage du jeu
        resultCanvas.SetActive(false);
    }

    private void OnMouseDown()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        // Affiche le canvas avec le r�sultat
        resultCanvas.SetActive(true);
        if (resultCanvas != null) {
            foreach (Canvas canvas in canvases)
            {
                canvas.gameObject.SetActive(false);
            }
        }

     }
}