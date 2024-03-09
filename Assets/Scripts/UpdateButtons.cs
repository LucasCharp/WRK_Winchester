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

    public Button buttonFloor;
    public Button buttonFloorRed;
    public Button buttonFloorBrown;
    public Button buttonFloorWhite;
    public Button buttonFloorRetour;

    public Color redColor;
    public Color brownColor;
    public Color whiteColor;
    public GameObject floor;

    public Transform targetBar;
    public Camera mainCamera;
    public MainSceneManager mainSceneManager;
    public CameraRotation cameraRotation;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private bool goToBar;
    private bool floorCliqued;

    private float moveSpeed = 5f;
    private float rotationSpeed = 5f;
 



    // Start is called before the first frame update
    void Start()
    {
        buttonRefill.gameObject.SetActive(false);
        buttonRetourBar.gameObject.SetActive(false);

        buttonFloorRed.gameObject.SetActive(false);
        buttonFloorBrown.gameObject.SetActive(false);
        buttonFloorWhite.gameObject.SetActive(false);
        buttonFloorRetour.gameObject.SetActive(false);
    }



    public void OnBarCliqued()
    {
        buttonFloor.gameObject.SetActive(false);

        initialCameraPosition = mainCamera.transform.position; // récupère les valeurs de la caméra avant de la faire bouger pour la remettre en place à la fin
        initialCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(MoveAndRotateCamera(targetBar.position, targetBar.rotation));
        goToBar = true;
        cameraRotation.canRotate = false;
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
        if (Vector3.Equals(targetPosition, initialCameraPosition))
        {
            cameraRotation.canRotate = true; // rétablit le fait de pouvoir touner la caméra
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
                buttonFloor.gameObject.SetActive(true);
                goToBar = false;
            }
           
        }
        
    }


    public void OnRetourCLiqued()
    {
        if (floorCliqued == true)
        {
            cameraRotation.canRotate = true;
            buttonFloor.gameObject.SetActive(true);
            buttonFloorRed.gameObject.SetActive(false);
            buttonFloorBrown.gameObject.SetActive(false);
            buttonFloorWhite.gameObject.SetActive(false);
            buttonFloorRetour.gameObject.SetActive(false);
            buttonBar.gameObject.SetActive(true);
            floorCliqued = false;
        }
        else StartCoroutine(MoveAndRotateCamera(initialCameraPosition, initialCameraRotation));
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
        floorCliqued = true;
        cameraRotation.canRotate = false;
        buttonFloor.gameObject.SetActive(false);
        buttonFloorRed.gameObject.SetActive(true);
        buttonFloorBrown.gameObject.SetActive(true);
        buttonFloorWhite.gameObject.SetActive(true);
        buttonFloorRetour.gameObject.SetActive(true);
        buttonBar.gameObject.SetActive(false);
    }

    public void OnRedFloorCliqued()
    {
        Renderer renderer = floor.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Changez la couleur de tous les matériaux du Renderer
            foreach (Material material in renderer.materials)
            {
                material.color = redColor;
            }
        }
    }

    public void OnBrownFloorCliqued()
    {
        Renderer renderer = floor.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Changez la couleur de tous les matériaux du Renderer
            foreach (Material material in renderer.materials)
            {
                material.color = brownColor;
            }
        }
    }

    public void OnWhiteFloorCliqued()
    {
        Renderer renderer = floor.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Changez la couleur de tous les matériaux du Renderer
            foreach (Material material in renderer.materials)
            {
                material.color = whiteColor;
            }
        }
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
