using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    int Level;
    string nameOfButtonClicked;
    public levelLoderScript loadLevel;
    public void OnPress()
    {
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("UI_Click");
        nameOfButtonClicked = transform.name;
        switch (nameOfButtonClicked)
        {
            case "1":
                Level = 1;
                break;
            case "2":
                Level = 2;
                break;
            case "3":
                Level = 3;
                break;
            case "4":
                Level = 4;
                break;
            case "5":
                Level = 5;
                break;
            case "6":
                Level = 6;
                break;
        }
        loadLevel.LoadLevelAnimation(Level);
    }
}
