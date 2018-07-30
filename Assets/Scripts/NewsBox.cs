using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsBox : MonoBehaviour {
    public Text textField;
    public Text newsText;
    public static int dictLength;

    private float yPos;

    public static Dictionary<int, string> dictNews = new Dictionary<int, string>()
    {
        {0,null},
        {1,null},
        {2,null},
        {3,null},
        {4,null},
        {5,null},
        {6,null},
        {7,null},
    };

    public void WakeNewsBox()
    {
        if(gameObject.activeSelf == true)//Triggers if box is active
        {
            yPos = 0;
            foreach (Transform child in GameObject.Find("Texts").transform)//with this function we destroy all the TextChilds (News) upon leaving the NewsMenu
            {
                GameObject.Destroy(child.gameObject);
            }
            gameObject.SetActive(false);
        }
        else                             //Triggers if box is inactive
        {
            gameObject.SetActive(true);
            spawnAllNews();
        }       
    }

    void spawnAllNews()
    {
        yPos = 150f;
        for (int i = 0; i <= 7; i++)
        {
            if (!(dictNews[i] == null))
            {
                textField = Instantiate(newsText) as Text;
                textField.transform.SetParent(GameObject.Find("Texts").transform,false);
                textField.transform.position = new Vector3(0,yPos, 0);
                yPos -= 40f;
                textField.text = "Week - " + TimeManagement.timeArray[0].ToString() + ":\n" + dictNews[i];
            }
        }
    }

    public static int getDictLength()
    {
        dictLength = dictNews.Count;
        return dictLength - 1;//-1 because Lists start at index 0, not 1
    }
}
