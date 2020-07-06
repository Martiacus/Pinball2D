using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsCanvas : MonoBehaviour {

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
        ReturnToShopScreen(yesOrNo);
    }

    /// <summary>
    /// Here we send the info to the sender
    /// </summary>
    /// <param name="yesOrNo">The result we are sending back</param>
    private void ReturnToShopScreen(bool yesOrNo)
    {

    }
}
