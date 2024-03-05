using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isDragging = false;
    private bool rotationStarted = false;
    private float targetRotation = 0f;
    private float rotationSpeed = 90f; // Vitesse de rotation de la cam�ra en degr�s par seconde
    public bool canRotate;

    void Update()
    {
        if (canRotate == true) 
        {
            // V�rifie si le joueur touche l'�cran
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // G�re les diff�rents �tats du toucher
                switch (touch.phase)
                {

                    case TouchPhase.Began:
                        // D�marre le drag
                        isDragging = true;
                        touchStartPos = touch.position;
                        break;

                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            // Calcule la distance horizontale du slide
                            float slideDistanceX = touch.position.x - touchStartPos.x;

                            // Si le slide d�passe un certain seuil et que la rotation n'a pas encore commenc�
                            if (Mathf.Abs(slideDistanceX) > 50f && !rotationStarted)
                            {
                                rotationStarted = true;

                                // D�termine la direction de rotation en fonction du slide
                                float rotationDirection = Mathf.Sign(slideDistanceX);

                                // Calcule la rotation cible en fonction de la direction du slide
                                targetRotation = (transform.rotation.eulerAngles.y + 90f * rotationDirection) % 360f;
                            }// D�marre la rotation de la cam�ra
                        }
                        break;

                    case TouchPhase.Ended:
                        // R�initialise les param�tres lorsque le toucher est termin�
                        isDragging = false;
                        break;
                }
            }

            Canvas[] canvases = FindObjectsOfType<Canvas>();
            // Si la rotation a commenc�, fait tourner progressivement la cam�ra vers la rotation cible
            if (rotationStarted)
            {
                float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

                // Parcourt tous les canvas et les d�sactive
                foreach (Canvas canvas in canvases)
                {
                    canvas.gameObject.SetActive(false);
                }

                // Si la cam�ra a atteint la rotation cible, r�initialise la rotation
                if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation)) < 0.01f)
                {
                    rotationStarted = false;
                    // R�initialise la rotation cible
                    targetRotation = transform.rotation.eulerAngles.y;
                }
            }
        }
    }
}
