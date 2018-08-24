using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dicts : MonoBehaviour {


    /// <summary>
    /// Used for the NewsBox, which consist of 8 possible Entry slots
    /// </summary>
    public static Dictionary<int, string> dictNews = new Dictionary<int, string>()
    {
        {0,null},
        {1,null},
        {2,null},
        {3,null},
        {4,null},
        {5,null},
        {6,null},
        {7,null}
    };

    /// <summary>
    /// This Dict is used to save the Week when the related news appeard. It is in direct relation to
    /// the Dictionary dictNews and is initialized with all weeks = 0
    /// </summary>
    public static Dictionary<int, int> dictNewsWeek = new Dictionary<int, int>()
    {
        {0,0},
        {1,0},
        {2,0},
        {3,0},
        {4,0},
        {5,0},
        {6,0},
        {7,0}
    };


    /// <summary>
    /// In here go all the possible Newsmessages which can be displayed in Game. If we have messages that occure more 
    /// than once, such as "The building Upgrade has been completed", we can use a key specific integer value such as
    /// 999: Building Upgrade Message, 998: Fight Message, 997: etc etc.
    /// </summary>
    public static Dictionary<int, string> dictTexts = new Dictionary<int, string>()//Dict with all the News Messages
    {
        {2,"Ein Fremder trifft in eurem Lager ein und bittet euch um einen gefallen..."},
        {8,"Get Rekt or Git Better"},
        {10,"Sup Bro?"},
        {12,"We Hawt"},
        {14,"Two plus two is four minus one thats three quick math"},
        {16,"Hoooi Baaabbpe"},
        {20,"U dunno"},
        {26,"Hoi zamen, ih verchaufe eu eh Bier für 15 SCHÄDE"},
        {30,"News3"},
        {50,"News4"},
        {70,"It is I who alone can tell thee of his saga..."},
        {72,"A witch read from the palm of his hand..." }
    };

    /// <summary>
    /// 
    /// </summary>
    public static Dictionary<string, float> dictDuration = new Dictionary<string, float>()
    {
        {"Tower", 3},
        {"House", 5},
        {"Mill", 3},
        {"Foresthut", 20}
    };

    /// <summary>
    /// 
    /// </summary>
    public static Dictionary<string, float> dictProgress = new Dictionary<string, float>()
    {
        {"Tower", 0},
        {"House", 0},
        {"Mill", 0},
        {"Foresthut", 0}
    };

    /// <summary>
    /// This Dict keeps track of which Buttons in the Menu can alrdy be used (1) and which are not accessible yet (0)
    /// IMPORTANT: To activate a new Button in the Menu, we must set its key value to 1. To deactivate it we must set it to 0
    /// </summary>
    public static Dictionary<string, int> dictMenuButtonsAvailable = new Dictionary<string, int>()
    {
        {"build",1},//Build Button, initially accessible
        {"druid",1},//Druid Button, initially accessible
        {"battle",1},//Battle Button, initially not accessible
        {"quest",1},//Quest Button, initially not accessible
        {"merc",1},//Mercenaries Button, initially not accessible
        {"library",1}//Library Button, initially not accessible
    };

    /// <summary>
    /// This Dict consists of all the information showed in the monthInfoBox. Each entry is either a Subtitle or the relating SubText (see remarks below).
    /// In Order to see more than just the first Subtitle and SubText, the player needs to research more information. He can research to a max of
    /// 8 Entries in the monthInfoBox
    /// </summary>
    public static Dictionary<int, string> dictMonthInfoBox = new Dictionary<int, string>()//Dict with all the News Messages
    {
        {0,null},//SubTitleA
        {1,null},//SubTextA
        {2,null},//SubTitleB
        {3,null},//SubTitleB
        {4,null},
        {5,null},
        {6,null},
        {7,null},
        {8,null},
        {9,null},
        {10,null},
        {11,null},
        {12,null},
        {13,null},
        {14,null},
        {15,null}
    };

    /// <summary>
    /// This is the ResearchDict! In here are all the possible researches of the game. 0 means the research has not been learned yet, 1 means it has been learned.
    /// </summary>
    public static Dictionary<string,int> dictResearch = new Dictionary<string,int>()//Dict with all the News Messages
    {
        {"monthInfoBoxResearch1",0},//Name is just a placeholder, can be replaced by something like "meteorology" etc etc.
        {"monthInfoBoxResearch2",0},
        {"monthInfoBoxResearch3",0},
        {"monthInfoBoxResearch4",0},
        {"monthInfoBoxResearch5",0},
        {"monthInfoBoxResearch6",0},
        {"monthInfoBoxResearch7",0}
    };
}
