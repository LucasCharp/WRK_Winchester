using UnityEngine;


public class VideurUIManager : MonoBehaviour
{
    public CameraRotation cameraRotation;
    public MainSceneManager mainSceneManager;
    public GameObject videurBox;
    public Canvas videurCanvas;
    private bool hasCliqued;

    private void OnMouseDown()
    {
        if (hasCliqued == false)
        {
            videurCanvas.gameObject.SetActive(true);
            cameraRotation.canRotate = false;
            hasCliqued = true;
        }
        else
        {
            videurCanvas.gameObject.SetActive(false);
            cameraRotation.canRotate = true;
            hasCliqued = false;
        }
    }
}

