using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MusicGenrePointCollector : MonoBehaviour
{
    public GameObject PNJ;
    public MusicGenreAnalyzer scriptAffiche;
    public Collider triggerCollider;
    public List<GameObject> genreButtons; // Liste des boutons de genre musical
    private Dictionary<string, int> genreValues = new Dictionary<string, int>(); // Déclaration et initialisation de genreValues
    private Dictionary<Collider, CharacterMusicCounter> pnjCounters = new Dictionary<Collider, CharacterMusicCounter>();

    void Start()
    {
        // Initialisez les valeurs des genres à 0
        genreValues["Afro"] = 0;
        genreValues["Drill"] = 0;
        genreValues["Jazz"] = 0;
        genreValues["Country"] = 0;
        genreValues["Brasil"] = 0;
        genreValues["Rock"] = 0;
        genreValues["Disco"] = 0;
        genreValues["Metal"] = 0;
    }
    void OnTriggerEnter(Collider PNJ)
    {
        CharacterMusicCounter musicCounter = PNJ.GetComponent<CharacterMusicCounter>();
        Animator animator = PNJ.GetComponent<Animator>();
        animator.SetBool("willDance", true);
        if (musicCounter != null)
        {
            if (!pnjCounters.ContainsKey(PNJ))
            {
                pnjCounters.Add(PNJ, musicCounter);
                UpdateGenreAnalyzer();
            }
        }
    }

    void OnTriggerExit(Collider PNJ)
    {
        Animator animator = PNJ.GetComponent<Animator>();
        animator.SetBool("willDance", false);
        if (pnjCounters.ContainsKey(PNJ))
        {
            pnjCounters.Remove(PNJ);
            UpdateGenreAnalyzer();
        }        
    }

    void UpdateGenreAnalyzer()
    {
        Dictionary<string, int> totalPoints = CalculateTotalPoints();
        scriptAffiche.reloadFavoriteGenres(totalPoints);
        scriptAffiche.DisplayResult(totalPoints);
    }

    public Dictionary<string, int> CalculateTotalPoints()
 {
        // Réinitialiser les valeurs des genres à 0
        foreach (var genre in genreValues.Keys.ToList())
        {
            genreValues[genre] = 0;
        }

        // Parcourir chaque PNJ pour collecter les données sur les genres musicaux
        foreach (var pair in pnjCounters)
        {
            // Ajouter les données de chaque PNJ aux valeurs des genres
            genreValues["Afro"] += pair.Value.afroCounter;
            genreValues["Drill"] += pair.Value.drillCounter;
            genreValues["Jazz"] += pair.Value.jazzCounter;
            genreValues["Country"] += pair.Value.countryCounter;
            genreValues["Brasil"] += pair.Value.brasilCounter;
            genreValues["Rock"] += pair.Value.rockCounter;
            genreValues["Disco"] += pair.Value.discoCounter;
            genreValues["Metal"] += pair.Value.metalCounter;
        }

        // Afficher les valeurs de genreValues
        foreach (var genre in genreValues)
        {
            Debug.Log("Genre: " + genre.Key + ", Value: " + genre.Value);
        }

        // Retourner un nouveau dictionnaire avec les valeurs mises à jour de genreValues
        return new Dictionary<string, int>(genreValues);
    }

}