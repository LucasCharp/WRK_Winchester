using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraColliderDetection : MonoBehaviour
{
    public int boxNumber = 0; // D�claration de boxNumber en tant que variable statique
    public Button barButton;
    public Button wallButton;
    public Renderer[] barRenderer;
    public Renderer[] djRenderer;
    public Renderer[] videurRenderer;
    public Renderer[] toilettesRenderer;

    public Collider djBox;

    public bool hideBar;
    public bool hideWall;


    private void Start()
    {
        djBox = GetComponent<Collider>();
        djBox.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        djBox.enabled = (false);
        if (other.gameObject.layer == LayerMask.NameToLayer("Box11")) // Videur
        {
           // wallButton.gameObject.SetActive(true);
            //hideWall = false;
            Debug.Log("La cam�ra est entr�e dans la Box 1");

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
                    rend.enabled = true;// Inverser la visibilit�
                }
            }
            if (toilettesRenderer != null)
            {
                foreach (Renderer rend in toilettesRenderer)
                {
                    rend.enabled = true; // Inverser la visibilit�
                }
            }
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box12")) // DJ
        {
            djBox.enabled = (true);
            Debug.Log("La cam�ra est entr�e dans la Box 2");
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
                    rend.enabled = true; // Inverser la visibilit�
                }
            }
            if (barRenderer != null)
            {
                foreach (Renderer rend in barRenderer)
                {
                    rend.enabled = true; // Inverser la visibilit�
                }
            }
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box13")) // Bar
        {
            djBox.enabled = (false);
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
                    rend.enabled = true; // Inverser la visibilit�
                }
            }
            if (djRenderer != null)
            {
                foreach (Renderer rend in djRenderer)
                {
                    rend.enabled = true; // Inverser la visibilit�
                }
            }
            
            Debug.Log("La cam�ra est entr�e dans la Box 3");
        }




        else if (other.gameObject.layer == LayerMask.NameToLayer("Box14")) // Toilettes
        {
            //wallButton.gameObject.SetActive(false);
            //barButton.gameObject.SetActive(true);
            //hideWall = true;
            //hideBar = false;
            Debug.Log("La cam�ra est entr�e dans la Box 4");
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
                        rend.enabled = true; // Inverser la visibilit�
                    }
                }
                if (barRenderer != null)
                {
                    foreach (Renderer rend in barRenderer)
                    {
                        rend.enabled = true; // Inverser la visibilit�
                    }
                }
            }
        }
    }

}
