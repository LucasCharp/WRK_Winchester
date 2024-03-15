using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int moneyChange;
    public int moneyTotal;
    public TextMeshProUGUI moneyTextStart;
    public TextMeshProUGUI moneyTextPlay;

    // Start is called before the first frame update
    void Start()
    {
        moneyTotal = 1500;
        OnMoneyChange();
    }

    public void OnMoneyChange()
    {
        moneyTotal = moneyTotal + moneyChange;
        moneyTextStart.text = moneyTotal.ToString();
        moneyTextPlay.text = moneyTotal.ToString();
    }
}
