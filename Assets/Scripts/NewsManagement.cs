using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManagement : MonoBehaviour {
    public GameObject NewsPanel;//Used for instantiating the NewsPanel Sprite
    public Text newsText;//Used for adjusting the Message inside the Panel
    private bool newsCast = true; //used to avoid the animation playing more than once
    private GameObject NewsBox;

    Dictionary<int,string> dictTexts = new Dictionary<int, string>()//Dict with all the News Messages
    {
        {5,"News1: Ein Fremder trifft in eurem Lager ein und bittet euch um einen gefallen..."},
        {20,"News2"},
        {40,"News3"},
        {60,"News4"}
    };

    public bool SetNewsText(float seconds)//This Function feeds the Dictionary into the animated Panel
    {
        int secondsRounded = (int)Mathf.Round(seconds);//Rounds the seconds float to a whole integer

        //following are all the timebased, scripted News at the secondsRounded Mark
        if(secondsRounded == 5)
        {
            newsText.GetComponentInChildren<Text>().text = dictTexts[secondsRounded];
            return true;

        } else if (secondsRounded == 20)
        {
            newsText.GetComponentInChildren<Text>().text = dictTexts[secondsRounded];
            return true;

        }else if (secondsRounded == 40)
        {
            newsText.GetComponentInChildren<Text>().text = dictTexts[secondsRounded];
            return true;

        }else if (secondsRounded == 60)
        {
            newsText.GetComponentInChildren<Text>().text = dictTexts[secondsRounded];
            return true;
        }
        return false;
    }
	
    public void DisplayNews(float seconds)//seconds is the global time in which we know when to trigger which news
    {
        if (SetNewsText(seconds) && newsCast)
        {
            NewsBox = Instantiate(NewsPanel, transform.position = new Vector3(35f, 152f, 0), Quaternion.identity) as GameObject;
            NewsBox.transform.SetParent(GameObject.Find("MiddleCanvas").transform, false);//mittels false skaliert das instantiierte Objekt an den Globalen X und Y, nicht am Parent
            NewsBox.transform.SetParent(GameObject.Find("NewsPanel").transform, false);
            float delay = GameObject.Find("NewsPanel").GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length;//length of the Animator Clip of the Panel
            StartCoroutine (WaitTime(delay));//with this syntax we call the IEnumerator WaitTime
            Destroy(NewsBox, delay);//Delets Object afer "delay" seconds
            newsCast = false;//bool to false so the animation is not starting again
        }   
    }

    IEnumerator WaitTime(float seconds)//This Function is used to wait "float seconds" seconds until the code below yield return... is executed
    {
        yield return new WaitForSeconds(seconds);
        newsCast = true;
    }
}
