using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light myLight;
    
    //Faire fluctuer la lampe
    public float intensityRange = 1.0f; // Plage d'intensité pour la variation
    public float intensitySpeed = 1.0f; // Vitesse de variation d'intensité
    private float initialIntensity; // Intensité initiale

    void Start()
    {
        // Récupérer le composant Light attaché au GameObject
        myLight = GetComponent<Light>();

        initialIntensity = myLight.intensity; // Sauvegarder l'intensité initiale

    }

    void Update()
    {
        // Variation de couleur au fil du temps (comme avant)
        //float t = Mathf.PingPong(Time.time, 1.0f) / 1.0f;
        //myLight.color = Color.Lerp(Color.red, Color.blue, t);

        // Variation sinusoïdale de l'intensité
        float intensityFactor = Mathf.Sin(Time.time * intensitySpeed);
        myLight.intensity = initialIntensity + intensityRange * intensityFactor;
    }

    public void ChangeColorHex(string hexColor)
    {
        Color newColor;

        // Convertir le code hexadécimal en couleur
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
        myLight.intensity = newSpeed;
    }
}
