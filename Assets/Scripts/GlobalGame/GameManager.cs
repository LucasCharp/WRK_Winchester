using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int boxNumber;
    private bool isContinuousScoreIncreasing = false; // Indique si le score augmente continuellement

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�thode pour augmenter le score
    public void AugmenterScore(int points)
    {
        score += points;
        Debug.Log("Score augment� de " + points + " points. Nouveau score : " + score);
    }

    // M�thode pour d�cr�menter le score
    public void DiminuerScore(int points)
    {
        score -= points;
        Debug.Log("Score diminu� de " + points + " points. Nouveau score : " + score);
    }

    // M�thode pour augmenter le score de mani�re continue avec un d�lai
    public void IncreaseScoreContinuous(int amount)
    {
        // Augmenter le score de fa�on continue
        score += amount;
        isContinuousScoreIncreasing = true;
    }

    public void DecreaseScoreContinuous(int amount)
    {
        // Diminuer le score de fa�on continue
        if (isContinuousScoreIncreasing)
        {
            score -= amount;
            if (score < 0)
            {
                score = 0;
            }
        }
    }
    // Coroutine pour augmenter le score de mani�re continue
    private IEnumerator IncreaseScoreCoroutine(int points)
    {
        while (true)
        {
            // Augmenter le score de points
            AugmenterScore(points);

            // Attendre un court instant (par exemple, 1 seconde) avant d'augmenter � nouveau le score
            yield return new WaitForSeconds(1f);
        }
    }
    public void ResetContinuousScore()
    {
        // Impl�mentez ici la logique pour r�initialiser le score continu
        Debug.Log("Continuous score reset.");
    }
}