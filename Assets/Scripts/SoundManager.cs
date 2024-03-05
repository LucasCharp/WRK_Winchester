using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource ambianceAudioSource;
    public AudioClip[] sonsAmbiance;
    public float tempsEntreSonsMin = 10f;
    public float tempsEntreSonsMax = 30f;

    void Start()
    {
        // Assurez-vous que l'Audio Source est attach� au m�me objet ou r�f�renc� dans l'inspecteur
        ambianceAudioSource = GetComponent<AudioSource>();

        // Commencez la coroutine pour jouer des sons al�atoires
        StartCoroutine(JouerAmbianceAleatoire());
    }

    IEnumerator JouerAmbianceAleatoire()
    {
        while (true)
        {
            // Choisissez un son al�atoire dans la liste
            AudioClip sonChoisi = sonsAmbiance[Random.Range(0, sonsAmbiance.Length)];

            // Jouez le son
            ambianceAudioSource.clip = sonChoisi;
            ambianceAudioSource.Play();

            // Attendez un certain temps avant de jouer le prochain son
            float tempsAttente = Random.Range(tempsEntreSonsMin, tempsEntreSonsMax);
            yield return new WaitForSeconds(tempsAttente);
        }
    }
}
