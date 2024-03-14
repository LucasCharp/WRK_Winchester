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
        // Attendre pendant le temps sp�cifi�
        yield return new WaitForSeconds(waitTime);

        // Boucler � travers tous les GameObjects des lumi�res dans la liste
        foreach (GameObject lightObject in lightsToOff)
        {
            // D�sactiver le GameObject de la lumi�re
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