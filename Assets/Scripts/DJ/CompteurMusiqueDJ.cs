using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public Text resultText; // Référence au texte dans le jeu pour afficher les résultats

    // Fonction pour analyser les goûts musicaux des PNJ et déterminer le genre préféré
    public void AnalyzeMusicGenres(Dictionary<string, int> totalPoints)
    {
        // Déterminer le genre musical avec le plus de points
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

        // Construire le texte à afficher dans le jeu
        string result = "Genre musical préféré des PNJ : " + favoriteGenre + "\n\n";
        result += "Répartition des points :\n";
        foreach (var pair in totalPoints)
        {
            result += pair.Key + " : " + pair.Value + "\n";
        }

        // Afficher le texte dans le jeu
        resultText.text = result;
    }
}