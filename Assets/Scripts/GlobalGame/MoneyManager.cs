using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public float moneyChange;
    public float moneyTotal;
    public TextMeshProUGUI moneyTextStart;
    public TextMeshProUGUI moneyTextPlay;
    public float Multiplicatir = 1f;
    public UpgradeButtons UpdateButtons;
    public static MoneyManager instance;

    // Start is called before the first frame update
    void Start()
    {
        moneyTotal = 1500;
        OnMoneyChange();
    }
    public void AddMultiplicato()
    {
        Multiplicatir += 0.1f;
        Debug.Log(Multiplicatir);
    }
    public void OnMoneyChange()
    {
        Debug.Log(moneyChange);
        if (moneyChange > 0)
        {
            moneyTotal = moneyTotal + moneyChange * Multiplicatir;
            //moneyTextPlay.text = moneyTotal.ToString();
        }
        else
        {
            moneyTotal = moneyTotal + moneyChange;
            moneyTextStart.text = moneyTotal.ToString();
            moneyTextPlay.text = moneyTotal.ToString();
        }
    }
}
