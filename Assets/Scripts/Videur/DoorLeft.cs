using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DoorLeft : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoothTime;

    private bool isMoving = false;
    private bool isOpen = false;

    void Start()
    {
        // Assurez-vous que le composant NavMeshSurface est référencé
        if (navMeshSurface == null)
        {
            navMeshSurface = FindObjectOfType<NavMeshSurface>();
        }

        // Mettez à jour le NavMesh au démarrage
        UpdateNavMesh();
    }

    void Update()
    {
        // Vérifier s'il y a un toucher sur l'écran
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isMoving)
        {
            // Si la porte est ouverte, la fermer. Sinon, l'ouvrir.
            if (isOpen)
                CloseDoor();
            else
                OpenDoor();

            // Attendez la fin du mouvement de la porte avant de mettre à jour le NavMesh
            StartCoroutine(WaitForDoorMovement());
        }
    }

    void OpenDoor()
    {
        StartCoroutine(MoveDoor(doorOpenAngle));
    }

    void CloseDoor()
    {
        StartCoroutine(MoveDoor(doorCloseAngle));
    }

    IEnumerator MoveDoor(float targetAngle)
    {
        isMoving = true;

        float currentAngle = transform.localEulerAngles.y;
        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(currentAngle, targetAngle, elapsedTime / smoothTime), 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localEulerAngles = new Vector3(0f, targetAngle, 0f);

        isOpen = (targetAngle == doorOpenAngle);
        isMoving = false;
    }

    IEnumerator WaitForDoorMovement()
    {
        // Attendez la fin du mouvement de la porte
        yield return new WaitForSeconds(smoothTime);

        // Mettez à jour le NavMesh après l'ouverture ou la fermeture de la porte
        UpdateNavMesh();
    }

    void UpdateNavMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
