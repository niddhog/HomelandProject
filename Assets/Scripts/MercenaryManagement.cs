using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryManagement : MonoBehaviour {

    public static int merc;//holds the number of mercenaries the player has in his realm
    public static int mercGain;//the number of mercs GAINED over time in a constant timefrequency
    public static int mercLoss;//the number of mercs LOST over time in a constant timefrequency
    public static bool animateMercGain;
    public static bool animateMercLoss;

    public Text mercText;
    public Text mercZahlText;
    public GameObject mercInfoBox;
    public RuntimeAnimatorController infoBoxIn;
    public RuntimeAnimatorController infoBoxOut;
    public GameObject mercZahlAnimationIncome;
    public GameObject mercZahlAnimationCost;

    private string apostroph;
    private GameObject mercInfoBoxClone;
    private Text mercGainAnimationText;
    private Text mercLossAnimationText;
    private Text mercInfoGainZahl;
    private Text mercInfoLossZahl;
    private Animator mercGainAnimator;
    private Animator mercLossAnimator;
    private Animator infoBoxAnimator;
    private GameObject parent;
    private bool mercInfoBoxActive;
    private Text mercIncomeAnimationText;
    private Text mercCostAnimationText;
    private float mercCostAnimationLength;
    private GameObject mercAnimatedNumbers;


    /// <summary>
    /// Updates the CoinZahlText and the CoinText
    /// </summary>
    private string UpdateMercTexts(int value)
    {
        if (value > 999999999)//Ceiling, the player can't have any more mercenaries
        {
            value = 999999999;
            merc = 999999999;
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
            merc = 0;
        }
        return apostroph;
    }

    void Start()
    {
        merc = 0;//Player starts the game with 0 Mercenaries in his realm
        mercGain = 37;
        mercLoss = 11;
        mercInfoBoxActive = false;
    }


    public static void AddMercs()
    {
        merc += mercGain;
    }

    public static void SubtractMercs()
    {
        merc -= mercLoss;
    }

    /// <summary>
    /// If Mouse hovers over the CoinPanel, a new infotab is displayed showing the current income and current Costs
    /// </summary>
    private void OnMouseOver()
    {
            if (!mercInfoBoxActive)
            {
                mercInfoBoxClone = Instantiate(mercInfoBox) as GameObject;
                mercInfoGainZahl = GameObject.Find("mercGainZahl").GetComponent<Text>();
                mercInfoGainZahl.text = UpdateMercTexts(mercGain);
                mercInfoLossZahl = GameObject.Find("mercCostZahl").GetComponent<Text>();
                mercInfoLossZahl.text = UpdateMercTexts(mercLoss);
                infoBoxAnimator = mercInfoBoxClone.GetComponent<Animator>();
                infoBoxAnimator.runtimeAnimatorController = infoBoxIn;
                mercInfoBoxClone.transform.SetParent(GameObject.Find("InfoBoxesMerc").transform, false);
                mercInfoBoxClone.transform.position = new Vector3(0, 0, 0);
                mercInfoBoxActive = true;
            }
    }

    private void OnMouseExit()
    {
        if (mercInfoBoxActive)
        {
            infoBoxAnimator.runtimeAnimatorController = infoBoxOut;
            StartCoroutine(WaitForInfoBoxDestroy(infoBoxAnimator.GetCurrentAnimatorStateInfo(0).length));
        }
    }

    private void Update()
    {
        mercZahlText.text = UpdateMercTexts(merc);
        if (mercGain == 0) { }//if the baseIncome is 0, no AnimationText is displayed
        else
        {
            if (animateMercGain)
            {
                mercAnimatedNumbers = Instantiate(mercZahlAnimationIncome) as GameObject;//child(0) = CoinIcon, child(1) = MercIcon, child(2) = WisdomIcon
                mercAnimatedNumbers.transform.SetParent(GameObject.Find("MercZahlenAnimation").transform, false);
                mercAnimatedNumbers.transform.GetChild(0).gameObject.SetActive(false);//Sets Coin Icon inactive as we only want child(1), the Merc Icon
                mercAnimatedNumbers.transform.GetChild(2).gameObject.SetActive(false);//Sets Wisdom Icon inactive as we only want child(1), the Merc Icon
                mercAnimatedNumbers.GetComponent<Text>().text = "+" + UpdateMercTexts(mercGain);
                mercGainAnimator = mercAnimatedNumbers.GetComponent<Animator>();
                StartCoroutine(WaitForMercAnimation(mercGainAnimator.GetCurrentAnimatorStateInfo(0).length));
                animateMercGain = false;
            }
        }

        if (mercLoss == 0) { }//if the baseCost is 0, no AnimationText is displayed
        else
        {
            if (animateMercLoss)
            {
                mercAnimatedNumbers = Instantiate(mercZahlAnimationCost) as GameObject;
                mercAnimatedNumbers.transform.SetParent(GameObject.Find("MercZahlenAnimation").transform, false);
                mercAnimatedNumbers.transform.GetChild(0).gameObject.SetActive(false);//Sets Coin Icon inactive as we only want child(1), the Merc Icon
                mercAnimatedNumbers.transform.GetChild(2).gameObject.SetActive(false);//Sets Wisdom Icon inactive as we only want child(1), the Merc Icon
                mercAnimatedNumbers.GetComponent<Text>().text = "-" + UpdateMercTexts(mercLoss);
                mercLossAnimator = mercAnimatedNumbers.GetComponent<Animator>();
                StartCoroutine(WaitForMercAnimation(mercLossAnimator.GetCurrentAnimatorStateInfo(0).length));
                animateMercLoss = false;
            }
        }
    }


    IEnumerator WaitForInfoBoxDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        parent = GameObject.Find("InfoBoxesMerc");
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        mercInfoBoxActive = false;
    }

    IEnumerator WaitForMercAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        foreach (Transform child in GameObject.Find("MercZahlenAnimation").transform)
        {
            Destroy(child.gameObject);
        }
    }
}
