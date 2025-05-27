using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 6f;
    public float asteroidSpeed = 1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefab == null || Camera.main == null) return;

        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float spawnY = cam.transform.position.y + camHeight / 2 + 1f;

        int spawnCount = Random.Range(1, 4); // 1 to 3 asteroids

        for (int i = 0; i < spawnCount; i++)
        {
            float spawnX = Random.Range(cam.transform.position.x - camWidth / 2, cam.transform.position.x + camWidth / 2);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            asteroid.transform.localScale = Vector3.one * 2f; // Tăng kích thước gấp đôi

            Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.down * asteroidSpeed;
                rb.angularVelocity = Random.Range(-90f, 90f);
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        if (Camera.main != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Camera.main.transform.position, spawnRadius);
        }
    }
}
