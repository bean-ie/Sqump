using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] PostProcessingSwitch ppSwitch;
    [SerializeField] TMP_Dropdown coloringRandomnessDropdown, postProcessingDropdown;
    [SerializeField] Slider soundSlider;


    private void Start()
    {
        if (coloringRandomnessDropdown)
            coloringRandomnessDropdown.value = PlayerPrefs.GetInt("coloringrandomness");
        if (postProcessingDropdown)
            postProcessingDropdown.value = PlayerPrefs.GetInt("postprocessing");
        if (soundSlider)
            soundSlider.value = PlayerPrefs.GetFloat("sound", 1);
    }

    public void ChangeSetting(string setting)
    {
        switch (setting.ToLower())
        {
            case "coloringrandomness":
                {
                    PlayerPrefs.SetInt(setting, coloringRandomnessDropdown.value);
                    return;
                }
            case "postprocessing":
                {
                    PlayerPrefs.SetInt(setting, postProcessingDropdown.value);
                    ppSwitch.SwitchPostProcessing();
                    return;
                }
            case "sound":
                {
                    PlayerPrefs.SetFloat(setting, soundSlider.value);
                    AudioListener.volume = PlayerPrefs.GetFloat(setting, 1);
                    return;
                }
            default: return;
        }
    }
}
