using UnityEngine;
using System.Collections.Generic;
using TMPro;

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
        if (favoriteGenres == null || favoriteGenres.Count == 0) // Ajout d'une vérification pour ne mettre à jour les genres préférés qu'une seule fois
        {
            reloadFavoriteGenres(totalPoints);
        }

        DisplayResult(totalPoints);
    }

    public void reloadFavoriteGenres(Dictionary<string, int> totalPoints)
    {
        maxPoints = 0; // Réinitialiser maxPoints

        // Trouver le nombre maximal de points
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                maxPoints = pair.Value;
                //Debug.Log(maxPoints);
            }
        }

        // Réinitialiser la liste des genres préférés
        favoriteGenres = new List<string>();

        // Ajouter tous les genres ayant le nombre maximal de points à la liste des genres préférés
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
        resultString = "Résultats :\n\n"; // Réinitialiser la chaîne de résultat

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