using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class ButtonGenre : MonoBehaviour
{
    public string genreName; // Le nom du genre musical associ� � ce bouton
    public MusicGenrePointCollector pointCollector; // R�f�rence au script de collecte de points
    public GameManager gameManager; // R�f�rence au gestionnaire de jeu pour le score
    public static string selectedGenre;


    private void Start()
    {
        genreName = genreName.ToString();
        // Trouver la r�f�rence du MusicGenrePointCollector si elle n'est pas d�j� d�finie
        if (pointCollector == null)
        {
            pointCollector = FindObjectOfType<MusicGenrePointCollector>();
            if (pointCollector == null)
            {
                Debug.LogError("MusicGenrePointCollector non trouv� !");
            }
        }

        // Trouver la r�f�rence du GameManager si elle n'est pas d�j� d�finie
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager non trouv� !");
            }
        }
    Button btn = GetComponent <Button>();
        btn.onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        selectedGenre = genreName;
        Debug.Log(selectedGenre);
    }

    public static string getSelectedGenre()
    { return selectedGenre; }
        
}
