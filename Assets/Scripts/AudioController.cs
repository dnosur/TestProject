using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource MainAudioSource;
    [SerializeField] AudioSource AudioEffect;

    private Assets.Scripts.AudioSettings audioSettings;

    // Start is called before the first frame update
    void Start()
    {
        audioSettings = new Assets.Scripts.AudioSettings();
        audioSettings.Deserialize();

        AudioEffect.volume = audioSettings.AudioEffectsVolume;
        MainAudioSource.volume = audioSettings.MainAudioVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if(MainAudioSource.isPlaying && MainAudioSource.volume < audioSettings.MainAudioVolume)
        {
            MainAudioSource.volume += 0.3f * Time.deltaTime;
        }
        else if(MainAudioSource.isPlaying && MainAudioSource.volume > audioSettings.MainAudioVolume)
        {
            MainAudioSource.volume -= 0.3f * Time.deltaTime;
        }
    }

    public void PlayMain()
    {
        MainAudioSource.Play();
        MainAudioSource.loop = true;
    }

    public void OnAudioEffect()
    {
        if(audioSettings.AudioEffectsVolume != 1)
        {
            audioSettings.ChangeAudioEffectsVolume(1);
        }

        AudioEffect.volume = audioSettings.AudioEffectsVolume;
        AudioEffect.gameObject.SetActive(true);
    }

    public void OffAudioEffect()
    {
        if (audioSettings.AudioEffectsVolume != 0)
        {
            audioSettings.ChangeAudioEffectsVolume(0);
        }

        AudioEffect.gameObject.SetActive(false);
    }

    public void SetMainAudioVolume(float volume) { audioSettings.ChangeMainAudioVolume(volume); }

    public bool GetMainAudioParameter() { return (audioSettings.MainAudioVolume > 0); }
    public bool GetAudioEffectsParameter() { return (audioSettings.AudioEffectsVolume > 0); }
}
