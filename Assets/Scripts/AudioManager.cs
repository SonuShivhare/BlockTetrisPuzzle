using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip selectionSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip snapBackSound;

    private new AudioSource audio;

    public static AudioManager instance;

    public bool isActive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != null)
        {
            Destroy(gameObject);
        }

        audio = GetComponent<AudioSource>();
    }

    public void SelectionSound()
    {
        if (isActive) audio.PlayOneShot(selectionSound);
    }

    public void DropSound()
    {
        if (isActive) audio.PlayOneShot(dropSound);
    }

    public void SnapBackSound()
    {
        if (isActive) audio.PlayOneShot(snapBackSound);
    }
}
