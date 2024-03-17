using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public bool rotating = false;
    public float rotationSpeed;
    public float rotationAmount;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    public MainSceneManager mainSceneManager;
    private bool hasStarted;
    public bool canRotate = true;
    private void Start()
    {
        hasStarted = false;
    }
    void StartMoving()
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(startRotation.eulerAngles + new Vector3(0f, rotationAmount, 0f));
    }

    void Update()
    {
        if (mainSceneManager.startGame == true)
        {
            if (!hasStarted)
            {
                StartMoving();
                hasStarted = true;
            }
            if (!rotating)
            {
                rotating = true;
                RotateObject();
            }
        }
         else if (mainSceneManager.startGame == false)
         {
            rotating = false;
         }
    }

    void RotateObject()
    {
        if (canRotate == true)
        {
            Invoke("StartRotating", Time.deltaTime);
        }
        
    }

    public void StartRotating()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (transform.rotation == targetRotation)
        {
            Invoke("ReturnToStartRotation", Time.deltaTime);
            //Debug.Log("Je suis arrivé");
        }
        else
        {
            Invoke("RotateObject", Time.deltaTime);
        }
    }
     void ReturnToStartRotation()
    {
    
        transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, rotationSpeed * Time.deltaTime);

        if (transform.rotation == startRotation)
        {
            rotating = false;
        }
        else
        {
            Invoke("ReturnToStartRotation", Time.deltaTime);
        }
    }
}
