using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject starPrefab;
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBulletTag") || other.CompareTag("PlayerShipTag"))
        {
            PlayExplosion();
            DropStars();
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }

    void DropStars()
    {
        int starsToDrop = Random.Range(0, 6); // 0 - 5 sao

        for (int i = 0; i < starsToDrop; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.3f, 0.3f), 0);
            GameObject star = Instantiate(starPrefab, transform.position + spawnOffset, Quaternion.identity);
        }
    }
}
