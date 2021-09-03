using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Manager : MonoBehaviour
{
    //    PlayerPrefab currentSet, currentLevel;
    public Image level_bar;
    public Slider_Canvas slider;
    public levelLoderScript animations;
    float snappingLimit, timeLimitSeconds;
    bool rewardTime, rewardHint, match, startTimer, won, justPromoted;
    public GameObject menuCanvas, gameCanvas, popupCanvas, nameCanvas, star, levelClearPopup;
    public Transform Question, vs1, vs2, hs1, hs2, vsPivot, hsPivot, currentStars;
    public TMP_Text question_Text, timer_Text, level_Complete_Time_Text, player_Name;
    string currentAnswers, x, y, stringToBeReplacedWithIfMatched, foundWordCode, keepNameOfImage;
    int no_of_gameplays, timeLimitMinutes_Second, currentLevel, timeLimitMinutes, currentMaxLevel, currentSet, currentSetOfMaxLevel, currentGamePlayNumber, totalNoOfWords, numberOfWordsFound, addIndex, noOfCharToCheck, hint, subtractIfTimeInMinutesOnly;
    public float[] snappingLimits;
    public GameObject[] gamePlays;
    private void Awake() => no_of_gameplays = 0; 
    void Start()
    {
        //      Use PlayerPrefab to get the current level Player is in, if the current Level is Equal to the level player
        //      Wants to play then take him to the last set he was playing in that level otherwise start from the first
        //      Set in that level;
        startTimer = false;
        won = false;
        rewardTime = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public void Update()
    {
        if(startTimer == true)
        {
            timeLimitSeconds += Time.deltaTime;
            if((59 - (((int)timeLimitSeconds + timeLimitMinutes_Second) % 60)) >= 10)
                timer_Text.SetText((timeLimitMinutes - (int)((timeLimitSeconds + timeLimitMinutes_Second) / 60) - subtractIfTimeInMinutesOnly).ToString() + ":" + (59 - (((int)timeLimitSeconds + timeLimitMinutes_Second) % 60)).ToString());
            else
                timer_Text.SetText((timeLimitMinutes - (int)((timeLimitSeconds + timeLimitMinutes_Second) / 60) - subtractIfTimeInMinutesOnly).ToString() + ":" + "0" + (59 - (((int)timeLimitSeconds + timeLimitMinutes_Second) % 60)).ToString());
            if ((timeLimitMinutes - (int)((timeLimitSeconds + timeLimitMinutes_Second ) / 60) - subtractIfTimeInMinutesOnly) == 0 && (59 - (((int)timeLimitSeconds + timeLimitMinutes_Second) % 60)) == 0)
            {
                startTimer = false;
                //GameOver;
                popupCanvas.SetActive(true);
                popupCanvas.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else if(rewardTime == true)
        {
            timeLimitSeconds += Time.deltaTime; 
            if ((59 - ((int)timeLimitSeconds % 60)) >= 10)
                timer_Text.SetText("00" + ":" + (59 - (int)(timeLimitSeconds % 60)).ToString());
            else
                timer_Text.SetText("00" + ":" + "0" + (59 - ((int)timeLimitSeconds % 60)).ToString());
            if (timeLimitSeconds >= 60)
            {
                rewardTime = false;
                timer_Text.SetText("00" + ":" + "00");
                popupCanvas.SetActive(true);
                popupCanvas.transform.GetChild(8).gameObject.SetActive(true);
            }
        }
    }
    public void Check()
    {
        match = false;
        if(currentLevel != 6)
        {
            for (int i = 0; i < totalNoOfWords - numberOfWordsFound && match == false; i++)
            {
                //Debug.Log("Search: " + currentAnswers.Substring((i * 4), 4));
                stringToBeReplacedWithIfMatched = currentAnswers.Substring(0, (i * 4) * noOfCharToCheck) + currentAnswers.Substring(4 * noOfCharToCheck + (i * 4) * noOfCharToCheck, (currentAnswers.Length) - (i * 4) * noOfCharToCheck - 4 * noOfCharToCheck);
                string s = currentAnswers.Substring(i * 4 * noOfCharToCheck, 4 * noOfCharToCheck);
                x = currentAnswers.Substring((i * 4 * noOfCharToCheck), 1 * noOfCharToCheck);
                y = currentAnswers.Substring((i * 4 * noOfCharToCheck) + 1 * noOfCharToCheck, 1 * noOfCharToCheck);
                if ((int)(vs1.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(x))
                {
                    if ((int)(vs2.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(y))
                    {
                        x = currentAnswers.Substring((i * 4 * noOfCharToCheck) + 2 * noOfCharToCheck, 1 * noOfCharToCheck);
                        y = currentAnswers.Substring((i * 4 * noOfCharToCheck) + 3 * noOfCharToCheck, 1 * noOfCharToCheck);
                        if (((int)(hs1.transform.localPosition.x / snappingLimit)) + addIndex == int.Parse(x))
                        {
                            if (((int)(hs2.transform.localPosition.x / snappingLimit)) + addIndex == int.Parse(y))
                            {
                                match = true;
                                foundWordCode = currentAnswers.Substring((i * 4 * noOfCharToCheck), 4 * noOfCharToCheck);
                                currentAnswers = stringToBeReplacedWithIfMatched;
                            }
                        }
                        else if (((int)(hs1.transform.localPosition.x / snappingLimit)) + addIndex == int.Parse(y))
                        {
                            if (((int)(hs2.transform.localPosition.x / snappingLimit)) + addIndex == int.Parse(x))
                            {
                                match = true;
                                foundWordCode = currentAnswers.Substring((i * 4 * noOfCharToCheck), 4 * noOfCharToCheck);
                                currentAnswers = stringToBeReplacedWithIfMatched;
                            }
                        }
                    }
                }
                else if ((int)(vs1.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(y))
                {
                    if ((int)(vs2.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(x))
                    {
                        x = currentAnswers.Substring((i * 4 * noOfCharToCheck) + 2 * noOfCharToCheck, 1 * noOfCharToCheck);
                        y = currentAnswers.Substring((i * 4 * noOfCharToCheck) + 3 * noOfCharToCheck, 1 * noOfCharToCheck);
                        if ((int)(hs1.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(x))
                        {
                            if ((int)(hs2.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(y))
                            {
                                match = true;
                                foundWordCode = currentAnswers.Substring((i * 4) * noOfCharToCheck, 4 * noOfCharToCheck);
                                currentAnswers = stringToBeReplacedWithIfMatched;
                            }
                        }
                        else if ((int)(hs1.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(y))
                        {
                            if ((int)(hs2.transform.localPosition.x / snappingLimit) + addIndex == int.Parse(x))
                            {
                                match = true;
                                foundWordCode = currentAnswers.Substring((i * 4) * noOfCharToCheck, 4 * noOfCharToCheck);
                                currentAnswers = stringToBeReplacedWithIfMatched;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            float a, b, c, d;
            a = (vs1.transform.localPosition.x / snappingLimit) + addIndex;
            b = (vs2.transform.localPosition.x / snappingLimit) + addIndex;
            c = (hs1.transform.localPosition.x / snappingLimit) + addIndex;
            d = (hs2.transform.localPosition.x / snappingLimit) + addIndex;
            string name = "";
            string name2 = "";
            string name3 = "";
            string name4 = "";
            if (a < 10)
                name = string.Format("{0}{1}{2}", name, "0", a.ToString());
            else
                name = string.Format("{0}{1}", name, a.ToString());
            if (b < 10)
                name = string.Format("{0}{1}{2}", name, "0", b.ToString());
            else
                name = string.Format("{0}{1}", name, b.ToString());
            if (c < 10)
                name = string.Format("{0}{1}{2}", name, "0", c.ToString());
            else
                name = string.Format("{0}{1}", name, c.ToString());
            if (d < 10)
                name = string.Format("{0}{1}{2}", name, "0", d.ToString());
            else
                name = string.Format("{0}{1}", name, d.ToString());
            name2 = string.Format("{0}{1}{2}", name.Substring(0, 4), name.Substring(6, 2), name.Substring(4, 2));
            name3 = string.Format("{0}{1}{2}", name.Substring(2, 2), name.Substring(0, 2), name.Substring(4, 4));
            name4 = string.Format("{0}{1}{2}{3}", name.Substring(2, 2), name.Substring(0, 2), name.Substring(6, 2), name.Substring(4, 2));
            for (int i = 0; i < totalNoOfWords; i++)
            {
                Debug.Log(i);
                if (name == Question.GetChild(0).GetChild(i).gameObject.name)
                {
                    if (!Question.GetChild(0).GetChild(i).gameObject.activeSelf)
                    {
                        match = true;
                        numberOfWordsFound++;
                        StartCoroutine(MatchLevel6(Question.GetChild(0).GetChild(i).gameObject));
                    }
                }
                else if (name2 == Question.GetChild(0).GetChild(i).gameObject.name)
                {
                    if (!Question.GetChild(0).GetChild(i).gameObject.activeSelf)
                    {
                        match = true;
                        numberOfWordsFound++;
                        StartCoroutine(MatchLevel6(Question.GetChild(0).GetChild(i).gameObject));
                    }
                }
                else if (name3 == Question.GetChild(0).GetChild(i).gameObject.name)
                {
                    if (!Question.GetChild(0).GetChild(i).gameObject.activeSelf)
                    {
                        match = true;
                        numberOfWordsFound++;
                        StartCoroutine(MatchLevel6(Question.GetChild(0).GetChild(i).gameObject));
                    }
                }
                else if(name4 == Question.GetChild(0).GetChild(i).gameObject.name)
                {
                    if (!Question.GetChild(0).GetChild(i).gameObject.activeSelf)
                    {
                        match = true;
                        numberOfWordsFound++;
                        StartCoroutine(MatchLevel6(Question.GetChild(0).GetChild(i).gameObject));
                    }
                }
            }
        }
        if (match == true)
        {
            if(currentLevel != 6)
                StartCoroutine(Match());
            animations.Match();
            if(currentLevel != 6)
                numberOfWordsFound++;
            //gm.GetComponent<SpriteRenderer>().sortingOrder = numberOfWordsFound;
            if (numberOfWordsFound == totalNoOfWords)
            {
                //Enters only if the Player finds all the words!
                won = true;
                StartCoroutine(Win());
                startTimer = false;
            }
        }
    }
    IEnumerator Match()
    {
        yield return new WaitForSeconds(0.3f);
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("Match");
        GameObject gm = GameObject.Find("GamePlayCanvas/GamePlay/Question/" + keepNameOfImage + "/" + foundWordCode);
        gm.SetActive(true);
        gm.transform.SetSiblingIndex(numberOfWordsFound);
        string x = question_Text.text;
        x = x.Replace(gm.transform.GetChild(0).gameObject.name + "  ","");
        question_Text.SetText(x);
    }
    IEnumerator MatchLevel6(GameObject gm)
    {
        yield return new WaitForSeconds(0.3f);
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("Match");
        gm.SetActive(true);
        gm.transform.SetSiblingIndex(numberOfWordsFound);
        string x = question_Text.text;
        x = x.Replace(gm.transform.GetChild(0).gameObject.name + "  ", "");
        question_Text.SetText(x);
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(1.2f);
        Handheld.Vibrate();
        popupCanvas.SetActive(true);
        if (currentMaxLevel == 6 && PlayerPrefs.GetInt("CURRENTSET", 1) == 13)
            LoadNextLevelorSet();
        else
        {
            int completedIn = (timeLimitMinutes * 60 - (int)(timeLimitSeconds));
            if (completedIn > 10 * (currentLevel + 2))
                popupCanvas.transform.GetChild(4).gameObject.SetActive(true);
            else if (completedIn > 10 * (currentLevel + 1))
                popupCanvas.transform.GetChild(5).gameObject.SetActive(true);
            else
                popupCanvas.transform.GetChild(6).gameObject.SetActive(true);
        }
        //Activate Level 6 Completed canvas;
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("Win");
    }
    public int CheckIfSliderIsPositionedCorrectlyOrNot(Transform t)
    {
        int ans = 0;
        if(currentLevel != 0)
        {
            if (((t.transform.localPosition.x / snappingLimit) + addIndex) < 1)
                ans = -1;
            else if (((t.transform.localPosition.x / snappingLimit) + addIndex) > currentLevel + 4)
                ans = 1;
        }
        else
        {
            if (((int)(t.transform.localPosition.x / snappingLimit) + addIndex) < 1)
                ans = -1;
            else if (((int)(t.transform.localPosition.x / snappingLimit) + addIndex) > currentLevel + 5)
                ans = 1;
        }
        return ans;
    }
    public void CorrectPos(Transform t)
    {
        if (currentLevel != 0)
        {
            if (((t.transform.localPosition.x / snappingLimit) + addIndex) < 1)
                t.localPosition = new Vector3(-snappingLimit * (addIndex - 1), 0, 0);
            else if (((t.transform.localPosition.x / snappingLimit) + addIndex) > currentLevel + 4)
            {
                if (currentLevel % 2 != 0)
                    t.localPosition = new Vector3(snappingLimit * (addIndex - 1), 0, 0);
                else
                    t.localPosition = new Vector3(snappingLimit * addIndex, 0, 0);
            }
        }
        else
        {
            if (((int)(t.transform.localPosition.x / snappingLimit) + addIndex) < 1)
                t.localPosition = new Vector3(-snappingLimit * (addIndex - 1), 0, 0);
            else if (((int)(t.transform.localPosition.x / snappingLimit) + addIndex) > currentLevel + 5)
                t.localPosition = new Vector3(snappingLimit * (addIndex - 1), 0, 0);
        }
        Check();
    }
    void ShowWords(GameObject gm, int totalNoWords)
    {
        string question;
        question = "";
        for(int i = 0; i < totalNoOfWords; i++)
        {
            question += gm.transform.GetChild(i).GetChild(0).name;
            //if(i != totalNoOfWords - 1)
            question += "  ";
        }
        question_Text.SetText(question);
    }
    public void GetQuestion()
    {
        justPromoted = false;
        rewardHint = false;
        rewardTime = false;
        no_of_gameplays++;
        won = false;
        player_Name.SetText(PlayerPrefs.GetString("NAME", "Player_Name"));
        level_bar.fillAmount = (float)(currentSetOfMaxLevel - 1) / (float)((currentMaxLevel * 2) + 1);
        for (int i = 0; i < currentMaxLevel; i++)
        {
            GameObject gmm = Instantiate(star);
            gmm.transform.parent = currentStars;
            gmm.transform.localPosition = new Vector3(80 * i, 0, 0);
            gmm.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (currentLevel != 0)
            Question.gameObject.GetComponent<Demo>().enabled = false;
        if (currentLevel == 0 || currentLevel == 1 || currentLevel == 2)
            addIndex = 3;
        else if (currentLevel == 3 || currentLevel == 4)
            addIndex = 4;
        else
            addIndex = 5;
        if (currentLevel != 6)
            noOfCharToCheck = 1;
        else
            noOfCharToCheck = 2;
        vsPivot.gameObject.SetActive(true);
        hsPivot.gameObject.SetActive(true);
        if (currentLevel == 0)
            totalNoOfWords = 3;
        else if (currentLevel == currentMaxLevel)
            totalNoOfWords = currentMaxLevel + 2;
        else
            totalNoOfWords = currentLevel + 2;    //PlayerPref(CurrentLevel);
        numberOfWordsFound = 0;
        GameObject gm = Instantiate(gamePlays[currentGamePlayNumber]);
        gm.transform.SetParent(Question);
        gm.transform.localPosition = Vector3.zero;
        gm.transform.localScale = Vector3.one;
        currentAnswers = gamePlays[currentGamePlayNumber].transform.name;
        keepNameOfImage = currentAnswers;
        gm.transform.name = currentAnswers;
        vs1.gameObject.SetActive(true);
        vs2.gameObject.SetActive(true);
        hs1.gameObject.SetActive(true);
        hs2.gameObject.SetActive(true);
        ShowWords(gm, totalNoOfWords);
        hint = PlayerPrefs.GetInt("HINT", 0);
        if (no_of_gameplays % 3 == 0)
            Invoke("Interstitial", 1.5f);
        else if(no_of_gameplays % 5 == 0)
            Invoke("Video", 1.5f);
        if (PlayerPrefs.GetInt("CURRENTMAXLEVEL", 0) < 2)
            ShowInstructions();
    }
    public void AfterInterstitialClosed() => startTimer = true;
    public void PlayDemo()
    {
        SetEverythingAfterDemoDone();
        PlayerPrefs.SetInt("HINT", 3);
        currentGamePlayNumber = 0;
        currentLevel = 0;
        currentSet = 1;
        gameCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        SetEveryThingForNewGane();
        GetQuestion();
        PlayerPrefs.SetInt("LEVEL1", 0);
        PlayerPrefs.SetInt("LEVEL2", 0);
        PlayerPrefs.SetInt("LEVEL3", 0);
        PlayerPrefs.SetInt("LEVEL4", 0);
        PlayerPrefs.SetInt("LEVEL5", 0);
        PlayerPrefs.SetInt("LEVEL6", 0);
        PlayerPrefs.SetFloat("TOTALTIME", 0);
    }
    public void SelectLevelandSet(int level)
    {
        currentMaxLevel = PlayerPrefs.GetInt("CURRENTMAXLEVEL", 1);
        currentSetOfMaxLevel = PlayerPrefs.GetInt("CURRENTSET", 1);
        startTimer = false;
        currentGamePlayNumber = 0;
        currentLevel = level;
        if (currentLevel == currentMaxLevel)
        {
            currentSet = currentSetOfMaxLevel;
            for (int i = 0; i < currentMaxLevel - 1; i++)
                currentGamePlayNumber += 3 + (i * 2);
            currentGamePlayNumber += currentSetOfMaxLevel;
        }
        else
        {
            currentSet = 1;
            for (int i = 0; i < currentLevel - 1; i++)
                currentGamePlayNumber += 3 + (i * 2);
            currentGamePlayNumber++;
        }
        gameCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        SetEveryThingForNewGane();
        GetQuestion();
    }
    public void LoadNextLevelorSet()
    {
        currentGamePlayNumber = 0;
        justPromoted = false;
        //  If current set == currentLevel*2 + 1;
        //      Change PlayerPref CURRENTLEVEL by +1 && CURRENTSET = 1;
        //      Change the Slider snapping Limit:
        //  Else change CURRENTSET Playerpref by +1;

        /////   ------  /////
        if (currentLevel == 0)
        {
            currentMaxLevel = 1;
            currentSetOfMaxLevel = 1;
            currentLevel = 1;
            currentSet = 1;
            currentGamePlayNumber = 1;
            PlayerPrefs.SetInt("DEMO", 1);
            justPromoted = true;
        }
        else if (currentLevel == currentMaxLevel)
        {
            if (currentSetOfMaxLevel == (currentLevel * 2) + 1)
            {
                if(currentLevel != 6)
                {
                    PlayerPrefs.SetInt("CURRENTMAXLEVEL", currentMaxLevel + 1);
                    currentMaxLevel++;
                    currentLevel++;
                    currentSetOfMaxLevel = 1;
                }
                currentSet = 1;
                PlayerPrefs.SetInt("CURRENTSET", 1);
                justPromoted = true;
                float currentTimeTakenForTheLevel;
                currentTimeTakenForTheLevel = PlayerPrefs.GetFloat("TOTALTIME", 0);
                PlayerPrefs.SetFloat("TOTALTIME", currentTimeTakenForTheLevel + timeLimitSeconds);
                float totalTimeforThisLevel;
                totalTimeforThisLevel = PlayerPrefs.GetFloat("TOTALTIME", 0);
                totalTimeforThisLevel = totalTimeforThisLevel / (((currentLevel - 1) * 2) + 1);
                float compareTime;
                compareTime = ((currentLevel - 1) * 30 + 30) / 6;
                string levelName = "LEVEL" + (currentLevel - 1).ToString();
                if (totalTimeforThisLevel < compareTime)
                    PlayerPrefs.SetInt(levelName, 3);
                else if(totalTimeforThisLevel < compareTime * 2)
                    PlayerPrefs.SetInt(levelName, 2);
                else
                    PlayerPrefs.SetInt(levelName, 1);
            }
            else if (currentSet == currentSetOfMaxLevel)
            {
                PlayerPrefs.SetInt("CURRENTSET", currentSet + 1);
                currentSetOfMaxLevel++;
                currentSet++;
                float currentTimeTakenForTheLevel;
                currentTimeTakenForTheLevel = PlayerPrefs.GetFloat("TOTALTIME", 0);
                PlayerPrefs.SetFloat("TOTALTIME", currentTimeTakenForTheLevel + timeLimitSeconds);
            }
            else
                currentSet++;
            for (int i = 0; i < currentMaxLevel - 1; i++)
                currentGamePlayNumber += 3 + (i * 2);
            currentGamePlayNumber += currentSet;
        }
        else
        {
            if (currentSet == (currentLevel * 2) + 1)
            {
                currentLevel++;
                currentSet = 1;
            }
            else
                currentSet++;
            for (int i = 0; i < currentLevel - 1; i++)
                currentGamePlayNumber += 3 + (i * 2);
            currentGamePlayNumber += currentSet;
        }
        if(justPromoted == true)
        {
            if (Question.childCount > 0)
                Destroy(Question.GetChild(0).gameObject);
            popupCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            levelClearPopup.SetActive(true);
            if(currentLevel == 1)
                level_Complete_Time_Text.SetText("You Cleared this level!");
            else if(currentLevel != 6)
                level_Complete_Time_Text.SetText("You cleared level " + (currentLevel - 1).ToString());
            else
                level_Complete_Time_Text.SetText("You cleared level " + (currentLevel).ToString());
            PlayerPrefs.SetFloat("TOTALTIME", 0);
        }
        else
        {
            SetEveryThingForNewGane();
            GetQuestion();
        }
    }
    public void RateApp()
    {
        GameObject gm = popupCanvas.transform.Find("Game_Complete").gameObject;
        gm.SetActive(false);
    }
    void SetEveryThingForNewGane()
    {
        if(Question.childCount > 0)
            Destroy(Question.GetChild(0).gameObject);
        vsPivot.gameObject.SetActive(false);
        hsPivot.gameObject.SetActive(false);
    }
    public float SendSnappingValue()
    {
        if(currentLevel == 0)
            snappingLimit = snappingLimits[0]/(currentLevel+4);
        else
            snappingLimit = snappingLimits[currentLevel-1] / (currentLevel + 3);
        if (currentLevel % 2 == 0 && currentLevel != 0)
        {
            vsPivot.transform.localPosition = new Vector3(-snappingLimit / 2, vsPivot.transform.localPosition.y, vsPivot.transform.localPosition.z);
            hsPivot.transform.localPosition = new Vector3(-snappingLimit / 2, hsPivot.transform.localPosition.y, hsPivot.transform.localPosition.z);
        }
        else
        {
            vsPivot.transform.localPosition = Vector3.zero;
            hsPivot.transform.localPosition = Vector3.zero;
            vs1.localPosition = new Vector3(-snappingLimit, 0, 0);
            vs2.localPosition = new Vector3(snappingLimit, 0, 0);
            hs1.localPosition = new Vector3(-snappingLimit, 0, 0);
            hs2.localPosition = new Vector3(snappingLimit, 0, 0);
        }
        return snappingLimit;
    }
    public float SendUpperLimit()
    {
        if (currentLevel % 2 == 0 && currentLevel != 0)
            return (snappingLimit * (addIndex) + 20f);
        else
            return (snappingLimit * (addIndex - 1) + 20f);
    }
    public float SendLowerLimit()
    {
        return (snappingLimit * (addIndex - 1) + 20f);
    }
    public void StartTimer()
    {
        timeLimitMinutes = ((currentLevel * 30) + 30) / 60;
        if (currentLevel == 0)
            timeLimitMinutes = 5;
        else if (((currentLevel * 30) + 30) % 60 == 0)
        {
            timeLimitMinutes_Second = 0;
            subtractIfTimeInMinutesOnly = 1;
        }
        else
        {
            timeLimitMinutes_Second = 30;
            subtractIfTimeInMinutesOnly = 0;
        }
        timeLimitSeconds = 0;
        startTimer = true;
        timer_Text.SetText(timeLimitMinutes.ToString() + ":" + timeLimitMinutes_Second.ToString());
    }
    public void GoToMenu()
    {
        if (currentLevel == 0)
        {
            currentMaxLevel = 1;
            currentSetOfMaxLevel = 1;
            currentLevel = 1;
            currentSet = 1;
            currentGamePlayNumber = 1;
            PlayerPrefs.SetInt("DEMO", 1);
            popupCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            levelClearPopup.SetActive(true);
            level_Complete_Time_Text.SetText("You Cleared this level!");
            PlayerPrefs.SetFloat("TOTALTIME", 0);
        }
        else if (currentLevel == currentMaxLevel && won == true && PlayerPrefs.GetInt("DEMO", 0) != 0)
        {
            if (currentSetOfMaxLevel == (currentMaxLevel * 2) + 1)
            {
                PlayerPrefs.SetInt("CURRENTMAXLEVEL", currentMaxLevel + 1);
                PlayerPrefs.SetInt("CURRENTSET", 1); 
                float currentTimeTakenForTheLevel;
                currentTimeTakenForTheLevel = PlayerPrefs.GetFloat("TOTALTIME", 0);
                PlayerPrefs.SetFloat("TOTALTIME", currentTimeTakenForTheLevel + timeLimitSeconds);
                float totalTimeforThisLevel;
                totalTimeforThisLevel = PlayerPrefs.GetFloat("TOTALTIME", 0);
                totalTimeforThisLevel = totalTimeforThisLevel / ((currentLevel * 2) + 1);
                float compareTime;
                compareTime = ((currentLevel - 1) * 30 + 30) / 6;
                string levelName = "LEVEL" + currentLevel.ToString();
                if (totalTimeforThisLevel < compareTime)
                    PlayerPrefs.SetInt(levelName, 3);
                else if (totalTimeforThisLevel < compareTime * 2)
                    PlayerPrefs.SetInt(levelName, 2);
                else
                    PlayerPrefs.SetInt(levelName, 1);
                popupCanvas.SetActive(true);
                levelClearPopup.SetActive(true);
                if (currentLevel != 6)
                    level_Complete_Time_Text.SetText("You cleared level " + (currentLevel - 1).ToString());
                else
                    level_Complete_Time_Text.SetText("You cleared level " + (currentLevel).ToString());
                PlayerPrefs.SetFloat("TOTALTIME", 0);
            }
            else
                PlayerPrefs.SetInt("CURRENTSET", currentSet + 1);
        }
        won = false;
        startTimer = false;
        rewardTime = false;
        Destroy(Question.GetChild(0).gameObject);
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
    public void Hint()
    {
        hint = PlayerPrefs.GetInt("HINT", 3);
        if(PlayerPrefs.GetInt("DEMO", 0) == 0)
        {
            Handheld.Vibrate();
            return;
        }
        if (hint > 0)
        {
            string wordCodeToBeShownAsHint = currentAnswers.Substring(0, 4 * noOfCharToCheck);
            if (currentLevel != 6)
            {
                GameObject gm;
                gm = GameObject.Find("GamePlayCanvas/GamePlay/Question/" + keepNameOfImage + "/" + wordCodeToBeShownAsHint);
                gm.SetActive(true);
                gm.transform.SetSiblingIndex(numberOfWordsFound + 1);
                StartCoroutine(RemoveHint(wordCodeToBeShownAsHint));
            }
            else
            {
                GameObject gm;
                for (int i = 0; i < totalNoOfWords; i++)
                {
                    if (Question.GetChild(0).GetChild(i).gameObject.activeSelf == false)
                    {
                        gm = Question.GetChild(0).GetChild(i).gameObject;
                        wordCodeToBeShownAsHint = gm.name;
                        gm.SetActive(true);
                        gm.transform.SetSiblingIndex(numberOfWordsFound + 1);
                        StartCoroutine(RemoveHint(wordCodeToBeShownAsHint));
                        i = totalNoOfWords;
                    }
                }
            }
            if(PlayerPrefs.GetString("SFX", "true") == "true")
                FindObjectOfType<AudioManager>().Play("Hint");
            hint = hint - 1;
            PlayerPrefs.SetInt("HINT", hint);
        }
        else
        {
            //Ask them to watch add or share via whatsapp to other people to gain more Hints;
         
            popupCanvas.SetActive(true);
            popupCanvas.transform.GetChild(7).gameObject.SetActive(true);
            if (PlayerPrefs.GetString("SFX", "true") == "true")
                FindObjectOfType<AudioManager>().Play("OutOfHint");
        }
    }
    IEnumerator RemoveHint(string code)
    {
        GameObject gm = GameObject.Find("GamePlayCanvas/GamePlay/Question/" + keepNameOfImage + "/" + code);
        bool state;
        for(int i=0; i<5; i++)
        {
            if (i % 2 != 0)
                state = true;
            else
                state = false;
            yield return new WaitForSeconds(0.4f);
            gm.SetActive(state);
        }
        PlayerPrefs.SetInt("HINT", hint - 1);
        hint--;
    }
    public void SetEverythingForDemo()
    {
        nameCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }
    public void SetEverythingAfterDemoDone()
    {
        menuCanvas.SetActive(true);
        nameCanvas.SetActive(false);
    }

    /// /// REWARDS /// ///
    

    void Video() => EMAG_Yodo.instance.Show_RewardedVideo();
    void Interstitial() => EMAG_Yodo.instance.Show_Interstitial();
    public void RewardHint()
    {
        rewardHint = true;
        EMAG_Yodo.instance.Show_RewardedVideo();
    }
    public void RewardTime()
    {
        rewardTime = true;
        EMAG_Yodo.instance.Show_RewardedVideo();
    }
    public void Reward()
    {
        if (rewardHint == true)
        {
            PlayerPrefs.SetInt("HINT", 1);
            rewardHint = false;
            //
            popupCanvas.transform.GetChild(7).gameObject.SetActive(false);
            popupCanvas.SetActive(false);
        }
        else if (rewardTime == true)
        {
            TimerReward();
            popupCanvas.transform.GetChild(1).gameObject.SetActive(false);
            popupCanvas.SetActive(false);
        }
    }
    public void TimerReward()
    {
        if (rewardHint != true)
        {
            timeLimitMinutes = 1;
            timeLimitSeconds = 0;
            timer_Text.SetText(timeLimitMinutes.ToString() + ":0" + timeLimitMinutes_Second.ToString());
            startTimer = false;
            rewardTime = true;
        }
        rewardHint = false;
    }
    /// /// REWARDS /// ///
    
    /// /// RETRY SAME LEVEL AFTER TIMEUP /// ///
    public void Retry()
    {
        if (currentLevel == currentMaxLevel && currentSet == currentSetOfMaxLevel)
        {
            PlayerPrefs.SetInt("CURRENTSET", 1);
            popupCanvas.SetActive(true);
            popupCanvas.transform.GetChild(10).gameObject.SetActive(true);
        }
        else
            RetrySure();
    }
    public void RetrySure() => animations.LoadGoToMenuAnimation();
    /// /// RETRY SAME LEVEL AFTER TIMEUP /// ///
    
    public void UI_ButtonSound()
    {
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("UI_Click");
    }
    private void ShowInstructions()
    {
        startTimer = false;
        popupCanvas.SetActive(true);
        popupCanvas.transform.GetChild(13).gameObject.SetActive(true);
    }

    public void RemoveInstructions()
    {
        startTimer = true;
        popupCanvas.transform.GetChild(13).gameObject.SetActive(false);
        popupCanvas.SetActive(false);
    }
    public Vector3 ReturnCoordinateOfSlidersLocal(int i)
    {
        switch (i) {
            case 0: 
                return vs1.localPosition;
            case 1:
                return vs2.localPosition;
            case 2:
                return hs1.localPosition;
            case 3:
                return hs2.localPosition;
        }
        return Vector3.zero;
    }
}