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

    // Méthode pour augmenter le score
    public void AugmenterScore(int points)
    {
        score += points;
        Debug.Log("Score augmenté de " + points + " points. Nouveau score : " + score);
    }

    // Méthode pour décrémenter le score
    public void DiminuerScore(int points)
    {
        score -= points;
        Debug.Log("Score diminué de " + points + " points. Nouveau score : " + score);
    }

    // Méthode pour augmenter le score de manière continue avec un délai
    public void IncreaseScoreContinuous(int amount)
    {
        // Augmenter le score de façon continue
        score += amount;
        isContinuousScoreIncreasing = true;
    }

    public void DecreaseScoreContinuous(int amount)
    {
        // Diminuer le score de façon continue
        if (isContinuousScoreIncreasing)
        {
            score -= amount;
            if (score < 0)
            {
                score = 0;
            }
        }
    }
}