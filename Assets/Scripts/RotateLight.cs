using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    private bool rotating = false;
    public float rotationSpeed;
    public float rotationAmount;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    void Start()
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(startRotation.eulerAngles + new Vector3(0f, rotationAmount, 0f));
    }

    void Update()
    {
        if (!rotating)
        {
            rotating = true;
            RotateObject();
        }
    }

     void RotateObject()
    {
        Invoke("StartRotating",Time.deltaTime);
    }

    void StartRotating()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (transform.rotation == targetRotation)
        {
            Invoke("ReturnToStartRotation", Time.deltaTime);
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
