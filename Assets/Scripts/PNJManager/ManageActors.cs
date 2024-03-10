using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageActors : MonoBehaviour
{
    private GameObject[] doors;

    private void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
            Destroy(other.gameObject, 1);
        }
    }
}
