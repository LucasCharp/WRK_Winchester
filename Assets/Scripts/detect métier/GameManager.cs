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
}