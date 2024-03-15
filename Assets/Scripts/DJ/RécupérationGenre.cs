using UnityEngine;
using System.Collections.Generic;

public class MusicGenrePointCollector : MonoBehaviour
{
    public GameObject PNJ;
    public MusicGenreAnalyzer scriptAffiche;
    public Collider triggerCollider;
    public List<GameObject> genreButtons; // Liste des boutons de genre musical
    private Dictionary<Collider, CharacterMusicCounter> pnjCounters = new Dictionary<Collider, CharacterMusicCounter>();

    void OnTriggerEnter(Collider PNJ)
    {
        CharacterMusicCounter musicCounter = PNJ.GetComponent<CharacterMusicCounter>();
        Animator animator = PNJ.GetComponent<Animator>();
        if (musicCounter != null)
        {
            if (!pnjCounters.ContainsKey(PNJ))
            {
                pnjCounters.Add(PNJ, musicCounter);
                var totalPoints = CalculateTotalPoints();
            }
        }
    }

    void OnTriggerExit(Collider PNJ)
    {
        Animator animator = PNJ.GetComponent<Animator>();
        if (pnjCounters.ContainsKey(PNJ))
        {
            pnjCounters.Remove(PNJ);
            var totalPoints = CalculateTotalPoints();
            //string favoriteGenre = GetFavoriteGenre(totalPoints);
            // Utilisez le genre préféré récupéré ici
        }
        animator.SetBool("willDance", false);
    }

    public Dictionary<string, int> CalculateTotalPoints()
    {
        Dictionary<string, int> totalPoints = new Dictionary<string, int>();

        // Ajouter les nouveaux genres musicaux
        totalPoints["Afro"] = 0;
        totalPoints["Drill"] = 0;
        totalPoints["Jazz"] = 0;
        totalPoints["Country"] = 0;
        totalPoints["Brasil"] = 0;
        totalPoints["Rock"] = 0;
        totalPoints["Disco"] = 0;
        totalPoints["Metal"] = 0;

        foreach (var pair in pnjCounters)
        {
            // Ajouter les nouveaux genres musicaux
            totalPoints["Afro"] += pair.Value.afroCounter;
            totalPoints["Drill"] += pair.Value.drillCounter;
            totalPoints["Jazz"] += pair.Value.jazzCounter;
            totalPoints["Country"] += pair.Value.countryCounter;
            totalPoints["Brasil"] += pair.Value.brasilCounter;
            totalPoints["Rock"] += pair.Value.rockCounter;
            totalPoints["Disco"] += pair.Value.discoCounter;
            totalPoints["Metal"] += pair.Value.metalCounter;
        }

        return totalPoints;
    }

}