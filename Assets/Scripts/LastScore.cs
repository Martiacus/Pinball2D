using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastScore : MonoBehaviour
{

    public CentralController controller;

    private void Start()
    {
        controller = GameObject.FindObjectOfType<CentralController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shows our highscore
        this.gameObject.GetComponent<Text>().text = "Last score: " + controller.score.ToString();
    }
}
