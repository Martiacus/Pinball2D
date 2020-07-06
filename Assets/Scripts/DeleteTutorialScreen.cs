using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteTutorialScreen : MonoBehaviour {

    public GameObject leftButton;

    public GameObject rightButton;

	// Use this for initialization
	void Start ()
    {
        AddButtonsToGameobject();
        StartCoroutine(ExecuteAfterTime(2f));
    }

    /// <summary>
    /// Here we find our gameobjects
    /// </summary>
    private void AddButtonsToGameobject()
    {
            leftButton = GameObject.Find("Left Button");
            rightButton = GameObject.Find("Right Button");
    }

    /// <summary>
    /// Destroys this screen spit and resets the color
    /// </summary>
    private void DestroyTutorialScreen()
    {
        leftButton.GetComponent<Text>().text = "";
        rightButton.GetComponent<Text>().text = "";

        Destroy(this.gameObject);
    }

    /// <summary>
    /// Destroys the gameobjects after "time"
    /// </summary>
    /// <param name="time">time to wait</param>
    /// <returns></returns>
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        DestroyTutorialScreen();
    }
}
