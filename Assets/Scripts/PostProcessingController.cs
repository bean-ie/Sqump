using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;
using Cinemachine.PostFX;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField]
    CinemachinePostProcessing cinemachinePP;
    [SerializeField] float randomizeContrastChance = 0.5f;
    PostProcessProfile profile;
    ColorGrading colorGrading;
    ChromaticAberration chromaticAberration;
    LensDistortion lensDistortion;
    private void Start()
    {
        profile = cinemachinePP.m_Profile;
        if (PlayerPrefs.GetInt("postprocessing") == 1)
            cinemachinePP.enabled = false;
        profile.TryGetSettings<ColorGrading>(out colorGrading);
        profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        profile.TryGetSettings<LensDistortion>(out lensDistortion);
        if (LevelInfoHolder.instance.chosenLevel.dontRandomizePostProcessing || PlayerPrefs.GetInt("coloringrandomness") == 0)
        {
            colorGrading.hueShift.value = 0;
            colorGrading.saturation.value = 0;
            colorGrading.contrast.value = 0;
        } else
        {
            RandomizeEverything();
        }
    }

    public void RandomizeEverything()
    {
        if (LevelInfoHolder.instance.chosenLevel.dontRandomizePostProcessing || PlayerPrefs.GetInt("coloringrandomness") == 0)
        {
            colorGrading.hueShift.value = 0;
            colorGrading.saturation.value = 0;
            colorGrading.contrast.value = 0;
            return;
        }
        RandomizeHueShift();
        RandomizeSaturation();
        RandomizeContrast();
    }

    public void RandomizeHueShift()
    {
        if (PlayerPrefs.GetInt("coloringrandomness") == 1)
        {
            colorGrading.hueShift.value = Random.Range(-40f, 40f);
            return;
        }
        colorGrading.hueShift.value = Random.Range(-180f, 180f);

    }
    public void RandomizeSaturation()
    {
        if (PlayerPrefs.GetInt("coloringrandomness") == 1)
        {
            colorGrading.saturation.value = Random.Range(-40f, 40f);
            return;
        }
        colorGrading.saturation.value = Random.Range(-100f, 100f);
    }
    public void RandomizeContrast()
    {
        if (PlayerPrefs.GetInt("coloringrandomness") == 1)
        {
            colorGrading.contrast.value = 0;
            return;
        }
            if (Random.value > randomizeContrastChance)
            colorGrading.contrast.value = Random.Range(-30f, 100f);
        else
            colorGrading.contrast.value = 0;
    }

    public void SetScoreGlitches(float score)
    {
        chromaticAberration.intensity.value = Mathf.Clamp((score / 6f) * 0.4f, 0, 0.4f);
        lensDistortion.intensity.value = Mathf.Clamp(score / 6f, 0, 1) * -25;
    }
}
