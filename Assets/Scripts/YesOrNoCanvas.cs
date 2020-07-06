using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesOrNoCanvas : MonoBehaviour {

    private GameObject senderCanvas;
    private GameObject senderButton;

    /// <summary>
    /// Here we reciave info from the "Sender" so we know where to send the result of Yes or No back
    /// </summary>
    /// <param name="canvas">The canvas the button that sent the info is based in. We use this info to determine the script where to send the info</param>
    /// <param name="button">This is the button that sent the info and where we send it back</param>
    public void RecieveSenderInfo(GameObject canvas, GameObject button)
    {
        senderCanvas = canvas;
        senderButton = button;
    }

    /// <summary>
    /// Here we get the result from our buttons if the player chose Yes or No
    /// </summary>
    /// <param name="yesOrNo"></param>
    public void ButtonValue(bool yesOrNo)
    {
        ReturnToSender(yesOrNo);
    }
	
    /// <summary>
    /// Here we send the info to the sender
    /// </summary>
    /// <param name="yesOrNo">The result we are sending back</param>
    private void ReturnToSender(bool yesOrNo)
    {
        WhereToSend(yesOrNo);
    }

    /// <summary>
    /// Here we determine where to send the info bu cheking from wich canvas the info came from
    /// </summary>
    /// <param name="yesOrNo">The result we are sending back</param>
    private void WhereToSend(bool yesOrNo)
    {
        if(senderCanvas.name == "PinBall shop screen")
        {
            SendToPinball(yesOrNo);
        }
        else if (senderCanvas.name == "Background shop screen")
        {
            SendToBackground(yesOrNo);
        }
        else if (senderCanvas.name == "Bouncy shop screen")
        {
            SendToBouncy(yesOrNo);
        }
        else if (senderCanvas.name == "Coin shop screen")
        {
            SendToCoin(yesOrNo);
        }
    }

    /// <summary>
    /// Here we send back the info to che correct recipient
    /// </summary>
    /// <param name="yesOrNo">The result we are sending back</param>
    private void SendToPinball(bool yesOrNo)
    {
        senderButton.GetComponent<PinBallButonScript>().PurchaseConfirmed(yesOrNo);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Here we send back the info to che correct recipient
    /// </summary>
    /// <param name="yesOrNo">The result we are sending back</param>
    private void SendToBackground(bool yesOrNo)
    {
        senderButton.GetComponent<BackgroundButonScript>().PurchaseConfirmed(yesOrNo);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Here we send back the info to che correct recipient
    /// </summary>
    /// <param name="yesOrNo">The result we are sending back</param>
    private void SendToBouncy(bool yesOrNo)
    {
        senderButton.GetComponent<BouncyButonScript>().PurchaseConfirmed(yesOrNo);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Here we send back the info to che correct recipient
    /// </summary>
    /// <param name="yesOrNo">The result we are sending back</param>
    private void SendToCoin(bool yesOrNo)
    {
        senderButton.GetComponent<CoinButonScript>().PurchaseConfirmed(yesOrNo);
        this.gameObject.SetActive(false);
    }
}
