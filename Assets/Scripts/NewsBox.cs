using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsBox : MonoBehaviour {
    private Text textField;
    private bool destroyComplete = false; //destroyComplete works as a sentinel so that the loop in the update function does only run 1 time and not infinite times
    public Text newsText;
    public static int dictLength;
    public GameObject newsBox;
    public float yPos;
    public static bool coroutineOn = false;
    public static bool trigger = false; //used for synching the NewsBox realtime, linked to NewsManagement
    public static bool newsBoxActive = false;

    public void WakeNewsBox()
    {
        if(gameObject.activeSelf == true)//Triggers if box is active
        {
            newsBoxActive = false;
        }
        else                             //Triggers if box is inactive
        {
            if (ProgressBox.progressBoxActive)
            {
                ProgressBox.progressBoxActive = false;
            }
            destroyComplete = false;
            newsBoxActive = true;
            gameObject.SetActive(true);
            spawnAllNews();
        }       
    }

    private void destroyNewsBox()
    {
        newsBox.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//needed in case the Animation of the Box is interrupted before finishing
        newsBox.transform.position = new Vector2(0, 0);//needed i case the Animaton of the Box is interrupted before finishing
        yPos = 0;
        destroyTextChildren();
        gameObject.SetActive(false);
        destroyComplete = true;
    }

    /// <summary>
    /// Destroy all Texts in the NewsBox. This triggers when a new Text Line enters the NewsBox. In this case, all textChildren in the NewsBox
    /// are deleted and then moved down one step
    /// </summary>
    private void destroyTextChildren()
    {
        foreach (Transform child in GameObject.Find("Texts").transform)//with this function we destroy all the TextChilds (News) upon leaving the NewsMenu
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Instantiate all News Texts inside the NewsBox
    /// </summary>
    public void spawnAllNews()
    {
        yPos = 150f; //global y pos where to spawn News in Slot 0, after -40 is subtraced for each following news
        for (int i = 0; i <= 7; i++)
        {
            if (!(Dicts.dictNews[i] == null))
            {
                    textField = Instantiate(newsText) as Text;
                    textField.transform.SetParent(GameObject.Find("Texts").transform, false);
                    textField.transform.position = new Vector3(0, yPos, 0);
                    yPos -= 40f;
                    textField.text = "Week - " + Dicts.dictNewsWeek[i] + ":\n" + Dicts.dictNews[i];
            }
        }
    }

    public static int getDictLength()
    {
        dictLength = Dicts.dictNews.Count;
        return dictLength - 1;//-1 because Lists start at index 0, not 1
    }

    private void Update()
    {
        if (!newsBoxActive && !destroyComplete)
        {
            destroyNewsBox();
        }
        if (trigger && newsBox.activeSelf)
        {
            if (!coroutineOn)
            {
                destroyTextChildren();
                spawnAllNews();
                coroutineOn = true;
            }
        }
    }
}
