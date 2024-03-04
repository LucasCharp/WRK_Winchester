using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuZoom : MonoBehaviour
{
    public Camera mainCamera; // R�f�rence � la cam�ra que vous souhaitez d�placer
    public Transform targetObject; // L'objet vers lequel la cam�ra se d�placera
    public float moveSpeed = 5f; // Vitesse de d�placement de la cam�ra
    public float rotationSpeed = 5f; // Vitesse de rotation de la cam�ra

    void Update()
    {
        // V�rifie si le joueur a cliqu� avec le bouton gauche de la souris ou touch� l'�cran
        if (Input.GetMouseButtonDown(0))
        {
            // Cr�e un rayon depuis la position de la souris/toucher dans la sc�ne
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si le rayon intersecte un objet dans la sc�ne
            if (Physics.Raycast(ray, out hit))
            {
                // V�rifie si l'objet touch� est celui sur lequel le joueur a cliqu�
                if (hit.collider.gameObject == gameObject)
                {
                    // D�place et oriente la cam�ra vers l'objet
                    StartCoroutine(MoveAndRotateCamera(targetObject.position, targetObject.rotation));
                }
            }
        }
    }

    // Coroutine pour d�placer progressivement la cam�ra vers une position cible et l'orienter
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
    }
}
