using UnityEngine;


public class VideurUIManager : MonoBehaviour
{
    public CameraRotation cameraRotation;
    public MainSceneManager mainSceneManager;
    public GameObject videurBox;
    public Canvas videurCanvas;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        videurBox.SetActive(false);
        videurCanvas.gameObject.SetActive(true);
        cameraRotation.canRotate = false;
    }

    public void OnRetourCliqued()
    {
        cameraRotation.canRotate = true;
        videurCanvas.gameObject.SetActive(false);
        videurBox.SetActive(true);
    }
}

