using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float fallSpeed = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * fallSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
