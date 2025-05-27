using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxLives = 3;
    private int currentLives;

    public Image[] lifeImages; // Gắn 3 hình trái tim UI vào đây

    private Camera mainCam;
    private float minX, maxX, minY, maxY;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        mainCam = Camera.main;

        float camDistance = Mathf.Abs(mainCam.transform.position.z - transform.position.z);
        Vector3 bottomLeft = mainCam.ScreenToWorldPoint(new Vector3(0, 0, camDistance));
        Vector3 topRight = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camDistance));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.extents.x;
            objectHeight = sr.bounds.extents.y;
        }

        minX = bottomLeft.x + objectWidth;
        maxX = topRight.x - objectWidth;
        minY = bottomLeft.y + objectHeight;
        maxY = topRight.y - objectHeight;

        currentLives = maxLives;
        UpdateLivesUI();
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Keyboard.current.upArrowKey.isPressed)
            move += Vector3.up;
        if (Keyboard.current.downArrowKey.isPressed)
            move += Vector3.down;
        if (Keyboard.current.leftArrowKey.isPressed)
            move += Vector3.left;
        if (Keyboard.current.rightArrowKey.isPressed)
            move += Vector3.right;

        Vector3 newPos = transform.position + move.normalized * moveSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Gán tag cho thiên thạch hoặc địch
        {
            currentLives--;
            UpdateLivesUI();

            if (currentLives <= 0)
            {
                Debug.Log("Game Over!");
                gameObject.SetActive(false); // Ẩn tàu, có thể load Game Over scene tại đây
            }
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = i < currentLives;
        }
    }
}
