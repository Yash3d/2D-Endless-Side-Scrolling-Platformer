using UnityEngine;

public class Audio_Manager : MonoBehaviour {

    public static Audio_Manager instance;
    public Sound[] sounds;

    private void Awake() {

        foreach (Sound s in sounds) {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.name = s.name;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start() {

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }

        //PlaySound("Theme");
    }

    public void PlaySound(string name) {

        foreach (Sound s in sounds) {
            if (s.name == name) {
                s.source.Play();
            }
        }
    }
}
