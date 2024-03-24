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
    public TextMeshProUGUI costBar;

    public Button buttonFloor;
    public Button buttonFloorRed;
    public Button buttonFloorBrown;
    public Button buttonFloorWhite;
    public Button buttonFloorRetour;
    public TextMeshProUGUI costFloor;

    public Button buttonWall;
    public Button buttonWallRed;
    public Button buttonWallBrown;
    public Button buttonWallWhite;
    public Button buttonWallRetour;
    public TextMeshProUGUI costWall;

    public Button buttonTable;
    public Button buttonTableRed;
    public Button buttonTableYellow;
    public Button buttonTableBlue;
    public Button buttonTableGreen;
    public Button buttonTableRetour;
    public TextMeshProUGUI costTable;

    public Button buttonBanquette;
    public Button buttonBanqRed;
    public Button buttonBanqYellow;
    public Button buttonBanqBlue;
    public Button buttonBanqGreen;
    public Button buttonBanqRetour;
    public TextMeshProUGUI costBanq;

    public Button buttonChaise;
    public Button buttonChaiseRed;
    public Button buttonChaiseYellow;
    public Button buttonChaiseBlue;
    public Button buttonChaiseGreen;
    public Button buttonChaiseRetour;
    public TextMeshProUGUI costChaise;

    public Button startButton;

    public Color redColor;
    public Color brownColor;
    public Color whiteColor;
    public GameObject floor;
    public List<GameObject> tables;
    public List<GameObject> walls;
    public List<GameObject> banquettes;
    public List<GameObject> chaises;

    public Material redMat;
    public Material yellowMat;
    public Material blueMat;
    public Material greenMat;

    public Transform targetBar;
    public Transform targetTable;
    public Transform targetChaise;
    public Transform targetBanquette;
    public Camera mainCamera;
    public MainSceneManager mainSceneManager;
    public CameraRotation cameraRotation;
    public CameraColliderDetection cameraCollider;
    public Canvas upgradeCanvas;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private bool goToBar = false;
    private bool floorCliqued = false;
    private bool wallCliqued = false;
    private bool tableCliqued = false;
    private bool banqCliqued = false;
    private bool chaiseCliqued = false;

    private float moveSpeed = 5f;
    private float rotationSpeed = 5f;
    private int moneyCost = 100;
    private int refillCost = 50;

    public AudioClip[] son;
    public List<string> purchasedButtons = new List<string>();
    public GameManager gameManager;

    private void Update()
    {
        if (mainSceneManager.startGame == true)
        {
            Destroy(upgradeCanvas.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonRefill.gameObject.SetActive(false);
        buttonRetourBar.gameObject.SetActive(false);
        costBar.gameObject.SetActive(false);

        buttonFloorRed.gameObject.SetActive(false);
        buttonFloorBrown.gameObject.SetActive(false);
        buttonFloorWhite.gameObject.SetActive(false);
        buttonFloorRetour.gameObject.SetActive(false);
        costFloor.gameObject.SetActive(false);

        buttonWallRed.gameObject.SetActive(false);
        buttonWallBrown.gameObject.SetActive(false);
        buttonWallWhite.gameObject.SetActive(false);
        buttonWallRetour.gameObject.SetActive(false);
        costWall.gameObject.SetActive(false);

        buttonTableRed.gameObject.SetActive(false);
        buttonTableYellow.gameObject.SetActive(false);
        buttonTableGreen.gameObject.SetActive(false);
        buttonTableBlue.gameObject.SetActive(false);
        buttonTableRetour.gameObject.SetActive(false);
        costTable.gameObject.SetActive(false);

        buttonBanqRed.gameObject.SetActive(false);
        buttonBanqYellow.gameObject.SetActive(false);
        buttonBanqGreen.gameObject.SetActive(false);
        buttonBanqBlue.gameObject.SetActive(false);
        buttonBanqRetour.gameObject.SetActive(false);
        costBanq.gameObject.SetActive(false);

        buttonChaiseRed.gameObject.SetActive(false);
        buttonChaiseYellow.gameObject.SetActive(false);
        buttonChaiseGreen.gameObject.SetActive(false);
        buttonChaiseBlue.gameObject.SetActive(false);
        buttonChaiseRetour.gameObject.SetActive(false);
        costChaise.gameObject.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager instance not found.");
        }
    }

    public void YourMethod()
    {
        if (gameManager != null)
        {
            gameManager.AddMultiplicata();
        }
        else
        {
            Debug.LogWarning("GameManager instance is null.");
        }
    }
    public void YourMethod2()
    {
        if (gameManager != null)
        {
            gameManager.AddMultiplicato();
        }
    }

    public void OnBarCliqued()
    {
        costBar.text = "£" + refillCost.ToString();
        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);

        startButton.gameObject.SetActive(false);
        buttonBar.interactable = false;
        buttonWall.gameObject.SetActive(false);
        buttonTable.gameObject.SetActive(false);
        buttonFloor.gameObject.SetActive(false);
        buttonBanquette.gameObject.SetActive(false);
        buttonChaise.gameObject.SetActive(false);

        initialCameraPosition = mainCamera.transform.position; // récupère les valeurs de la caméra avant de la faire bouger pour la remettre en place à la fin
        initialCameraRotation = mainCamera.transform.rotation;

        foreach (Renderer rend in cameraCollider.barRenderer)
        {
            rend.enabled = true; 
        }

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
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;

        // Assurez-vous que la position et la rotation finales soient exactes
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;

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
                costBar.gameObject.SetActive(true);
            }
            else if (!buttonBar.gameObject.activeSelf)
            {
                Debug.Log("Coucou je suis le bar, et je remet les boutons en visible");
                buttonBar.gameObject.SetActive(true);
                buttonFloor.gameObject.SetActive(true);
                buttonTable.gameObject.SetActive(true);
                buttonBanquette.gameObject.SetActive(true);
                buttonChaise.gameObject.SetActive(true);
                 buttonWall.gameObject.SetActive(true);
                
                //if (cameraCollider.hideWall == false)
                //{
                    //buttonWall.gameObject.SetActive(true);
                //}
                goToBar = false;
                startButton.gameObject.SetActive(true);
                buttonBar.interactable = true;
                cameraRotation.canRotate = true;
            }

        }
        if (tableCliqued == true)
        {
            if (buttonTable.gameObject.activeSelf)
            {
                buttonTable.gameObject.SetActive(false);
                buttonBar.gameObject.SetActive(false);
                buttonWall.gameObject.SetActive(false);
                buttonFloor.gameObject.SetActive(false);
                buttonChaise.gameObject.SetActive(false);

                buttonTable.gameObject.SetActive(false);
                buttonTableRed.gameObject.SetActive(true);
                buttonTableYellow.gameObject.SetActive(true);
                buttonTableGreen.gameObject.SetActive(true);
                buttonTableBlue.gameObject.SetActive(true);
                buttonTableRetour.gameObject.SetActive(true);
                costTable.gameObject.SetActive(true);

            }
            else if (!buttonTable.gameObject.activeSelf)
            {
                buttonBanquette.gameObject.SetActive(true);
               
                buttonFloor.gameObject.SetActive(true);
                buttonChaise.gameObject.SetActive(true);
                buttonTable.gameObject.SetActive(true);
                tableCliqued = false;
                startButton.gameObject.SetActive(true);
                buttonTable.interactable = true;
                cameraRotation.canRotate = true;
                buttonBar.gameObject.SetActive(true);
                buttonWall.gameObject.SetActive(true);
            }
        }
        if (banqCliqued == true)
        {
            if (buttonBanquette.gameObject.activeSelf)
            {
                buttonBanqRed.gameObject.SetActive(true);
                buttonBanqGreen.gameObject.SetActive(true);
                buttonBanqYellow.gameObject.SetActive(true);
                buttonBanqBlue.gameObject.SetActive(true);
                buttonBanqRetour.gameObject.SetActive(true);
                costBanq.gameObject.SetActive(true);
                buttonBanquette.gameObject.SetActive(false);
            }
             else if (!buttonBanquette.gameObject.activeSelf)
            {
                buttonBanquette.gameObject.SetActive(true);
                buttonFloor.gameObject.SetActive(true);
                buttonTable.gameObject.SetActive(true);
                buttonChaise.gameObject.SetActive(true);
                startButton.gameObject.SetActive(true);
                banqCliqued = false;
                buttonBanquette.interactable = true;
                cameraRotation.canRotate = true;
                buttonBar.gameObject.SetActive(true);
                buttonWall.gameObject.SetActive(true);
            }
        }
        if (chaiseCliqued == true)
        {
            if (buttonChaise.gameObject.activeSelf)
            {
                buttonChaiseRed.gameObject.SetActive(true);
                buttonChaiseGreen.gameObject.SetActive(true);
                buttonChaiseYellow.gameObject.SetActive(true);
                buttonChaiseBlue.gameObject.SetActive(true);
                buttonChaiseRetour.gameObject.SetActive(true);
                costChaise.gameObject.SetActive(true);
                buttonChaise.gameObject.SetActive(false);
            }
            else if (!buttonChaise.gameObject.activeSelf)
            {
                buttonBanquette.gameObject.SetActive(true);
                buttonFloor.gameObject.SetActive(true);
                buttonTable.gameObject.SetActive(true);
                buttonChaise.gameObject.SetActive(true);
                startButton.gameObject.SetActive(true);
                chaiseCliqued = false;
                buttonChaise.interactable = true;
                cameraRotation.canRotate = true;
                buttonBar.gameObject.SetActive(true);
                buttonWall.gameObject.SetActive(true);
            }
        }
        
    }


    public void OnRetourCLiqued()
    {
        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);

        
        if (floorCliqued == true)
        {
            cameraRotation.canRotate = true;
            buttonFloorRed.gameObject.SetActive(false);
            buttonFloorBrown.gameObject.SetActive(false);
            buttonFloorWhite.gameObject.SetActive(false);
            buttonFloorRetour.gameObject.SetActive(false);
            costFloor.gameObject.SetActive(false);
            buttonBar.gameObject.SetActive(true);
            buttonWall.gameObject.SetActive(true);
            buttonTable.gameObject.SetActive(true);
            buttonBanquette.gameObject.SetActive(true);
            buttonFloor.gameObject.SetActive(true);
            buttonChaise.gameObject.SetActive(true);
            floorCliqued = false;
            
        }
        else if (wallCliqued == true)
        {
            cameraRotation.canRotate = true;
            buttonFloor.gameObject.SetActive(true);
            buttonTable.gameObject.SetActive(true);
            buttonChaise.gameObject.SetActive(true);
            buttonBanquette.gameObject.SetActive(true);
            buttonWall.gameObject.SetActive(true);
            buttonBar.gameObject.SetActive(true);

            buttonWallRed.gameObject.SetActive(false);
            buttonWallBrown.gameObject.SetActive(false);
            buttonWallWhite.gameObject.SetActive(false);
            buttonWallRetour.gameObject.SetActive(false);
            costWall.gameObject.SetActive(false);
            wallCliqued = false;
            
        }

        else StartCoroutine(MoveAndRotateCamera(initialCameraPosition, initialCameraRotation));

        if (goToBar == true)
        {

            buttonRefill.gameObject.SetActive(false);
            buttonRetourBar.gameObject.SetActive(false);
            costBar.gameObject.SetActive(false);
        }
        if (banqCliqued == true)
        {
            buttonBanqRed.gameObject.SetActive(false);
            buttonBanqGreen.gameObject.SetActive(false);
            buttonBanqYellow.gameObject.SetActive(false);
            buttonBanqBlue.gameObject.SetActive(false);
            buttonBanqRetour.gameObject.SetActive(false);
            costBanq.gameObject.SetActive(false);
        }
        if (wallCliqued == true)
        {
            wallCliqued = false;
            buttonBar.gameObject.SetActive(false);
            buttonFloor.gameObject.SetActive(false);
            buttonWall.gameObject.SetActive(false);

            buttonWallRed.gameObject.SetActive(true);
            buttonWallBrown.gameObject.SetActive(true);
            buttonWallWhite.gameObject.SetActive(true);
            buttonWallRetour.gameObject.SetActive(true);
            buttonChaise.gameObject.SetActive(true);

        }
        if (tableCliqued)
        {
            buttonTableRed.gameObject.SetActive(false);
            buttonTableYellow.gameObject.SetActive(false);
            buttonTableGreen.gameObject.SetActive(false);
            buttonTableBlue.gameObject.SetActive(false);
            buttonTableRetour.gameObject.SetActive(false);
            costTable.gameObject.SetActive(false);

        }
        if (chaiseCliqued)
        {
            buttonChaiseRed.gameObject.SetActive(false);
            buttonChaiseGreen.gameObject.SetActive(false);
            buttonChaiseYellow.gameObject.SetActive(false);
            buttonChaiseBlue.gameObject.SetActive(false);
            buttonChaiseRetour.gameObject.SetActive(false);
            costChaise.gameObject.SetActive(false);
        }
    }

    public void OnRefillCliqued()
    {
        if (GameManager.money >= refillCost && mainSceneManager.barLevel != 3)
        {
            SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);
            SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
            gameManager.AugmenterArgent(-refillCost);
            mainSceneManager.SetEtagere();
        }
        else
        {
            SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            costBar.gameObject.SetActive(false);
        }
    }

    public void OnFloorCliqued()
    {
        costFloor.text = "£" + moneyCost.ToString();
        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);

        floorCliqued = true;
        cameraRotation.canRotate = false;
        buttonFloor.gameObject.SetActive(false);
        buttonFloorRed.gameObject.SetActive(true);
        buttonFloorBrown.gameObject.SetActive(true);
        buttonFloorWhite.gameObject.SetActive(true);
        buttonFloorRetour.gameObject.SetActive(true);
        costFloor.gameObject.SetActive(true);

        buttonTable.gameObject.SetActive(false);
        buttonBar.gameObject.SetActive(false);
        buttonWall.gameObject.SetActive(false);
        buttonBanquette.gameObject.SetActive(false);
        buttonChaise.gameObject.SetActive(false);
        
    }

    public void OnRedFloorCliqued()
    {
        if (!purchasedButtons.Contains("RedFloor"))
        {
            if (GameManager.money >= moneyCost)
            {
                gameManager.AugmenterArgent(-moneyCost);

                purchasedButtons.Add("RedFloor");
                SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
            }
            else
            {
                SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            }
        }
        
        if (purchasedButtons.Contains("RedFloor"))
        {
            gameManager.AddMultiplicato();
            Renderer renderer = floor.GetComponent<Renderer>();
          if (renderer != null)
          {
             SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
             // Changez la couleur de tous les matériaux du Renderer
             foreach (Material material in renderer.materials)
             {
                material.color = redColor;
             }
          }
        }
    }

    public void OnBrownFloorCliqued()
    {
        if (!purchasedButtons.Contains("BrownFloor"))
        {
            if (GameManager.money >= moneyCost)
            {
                gameManager.AugmenterArgent(-moneyCost);

                purchasedButtons.Add("BrownFloor");
                SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
            }
            else
            {
                SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            }
        }
        if (purchasedButtons.Contains("BrownFloor"))
        {
            gameManager.AddMultiplicato();
            SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);

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
    }

    public void OnWhiteFloorCliqued()
    {
        if (!purchasedButtons.Contains("WhiteFloor"))
        {
            if (GameManager.money >= moneyCost)
            {
                gameManager.AugmenterArgent(-moneyCost);

                purchasedButtons.Add("WhiteFloor");
                SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
            }
            else
            {
                SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            }
        }
        if (purchasedButtons.Contains("WhiteFloor"))
        {
            gameManager.AddMultiplicato();
            SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);

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
    }




    public void OnWallCliqued()
    {
        costWall.text = "£" + moneyCost.ToString();
        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);

        cameraRotation.canRotate = false;
        wallCliqued = true;
        buttonBar.gameObject.SetActive(false);
        buttonFloor.gameObject.SetActive(false);
        buttonWall.gameObject.SetActive(false);
        buttonTable.gameObject.SetActive(false);
        buttonBanquette.gameObject.SetActive(false);

        buttonWallRed.gameObject.SetActive(true);
        buttonWallBrown.gameObject.SetActive(true);
        buttonWallWhite.gameObject.SetActive(true);
        buttonWallRetour.gameObject.SetActive(true);
        costWall.gameObject.SetActive(true);
        buttonChaise.gameObject.SetActive(false);
    }

    public void OnRedWallCliqued()
    {
        if (!purchasedButtons.Contains("RedWall"))
        {
            if (GameManager.money >= moneyCost)
            {
                gameManager.AugmenterArgent(-moneyCost);

                purchasedButtons.Add("RedWall");
                SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
            }
            else
            {
                SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            }
        }
       
        if (purchasedButtons.Contains("RedWall"))
        {
            gameManager.AddMultiplicato();
            SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
            foreach (GameObject wall in walls)
            {
                Renderer renderer = wall.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Changez la couleur de tous les matériaux du Renderer
                    foreach (Material material in renderer.materials)
                    {
                        material.color = redColor;
                    }
                }
            }
        }
    }

    public void OnBrownWallCliqued()
    {
        if (!purchasedButtons.Contains("BrownWall"))
        {
            if (GameManager.money >= moneyCost)
            {
                gameManager.AugmenterArgent(-moneyCost);

                purchasedButtons.Add("BrownWall");
                SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
            }
            else
            {
                SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            }
        }
        
        if (purchasedButtons.Contains("BrownWall"))
        {
            gameManager.AddMultiplicato();
            SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);

            foreach (GameObject wall in walls)
            {
                Renderer renderer = wall.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Changez la couleur de tous les matériaux du Renderer
                    foreach (Material material in renderer.materials)
                    {
                        material.color = brownColor;
                    }
                }
            }
        }
    }

    public void OnWhiteWallCliqued()
    {
        if (!purchasedButtons.Contains("WhiteWall"))
        {
            if (GameManager.money >= moneyCost)
            {
                gameManager.AugmenterArgent(-moneyCost);

                purchasedButtons.Add("WhiteWall");
                SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
            }
            else
            {
                SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            }
        }
        
        if (purchasedButtons.Contains("WhiteWall"))
        {
            gameManager.AddMultiplicato();
            SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);

            foreach (GameObject wall in walls)
            {
                Renderer renderer = wall.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Changez la couleur de tous les matériaux du Renderer
                    foreach (Material material in renderer.materials)
                    {
                        material.color = whiteColor;
                    }
                }
            }
        }
    }






    public void OnTableCliqued()
    {
        costTable.text = "£" + moneyCost.ToString();
        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);

        initialCameraPosition = mainCamera.transform.position; // récupère les valeurs de la caméra avant de la faire bouger pour la remettre en place à la fin
        initialCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(MoveAndRotateCamera(targetTable.position, targetTable.rotation)); buttonTableRed.gameObject.SetActive(false);

        buttonBar.gameObject.SetActive(false);
        buttonWall.gameObject.SetActive(false);
        buttonFloor.gameObject.SetActive(false);
        buttonBanquette.gameObject.SetActive(false);
        buttonChaise.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        cameraRotation.canRotate = false;

        tableCliqued = true;
        buttonTable.interactable = false;
    }
 
    

    public void OnPrefabRed()
    {
        

        if (tableCliqued == true)
        {
            if (!purchasedButtons.Contains("RedTable"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("RedTable");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
            
            if (purchasedButtons.Contains("RedTable"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject table in tables)
                {
                    if (table != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = table.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = redMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }

        }
        else if (banqCliqued == true)
        {
            if (!purchasedButtons.Contains("RedBanq"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("RedBanq");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
            
            if (purchasedButtons.Contains("RedBanq"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject banquette in banquettes)
                {
                    if (banquette != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = banquette.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = redMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
        else if (chaiseCliqued == true)
        {
            if (!purchasedButtons.Contains("RedChaise"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("RedChaise");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
            
            if (purchasedButtons.Contains("RedChaise"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject chaise in chaises)
                {
                    if (chaise != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = chaise.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = redMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
    }

    public void OnPrefabGreen()
    {
        if (tableCliqued == true)
        {
            if (!purchasedButtons.Contains("GreenTable"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("GreenTable");
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
            
            if (purchasedButtons.Contains("GreenTable"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject table in tables)
                {
                    if (table != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = table.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = greenMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
        else if (banqCliqued == true)
        {
            if (!purchasedButtons.Contains("GreenBanq"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("GreenBanq");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
           
            if (purchasedButtons.Contains("GreenBanq"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject banquette in banquettes)
                {
                    if (banquette != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = banquette.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = greenMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
        else if (chaiseCliqued == true)
        {
            if (!purchasedButtons.Contains("GreenChaise"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("GreenChaise");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
            
            if (purchasedButtons.Contains("GreenChaise"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject chaise in chaises)
                {
                    if (chaise != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = chaise.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = greenMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
    }

    public void OnPrefabYellow()
    {
        if (tableCliqued == true)
        {
            if (!purchasedButtons.Contains("YellowTable"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("YellowTable");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
           
            if (purchasedButtons.Contains("YellowTable"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject table in tables)
                {
                    if (table != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = table.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = yellowMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
        else if (banqCliqued == true)
        {
            if (!purchasedButtons.Contains("YellowBanq"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("YellowBanq");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
            
            if (purchasedButtons.Contains("YellowBanq"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject banquette in banquettes)
                {
                    if (banquette != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = banquette.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = yellowMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
        else if (chaiseCliqued == true)
        {
            if (!purchasedButtons.Contains("YellowChaise"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("YellowChaise");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }
            
            if (purchasedButtons.Contains("YellowChaise"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject chaise in chaises)
                {
                    if (chaise != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = chaise.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = yellowMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
    }

    public void OnPrefabBlue()
    {
        if (tableCliqued == true)
        {
            if (!purchasedButtons.Contains("BlueTable"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("BlueTable");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }

            if (purchasedButtons.Contains("BlueTable"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject table in tables)
                {
                    if (table != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = table.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = blueMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
        else if (banqCliqued == true)
        {
            if (!purchasedButtons.Contains("BlueBanq"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("BlueBanq");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }

            if (purchasedButtons.Contains("BlueBanq"))
            {
                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject banquette in banquettes)
                {
                    if (banquette != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = banquette.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = blueMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
        else if (chaiseCliqued == true)
        {
            if (!purchasedButtons.Contains("BlueChaise"))
            {
                if (GameManager.money >= moneyCost)
                {
                    gameManager.AddMultiplicata();
                    gameManager.AugmenterArgent(-moneyCost);

                    purchasedButtons.Add("BlueChaise");
                    SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);
                }
                else
                {
                    SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
                }
            }

            if (purchasedButtons.Contains("BlueChaise"))
            {

                SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
                foreach (GameObject chaise in chaises)
                {
                    if (chaise != null)
                    {
                        // Obtenez tous les renderers de l'objet
                        Renderer[] renderers = chaise.GetComponentsInChildren<Renderer>();

                        // Parcourez tous les renderers et changez leurs matériaux
                        foreach (Renderer renderer in renderers)
                        {
                            // Obtenez tous les matériaux actuels du Renderer
                            Material[] materials = renderer.sharedMaterials;

                            // Changez chaque matériau dans la liste de rendu pour le matériau cible
                            for (int i = 0; i < materials.Length; i++)
                            {
                                materials[i] = blueMat;
                            }

                            // Appliquez les nouveaux matériaux au Renderer
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }
    }

    public void OnBanquetteCliqued()
    {
        costBanq.text = "£" + moneyCost.ToString();
        initialCameraPosition = mainCamera.transform.position; // récupère les valeurs de la caméra avant de la faire bouger pour la remettre en place à la fin
        initialCameraRotation = mainCamera.transform.rotation;

        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);
        banqCliqued = true;
        StartCoroutine(MoveAndRotateCamera(targetBanquette.position, targetBanquette.rotation));

        buttonBar.gameObject.SetActive(false);
        buttonWall.gameObject.SetActive(false);
        buttonFloor.gameObject.SetActive(false);
        buttonTable.gameObject.SetActive(false);
        buttonChaise.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        cameraRotation.canRotate = false;
        buttonBanquette.interactable = false;
    }

    public void OnBarstoolCliqued()
    {
        costChaise.text = "£" + moneyCost.ToString();
        initialCameraPosition = mainCamera.transform.position; // récupère les valeurs de la caméra avant de la faire bouger pour la remettre en place à la fin
        initialCameraRotation = mainCamera.transform.rotation;

        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);
        StartCoroutine(MoveAndRotateCamera(targetChaise.position, targetChaise.rotation));
        cameraRotation.canRotate = false;
        buttonBar.gameObject.SetActive(false);
        buttonWall.gameObject.SetActive(false);
        buttonFloor.gameObject.SetActive(false);
        buttonTable.gameObject.SetActive(false);
        buttonBanquette.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        chaiseCliqued = true;
        buttonChaise.interactable = false;
    }
}
