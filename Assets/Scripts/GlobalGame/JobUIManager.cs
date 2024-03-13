using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobUIManager : MonoBehaviour
{
    public CameraRotation cameraRotation;
    public MainSceneManager mainSceneManager;
    public GameObject djBox;
    public Canvas djCanvas;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        
        if (mainSceneManager.startGame == true)
        {
            GameObject clickedObject = gameObject;
            if (clickedObject.name == "DJUIBox")
            {
                // Faites quelque chose avec l'objet cliqué
                Debug.Log("Souris cliquée sur : " + clickedObject.name);
                djBox.SetActive(false);
                djCanvas.gameObject.SetActive(true);
                cameraRotation.canRotate = false;
            }
                
        }
    }

    public void OnRetourCliqued()
    {
        djCanvas.gameObject.SetActive(false);
        cameraRotation.canRotate = true;
        djBox.SetActive(true);
        
    }
}
