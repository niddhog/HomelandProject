using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManagement : MonoBehaviour {
    public GameObject[] MonthList = new GameObject[12];
    public Transform zeiger;
    public Text timeText;
    static public bool StopTime = false; //um die Zeit anzuhalten
    public GameObject MonthPlacer;
    public Text yearText;
    NewsManagement CallNewsManagement;


    private const float secondsToDegrees = 360f / 240f; //wird für die EulerRotation benötigt
    private int[] timeArray = new int[4]; //Timearray hat 3 Felder, Week(0), Month(1) und Year(2), speichert Total an Wochen, Monaten, jahren
    private float seconds; //Anzahl Sekunden, die das Spiel bereits läuft
    private bool lockMonth;//sorgt dafür, dass das Monatsemblem nicht bei jedem Update neu instantiated wird
    private static int monthSentinel;
    private GameObject tempMonth;

    // Use this for initialization
    int[] TimeCalculator(float t) //Eine Woche dauert 1 Minute
    {
       zeiger.localRotation =
                Quaternion.Euler(0f, 0f, t * -secondsToDegrees);
        timeArray[0] = ((int)t / 60); //Week
        timeArray[1] = ((int)t / 240); //Month
        timeArray[2] = (int)t / 2880; //Year
        timeArray[3] = timeArray[1] % 12; //Month in Sequence of 12 (0,1,2,3,...,12)
        return timeArray;
    }

    void displayMonth(int monthSequence) //updates und displays das Monats Emblem
    {
        if (!lockMonth)
        {
            //Hier wird das Emblem erstellt (instantiate) und anschliessend plaziert:
            GameObject month = (GameObject)Instantiate(MonthList[monthSequence], transform.position = new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            month.transform.parent = GameObject.Find("MiddleCanvas").transform;
            month.transform.parent = GameObject.Find("MonthPosition").transform;
            month.transform.position = GameObject.Find("MonthPosition").transform.position;
            tempMonth = month; //Typecast ist nötig wegen Cancer Unity...
        }

        if (monthSentinel == monthSequence)
        {
            lockMonth = true;
        }
        else
        {
            Destroy(tempMonth);
            lockMonth = false;
            monthSentinel = monthSequence;
        }
    }

    void Start () {
        yearText.text = "1";
        lockMonth = false;
        monthSentinel = 0; //prüft ob sich der Monat verändert hat

    }
	
	// Update is called once per frame
	void Update () {
        if (!StopTime)//Hält Zeit an solange true
        {
            seconds += Time.deltaTime*10; //Time.time gibt die Zeit in Sekunden an, hier wird die Zeit vom Spielstart bis zum Timer start abgezogen, d.h. Timer wird genullt
            TimeCalculator(seconds);

            if (timeArray[2] == 10)//Spiel endet nach 10 jahren, End Condition
            {
                timeText.text = "The End";
                Debug.Log("The End");
            }
            timeText.text = "Year: " + timeArray[2] + "; Month: " + (timeArray[1] % 12) + "; Week: " + (timeArray[0] % 4);
            yearText.text = (timeArray[2] + 1).ToString();
            displayMonth(timeArray[3]);
            //CallNewsManagement.DisplayNews(seconds);
        }
        
	}
}
