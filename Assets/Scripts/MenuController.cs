using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button retourPoubelle;
    public Button retourMenu;
    void Start()
    {
        retourPoubelle.gameObject.SetActive(false);
        retourMenu.gameObject.SetActive(false);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
