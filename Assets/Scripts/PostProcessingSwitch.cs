using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingSwitch : MonoBehaviour
{

    private void Start()
    {
        SwitchPostProcessing();
    }
    
    public void SwitchPostProcessing()
    {
        if (PlayerPrefs.GetInt("postprocessing") == 1)
        {
            FindObjectOfType<PostProcessLayer>().enabled = false;
        } else
        {
            FindObjectOfType<PostProcessLayer>().enabled = true;
        }
    }
}
