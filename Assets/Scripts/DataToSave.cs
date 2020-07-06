using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave : MonoBehaviour {

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
    /// Updates our purchases
    /// </summary>
    /// <param name="background">backgrounds purchases</param>
    /// <param name="pinBall">pinBalls purchases</param>
    /// <param name="coin">coins purchases</param>
    /// <param name="bouncy">bouncys purchases</param>
    public void SavePurchases(bool[] background, bool[] pinBall, bool[] coin, bool[] bouncy)
    {
        boughtBackground = background;
        boughtPinBall = pinBall;
        boughtCoin = coin;
        boughtBouncy = bouncy;
    }

    /// <summary>
    /// Saves our last game score
    /// </summary>
    /// <param name="score">the score we are saving</param>
    public void SaveLastScore(int score)
    {
        lastScore = score;
    }

    /// <summary>
    /// Updates a new bought background ID
    /// </summary>
    /// <param name="id"></param>
    public void SaveBoughtBackground(int id)
    {
        boughtBackground[id] = true;
    }

    /// <summary>
    /// Updates a new bought pinBall ID
    /// </summary>
    /// <param name="id"></param>
    public void SaveBoughtPinBall(int id)
    {
        boughtPinBall[id] = true;
    }

    /// <summary>
    /// Updates a new bought coin ID
    /// </summary>
    /// <param name="id"></param>
    public void SaveBoughtCoin(int id)
    {
        boughtCoin[id] = true;
    }

    /// <summary>
    /// Updates a new bought bouncy ID
    /// </summary>
    /// <param name="id"></param>
    public void SaveBoughtBouncy(int id)
    {
        boughtBouncy[id] = true;
    }


    /// <summary>
    /// Saves a new highscore
    /// </summary>
    /// <param name="number">highscore</param>
    public void SaveHighscore(int number)
    {
        highScore = number;
    }

    /// <summary>
    /// Saves our current wallet
    /// </summary>
    /// <param name="number">the ammount of coins in the wallet</param>
    public void SaveWallet(int number)
    {
        wallet = number;
    }

    /// <summary>
    /// Saves our current background ID
    /// </summary>
    /// <param name="number"></param>
    public void SaveBackground(int number)
    {
        backgroundID = number;
    }

    /// <summary>
    /// Saves our current bouncy ID
    /// </summary>
    /// <param name="number"></param>
    public void SaveBouncy(int number)
    {
        bouncyID = number;
    }

    /// <summary>
    /// Saves our current pinBall ID
    /// </summary>
    /// <param name="number"></param>
    public void SavePinBall(int number)
    {
        pinBallID = number;
    }

    /// <summary>
    /// Saves our current coin ID
    /// </summary>
    /// <param name="number"></param>
    public void SaveCoin(int number)
    {
        coinID = number;
    }
}
