using System.Collections;
using UnityEngine;
using TMPro;

public class levelLoderScript : MonoBehaviour
{
    public Manager manager; 
    public Animator emag;
    public float setTransitionTime;
    public GameObject inputField;
    int demo;
    private void Awake() => Application.targetFrameRate = 24;
    private void Start()
    {
        demo = PlayerPrefs.GetInt("DEMO", 0);
        if (demo == 0)
            manager.SetEverythingForDemo();
        else
            manager.SetEverythingAfterDemoDone();
    }
    public void afterNameSave()
    {
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("UI_Click");
        demo = PlayerPrefs.GetInt("DEMO", 0);
        string name;
        name = inputField.GetComponent<TMP_Text>().text;
        if (name.Length > 1)
        {
            PlayerPrefs.SetString("NAME", name);
            if (demo == 0)
                LoadLevelAnimation(0);
            else
                manager.SetEverythingAfterDemoDone();
        }
        else
            Handheld.Vibrate();
    }
    public void LoadLevelAnimation(int Level)
    {
        FindObjectOfType<AudioManager>().Stop("Background");
        StartCoroutine(LoadLevelChangeAnim(Level));
    }
    IEnumerator LoadLevelChangeAnim(int Level)
    {
        emag.SetTrigger("SetChange");
        yield return new WaitForSeconds(setTransitionTime);
        if (Level != 0)
            manager.SelectLevelandSet(Level);
        else
            manager.PlayDemo();
        Timer();
    }
    public void LoadSetChangeAnimation()
    {
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("UI_Click");
        FindObjectOfType<AudioManager>().Stop("Background");
        StartCoroutine(LoadSetChangeAnim());
    }
    IEnumerator LoadSetChangeAnim()
    {
        emag.SetTrigger("SetChange");
        yield return new WaitForSeconds(setTransitionTime);
        manager.LoadNextLevelorSet();
        Timer();
    }
    public void LoadGoToMenuAnimation()
    {
        if (PlayerPrefs.GetInt("DEMO", 0) != 0)
        {
            if (PlayerPrefs.GetString("SFX", "true") == "true")
                FindObjectOfType<AudioManager>().Play("UI_Click");
            FindObjectOfType<AudioManager>().Stop("Background");
            StartCoroutine(MenuAnim());
        }
        else
            Handheld.Vibrate();
    }
    IEnumerator MenuAnim()
    {
        emag.SetTrigger("SetChange");
        yield return new WaitForSeconds(setTransitionTime);
        manager.GoToMenu();
    }
    void Timer()
    {
        StartCoroutine(StartTimerAfterLoad());
    }
    IEnumerator StartTimerAfterLoad()
    {
        yield return new WaitForSeconds(setTransitionTime);
        if (PlayerPrefs.GetString("BG_MUSIC", "true") == "true")
            FindObjectOfType<AudioManager>().Play("Background");
        manager.StartTimer();
    }
    public void Match() => emag.SetTrigger("Match");
}
