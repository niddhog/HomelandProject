using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManagement : MonoBehaviour {

    public static int CoinBaseIncome;//Baseincome that triggers each 5 seconds
    public static int coin;//This integer holds all the coin the player possesses
    public static bool animateIncome;

    public Text coinZahlText;
    public Text coinText;
    public GameObject coinZahlAnimation;


    private Text textColor;
    private Text incomeAnimationText;
    private string apostroph;
    private float incomeAnimationLength;
    private Animator incomeAnimator;

    /// <summary>
    /// Updates the CoinZahlText and the CoinText
    /// </summary>
    private string UpdateCoinTexts(int value)
    {
        if(value > 999999999)//Ceiling, the player can't have any more money
        {
            value = 999999999;
        }
        if(value < 0)//If the Coin falls below 0, it is displayed in red colour
        {
            textColor = coinText.GetComponent<Text>();
            textColor.color = new Color(255f, 0f, 0f);
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
        return apostroph;
    } 


	void Start () {
        coin = 1000; //Amount of Coin the player starts the game with
        CoinBaseIncome = 10;
        coinZahlText.text = UpdateCoinTexts(coin);
        incomeAnimationText = coinZahlAnimation.GetComponent<Text>();
    }
	
    public static void SetCoins()
    {
        coin += CoinBaseIncome;
    }

    private void Update()
    {
        coinZahlText.text = UpdateCoinTexts(coin);
        if (animateIncome)
        {
            incomeAnimationText.text = "+" + UpdateCoinTexts(CoinBaseIncome);//Darstellung noch nicht korrekt
            coinZahlAnimation.SetActive(true);
            incomeAnimator = GameObject.Find("CoinZahlAnimation").GetComponent<Animator>();
            incomeAnimationLength = incomeAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(WaitForIncomeAnimation(incomeAnimationLength));
            animateIncome = false;
        }
    }

    IEnumerator WaitForIncomeAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        coinZahlAnimation.SetActive(false);
    }
}
