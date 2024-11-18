using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance for global access

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;    // Source for background music
    [SerializeField] private AudioSource sfxSource;      // Source for sound effects

    [Header("Clips")]
    [SerializeField] private AudioClip clickSound;       // Button click sound
    [SerializeField] private AudioClip deathSound;       // Sound to play on death
    [SerializeField] private AudioClip winSound;         // Sound to play on win

    private void Awake()
    {
        // Singleton pattern to persist AudioManager across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(); // Start playing music on game start
    }

    public void PlayMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayClickSound()
    {
        PlaySFX(clickSound);
    }

    public void PlayDeathSound()
    {
        PlaySFX(deathSound);
    }

    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
