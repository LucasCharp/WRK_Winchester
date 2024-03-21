using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

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
    private float TotalValueGenre = 0;
    private float ValueGenre = 0;
    private Dictionary<string, float> genreValues = new Dictionary<string, float>(); // Dictionnaire pour stocker les valeurs de chaque genre musical
    void Start()
    {
        // Initialisation des valeurs des genres musicaux
        genreValues["Afro"] = 0;
        genreValues["Drill"] = 0;
        genreValues["Jazz"] = 0;
        genreValues["Country"] = 0;
        genreValues["Brasil"] = 0;
        genreValues["Rock"] = 0;
        genreValues["Disco"] = 0;
        genreValues["Metal"] = 0;
    }
    public void InfoValueTotal()
    {
        if (MesInfos.TotalValueGenre > 0)
        {
            if (TotalValueGenre != MesInfos.TotalValueGenre)
            {
                TotalValueGenre = 0;
                
                TotalValueGenre += MesInfos.TotalValueGenre;
                Debug.Log("Valeur total" + TotalValueGenre);
            }
        }
    }

    public void InfoValueGenre()
    {
        if (MesInfos.ValueGenre.Value > 0)
        {
            ValueGenre = 0;
            ValueGenre += MesInfos.ValueGenre.Value;
            Debug.Log("Valeur par musique" + MesInfos.ValueGenre);
            genreValues["Afro"] = 0;
            genreValues["Drill"] = 0;
            genreValues["Jazz"] = 0;
            genreValues["Country"] = 0;
            genreValues["Brasil"] = 0;
            genreValues["Rock"] = 0;
            genreValues["Disco"] = 0;
            genreValues["Metal"] = 0;

            // Récupérer le nom du genre musical
            string genreName = MesInfos.ValueGenre.Key;

            // Vérifier si le genre musical existe dans le dictionnaire
            if (genreValues.ContainsKey(genreName))
            {
                // Augmenter la valeur du genre musical correspondant
                genreValues[genreName] += ValueGenre;
            }
        }
    }

    // Méthode pour calculer le pourcentage de chaque musique préférée par rapport à la valeur totale
    public float CalculatePercentage()
    {
        float maxPercentage = 0f; // Initialiser la valeur maximale du pourcentage

        foreach (var genre in genreValues)
        {
            if (TotalValueGenre > 0)
            {
                float percentage = (genre.Value / TotalValueGenre) * 100f;
                Debug.Log("Pourcentage de " + genre.Key + " : " + percentage + "%");

                if (percentage > maxPercentage)
                {
                    maxPercentage = percentage; // Mettre à jour la valeur maximale du pourcentage si nécessaire
                }
            }
            else
            {
                Debug.LogWarning("La valeur totale est égale à zéro !");
            }
        }
        //SliderAfro.value = 0.6F;
        //SliderDrill.value = 0.6F;
        //SliderJazz.value = 1F;
        //SliderCountry.value = 2F;
        //SliderBrazil.value = 0.6F;
        //SliderRock.value = 3F;
        //SliderDisco.value = 0.6F;
        //SliderMetal.value = 10F;
        return maxPercentage; // Retourner la valeur maximale du pourcentage
    }
}