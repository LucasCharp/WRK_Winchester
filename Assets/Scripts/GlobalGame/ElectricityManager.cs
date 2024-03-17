using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    private int waitTime;
    private bool hasLaunched;
    private int cost = 200;

    public GameObject[] lightsToOff;
    public bool lightsBackOn = false;
    public MainSceneManager mainSceneManager;
    public MoneyManager moneyManager;
    public List<GameObject> redLights;
    public Collider boxCollider;

    private void Start()
    {
        foreach (GameObject light in redLights)
        {
            light.gameObject.SetActive(false);
        }
        boxCollider.enabled = false;
    }


    private void OnMouseDown()
    {
         if (moneyManager.moneyTotal >= cost)
         {
            moneyManager.moneyChange = -cost;
            moneyManager.OnMoneyChange();
            lightsBackOn = true;
            foreach (GameObject light in redLights)
            {
                light.gameObject.SetActive(false);
            }
            boxCollider.enabled = false;
        }
    }

    IEnumerator TurnOffLights() //éteind les lights au bout d'un temps random
    {
        waitTime = Random.Range(20, 30);
        yield return new WaitForSeconds(waitTime);
        foreach (GameObject light in redLights)
        {
            light.gameObject.SetActive(true);
        }
        boxCollider.enabled = true;
        foreach (GameObject lightObject in lightsToOff)
        {
            lightObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (mainSceneManager.startGame == true && hasLaunched == false) // ne se joue q"une fois, pour lancer la coupure dès que le startGame est en vrai
        {
            StartCoroutine(TurnOffLights());
            hasLaunched = true;
        }
        if (lightsBackOn == true)// allume les lights dès que la variable lightsBackOn est vraie, puis relance la coroutine
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