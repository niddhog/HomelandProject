using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    private GameObject menuButton;
    private float yPos = -195f;
    private int dictIndex;
    private Image buttonImage;
    private Animator animator;

    public bool menuActive = false;
    public GameObject buildButton;
    public GameObject druidButton;
    public GameObject battleButton;
    public GameObject questButton;
    public GameObject mercButton;
    public GameObject libraryButton;
    public Sprite buttonA;
    public Sprite buttonB;
    public RuntimeAnimatorController animationEven;
    public RuntimeAnimatorController animationUneven;

    /// <summary>
    /// Dictionary for the menu Buttons. There are (so far) 6 possible Button Slots. This Dict has to be
    /// used inside this script, as GameObjects can not be of static type
    /// </summary>
    public static Dictionary<int, GameObject> dictMenuButtons = new Dictionary<int, GameObject>()
    {
        {0,null},
        {1,null},
        {2,null},
        {3,null},
        {4,null},
        {5,null},
    };


    public void CallMenu()
    {
        if (!menuActive)//Menu inactive
        {
            SetupButtonOrder();
            InstantiateMenu();
        }
        else//Menu active
        {
            DestroyMenu();
        }
    }

    /// <summary>
    /// Check weither individual Menu Buttons are available or not (e.g. discovered or not yet discovered)
    /// </summary>
    private void SetupButtonOrder()
    {
        dictIndex = 0;
        if (Dicts.dictMenuButtonsAvailable["build"] != 0)
        {
            dictMenuButtons[dictIndex] = buildButton;
            dictIndex += 1;
        }
        if (Dicts.dictMenuButtonsAvailable["druid"] != 0)
        {
            dictMenuButtons[dictIndex] = druidButton;
            dictIndex += 1;
        }
        if (Dicts.dictMenuButtonsAvailable["battle"] != 0)
        {
            dictMenuButtons[dictIndex] = battleButton;
            dictIndex += 1;
        }
        if (Dicts.dictMenuButtonsAvailable["quest"] != 0)
        {
            dictMenuButtons[dictIndex] = questButton;
            dictIndex += 1;
        }
        if (Dicts.dictMenuButtonsAvailable["merc"] != 0)
        {
            dictMenuButtons[dictIndex] = mercButton;
            dictIndex += 1;
        }
        if (Dicts.dictMenuButtonsAvailable["library"] != 0)
        {
            dictMenuButtons[dictIndex] = libraryButton;
        }
    }

    /// <summary>
    /// Sets up the Menu by placing the individual buttons to the right place
    /// </summary>
    private void InstantiateMenu()
    {
        for(int i = 0; i< 6; i++)
        {
            if(dictMenuButtons[i] != null)
            {
                menuButton = Instantiate(dictMenuButtons[i]) as GameObject;
                buttonImage = menuButton.GetComponent<Image>();
                animator = menuButton.GetComponent<Animator>();
                if (i % 2 == 0)//check weither i is even
                {
                    buttonImage.sprite = buttonA;
                    animator.runtimeAnimatorController = animationEven;
                }
                else
                {
                    buttonImage.sprite = buttonB;
                    animator.runtimeAnimatorController = animationUneven;
                }
                menuButton.transform.SetParent(GameObject.Find("MenuButton").transform, false);
                menuButton.transform.position = new Vector3(0, yPos, 0);
                yPos += 40;
            }
        }
        menuActive = true;
    }

    private void DestroyMenu()
    {
        menuActive = false;
    }
}
