using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject objectDumpster;
    public GameObject objectMenu;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Lid;
    private GameObject objectTouched;

    public Button buttonDumpster;
    public Button buttonMenu;
    public Button playButton;

    public Camera mainCamera;

    public Transform targetDumpster;
    public Transform targetMenu;
    public Transform initialPosition;
    public Transform targetPlay;

    private float moveSpeed = 5f;
    private float moveSpeedBack = 8f;
    private float rotationSpeed = 5f;
    private float playSpeed = 4f;
    private float rotationDuration = 100f;
    private float lidRotDuration = 1f;
    private float rotationAmount = 110f;
    private Quaternion originalRotation; //= Quaternion.Euler(-20f, 0f, 0f);
    private bool isRotating = false;

    public List<AudioClip> son;
    public AudioSource soundPlayer;

    
    

    //public Transform initialCameraPosition;
    private bool isCameraClose = false;

    void Start()
    {
        // Sauvegarde la rotation d'origine du couvercle de la poubelle 
        originalRotation = Lid.transform.localRotation;
        //initialCameraPosition = mainCamera.transform.position;
        buttonMenu.gameObject.SetActive(false);
        buttonDumpster.gameObject.SetActive(false);
        soundPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCameraClose)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == objectDumpster)
                {
                    objectTouched = hit.collider.gameObject;
                    StartCoroutine(MoveAndRotateCamera(targetDumpster.position, targetDumpster.rotation));
                }
                else if (hit.collider.gameObject == objectMenu)
                {
                    objectTouched = hit.collider.gameObject;
                    StartCoroutine(MoveAndRotateCamera(targetMenu.position, targetMenu.rotation));
                    Debug.Log("Je vais vers le menu");
                }
            }
        }
    }




    IEnumerator MoveAndRotateCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        if (objectTouched.CompareTag("Dumpster"))
        {
            StartCoroutine(RotateDumpLid());
            Debug.Log("Je suis là aussi du con");
            playButton.gameObject.SetActive(false);
            Debug.Log("Je touche la poubelle");
            soundPlayer.clip = son[1];
            ClickSound();
            // Tant que la distance entre la caméra et la position cible est supérieure à une petite marge
            while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
            {
                // Déplace la caméra progressivement vers la position cible
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Oriente la caméra progressivement vers la rotation cible
                mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                yield return null; // Attend une frame

                objectDumpster.GetComponent<Collider>().enabled = false;
                objectMenu.GetComponent<Collider>().enabled = false;
            }
            buttonDumpster.gameObject.SetActive(true);// met le bouton en visible après que la caméra soit arrivée, pour pas spam dessus
        }
        if (objectTouched.CompareTag("Menu"))
        {
            playButton.gameObject.SetActive(false);
            Debug.Log("Je touche le menu");
            soundPlayer.clip = son[3];
            ClickSound();
            // Tant que la distance entre la caméra et la position cible est supérieure à une petite marge
            while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
            {
                // Déplace la caméra progressivement vers la position cible
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Oriente la caméra progressivement vers la rotation cible
                mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                yield return null; // Attend une frame

                objectMenu.GetComponent<Collider>().enabled = false;
                objectDumpster.GetComponent<Collider>().enabled = false;
            }
            buttonMenu.gameObject.SetActive(true);// met le bouton en visible après que la caméra soit arrivée, pour pas spam dessus
        }

    }

    IEnumerator RotateDumpLid()
    {
        // Calcul de la rotation cible
        Quaternion targetRotation = Quaternion.Euler(
            Lid.transform.localRotation.eulerAngles.x - 45f, // Rotation sur l'axe X
            Lid.transform.localRotation.eulerAngles.y,      // Conserver la rotation sur l'axe Y
            Lid.transform.localRotation.eulerAngles.z  // Rotation sur l'axe Z
        );

        // Rotation progressive des objets
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            // Interpolation linéaire de la rotation sur la durée spécifiée
            Lid.transform.localRotation = Quaternion.Slerp(
                Lid.transform.localRotation, // Rotation actuelle
                targetRotation,              // Rotation cible
                elapsedTime / rotationDuration
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assure que la rotation finale soit exacte
        Lid.transform.localRotation = targetRotation;
    }







    public void OnButtonDumpster()
    {
        StartCoroutine(MoveCameraBackDump(initialPosition.position, initialPosition.rotation));
        buttonDumpster.gameObject.SetActive(false);
        StartCoroutine(RotateLidBack());
    }
   
    IEnumerator MoveCameraBackDump(Vector3 targetPosition, Quaternion targetRotation)
    {
        soundPlayer.clip = son[2];
        ClickSound();
        
        // Tant que la caméra n'est pas revenu à la position cible
        while (mainCamera.transform.position != targetPosition)
        {
            // Déplace la caméra progressivement vers la position cible
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, moveSpeedBack * Time.deltaTime);

            // Oriente la caméra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }

        // Une fois que la caméra est revenue à la position cible, désactive le bouton retour, réactive le clic sur l'objet
        
        isCameraClose = false;
        objectDumpster.GetComponent<Collider>().enabled = true;
        objectMenu.GetComponent<Collider>().enabled = true;
        playButton.gameObject.SetActive(true);
    }

    IEnumerator RotateLidBack()
{
    if (isRotating) yield break; // Si la coroutine est déjà en cours d'exécution, ne rien faire

    isRotating = true;

    Quaternion lidStartRotation = Lid.transform.rotation;
    Quaternion lidTargetRotation = originalRotation; // Utiliser la rotation d'origine comme rotation cible
    lidTargetRotation.eulerAngles = new Vector3(originalRotation.eulerAngles.x, lidStartRotation.eulerAngles.y, lidStartRotation.eulerAngles.z); // Garder la rotation sur l'axe Y et Z
    float elapsedTime = 0f;

    while (elapsedTime < lidRotDuration)
    {
        elapsedTime += Time.deltaTime;
        float time = Mathf.Clamp01(elapsedTime / lidRotDuration);
        Lid.transform.rotation = Quaternion.Slerp(lidStartRotation, lidTargetRotation, time);
        yield return null;
        Debug.Log("Je bouge");
    }
    Debug.Log("Je suis arrivé");
    // Assure que la rotation finale soit exacte
    Lid.transform.rotation = lidTargetRotation;

    isRotating = false; // Réinitialiser la variable booléenne une fois la coroutine terminée
}







    public void OnButtonMenu()
    {
        StartCoroutine(MoveCameraBackMenu(initialPosition.position, initialPosition.rotation));
    }
    IEnumerator MoveCameraBackMenu(Vector3 targetPosition, Quaternion targetRotation)
    {
        soundPlayer.clip = son[4];
        ClickSound();
        buttonMenu.gameObject.SetActive(false);
        // Tant que la caméra n'est pas revenu à la position cible
        while (mainCamera.transform.position != targetPosition)
        {
            // Déplace la caméra progressivement vers la position cible
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, moveSpeedBack * Time.deltaTime);

            // Oriente la caméra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }

        // Une fois que la caméra est revenue à la position cible, désactive le bouton retour, réactive le clic sur l'objet

        isCameraClose = false;
        objectMenu.GetComponent<Collider>().enabled = true;
        objectDumpster.GetComponent<Collider>().enabled = true;
        playButton.gameObject.SetActive(true);
    }






    public void OnPlayClicked()
    {
        soundPlayer.clip = son[0];
        StartCoroutine(MoveCameraPlay(targetPlay.position, targetPlay.rotation));
        playButton.gameObject.SetActive(false);

        // Rotation des objets
        StartCoroutine(RotateDoors());
    }
    IEnumerator RotateDoors()
    {
        // Calcul de la rotation cible
        Quaternion targetRotation1 = Quaternion.Euler(Door1.transform.localRotation.eulerAngles + Vector3.up * rotationAmount);
        Quaternion targetRotation2 = Quaternion.Euler(Door2.transform.localRotation.eulerAngles + Vector3.up * -rotationAmount);

        // Rotation progressive des objets
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            Door1.transform.localRotation = Quaternion.Slerp(Door1.transform.localRotation, targetRotation1, elapsedTime / rotationDuration);
            Door2.transform.localRotation = Quaternion.Slerp(Door2.transform.localRotation, targetRotation2, elapsedTime / rotationDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Assure que la rotation finale soit exacte
        Door1.transform.localRotation = targetRotation1;
        Door2.transform.localRotation = targetRotation2;
    }






    IEnumerator MoveCameraPlay(Vector3 targetPosition, Quaternion targetRotation)
    {
        playButton.gameObject.SetActive(false);
        // Tant que la caméra n'est pas revenu à la position cible
        while (mainCamera.transform.position != targetPosition)
        {
            // Déplace la caméra progressivement vers la position cible
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, playSpeed * Time.deltaTime);

            // Oriente la caméra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }
        // Une fois que la caméra est revenue à la position cible, désactive le bouton retour, réactive le clic sur l'objet
       
        SceneManager.LoadScene("MainScene");
        isCameraClose = false;
        objectMenu.GetComponent<Collider>().enabled = true;
        playButton.gameObject.SetActive(true); 
    }

    public void ClickSound()
    {
        // Vérifiez si l'Audio Source existe et jouez le son
        if (son != null)
        {
            soundPlayer.Play();
        }
        else
        {
            Debug.LogError("Audio Source non assigné. Assurez-vous d'attacher un Audio Source dans l'inspecteur.");
        }
    }
}