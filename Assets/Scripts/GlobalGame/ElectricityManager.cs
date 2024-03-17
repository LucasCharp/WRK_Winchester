using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    private int waitTime = 20;
    private bool hasLaunched;
    private int cost = 200;
    private int preventTime = 10;
    private bool canStop = true;

    public bool electricityCut = false;
    public GameObject[] lightsToOff;
    public MainSceneManager mainSceneManager;
    public MoneyManager moneyManager;
    public List<GameObject> redLights;
    public Collider boxCollider;
    public JukeboxManager jukeboxManager;
    private void Start()
    {
        foreach (GameObject light in redLights)
        {
            light.gameObject.SetActive(false);
        }
        boxCollider.enabled = false;
    }
    private void Update()
    {
        if (mainSceneManager.startGame == true && hasLaunched == false) // ne se joue q"une fois, pour lancer la coupure d�s que le startGame est en vrai
        {
            StartCoroutine(TurnOnRed());
            hasLaunched = true;
        }
    }

    private void OnMouseDown()
    {
        if (moneyManager.moneyTotal >= cost)
        {
            moneyManager.moneyChange = -cost;
            moneyManager.OnMoneyChange();
            foreach (GameObject light in redLights)
            {
                light.gameObject.SetActive(false);
            }
            boxCollider.enabled = false;
            canStop = false;
            LightsBackOn();
        }
    }

    IEnumerator TurnOnRed() //allume les lights rouges au bout de waitTime
    {
        yield return new WaitForSeconds(waitTime);
        canStop = true;
        foreach (GameObject light in redLights)
        {
            light.gameObject.SetActive(true);
        }
        boxCollider.enabled = true;
        yield return new WaitForSeconds(preventTime);//attends avant de lancer la fonction qui �teint les lumi�res
        StopElectricity();
    }

    private void StopElectricity() // v�rifie si on peut �teindre les lumi�res, et si oui alors piouf on �teint tout
    { 
        if (canStop == true)
        {
            Debug.Log("Je suis le temps complet et j'�teins tout");
            foreach (GameObject lightObject in lightsToOff)
            {
                lightObject.SetActive(false);
            }
            jukeboxManager.TogglePause();
            electricityCut = true;
        }
    }

    private void LightsBackOn()
    {
        if (jukeboxManager.isPlaying == false)
        {
            jukeboxManager.TogglePause();
        }
        
        foreach (GameObject lightObject in lightsToOff)
        {
            lightObject.SetActive(true);
            electricityCut = false;
            StartCoroutine(TurnOnRed());
        }
    }
}
        