using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsScript : MonoBehaviour, IPointerDownHandler {

    public GameObject bouncy;

    public Button button;

	// Use this for initialization
	void Start ()
    {
        AddButtonToGameobject();
        //SetUpButton();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
    }

    /// <summary>
    /// Here we add all the components we need for the button
    /// </summary>
    private void AddButtonToGameobject()
    {
        if (gameObject.name == "Left Button")
        {
            bouncy = GameObject.Find("Bouncy Left(Clone)");
        }

        if (gameObject.name == "Right Button")
        {
            bouncy = GameObject.Find("Bouncy Right(Clone)");
        }

        button = gameObject.GetComponent<Button>();
    }

    /// <summary>
    /// Here we add the necesary parameters for the button to function
    /// </summary>
    void SetUpButton()
    {
        button.onClick.AddListener(OnClick);            // Here we add a listener that says if the button was clicked and executes the code if it was
    }


    /// <summary>
    /// Our code that executes the command on button click
    /// </summary>
    void OnClick()
    {
        bouncy.GetComponent<Animation>().Play();        // Plays bouncy animation
    }

}
