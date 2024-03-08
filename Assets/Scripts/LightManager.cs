using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light myLight;
    
    //Faire fluctuer la lampe
    public float intensityRange = 1.0f; // Plage d'intensit� pour la variation
    public float intensitySpeed = 1.0f; // Vitesse de variation d'intensit�
    private float initialIntensity; // Intensit� initiale
    public MainSceneManager mainSceneManager;
    private bool hasStarted;

    void Start()
    {
        hasStarted = false;
        // R�cup�rer le composant Light attach� au GameObject
        myLight = GetComponent<Light>();

        initialIntensity = myLight.intensity; // Sauvegarder l'intensit� initiale

    }

    void Update()
    {
        if (mainSceneManager.startGame == false)
        {
            intensitySpeed = 0;
        }
        else if (mainSceneManager.startGame == true)
        {
            if (hasStarted == false)
            {
                intensitySpeed = 1;
                hasStarted = true;
            }  
        }
            // Variation de couleur au fil du temps (comme avant)
            //float t = Mathf.PingPong(Time.time, 1.0f) / 1.0f;
            //myLight.color = Color.Lerp(Color.red, Color.blue, t);

            // Variation sinuso�dale de l'intensit�
            float intensityFactor = Mathf.Sin(Time.time * intensitySpeed);
        myLight.intensity = initialIntensity + intensityRange * intensityFactor;
    }

    public void ChangeColorHex(string hexColor)
    {
        Color newColor;

        // Convertir le code hexad�cimal en couleur
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            myLight.color = newColor;
        }
        else
        {
            Debug.LogError("Invalid hex color code");
        }
    }

    public void ChangeIntensitySpeed(float newSpeed)
    {
        intensitySpeed = newSpeed;
    }
}
