using System.Collections;
using System.Collections.Generic;
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
    public GameObject scoreSheet;
    public GameObject scoreSheetTarget;
    public GameObject scoreSheetOrigin;
    private GameObject objectTouched;
    //private Vector3 scorePosition;

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
    private float playSpeed = 5f;
    private float rotationDuration = 100f;
    private float lidRotDuration = 1f;
    private float scoreMoveDuration = 0.2f;
    private float rotationAmount = 110f;

    public List<AudioClip> son;
    public AudioSource soundPlayer;

    
    

    //public Transform initialCameraPosition;
    private bool isCameraClose = false;

    void Start()
    {
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
            //soundPlayer.clip = son[1];
            //ClickSound();
            SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);

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
            //soundPlayer.clip = son[3];
            //ClickSound();
            SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);

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
        // Sauvegarde de la position initiale de scoreSheet
        Vector3 initialPosition = scoreSheet.transform.position;

        // Calcul de la rotation cible pour Lid
        Quaternion targetRotation = Quaternion.Euler(
            Lid.transform.localRotation.eulerAngles.x - 45f, // Rotation sur l'axe X
            Lid.transform.localRotation.eulerAngles.y,      // Conserver la rotation sur l'axe Y
            Lid.transform.localRotation.eulerAngles.z       // Rotation sur l'axe Z
        );

        // Calcul de la position cible pour scoreSheet
        Vector3 targetPosition = scoreSheetTarget.transform.position;

        // Durée de la rotation de Lid
        float lidElapsedTime = 0f;
        while (lidElapsedTime < lidRotDuration)
        {
            float t = lidElapsedTime / lidRotDuration;
            Lid.transform.localRotation = Quaternion.Slerp(Lid.transform.localRotation, targetRotation, t);

            // Interpolation linéaire pour déplacer scoreSheet vers targetPosition
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            scoreSheet.transform.position = newPosition;

            lidElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assure que la rotation finale de Lid soit exacte
        Lid.transform.localRotation = targetRotation;
        Debug.Log("Rotation Lid terminée.");

        // Assure que la position finale de scoreSheet soit exacte
        scoreSheet.transform.position = targetPosition;
        Debug.Log("Déplacement de scoreSheet terminé.");
    }







    public void OnButtonDumpster()
    {
        StartCoroutine(MoveCameraBackDump(initialPosition.position, initialPosition.rotation));
        buttonDumpster.gameObject.SetActive(false);
        StartCoroutine(RotateLidBack());
        StartCoroutine(ScoreSheetBack());
    }
   
    IEnumerator MoveCameraBackDump(Vector3 targetPosition, Quaternion targetRotation)
    {
        //soundPlayer.clip = son[2];
        //ClickSound();
        SFXManager.instance.PlaySoundFXClip(son[2], transform, 1f);

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

        // Calcul de la rotation cible pour Lid
        Quaternion targetRotation = Quaternion.Euler(
            Lid.transform.localRotation.eulerAngles.x - -45f, // Rotation sur l'axe X
            Lid.transform.localRotation.eulerAngles.y,      // Conserver la rotation sur l'axe Y
            Lid.transform.localRotation.eulerAngles.z       // Rotation sur l'axe Z
        );

        // Durée de la rotation de Lid
        float lidElapsedTime = 0f;
        while (lidElapsedTime < lidRotDuration)
        {
            float t = lidElapsedTime / lidRotDuration;
            Lid.transform.localRotation = Quaternion.Slerp(Lid.transform.localRotation, targetRotation, t);


            lidElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assure que la rotation finale de Lid soit exacte
        Lid.transform.localRotation = targetRotation;
        Debug.Log("Rotation Lid terminée.");

    }

    IEnumerator ScoreSheetBack()
    {
        // Sauvegarde de la position initiale de scoreSheet
        Vector3 initialPosition = scoreSheet.transform.position;
        // Calcul de la position cible pour scoreSheet
        Vector3 targetPosition = scoreSheetOrigin.transform.position;
       
        float lidElapsedTime = 0f;
        while (lidElapsedTime < scoreMoveDuration)
        {
            float t = lidElapsedTime / scoreMoveDuration;
            // Interpolation linéaire pour déplacer scoreSheet vers targetPosition
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            scoreSheet.transform.position = newPosition;

            lidElapsedTime += Time.deltaTime;
            yield return null;
        }
        // Assure que la position finale de scoreSheet soit exacte
        scoreSheet.transform.position = targetPosition;
        Debug.Log("Déplacement de scoreSheet terminé.");
    }




    public void OnButtonMenu()
    {
        StartCoroutine(MoveCameraBackMenu(initialPosition.position, initialPosition.rotation));
    }
    IEnumerator MoveCameraBackMenu(Vector3 targetPosition, Quaternion targetRotation)
    {
        //soundPlayer.clip = son[4];
        //ClickSound();
        SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);

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
        //soundPlayer.clip = son[0];
        SFXManager.instance.PlaySoundFXClip(son[0], transform, 1f);

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
       
        SceneManager.LoadScene("Scene-Claire");
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