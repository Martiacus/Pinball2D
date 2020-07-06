using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWalletScript : MonoBehaviour {

    public CentralController controller;

    private void Start()
    {
        controller = GameObject.FindObjectOfType<CentralController>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Coins: " + controller.currentCoins.ToString();
    }
}