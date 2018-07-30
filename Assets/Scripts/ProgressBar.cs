using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject ProgressPanel; // adding the ProgressPanel parent for the ProgressBars 

    public bool bauInProgress;
    bool forschungInProgress;
    bool upgradeInProgress;
    string dictKey;

    private float startTime; 
    private static float currentTime;

    public Slider progressBar; // public
    public Text progressText; // public

    private static float limit;
    private float timePercent;
    private static float returnTime;

    Dictionary<string, float> dictDuration = new Dictionary<string, float>()
    {
        {"Tower", 3},
        {"House", 5},
        {"Mill", 3},
        {"Foresthut", 20}
    };

    Dictionary<string, float> dictProgress = new Dictionary<string, float>()
    {
        {"Tower", 0},
        {"House", 0},
        {"Mill", 0},
        {"Foresthut", 0}
    };

    /// <summary>
    /// Calculates the time in percent that a certain task needs.
    /// Is used to set the slider value in a progress bar respective to it's time value in percentage.
    /// </summary>
    /// <returns> The percentage of a total interval that has passed at this time. </returns>
    private float TimeInPercent()
    {
        returnTime = 100f / limit * currentTime;
        return returnTime;
    }

    /// <summary>
    /// Sets a building process, defined by the method parameter, to active.
    /// Executed by the BauProgress button in the MiddleCanvas
    /// Evaluates whether an ingame event "Bau" is already active.
    /// If true it disallows to trigger another event.
    /// If false it flags Bau as active and passes the correct button argument (from which source the active process derives from),
    /// to read out the correct time values from the dictDuration dictionary and the dictProgress dictionary,
    /// where the reached progress of each building is stored.
    /// </summary>
    /// <param name="buttonArgument">The key for the dictDuration and dictProgress dictionaries.</param>
    public void ActiveBauInProgress(string buttonArgument)
    {
        if (bauInProgress) // if there is already a building in progress
        {
            Debug.Log("There is already building in progress."); 
        }
        else // if there's no building in progress allow something new to be built, and activate a progress bar.
        {
            ResetSlider();
            ProgressPanel.gameObject.SetActive(true); // setting the panel to active so it's visible, (maybe add parts first then make it visible?)
            bauInProgress = true; // if the bauInProgress button is pressed this variable is set to true
            dictKey = buttonArgument; // get the ButtonArgument from the dictionary to read out the correct time value for the progressBar
            limit = dictDuration[dictKey]; // get the time duration of the process as a limit for the progress bar
            progressBar.GetComponent<Slider>().maxValue = limit; // set the limit of the progress bar to the limit.
        }
    }

    /// <summary>
    /// Displays the progress of an ingame event in a progress bar.
    /// </summary>
    public void DisplayProgress()
    {
        currentTime += Time.deltaTime;
        progressBar.GetComponent<Slider>().value = currentTime; // define the slider to the current time/progress
        progressText.GetComponent<Text>().text = TimeInPercent().ToString("n0") + " %"; 
        if (currentTime >= limit) // if the active progess is finished
        {
            Debug.Log("Upgrade Complete");
            bauInProgress = false; // set active progress to inactive (because it's finished)
            // redefining dictionary entries to the new state
            if (dictProgress[dictKey] > 3) // if the maximal progress of a building was reached
            {
                Debug.Log("Throw an exception in the future: maximal progress already reached!");
            } 
            else
            {
                dictProgress[dictKey] += 1; //incrementing the progress of a building by 1
                dictDuration[dictKey] += 5; //change duration of progress in the respective key
                ResetSlider(); // Reset previous slider activity
                ProgressPanel.gameObject.SetActive(false); // setting the Panel to inactive, as it should only be visible when a progressbar is active
            }
        }
    }

    /// <summary>
    /// Resets a slider back to its zero state
    /// </summary>
    public void ResetSlider()
    {
        startTime = 0; // start time is set to zero so that necessary time for progressbar can be calculated
        progressBar.GetComponent<Slider>().value = 0; // the slider value is 0 = zero progress by default
        currentTime = 0; // reset currentTime to zero otherwise sliders start at n again
    }

    /// <summary>
    /// Initiates correct ProgressBar handling at the start of the process.
    /// Sets ProgressPanel to inactive, and slider values to 0, aswell as all building processes to false.
    /// </summary>
    void Start()
    {
        ProgressPanel.gameObject.SetActive(false); // setting the Panel to inactive, as it should only be visible when a progressbar is active
        ResetSlider(); // Reset the sliders

        // set all the progress variables to false, so there is no active progress when the game starts.
        bauInProgress = false;
        forschungInProgress = false;
        upgradeInProgress = false;
    }

    /// <summary>
    /// Update method evaluates whether an active bau is in progress.
    /// Initiates DisplayProgress method
    /// </summary>
    void Update()
    {
        if (bauInProgress)
        {
            DisplayProgress();
        }
    }
}

