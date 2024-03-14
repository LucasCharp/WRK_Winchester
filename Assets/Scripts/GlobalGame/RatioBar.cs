using UnityEngine;
using UnityEngine.UI;

public class RatioBar : MonoBehaviour
{
    public Slider ratioSlider; // Référence au Slider pour afficher le ratio
   

    private void Update()
    {
        // Compter le nombre d'humains et de zombies actifs dans la scène
        GameObject[] pnjs = GameObject.FindGameObjectsWithTag("PNJ");
        int activeHumans = 0;
        int activeZombies = 0;

        foreach (GameObject pnj in pnjs)
        {
            PNJScript pnjScript = pnj.GetComponent<PNJScript>();
            if (pnjScript != null)
            {
                if (pnjScript.isHuman)
                {
                    activeHumans++;
                }
                else 
                {
                    Debug.Log("Zombies touvésss");
                    activeZombies++;
                }
            }
        }

        // Calculer le ratio
        float totalPopulation = activeHumans + activeZombies;
        float humanRatio = totalPopulation == 0 ? 0f : (float)activeHumans / totalPopulation;
        float zombieRatio = 1 - humanRatio;

        // Mettre à jour le slider avec le ratio calculé
        ratioSlider.value = humanRatio;
    }
}

