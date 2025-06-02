using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;


    public int maxLives = 3;
    private int currentLives;

    public PauseMenu pauseMenu;
    public Image[] lifeImages;

    private Camera mainCam;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    
    public GameObject Bullet;
    public GameObject Bullet01;
    public GameObject Bullet02;

    public AudioClip shootClip; 
    private AudioSource audioSource;
    
    public AudioClip explosionSound; //sound for the explosion
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

        
        if (Keyboard.current.upArrowKey.isPressed)
            move += Vector3.up;
        if (Keyboard.current.downArrowKey.isPressed)
            move += Vector3.down;
        if (Keyboard.current.leftArrowKey.isPressed)
            move += Vector3.left;
        if (Keyboard.current.rightArrowKey.isPressed)
            move += Vector3.right;

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
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
                PlayExplosion(); 

                Debug.Log("Game Over!");
                gameObject.SetActive(false);

                // Call the GameOver method from the PauseMenu
                if (pauseMenu != null)
                    pauseMenu.GameOver();
            }
            else
            {
                PlayExplosion(); 
            }
        }

    }

    void PlayExplosion()
    {
        
        if (Explosion != null)
        {
            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        }

        
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
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
