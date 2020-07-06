using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    private Button button;

    private GameObject canvas;

    private CentralController controller;

    // Use this for initialization
    void Start()
    {
        SetGameobjects();
        SetUpButton();
    }

    /// <summary>
    /// Finds gameobjects
    /// </summary>
    void FindGameObjects()
    {
        controller = GameObject.FindObjectOfType<CentralController>();
    }

    /// <summary>
    /// Here we determine what the button is supposed to return "Yes" or "No"
    /// </summary>
    private bool DetermineButtonPurpose()
    {
        if (this.gameObject.name == "Yes")
        {
            return true;
        }
        else if (this.gameObject.name == "No")
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// Here we assign gameobjects to their gameobjects
    /// </summary>
    private void SetGameobjects()
    {
        button = this.gameObject.GetComponent<Button>();
        canvas = this.gameObject.transform.parent.gameObject;
        FindGameObjects();
    }

    /// <summary>
    /// Here we add the necesary parameters for the button to function
    /// </summary>
    private void SetUpButton()
    {
        button.onClick.AddListener(OnClick);            // Here we add a listener that says if the button was clicked and executes the code if it was
    }

    /// <summary>
    /// On click we show an ad, give some coinsand go back to previous screen
    /// </summary>
    private void OnClick()
    {
        if(DetermineButtonPurpose())
        {
            ADSManager.instance.ShowRewardedVideoAD();
            controller.AddToCurrentCoins(controller.adsReward);
        }
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}