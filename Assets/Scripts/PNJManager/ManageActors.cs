using UnityEngine;

public class ManageActors : MonoBehaviour
{
    private GameObject[] doors;

    private void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            // Détruit l'objet s'il a le tag "PNJ"
            foreach (GameObject door in doors)
            {
                DoorLeft doorScript = door.GetComponent<DoorLeft>();
                doorScript.howManyPnjUseDoors -= 1;
                if (doorScript.howManyPnjUseDoors == 0)
                {
                    doorScript.CloseDoor();
                }
            }
            Destroy(other.gameObject, 1f);
        }
    }
}
