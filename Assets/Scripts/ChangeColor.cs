using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private float timeBetweenChanges;
    private float timer;
    private int minTime = 2;
    private int maxTime = 2;
    private string[] hexColors = new string[4];
    public MainSceneManager mainSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        // Initialisation du timer et du temps entre les changements
        timeBetweenChanges = Random.Range(minTime, maxTime);
        timer = 0f;

        // Initialise les couleurs disponibles
        hexColors[0] = "#FFFF00"; // Jaune
        hexColors[1] = "#FF00FF"; // Magenta
        hexColors[2] = "#00FFFF"; // Cyan
        hexColors[3] = "#595959"; // Gris


        ChangeCubeColor();
    }

    private void Update()
    {
        if (mainSceneManager.startGame == true)
        {
            // Incrémente le timer
            timer += Time.deltaTime;

            // Vérifie si le timer dépasse le temps entre les changements
            if (timer >= timeBetweenChanges)
            {
                // Change la couleur du cube et réinitialise le timer
                ChangeCubeColor();
                timer = 0f;

                // Redéfinit le temps entre les changements pour le prochain changement
                timeBetweenChanges = Random.Range(minTime, maxTime);
            }
        }
        
    }
    void ChangeCubeColor()
    {
        // Choisis une couleur aléatoire parmi les couleurs en format hexadécimal
        string randomHexColor = hexColors[Random.Range(0, hexColors.Length)];
        Color randomColor = HexToColor(randomHexColor);

        // Change la couleur du cube
        meshRenderer.material.color = randomColor;
        // Change la couleur émissive du cube
        meshRenderer.material.SetColor("_EmissionColor", randomColor);
        // Active l'émission
        meshRenderer.material.EnableKeyword("_EMISSION");
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
