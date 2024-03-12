using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    Animator animator;
    public float numberOfPeopleInQueue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            numberOfPeopleInQueue += 1;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            numberOfPeopleInQueue -= 1;
        }
    }

}
