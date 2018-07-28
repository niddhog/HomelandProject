using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    bool bauInProgress;
    bool forschungInProgress;
    bool upgradeInProgress;
    string dictKey;

    public float startTime;
    public static float currentTime;

    public Slider progressBar;
    public Text progressText;

    private static float limit;
    private float timePercent;
    private static float returnTime;

    Dictionary<string, float> dictDuration = new Dictionary<string, float>()
    {
        {"Tower", 30},
        {"House", 50},
        {"Mill", 15},
        {"Foresthut", 20}
    };

    Dictionary<string, float> dictProgress = new Dictionary<string, float>()
    {
        {"Tower", 0},
        {"House", 0},
        {"Mill", 0},
        {"Foresthut", 0}
    };

    private float TimeInPercent()
    {
        returnTime = 100f / limit * currentTime;
        return returnTime;
    }

    void Start()
    {
        startTime = 0;
        progressBar.GetComponent<Slider>().value = 0;

        bauInProgress = false;
        forschungInProgress = false;
        upgradeInProgress = false;
    }

    public void ActiveBauInProgress(string buttonArgument)
    {
        if (bauInProgress)
        {

        }
        else
        {
            bauInProgress = true;
            dictKey = buttonArgument;
            limit = progressBar.GetComponent<Slider>().maxValue = dictDuration[dictKey];
        }
       
    }

    public void DisplayProgress()
    {
        currentTime += Time.deltaTime;
        progressBar.GetComponent<Slider>().value = currentTime;
        progressText.GetComponent<Text>().text = TimeInPercent().ToString("n0") + " %";
        if (currentTime >= limit)
        {
            Debug.Log("Upgrade Complete");
            bauInProgress = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bauInProgress)
        {
            DisplayProgress();
            //Debug.Log("Button for bauInProgress was activated.");
            if (dictProgress[dictKey] <= 3)
            {
                dictProgress[dictKey] += 1; //incrementing the progress of a building by 1
                Debug.Log(dictProgress[dictKey]);
                dictDuration[dictKey] += 50; //change duration of progress in the respective key
                Debug.Log(dictDuration[dictKey]);
            }
            else
            {
                //Debug.Log("Maximal Progress reached."); //throw an error of maximal progress is reached
            }
        }
    }
}
