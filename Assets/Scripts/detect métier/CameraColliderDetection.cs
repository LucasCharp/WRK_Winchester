using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraColliderDetection : MonoBehaviour
{
    public int boxNumber = 0;
    public Button barButton;
    public Button wallButton;
    public Button upgradeButton;
    public Button pauseButton;
    public Renderer[] barRenderer;
    public Renderer[] djRenderer;
    public Renderer[] videurRenderer;
    public Renderer[] toilettesRenderer;
    public string visibleUI;
    public MainSceneManager mainSceneManager;
    public CameraRotation cameraRotation;
    public Camera mainCamera;
    public GameObject djBox;
    public GameObject barBox;
    public GameObject videurBox;

    public Transform barUpgradeTarget;
    public Transform djUpgradeTarget;
    public Transform videurUpgradeTarget;

    public Canvas barUpgradeCanvas;
    public Canvas djUpgradeCanvas;
    public Canvas videurUpgradeCanvas;

    private Canvas canvasToShow;
    private Transform target;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private int moveSpeed = 5;
    private int rotationSpeed = 5;
    private GameObject boxToHide;
 

    private void Start()
    {
        //djBox.SetActive(false);
        //barBox.SetActive(false);
        //videurBox.SetActive(false);
    }


    public void OnUpgradeCliqued()
    {
        initialCameraPosition = mainCamera.transform.position;
        initialCameraRotation = mainCamera.transform.rotation;
        cameraRotation.canRotate = false; 

        StartCoroutine(MoveCamera(target.position, target.rotation));
        upgradeButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        boxToHide.gameObject.SetActive(false);
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

        canvasToShow.gameObject.SetActive(true);

    }

    public void OnRetourCliqued()
    {
        Debug.Log("Je clique sur le bouton retour");
        canvasToShow.gameObject.SetActive(false);
        StartCoroutine(MoveCameraBack(initialCameraPosition, initialCameraRotation));
        boxToHide.gameObject.SetActive(true);
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
        upgradeButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Box11")) // Videur
        {
            canvasToShow = videurUpgradeCanvas.GetComponent<Canvas>();
            target = videurUpgradeTarget;
            boxToHide = videurBox;

            if (mainSceneManager.startGame == true)
            {

                visibleUI = "Videur";
                videurBox.SetActive(true);
                djBox.SetActive(true);
                
            }
            Debug.Log("La caméra est entrée dans la Box 1");

            if (videurRenderer != null)
            {
                foreach (Renderer rend in videurRenderer)
                {
                    rend.enabled = false; // Rendre invisible
                }
            }

            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = true;// Inverser la visibilité
                }
            }
            if (toilettesRenderer != null)
            {
                foreach (Renderer rend in toilettesRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box12")) // DJ
        {
            canvasToShow = djUpgradeCanvas.GetComponent<Canvas>();
            target = djUpgradeTarget;
            boxToHide = djBox;

            if (mainSceneManager.startGame == true)
            {
                barBox.SetActive(true);
                djBox.SetActive(true);
                videurBox.SetActive(true);
            }
           
            Debug.Log("La caméra est entrée dans la Box 2");

            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = false; // Rendre invisible
                }
            }

            if (videurRenderer != null)
            {
                foreach (Renderer rend in videurRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
            if (barRenderer != null)
            {
                foreach (Renderer rend in barRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box13")) // Bar
        {
            canvasToShow = barUpgradeCanvas.GetComponent<Canvas>();
            target = barUpgradeTarget;
            boxToHide = barBox;

            if (mainSceneManager.startGame == true)
            {
                visibleUI = "Bar";
                barBox.SetActive(true);
                djBox.SetActive(true);
            }
           

            if (barRenderer != null)
            {
                foreach (Renderer rend in barRenderer)
                {
                    rend.enabled = false; // Rendre invisible
                }
            }

            if (toilettesRenderer != null)
            {
                foreach (Renderer rend in toilettesRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
            
            Debug.Log("La caméra est entrée dans la Box 3");
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box14")) // Toilettes
        {
            if (mainSceneManager.startGame == true)
            {
                //Canvas desiredCanvas = canvasUpgradeManager.canvasList[3];
                visibleUI = "Videur";
                barBox.SetActive(true);
                videurBox.SetActive(true);
            }

            Debug.Log("La caméra est entrée dans la Box 4");
            if (videurRenderer != null)
            {
                if (toilettesRenderer != null)
                {
                    foreach (Renderer rend in toilettesRenderer)
                    {
                        rend.enabled = false; // Rendre invisible
                    }
                }

                if (videurRenderer != null)
                {
                    foreach (Renderer rend in videurRenderer)
                    {
                        rend.enabled = true; // Inverser la visibilité
                    }
                }
                if (barRenderer != null)
                {
                    foreach (Renderer rend in barRenderer)
                    {
                        rend.enabled = true; // Inverser la visibilité
                    }
                }
            }
        }
    }
}
