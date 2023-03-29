using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Audios
{
    riffleSound,
    blackholesound,
}
public class AudioController : MonoBehaviour
{
    public AudioClip riffleSoundPrefab;
    public AudioClip blackholeSoundPrefab;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAudio(Audios.blackholesound);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(Audios a)
    {
        switch (a)
        {
            case (Audios.blackholesound):


                audioSource.PlayOneShot(blackholeSoundPrefab);
                Debug.Log("Blackhole Sound");
                break;
            case (Audios.riffleSound):
                audioSource.PlayOneShot(riffleSoundPrefab);
                break;
        }
    }


}
