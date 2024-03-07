using UnityEngine;

public class PNJCanvasController : MonoBehaviour
{
    public GameObject[] canvases; // Liste des canvas liés au PNJ
    public GameObject[] pnjCanvases; // Ajoutez cette définition pour pnjCanvases

    private void OnMouseDown()
    {
        // Active ou désactive les canvas liés au PNJ lorsqu'il est cliqué
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }
}