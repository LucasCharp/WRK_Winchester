using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject objectToClick;
    public Button backButton;
    public Camera mainCamera;
    public Transform targetObject;
    public Transform initialPosition;
    private float moveSpeed = 5f;
    private float moveSpeedBack = 8f;
    private float rotationSpeed = 5f;

    //public Transform initialCameraPosition;
    private bool isCameraClose = false;

    void Start()
    {
        //initialCameraPosition = mainCamera.transform.position;
        backButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCameraClose)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == objectToClick)
                {
                    StartCoroutine(MoveAndRotateCamera(targetObject.position, targetObject.rotation));
                }
                
            }
        }
    }

    IEnumerator MoveAndRotateCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        Debug.Log("Je touche");
            // Tant que la distance entre la cam�ra et la position cible est sup�rieure � une petite marge
            while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
            {
                // D�place la cam�ra progressivement vers la position cible
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Oriente la cam�ra progressivement vers la rotation cible
                mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                yield return null; // Attend une frame

                backButton.gameObject.SetActive(true);// met le bouton retour en visible

                isCameraClose = true;
                backButton.gameObject.SetActive(true);
                // D�sactiver le clic sur l'objet
                objectToClick.GetComponent<Collider>().enabled = false;
            }
    }

    public void OnBackButtonClicked()
    {
        StartCoroutine(MoveAndRotateCameraBack(initialPosition.position, initialPosition.rotation));
    }
    IEnumerator MoveAndRotateCameraBack(Vector3 targetPosition, Quaternion targetRotation)
    {
        backButton.gameObject.SetActive(false);
        // Tant que la cam�ra n'est pas revenu � la position cible
        while (mainCamera.transform.position != targetPosition)
        {
            // D�place la cam�ra progressivement vers la position cible
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, moveSpeedBack * Time.deltaTime);

            // Oriente la cam�ra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame
        }

        // Une fois que la cam�ra est revenue � la position cible, d�sactive le bouton retour, r�active le clic sur l'objet
        
        isCameraClose = false;
        objectToClick.GetComponent<Collider>().enabled = true;
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
