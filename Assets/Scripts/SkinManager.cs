using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr;  // The SpriteRenderer to display the selected skin
    public List<Sprite> skins = new List<Sprite>();  // List of skins (populated in the inspector)
    private int selectedSkin = 0;  // The currently selected skin index
    public GameObject playerskin;


    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count - 1;

        }
        sr.sprite = skins[selectedSkin];
    }

    public void PlayGame()
    {
        // Save the selected skin index in PlayerPrefs
        PrefabUtility.SaveAsPrefabAsset(playerskin, "Assets/NFTs/selectedskin.prefab");
        Debug.Log("Selected Skin: " + selectedSkin);

        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }
}