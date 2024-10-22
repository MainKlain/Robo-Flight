using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSkinManagerScript : MonoBehaviour
{
    public List<Sprite> skins;  // List of all available skins (assigned in the inspector)
    public GameObject Robot;    // The player GameObject
    private int selectedSkinIndex;

    // Use Awake() to load the PlayerPrefs early
    void Awake()
    {
        // Retrieve the selected skin index from PlayerPrefs
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", -1);  // -1 if not found
        Debug.Log("Loaded Skin Index from PlayerPrefs in Awake: " + selectedSkinIndex);
    }

    // Use Start() to apply the skin after all objects have been initialized
    void Start()
    {
        Debug.Log("GameSkinManagerScript Start is called");

        // Check if the skin index is valid and the skins list is ready
        if (selectedSkinIndex >= 0 && selectedSkinIndex < skins.Count)
        {
            Debug.Log("Applying selected skin: " + selectedSkinIndex);
            Robot.GetComponent<SpriteRenderer>().sprite = skins[selectedSkinIndex];
        }
        else
        {
            Debug.LogError("Selected skin index is out of range or not set properly!");
        }
    }
}