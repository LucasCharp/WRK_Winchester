using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public MusicGenrePointCollector pointCollector;
    float maxPoints = 0;
    string resultString = "Résultats :\n\n";
    private static List<string> favoriteGenres;
    private KeyValuePair<string, float> ValueGenre; // Déclaration de ValueGenre
    void Start()
    {
        pointCollector = FindObjectOfType<MusicGenrePointCollector>();
        if (pointCollector == null)
        {
            Debug.LogError("Le script de collecte de points n'a pas été trouvé !");
            Debug.LogError("VoirMusicGenreAnalyzer");
            return;
        }

        Dictionary<string, float> totalPoints = pointCollector.CalculateTotalPoints();
        if (favoriteGenres == null || favoriteGenres.Count == 0)
        {
            reloadFavoriteGenres(totalPoints);
        }
        DisplayResult(totalPoints);
    }

    public void reloadFavoriteGenres(Dictionary<string, float> totalPoints)
    {
        maxPoints = 0;
        favoriteGenres = new List<string>();

        foreach (KeyValuePair<string, float> pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                maxPoints = pair.Value;
            }
        }

        foreach (KeyValuePair<string, float> pair in totalPoints)
        {
            if (pair.Value == maxPoints)
            {
                favoriteGenres.Add(pair.Key);
            }
        }
    }
    public static List<string> getFavoriteGenres()
    {
        return favoriteGenres;
    }

    public void DisplayResult(Dictionary<string, float> totalPoints)
    {
        resultString = "Résultats :\n\n"; // Réinitialiser la chaîne de résultat
        float totalValue = 0; // Variable pour stocker la somme des valeurs de genre

        foreach (KeyValuePair<string, float> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
            totalValue += pair.Value; // Ajouter la valeur du genre à la somme totale
            ValueGenre = pair;

            if (ValueGenre.Value != 0)
            {
                // Appeler la méthode pour calculer le pourcentage de chaque musique préférée
                pointCollector.CalculateAndSetPercentages(); // Appel de la méthode ici
            }
        }

        resultString += "\nLes genres musicaux préférés sont :";
        foreach (string genre in favoriteGenres)
        {
            resultString += "\n- " + genre;
        }
        resultText.text = resultString;
    }
}