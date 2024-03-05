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
    public Button buttonDumpster;
    public Button buttonMenu;
    public Camera mainCamera;
    public Transform targetDumpster;
    public Transform targetMenu;
    public Transform initialPosition;
    private float moveSpeed = 5f;
    private float moveSpeedBack = 8f;
    private float rotationSpeed = 5f;
    public Button playButton;
    private GameObject objectTouched;

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
            }
            buttonMenu.gameObject.SetActive(true);// met le bouton en visible après que la caméra soit arrivée, pour pas spam dessus
        }

    }

    public void OnButtonDumpster()
    {
        StartCoroutine(MoveCameraBackDump(initialPosition.position, initialPosition.rotation));
    }
    IEnumerator MoveCameraBackDump(Vector3 targetPosition, Quaternion targetRotation)
    {
        soundPlayer.clip = son[2];
        ClickSound();
        buttonDumpster.gameObject.SetActive(false);
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
        playButton.gameObject.SetActive(true);
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
        playButton.gameObject.SetActive(true);
    }

    public void OnPlayClicked()
    {
        soundPlayer.clip = son[0];
        SceneManager.LoadScene("MainScene");
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