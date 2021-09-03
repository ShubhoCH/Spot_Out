using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuLoader : MonoBehaviour
{
    public TMP_Text text;
    public Image level_bar;
    public GameObject[] stars;
    public Transform currentStars;
    int currentSetOfMaxLevel, currentMaxLevel;
    private void OnEnable()
    {
        if (PlayerPrefs.GetString("BG_MUSIC", "true") == "true")
            FindObjectOfType<AudioManager>().Play("Background");
        text.SetText(PlayerPrefs.GetString("NAME", "Player_Name"));
        currentSetOfMaxLevel = PlayerPrefs.GetInt("CURRENTSET", 1);
        currentMaxLevel = PlayerPrefs.GetInt("CURRENTMAXLEVEL", 1);
        level_bar.fillAmount = (float)(currentSetOfMaxLevel - 1) / (float)((currentMaxLevel * 2) + 1);
        for (int i = 0; i < currentMaxLevel; i++)
        {
            GameObject gm = Instantiate(stars[3]);
            gm.transform.SetParent(currentStars);
            gm.transform.localPosition = new Vector3(80 * i, 0, 0);
            gm.transform.localScale = Vector3.one;
        }
        for (int i = 1; i <= 6; i++)
        {
            string level = "LEVEL" + i.ToString();
            int noOfStars = PlayerPrefs.GetInt(level, 0);
            GameObject gm = transform.GetChild(1).transform.Find(i.ToString()).gameObject;
            if (i <= currentMaxLevel)
            {
                Image image = gm.transform.GetComponent<Image>();
                var temp = image.color;
                temp.a = 255f;
                image.color = temp;
                gm.transform.GetChild(1).gameObject.SetActive(false);
                gm.transform.GetChild(3).gameObject.SetActive(true);
                if (noOfStars != 0)
                {
                    GameObject gmm = Instantiate(stars[noOfStars - 1]);
                    gmm.transform.SetParent(gm.transform.GetChild(2).transform);
                    gmm.transform.localScale = Vector3.one;
                    gmm.transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                Image image = gm.transform.GetComponent<Image>();
                var temp = image.color;
                temp.a = 1;
                image.color = temp;
                gm.transform.GetChild(1).gameObject.SetActive(true);
                gm.transform.GetChild(3).gameObject.SetActive(false);
                gm.transform.GetComponent<Button>().interactable = false;
            }
        }
        if (currentMaxLevel != 7)
            StartCoroutine(Blink(currentMaxLevel));
    }
    public void Load()
    {
        GameObject gm = transform.GetChild(1).transform.Find(PlayerPrefs.GetInt("CURRENTMAXLEVEL", 1).ToString()).gameObject;
        gm.transform.GetComponent<Button>().interactable = true;
    }
    IEnumerator Blink(int currentLevel)
    {
        GameObject gm = transform.GetChild(1).transform.Find(currentLevel.ToString()).gameObject;
        Vector2 scale;
        for (int i = 0; ; i++)
        {
            if (i % 2 != 0)
                scale = new Vector2(0.7425f, 0.825f);
            else
                scale = new Vector2(0.72f, 0.8f);
            gm.transform.localScale = scale;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
