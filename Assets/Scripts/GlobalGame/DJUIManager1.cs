using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJUIManager1 : MonoBehaviour
{
    public CameraRotation cameraRotation;
    public MainSceneManager mainSceneManager;
    public GameObject djBox;

    public Canvas djCanvas;


    // Start is called before the first frame update
    private void OnMouseDown()
    {
        djBox.SetActive(false);
        djCanvas.gameObject.SetActive(true);
        cameraRotation.canRotate = false;
    }

    public void OnRetourCliqued()
    {
        cameraRotation.canRotate = true;
        djCanvas.gameObject.SetActive(false);
        djBox.SetActive(true);
    }
}

