using UnityEngine;

public class StarPoints : MonoBehaviour
{
    public int value = 10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShipTag"))
        {
            GameManager.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }
}
