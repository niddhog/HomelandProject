using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This skript is for dynamicly adding NewsMessages to the game. The order of display in case of News Overlap is handled through a LastInFirstOut Stack.
//To add new messages, don't forget to adjust the dictonary {time Stamp,"News Message} either aswell as the StackSetupTimeEvents. To add news from outside,
//like news that are dependent on Player action and not on Time, simply add to the stack by using newsStack.Add(x);, whereas x is the timestamp in the dict.
//For repeating messages such as "The upgrade for your building is complete" we can use specific timestamps in the dict such as
//{9999,"The upgrade..."}.
public class NewsManagement : MonoBehaviour
{
    public GameObject newsPanel;//Used for instantiating the NewsPanel Sprite
    public Text newsText;//Used for adjusting the Message inside the Panel
    public static bool newsCast = true; //used to avoid the animation playing more than once
    public List<int> newsStack = new List<int>();//Stack Datastructure used for queueing News if News overlap LiFo (LastIn/FirstOut), we save the dictText Keys in the Stack
    public GameObject newsBox;

    private GameObject newsPannel;
    private int stackLength;
    private float delay; //used to store animationLength

    /// <summary>
    /// Returns the current length of newsStack (nr. of entries in this lsit)
    /// </summary>
    /// <returns>length of the stack</returns>
    private int getStackLength()
    {
        stackLength = newsStack.Count;
        return stackLength-1;//-1 because Lists start at index 0, not 1
    }

    public void DisplayNews(float seconds)//seconds is the global time in which we know when to trigger which news
    {
        StackSetupTimeEvents(seconds);//Function draws all the time based news into the Stack in case there exist some at the given timemark
        if (getStackLength() >= 0 && newsCast)//The empty stack has a value of -1, thus a length of 0 means there is 1 element in the Stack
                                              //newsCast means, that currently there is no newsAnimation running and thus News can be displayed
        {
            for (int i = getStackLength(); i >= 0; i--)//We start working on the stack from the End. Therefor, the last element in the Stack is triggered first
            {
                newsText.GetComponentInChildren<Text>().text = Dicts.dictTexts[newsStack[i]];//get the NewsText from the Dictonary. First in the newsStack, the i-th element is referenced
                                                                                             //and used as key value to search for the according string in the dictTexts Dictionary
                if (newsCast)
                {
                    AddNewsToNewsBox(i);//We only add news to the NewsBox when the News starts appearing on screen, not before
                    //Following code is used to animate and afterwards destroy the news Panel
                    newsPannel = Instantiate(newsPanel, transform.position = new Vector3(35f, 152f, 0), Quaternion.identity) as GameObject;
                    newsPannel.transform.SetParent(GameObject.Find("MiddleCanvas").transform, false);//mittels false skaliert das instantiierte Objekt an den Globalen X und Y, nicht am Parent
                    newsPannel.transform.SetParent(GameObject.Find("NewsPanel").transform, false);
                    delay = GameObject.Find("NewsPanel").GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length;//length of the Animator Clip of the Panel
                    StartCoroutine(WaitTimePanelAnimation(delay,i));//with this syntax we call the IEnumerator WaitTime
                    Destroy(newsPannel, delay);//Delets Object afer "delay" seconds
                    newsCast = false;//bool to false so the animation is not starting again
                }
            }
        }



    }

    IEnumerator WaitTimePanelAnimation(float seconds, int i)//This Function is used to wait "float seconds" seconds until the code below yield return... is executed
    {
        NewsBox.trigger = true;//used for synching the NewsBox realtime
        yield return new WaitForSeconds(seconds);
        NewsBox.trigger = false;
        NewsBox.coroutineOn = false;
        newsStack.RemoveAt(i);//We remove the last Element of the Stack
        yield return new WaitForSeconds(0.0001f);
        newsCast = true;
    }

    /// <summary>
    /// Fills up the Stack from bottom to top with time based news events
    /// Each Entry in the stack consists of an integer (ex. 2), which acts as
    /// Key in the Dictionary and finds the corresponding news String
    /// </summary>
    /// <param name="seconds"></param>
    private void StackSetupTimeEvents(float seconds)
    {
        int secondsRounded = (int)Mathf.Round(seconds);//Rounds the seconds float to a whole integer
        
        //following are all the timebased, scripted News at the "secondsRounded" Mark
        if (secondsRounded == 2 && !newsStack.Contains(2))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 8 && !newsStack.Contains(8))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 10 && !newsStack.Contains(10))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 12 && !newsStack.Contains(12))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 14 && !newsStack.Contains(14))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 16 && !newsStack.Contains(16))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 20 && !newsStack.Contains(20))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 26 && !newsStack.Contains(26))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 30 && !newsStack.Contains(30))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 50 && !newsStack.Contains(50))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 70 && !newsStack.Contains(70))
        {
            newsStack.Add(secondsRounded);
        }
        else if (secondsRounded == 72 && !newsStack.Contains(72))
        {
            newsStack.Add(secondsRounded);
        }
    }

    private void AddNewsToNewsBox(int j)//hier stimmt noch was nicht wenn alle Slots Full sind...
    {
        int slotsOccupied = 0;
        for(int i = Dicts.dictNews.Count-1; i >= 0; i--)//First check weither there is a free slot in the dict or not
        {
            if (Dicts.dictNews[i]==null)
            {
                Dicts.dictNews[i] = Dicts.dictTexts[newsStack[j]];
                Dicts.dictNewsWeek[i] = TimeManagement.timeArray[0];//we set the week to the week it appeared
                slotsOccupied += 1;
                return;
            }
            slotsOccupied += 1;
        }
        if(slotsOccupied == 8)//If all NewsSlots are Occupied, Slot 7 is set free and all news are shiftet one down, freeing slot 0, which is filled with the latest news
        {
            for(int k = Dicts.dictNews.Count-1; k >= 0; k--)
            {
                if (k == 0)
                {
                    Dicts.dictNews[0] = Dicts.dictTexts[newsStack[j]];
                    Dicts.dictNewsWeek[0] = TimeManagement.timeArray[0];
                }
                else
                {
                    Dicts.dictNews[k] = Dicts.dictNews[k - 1];
                    Dicts.dictNewsWeek[k] = Dicts.dictNewsWeek[k - 1];
                }        
            }
        }
    }
}
