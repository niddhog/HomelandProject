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
    private bool newsCast = true; //used to avoid the animation playing more than once
    private GameObject newsBox;
    public List<int> newsStack = new List<int>();//Stack Datastructure used for queueing News if News overlap LiFo (LastIn/FirstOut), we save the dictText Keys in the Stack
    private bool stopper = false;
    private int stackLength;

    Dictionary<int, string> dictTexts = new Dictionary<int, string>()//Dict with all the News Messages
    {
        {2,"Ein Fremder trifft in eurem Lager ein und bittet euch um einen gefallen..."},
        {8,"Get Rekt or Git Better"},
        {20,"Hoi zamen, ih verchaufe eu eh Bier für 15 SCHÄDE!"},
        {30,"News3"},
        {40,"News4"}
    };

    private int getStackLength()
    {
        stackLength = newsStack.Count;
        return stackLength-1;//-1 because Lists start at index 0, not 1
    }

    public void DisplayNews(float seconds)//seconds is the global time in which we know when to trigger which news
    {

        StackSetupTimeEvents(seconds);
        if (getStackLength() >= 0 && newsCast)
        {
            for (int i = getStackLength(); i >= 0; i--)
            {
                newsText.GetComponentInChildren<Text>().text = dictTexts[newsStack[i]];//get the NewsText from the Dictonary
                if (newsCast)
                {
                    newsStack.RemoveAt(getStackLength());
                    newsBox = Instantiate(newsPanel, transform.position = new Vector3(35f, 152f, 0), Quaternion.identity) as GameObject;
                    newsBox.transform.SetParent(GameObject.Find("MiddleCanvas").transform, false);//mittels false skaliert das instantiierte Objekt an den Globalen X und Y, nicht am Parent
                    newsBox.transform.SetParent(GameObject.Find("NewsPanel").transform, false);
                    float delay = GameObject.Find("NewsPanel").GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length;//length of the Animator Clip of the Panel
                    StartCoroutine(WaitTimePanelAnimation(delay));//with this syntax we call the IEnumerator WaitTime
                    Destroy(newsBox, delay);//Delets Object afer "delay" seconds
                    newsCast = false;//bool to false so the animation is not starting again
                }
            }
        }

        
    }

    IEnumerator WaitTimePanelAnimation(float seconds)//This Function is used to wait "float seconds" seconds until the code below yield return... is executed
    {
        yield return new WaitForSeconds(seconds);
        newsCast = true;
    }

    IEnumerator WaitStackAdd(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        stopper = false;
    }

    private void StackSetupTimeEvents(float seconds)
    {
        int secondsRounded = (int)Mathf.Round(seconds);//Rounds the seconds float to a whole integer
        if (!stopper)
        {
            //following are all the timebased, scripted News at the "secondsRounded" Mark
            if (secondsRounded == 2)
            {
                newsStack.Add(secondsRounded);
                newsStack.Add(20);//just for testing
                stopAddingToStack();
            }
            else if (secondsRounded == 8)
            {
                newsStack.Add(secondsRounded);
                stopAddingToStack();
            }
            else if (secondsRounded == 30)
            {
                newsStack.Add(secondsRounded);
                stopAddingToStack();
            }
            else if (secondsRounded == 40)
            {
                newsStack.Add(secondsRounded);
                stopAddingToStack();
            }
        }
        
    }

    private void stopAddingToStack()
    {
        StartCoroutine(WaitStackAdd(1));
        stopper = true;
    }
}
