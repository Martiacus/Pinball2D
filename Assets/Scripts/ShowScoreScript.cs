using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreScript : MonoBehaviour {

    public CentralController controller;

    private void Start()
    {
        controller = GameObject.FindObjectOfType<CentralController>();
    }

    // Update is called once per frame
    void Update () {
        //Shows score
        gameObject.GetComponent<Text>().text = "Score: " + controller.score.ToString();
	}
}
