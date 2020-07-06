using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackToMainMenuScript : MonoBehaviour {

    public GameObject back;

    public GameObject buttonCanvas;

    public Button button;

    public GameObject mainMenuCanvas;

    // Use this for initialization
    void Start()
    {
        AddButtonToGameobject();
        SetUpButton();
        FindGameObjects();
    }

    /// <summary>
    /// Finds game objects needed for the scrippt
    /// </summary>
    void FindGameObjects()
    {
        buttonCanvas = gameObject.transform.parent.gameObject;
        mainMenuCanvas = buttonCanvas.transform.parent.gameObject;
    }

    /// <summary>
    /// Here we add all the components we need for the button
    /// </summary>
    void AddButtonToGameobject()
    {
        back = gameObject;

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
        mainMenuCanvas.transform.Find("Main screen").gameObject.SetActive(true);

        buttonCanvas.SetActive(false);
}

}
