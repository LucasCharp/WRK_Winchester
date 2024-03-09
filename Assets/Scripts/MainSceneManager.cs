using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public bool startGame;
    public GameObject etagereFull;
    public GameObject etagereHalf;
    public GameObject etagereQuarter;
    public GameObject etagereEmpty;
    private int barLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
        etagereFull.SetActive(false); // rend les étagères invisibles, sauf la empty (en prévision du tuto)
        etagereHalf.SetActive(false);
        etagereQuarter.SetActive(false);
        etagereEmpty.SetActive(true);
    }

    // Update is called once per frame
    public void SetEtagere()
    {
        if (barLevel == 0)
        {
            barLevel = 1;
            etagereFull.SetActive(false); 
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(true);
            etagereEmpty.SetActive(false);
        }
        else if (barLevel == 1)
        {
            barLevel = 2;
            etagereFull.SetActive(false);
            etagereHalf.SetActive(true);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
        }
        else if (barLevel == 2)
        {
            barLevel = 3;
            etagereFull.SetActive(true);
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
        }
    }
}
