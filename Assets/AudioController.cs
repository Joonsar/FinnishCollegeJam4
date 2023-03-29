using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Audios
{
    riffleSound,
    blackholesound,

    flamethrowsound,
    levelupsound
}
public class AudioController : MonoBehaviour
{
    public AudioClip riffleSoundPrefab;
    public AudioClip blackholeSoundPrefab;

    public AudioClip flamethrowerSoundPrefab;
    public AudioClip levelupSoundPrefab;

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

                break;
            case (Audios.riffleSound):
                audioSource.PlayOneShot(riffleSoundPrefab);
                break;
            case (Audios.flamethrowsound):
                audioSource.PlayOneShot(flamethrowerSoundPrefab);
                break;
            case (Audios.levelupsound):
                audioSource.PlayOneShot(levelupSoundPrefab);
                break;
            default:
                break;
        }
    }


}
