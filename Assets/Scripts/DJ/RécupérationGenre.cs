using UnityEngine;
using System.Collections.Generic;

public class MusicGenrePointCollector : MonoBehaviour
{
    // Dictionnaire pour stocker les compteurs de genres musicaux de chaque PNJ
    private Dictionary<Collider, CharacterMusicCounter> pnjCounters = new Dictionary<Collider, CharacterMusicCounter>();

    public MusicGenreAnalyzer scriptAffiche; // Assurez-vous d'assigner cette référence dans l'inspecteur Unity

    void OnTriggerEnter(Collider PNJ)
    {
        // Vérifier si le collider est un PNJ et s'il possède un composant CharacterMusicCounter
        CharacterMusicCounter musicCounter = PNJ.GetComponent<CharacterMusicCounter>();
        if (musicCounter != null)
        {
            // Ajouter le PNJ au dictionnaire s'il n'est pas déjà présent
            if (!pnjCounters.ContainsKey(PNJ))
            {
                pnjCounters.Add(PNJ, musicCounter);
                var totalPoints = CalculateTotalPoints();
                scriptAffiche.DisplayResult(totalPoints, scriptAffiche.GetFavoriteGenres(totalPoints));
            }
        }
    }

    void OnTriggerExit(Collider PNJ)
    {
        // Retirer le PNJ du dictionnaire lorsqu'il quitte la boîte de collision
        if (pnjCounters.ContainsKey(PNJ))
        {
            pnjCounters.Remove(PNJ);
            var totalPoints = CalculateTotalPoints();
            scriptAffiche.DisplayResult(totalPoints, scriptAffiche.GetFavoriteGenres(totalPoints));
        }
    }

    // Fonction pour calculer le total des points pour chaque genre musical parmi tous les PNJ présents
    public Dictionary<string, int> CalculateTotalPoints()
    {
        Dictionary<string, int> totalPoints = new Dictionary<string, int>();

        // Initialiser les compteurs totaux à zéro
        totalPoints["Rock"] = 0;
        totalPoints["Pop"] = 0;
        totalPoints["Jazz"] = 0;
        totalPoints["Electronic"] = 0;
        totalPoints["Classical"] = 0;

        // Parcourir tous les PNJ présents dans la boîte de collision et ajouter leurs points au total
        foreach (var pair in pnjCounters)
        {
            totalPoints["Rock"] += pair.Value.rockCounter;
            totalPoints["Pop"] += pair.Value.popCounter;
            totalPoints["Jazz"] += pair.Value.jazzCounter;
            totalPoints["Electronic"] += pair.Value.electronicCounter;
            totalPoints["Classical"] += pair.Value.classicalCounter;
        }

        return totalPoints;
    }
}