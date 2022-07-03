using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip OnMouce;
    [SerializeField] private AudioClip OnClick;

    public void PlayAudio(int what)
    {
        if(what == 0)
        {
            audioSource.clip = OnMouce;
            audioSource.Play();
        }
        else if (what == 1)
        {
            audioSource.clip = OnClick;
            audioSource.Play();
        }
    }
}
