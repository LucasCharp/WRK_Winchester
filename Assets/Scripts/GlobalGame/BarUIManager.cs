using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BarUIManager : MonoBehaviour
{
    public CameraRotation cameraRotation;
    public MainSceneManager mainSceneManager;
    public Camera mainCamera;
    public GameObject barBox;
    public Canvas barCanvas;
    public Transform barUpgradeTarget;
    public Collider barBoxCollider;

    private int moveSpeed = 5;
    private int rotationSpeed = 5;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private bool doOnce = false;

    private void Start()
    {
        
    }
    private void OnMouseDown()
    {
        if (doOnce == false)
        {
            initialCameraPosition = mainCamera.transform.position;
            initialCameraRotation = mainCamera.transform.rotation;
            doOnce = true;
        }
        Debug.Log("J'ai cliqué sur le bar");
        StartCoroutine(MoveCamera(barUpgradeTarget.position, barUpgradeTarget.rotation));
        cameraRotation.canRotate = false;
    }

    IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            // Déplace la caméra progressivement vers la position cible
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Oriente la caméra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
        barCanvas.gameObject.SetActive(true);
        barBoxCollider.enabled = false;
    }


    public void OnRetourCliqued()
    {
        barCanvas.gameObject.SetActive(false);
        StartCoroutine(MoveCameraBack(initialCameraPosition, initialCameraRotation));
    }


    IEnumerator MoveCameraBack(Vector3 targetPosition, Quaternion targetRotation)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            // Déplace la caméra progressivement vers la position cible
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Oriente la caméra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
        cameraRotation.canRotate = true;
        barBoxCollider.enabled = true;
    }
}

