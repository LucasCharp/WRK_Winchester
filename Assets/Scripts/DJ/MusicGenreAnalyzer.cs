using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public MusicGenrePointCollector pointCollector;

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
        string[] favoriteGenres = GetFavoriteGenres(totalPoints);

        DisplayResult(totalPoints, favoriteGenres);
    }

    public string[] GetFavoriteGenres(Dictionary<string, int> totalPoints)
    {
        List<string> favoriteGenres = new List<string>();
        int maxPoints = 0;

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

        return favoriteGenres.ToArray();
    }

    public void DisplayResult(Dictionary<string, int> totalPoints, string[] favoriteGenres)
    {
        string resultString = "Résultats :\n\n";
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