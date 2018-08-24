using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WisdomManagement : MonoBehaviour {

    public static int wisdom;//holds the number of mercenaries the player has in his realm
    public static int wisdomGain;//the number of mercs GAINED over time in a constant timefrequency
    public static int wisdomLoss;//the number of mercs LOST over time in a constant timefrequency
    public static bool animateWisdomGain;
    public static bool animateWisdomLoss;

    public Text wisdomText;
    public Text wisdomZahlText;
    public GameObject wisdomInfoBox;
    public RuntimeAnimatorController infoBoxIn;
    public RuntimeAnimatorController infoBoxOut;
    public GameObject wisdomZahlAnimationIncome;
    public GameObject wisdomZahlAnimationCost;

    private string apostroph;
    private GameObject wisdomInfoBoxClone;
    private Text wisdomGainAnimationText;
    private Text wisdomLossAnimationText;
    private Text wisdomInfoGainZahl;
    private Text wisdomInfoLossZahl;
    private Animator wisdomGainAnimator;
    private Animator wisdomLossAnimator;
    private Animator infoBoxAnimator;
    private GameObject parent;
    private bool wisdomInfoBoxActive;
    private GameObject wisdomAnimatedNumbers;


    /// <summary>
    /// Updates the CoinZahlText and the CoinText
    /// </summary>
    private string UpdateWisdomTexts(int value)
    {
        if (value > 999999999)//Ceiling, the player can't have any more mercenaries
        {
            value = 999999999;
            wisdom = 999999999;
        }

        //The Following Code is used to set the apostrophes on the right place
        if (value >= 100000000)
        {
            apostroph = value.ToString().Insert(3, "'").Insert(7, "'");
        }
        else if (value >= 10000000)
        {
            apostroph = value.ToString().Insert(2, "'").Insert(6, "'");
        }
        else if (value >= 1000000)
        {
            apostroph = value.ToString().Insert(1, "'").Insert(5, "'");
        }
        else if (value >= 100000)
        {
            apostroph = value.ToString().Insert(3, "'");
        }
        else if (value >= 10000)
        {
            apostroph = value.ToString().Insert(2, "'");
        }
        else if (value >= 1000)
        {
            apostroph = value.ToString().Insert(1, "'");
        }
        else
        {
            apostroph = value.ToString();
        }
        if (value < 0)
        {
            value = 0;
            wisdom = 0;
        }
        return apostroph;
    }

    void Start () {
        wisdomInfoBoxActive = false;
        wisdom = 0;//Player starts the game with 0 Mercenaries in his realm
        wisdomGain = 150;
        wisdomLoss = 45;
	}

    public static void AddWisdom()
    {
        wisdom += wisdomGain;
    }

    public static void SubtractWisdom()
    {
        wisdom -= wisdomLoss;
    }

    /// <summary>
    /// If Mouse hovers over the CoinPanel, a new infotab is displayed showing the current income and current Costs
    /// </summary>
    private void OnMouseOver()
    {
        if (!wisdomInfoBoxActive)
        {
            wisdomInfoBoxClone = Instantiate(wisdomInfoBox) as GameObject;
            wisdomInfoGainZahl = GameObject.Find("wisdomGainZahl").GetComponent<Text>();
            wisdomInfoGainZahl.text = UpdateWisdomTexts(wisdomGain);
            wisdomInfoLossZahl = GameObject.Find("wisdomCostZahl").GetComponent<Text>();
            wisdomInfoLossZahl.text = UpdateWisdomTexts(wisdomLoss);
            infoBoxAnimator = wisdomInfoBoxClone.GetComponent<Animator>();
            infoBoxAnimator.runtimeAnimatorController = infoBoxIn;
            wisdomInfoBoxClone.transform.SetParent(GameObject.Find("InfoBoxesWisdom").transform, false);
            wisdomInfoBoxClone.transform.position = new Vector3(0, 0, 0);
            wisdomInfoBoxActive = true;
        }
    }

    private void OnMouseExit()
    {
        if (wisdomInfoBoxActive)
        {
            infoBoxAnimator.runtimeAnimatorController = infoBoxOut;
            StartCoroutine(WaitForInfoBoxDestroy(infoBoxAnimator.GetCurrentAnimatorStateInfo(0).length));
        }
    }


    private void Update()
    {
        wisdomZahlText.text = UpdateWisdomTexts(wisdom);

        if (wisdomGain == 0) { }//if the baseIncome is 0, no AnimationText is displayed
        else
        {
            if (animateWisdomGain)
            {
                wisdomAnimatedNumbers = Instantiate(wisdomZahlAnimationIncome) as GameObject;//child(0) = CoinIcon, child(1) = MercIcon, child(2) = WisdomIcon
                wisdomAnimatedNumbers.transform.SetParent(GameObject.Find("WisdomZahlenAnimation").transform, false);
                wisdomAnimatedNumbers.transform.GetChild(0).gameObject.SetActive(false);//Sets Coin Icon inactive as we only want child(2), the Wisdom Icon
                wisdomAnimatedNumbers.transform.GetChild(1).gameObject.SetActive(false);//Sets Merc Icon inactive as we only want child(2), the Wisdom Icon
                wisdomAnimatedNumbers.GetComponent<Text>().text = "+" + UpdateWisdomTexts(wisdomGain);
                wisdomGainAnimator = wisdomAnimatedNumbers.GetComponent<Animator>();
                StartCoroutine(WaitForWisdomAnimation(wisdomGainAnimator.GetCurrentAnimatorStateInfo(0).length));
                animateWisdomGain = false;
            }
        }

        if (wisdomLoss == 0) { }//if the baseCost is 0, no AnimationText is displayed
        else
        {
            if (animateWisdomLoss)
            {
                wisdomAnimatedNumbers = Instantiate(wisdomZahlAnimationCost) as GameObject;
                wisdomAnimatedNumbers.transform.SetParent(GameObject.Find("WisdomZahlenAnimation").transform, false);
                wisdomAnimatedNumbers.transform.GetChild(0).gameObject.SetActive(false);//Sets Coin Icon inactive as we only want child(2), the Wisdom Icon
                wisdomAnimatedNumbers.transform.GetChild(1).gameObject.SetActive(false);//Sets Merc Icon inactive as we only want child(2), the Wisdom Icon
                wisdomAnimatedNumbers.GetComponent<Text>().text = "-" + UpdateWisdomTexts(wisdomLoss);
                wisdomLossAnimator = wisdomAnimatedNumbers.GetComponent<Animator>();
                StartCoroutine(WaitForWisdomAnimation(wisdomLossAnimator.GetCurrentAnimatorStateInfo(0).length));
                animateWisdomLoss = false;
            }
        }
    }

    IEnumerator WaitForInfoBoxDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        parent = GameObject.Find("InfoBoxesWisdom");
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        wisdomInfoBoxActive = false;
    }

    IEnumerator WaitForWisdomAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        foreach (Transform child in GameObject.Find("WisdomZahlenAnimation").transform)
        {
            Destroy(child.gameObject);
        }
    }
}
