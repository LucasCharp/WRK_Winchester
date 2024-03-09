using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour // NE PAS OUBLIER DE DISABLE LES BOUTONS QUAND LA CAMERA TOUUUURNE (Seb proof)
{
    public Button buttonRefill;
    public Button buttonBar;
    public Button buttonRetourBar;
    public Transform targetBar;
    public Camera mainCamera;
    public MainSceneManager mainSceneManager;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private bool goToBar;

    private float moveSpeed = 5f;
    private float rotationSpeed = 5f;
 



    // Start is called before the first frame update
    void Start()
    {
        buttonRefill.gameObject.SetActive(false);
        buttonRetourBar.gameObject.SetActive(false);
    }



    public void OnBarCliqued()
    {
        initialCameraPosition = mainCamera.transform.position; // récupère les valeurs de la caméra avant de la faire bouger pour la remettre en place à la fin
        initialCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(MoveAndRotateCamera(targetBar.position, targetBar.rotation));
        goToBar = true;
    }
    IEnumerator MoveAndRotateCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        // Tant que la distance entre la caméra et la position cible est supérieure à une petite marge
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            // Déplace la caméra progressivement vers la position cible
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Oriente la caméra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }
        if (goToBar == true)
        {
            if (buttonBar.gameObject.activeSelf)
            {
                buttonBar.gameObject.SetActive(false);
                buttonRefill.gameObject.SetActive(true);
                buttonRetourBar.gameObject.SetActive(true);
            }
            else if (!buttonBar.gameObject.activeSelf)
            {
                buttonBar.gameObject.SetActive(true);
                goToBar = false;
            }
           
        }
        
    }


    public void OnRetourCLiqued()
    {
        StartCoroutine(MoveAndRotateCamera(initialCameraPosition, initialCameraRotation));
        if (goToBar == true)
        {
            buttonRefill.gameObject.SetActive(false);
            buttonRetourBar.gameObject.SetActive(false);
        }
    }

    public void OnRefillCliqued()
    {
        mainSceneManager.SetEtagere();
    }

    public void OnFloorCliqued()
    {
        Debug.Log("Je clique");
    }

    public void OnTableCliqued()
    {
        Debug.Log("Je clique");
    }

    public void OnBanquetteCliqued()
    {
        Debug.Log("Je clique");
    }

    public void OnBarstoolCliqued()
    {
        Debug.Log("Je clique");
    }

    public void OnWallCliqued()
    {
        Debug.Log("Je clique");
    }
}
