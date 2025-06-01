using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;


    public int maxLives = 3;
    private int currentLives;

    public Image[] lifeImages;

    private Camera mainCam;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Biến để lưu trữ các đối tượng đạn
    public GameObject Bullet;
    public GameObject Bullet01;
    public GameObject Bullet02;

    public AudioClip shootClip; // Gán file âm thanh vào đây
    private AudioSource audioSource;
    //hiệu ứng nổ
    public GameObject Explosion;


    void Start()
    {
        mainCam = Camera.main;
        screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.extents.x;
            objectHeight = sr.bounds.extents.y;
        }

        currentLives = maxLives;
        UpdateLivesUI();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {

        Vector3 move = Vector3.zero;

        // Sử dụng phím mũi tên thay vì WASD
        if (Keyboard.current.upArrowKey.isPressed)
            move += Vector3.up;
        if (Keyboard.current.downArrowKey.isPressed)
            move += Vector3.down;
        if (Keyboard.current.leftArrowKey.isPressed)
            move += Vector3.left;
        if (Keyboard.current.rightArrowKey.isPressed)
            move += Vector3.right;

        // Sử dụng phím Space để bắn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Tạo đạn mới và đặt vị trí bắn
            GameObject bullet01 = (GameObject)Instantiate(Bullet);
            bullet01.transform.position = Bullet01.transform.position;
            GameObject bullet02 = (GameObject)Instantiate(Bullet);
            bullet02.transform.position = Bullet02.transform.position;

            if (shootClip != null && audioSource != null)
                audioSource.PlayOneShot(shootClip);
        }

        Vector3 newPos = transform.position + move.normalized * moveSpeed * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        newPos.y = Mathf.Clamp(newPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyShipTag"))
        {
            currentLives--;
            UpdateLivesUI();

            if (currentLives <= 0)
            {
                Debug.Log("Game Over!");
                gameObject.SetActive(false);
            }

            PlayExplosion(); // Gọi hàm để phát hiệu ứng nổ            
            //Destroy(gameObject); // Hủy đối tượng người chơi
        }

    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position ;
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = i < currentLives;
        }
    }
}
