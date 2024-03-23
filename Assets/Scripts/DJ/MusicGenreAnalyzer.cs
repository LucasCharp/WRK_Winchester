using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public MusicGenrePointCollector pointCollector;
    int maxPoints = 0;
    string resultString = "Résultats :\n\n";
    private static List<string> favoriteGenres;
    private int totalGenreCount = 0; // Nouvelle variable pour stocker le nombre total de genres musicaux
    public MusiqueBar Info;
    private KeyValuePair<string, int> ValueGenre; // Déclaration de ValueGenre
    void Start()
    {
        pointCollector = FindObjectOfType<MusicGenrePointCollector>();
        if (pointCollector == null)
        {
            Debug.LogError("Le script de collecte de points n'a pas été trouvé !");
            Debug.LogError("VoirMusicGenreAnalyzer");
            return;
        }

        Dictionary<string, int> totalPoints = pointCollector.CalculateTotalPoints();
        if (favoriteGenres == null || favoriteGenres.Count == 0)
        {
            reloadFavoriteGenres(totalPoints);
        }

        // Calculer le nombre total de genres musicaux présents dans la zone de danse
        totalGenreCount = CalculateTotalGenreCount(totalPoints);

        DisplayResult(totalPoints);
    }

    public void reloadFavoriteGenres(Dictionary<string, int> totalPoints)
    {
        maxPoints = 0;
        favoriteGenres = new List<string>();

        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                maxPoints = pair.Value;
            }
        }

        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            if (pair.Value == maxPoints)
            {
                favoriteGenres.Add(pair.Key);
            }
        }
    }

    private int CalculateTotalGenreCount(Dictionary<string, int> totalPoints)
    {
        int count = 0;
        foreach (var pair in totalPoints)
        {
            count += pair.Value; // Ajouter le nombre de genres de chaque type
        }
        return count;
    }

    public static List<string> getFavoriteGenres()
    {
        return favoriteGenres;
    }

    public void DisplayResult(Dictionary<string, int> totalPoints)
    {
        resultString = "Résultats :\n\n"; // Réinitialiser la chaîne de résultat
        int totalValue = 0; // Variable pour stocker la somme des valeurs de genre

        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
            totalValue += pair.Value; // Ajouter la valeur du genre à la somme totale
            ValueGenre = pair;

            if (ValueGenre.Value != 0)
            {
                // Appeler la méthode pour calculer le pourcentage de chaque musique préférée
                Info.CalculateAndSetPercentages(); // Appel de la méthode ici
                Debug.Log("appel clean");
            }
        }

        resultString += "\nLes genres musicaux préférés sont :";
        foreach (string genre in favoriteGenres)
        {
            resultString += "\n- " + genre;
        }
        resultText.text = resultString;
        Debug.Log("display end");
    }
}