using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ExitManager : MonoBehaviour
{
    Animator animator;

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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            if (animator.GetBool("hasEnterPub") == false)
            {
                foreach (GameObject door in doors)
                {
                    DoorLeft doorScript = door.GetComponent<DoorLeft>();
                    doorScript.howManyPnjUseDoors -= 1;
                    if (doorScript.howManyPnjUseDoors == 0)
                    {
                        doorScript.CloseDoor();
                    }
                }
                animator.SetBool("hasEnterPub", true);
                GoBackInPub(other);
            }
        }
    }

    public void GoBackInPub(Collider other)
    {
        animator = other.GetComponent<Animator>();
        int actions = animator.GetInteger("actions");
        if (-1 < actions && actions < 3)
        {
            animator.SetBool("shouldGoIn", true);
        }
    }
}
