using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliderDetection : MonoBehaviour
{
    public int boxNumber = 0; // D�claration de boxNumber en tant que variable statique


    private void OnTriggerEnter(Collider other)
    {

        // V�rifie si le collider avec lequel la cam�ra entre en collision appartient � un des Layers d�sir�s
        if (other.gameObject.layer == LayerMask.NameToLayer("Box11"))
        {
            boxNumber = 1;
            Debug.Log("La cam�ra est entr�e dans la Box 1");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Box12"))
        {
            boxNumber = 2;
            Debug.Log("La cam�ra est entr�e dans la Box 2");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Box13"))
        {
            boxNumber = 3;
            Debug.Log("La cam�ra est entr�e dans la Box 3");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Box14"))
        {
            boxNumber = 4;
            Debug.Log("La cam�ra est entr�e dans la Box 4");
        }
        else
        Debug.Log("pas de box reconnu");
    }

}