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

    void StartRepeatingInvoke(float interval)
    {
        // Arr�ter l'invocation r�p�t�e actuelle, s'il y en a une
        StopRepeatingInvoke();

        if (interval > 0)
        {
            // D�marrer une nouvelle invocation r�p�t�e avec l'intervalle donn�
            repeatingInvoke = StartCoroutine(RepeatInvoke(interval));
        }
    }

    // Fonction pour arr�ter l'invocation r�p�t�e
    void StopRepeatingInvoke()
    {
        if (repeatingInvoke != null)
        {
            StopCoroutine(repeatingInvoke);
            repeatingInvoke = null;
        }
    }

    // Coroutine pour l'invocation r�p�t�e
    IEnumerator RepeatInvoke(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Appeler la fonction InvokeInQueue
            InvokeInQueue();

            // Sortir de la coroutine si l'interval a chang�
            if (interval != entryInterval)
            {
                break;
            }
        }

        // Red�marrer l'invocation r�p�t�e avec le nouvel intervalle
        StartRepeatingInvoke(entryInterval);
    }
}
