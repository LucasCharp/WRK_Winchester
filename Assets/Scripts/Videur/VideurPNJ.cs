using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideurPNJ : MonoBehaviour
{
    public float entryInterval;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InvokeInQueue", 15f, entryInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InvokeInQueue()
    {
        print("je me lance");
        GameObject spawner = GameObject.Find("Spawner");
        SpawnManager spawnManager = spawner.GetComponent<SpawnManager>();
        if (spawnManager.numberOfPeopleInQueue > 0)
        {
            // Trouver tous les GameObjects avec le tag "PNJ" dans la sc�ne
            GameObject[] pnjArray = GameObject.FindGameObjectsWithTag("PNJ");

            // Boucler sur tous les GameObjects trouv�s
            foreach (GameObject pnj in pnjArray)
            {
                // R�cup�rer le composant RandomNavMeshMovement attach� � chaque GameObject
                RandomNavMeshMovement randomMove = pnj.GetComponent<RandomNavMeshMovement>();
                if (randomMove != null)
                {
                    // Appeler la fonction InQueue() du script RandomNavMeshMovement
                    randomMove.QuitQueue();
                }
                else
                {
                    Debug.LogError("La r�f�rence au script RandomNavMeshMovement n'est pas d�finie dans le SpawnManager !");
                }
            }
        }
    }
}
