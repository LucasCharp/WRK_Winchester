using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    private int waitTime;
    public GameObject[] lightsToOff;
    public bool lightsBackOn = false;
    public MainSceneManager mainSceneManager;
    private bool hasLaunched;

    IEnumerator TurnOffLights()
    {
        waitTime = Random.Range(20, 60);
        // Attendre pendant le temps spécifié
        yield return new WaitForSeconds(waitTime);

        // Boucler à travers tous les GameObjects des lumières dans la liste
        foreach (GameObject lightObject in lightsToOff)
        {
            // Désactiver le GameObject de la lumière
            lightObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(mainSceneManager.startGame == true && hasLaunched == false)
        {
            StartCoroutine(TurnOffLights());
            hasLaunched = true;
        }
        if (lightsBackOn == true)
        {
            foreach (GameObject lightObject in lightsToOff)
            {
                lightObject.SetActive(true);
                StartCoroutine(TurnOffLights());
                lightsBackOn = false;
            }
        }
    }
}