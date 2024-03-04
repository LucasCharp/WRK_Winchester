using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button retourPoubelle;
    public Button retourMenu;
    public Camera mainCamera;
    public Transform targetObject;
    public Button retourButton;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    private MenuZoom menuZoom;
    void Start()
    {
        retourPoubelle.gameObject.SetActive(false);
        retourMenu.gameObject.SetActive(false);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnButton()
    {
        StartCoroutine(MoveAndRotateCamera(targetObject.position, targetObject.rotation));
    }
    IEnumerator MoveAndRotateCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        Debug.Log("Je touche");
        // Tant que la distance entre la caméra et la position cible est supérieure à une petite marge
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            // Déplace la caméra progressivement vers la position cible
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Oriente la caméra progressivement vers la rotation cible
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null; // Attend une frame

            retourButton.gameObject.SetActive(false);// met le bouton retour en visible
            
            menuZoom = GetComponent<MenuZoom>();
            menuZoom.SetCollider();
        }
    }
}