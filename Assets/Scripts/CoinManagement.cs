using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManagement : MonoBehaviour {

    public static int coinBaseIncome;//Baseincome that triggers each 5 seconds
    public static int coinBaseCost;//BaseCost that trigger each other 5 seconds
    public static int coin;//This integer holds all the coin the player possesses
    public static bool animateIncome;
    public static bool animateCost;

    public Text coinZahlText;
    public Text coinText;
    public GameObject coinZahlAnimationIncome;
    public GameObject coinZahlAnimationCost;
    public GameObject coinInfoBox;
    public RuntimeAnimatorController infoBoxIn;
    public RuntimeAnimatorController infoBoxOut;

    private Text coinTextColor;
    private Text coinZahlColor;
    private Text coinInfoIncomeZahl;
    private Text coinInfoCostZahl;
    private string apostroph;
    private float coinCostAnimationLength;
    private Animator coinIncomeAnimator;
    private Animator coinCostAnimator;
    private Animator infoBoxAnimator;
    private GameObject coinInfoBoxClone;
    private bool coinInfoBoxActive;
    private GameObject coinAnimatedNumbers;

    /// <summary>
    /// Updates the CoinZahlText and the CoinText
    /// </summary>
    private string UpdateCoinTexts(int value)
    {
        if (value > 999999999)//Ceiling, the player can't have any more money
        {
            value = 999999999;
            coin = 999999999;
        }
        if(value < 0)//If the Coin falls below 0, it is displayed in red colour
        {
            coinTextColor = coinText.GetComponent<Text>();
            coinZahlColor = coinZahlText.GetComponent<Text>();
            coinTextColor.color = new Color(255f, 0, 0);
            coinZahlColor.color = new Color(255f, 0, 0);
        }
        else
        {
            coinTextColor = coinText.GetComponent<Text>();
            coinZahlColor = coinZahlText.GetComponent<Text>();
            coinTextColor.color = new Color(0, 0, 0);
            coinZahlColor.color = new Color(0, 0, 0);
        }
        //The Following Code is used to set the apostrophes on the right place
        if(value >= 100000000)
        {
            apostroph = value.ToString().Insert(3, "'").Insert(7,"'");
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
            if (value <= -100000000)
            {
                apostroph = value.ToString().Insert(4, "'").Insert(8, "'");
            }
            else if (value <= -10000000)
            {
                apostroph = value.ToString().Insert(3, "'").Insert(7, "'");
            }
            else if (value <= -1000000)
            {
                apostroph = value.ToString().Insert(2, "'").Insert(6, "'");
            }
            else if (value <= -100000)
            {
                apostroph = value.ToString().Insert(4, "'");
            }
            else if (value <= -10000)
            {
                apostroph = value.ToString().Insert(3, "'");
            }
            else if (value <= -1000)
            {
                apostroph = value.ToString().Insert(2, "'");
            }
            else
            {
                apostroph = value.ToString();
            }
        }
        return apostroph;
    } 


	void Start () {
        coinInfoBoxActive = false;
        coin = 1000; //Amount of Coin the player starts the game with
        coinBaseIncome = 87;
        coinBaseCost = 25;
        coinZahlText.text = UpdateCoinTexts(coin);
    }
	
    public static void AddCoins()
    {
        coin += coinBaseIncome;
    }

    public static void SubtractCoins()
    {
        coin -= coinBaseCost;
    }

    private void Update()
    {
        coinZahlText.text = UpdateCoinTexts(coin);//on each Tick, the coinnumber text is formated with apostrophs
        if (coinBaseIncome == 0) { }//if the baseincome is 0, no AnimationText is displayed
        else
        {
            if (animateIncome)//child(0) = CoinIcon, child(1) = MercIcon, child(2) = WisdomIcon
            {
                coinAnimatedNumbers = Instantiate(coinZahlAnimationIncome) as GameObject;
                coinAnimatedNumbers.transform.SetParent(GameObject.Find("CoinZahlenAnimation").transform, false);
                coinAnimatedNumbers.transform.GetChild(1).gameObject.SetActive(false);//Sets Merc Icon inactive as we only want child(0), the Coin Icon
                coinAnimatedNumbers.transform.GetChild(2).gameObject.SetActive(false);//Sets Wisdom Icon inactive as we only want child(0), the Coin Icon
                coinAnimatedNumbers.GetComponent<Text>().text = "+" + UpdateCoinTexts(coinBaseIncome);
                coinIncomeAnimator = coinAnimatedNumbers.GetComponent<Animator>();
                StartCoroutine(WaitForCoinAnimation(coinIncomeAnimator.GetCurrentAnimatorStateInfo(0).length));
                animateIncome = false;
            }
        }

        if (coinBaseCost == 0) { }//if the baseCost is 0, no AnimationText is displayed
        else
        {
            if (animateCost)
            {
                coinAnimatedNumbers = Instantiate(coinZahlAnimationCost) as GameObject;
                coinAnimatedNumbers.transform.SetParent(GameObject.Find("CoinZahlenAnimation").transform, false);
                coinAnimatedNumbers.transform.GetChild(1).gameObject.SetActive(false);//Sets Merc Icon inactive as we only want child(0), the Coin Icon
                coinAnimatedNumbers.transform.GetChild(2).gameObject.SetActive(false);//Sets Wisdom Icon inactive as we only want child(0), the Coin Icon
                coinAnimatedNumbers.GetComponent<Text>().text = "-" + UpdateCoinTexts(coinBaseCost);
                coinIncomeAnimator = coinAnimatedNumbers.GetComponent<Animator>();
                StartCoroutine(WaitForCoinAnimation(coinIncomeAnimator.GetCurrentAnimatorStateInfo(0).length));
                animateCost = false;
            }
        }
    }

    /// <summary>
    /// If Mouse hovers over the CoinPanel, a new infotab is displayed showing the current income and current Costs
    /// </summary>
    private void OnMouseOver()
    {
            if (!coinInfoBoxActive)
            {
                coinInfoBoxClone = Instantiate(coinInfoBox) as GameObject;
                coinInfoIncomeZahl = GameObject.Find("CoinGainZahl").GetComponent<Text>();
                coinInfoIncomeZahl.text = UpdateCoinTexts(coinBaseIncome);
                coinInfoCostZahl = GameObject.Find("CoinCostZahl").GetComponent<Text>();
                coinInfoCostZahl.text = UpdateCoinTexts(coinBaseCost);
                infoBoxAnimator = coinInfoBoxClone.GetComponent<Animator>();
                infoBoxAnimator.runtimeAnimatorController = infoBoxIn;
                coinInfoBoxClone.transform.SetParent(GameObject.Find("InfoBoxesCoin").transform, false);
                coinInfoBoxClone.transform.position = new Vector3(0, 0, 0);
                coinInfoBoxActive = true;
            }
    }

    private void OnMouseExit()
    {
        if (coinInfoBoxActive)
        {
            infoBoxAnimator.runtimeAnimatorController = infoBoxOut;
            StartCoroutine(WaitForInfoBoxDestroy(infoBoxAnimator.GetCurrentAnimatorStateInfo(0).length));
        }
    }

    IEnumerator WaitForCoinAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        foreach (Transform child in GameObject.Find("CoinZahlenAnimation").transform)
        {
            Destroy(child.gameObject);
        }
    }

    IEnumerator WaitForInfoBoxDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        foreach(Transform child in GameObject.Find("InfoBoxesCoin").transform)
        {
            Destroy(child.gameObject);
        }
        coinInfoBoxActive = false;
    }
}


