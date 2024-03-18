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
        if (mainSceneManager.startGame == true && hasLaunched == false) // ne se joue q"une fois, pour lancer la coupure dès que le startGame est en vrai
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
        yield return new WaitForSeconds(preventTime);//attends avant de lancer la fonction qui éteint les lumières
        StopElectricity();
    }

    private void StopElectricity() // vérifie si on peut éteindre les lumières, et si oui alors piouf on éteint tout
    { 
        if (canStop == true)
        {
            
            Debug.Log("Je suis le temps complet et j'éteins tout");
            foreach (GameObject lightObject in lightsToOff)
            {
                lightObject.SetActive(false);
            }
            RotateLight[] scripts = FindObjectsOfType<RotateLight>();
            foreach (RotateLight script in scripts)
            {
                script.canRotate = false;
                script.rotating = false;
            }
            jukeboxManager.gameObject.SetActive(false);
            electricityCut = true;
            ChangeColor[] colors = FindObjectsOfType<ChangeColor>();
            foreach (ChangeColor color in colors)
            {
                color.canChangeColor = false;
            }
            ChangeColor_2[] colors_2 = FindObjectsOfType<ChangeColor_2>();
            foreach (ChangeColor_2 color in colors_2)
            {
                color.canChangeColor = false;
            }
            
        }
    }

    private void LightsBackOn()
    {
        
            jukeboxManager.gameObject.SetActive(true);
        
        foreach (GameObject lightObject in lightsToOff)
        {
            lightObject.SetActive(true);
            electricityCut = false;
            StartCoroutine(TurnOnRed());
        }
        RotateLight[] scripts = FindObjectsOfType<RotateLight>();
        foreach (RotateLight script in scripts)
        {
            script.canRotate = true;
            script.StartRotating();
        }
        ChangeColor[] colors = FindObjectsOfType<ChangeColor>();
        foreach (ChangeColor color in colors)
        {
            color.canChangeColor = true;
        }
        ChangeColor_2[] colors_2 = FindObjectsOfType<ChangeColor_2>();
        foreach (ChangeColor_2 color in colors_2)
        {
            color.canChangeColor = true;
        }
    }
}
        