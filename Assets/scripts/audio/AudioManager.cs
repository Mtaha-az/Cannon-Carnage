using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source---------")]
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource sfxrockcollision;

    [Header("----------Audio Clip---------")]
    public AudioClip firing;
    public AudioClip explosion;
    public AudioClip brick_break;
    public AudioClip cutter;
    public AudioClip coinsFalling;
    public AudioClip coinsCollecting;
    public AudioClip jump;
    public AudioClip Rockcollide;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFXrock(AudioClip clip)
    {
        sfxrockcollision.PlayOneShot(clip);
    }
}
