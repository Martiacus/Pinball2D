using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData {

    public int highScore;                   // Highscore
    public int wallet;                      // Our wallet
    public int backgroundID;                // Our background ID
    public int bouncyID;                    // Our bouncy ID
    public int pinBallID;                   // Our pinBall ID
    public int coinID;                      // Our coin ID
    public int lastScore;                   // Our last game score
    public bool[] boughtBackground;         // The backgrounds we have bought
    public bool[] boughtPinBall;            // The pinballs we have bought
    public bool[] boughtCoin;               // The coins we have bought
    public bool[] boughtBouncy;             // The bouncys we have bought

    /// <summary>
    /// Adds parameters to save
    /// </summary>
    /// <param name="data">Where we take the data from</param>
    public SaveData (DataToSave data)
    {
        highScore = data.highScore;
        wallet = data.wallet;
        backgroundID = data.backgroundID;
        bouncyID = data.bouncyID;
        pinBallID = data.pinBallID;
        coinID = data.coinID;
        lastScore = data.lastScore;
        boughtBackground = data.boughtBackground;
        boughtPinBall = data.boughtPinBall;
        boughtCoin = data.boughtCoin;
        boughtBouncy = data.boughtBouncy;
    }
}
