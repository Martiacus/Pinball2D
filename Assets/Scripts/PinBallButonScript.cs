using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinBallButonScript : MonoBehaviour {

    public GameObject pinBall;

    public GameObject buttonCanvas;

    public Button button;

    public GameObject mainMenuCanvas;

    public CentralController controller;

    private int integer;                            // ID of the item we are buying/selecting


    // Use this for initialization
    void Start()
    {
        AddButtonToGameobject();
        SetUpButton();
        FindGameObjects();
    }

    /// <summary>
    /// Finds game objects needed for the scrippt
    /// </summary>
    void FindGameObjects()
    {
        controller = GameObject.FindObjectOfType<CentralController>();
        buttonCanvas = gameObject.transform.parent.gameObject;
        mainMenuCanvas = buttonCanvas.transform.parent.gameObject;
    }

    /// <summary>
    /// Here we add all the components we need for the button
    /// </summary>
    void AddButtonToGameobject()
    {
        pinBall = gameObject;

        button = gameObject.GetComponent<Button>();
    }

    /// <summary>
    /// Here we add the necesary parameters for the button to function
    /// </summary>
    void SetUpButton()
    {
        button.onClick.AddListener(OnClick);            // Here we add a listener that says if the button was clicked and executes the code if it was
    }

    /// <summary>
    /// Our code that executes the command on button click
    /// </summary>
    void OnClick()
    {
        char[] temp = gameObject.name.ToCharArray();
        char letter = temp[0];
        int.TryParse(letter.ToString(), out integer);

        if (WasItBought(integer))
        {
            controller.UpdatePinBallSprite(integer - 1);
            SaveGameScript();
            SetShopSpriteToCurrent(integer - 1);
            ResetHighliter();
        }
        else if (controller.currentCoins >= (integer - 1) * controller.priceMultiplier)
        {
            ConfirmPurchase();
        }
        else if (controller.adsEnabled)
        {
            SuggestAd();
        }
    }

    /// <summary>
    /// Enables the ads canvas
    /// </summary>
    private void SuggestAd()
    {
        mainMenuCanvas.transform.Find("Ads canvas").gameObject.SetActive(true);
    }

    /// <summary>
    /// Send a request to a different canvas
    /// </summary>
    private void ConfirmPurchase()
    {
        mainMenuCanvas.transform.Find("Yes or No canvas").gameObject.SetActive(true);

        GameObject temp = GameObject.FindGameObjectWithTag("YesOrNoCanvas");

        temp.GetComponent<YesOrNoCanvas>().RecieveSenderInfo(buttonCanvas, this.gameObject);
    }

    /// <summary>
    /// After we confirm purchase we finish it
    /// </summary>
    /// <param name="yesOrNo"></param>
    public void PurchaseConfirmed(bool yesOrNo)
    {
        if (yesOrNo)
        {
            BuyItem(integer);
            SetShopSpriteToCurrent(integer - 1);
            ResetHighliter();
            SaveGameScript();
        }
    }

    /// <summary>
    /// Sets the highliter to this gameobject if it was bough and clicked
    /// </summary>
    private void ResetHighliter()
    {
        GameObject highliter = buttonCanvas.transform.Find("Highlight").gameObject;

        highliter.transform.position = this.gameObject.transform.position;
    }

    /// <summary>
    /// Sets shop menu sprite to current image
    /// </summary>
    /// <param name="integer">ID of the image</param>
    private void SetShopSpriteToCurrent(int integer)
    {
        GameObject shopscreen = mainMenuCanvas.transform.Find("Shop screen").gameObject;
        GameObject pinBall = shopscreen.transform.Find("Pinball").gameObject;

        pinBall.GetComponent<Image>().sprite = controller.pinBallSprites[integer];
    }

    /// <summary>
    /// Saves bought item
    /// </summary>
    /// <param name="id">saves bought item</param>
    private void SaveBoughtItem(int id)
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Save");
        DataToSave save = temp.GetComponent<DataToSave>();

        save.SaveBoughtPinBall(id);

        SaveGame.SaveGameData(save);
    }

    /// <summary>
    /// Determines if the item was bought
    /// </summary>
    /// <param name="integer">Item ID</param>
    /// <returns></returns>
    private bool WasItBought(int integer)
    {
        if (controller.boughtPinBall[integer - 1])
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Buys item, deducts money and destroys price tag
    /// </summary>
    /// <param name="integer">ID of the item</param>
    private void BuyItem(int integer)
    {
        controller.currentCoins -= (integer - 1) * controller.priceMultiplier;
        controller.UpdatePinBallSprite(integer - 1);
        controller.boughtPinBall[integer - 1] = true;
        SaveBoughtItem(integer - 1);

        Destroy(this.gameObject.transform.GetChild(0).gameObject);
    }

    /// <summary>
    /// Saves our game
    /// </summary>
    private void SaveGameScript()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Save");
        DataToSave save = temp.GetComponent<DataToSave>();

        save.SavePinBall(controller.pinBallID);

        SaveGame.SaveGameData(save);
    }
}
