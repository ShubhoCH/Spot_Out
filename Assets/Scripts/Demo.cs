
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    string NAAM;
    Vector3 end;
    GameObject gmm;
    Manager manager;
    bool ShowDemo, gotAns;
    public GameObject finger;
    public int[] coordinates;
    int currentSliderIndex, currentAnswer;
    public Transform Parent_Vertical, Parent_Horizontal;
    private void Start()
    {
        NAAM = "";
        ShowDemo = true;
        gotAns = false;
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }
    private void Update()
    {
        for(int i = 0; i < 3 && gotAns == false; i++)
        {
            if (transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.activeSelf == false)
            {
                gotAns = true;
                gmm = Instantiate(finger);
                gmm.transform.gameObject.SetActive(true);
                gmm.transform.SetParent(Parent_Vertical);
                gmm.transform.localScale = Vector3.one;
                gmm.transform.position = Vector3.zero;
                NAAM = transform.GetChild(0).gameObject.transform.GetChild(i).name;
                Debug.Log(name);
                for (int j = 0; j < 4; j++)
                    coordinates[j] = (int.Parse(NAAM[j].ToString()) - 3) * 255;
                SetStartPosAndEndPos();
            }
        }
        if (ShowDemo == true)
        {
            if ((gmm.transform.localPosition.x / 255) + 3 != currentAnswer)
                gmm.transform.localPosition = Vector3.MoveTowards(gmm.transform.localPosition, end, Time.deltaTime * 500);
            else
                SetStartPosAndEndPos();
        }
    }
    void SetStartPosAndEndPos()
    {
        //Aligh finger
        gmm.transform.SetParent(Parent_Vertical);
        gmm.transform.localRotation = Quaternion.Euler(0, 0, 180);
        if ((int)(manager.ReturnCoordinateOfSlidersLocal(0).x / 255) + 3 != int.Parse(NAAM[0].ToString()) && (int)(manager.ReturnCoordinateOfSlidersLocal(1).x / 255) + 3 != int.Parse(NAAM[0].ToString()))
        {
            //Set Start pos;
            gmm.transform.localPosition = manager.ReturnCoordinateOfSlidersLocal(0) + new Vector3(0,80,0);
            //translate;
            end = gmm.transform.localPosition + new Vector3(coordinates[0] - manager.ReturnCoordinateOfSlidersLocal(0).x, 0, 0);
            currentSliderIndex = 0;
            currentAnswer = int.Parse(NAAM[0].ToString());
            ShowDemo = true;
        }
        else
        {
            if ((int)(manager.ReturnCoordinateOfSlidersLocal(0).x / 255) + 3 == int.Parse(NAAM[0].ToString()) && (int)(manager.ReturnCoordinateOfSlidersLocal(1).x / 255) + 3 != int.Parse(NAAM[1].ToString()))
            {
                //Set Start pos;
                gmm.transform.localPosition = manager.ReturnCoordinateOfSlidersLocal(1) + new Vector3(0, 80, 0);
                //translate;
                end = gmm.transform.localPosition + new Vector3(coordinates[1] - manager.ReturnCoordinateOfSlidersLocal(1).x, 0, 0);
                currentSliderIndex = 1;
                currentAnswer = int.Parse(NAAM[1].ToString());
                ShowDemo = true;
            }
            else if ((int)(manager.ReturnCoordinateOfSlidersLocal(1).x / 255) + 3 == int.Parse(NAAM[0].ToString()) && (int)(manager.ReturnCoordinateOfSlidersLocal(0).x / 255) + 3 != int.Parse(NAAM[1].ToString()))
            {
                //Set Start pos;
                gmm.transform.localPosition = manager.ReturnCoordinateOfSlidersLocal(0) + new Vector3(0, 80, 0);
                //translate;
                end = gmm.transform.localPosition + new Vector3(coordinates[1] - manager.ReturnCoordinateOfSlidersLocal(0).x, 0, 0);
                currentSliderIndex = 0;
                currentAnswer = int.Parse(NAAM[1].ToString());
                ShowDemo = true;
            }
            else
            {
                // Change Parent
                gmm.transform.SetParent(Parent_Horizontal);
                // Rotate Finger
                gmm.transform.localRotation = Quaternion.Euler(0, 0, 180);
                if ((int)(manager.ReturnCoordinateOfSlidersLocal(2).x / 255) + 3 != int.Parse(NAAM[2].ToString()) && (int)(manager.ReturnCoordinateOfSlidersLocal(3).x / 255) + 3 != int.Parse(NAAM[2].ToString()))
                {
                    //Set Start pos;
                    gmm.transform.localPosition = manager.ReturnCoordinateOfSlidersLocal(2) + new Vector3(0, 100, 0);
                    //translate;
                    end = gmm.transform.localPosition + new Vector3(coordinates[2] - manager.ReturnCoordinateOfSlidersLocal(2).x, 0, 0);
                    currentSliderIndex = 2;
                    currentAnswer = int.Parse(NAAM[2].ToString());
                    ShowDemo = true;
                }
                else
                {
                    if ((int)(manager.ReturnCoordinateOfSlidersLocal(2).x / 255) + 3 == int.Parse(NAAM[2].ToString()) && (int)(manager.ReturnCoordinateOfSlidersLocal(3).x / 255) + 3 != int.Parse(NAAM[3].ToString()))
                    {
                        //Set Start pos;
                        gmm.transform.localPosition = manager.ReturnCoordinateOfSlidersLocal(3) + new Vector3(0, 100, 0);
                        //translate;
                        end = gmm.transform.localPosition + new Vector3(coordinates[3] - manager.ReturnCoordinateOfSlidersLocal(3).x, 0, 0);
                        currentSliderIndex = 3;
                        currentAnswer = int.Parse(NAAM[3].ToString());
                        ShowDemo = true;
                    }
                    else if ((int)(manager.ReturnCoordinateOfSlidersLocal(3).x / 255) + 3 == int.Parse(NAAM[2].ToString()) && (int)(manager.ReturnCoordinateOfSlidersLocal(2).x / 255) + 3 != int.Parse(NAAM[3].ToString()))
                    {
                        //Set Start pos;
                        gmm.transform.localPosition = manager.ReturnCoordinateOfSlidersLocal(2) + new Vector3(0, 100, 0);
                        //translate;
                        end = gmm.transform.localPosition + new Vector3(coordinates[3] - manager.ReturnCoordinateOfSlidersLocal(2).x, 0, 0);
                        currentSliderIndex = 2;
                        currentAnswer = int.Parse(NAAM[3].ToString());
                        ShowDemo = true;
                    }
                    else
                    {
                        finger.SetActive(false);
                        ShowDemo = false;
                        gotAns = false;
                        Destroy(gmm);
                        Debug.Log("Whyyyyyyyyyyyyy Here");
                    }
                }
            }
        }
    }
}
