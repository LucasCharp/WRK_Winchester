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

        // Déterminer le(s) genre(s) musical(aux) préféré(s) (celui/celleux avec le plus de points)
        List<string> favoriteGenres = GetFavoriteGenres(totalPoints);

        // Afficher le résultat dans le texte
        DisplayResult(totalPoints, favoriteGenres);
    }

    // Fonction pour déterminer le(s) genre(s) musical(aux) préféré(s)
    public List<string> GetFavoriteGenres(Dictionary<string, int> totalPoints)
    {
        List<string> favoriteGenres = new List<string>();
        int maxPoints = 0;

        // Parcourir chaque genre musical pour trouver le(s) genre(s) avec le plus de points
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

        return favoriteGenres;
    }

    // Fonction pour afficher le résultat dans le texte
    public void DisplayResult(Dictionary<string, int> totalPoints, List<string> favoriteGenres)
    {
        // Créer une chaîne de texte contenant les résultats
        string resultString = "Résultats :\n\n";
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
        }

        // Ajouter les genres préférés à la chaîne de texte
        resultString += "\nGenre(s) musical(aux) préféré(s) :\n";
        foreach (string genre in favoriteGenres)
        {
            resultString += genre + "\n";
        }

        // Afficher le texte dans l'UI
        resultText.text = resultString;
    }
}