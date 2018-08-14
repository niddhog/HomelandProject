using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManagement : MonoBehaviour {
    //Variables for the Clock//
    public GameObject[] monthList = new GameObject[12];
    public Transform zeiger;
    public Text timeText;
    public Text yearText;
    public Text secondsText; //Testvariable to display Seconds, just for testing
    private const float secondsToDegrees = 360f / 240f; //wird für die EulerRotation benötigt
    private static int monthSentinel;//Prevents the MonthEmblem from being destroyed before month has ended
    private bool lockMonth;//Prevents Emblem from being instantiated after every update call
    private GameObject month;//This Gameobject is used to Clone Instances of MonthEmblems


    //Variables for TimeManagement//
    public static bool stopTime = false; //Stops or Starts Timeflow
    public static int[] timeArray = new int[4]; //Week(0), Month(1), Year(2), Month in Sequence of 12(3)
    public float seconds; //Gametime in Number of Seconds. Core Time Variable for the whole Game-Timeflow  


    //Variables for NewsPanelPopup
    public NewsManagement callNewsManagement;

    //Initializining Function
    void Start()
    {
        yearText.text = "1"; //Starts the Game with Year set to 1
        lockMonth = false; //Month are not locked as they need to go through an initial Loop on Gamestart first
        monthSentinel = 0; //Checks weither the Month has already changed or not
    }

    //Function to Define the Clock and its Subvalues (Weeks, Month, Years)
    int[] TimeCalculator(float t) //One Week equals to 1 Minute
    {
       zeiger.localRotation =
                Quaternion.Euler(0f, 0f, t * -secondsToDegrees);
        timeArray[0] = ((int)t / 60); //Week
        timeArray[1] = ((int)t / 240); //Month
        timeArray[2] = (int)t / 2880; //Year
        timeArray[3] = timeArray[1] % 12; //Month in Sequence of 12 (0,1,2,3,...,12)
        return timeArray;
    }

    //Function to update and display the Month Emblem on the Clock
    void displayMonth(int monthSequence)
    {
        if (!lockMonth)
        {
            //The Following Code Creates the Month Emblem as A Child_Child of MiddleCanvas->MonthPosition.
            month = Instantiate(monthList[monthSequence], transform.position = new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            month.transform.parent = GameObject.Find("MiddleCanvas").transform;
            month.transform.parent = GameObject.Find("MonthPosition").transform;
            month.transform.position = GameObject.Find("MonthPosition").transform.position;//Emblem gets the same Position as the EmptyObject "MonthPosition"
        }
        if (monthSentinel == monthSequence)
        {
            lockMonth = true;
        }
        else
        {
            Destroy(month);
            lockMonth = false;
            monthSentinel = monthSequence;
        }
    }

	// Update is called once per frame
	void Update () {
        if (!stopTime)//If True, Gametime Stops
        {
            seconds += Time.deltaTime; //Time.deltatime is the time per frame and translates into Seconds, the multiplier is just for Test reasons to speed up time
            secondsText.text = seconds.ToString();//Display Seconds, For Tests Purpose Only
            TimeCalculator(seconds);//Translate the current time into Weeks, Month and Years

            if (timeArray[2] == 10)//End the Game Condition
            {
                timeText.text = "The End";
                Debug.Log("The End");
            }
            timeText.text = "Year: " + timeArray[2] + "; Month: " + (timeArray[1] % 12) + "; Week: " + (timeArray[0] % 4);//Display current Time in Weeks, Month and Years, for test only
            yearText.text = (timeArray[2] + 1).ToString();//Adjust the YearText
            displayMonth(timeArray[3]);
            callNewsManagement.DisplayNews(seconds);
        }
	}
}
