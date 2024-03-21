
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isSwiping = false;
    private bool rotationStarted = false;
    private float targetRotation = 0f;
    private float rotationSpeed = 90f; // Vitesse de rotation de la cam�ra en degr�s par seconde
    public bool canRotate;
    public GameObject jobCanvas;
    public List<Button> buttonsToDisable;
    void Update()
    {
        // V�rifie si la rotation est autoris�e et si un mouvement de glissement est d�tect�
        if (canRotate && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // D�marre le mouvement de glissement
                    isSwiping = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        // V�rifie si le mouvement de glissement d�passe un certain seuil
                        float slideDistanceX = touch.position.x - touchStartPos.x;
                        if (Mathf.Abs(slideDistanceX) > 250f && !rotationStarted)
                        {
                            // Calcule la direction de la rotation et d�marre la rotation
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

        // Si la rotation a commenc�, fait tourner progressivement la cam�ra vers la rotation cible
        if (rotationStarted)
        {
            float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

            // Si la cam�ra a atteint la rotation cible, r�initialise la rotation
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