
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isSwiping = false;
    private bool rotationStarted = false;
    private float targetRotation = 0f;
    private float rotationSpeed = 90f; // Vitesse de rotation de la caméra en degrés par seconde
    public bool canRotate;
    public GameObject jobCanvas;
    public List<Button> buttonsToDisable;
    void Update()
    {
        // Vérifie si la rotation est autorisée et si un mouvement de glissement est détecté
        if (canRotate && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Démarre le mouvement de glissement
                    isSwiping = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        // Vérifie si le mouvement de glissement dépasse un certain seuil
                        float slideDistanceX = touch.position.x - touchStartPos.x;
                        if (Mathf.Abs(slideDistanceX) > 250f && !rotationStarted)
                        {
                            // Calcule la direction de la rotation et démarre la rotation
                            float rotationDirection = Mathf.Sign(slideDistanceX);
                            targetRotation = (transform.rotation.eulerAngles.y + 90f * rotationDirection) % 360f;
                            rotationStarted = true;
                            foreach (Button button in buttonsToDisable)
                            {
                                button.interactable = false;
                            }
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    // Termine le mouvement de glissement
                    isSwiping = false;
                    break;
            }
        }

        // Si la rotation a commencé, fait tourner progressivement la caméra vers la rotation cible
        if (rotationStarted)
        {
            float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

            // Si la caméra a atteint la rotation cible, réinitialise la rotation
            if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation)) < 0.01f)

            {
                foreach (Button button in buttonsToDisable)
                {
                    button.interactable = true;
                }
                rotationStarted = false;
            }
        }
    }
}