using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public CameraRotation cameraRotation;
    public CameraColliderDetection cameraCollider;
    public JukeboxManager jukeboxManager;
    public ElectricityManager electricityManager;
    public AmbianceUpgradeManager ambianceManager;
    public SpawnManager spawnManager;
    public Animator djAnimator;
    public VictoryManager victoryManager;
    public GameManager gameManager;
    public TextMeshProUGUI scoreFinalText;


    public Canvas victoryCanvas;
    public List<Canvas> canvasToHide;

    public int drinkCount = 0;
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

    public void RemoveDrink()
    {
        drinkCount = drinkCount - 1;
        if (drinkCount == 0)
        {
            barLevel = 0;
            etagereFull.SetActive(false);
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(true);
        }
        if (drinkCount >0 && drinkCount <= 5)
        {
            barLevel = 1;
            etagereFull.SetActive(false);
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(true);
            etagereEmpty.SetActive(false);
        }
        if (drinkCount <= 15 && drinkCount >5)
        {
            barLevel = 2;
            etagereFull.SetActive(false);
            etagereHalf.SetActive(true);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
        }
        if (drinkCount <= 30 && drinkCount >15)
        {
            barLevel = 3;
            etagereFull.SetActive(true);
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
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
            drinkCount = 5;
        }
        else if (barLevel == 1)
        {
            SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);

            barLevel = 2;
            etagereFull.SetActive(false);
            etagereHalf.SetActive(true);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
            drinkCount = 15;
        }
        else if (barLevel == 2)
        {
            SFXManager.instance.PlaySoundFXClip(son[4], transform, 1f);

            barLevel = 3;
            etagereFull.SetActive(true);
            etagereHalf.SetActive(false);
            etagereQuarter.SetActive(false);
            etagereEmpty.SetActive(false);
            drinkCount = 30;
        }
    }

    public void OnStartCliqued()
    {
        SFXManager.instance.PlaySoundFXClip(son[1], transform, 1f);
        cameraRotation.canRotate = true;
        startGame = true;

        playCanvas.gameObject.SetActive(true);
        startCanvas.gameObject.SetActive(false) ;


        djAnimator.SetBool("isDancing", true);
        StartCoroutine(EndGameTimer());
    }


    IEnumerator EndGameTimer()
    {
        yield return new WaitForSeconds(360);
        spawnManager.DestroySpawner();
        yield return new WaitForSeconds(60);
        Time.timeScale = 0f;
        victoryManager.PlayVictorySound();
        victoryCanvas.gameObject.SetActive(true);
        scoreFinalText.text = gameManager.score.ToString();
        foreach (Canvas canvas in canvasToHide)
        {
            canvas.gameObject.SetActive(false); 
        }
        
   
    }

    public void OnPauseCliqued()
    {
        if (isPaused == false)
        {
            SFXManager.instance.PlaySoundFXClip(son[2], transform, 1f);

            if (electricityManager.electricityCut == false)
            {
                jukeboxManager.TogglePause();
            }
            if (startGame == false)
            {
                startCanvas.gameObject.SetActive(false);
                
                ambianceManager.TogglePause();

            }
            pauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            cameraRotation.canRotate = false;
            if (cameraCollider.visibleUI == "Videur")
            {
                cameraCollider.videurBox.SetActive(false);
            }
            else if (cameraCollider.visibleUI == "DJ")
            {
                cameraCollider.djBox.SetActive(false);
            }
            else if (cameraCollider.visibleUI == "Bar")
            {
                cameraCollider.barBox.SetActive(false);
            }
        }



       else if (isPaused == true)
        {
            SFXManager.instance.PlaySoundFXClip(son[3], transform, 1f);
            if (electricityManager.electricityCut == false)
            {
                jukeboxManager.TogglePause();
            }
            if (startGame == false)
            {
                startCanvas.gameObject.SetActive(true);
                ambianceManager.TogglePause();
            }
            pauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            cameraRotation.canRotate = true;

            if (cameraCollider.visibleUI == "Videur")
            {
                cameraCollider.videurBox.SetActive(true);
            }
            else if (cameraCollider.visibleUI == "DJ")
            {
                cameraCollider.djBox.SetActive(true);
            }
            else if (cameraCollider.visibleUI == "Bar")
            {
                cameraCollider.barBox.SetActive(true);
            }
        }
    }

    public void OnQuitterCliqued()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}