using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class MusicGenrePointCollector : MonoBehaviour
{
    public GameObject PNJ;
    public MusicGenreAnalyzer scriptAffiche;
    public Collider triggerCollider;
    public List<GameObject> genreButtons; // Liste des boutons de genre musical
    private Dictionary<Collider, CharacterMusicCounter> pnjCounters = new Dictionary<Collider, CharacterMusicCounter>();
    public Slider SliderAfro;
    public Slider SliderDrill;
    public Slider SliderCountry;
    public Slider SliderJazz;
    public Slider SliderBrazil;
    public Slider SliderRock;
    public Slider SliderDisco;
    public Slider SliderMetal;
    private Dictionary<string, float> genreValues = new Dictionary<string, float>(); // Déclaration et initialisation de genreValues
    private float totalPoints = 0; // Variable pour stocker le total des points

    void Start()
    {
        // Initialisez les valeurs des genres à 0
        genreValues["Afro"] = 0;
        genreValues["Drill"] = 0;
        genreValues["Jazz"] = 0;
        genreValues["Country"] = 0;
        genreValues["Brazil"] = 0;
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
        Dictionary<string, float> totalPoints = CalculateTotalPoints();
        scriptAffiche.reloadFavoriteGenres(totalPoints);
        scriptAffiche.DisplayResult(totalPoints);
    }

    public Dictionary<string, float> CalculateTotalPoints()
    {
        // Réinitialiser les valeurs des genres à 0
        foreach (var genre in genreValues.Keys.ToList())
        {
            genreValues[genre] = 0;
        }

        // Réinitialiser le total des points à 0
        totalPoints = 0;

        // Parcourir chaque PNJ pour collecter les données sur les genres musicaux
        foreach (var pair in pnjCounters)
        {
            // Ajouter les données de chaque PNJ aux valeurs des genres
            genreValues["Afro"] += pair.Value.afroCounter;
            genreValues["Drill"] += pair.Value.drillCounter;
            genreValues["Jazz"] += pair.Value.jazzCounter;
            genreValues["Country"] += pair.Value.countryCounter;
            genreValues["Brazil"] += pair.Value.brazilCounter;
            genreValues["Rock"] += pair.Value.rockCounter;
            genreValues["Disco"] += pair.Value.discoCounter;
            genreValues["Metal"] += pair.Value.metalCounter;

            // Incrémenter le total des points
            totalPoints += pair.Value.afroCounter + pair.Value.drillCounter + pair.Value.jazzCounter +
                           pair.Value.countryCounter + pair.Value.brazilCounter + pair.Value.rockCounter +
                           pair.Value.discoCounter + pair.Value.metalCounter;
        }
        // Retourner un nouveau dictionnaire avec les valeurs mises à jour de genreValues
        return new Dictionary<string, float>(genreValues);
    }

    public void CalculateAndSetPercentages()
    {
        foreach (KeyValuePair<string, float> genre in genreValues)
        {
            float percentage = totalPoints > 0 ? (genre.Value / (float)totalPoints) * 100f : 0f;
            SetSliderValue(genre.Key, percentage);
        }
    }
    private void SetSliderValue(string genreName, float percentage)
    {
        Debug.Log(percentage + "%");
        switch (genreName)
        {
            case "Afro":
                SliderAfro.value = percentage / 100;
                break;
            case "Drill":
                SliderDrill.value = percentage / 100;
                break;
            case "Jazz":
                SliderJazz.value = percentage / 100;
                break;
            case "Country":
                SliderCountry.value = percentage / 100;
                break;
            case "Brazil":
                SliderBrazil.value = percentage / 100;
                break;
            case "Rock":
                SliderRock.value = percentage / 100;
                break;
            case "Disco":
                SliderDisco.value = percentage / 100;
                break;
            case "Metal":
                SliderMetal.value = percentage / 100;
                break;
            default:
                Debug.LogWarning("Genre musical non reconnu : " + genreName);
                break;
        }
    }
}