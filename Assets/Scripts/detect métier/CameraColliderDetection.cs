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

    public GameObject djBox;

    public bool hideBar;
    public bool hideWall;


    private void Start()
    {
        djBox.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Box11")) // Videur
        {
            djBox.SetActive(false);
            // wallButton.gameObject.SetActive(true);
            //hideWall = false;
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
            
            Debug.Log("La caméra est entrée dans la Box 2");
            //barButton.gameObject.SetActive(true);
            //hideBar = false;
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
            djBox.SetActive(false);
            //wallButton.gameObject.SetActive(true);
            //barButton.gameObject.SetActive(false);

            //hideBar = true;
            //hideWall = false;
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
            djBox.SetActive(true);
            //wallButton.gameObject.SetActive(false);
            //barButton.gameObject.SetActive(true);
            //hideWall = true;
            //hideBar = false;
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
