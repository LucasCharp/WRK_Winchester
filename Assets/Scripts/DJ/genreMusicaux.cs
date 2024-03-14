using UnityEngine;

public class CharacterMusicCounter : MonoBehaviour
{
    // Variables publiques pour les genres musicaux
    public int rockCounter = 0;
    public int afroCounter = 0;
    public int jazzCounter = 0;
    public int drillCounter = 0;
    public int countryCounter = 0;
    public int discoCounter = 0;
    public int brasilCounter = 0;
    public int metalCounter = 0;

    void Start()
    {
        // Attribution aléatoire d'un point à l'un des genres musicaux dès l'apparition du personnage
        AddRandomPointToGenre();
    }
    public int GetGenrePreference(int genreIndex)
    {
        switch (genreIndex)
        {
            case 0:
                return afroCounter;
            case 1:
                return drillCounter;
            case 2:
                return jazzCounter;
            case 3:
                return countryCounter;
            case 4:
                return brasilCounter;
            case 5:
                return rockCounter;
            case 6:
                return discoCounter;
            case 7:
                return metalCounter;
            default:
                Debug.LogWarning("Indice de genre musical invalide");
                return 0;
        }
    }
    // Fonction pour ajouter un point à un genre musical aléatoire
    void AddRandomPointToGenre()
    {
        // Générer un nombre aléatoire entre 0 et 4 inclus pour choisir le genre musical
        int randomGenreIndex = Random.Range(0, 7);

        // Ajouter un point au genre musical correspondant
        switch (randomGenreIndex)
        {
            case 0:
                afroCounter++;
                Debug.Log("Un point ajouté au genre musical : Afro");
                break;
            case 1:
                drillCounter++;
                Debug.Log("Un point ajouté au genre musical : Drill");
                break;
            case 2:
                jazzCounter++;
                Debug.Log("Un point ajouté au genre musical : Jazz");
                break;
            case 3:
                countryCounter++;
                Debug.Log("Un point ajouté au genre musical : Country");
                break;
            case 4:
                brasilCounter++;
                Debug.Log("Un point ajouté au genre musical : Brasil");
                break;
            case 5:
                rockCounter++;
                Debug.Log("Un point ajouté au genre musical : rock");
                break;
            case 6:
                discoCounter++;
                Debug.Log("Un point ajouté au genre musical : Disco");
                break;
            case 7:
                metalCounter++;
                Debug.Log("Un point ajouté au genre musical : Metal");
                break;
            default:
                Debug.LogWarning("Indice de genre musical invalide");
                break;
        }
        
    }
}