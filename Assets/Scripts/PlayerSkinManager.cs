using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("skin") == -1)
        {
            GetComponent<SpriteRenderer>().sprite = SkinInfoHolder.instance.customSkin;
        } 
        else
        {
            GetComponent<SpriteRenderer>().sprite = SkinInfoHolder.instance.skins[PlayerPrefs.GetInt("skin", 0)];
        }
    }
    
    public Sprite GetCurrentSkin()
    {
        if (PlayerPrefs.GetInt("skin") == -1) return SkinInfoHolder.instance.customSkin;
        return SkinInfoHolder.instance.skins[PlayerPrefs.GetInt("skin", 0)];
    }
}
