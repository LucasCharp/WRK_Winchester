using UnityEngine;

public class CameraBoxDetector : MonoBehaviour
{
    private CameraColliderDetection cameraColliderDetection; // R�f�rence au script CameraColliderDetection
    private GameObject currentPNJ; // R�f�rence au PNJ de la bo�te actuelle
    private GameObject currentPNJCanvas; // R�f�rence au canvas du PNJ de la bo�te actuelle
    public GameObject[] pnjCanvases; // Liste des canvas li�s au PNJ

    private void Start()
    {
        // Trouver le script CameraColliderDetection attach� � la cam�ra
        cameraColliderDetection = GetComponent<CameraColliderDetection>();

        // Appeler la fonction pour lier les PNJ aux bo�tes
        LinkPNJsToBoxes();
    }

    private void LinkPNJsToBoxes()
    {
        // Trouver tous les PNJs dans la sc�ne
        GameObject[] pnjs = GameObject.FindGameObjectsWithTag("PNJ");

        // Lier chaque PNJ � sa bo�te respective
        foreach (GameObject pnj in pnjs)
        {
            // R�cup�rer le composant BoxToPNJLinker associ� au PNJ
            BoxToPNJLinker linker = pnj.GetComponent<BoxToPNJLinker>();
            Debug.Log(linker);

            // V�rifier si le composant existe
            if (linker != null)
            {
                // Lier le PNJ � sa bo�te respective en utilisant la variable boxNumber de CameraColliderDetection
                linker.LinkPNJToBox(cameraColliderDetection.boxNumber);
            }
            else
            {
                Debug.LogWarning("Aucun composant BoxToPNJLinker trouv� sur le PNJ : " + pnj.name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifier si le collider avec lequel la cam�ra entre en collision appartient � une box
        if (other.CompareTag("Box"))
        {
            // Obtenir une r�f�rence � CameraColliderDetection
            CameraColliderDetection cameraColliderDetection = other.GetComponent<CameraColliderDetection>();

            if (cameraColliderDetection != null)
            {
                // Utiliser la variable boxNumber de l'instance de CameraColliderDetection pour d�terminer la box
                int boxNumber = cameraColliderDetection.boxNumber;
                Debug.Log("La cam�ra est entr�e dans la Box " + boxNumber);

                // Trouver le PNJ li� � la box
                currentPNJ = FindPNJByBoxNumber(boxNumber);
                if (currentPNJ != null)
                {
                    Debug.Log("PNJ trouv� pour la bo�te " + boxNumber + ": " + currentPNJ.name);
                    // D�sactiver le canvas du PNJ pr�c�dent
                    if (currentPNJCanvas != null)
                    {
                        currentPNJCanvas.SetActive(false);
                    }
                    // Activer le canvas du PNJ actuel
                    currentPNJCanvas = GetCanvasOfPNJ(currentPNJ);
                    if (currentPNJCanvas != null)
                    {
                        currentPNJCanvas.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogWarning("Aucun PNJ trouv� pour la bo�te " + boxNumber);
                }
            }
            else
            {
                Debug.LogWarning("CameraColliderDetection non trouv�e sur le collider.");
            }
        }
    }

    private GameObject GetCanvasOfPNJ(GameObject pnj)
    {
        if (pnj != null)
        {
            PNJCanvasController canvasController = pnj.GetComponent<PNJCanvasController>();
            if (canvasController != null)
            {
                foreach (GameObject canvasObject in canvasController.pnjCanvases)
                {
                    Canvas canvas = canvasObject.GetComponent<Canvas>();
                    if (canvas != null)
                    {
                        return canvasObject;
                    }
                    else
                    {
                        Debug.LogWarning("Aucun composant Canvas trouv� sur le canvas PNJ.");
                    }
                }
            }
            else
            {
                Debug.LogWarning("Aucun script PNJCanvasController trouv� sur le PNJ.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun PNJ trouv� pour la bo�te donn�e.");
        }
        return null;
    }

    public GameObject FindPNJByBoxNumber(int boxNumber)
    {
        GameObject[] allPNJs = GameObject.FindGameObjectsWithTag("PNJ"); // Trouver tous les PNJs dans la sc�ne

        foreach (GameObject pnj in allPNJs)
        {
            BoxToPNJLinker linker = pnj.GetComponent<BoxToPNJLinker>();
            if (linker != null && linker.boxNumber == boxNumber)
            {
                // Si le num�ro de la bo�te du lien correspond au num�ro de la bo�te recherch�e
                return pnj; // Retourner le PNJ associ�
            }
        }

        Debug.LogWarning("Aucun PNJ trouv� pour la bo�te " + boxNumber);
        return null; // Retourner null si aucun PNJ trouv� pour le num�ro de bo�te donn�
    }
}