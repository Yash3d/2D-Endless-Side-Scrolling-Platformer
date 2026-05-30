using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource shootSource;
    public AudioSource jumpSource;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void PlayShootSound() {
        shootSource.Play();
    }

    public void PlayJumpSound() {
        jumpSource.Play();
    }
}
