using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; //Wichtiges Package
using UnityEngine;

public class AktionsMenu : MonoBehaviour {

    public TimeManagement StopCounterDebug;
    int bauZahl;//wieviel wir bauen können
    public Text bauZahlText; //wir erschaffen ein Objekt mit Bezug aufs Interface, Text direkt ansteuerbar dank UnityEngine.UI Package, sonst Gameobject

    // Use this for initialization
    void Start () {

        bauZahl = 1;//initialisiert bauZahl
}
	
	// Update is called once per frame
	void Update () {
		

	}

   public void bauenOnClick()
    {
        if(TimeManagement.stopTime == true)
        {
            TimeManagement.stopTime = false;
        }
        else
        {
            TimeManagement.stopTime = true;
            StopCounterDebug.seconds += 0.51f;//nötig um den Rundungsfehler auszumerzen
        }
        if (bauZahl > 0) {
            bauZahl--;
        }
        Debug.Log(bauZahl); //zeigt uns die Variabel Storecount in der Konsole an
        //bauZahlText.text = bauZahl.ToString();
    }
}
