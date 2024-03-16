using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraColliderDetection : MonoBehaviour
{
    public int boxNumber = 0;
    public Button barButton;
    public Button wallButton;
    public Renderer[] barRenderer;
    public Renderer[] djRenderer;
    public Renderer[] videurRenderer;
    public Renderer[] toilettesRenderer;

    public GameObject djBox;
    public GameObject barBox;
    public GameObject videurBox;
    //public Camera mainCamera;
    //public float increaseAmountY = 3f;
    //public float increaseAmountX = 4f;
    private void Start()
    {
        djBox.SetActive(false);
        barBox.SetActive(false);
        videurBox.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Box11")) // Videur
        {
            videurBox.SetActive(true);
            djBox.SetActive(false);

            Debug.Log("La caméra est entrée dans la Box 1");

            if (videurRenderer != null)
            {
                foreach (Renderer rend in videurRenderer)
                {
                    rend.enabled = false; // Rendre invisible
                }
            }

            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = true;// Inverser la visibilité
                }
            }
            if (toilettesRenderer != null)
            {
                foreach (Renderer rend in toilettesRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box12")) // DJ
        {
            barBox.SetActive(false);
            djBox.SetActive(true);
            videurBox.SetActive(false);
            Debug.Log("La caméra est entrée dans la Box 2");

            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = false; // Rendre invisible
                }
            }

            if (videurRenderer != null)
            {
                foreach (Renderer rend in videurRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
            if (barRenderer != null)
            {
                foreach (Renderer rend in barRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box13")) // Bar
        {
            barBox.SetActive(true);
            djBox.SetActive(false);

            if (barRenderer != null)
            {
                foreach (Renderer rend in barRenderer)
                {
                    rend.enabled = false; // Rendre invisible
                }
            }

            if (toilettesRenderer != null)
            {
                foreach (Renderer rend in toilettesRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = true; // Inverser la visibilité
                }
            }
            
            Debug.Log("La caméra est entrée dans la Box 3");
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box14")) // Toilettes
        {
            barBox.SetActive(false);
            videurBox.SetActive(false);
            
            //Vector3 newCameraPosition = mainCamera.transform.position;
            //newCameraPosition.y += increaseAmountY;
            //newCameraPosition.x += increaseAmountX;
            //mainCamera.transform.position = newCameraPosition;

            Debug.Log("La caméra est entrée dans la Box 4");
            if (videurRenderer != null)
            {
                if (toilettesRenderer != null)
                {
                    foreach (Renderer rend in toilettesRenderer)
                    {
                        rend.enabled = false; // Rendre invisible
                    }
                }

                if (videurRenderer != null)
                {
                    foreach (Renderer rend in videurRenderer)
                    {
                        rend.enabled = true; // Inverser la visibilité
                    }
                }
                if (barRenderer != null)
                {
                    foreach (Renderer rend in barRenderer)
                    {
                        rend.enabled = true; // Inverser la visibilité
                    }
                }
            }
        }
    }
}
