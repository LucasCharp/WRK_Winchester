using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] pnjPrefabs;
    public float spawnInterval;
    public float numberOfPeopleInQueue;
    public int maxQueueSize = 5;
    public bool isFull = false;
    public GameObject Main;
    public bool spawnerHasStart = false;

    void Start()
    {

    }

    public void DestroySpawner()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        MainSceneManager mainScene = Main.GetComponent<MainSceneManager>();
        if (mainScene.startGame == true && spawnerHasStart == false)
        {
            InvokeRepeating("SpawnRandomPNJ", 8f, spawnInterval);
            spawnerHasStart = true;
        }

        if (numberOfPeopleInQueue == maxQueueSize)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }
    }

    void SpawnRandomPNJ()
    {
        // Vérifiez si la file d'attente est pleine avant de spawn un nouveau PNJ
        if (!isFull)
        {
            GameObject selectedPrefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f);
            Instantiate(selectedPrefab, spawnPosition, spawnRotation);
        }
    }

    public void StartQueue()
    {
        numberOfPeopleInQueue += 1;

        // Vérifiez si la file d'attente est pleine
        if (numberOfPeopleInQueue >= maxQueueSize)
        {
            isFull = true;
            Debug.Log("File d'attente pleine !");
        }
    }
}