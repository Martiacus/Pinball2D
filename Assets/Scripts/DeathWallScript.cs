using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallScript : MonoBehaviour {

    public CentralController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<AudioSource>().Play();

            //Finds central controller script
            controller = GameObject.FindObjectOfType<CentralController>();

            //Since our player died we are checking if the high score was increased
            controller.CheckForHighscore();

            //We are destroying our player
            Destroy(collision.gameObject);

            controller.CancelInvoke("CreateCoin");

            Destroy(GameObject.FindGameObjectWithTag("BouncyButtons"));

            if (controller.adsEnabled && CanAdBeShown())
            {
                ADSManager.instance.ShowVideoAD();
            }

            SaveGameScript();

            controller.CreateCanvas();
        }
    }

    private bool CanAdBeShown()
    {
        if(controller.score % 10 == 1 || controller.score % 10 ==4 || controller.score % 10 == 7)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    private void SaveGameScript()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Save");
        DataToSave save = temp.GetComponent<DataToSave>();

        save.SaveHighscore(controller.highscore);
        save.SaveWallet(controller.currentCoins);
        save.SaveLastScore(controller.score);

        SaveGame.SaveGameData(save);
    }
}
