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
        InvokeRepeating("SpawnRandomPNJ", 2f, spawnInterval);     
    }

    void SpawnRandomPNJ()
    {
        GameObject file = GameObject.Find("File");
        QueueManager queueManager = file.GetComponent<QueueManager>();
        if (queueManager.isFull == false)
        {
            // S�lectionner un pr�fab de mani�re al�atoire
            GameObject selectedPrefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];

            // Utiliser la position du GameObject qui d�tient le script comme position de spawn
            Vector3 spawnPosition = transform.position;

            // Cr�er une rotation de 90 degr�s autour de l'axe Y
            Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f);

            // Instancier le pr�fab avec la position et la rotation sp�cifi�es
            Instantiate(selectedPrefab, spawnPosition, spawnRotation);
        }
    }
}