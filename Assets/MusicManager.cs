using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Audio Components")]
    public AudioSource musicSource;

    [Header("Music Tracks")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip gameOverMusic;

    public bool loopGameOverMusic = false;


    [Header("Settings")]
    [Range(0f, 1f)] public float menuVolume = 1.0f;
    [Range(0f, 1f)] public float gameplayVolume = 0.8f;
    [Range(0.1f, 3.0f)] public float fadeSpeed = 1.0f;
    public bool useFading = true;


    // Internal tracking variables
    private AudioClip targetClip;
    private float targetVolume;
    private float originalVolume;
    private bool isFading = false;
    private string currentSceneName;
    private bool isInGameOverState = false;

    private void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Create audio source if not assigned
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
            }

            // Listen for scene changes
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Store original volume
            originalVolume = musicSource.volume;
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Begin playing appropriate music for starting scene
        currentSceneName = SceneManager.GetActiveScene().name;
        HandleSceneMusic(currentSceneName);
    }

    private void Update()
    {
        // Handle fading between tracks
        if (isFading)
        {
            if (isInGameOverState && !musicSource.isPlaying && !loopGameOverMusic)
            {
                // Music finished playing and not set to loop
                isInGameOverState = false;
            }
                // If fading out
                if (musicSource.clip != targetClip)
            {
                musicSource.volume -= Time.unscaledDeltaTime * fadeSpeed;

                // When volume is near zero, change tracks
                if (musicSource.volume <= 0.01f)
                {
                    musicSource.Stop();
                    musicSource.clip = targetClip;
                    musicSource.volume = 0f;
                    musicSource.Play();
                }
            }
            // If fading in
            else
            {
                musicSource.volume += Time.unscaledDeltaTime * fadeSpeed;

                // When volume reaches target, stop fading
                if (musicSource.volume >= targetVolume)
                {
                    musicSource.volume = targetVolume;
                    isFading = false;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Detect scene changes and update music accordingly
        currentSceneName = scene.name;
        HandleSceneMusic(currentSceneName);
    }

    private void HandleSceneMusic(string sceneName)
    {
        // Don't change music if we're in game over state
        if (isInGameOverState)
            return;

        // Regular music handling...
        if (sceneName == "MainMenu")
        {
            PlayMusic(menuMusic, menuVolume);
        }
        else if (sceneName.Contains("Gameplay") || sceneName.Contains("Level"))
        {
            PlayMusic(gameplayMusic, gameplayVolume);
        }
    }

    public void PlayMusic(AudioClip music, float volume = 1.0f)
    {
        // Don't change if same track is already playing
        if (musicSource.clip == music && musicSource.isPlaying)
            return;

        targetClip = music;
        targetVolume = volume;

        if (useFading && musicSource.isPlaying)
        {
            // Start fading if not already doing so
            isFading = true;
        }
        else
        {
            // Direct switch if not using fading or no music playing
            musicSource.clip = music;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }
    // Add a method to reset state when needed
    public void ResetGameOverState()
    {
        isInGameOverState = false;
        musicSource.loop = true; // Reset to default looping behavior
    }

    public void PlayGameOverMusic()
    {
        // Stop any current music
        musicSource.Stop();

        // Set the clip
        musicSource.clip = gameOverMusic;

        // Set looping based on preference
        musicSource.loop = loopGameOverMusic;

        // Play the game over music
        musicSource.volume = targetVolume;
        musicSource.Play();

        // Set state flag
        isInGameOverState = true;
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        isFading = false;
    }

    public void SetVolume(float volume)
    {
        targetVolume = volume;
        if (!isFading)
            musicSource.volume = volume;
    }

    private void OnDestroy()
    {
        // Remove scene change listener to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}