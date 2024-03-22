using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float score = 0;
    public int boxNumber;
    public bool jeGagneEnContinue = false;
    public bool jeChieEnContinue = false;
    public ToiletArea toiletArea;
    public TextMeshProUGUI scoreText;
    string resultString = "score: ";
    public float Multiplicateur = 0;
    public UpgradeButtons UpdateButtons;
    public List<TextMeshProUGUI> scoreTexts;
    void Awake()
    {
        foreach (TextMeshProUGUI scoreText in scoreTexts)
        {
            scoreText.text = score.ToString();
        }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (ButtonGenre.getSelectedGenre() != null && MusicGenreAnalyzer.getFavoriteGenres() != null)
        {
            foreach (string genre in MusicGenreAnalyzer.getFavoriteGenres())
            {
                if (genre == ButtonGenre.getSelectedGenre())
                {
                    if (jeGagneEnContinue == false)
                    {
                        jeGagneEnContinue = true;
                        Invoke("GainContinue", 2f);
                    }
                }
                else if (jeGagneEnContinue == false)
                {
                    jeGagneEnContinue = true;
                    Invoke("PerteContinue", 2f);
                }
            }
        }
    }
    // Méthode pour augmenter le score
    public void AugmenterScore(int points)
    {
        score += points * Multiplicateur;
        Debug.Log("Score augmenté de " + points + " points. Nouveau score : " + score);
        resultString = null;
        resultString += score;
        scoreText.text = resultString;
        foreach (TextMeshProUGUI scoreText in scoreTexts)
        {
            scoreText.text = score.ToString();
        }
    }

    // Méthode pour décrémenter le score
    public void DiminuerScore(int points)
    {
        score -= points * Multiplicateur;
        Debug.Log("Score diminué de " + points + " points. Nouveau score : " + score);
        resultString = null;
        resultString += score;
        scoreText.text = resultString;
        foreach (TextMeshProUGUI scoreText in scoreTexts)
        {
            scoreText.text = score.ToString();
        }
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
}