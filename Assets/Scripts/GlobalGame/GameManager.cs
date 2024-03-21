using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int boxNumber;
    private bool jeGagneEnContinue = false;
    public ToiletArea toiletArea;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
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
                        Debug.Log("bien joué");
                    }
                }
                else if (jeGagneEnContinue == false)
                {
                    jeGagneEnContinue = true;
                    Invoke("PerteContinue", 2f);
                    Debug.Log("Dommage");
                }
            }
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
    private void GainContinue()
    {
        AugmenterScore(7);
        jeGagneEnContinue = false;
    }
    private void PerteContinue()
    {
        DiminuerScore(4);
        jeGagneEnContinue = false;
    }
}