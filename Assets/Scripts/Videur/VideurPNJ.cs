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
            // Trouver tous les GameObjects avec le tag "PNJ" dans la scène
            GameObject[] pnjArray = GameObject.FindGameObjectsWithTag("PNJ");

            // Boucler sur tous les GameObjects trouvés
            foreach (GameObject pnj in pnjArray)
            {
                // Récupérer le composant RandomNavMeshMovement attaché à chaque GameObject
                RandomNavMeshMovement randomMove = pnj.GetComponent<RandomNavMeshMovement>();
                if (randomMove != null)
                {
                    // Appeler la fonction InQueue() du script RandomNavMeshMovement
                    randomMove.QuitQueue();
                }
                else
                {
                    Debug.LogError("La référence au script RandomNavMeshMovement n'est pas définie dans le SpawnManager !");
                }
            }
        }
    }
}
