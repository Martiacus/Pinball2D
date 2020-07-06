using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour {

    public CentralController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<AudioSource>().Play();

            //Finds central controller script
            controller = GameObject.FindObjectOfType<CentralController>();

            for (int i = 1; i <= 10; i++)
            {
                //Here we are cheking for matching tags to increase the score
                if (this.gameObject.tag == i + "coin")
                {
                    //Here we are calling a score increase void in centralcontroller
                    controller.IncreaseScore(i);
                    controller.AddToCurrentCoins(i);
                }
            }
            // We destroy oir coin gameobject
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        SelfDestruct();
    }
    
    /// <summary>
    /// Seft destruct script
    /// </summary>
    void SelfDestruct()
    {
        // if our pinBall is destroyed destroy remaining coins ( Less lag )
        if (!GameObject.Find("Pin Ball"))
        {
            // Destroys our coin gameobject
            Destroy(this.gameObject);
        }
    }
}
