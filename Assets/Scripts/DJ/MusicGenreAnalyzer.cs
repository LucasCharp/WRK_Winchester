using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MusicGenreAnalyzer : MonoBehaviour
{
    public TextMeshProUGUI resultText; // R�f�rence au texte o� afficher le r�sultat
    public MusicGenrePointCollector pointCollector; // R�f�rence au script de collecte de points

    void Start()
    {
        // Trouver le script de collecte de points dans la sc�ne
        pointCollector = FindObjectOfType<MusicGenrePointCollector>();

        // V�rifier si le script de collecte de points a �t� trouv�
        if (pointCollector == null)
        {
            Debug.LogError("Le script de collecte de points n'a pas �t� trouv� !");
            Debug.LogError("VoirMusicGenreAnalyzer");
            return;
        }

        // Calculer le total des points pour chaque genre musical
        Dictionary<string, int> totalPoints = pointCollector.CalculateTotalPoints();

        // D�terminer le genre musical pr�f�r� (celui avec le plus de points)
        string favoriteGenre = GetFavoriteGenre(totalPoints);

        // Afficher le r�sultat dans le texte
        DisplayResult(totalPoints, favoriteGenre);
    }

    // Fonction pour d�terminer le genre musical pr�f�r�
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

    // Fonction pour afficher le r�sultat dans le texte
    public void DisplayResult(Dictionary<string, int> totalPoints, string favoriteGenre)
    {
        // Cr�er une cha�ne de texte contenant les r�sultats
        string resultString = "R�sultats :\n\n";
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            resultString += pair.Key + " : " + pair.Value + " points\n";
        }

        resultString += "\nLe genre musical pr�f�r� est : " + favoriteGenre;

        // Afficher le texte dans l'UI
        resultText.text = resultString;
    }
}