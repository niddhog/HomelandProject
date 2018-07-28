using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManagement : MonoBehaviour {
    public GameObject NewsPanel;//Used for instantiating the NewsPanel Sprite
    public Text newsText;//Used for adjusting the Message inside the Panel
    private float delay = 5f;//Delay in seconds, until Panel Animation is destroyed
    private bool newsCast = true;

    Dictionary<int,string> dictTexts = new Dictionary<int, string>()//Dict with all the News Messages
    {
        {5,"News1: Ein Fremder trifft in eurem Lager ein und bittet euch um einen gefallen..."},
        {20,"News2"},
        {40,"News3"},
        {60,"News4"}
    };

    public bool SetNewsText(float seconds)//This Function feeds the Dictionary into the animated Panel
    {
        int secondsRounded = (int)Mathf.Round(seconds);

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
            GameObject NewsBox = Instantiate(NewsPanel, transform.position = new Vector3(35f, 152f, 0), Quaternion.identity) as GameObject;
            NewsBox.transform.SetParent(GameObject.Find("MiddleCanvas").transform, false);//mittels false skaliert das instantiierte Objekt an den Globalen X und Y, nicht am Parent
            NewsBox.transform.SetParent(GameObject.Find("NewsPanel").transform, false);
            Destroy(NewsBox, delay);
            newsCast = false;
        }
        //float test = NewsBox.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length;
        //AnimatorClipInfo[] infos = NewsBox.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        //Debug.Log(test);
        //Debug.Log(infos.Length);     
    }
}
