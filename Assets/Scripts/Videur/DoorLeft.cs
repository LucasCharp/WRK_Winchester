using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Controls;

public class DoorLeft : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoothTime;
    public float howManyPnjUseDoors = 0f;

    private bool isMoving = false;
    private bool isOpen = false;

    Animator animator;

    void Start()
    {
        // Assurez-vous que le composant NavMeshSurface est r�f�renc�
        if (navMeshSurface == null)
        {
            navMeshSurface = FindObjectOfType<NavMeshSurface>();
        }

        // Mettez � jour le NavMesh au d�marrage
        //UpdateNavMesh();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("oui");
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

            // Attendez la fin du mouvement de la porte avant de mettre � jour le NavMesh
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
        yield return new WaitForSeconds(smoothTime);

        // Mettez � jour le NavMesh apr�s l'ouverture ou la fermeture de la porte
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
