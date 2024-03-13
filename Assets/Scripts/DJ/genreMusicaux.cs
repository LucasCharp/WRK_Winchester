using UnityEngine;

public class CharacterMusicCounter : MonoBehaviour
{
    // Variables publiques pour les genres musicaux
    public int rockCounter = 0;
    public int popCounter = 0;
    public int jazzCounter = 0;
    public int electronicCounter = 0;
    public int classicalCounter = 0;

    void Start()
    {
        // Attribution al�atoire d'un point � l'un des genres musicaux d�s l'apparition du personnage
        AddRandomPointToGenre();
    }

    // Fonction pour ajouter un point � un genre musical al�atoire
    void AddRandomPointToGenre()
    {
        // G�n�rer un nombre al�atoire entre 0 et 4 inclus pour choisir le genre musical
        int randomGenreIndex = Random.Range(0, 5);

        // Ajouter un point au genre musical correspondant
        switch (randomGenreIndex)
        {
            case 0:
                rockCounter++;
                Debug.Log("Un point ajout� au genre musical : Rock");
                break;
            case 1:
                popCounter++;
                Debug.Log("Un point ajout� au genre musical : Pop");
                break;
            case 2:
                jazzCounter++;
                Debug.Log("Un point ajout� au genre musical : Jazz");
                break;
            case 3:
                electronicCounter++;
                Debug.Log("Un point ajout� au genre musical : Electronic");
                break;
            case 4:
                classicalCounter++;
                Debug.Log("Un point ajout� au genre musical : Classical");
                break;
            default:
                Debug.LogWarning("Indice de genre musical invalide");
                break;
        }
    }
}