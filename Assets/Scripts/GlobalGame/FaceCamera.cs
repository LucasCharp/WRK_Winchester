using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour // Ce code permet de faire en sorte que les boutons suivent toujours la caméra
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation*Vector3.forward, Camera.main.transform.rotation*Vector3.up);
    }
}