using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] pnjPrefabs; // Tableau contenant vos pr�fabs avec le tag "PNJ"
    public float spawnInterval; // Intervalle entre chaque spawn en secondes

    void Start()
    {
        // Appeler la fonction SpawnRandomPNJ au d�marrage
        InvokeRepeating("SpawnRandomPNJ", 0f, spawnInterval);
    }

    void SpawnRandomPNJ()
    {
        // S�lectionner un pr�fab de mani�re al�atoire
        GameObject selectedPrefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];

        // Utiliser la position du GameObject qui d�tient le script comme position de spawn
        Vector3 spawnPosition = transform.position;

        // Instancier le pr�fab � la position du GameObject
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

    }
}