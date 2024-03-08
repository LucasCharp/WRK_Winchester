using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightColor : MonoBehaviour
{
    public Color[] colors; // Liste des couleurs disponibles
    private Light spotlight;
    private float colorChangeInterval = 3f; // Intervalle de changement de couleur
    public MainSceneManager mainSceneManager;
    private bool hasStarted;
    private int lightIntensity = 600;
   
    void Start()
    {
        hasStarted = false;
        spotlight = GetComponent<Light>();
        spotlight.intensity = 0;
    }
    private void Update()
    {
        if (mainSceneManager.startGame == true)
        {
            if (!hasStarted ) 
            {
                // D�marre la Coroutine pour changer la couleur � intervalles r�guliers
                StartCoroutine(ChangeColorRoutine());
                hasStarted = true;
                spotlight.intensity = lightIntensity;
            }
        }
    }
    IEnumerator ChangeColorRoutine()
    {
        while (true)
        {
            // Choisit une couleur al�atoire parmi la liste et l'applique � la spotlight
            SetRandomColor();

            // Attend l'intervalle de changement de couleur
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }

    void SetRandomColor()
    {
        // Choix al�atoire d'un indice de couleur dans la liste
        int randomIndex = Random.Range(0, colors.Length);

        // Applique la couleur al�atoire � la spotlight
        spotlight.color = colors[randomIndex];
    }
}
