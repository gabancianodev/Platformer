using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : UIManager {

    public Image SoundToggleImage;
    [Space]
    public Sprite SoundToggleOn;
    public Sprite SoundToggleOff;

    private void Start()
    {
        LoadSoundToggleSettings();
    }

    void LoadSoundToggleSettings()
    {
        int audioEnabled = PlayerPrefs.GetInt("SETTINGS_AUDIO_ENABLED", 1);
        if(audioEnabled == 1)
        {
            AudioListener.pause = false;
            SoundToggleImage.sprite = SoundToggleOn;
        }
        else if (audioEnabled == 0)
        {
            AudioListener.pause = true;
            SoundToggleImage.sprite = SoundToggleOff;
        }
    }

    public void ToggleSound()
    {
        if (!AudioListener.pause)
        {
            AudioListener.pause = true;
            SoundToggleImage.sprite = SoundToggleOff;
            PlayerPrefs.SetInt("SETTINGS_AUDIO_ENABLED", 0);
        } 
        else if (AudioListener.pause)
        {
            AudioListener.pause = false;
            SoundToggleImage.sprite = SoundToggleOn;
            PlayerPrefs.SetInt("SETTINGS_AUDIO_ENABLED", 1);
        }
    }
}
