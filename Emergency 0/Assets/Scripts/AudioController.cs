using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    //* Varijable za Unity
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle musicToggle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //* Check if there is saved audio volume data
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            LoadAudioVolumeData();
        }
        else
        {
            SetAudioVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAudioVolume()
    {
        //* Set the audio volume to the slider value
        backgroundAudioSource.volume = musicSlider.value;

        //* Set the audio mute state to the toggle value
        backgroundAudioSource.mute = !musicToggle.isOn;

        //* Save the audio volume data to PlayerPrefs class
        PlayerPrefs.SetFloat("audioVolume", musicSlider.value);
    }

    public void VolumeMuter()
    {
        if (backgroundAudioSource.mute == true)
        {
            //* Unmute the audio
            backgroundAudioSource.mute = false;

            //* Save the audio volume data to PlayerPrefs class
            PlayerPrefs.SetInt("audioVolumeMuted", (backgroundAudioSource.mute ? 1 : 0));

            //* LOG
            Debug.Log("Audio unmuted.");
        }
        else
        {
            //* Mute the audio
            backgroundAudioSource.mute = true;

            //* Save the audio volume data to PlayerPrefs class
            PlayerPrefs.SetInt("audioVolumeMuted", (backgroundAudioSource.mute ? 1 : 0));

            //* LOG
            Debug.Log("Audio muted.");
        }
    }

    public void LoadAudioVolumeData()
    {
        //* Get the saved audio volume data and apply it to slider
        musicSlider.value = PlayerPrefs.GetFloat("audioVolume");
        musicToggle.isOn = !(PlayerPrefs.GetInt("audioVolumeMuted") != 0);

        SetAudioVolume();
        
        //* LOG
        Debug.Log("Loaded audio volume data from PlayerPrefs class.");
    }
}
