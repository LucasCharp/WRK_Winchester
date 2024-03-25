using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class DoorLeft : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoothTime;
    public float howManyPnjUseDoors = 0f;

    private bool isMoving = false;
    private bool isOpen = false;

    void Start()
    {
        if (navMeshSurface == null)
        {
            navMeshSurface = FindObjectOfType<NavMeshSurface>();
        }
    }
    public void OpenDoor()
    {
        StartCoroutine(MoveDoor(doorOpenAngle));
        StartCoroutine(WaitForDoorMovement());
    }

    public void CloseDoor()
    {
        StartCoroutine(MoveDoor(doorCloseAngle));
        StartCoroutine(WaitForDoorMovement());
    }

    public void ManageDoor()
    {
        if (!isMoving)
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
        yield return new WaitForSeconds(smoothTime + 2);

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
