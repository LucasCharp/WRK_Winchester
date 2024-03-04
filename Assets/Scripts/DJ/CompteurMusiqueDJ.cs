using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public Text resultText; // R�f�rence au texte dans le jeu pour afficher les r�sultats

    // Fonction pour analyser les go�ts musicaux des PNJ et d�terminer le genre pr�f�r�
    public void AnalyzeMusicGenres(Dictionary<string, int> totalPoints)
    {
        // D�terminer le genre musical avec le plus de points
        string favoriteGenre = "";
        int maxPoints = 0;
        foreach (var pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                favoriteGenre = pair.Key;
                maxPoints = pair.Value;
            }
        }

        // Construire le texte � afficher dans le jeu
        string result = "Genre musical pr�f�r� des PNJ : " + favoriteGenre + "\n\n";
        result += "R�partition des points :\n";
        foreach (var pair in totalPoints)
        {
            result += pair.Key + " : " + pair.Value + "\n";
        }

        // Afficher le texte dans le jeu
        resultText.text = result;
    }
}