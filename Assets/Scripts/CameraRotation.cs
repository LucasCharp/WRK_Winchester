using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isDragging = false;
    private bool rotationStarted = false;
    private float targetRotation = 0f;
    private float rotationSpeed = 90f; // Vitesse de rotation de la caméra en degrés par seconde
    public bool canRotate;

    void Update()
    {
        if (canRotate == true) 
        {
            // Vérifie si le joueur touche l'écran
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Gère les différents états du toucher
                switch (touch.phase)
                {

                    case TouchPhase.Began:
                        // Démarre le drag
                        isDragging = true;
                        touchStartPos = touch.position;
                        break;

                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            // Calcule la distance horizontale du slide
                            float slideDistanceX = touch.position.x - touchStartPos.x;

                            // Si le slide dépasse un certain seuil et que la rotation n'a pas encore commencé
                            if (Mathf.Abs(slideDistanceX) > 50f && !rotationStarted)
                            {
                                rotationStarted = true;

                                // Détermine la direction de rotation en fonction du slide
                                float rotationDirection = Mathf.Sign(slideDistanceX);

                                // Calcule la rotation cible en fonction de la direction du slide
                                targetRotation = (transform.rotation.eulerAngles.y + 90f * rotationDirection) % 360f;
                            }// Démarre la rotation de la caméra
                        }
                        break;

                    case TouchPhase.Ended:
                        // Réinitialise les paramètres lorsque le toucher est terminé
                        isDragging = false;
                        break;
                }
            }

            Canvas[] canvases = FindObjectsOfType<Canvas>();
            // Si la rotation a commencé, fait tourner progressivement la caméra vers la rotation cible
            if (rotationStarted)
            {
                float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

                // Parcourt tous les canvas et les désactive
                foreach (Canvas canvas in canvases)
                {
                    canvas.gameObject.SetActive(false);
                }

                // Si la caméra a atteint la rotation cible, réinitialise la rotation
                if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation)) < 0.01f)
                {
                    rotationStarted = false;
                    // Réinitialise la rotation cible
                    targetRotation = transform.rotation.eulerAngles.y;
                }
            }
        }
    }
}
