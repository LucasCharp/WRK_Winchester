using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarUIManager : MonoBehaviour
{
    public CameraRotation cameraRotation;
    public MainSceneManager mainSceneManager;
    public GameObject barBox;
    public Canvas barCanvas;


    // Start is called before the first frame update
    private void OnMouseDown()
    {
        barBox.SetActive(false);
        barCanvas.gameObject.SetActive(true);
        cameraRotation.canRotate = false;
    }

    public void OnRetourCliqued()
    {
        cameraRotation.canRotate = true;
        barCanvas.gameObject.SetActive(false);
        barBox.SetActive(true);
    }
}

