using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class ButtonGenre : MonoBehaviour
{
    public string genreName; // Le nom du genre musical associé à ce bouton
    public MusicGenrePointCollector pointCollector; // Référence au script de collecte de points
    public GameManager gameManager; // Référence au gestionnaire de jeu pour le score
    public static string selectedGenre;


    private void Start()
    {
        genreName = genreName.ToString();
        // Trouver la référence du MusicGenrePointCollector si elle n'est pas déjà définie
        if (pointCollector == null)
        {
            pointCollector = FindObjectOfType<MusicGenrePointCollector>();
            if (pointCollector == null)
            {
                Debug.LogError("MusicGenrePointCollector non trouvé !");
            }
        }

        // Trouver la référence du GameManager si elle n'est pas déjà définie
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager non trouvé !");
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
