using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float score = 0;
    public bool jeGagneEnContinue = false;
    public bool jeChieEnContinue = false;
    public ToiletArea toiletArea;
    string resultString = "score: ";
    public float Multiplicateur = 1;
    public static UpgradeButtons UpdateButtons;
    public float Multiplicatir = 1f;
    public static float money = 0;
    string moneyString = "money: ";
    private static bool one = false;
    public ExitManager exitManager = new ExitManager();
    public TextMeshProUGUI moneyTextstart = new TextMeshProUGUI();
    public TextMeshProUGUI scoreText;
    void Start()
    {
        if (one == false)
        {
            money = 800;
            one = true;
        }
        if (one == false)
        {
            score = 0;
            one = true;
        }
    }
    private void Update()
    {
        string selectedGenre = ButtonGenre.getSelectedGenre();
        List<string> favoriteGenres = MusicGenreAnalyzer.getFavoriteGenres();

        if (selectedGenre != null && favoriteGenres != null)
        {
            bool isGenreMatch = false;

            foreach (string genre in favoriteGenres)
            {
                Debug.Log(genre);

                if (genre == selectedGenre)
                {
                    isGenreMatch = true;
                    break;
                }
            }

            if (isGenreMatch)
            {
                if (!jeGagneEnContinue)
                {
                    jeGagneEnContinue = true;
                    Invoke("GainContinue", 2.5f);
                }
            }
            else
            {
                if (!jeGagneEnContinue)
                {
                    jeGagneEnContinue = true;
                    Invoke("PerteContinue", 2.5f);
                }
            }
        }
    }
    // Méthode pour augmenter le score
    public void AugmenterScore(float points)
    {
        score += points * Multiplicateur;
        Debug.Log("Score augmenté de " + points + " points. Nouveau score : " + score);
        resultString = null;
        resultString += score;
        scoreText.text = resultString;
        scoreText.ForceMeshUpdate();
    }
    private void GainContinue()
    {
        AugmenterScore(8);
        jeGagneEnContinue = false;
    }
    private void PerteContinue()
    {
        AugmenterScore(-3);
        jeGagneEnContinue = false;
    }
    private void shitContinue()
    {
        AugmenterScore(-2);
        jeChieEnContinue = false;
    }
    public void invokeRef()
    {
        Invoke("shitContinue", 2.1f);
    }
    public void AddMultiplicata()
    {
        Multiplicateur += 0.5f;
        Debug.Log(Multiplicateur);
    }
    public void AugmenterArgent(float points)
    {
        Debug.Log(points);
        if (points > 0)
        {
            money += points * Multiplicatir;
        }
        else
        {
            money += points;
        }
        Debug.Log("money augmenté de " + points + " points. Nouveau money : " + money);
        moneyString = null;
        moneyString += money;
        moneyTextstart.text = moneyString;
        moneyTextstart.ForceMeshUpdate(); // Forcer la mise à jour de l'interface utilisateur
    }
    public void AddMultiplicato()
    {
        Multiplicatir += 0.1f;
        Debug.Log(Multiplicatir);
    }
}