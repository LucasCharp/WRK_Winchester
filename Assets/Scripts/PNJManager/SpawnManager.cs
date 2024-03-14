using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] pnjPrefabs;
    public float spawnInterval;
    public float numberOfPeopleInQueue;
    public int maxQueueSize = 5;
    public bool isFull = false;

    void Start()
    {
        InvokeRepeating("SpawnRandomPNJ", 2f, spawnInterval);
    }

    void SpawnRandomPNJ()
    {
        // Vérifiez si la file d'attente est pleine avant de spawn un nouveau PNJ
        if (!isFull)
        {
            GameObject selectedPrefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f);
            Instantiate(selectedPrefab, spawnPosition, spawnRotation);
        }
    }

    public void StartQueue()
    {
        numberOfPeopleInQueue += 1;

        // Vérifiez si la file d'attente est pleine
        if (numberOfPeopleInQueue >= maxQueueSize)
        {
            isFull = true;
            Debug.Log("File d'attente pleine !");
        }
    }

    public void QuitQueue()
    {
        Debug.Log("Il quitte la queue");
        numberOfPeopleInQueue -= 1;

        // Réinitialisez la condition de file d'attente pleine
        isFull = false;
    }
}