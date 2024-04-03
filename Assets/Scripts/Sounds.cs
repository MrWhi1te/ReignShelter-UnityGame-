using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource _AudioSource;
    
    public AudioClip swipe;
    public AudioClip enterCards;
    public AudioClip die;

    public void PlayAudioSwipe()
    {
        _AudioSource.PlayOneShot(swipe);
    }

    public void PlayEnterCards()
    {
        _AudioSource.PlayOneShot(enterCards);
    }

    public void PlayAudioDie()
    {
        _AudioSource.PlayOneShot(die);
    }
}
