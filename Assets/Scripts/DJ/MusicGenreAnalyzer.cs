using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public MusicGenrePointCollector pointCollector;
    int maxPoints = 0;
    string resultString = "R�sultats :\n\n";
    private static List<string> favoriteGenres;
    void Start()
    {
        pointCollector = FindObjectOfType<MusicGenrePointCollector>();
        if (pointCollector == null)
        {
            Debug.LogError("Le script de collecte de points n'a pas �t� trouv� !");
            Debug.LogError("VoirMusicGenreAnalyzer");
            return;
        }

        Dictionary<string, int> totalPoints = pointCollector.CalculateTotalPoints();
        if (favoriteGenres == null || favoriteGenres.Count == 0) // Ajout d'une v�rification pour ne mettre � jour les genres pr�f�r�s qu'une seule fois
        {
            reloadFavoriteGenres(totalPoints);
        }

        DisplayResult(totalPoints);
    }

    public void reloadFavoriteGenres(Dictionary<string, int> totalPoints)
    {
        maxPoints = 0; // R�initialiser maxPoints

        // Trouver le nombre maximal de points
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                maxPoints = pair.Value;
                //Debug.Log(maxPoints);
            }
        }

        // R�initialiser la liste des genres pr�f�r�s
        favoriteGenres = new List<string>();

        // Ajouter tous les genres ayant le nombre maximal de points � la liste des genres pr�f�r�s
        foreach (KeyValuePair<string, int> pair in totalPoints)
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

    public void DisplayResult(Dictionary<string, int> totalPoints)
    {
        resultString = "R�sultats :\n\n"; // R�initialiser la cha�ne de r�sultat

        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
        }

        resultString += "\nLes genres musicaux pr�f�r�s sont :";
        foreach (string genre in favoriteGenres)
        {
            resultString += "\n- " + genre;
        }
        resultText.text = resultString;
    }
}