﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    private GameObject menuButton;
    private int deleteIndex;
    private float yPos = -195f;
    private int dictIndex;
    private Image buttonImage;
    private Animator animator;
    private float MenuExitDuration;

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
    public RuntimeAnimatorController OutanimationEven;
    public RuntimeAnimatorController OutanimationUneven;
    public static bool menuIsActive = false;

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

    /// <summary>
    /// Function which is linked to the NewsButton to call or deactivate the menu
    /// </summary>
    public void CallMenu()
    {
        if (!menuActive)//Menu inactive
        {
            DestroyChildren();
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

    /// <summary>
    /// Function is called when Player exits the Menu. This function manages the animation setup
    /// for each individual button and then calls the DestroyChild() Function which deletes
    /// each individual button from the hierarchy
    /// </summary>
    private void DestroyMenu()
    {
        deleteIndex = 0;
        foreach (Transform child in transform)
        {
            animator = child.GetComponent<Animator>();
            if (deleteIndex == 0)//Child 0 is the text of the button, we don't want anything to happen here
            {

            }
            else if (deleteIndex % 2 == 0)//checks if button is in even position or not
            {
                animator.runtimeAnimatorController = OutanimationEven;
            }
            else
            {
                animator.runtimeAnimatorController = OutanimationUneven;
            }
            deleteIndex += 1;
        }
        MenuExitDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(WaitBeforeDestroy(MenuExitDuration));
        menuActive = false;
        yPos = -195f;
    }


    /// <summary>
    /// Child with Index 0 is the Text in the MenuButton Gameobject and does not need to be destroyed, whereas
    /// the other children are instantiated buttons which all need to be destroyed. This function handles it.
    /// </summary>
    private void DestroyChildren()
    {
        deleteIndex = 0;
        foreach (Transform child in transform)
        {
            if(deleteIndex == 0)
            {
            }
            else
            {
                Destroy(child.gameObject);
            }
            deleteIndex += 1;
        }
    }

    /// <summary>
    /// This IEnumerator is used in order to wait until the exit Animation is over befor deleting the indivudial Buttons (children)
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator WaitBeforeDestroy(float seconds)//This Function is used to wait "float seconds" seconds until the code below yield return... is executed
    {
        yield return new WaitForSeconds(seconds);
        if (menuActive)
        {
            yield break;
        }
        DestroyChildren();
        yield return new WaitForSeconds(0.0001f);
    }
}
