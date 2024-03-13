using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int boxNumber;

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
}