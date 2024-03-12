using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraColliderDetection : MonoBehaviour
{
    public int boxNumber = 0; // Déclaration de boxNumber en tant que variable statique
    public Button barButton;
    public Button wallButton;
    public Renderer[] barRenderer;
    public Renderer[] djRenderer;
    public Renderer[] videurRenderer;
    public Renderer[] toilettesRenderer;



    //objectRenderer.enabled = !objectRenderer.enabled;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Box11")) // Videur
        {
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
                    rend.enabled = rend.enabled; // Inverser la visibilité
                }
            }
            if (toilettesRenderer != null)
            {
                foreach (Renderer rend in toilettesRenderer)
                {
                    rend.enabled = rend.enabled; // Inverser la visibilité
                }
            }
        }



        //J'en suis là




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box12")) // DJ
        {
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
                    rend.enabled = !rend.enabled; // Inverser la visibilité
                }
            }
            if (barRenderer != null)
            {
                foreach (Renderer rend in barRenderer)
                {
                    rend.enabled = !rend.enabled; // Inverser la visibilité
                }
            }
        }








        else if (other.gameObject.layer == LayerMask.NameToLayer("Box13")) // Bar
        {
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
                    rend.enabled = !rend.enabled; // Inverser la visibilité
                }
            }
            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = !rend.enabled; // Inverser la visibilité
                }
            }
            barButton.gameObject.SetActive(false);
            Debug.Log("La caméra est entrée dans la Box 3");
        }








        else if (other.gameObject.layer == LayerMask.NameToLayer("Box14")) // Toilettes
        {
            wallButton.gameObject.SetActive(false);
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
                        rend.enabled = !rend.enabled; // Inverser la visibilité
                    }
                }
                if (barRenderer != null)
                {
                    foreach (Renderer rend in barRenderer)
                    {
                        rend.enabled = !rend.enabled; // Inverser la visibilité
                    }
                }
            }
        }
    }

}
