using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] pnjPrefabs; // Tableau contenant vos préfabs avec le tag "PNJ"
    public float spawnInterval; // Intervalle entre chaque spawn en secondes

    void Start()
    {
        // Appeler la fonction SpawnRandomPNJ au démarrage
        InvokeRepeating("SpawnRandomPNJ", 2f, spawnInterval);     
    }

    void SpawnRandomPNJ()
    {
        GameObject file = GameObject.Find("File");
        QueueManager queueManager = file.GetComponent<QueueManager>();
        if (queueManager.isFull == false)
        {
            // Sélectionner un préfab de manière aléatoire
            GameObject selectedPrefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];

            // Utiliser la position du GameObject qui détient le script comme position de spawn
            Vector3 spawnPosition = transform.position;

            // Créer une rotation de 90 degrés autour de l'axe Y
            Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f);

            // Instancier le préfab avec la position et la rotation spécifiées
            Instantiate(selectedPrefab, spawnPosition, spawnRotation);
        }
    }
}