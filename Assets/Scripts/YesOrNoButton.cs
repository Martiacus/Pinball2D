using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesOrNoButton : MonoBehaviour {

    private Button button;

    private GameObject canvas;

    bool yesOrNo;

	// Use this for initialization
	void Start ()
    {
        SetGameobjects();
        SetUpButton();
    }

    /// <summary>
    /// Here we assign gameobjects to their gameobjects
    /// </summary>
    private void SetGameobjects()
    {
        button = this.gameObject.GetComponent<Button>();
        canvas = this.gameObject.transform.parent.gameObject;

        DetermineButtonPurpose();
    }

    /// <summary>
    /// Here we determine what the button is supposed to return "Yes" or "No"
    /// </summary>
    private void DetermineButtonPurpose()
    {
        if (this.gameObject.name == "Yes")
        {
            yesOrNo = true;
        }

        if (this.gameObject.name == "No")
        {
            yesOrNo = false;
        }
    }

    /// <summary>
    /// Here we add the necesary parameters for the button to function
    /// </summary>
    private void SetUpButton()
    {
        button.onClick.AddListener(OnClick);            // Here we add a listener that says if the button was clicked and executes the code if it was
    }

    /// <summary>
    /// On click we send info to canvas script to return to sender
    /// </summary>
    private void OnClick()
    {
        canvas.GetComponent<YesOrNoCanvas>().ButtonValue(yesOrNo);
    }
}
