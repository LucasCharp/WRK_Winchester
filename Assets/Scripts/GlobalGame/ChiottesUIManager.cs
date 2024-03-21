using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiottesUIManager : MonoBehaviour
{
    public CameraRotation cameraRotation;
    public MainSceneManager mainSceneManager;
    public GameObject chiottesBox;
    public Canvas chiottesCanvas;
    public bool hasCliqued = false;


    private void OnMouseDown()
    {
        if (hasCliqued == false)
        {
            chiottesCanvas.gameObject.SetActive(true);
            cameraRotation.canRotate = false;
            hasCliqued = true;
        }
        else
        {
            cameraRotation.canRotate = true;
            chiottesCanvas.gameObject.SetActive(false);
            hasCliqued = false;
        }
    }
}
