using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText; // Référence au texte où afficher le résultat
    public MusicGenrePointCollector pointCollector; // Référence au script de collecte de points

    void Start()
    {
        // Trouver le script de collecte de points dans la scène
        pointCollector = FindObjectOfType<MusicGenrePointCollector>();

        // Vérifier si le script de collecte de points a été trouvé
        if (pointCollector == null)
        {
            Debug.LogError("Le script de collecte de points n'a pas été trouvé !");
            Debug.LogError("VoirMusicGenreAnalyzer");
            return;
        }

        // Calculer le total des points pour chaque genre musical
        Dictionary<string, int> totalPoints = pointCollector.CalculateTotalPoints();

        // Déterminer le genre musical préféré (celui avec le plus de points)
        string favoriteGenre = GetFavoriteGenre(totalPoints);

        // Afficher le résultat dans le texte
        DisplayResult(totalPoints, favoriteGenre);
    }

    // Fonction pour déterminer le genre musical préféré
    public string GetFavoriteGenre(Dictionary<string, int> totalPoints)
    {
        string favoriteGenre = "";
        int maxPoints = 0;

        // Parcourir chaque genre musical pour trouver celui avec le plus de points
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                maxPoints = pair.Value;
                favoriteGenre = pair.Key;
            }
        }

        return favoriteGenre;
    }

    // Fonction pour afficher le résultat dans le texte
    public void DisplayResult(Dictionary<string, int> totalPoints, string favoriteGenre)
    {
        // Créer une chaîne de texte contenant les résultats
        string resultString = "Résultats :\n\n";
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
        }

        resultString += "\nLe genre musical préféré est : " + favoriteGenre;

        // Afficher le texte dans l'UI
        resultText.text = resultString;
    }
}