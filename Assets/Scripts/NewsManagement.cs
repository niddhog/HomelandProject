using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManagement : MonoBehaviour {
    public GameObject NewsPanel;
    private float delay = 5f;
    public Text newsText;

    Dictionary<int,string> dictTexts = new Dictionary<int, string>()
    {
        {5,"News1"},
        {20,"News2"},
        {40,"News3"},
        {60,"News4"}
    };

    public string SetNewsText(float seconds)//Hier können wir die ganzen NewsMeldungen abrufen
    {
        int secondsRounded = (int)Mathf.Round(seconds);
        string test = dictTexts[secondsRounded];
        return test;
    }
	
    public void DisplayNews(float seconds)//seconds is the global time in which we know when to trigger which news
    {
        GameObject NewsBox = (GameObject)Instantiate(NewsPanel, transform.position = new Vector3(35f, 152f, 0), Quaternion.identity) as GameObject;
        NewsBox.transform.SetParent(GameObject.Find("MiddleCanvas").transform, false);//mittels false skaliert das instantiierte Objekt an den Globalen X und Y, nicht am Parent
        NewsBox.transform.SetParent(GameObject.Find("NewsPanel").transform, false);
        newsText.GetComponentInChildren<Text>().text = SetNewsText(seconds);
        //float test = NewsBox.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length;
        //AnimatorClipInfo[] infos = NewsBox.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        //Debug.Log(test);
        //Debug.Log(infos.Length);
        Destroy(NewsBox, delay);
    }
}
