using UnityEngine;

public class PNJCanvasController : MonoBehaviour
{
    public GameObject[] canvases; // Liste des canvas li�s au PNJ
    public GameObject[] pnjCanvases; // Ajoutez cette d�finition pour pnjCanvases

    private void OnMouseDown()
    {
        // Active ou d�sactive les canvas li�s au PNJ lorsqu'il est cliqu�
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }
}