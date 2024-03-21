using TMPro;
using UnityEngine;

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
    public float Multiplicateur = 2;
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
    // M�thode pour augmenter le score
    public void AugmenterScore(int points)
    {
        score += points * Multiplicateur;
        Debug.Log("Score augment� de " + points + " points. Nouveau score : " + score);
        resultString = null;
        resultString += score;
        scoreText.text = resultString;
    }

    // M�thode pour d�cr�menter le score
    public void DiminuerScore(int points)
    {
        score -= points * Multiplicateur;
        Debug.Log("Score diminu� de " + points + " points. Nouveau score : " + score);
        resultString = null;
        resultString += score;
        scoreText.text = resultString;
    }
    private void GainContinue()
    {
        AugmenterScore(7);
        jeGagneEnContinue = false;
    }
    private void PerteContinue()
    {
        AugmenterScore(-4);
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
}