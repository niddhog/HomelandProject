using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBox : MonoBehaviour {

    private bool destroyComplete = false; //destroyComplete works as a sentinel so that the loop in the update function does only run 1 time and not infinite times
    public GameObject progressBox;
    public float yPos;
    public static bool progressBoxActive = false;

    public void WakeProgressBox()
    {
        if (gameObject.activeSelf == true)//Triggers if box is active
        {
            progressBoxActive = false;
        }
        else                             //Triggers if box is inactive
        {
            if (NewsBox.newsBoxActive)
            {
                NewsBox.newsBoxActive = false;
            }
            destroyComplete = false;
            progressBoxActive = true;
            gameObject.SetActive(true);
        }
    }

    private void destroyProgressBox()
    {
        progressBox.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//needed in case the Animation of the Box is interrupted before finishing
        progressBox.transform.position = new Vector2(0, 0);//needed i case the Animaton of the Box is interrupted before finishing
        yPos = 0;
        gameObject.SetActive(false);
        destroyComplete = true;
    }

    private void Update()
    {
        if (!progressBoxActive && !destroyComplete)
        {
            destroyProgressBox();
        }
    }
}
