using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public MusicGenrePointCollector pointCollector;
    float maxPoints = 0;
    string resultString = "R�sultats :\n\n";
    private static List<string> favoriteGenres;
    private KeyValuePair<string, float> ValueGenre; // D�claration de ValueGenre
    void Start()
    {
        pointCollector = FindObjectOfType<MusicGenrePointCollector>();
        if (pointCollector == null)
        {
            Debug.LogError("Le script de collecte de points n'a pas �t� trouv� !");
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
        resultString = "R�sultats :\n\n"; // R�initialiser la cha�ne de r�sultat
        float totalValue = 0; // Variable pour stocker la somme des valeurs de genre

        foreach (KeyValuePair<string, float> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
            totalValue += pair.Value; // Ajouter la valeur du genre � la somme totale
            ValueGenre = pair;

            if (ValueGenre.Value != 0)
            {
                // Appeler la m�thode pour calculer le pourcentage de chaque musique pr�f�r�e
                pointCollector.CalculateAndSetPercentages(); // Appel de la m�thode ici
            }
        }

        resultString += "\nLes genres musicaux pr�f�r�s sont :";
        foreach (string genre in favoriteGenres)
        {
            resultString += "\n- " + genre;
        }
        resultText.text = resultString;
    }
}