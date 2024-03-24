using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float score = 0;
    public bool jeGagneEnContinue = false;
    public bool jeChieEnContinue = false;
    public ToiletArea toiletArea;
    public TextMeshProUGUI scoreText;
    string resultString = "score: ";
    public float Multiplicateur = 1;
    public UpgradeButtons UpdateButtons;
    public TextMeshProUGUI moneyTextPlay;
    public float Multiplicatir = 1f;
    public float money = 0;
    public int boxNumber;
    string moneyString = "money: ";
    void Start()
    {
        money = 600;
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
                    Invoke("GainContinue", 2f);
                }
            }
            else
            {
                if (!jeGagneEnContinue)
                {
                    jeGagneEnContinue = true;
                    Invoke("PerteContinue", 2f);
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
        AugmenterScore(-1);
        jeChieEnContinue = false;
    }
    public void invokeRef()
    {
        Invoke("shitContinue", 1f);
    }
    public void AddMultiplicata()
    {
        Multiplicateur += 0.5f;
        Debug.Log(Multiplicateur);
    }
    public void AugmenterArgent(float points)
    {
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
        moneyTextPlay.text = moneyString;
    }
    public void AddMultiplicato()
    {
        Multiplicatir += 0.1f;
        Debug.Log(Multiplicatir);
    }
}