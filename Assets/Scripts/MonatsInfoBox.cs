using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonatsInfoBox : MonoBehaviour {
    public GameObject monthInfoBox;
    public GameObject box7;
    public GameObject box6;
    public GameObject box5;
    public GameObject box4;
    public GameObject box3;
    public GameObject box2;
    public GameObject box1;


    private GameObject monthInfoBoxClone;
    private bool monthBoxActive;
    private int monthBoxLength;
    private int monthInfoBoxIndex;
    private int titelIndex;
    private Text infoBoxText;
    private Rect boxSize;



    void Start () {
        monthBoxActive = false;
        monthBoxLength = 0;
	}

    private void OnMouseOver()
    {
        if (!monthBoxActive)
        {
            SetupMonthBoxText();
            monthBoxActive = true;
        }
        
    }

    private void OnMouseExit()
    {
        foreach (Transform child in GameObject.Find("MonatsInfoBox").transform)
        {
            Destroy(child.gameObject);
            monthBoxActive = false;
        }
    }


    /// <summary>
    /// This function sets up the effects each month brings. The effects are generated out of a random set of possible effects
    /// and alter each month
    /// </summary>
    private void SetupMonthBoxText()
    {
        monthBoxLength = 0;
        if (Dicts.dictResearch["monthInfoBoxResearch1"] == 1)
        {
            Dicts.dictMonthInfoBox[monthBoxLength] = "Info1";
            Dicts.dictMonthInfoBox[monthBoxLength + 1] = "This is the Info1 Text";
            monthBoxLength += 2;
        }
        if (Dicts.dictResearch["monthInfoBoxResearch2"] == 1)
        {
            Dicts.dictMonthInfoBox[monthBoxLength] = "Info2";
            Dicts.dictMonthInfoBox[monthBoxLength + 1] = "This is the Info2 Text";
            monthBoxLength += 2;
        }
        if (Dicts.dictResearch["monthInfoBoxResearch3"] == 1)
        {
            Dicts.dictMonthInfoBox[monthBoxLength] = "Info3";
            Dicts.dictMonthInfoBox[monthBoxLength + 1] = "This is the Info3 Text";
            monthBoxLength += 2;
        }
        if (Dicts.dictResearch["monthInfoBoxResearch4"] == 1)
        {
            Dicts.dictMonthInfoBox[monthBoxLength] = "Info4";
            Dicts.dictMonthInfoBox[monthBoxLength + 1] = "This is the Info4 Text";
            monthBoxLength += 2;
        }
        if (Dicts.dictResearch["monthInfoBoxResearch5"] == 1)
        {
            Dicts.dictMonthInfoBox[monthBoxLength] = "Info5";
            Dicts.dictMonthInfoBox[monthBoxLength + 1] = "This is the Info5 Text";
            monthBoxLength += 2;
        }
        if (Dicts.dictResearch["monthInfoBoxResearch6"] == 1)
        {
            Dicts.dictMonthInfoBox[monthBoxLength] = "Info6";
            Dicts.dictMonthInfoBox[monthBoxLength + 1] = "This is the Info6 Text";
            monthBoxLength += 2;
        }
        if (Dicts.dictResearch["monthInfoBoxResearch7"] == 1)
        {
            Dicts.dictMonthInfoBox[monthBoxLength] = "Info7";
            Dicts.dictMonthInfoBox[monthBoxLength + 1] = "This is the Info7 Text";
            monthBoxLength += 2;
        }

        SetupBoxSize();//now that we know the monthBoxLength we can set up its Box size
        monthInfoBoxIndex = 1;
        titelIndex = 2; //Space 1 is always researched, its the information about the weather, so we start from position 2 down to 8
        while(monthInfoBoxIndex <= monthBoxLength)
        {
            infoBoxText = GameObject.Find("Title" + titelIndex.ToString() + "Text").GetComponent<Text>();
            infoBoxText.text = Dicts.dictMonthInfoBox[monthInfoBoxIndex - 1];
            infoBoxText = GameObject.Find("Title" + titelIndex.ToString() + "SubText").GetComponent<Text>();
            infoBoxText.text = Dicts.dictMonthInfoBox[monthInfoBoxIndex];
            titelIndex += 1;
            monthInfoBoxIndex += 2;
        }
        while(titelIndex <= 8)//All the positions not used in the box are filled with an empty null text
        {
            infoBoxText = GameObject.Find("Title" + titelIndex.ToString() + "Text").GetComponent<Text>();
            infoBoxText.text = null;
            infoBoxText = GameObject.Find("Title" + titelIndex.ToString() + "SubText").GetComponent<Text>();
            infoBoxText.text = null;
            titelIndex += 1;
            monthInfoBoxIndex += 2;
        }
    }

    private void SetupBoxSize()
    {
        if(monthBoxLength == 14)
        {
            monthInfoBoxClone = Instantiate(monthInfoBox) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
        else if (monthBoxLength == 12)
        {
            monthInfoBoxClone = Instantiate(box7) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
        else if (monthBoxLength == 10)
        {
            monthInfoBoxClone = Instantiate(box6) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
        else if (monthBoxLength == 8)
        {
            monthInfoBoxClone = Instantiate(box5) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
        else if (monthBoxLength == 6)
        {
            monthInfoBoxClone = Instantiate(box4) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
        else if (monthBoxLength == 4)
        {
            monthInfoBoxClone = Instantiate(box3) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
        else if (monthBoxLength == 2)
        {
            monthInfoBoxClone = Instantiate(box2) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
        else
        {
            monthInfoBoxClone = Instantiate(box1) as GameObject;
            monthInfoBoxClone.transform.SetParent(GameObject.Find("MonatsInfoBox").transform, false);
        }
    }

}
