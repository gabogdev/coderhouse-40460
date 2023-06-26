using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private bool activateAudio;
    [SerializeField] private AudioClip musicEnviroment;
    public AudioClip colisionClip;
    public AudioClip itemClip;
    public AudioClip jumpClip;
    public AudioClip uiClip;

    private AudioSource audioSource;
    private Pooler musicPooler;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        musicPooler = GetComponent<Pooler>();
    }

    void Start()
    {
        PlayMusicEnviroment();
    }

    private void PlayMusicEnviroment()
    {
        if (activateAudio)
        {
            audioSource.clip = musicEnviroment;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySoundFX(AudioClip clip, float volume = 0.5f)
    {
        if (activateAudio)
        {
            GameObject newClip = musicPooler.GetInstancePooler();
            AudioSource audioSource = newClip.GetComponent<AudioSource>();
            newClip.SetActive(true);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = false;
            audioSource.Play();
            StartCoroutine(CODeactivate(newClip, clip.length));
        }
    }

    private IEnumerator CODeactivate(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
