using UnityEngine;
using UnityEngine.UI;

public class BarmanController : MonoBehaviour
{
    public GameObject menuPanel;

    private void Start()
    {
        menuPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        menuPanel.SetActive(true);
    }

    public void VendreBoisson(string nomBoisson)
    {
        // Code pour vendre la boisson et marquer des points
        Debug.Log("Boisson vendue : " + nomBoisson);
        menuPanel.SetActive(false);
    }
}