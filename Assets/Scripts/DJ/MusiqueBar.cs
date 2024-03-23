using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MusiqueBar : MonoBehaviour
{
    public Slider SliderAfro;
    public Slider SliderDrill;
    public Slider SliderCountry;
    public Slider SliderJazz;
    public Slider SliderBrazil;
    public Slider SliderRock;
    public Slider SliderDisco;
    public Slider SliderMetal;
    public MusicGenreAnalyzer MesInfos;
    private Dictionary<string, float> genreValues = new Dictionary<string, float>(); // Déclaration et initialisation de genreValues
    private int totalGenreCount = 0;


    void Start()
    {
        totalGenreCount = MusicGenreAnalyzer.getFavoriteGenres().Count; // Obtenir le nombre total de genres musicaux
                                                                        // Afficher les valeurs de genreValues
        foreach (var pair in genreValues)
        {
            Debug.Log("Genre: " + pair.Key + ", Value: " + pair.Value);
        }

        // Vérifier la valeur de totalGenreCount
        Debug.Log("Total Genre Count: " + totalGenreCount);
    }
    public void CalculateAndSetPercentages()
    {
        // Boucle à travers chaque paire clé-valeur dans le dictionnaire
        foreach (KeyValuePair<string, float> genre in genreValues)
        {
            // Récupérez la clé et la valeur actuelles
            string genreName = genre.Key;
            float value = genre.Value;

            // Affichez les données pour déboguer
            Debug.Log("Genre: " + genreName + ", Value: " + value);

            // Calculez le pourcentage et définissez la valeur du slider
            float percentage = totalGenreCount > 0 ? (value / (float)totalGenreCount) * 100f : 0f;
            SetSliderValue(genreName, percentage);
        }
    }


    private void SetSliderValue(string genreName, float percentage)
    {
        Debug.Log("oui2");
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
            case "Brasil":
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