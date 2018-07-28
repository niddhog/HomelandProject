using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EmblemGrafiks : MonoBehaviour {

    static public Image ImageComponents;



    public static Sprite Laisom;
    public static Sprite Ilithris;
    public static Sprite Wainu;
    public static Sprite Anjar;
    public static Sprite Sophtor;
    public static Sprite Eilum;
    public static Sprite Janafi;
    public static Sprite Tirnum;
    public static Sprite Bel;
    public static Sprite Dordonum;
    public static Sprite Yoilum;
    public static Sprite Krom;

    void Start()
    {

        ImageComponents = GetComponent<Image>();
        for (int i = 0; i<12; i++){
           //MonthlistOut[i] = MonthList[i];
        }
    }


}
