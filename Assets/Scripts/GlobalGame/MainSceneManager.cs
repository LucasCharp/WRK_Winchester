using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public bool startGame;
    public GameObject etagereFull;
    public GameObject etagereHalf;
    public GameObject etagereQuarter;
    public GameObject etagereEmpty;
    public List<Canvas> canvasList;
    public Canvas startCanvas;
    public Canvas pauseCanvas;
    public AudioClip son;

    private int barLevel = 0;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
        etagereFull.SetActive(false); // rend les étagères invisibles, sauf la empty (en prévision du tuto)
        etagereHalf.SetActive(false);
        etagereQuarter.SetActive(false);
        etagereEmpty.SetActive(true);

        pauseCanvas.gameObject.SetActive(false);
        foreach (Canvas canvas in canvasList)
        {
            canvas.gameObject.SetActive(false); 
        }
    }

    private void Update()
    {
        if (startGame == true)
        {
            foreach (Canvas canvas in canvasList)
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }
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

    public void OnStartCliqued()
    {
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        startGame = true;

        startCanvas.gameObject.SetActive(false);
    }

    public void OnPauseCliqued()
    {
        
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        if (isPaused == false)
        {
            if (startGame == false)
            {
                startCanvas.gameObject.SetActive(false);
            }
            pauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
       else if (isPaused == true)
        {
            if (startGame == false)
            {
                startCanvas.gameObject.SetActive(true);
            }
            pauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

}
