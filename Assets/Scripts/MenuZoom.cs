using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuZoom : MonoBehaviour
{
    public Camera mainCamera; // Référence à la caméra que vous souhaitez déplacer
    public Transform targetObject; // L'objet vers lequel la caméra se déplacera
    public float moveSpeed = 5f; // Vitesse de déplacement de la caméra
    public float rotationSpeed = 5f; // Vitesse de rotation de la caméra

    void Update()
    {
        // Vérifie si le joueur a cliqué avec le bouton gauche de la souris ou touché l'écran
        if (Input.GetMouseButtonDown(0))
        {
            // Crée un rayon depuis la position de la souris/toucher dans la scène
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si le rayon intersecte un objet dans la scène
            if (Physics.Raycast(ray, out hit))
            {
                // Vérifie si l'objet touché est celui sur lequel le joueur a cliqué
                if (hit.collider.gameObject == gameObject)
                {
                    // Déplace et oriente la caméra vers l'objet
                    StartCoroutine(MoveAndRotateCamera(targetObject.position, targetObject.rotation));
                }
            }
        }
    }

    // Coroutine pour déplacer progressivement la caméra vers une position cible et l'orienter
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
    }
}
