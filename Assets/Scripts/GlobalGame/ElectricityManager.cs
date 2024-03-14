using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    private int waitTime;
    public GameObject[] lightsToOff;
    public bool lightsBackOn = false;

    private void Start()
    {
        waitTime = Random.Range(20, 60);
        StartCoroutine(TurnOffLights());
    }

    IEnumerator TurnOffLights()
    {
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