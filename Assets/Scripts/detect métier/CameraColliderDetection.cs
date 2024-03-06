using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliderDetection : MonoBehaviour
{
    public int boxNumber = 0; // Variable publique contenant la valeur de la box

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le collider avec lequel la caméra entre en collision appartient à un des Layers désirés
        if (other.gameObject.layer == LayerMask.NameToLayer("Box11"))
        {
            boxNumber = 1;
            Debug.Log("La caméra est entrée dans la Box 1");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Box12"))
        {
            boxNumber = 2;
            Debug.Log("La caméra est entrée dans la Box 2");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Box13"))
        {
            boxNumber = 3;
            Debug.Log("La caméra est entrée dans la Box 3");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Box14"))
        {
            boxNumber = 4;
            Debug.Log("La caméra est entrée dans la Box 4");
        }
        else
        Debug.Log("pas de box reconnu");
        // Ajoutez d'autres conditions pour les autres Layers ici
    }
}