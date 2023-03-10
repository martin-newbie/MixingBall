using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource BGM;
    public GameObject SFXObject;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject sfx = GameObject.Instantiate(SFXObject,transform);
        sfx.name = "SFX - " + sfxName;
        sfx.TryGetComponent<AudioSource>(out AudioSource audiosource);
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(sfx, clip.length); 
    }

    public void BGMPlay(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.loop = true;
        BGM.Play();
    }
}
