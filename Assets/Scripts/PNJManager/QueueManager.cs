using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    public float numberOfPeopleInQueue;
    public bool isFull = false;
    public int maxQueueSize = 5; // Ajustez cette valeur selon votre besoin

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            numberOfPeopleInQueue += 1;

            // Vérifiez si la file d'attente est pleine
            if (numberOfPeopleInQueue >= maxQueueSize)
            {
                isFull = true;
                Debug.Log("File d'attente pleine !");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            Debug.Log("Il quitte la queue");
            numberOfPeopleInQueue -= 1;

            // Réinitialisez la condition de file d'attente pleine
            isFull = false;
        }
    }
}