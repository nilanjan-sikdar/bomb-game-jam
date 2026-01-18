using UnityEngine;

public class UIButtonSFX : MonoBehaviour
{
    public static UIButtonSFX Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSFX;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayClick()
    {
        if (buttonClickSFX != null)
            audioSource.PlayOneShot(buttonClickSFX);
    }
}
