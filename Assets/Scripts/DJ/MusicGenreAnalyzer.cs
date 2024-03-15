using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public MusicGenrePointCollector pointCollector;
    int maxPoints = 0;
    string resultString = "Résultats :\n\n";
    private static List<string> favoriteGenres;
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
        reloadFavoriteGenres(totalPoints);

        DisplayResult(totalPoints);
    }

    public void reloadFavoriteGenres(Dictionary<string, int> totalPoints)
    {
        favoriteGenres = new List<string>();

        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                maxPoints = pair.Value;
                favoriteGenres.Clear();
                favoriteGenres.Add(pair.Key);
            }
            else if (pair.Value == maxPoints)
            {
                favoriteGenres.Add(pair.Key);
            }
        }
    }
    public static List<string> getFavoriteGenres()
    {
        return favoriteGenres;
    }

    public void DisplayResult(Dictionary<string, int> totalPoints)
    {
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
        }

        resultString += "\nLes genres musicaux préférés sont :";
        foreach (string genre in favoriteGenres)
        {
            resultString += "\n- " + genre;
        }
        resultText.text = resultString;
    }
}