using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;
using System.Collections;

public class SkinPicker : MonoBehaviour
{
    List<Button> buttons = new List<Button>();
    [SerializeField] GameObject skinPrefab;
    [SerializeField] Transform grid;
    [SerializeField] Button useCustomSkinButton;
    [SerializeField] Image customSkinImage;

    private void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        if (SkinInfoHolder.instance.customSkin != null)
            customSkinImage.sprite = SkinInfoHolder.instance.customSkin;

        for (int i = 0; i < SkinInfoHolder.instance.skins.Length; i++)
        {
            GameObject createdSkin = Instantiate(skinPrefab, grid);
            createdSkin.GetComponent<Image>().sprite = SkinInfoHolder.instance.skins[i];
            int localI = i;
            createdSkin.GetComponentInChildren<Button>().onClick.AddListener(delegate {PickSkin(localI);});
            buttons.Add(createdSkin.GetComponentInChildren<Button>());
        }
        UpdateButtons();
    }

    public void PickSkin(int index)
    {
        PlayerPrefs.SetInt("skin", index);
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        if (PlayerPrefs.GetInt("skin") != -1)
        {
            useCustomSkinButton.interactable = true;
            useCustomSkinButton.GetComponentInChildren<TMP_Text>().text = "use";
        } else
        {
            useCustomSkinButton.interactable = false;
            useCustomSkinButton.GetComponentInChildren<TMP_Text>().text = "current";
        }
        for (int i = 0; i < buttons.Count; i++) 
        {
            if (PlayerPrefs.GetInt("skin") == i)
            {
                buttons[i].interactable = false;
                buttons[i].GetComponentInChildren<TMP_Text>().text = "current";
            } else
            {
                buttons[i].interactable = true;
                buttons[i].GetComponentInChildren<TMP_Text>().text = "use";
            }
        }
    }

    public void StartSelectingCustomSkin()
    {
        StartCoroutine("SelectCustomSkin");
    }

    public IEnumerator SelectCustomSkin()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select", "Select");
        if (FileBrowser.Success)
        {
            var fileContent = File.ReadAllBytes(FileBrowser.Result[0]);
            Texture2D customSkinTexture = new Texture2D(256, 256);
            ImageConversion.LoadImage(customSkinTexture, fileContent);
            if (customSkinTexture.width != customSkinTexture.height)
            {
                StopCoroutine("SelectCustomSkin");
            }
            Rect rect = new Rect(0, 0, customSkinTexture.width, customSkinTexture.height);
            SkinInfoHolder.instance.customSkin = Sprite.Create(customSkinTexture, rect, Vector2.one * 0.5f, customSkinTexture.width > customSkinTexture.height ? customSkinTexture.width : customSkinTexture.height); 
            customSkinImage.sprite = SkinInfoHolder.instance.customSkin;
        }
    }
}
