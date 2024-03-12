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
        InvokeRepeating("SpawnRandomPNJ", 0f, spawnInterval);
    }

    void SpawnRandomPNJ()
    {
        // Sélectionner un préfab de manière aléatoire
        GameObject selectedPrefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];

        // Utiliser la position du GameObject qui détient le script comme position de spawn
        Vector3 spawnPosition = transform.position;

        // Instancier le préfab à la position du GameObject
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

    }
}