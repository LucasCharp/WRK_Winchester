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

    public Button buttonWall;
    public Button buttonWallRed;
    public Button buttonWallBrown;
    public Button buttonWallWhite;
    public Button buttonWallRetour;

    public Button buttonTable;
    public Button buttonTableRed;
    public Button buttonTableYellow;
    public Button buttonTableBlue;
    public Button buttonTableGreen;
    public Button buttonTableRetour;

    public Color redColor;
    public Color brownColor;
    public Color whiteColor;
    public GameObject floor;
    public GameObject table;
    public List<GameObject> walls;

    public Transform targetBar;
    public Transform targetTable;
    public Camera mainCamera;
    public MainSceneManager mainSceneManager;
    public CameraRotation cameraRotation;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private bool goToBar;
    private bool floorCliqued;
    private bool wallCliqued;

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

        buttonWallRed.gameObject.SetActive(false);
        buttonWallBrown.gameObject.SetActive(false);
        buttonWallWhite.gameObject.SetActive(false);
        buttonWallRetour.gameObject.SetActive(false);

        buttonTableRed.gameObject.SetActive(false);
        buttonTableYellow.gameObject.SetActive(false);
        buttonTableGreen.gameObject.SetActive(false);
        buttonTableBlue.gameObject.SetActive(false);
        buttonTableRetour.gameObject.SetActive(false);
    }



    public void OnBarCliqued()
    {
        buttonFloor.gameObject.SetActive(false);

        initialCameraPosition = mainCamera.transform.position; // r�cup�re les valeurs de la cam�ra avant de la faire bouger pour la remettre en place � la fin
        initialCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(MoveAndRotateCamera(targetBar.position, targetBar.rotation));
        goToBar = true;
        cameraRotation.canRotate = false;
    }
    IEnumerator MoveAndRotateCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        // Tant que la distance entre la cam�ra et la position cible est sup�rieure � une petite marge
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            // D�place la cam�ra progressivement vers la position cible
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Oriente la cam�ra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }
        if (Vector3.Equals(targetPosition, initialCameraPosition))
        {
            cameraRotation.canRotate = true; // r�tablit le fait de pouvoir touner la cam�ra
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
            buttonFloorRed.gameObject.SetActive(false);
            buttonFloorBrown.gameObject.SetActive(false);
            buttonFloorWhite.gameObject.SetActive(false);
            buttonFloorRetour.gameObject.SetActive(false);

            buttonBar.gameObject.SetActive(true);
            buttonWall.gameObject.SetActive(true);
            buttonFloor.gameObject.SetActive(true);
            floorCliqued = false;
        }
        else if (wallCliqued == true)
        {
            buttonBar.gameObject.SetActive(true);
            buttonWall.gameObject.SetActive(true);
            buttonFloor.gameObject.SetActive(true);

            buttonWallRed.gameObject.SetActive(false);
            buttonWallBrown.gameObject.SetActive(false);
            buttonWallWhite.gameObject.SetActive(false);
            buttonWallRetour.gameObject.SetActive(false);
            wallCliqued = false;
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
        buttonWall.gameObject.SetActive(false);
    }

    public void OnRedFloorCliqued()
    {
        Renderer renderer = floor.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Changez la couleur de tous les mat�riaux du Renderer
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
            // Changez la couleur de tous les mat�riaux du Renderer
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
            // Changez la couleur de tous les mat�riaux du Renderer
            foreach (Material material in renderer.materials)
            {
                material.color = whiteColor;
            }
        }
    }




    public void OnWallCliqued()
    {
        wallCliqued = true;
        buttonBar.gameObject.SetActive(false);
        buttonFloor.gameObject.SetActive(false);
        buttonWall.gameObject.SetActive(false);

        buttonWallRed.gameObject.SetActive(true);
        buttonWallBrown.gameObject.SetActive(true);
        buttonWallWhite.gameObject.SetActive(true);
        buttonWallRetour.gameObject.SetActive(true);
    }

    public void OnRedWallCliqued()
    {
        foreach (GameObject wall in walls)
        {
            Renderer renderer = wall.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Changez la couleur de tous les mat�riaux du Renderer
                foreach (Material material in renderer.materials)
                {
                    material.color = redColor;
                }
            }
        }
    }

    public void OnBrownWallCliqued()
    {
        foreach (GameObject wall in walls)
        {
            Renderer renderer = wall.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Changez la couleur de tous les mat�riaux du Renderer
                foreach (Material material in renderer.materials)
                {
                    material.color = brownColor;
                }
            }
        }
    }

    public void OnWhiteWallCliqued()
    {
        foreach (GameObject wall in walls)
        {
            Renderer renderer = wall.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Changez la couleur de tous les mat�riaux du Renderer
                foreach (Material material in renderer.materials)
                {
                    material.color = whiteColor;
                }
            }
        }
    }






    public void OnTableCliqued()
    {
        initialCameraPosition = mainCamera.transform.position; // r�cup�re les valeurs de la cam�ra avant de la faire bouger pour la remettre en place � la fin
        initialCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(MoveAndRotateCamera(targetTable.position, targetTable.rotation)); buttonTableRed.gameObject.SetActive(false);
        buttonTable.gameObject.SetActive(false);
        buttonTableRed.gameObject.SetActive(true);
        buttonTableYellow.gameObject.SetActive(true);
        buttonTableGreen.gameObject.SetActive(true);
        buttonTableBlue.gameObject.SetActive(true);
        buttonTableRetour.gameObject.SetActive(true);
    }

    public void OnBanquetteCliqued()
    {
        Debug.Log("Je clique");
    }

    public void OnBarstoolCliqued()
    {
        Debug.Log("Je clique");
    }
}
