using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideurPNJ : MonoBehaviour
{
    public float entryInterval;
    private Coroutine repeatingInvoke;
    // Start is called before the first frame update
    void Start()
    {
        StartRepeatingInvoke(entryInterval);
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

    void StartRepeatingInvoke(float interval)
    {
        // Arrêter l'invocation répétée actuelle, s'il y en a une
        StopRepeatingInvoke();

        if (interval > 0)
        {
            // Démarrer une nouvelle invocation répétée avec l'intervalle donné
            repeatingInvoke = StartCoroutine(RepeatInvoke(interval));
        }
    }

    // Fonction pour arrêter l'invocation répétée
    void StopRepeatingInvoke()
    {
        if (repeatingInvoke != null)
        {
            StopCoroutine(repeatingInvoke);
            repeatingInvoke = null;
        }
    }

    // Coroutine pour l'invocation répétée
    IEnumerator RepeatInvoke(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Appeler la fonction InvokeInQueue
            InvokeInQueue();

            // Sortir de la coroutine si l'interval a changé
            if (interval != entryInterval)
            {
                break;
            }
        }

        // Redémarrer l'invocation répétée avec le nouvel intervalle
        StartRepeatingInvoke(entryInterval);
    }
}
