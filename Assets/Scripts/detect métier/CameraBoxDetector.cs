using UnityEngine;

public class CameraBoxDetector : MonoBehaviour
{
    private CameraColliderDetection cameraColliderDetection; // Référence au script CameraColliderDetection
    private GameObject currentPNJ; // Référence au PNJ de la boîte actuelle
    private GameObject currentPNJCanvas; // Référence au canvas du PNJ de la boîte actuelle
    public GameObject[] pnjCanvases; // Liste des canvas liés au PNJ

    private void Start()
    {
        // Trouver le script CameraColliderDetection attaché à la caméra
        cameraColliderDetection = GetComponent<CameraColliderDetection>();

        // Appeler la fonction pour lier les PNJ aux boîtes
        LinkPNJsToBoxes();
    }

    private void LinkPNJsToBoxes()
    {
        // Trouver tous les PNJs dans la scène
        GameObject[] pnjs = GameObject.FindGameObjectsWithTag("PNJ");

        // Lier chaque PNJ à sa boîte respective
        foreach (GameObject pnj in pnjs)
        {
            // Récupérer le composant BoxToPNJLinker associé au PNJ
            BoxToPNJLinker linker = pnj.GetComponent<BoxToPNJLinker>();
            Debug.Log(linker);

            // Vérifier si le composant existe
            if (linker != null)
            {
                // Lier le PNJ à sa boîte respective en utilisant la variable boxNumber de CameraColliderDetection
                linker.LinkPNJToBox(cameraColliderDetection.boxNumber);
            }
            else
            {
                Debug.LogWarning("Aucun composant BoxToPNJLinker trouvé sur le PNJ : " + pnj.name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si le collider avec lequel la caméra entre en collision appartient à une box
        if (other.CompareTag("Box"))
        {
            // Obtenir une référence à CameraColliderDetection
            CameraColliderDetection cameraColliderDetection = other.GetComponent<CameraColliderDetection>();

            if (cameraColliderDetection != null)
            {
                // Utiliser la variable boxNumber de l'instance de CameraColliderDetection pour déterminer la box
                int boxNumber = cameraColliderDetection.boxNumber;
                Debug.Log("La caméra est entrée dans la Box " + boxNumber);

                // Trouver le PNJ lié à la box
                currentPNJ = FindPNJByBoxNumber(boxNumber);
                if (currentPNJ != null)
                {
                    Debug.Log("PNJ trouvé pour la boîte " + boxNumber + ": " + currentPNJ.name);
                    // Désactiver le canvas du PNJ précédent
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
                    Debug.LogWarning("Aucun PNJ trouvé pour la boîte " + boxNumber);
                }
            }
            else
            {
                Debug.LogWarning("CameraColliderDetection non trouvée sur le collider.");
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
                        Debug.LogWarning("Aucun composant Canvas trouvé sur le canvas PNJ.");
                    }
                }
            }
            else
            {
                Debug.LogWarning("Aucun script PNJCanvasController trouvé sur le PNJ.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun PNJ trouvé pour la boîte donnée.");
        }
        return null;
    }

    public GameObject FindPNJByBoxNumber(int boxNumber)
    {
        GameObject[] allPNJs = GameObject.FindGameObjectsWithTag("PNJ"); // Trouver tous les PNJs dans la scène

        foreach (GameObject pnj in allPNJs)
        {
            BoxToPNJLinker linker = pnj.GetComponent<BoxToPNJLinker>();
            if (linker != null && linker.boxNumber == boxNumber)
            {
                // Si le numéro de la boîte du lien correspond au numéro de la boîte recherchée
                return pnj; // Retourner le PNJ associé
            }
        }

        Debug.LogWarning("Aucun PNJ trouvé pour la boîte " + boxNumber);
        return null; // Retourner null si aucun PNJ trouvé pour le numéro de boîte donné
    }
}