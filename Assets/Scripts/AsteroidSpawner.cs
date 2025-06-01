using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] Asteroid;
    float maxSpawnRateInSeconds = 5f;
    void Start()
    {
        Invoke("RandomAsteroid", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 15f); // Increase spawn rate every 10 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RandomAsteroid()
    {
        if (Asteroid.Length == 0) return;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        int randomIndex = Random.Range(0, Asteroid.Length);
        GameObject asteroid = Instantiate(Asteroid[randomIndex]);

        asteroid.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
        SpamAsteroid();
    }
    public void SpamAsteroid()
    {
        float spawnRateInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnRateInSeconds = Random.Range(1f ,maxSpawnRateInSeconds);
        }
        else
        {
            spawnRateInSeconds = 1f;
        }
        Invoke("RandomAsteroid", spawnRateInSeconds);
    }

    public void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f) maxSpawnRateInSeconds --;
        if (maxSpawnRateInSeconds == 1f)    CancelInvoke("IncreaseSpawnRate");
    }
}
