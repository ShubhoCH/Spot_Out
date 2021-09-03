using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public Transform sfx_circle, bg_circle;
    float add;
    private void OnEnable()
    {
        add = 0;
        this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
    private void Start()
    {
        if (PlayerPrefs.GetString("SFX", "true") == "false")
            sfx_circle.localPosition = new Vector3(-1 * transform.localPosition.x, 0, 0);
        if (PlayerPrefs.GetString("BG_MUSIC", "true") == "false")
            bg_circle.localPosition = new Vector3(-1 * transform.localPosition.x, 0, 0);
    }
    private void Update()
    {
        if (transform.localScale.x < 0.8f)
        {
            add += Time.deltaTime;
            this.transform.localScale = new Vector3(add + 0.4f, add + 0.4f, add + 0.4f);
        }
    }
    public void SFX()
    {
        if (PlayerPrefs.GetString("SFX", "true") == "true")
        {
            PlayerPrefs.SetString("SFX", "false");
            sfx_circle.localPosition = new Vector3(-29, 0, 0);
        }
        else
        {
            PlayerPrefs.SetString("SFX", "true");
            sfx_circle.localPosition = new Vector3(29, 0, 0);
        }
    }
    public void BG_Music()
    {
        if (PlayerPrefs.GetString("BG_MUSIC", "true") == "true")
        {
            PlayerPrefs.SetString("BG_MUSIC", "false");
            FindObjectOfType<AudioManager>().Stop("Background");
            bg_circle.localPosition = new Vector3(-29, 0, 0);
        }
        else
        {
            PlayerPrefs.SetString("BG_MUSIC", "true");
            FindObjectOfType<AudioManager>().Play("Background");
            bg_circle.localPosition = new Vector3(29, 0, 0);
        }
    }
}
