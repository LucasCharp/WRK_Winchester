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
    public Canvas playCanvas;
    public AudioClip[] son;

    public int barLevel = 0;
    private bool isPaused;

    public AudioClip pauseMusic;
    public AudioClip pauseAmbience;

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

    
    public void SetEtagere()
    {


        if (barLevel == 0)
        {
            SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);

            barLevel = 1;
            etagereFull.SetActive(false); 
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(true);
            etagereEmpty.SetActive(false);
        }
        else if (barLevel == 1)
        {
            SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);

            barLevel = 2;
            etagereFull.SetActive(false);
            etagereHalf.SetActive(true);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
        }
        else if (barLevel == 2)
        {
            SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);

            barLevel = 3;
            etagereFull.SetActive(true);
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
        }
    }

    public void OnStartCliqued()
    {
        SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);

        startGame = true;

        startCanvas.gameObject.SetActive(false);
        playCanvas.gameObject.SetActive(true);
    }

    public void OnPauseCliqued()
    {

        if (isPaused == false)
        {
            if (startGame == false)
            {
                SFXManager.instance.PlaySoundFXClip(son[2], transform, 1f);

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
                SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);

                startCanvas.gameObject.SetActive(true);
            }
            pauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

}
