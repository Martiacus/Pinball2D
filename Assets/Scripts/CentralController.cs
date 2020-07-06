using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CentralController : MonoBehaviour
{

    public readonly float originalHeight = 1024;                    // Original screen height on witch the game was created
    public readonly float originalWidth = 600;                      // Original screen width on witch the game was created

    private readonly float scaleX = 1;                              // Used when scaling objects to match the phone screen size
    private readonly float scaleY = 1;                              // Used when scaling objects to match the phone screen size

    public float wallWidth = 10f;                                   // Sets the width of the wall out of bound the faster the ball moves the more chance it has to break through
    public float wallRadius = 0.1f;                                 // Sets the radois of the colision object it is best to match it to wall width so the ball doesn appear to be touching empty wall

    public float pinBallPxSizeX = 50f;                              // Sets the ball texture size in pixels so we can modify the bals size
    public float pinBallPxSizeY = 50f;                              // Sets the ball texture size in pixels so we can modify the bals size
    public float pinBallStartPosX = -2f;                            // Sets the starting position of pinBall X axis
    public float pinBallStartPosY = 0f;                             // Sets the starting position of pinBall Y axis
    public float pinBallMaxSpeed = 200f;                            // Sets the max speed of pinBall (we dont want to break the speed of sound)

    public float bouncyPxSizeX = 450f;                              // Sets the knock up texture size in pixels so we can modify the knock up size
    public float bouncyPxSizeY = 40f;                               // Sets the knock up texture size in pixels so we can modify the knock up size

    public float bouncyWidth = 3f;                                  // Sets the left and right location for the bouncy
    public float bouncyHeight = 3f;                                 // Sets the height the bouncy is at
    public float bouncyRotation = 15f;                              // The rotation of the bouncy(at witch angle is it displayed on the screen

    public float coinPxSizeX = 35f;                                 // Sets the coin texture size in pixels so we can modify the knock up size
    public float coinPxSizeY = 35f;                                 // Sets the coin texture size in pixels so we can modify the knock up size
    public float coinStartPosX = -2f;                               // Sets the starting position of coin X axis
    public float coinStartPosY = 3f;                                // Sets the starting position of coin Y axis
    public float coinFinishPosX = 2f;                               // Sets the finishing position of coin X axis
    public float coinFinishPosY = -3f;                              // Sets the finishing position of coin Y axis

    public AudioClip gameOverSound;                                 // Game over sound
    public AudioClip coinSound;                                     // Sound of collecting coins

    private GameObject background;                                  // Gameobject created to be the background
    private GameObject pinBall;                                     // PinBall gameobject
    private GameObject platform;                                    // Botom platform gameobject
    private GameObject deathWall;                                   // Gameobject that destroys pinBall on collision
    private GameObject wallLeft;                                    // Left wall gameobject
    private GameObject wallRight;                                   // Right wall gameobject
    private GameObject wallTop;                                     // Top wall gameoject
    public GameObject bounceRight;                                  // The right bouncy platform
    public GameObject bounceLeft;                                   // The left bouncy platform
    private GameObject coin;                                        // Coin gameobject;

    private GameObject leftButton;                                  // This is our left button
    private GameObject rightButton;                                 // This is our right button
    private GameObject canvas;                                      // Tis is our game canvas
    private GameObject currentScore;                                // This gamepbject will display our current score
    private GameObject wallet;                                      // This gameobject will keep our money
    private GameObject highScore;                                   // This gameobject will keep our highscore

    private GameObject menuCanvas;                                  // This is our mmenu canvas
    private GameObject mainScreenCanvas;                            // This is our main menu screen
    private GameObject shopScreenCanvas;                            // This is our main shop screen
    private GameObject pinBallCanvas;                               // This is our pinBall sprites canvas
    private GameObject bouncyCanvas;                                // This is our bouncy sprites canvas
    private GameObject backgroundCanvas;                            // This is our backround sprites canvas
    private GameObject coinCanvas;                                  // This is our coins sprites canvas

    private readonly GameObject shopButton;                         // This is our shop button in mainScreenCanvas

    private Vector2 square;                                         // This is our square button
    private Vector2 rectangle;                                      // This is our rectangle button

    public List<Sprite> backgroundSprites;                          // Sprite list to store background pictures
    [SerializeField]
    public int backgroundID;                                        // Background sprite ID from the list                                          

    public List<Sprite> pinBallSprites;                             // Sprite list to store background pictures
    [SerializeField]
    public int pinBallID;                                           // PinBall sprite ID from the list

    public List<Sprite> bouncySprites;                              // Sprite list to store background sprites
    [SerializeField]
    public int bouncyID;                                            // Bouncy sprite ID from the list

    public Sprite standart;                                         // Standart texture 100x100 for measuing to be used for gameobjects that do not appear on screen

    public PhysicsMaterial2D pinBallMaterial;                       // PinBall 2d material so we can give it bounciness

    [SerializeField]
    public int coinID;                                              // ID for coin sprites (coin sprites 1-9)
    public List<CoinSprites> coinSprites;                           // Our coin sprites

    public Sprite shopButtonSprite;                                 // Our Shop button sprite
    public Sprite playButtonSprite;                                 // Our play button sprite
    public Sprite backButtonSprite;                                 // This is our back button sprite

    public int score = 0;                                           // Our score integer

    [SerializeField]
    public int highscore;                                           // Our highscore integer

    public float coinSpawnInterval = 2.5f;                          // Our coin respawn interval

    public int scoreTextSize = 50;                                  // Our score text size

    [SerializeField]
    public int currentCoins;                                        // Our current money   

    /// <summary>
    /// Shop button temp gameobject
    /// </summary>
    public GameObject ShopButton;

    public bool[] boughtBackground;                                 // Our bought backgrounds
    public bool[] boughtPinBall;                                    // Our bought pinballs
    public bool[] boughtCoin;                                       // Our bought coins
    public bool[] boughtBouncy;                                     // Our bought coins

    public int priceMultiplier;                                     // Our price multiplier to buy skins

    public Sprite selectionHighlightSprite;                         // This sprite will be used to highlight our selected item

    public int selectionHighlightPxSizeX;                           // X Size of our hghlight
    public int selectionHighlightPxSizeY;                           // Y Size of our hghlight

    public Sprite yesSprite;                                        // Our yes button sprite
    public Sprite noSprite;                                         // Our no Button sprite

    public bool adsEnabled;                                         // Our ads
    public int adsReward;                                           // The reward value of viewing an ad

    // Use this for initialization
    void Start()
    {
        StartingFunctions();
    }

    /// <summary>
    /// These are the functions that need to start when the game launches
    /// </summary>
    private void StartingFunctions()
    {
        LoadGame();                                                 // Loads our last gmae savedata
        ScreenScale();                                              // Start this first so we can get the scales needed to scale all the gameobjects to screensize
        CreateBackground();                                         // Creates the background image
        CreateWalls();                                              // Creates the top left and right walls
        CreateBouncy();                                             // Creates the left and right bouncys
        SetButtonSizes();                                           // Sets the button sizes
        CreateCanvas();                                             // Initializes our UI
    }

    /// <summary>
    /// Loads our last game data
    /// </summary>
    private void LoadGame()
    {
        SaveData data = SaveGame.LoadGameData();
        if (data != null)
        {
            backgroundID = data.backgroundID;
            bouncyID = data.bouncyID;
            coinID = data.coinID;
            pinBallID = data.pinBallID;
            currentCoins = data.wallet;
            highscore = data.highScore;
            score = data.lastScore;
            boughtBackground = data.boughtBackground;
            boughtPinBall = data.boughtPinBall;
            boughtCoin = data.boughtCoin;
            boughtBouncy = data.boughtBouncy;

            SaveDataForNextSave(data);
        }
        else
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Save");
            DataToSave save = temp.GetComponent<DataToSave>();

            backgroundID = 0;
            bouncyID = 0;
            coinID = 0;
            pinBallID = 0;
            currentCoins = 0;
            highscore = 0;
            score = 0;

            save.boughtBackground = boughtBackground;
            save.boughtBouncy = boughtBouncy;
            save.boughtCoin = boughtCoin;
            save.boughtPinBall = boughtPinBall;

            SaveDataForFirstSave(save);
        }
    }

    /// <summary>
    /// Saves data for the first time
    /// </summary>
    /// <param name="save"></param>
    private void SaveDataForFirstSave(DataToSave save)
    {
        save.SaveBackground(save.backgroundID);
        save.SaveBouncy(save.bouncyID);
        save.SaveCoin(save.coinID);
        save.SaveHighscore(save.highScore);
        save.SavePinBall(save.pinBallID);
        save.SaveWallet(save.wallet);
        save.SavePurchases(save.boughtBackground, save.boughtPinBall, save.boughtCoin, save.boughtBouncy);
    }

    /// <summary>
    /// We save data for next save
    /// </summary>
    /// <param name="data">the data we are saving</param>
    private void SaveDataForNextSave(SaveData data)
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Save");
        DataToSave save = temp.GetComponent<DataToSave>();

        save.SaveBackground(data.backgroundID);
        save.SaveBouncy(data.bouncyID);
        save.SaveCoin(data.coinID);
        save.SaveHighscore(data.highScore);
        save.SavePinBall(data.pinBallID);
        save.SaveWallet(data.wallet);
        save.SaveLastScore(score);
        save.SavePurchases(data.boughtBackground, data.boughtPinBall, data.boughtCoin, data.boughtBouncy);
    }

    /// <summary>
    /// Updates to a new sprite for pinBall
    /// </summary>
    /// <param name="id"></param>
    public void UpdatePinBallSprite(int id)
    {
        pinBallID = id;
    }

    /// <summary>
    /// Updates to a new sprite for bouncy
    /// </summary>
    /// <param name="id"></param>
    public void UpdateBouncySprite(int id)
    {
        bouncyID = id;
        GameObject bouncy1 = GameObject.Find("Bouncy Left(Clone)");
        GameObject bouncy2 = GameObject.Find("Bouncy Right(Clone)");

        bouncy1.GetComponent<SpriteRenderer>().sprite = bouncySprites[id];
        bouncy2.GetComponent<SpriteRenderer>().sprite = bouncySprites[id];
    }

    /// <summary>
    /// Updates to a new sprite for background
    /// </summary>
    /// <param name="id"></param>
    public void UpdateBackgroundSprite(int id)
    {
        backgroundID = id;

        GameObject background = GameObject.Find("Background");

        background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[id];
    }

    /// <summary>
    /// Updates to a new sprite for coin
    /// </summary>
    /// <param name="id"></param>
    public void UpdateCoinSprite(int id)
    {
        coinID = id;
    }

    /// <summary>
    /// Sets the button sizes
    /// </summary>
    private void SetButtonSizes()
    {
        Vector2 temp = new Vector2(originalWidth / 5, originalHeight / 8);
        square = temp;
        temp = new Vector2(originalWidth / 2, originalHeight / 7);
        rectangle = temp;
    }

    /// <summary>
    /// These are the functions for the game to start
    /// </summary>
    public void GameStart()
    {
        SetScoreToZero();                                           // Sets our current score to 0 for a new game
        CreatepinBall(new Vector3(pinBallStartPosX, pinBallStartPosY, 0));  // Creates the pinBall gameobject at vector 3 coordinates
        CoinSpawner(coinSpawnInterval);                             // This is our coinspawner script
        CreateGameCanvas();                                             // This creates our buttons
    }

    /// <summary>
    /// Here we create the main menu canvas
    /// </summary>
    private void CreateMenuCanvas()
    {
        menuCanvas = new GameObject("Menu canvas")
        {
            tag = "MenuScreen"
        };

        menuCanvas.AddComponent<Canvas>();
        menuCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        menuCanvas.AddComponent<CanvasScaler>();
        menuCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        menuCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(originalWidth, originalHeight);

        menuCanvas.AddComponent<GraphicRaycaster>();
        menuCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        menuCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        menuCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        menuCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);

        CreateWalletText(menuCanvas, new Vector3(originalWidth / -5, originalHeight / 2.5f, 0));
        CreateHighscoreText(menuCanvas, new Vector3(originalWidth / -5, originalHeight / 3f, 0));
        CreateLastScoreText(menuCanvas, new Vector3(originalWidth / -5, originalHeight / 3.75f, 0));

        menuCanvas.SetActive(true);
    }

    /// <summary>
    /// Create Yes or No canvas where a player can confirm his purchase
    /// </summary>
    private void CreateYesOrNoCanvas()
    {
        GameObject yesOrNoCanvas = new GameObject("Yes or No canvas")
        {
            tag = "YesOrNoCanvas"
        };

        yesOrNoCanvas.transform.SetParent(menuCanvas.transform);

        yesOrNoCanvas.AddComponent<Canvas>();
        yesOrNoCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        yesOrNoCanvas.AddComponent<GraphicRaycaster>();
        yesOrNoCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        yesOrNoCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        yesOrNoCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        yesOrNoCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);

        yesOrNoCanvas.AddComponent<YesOrNoCanvas>();

        CreateBlackScreen(yesOrNoCanvas);
        CreateYesOrNoButtons(yesOrNoCanvas);
        CreateYesorNoText(yesOrNoCanvas, new Vector3(0, originalHeight / 7, 0), "Are you sure you want to buy this item?");

        yesOrNoCanvas.SetActive(false);
    }

    /// <summary>
    /// Create a black transparent image to display an option to buy and a big transparent imgae so the player cannot click the background
    /// </summary>
    /// <param name="canvas">the canvas we are creating the images for</param>
    private void CreateBlackScreen(GameObject canvas)
    {
        GameObject blackScreen = new GameObject("Black Screen");

        blackScreen.transform.SetParent(canvas.transform);

        blackScreen.AddComponent<Image>();
        blackScreen.GetComponent<Image>().sprite = standart;
        blackScreen.GetComponent<Image>().color = new Color32(0, 0, 0, 175);
        blackScreen.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(originalWidth, originalHeight / 2);

        GameObject transparentScreen = new GameObject("Transparent Screen");

        transparentScreen.transform.SetParent(canvas.transform);

        transparentScreen.AddComponent<Image>();
        transparentScreen.GetComponent<Image>().sprite = standart;
        transparentScreen.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        transparentScreen.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(originalWidth, originalHeight);
    }

    /// <summary>
    /// Create a Tutorial screen split
    /// </summary>
    /// <param name="canvas">the canvas we are creating the images for</param>
    private void CreateTutorialScreenSpit(GameObject canvas)
    {
        GameObject tutorialScreen = new GameObject("Tutorial Screen Split");

        tutorialScreen.transform.SetParent(canvas.transform);

        tutorialScreen.AddComponent<Image>();
        tutorialScreen.GetComponent<Image>().sprite = standart;
        tutorialScreen.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        tutorialScreen.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(originalWidth / 20, originalHeight);

        tutorialScreen.AddComponent<DeleteTutorialScreen>();
    }

    /// <summary>
    /// Creates a tex inside the canvas to ask if a player wants to make a purchase
    /// </summary>
    /// <param name="canvas">the canvas we are creating the images for</param>
    /// <param name="position">The position of text on our screen</param>
    private void CreateYesorNoText(GameObject canvas, Vector3 position, string text)
    {
        GameObject yesOrNoText = new GameObject("Are you sure?");

        yesOrNoText.transform.parent = canvas.transform;

        yesOrNoText.AddComponent<Text>();

        yesOrNoText.GetComponent<Text>().fontSize = scoreTextSize;
        yesOrNoText.GetComponent<Text>().raycastTarget = true;
        yesOrNoText.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Wrap;
        yesOrNoText.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        yesOrNoText.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        yesOrNoText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        yesOrNoText.GetComponent<Text>().text = text;

        yesOrNoText.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth / 2, originalHeight / 10);
        yesOrNoText.GetComponent<RectTransform>().position = position;
    }

    /// <summary>
    /// Creates Yes and No buttons for our canvas
    /// </summary>
    /// <param name="temp">the canvas we are creating the buttons for</param>
    private void CreateYesOrNoButtons(GameObject temp)
    {
        CreateShopScreenButton("", ShopButton, "Yes", new Vector3(originalWidth / -5, originalHeight / -7, 0), yesSprite, temp.transform, new Vector2(originalWidth / 3, originalHeight / 7), new bool[0]);
        temp.transform.Find("Yes").transform.gameObject.AddComponent<YesOrNoButton>();

        CreateShopScreenButton("", ShopButton, "No", new Vector3(originalWidth / 5, originalHeight / -7, 0), noSprite, temp.transform, new Vector2(originalWidth / 3, originalHeight / 7), new bool[0]);
        temp.transform.Find("No").transform.gameObject.AddComponent<YesOrNoButton>();
    }

    /// <summary>
    /// Creates buttons to watch ad
    /// </summary>
    /// <param name="temp"></param>
    private void CreateAdsButtons(GameObject temp)
    {
        CreateShopScreenButton("Price", ShopButton, "Yes", new Vector3(originalWidth / -5, originalHeight / -7, 0), yesSprite, temp.transform, new Vector2(originalWidth / 3, originalHeight / 7), new bool[0]);
        temp.transform.Find("Yes").transform.gameObject.AddComponent<AdsButton>();

        CreateShopScreenButton("Price", ShopButton, "No", new Vector3(originalWidth / 5, originalHeight / -7, 0), noSprite, temp.transform, new Vector2(originalWidth / 3, originalHeight / 7), new bool[0]);
        temp.transform.Find("No").transform.gameObject.AddComponent<AdsButton>();
    }

    /// <summary>
    /// Here we create main screen canvas
    /// </summary>
    private void CreateMainScreenCanvas()
    {
        mainScreenCanvas = new GameObject("Main screen")
        {
            tag = "MainScreen"
        };

        mainScreenCanvas.transform.SetParent(menuCanvas.transform);

        mainScreenCanvas.AddComponent<Canvas>();
        mainScreenCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        mainScreenCanvas.AddComponent<CanvasScaler>();
        mainScreenCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        mainScreenCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(originalWidth, originalHeight);

        mainScreenCanvas.AddComponent<GraphicRaycaster>();
        mainScreenCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        mainScreenCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        mainScreenCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        mainScreenCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);

        CreateMainScreenButtons(mainScreenCanvas);

        mainScreenCanvas.SetActive(true);
    }

    /// <summary>
    /// Here we create shop screen canvas
    /// </summary>
    private void CreateShopScreenCanvas()
    {
        shopScreenCanvas = new GameObject("Shop screen")
        {
            tag = "ShopScreen"
        };

        shopScreenCanvas.AddComponent<Canvas>();
        shopScreenCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        shopScreenCanvas.AddComponent<GraphicRaycaster>();
        shopScreenCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        shopScreenCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        shopScreenCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        shopScreenCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);
        shopScreenCanvas.transform.SetParent(menuCanvas.transform);

        CreateMainShopScreenButtons(shopScreenCanvas);

        shopScreenCanvas.SetActive(false);
    }

    /// <summary>
    /// Here we create shop screen canvas
    /// </summary>
    private void CreateCoinShopScreenCanvas()
    {
        coinCanvas = new GameObject("Coin shop screen");

        coinCanvas.transform.SetParent(menuCanvas.transform.Find("Shop screen"), gameObject.transform);

        coinCanvas.AddComponent<Canvas>();
        coinCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        coinCanvas.AddComponent<GraphicRaycaster>();
        coinCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        coinCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        coinCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        coinCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);
        coinCanvas.transform.SetParent(menuCanvas.transform);

        CreateCoinShopScreenButtons(coinCanvas);

        coinCanvas.SetActive(false);
    }

    /// <summary>
    /// Here we create main shop screen buttons
    /// </summary>
    private void CreateCoinShopScreenButtons(GameObject temp)
    {
        CreateShopScreenButton("Price", ShopButton, "Back to shop screen", new Vector3((originalWidth / -2) + (originalWidth / 5 / 2), (originalHeight / 2) - (originalHeight / 8 / 2), 0), backButtonSprite, temp.transform, square, new bool[0]);
        temp.transform.Find("Back to shop screen").transform.gameObject.AddComponent<BackToMainShopScreenScript>();

        CreateShopScreenButton("Price", ShopButton, "1 Coin Button", new Vector2(originalWidth / -3, originalHeight / 7), coinSprites[0].sprites[0], temp.transform, square, boughtCoin);
        temp.transform.Find("1 Coin Button").transform.gameObject.AddComponent<CoinButonScript>();
        if (coinID == 0) CreateSelectedItemHighlight(temp, temp.transform.Find("1 Coin Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "2 Coin Button", new Vector2(0, originalHeight / 7), coinSprites[1].sprites[0], temp.transform, square, boughtCoin);
        temp.transform.Find("2 Coin Button").transform.gameObject.AddComponent<CoinButonScript>();
        if (coinID == 1) CreateSelectedItemHighlight(temp, temp.transform.Find("2 Coin Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "3 Coin Button", new Vector2(originalWidth / 3, originalHeight / 7), coinSprites[2].sprites[0], temp.transform, square, boughtCoin);
        temp.transform.Find("3 Coin Button").transform.gameObject.AddComponent<CoinButonScript>();
        if (coinID == 2) CreateSelectedItemHighlight(temp, temp.transform.Find("3 Coin Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "4 Coin Button", new Vector2(originalWidth / -3, originalHeight / -7), coinSprites[3].sprites[0], temp.transform, square, boughtCoin);
        temp.transform.Find("4 Coin Button").transform.gameObject.AddComponent<CoinButonScript>();
        if (coinID == 3) CreateSelectedItemHighlight(temp, temp.transform.Find("4 Coin Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "5 Coin Button", new Vector2(0, originalHeight / -7), coinSprites[4].sprites[0], temp.transform, square, boughtCoin);
        temp.transform.Find("5 Coin Button").transform.gameObject.AddComponent<CoinButonScript>();
        if (coinID == 4) CreateSelectedItemHighlight(temp, temp.transform.Find("5 Coin Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "6 Coin Button", new Vector2(originalWidth / 3, originalHeight / -7), coinSprites[5].sprites[0], temp.transform, square, boughtCoin);
        temp.transform.Find("6 Coin Button").transform.gameObject.AddComponent<CoinButonScript>();
        if (coinID == 5) CreateSelectedItemHighlight(temp, temp.transform.Find("6 Coin Button").transform.gameObject);
    }

    /// <summary>
    /// Creates a highlight for a selected item
    /// </summary>
    /// <param name="parent"></param>
    private void CreateSelectedItemHighlight(GameObject parent, GameObject highlighted)
    {
        GameObject highlight = new GameObject("Highlight")
        {
            tag = "Highlight"
        };

        highlight.transform.parent = parent.transform;
        highlight.AddComponent<Image>();
        highlight.transform.position = highlighted.transform.position;
        highlight.transform.SetAsFirstSibling();

        highlight.GetComponent<Image>().sprite = selectionHighlightSprite;
        highlight.GetComponent<RectTransform>().sizeDelta = new Vector2(selectionHighlightPxSizeX, selectionHighlightPxSizeY);
    }

    /// <summary>
    /// Here we create shop screen canvas
    /// </summary>
    private void CreateBackgroundShopScreenCanvas()
    {
        backgroundCanvas = new GameObject("Background shop screen");

        backgroundCanvas.transform.SetParent(menuCanvas.transform.Find("Shop screen"), gameObject.transform);

        backgroundCanvas.AddComponent<Canvas>();
        backgroundCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        backgroundCanvas.AddComponent<GraphicRaycaster>();
        backgroundCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        backgroundCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        backgroundCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        backgroundCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);
        backgroundCanvas.transform.SetParent(menuCanvas.transform);

        CreateBackgroundShopScreenButtons(backgroundCanvas);

        backgroundCanvas.SetActive(false);
    }

    /// <summary>
    /// Creates name tags for shop items
    /// </summary>
    /// <param name="nameFor">What gameobject you want to add a name tag to</param>
    /// <param name="name">The name of that gameobject</param>
    private void CreateNameTags(GameObject nameFor, string name)
    {
        GameObject nameGameobject = new GameObject(name);

        nameGameobject.transform.parent = nameFor.transform;

        nameGameobject.AddComponent<Text>();

        nameGameobject.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        nameGameobject.GetComponent<Text>().fontSize = scoreTextSize;
        nameGameobject.GetComponent<Text>().raycastTarget = true;
        nameGameobject.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        nameGameobject.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        nameGameobject.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        nameGameobject.GetComponent<RectTransform>().position = nameFor.transform.position - new Vector3(0, nameFor.GetComponent<RectTransform>().rect.height * 0.75f, 0);

        nameGameobject.GetComponent<Text>().text = name;
    }

    /// <summary>
    /// Creates price tags for our store items
    /// </summary>
    /// <param name="integer">Required to know the price</param>
    /// <param name="priceFor">Required to set the parent for a gameobject</param>
    /// <param name="bought">Checks if the item was bought, if it was we do not give it a price tag</param>
    private void CreatePriceTag(int integer, GameObject priceFor, bool[] bought)
    {
        integer -= 1;

        if (integer == 1 || integer == 2 || integer == 3 || integer == 4 || integer == 5)
        {
            if (!bought[integer])
            {
                int price = integer * priceMultiplier;

                GameObject priceGameobject = new GameObject(integer + " price");

                priceGameobject.transform.parent = priceFor.transform;

                priceGameobject.AddComponent<Text>();

                priceGameobject.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

                priceGameobject.GetComponent<Text>().fontSize = scoreTextSize;
                priceGameobject.GetComponent<Text>().raycastTarget = true;
                priceGameobject.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
                priceGameobject.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
                priceGameobject.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

                priceGameobject.GetComponent<RectTransform>().position = priceFor.transform.position - new Vector3(0, priceFor.GetComponent<RectTransform>().rect.height * 0.75f, 0);

                priceGameobject.GetComponent<Text>().text = price.ToString();
            }
        }
    }

    /// <summary>
    /// Here we create main shop screen buttons
    /// </summary>
    private void CreateBackgroundShopScreenButtons(GameObject temp)
    {
        CreateShopScreenButton("Price", ShopButton, "Back to shop screen", new Vector3((originalWidth / -2) + (originalWidth / 5 / 2), (originalHeight / 2) - (originalHeight / 8 / 2), 0), backButtonSprite, temp.transform, square, new bool[0]);
        temp.transform.Find("Back to shop screen").transform.gameObject.AddComponent<BackToMainShopScreenScript>();

        CreateShopScreenButton("Price", ShopButton, "1 Background Button", new Vector2(originalWidth / -3, originalHeight / 7), backgroundSprites[0], temp.transform, square, boughtBackground);
        temp.transform.Find("1 Background Button").transform.gameObject.AddComponent<BackgroundButonScript>();
        if (backgroundID == 0) CreateSelectedItemHighlight(temp, temp.transform.Find("1 Background Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "2 Background Button", new Vector2(0, originalHeight / 7), backgroundSprites[1], temp.transform, square, boughtBackground);
        temp.transform.Find("2 Background Button").transform.gameObject.AddComponent<BackgroundButonScript>();
        if (backgroundID == 1) CreateSelectedItemHighlight(temp, temp.transform.Find("2 Background Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "3 Background Button", new Vector2(originalWidth / 3, originalHeight / 7), backgroundSprites[2], temp.transform, square, boughtBackground);
        temp.transform.Find("3 Background Button").transform.gameObject.AddComponent<BackgroundButonScript>();
        if (backgroundID == 2) CreateSelectedItemHighlight(temp, temp.transform.Find("3 Background Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "4 Background Button", new Vector2(originalWidth / -3, originalHeight / -7), backgroundSprites[3], temp.transform, square, boughtBackground);
        temp.transform.Find("4 Background Button").transform.gameObject.AddComponent<BackgroundButonScript>();
        if (backgroundID == 3) CreateSelectedItemHighlight(temp, temp.transform.Find("4 Background Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "5 Background Button", new Vector2(0, originalHeight / -7), backgroundSprites[4], temp.transform, square, boughtBackground);
        temp.transform.Find("5 Background Button").transform.gameObject.AddComponent<BackgroundButonScript>();
        if (backgroundID == 4) CreateSelectedItemHighlight(temp, temp.transform.Find("5 Background Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "6 Background Button", new Vector2(originalWidth / 3, originalHeight / -7), backgroundSprites[5], temp.transform, square, boughtBackground);
        temp.transform.Find("6 Background Button").transform.gameObject.AddComponent<BackgroundButonScript>();
        if (backgroundID == 5) CreateSelectedItemHighlight(temp, temp.transform.Find("6 Background Button").transform.gameObject);
    }

    /// <summary>
    /// Here we create bouncy shop screen button
    /// </summary>
    /// <param name="tempbutton">this is our button gameobject</param>
    /// <param name="name">Name of the button</param>
    /// <param name="position">position of button</param>
    private void CreateShopScreenButton(string createfor, GameObject tempbutton, string name, Vector2 position, Sprite sprite, Transform parent, Vector2 size, bool[] bought)
    {
        tempbutton = new GameObject(name);
        tempbutton.transform.SetParent(parent);
        tempbutton.AddComponent<RectTransform>();
        tempbutton.AddComponent<Image>();
        tempbutton.AddComponent<Button>();

        tempbutton.GetComponent<Image>().raycastTarget = true;
        tempbutton.GetComponent<Image>().sprite = sprite;

        tempbutton.GetComponent<RectTransform>().position = position;
        tempbutton.GetComponent<RectTransform>().sizeDelta = size;

        int temp = 0;
        int.TryParse(tempbutton.name.ToCharArray()[0].ToString(), out temp);

        if(createfor == "Price")
        {
            CreatePriceTag(temp, tempbutton, bought);
        }
        else if(createfor == "Name")
        {
            CreateNameTags(tempbutton, tempbutton.gameObject.name);
        }
    }

    /// <summary>
    /// Here we create shop screen canvas
    /// </summary>
    private void CreatePinBallShopScreenCanvas()
    {
        pinBallCanvas = new GameObject("PinBall shop screen");

        pinBallCanvas.transform.SetParent(menuCanvas.transform.Find("Shop screen"), gameObject.transform);

        pinBallCanvas.AddComponent<Canvas>();
        pinBallCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        pinBallCanvas.AddComponent<GraphicRaycaster>();
        pinBallCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        pinBallCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        pinBallCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        pinBallCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);
        pinBallCanvas.transform.SetParent(menuCanvas.transform);

        CreatePinBallShopScreenButtons(pinBallCanvas);

        pinBallCanvas.SetActive(false);
    }

    /// <summary>
    /// Here we create main shop screen buttons
    /// </summary>
    private void CreatePinBallShopScreenButtons(GameObject temp)
    {
        CreateShopScreenButton("Price", ShopButton, "Back to shop screen", new Vector3((originalWidth / -2) + (originalWidth / 5 / 2), (originalHeight / 2) - (originalHeight / 8 / 2), 0), backButtonSprite, temp.transform, square, new bool[0]);
        temp.transform.Find("Back to shop screen").transform.gameObject.AddComponent<BackToMainShopScreenScript>();

        CreateShopScreenButton("Price", ShopButton, "1 PinBall Button", new Vector2(originalWidth / -3, originalHeight / 7), pinBallSprites[0], temp.transform, square, boughtPinBall);
        temp.transform.Find("1 PinBall Button").transform.gameObject.AddComponent<PinBallButonScript>();
        if (pinBallID == 0) CreateSelectedItemHighlight(temp, temp.transform.Find("1 PinBall Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "2 PinBall Button", new Vector2(0, originalHeight / 7), pinBallSprites[1], temp.transform, square, boughtPinBall);
        temp.transform.Find("2 PinBall Button").transform.gameObject.AddComponent<PinBallButonScript>();
        if (pinBallID == 1) CreateSelectedItemHighlight(temp, temp.transform.Find("2 PinBall Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "3 PinBall Button", new Vector2(originalWidth / 3, originalHeight / 7), pinBallSprites[2], temp.transform, square, boughtPinBall);
        temp.transform.Find("3 PinBall Button").transform.gameObject.AddComponent<PinBallButonScript>();
        if (pinBallID == 2) CreateSelectedItemHighlight(temp, temp.transform.Find("3 PinBall Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "4 PinBall Button", new Vector2(originalWidth / -3, originalHeight / -7), pinBallSprites[3], temp.transform, square, boughtPinBall);
        temp.transform.Find("4 PinBall Button").transform.gameObject.AddComponent<PinBallButonScript>();
        if (pinBallID == 3) CreateSelectedItemHighlight(temp, temp.transform.Find("4 PinBall Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "5 PinBall Button", new Vector2(0, originalHeight / -7), pinBallSprites[4], temp.transform, square, boughtPinBall);
        temp.transform.Find("5 PinBall Button").transform.gameObject.AddComponent<PinBallButonScript>();
        if (pinBallID == 4) CreateSelectedItemHighlight(temp, temp.transform.Find("5 PinBall Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "6 PinBall Button", new Vector2(originalWidth / 3, originalHeight / -7), pinBallSprites[5], temp.transform, square, boughtPinBall);
        temp.transform.Find("6 PinBall Button").transform.gameObject.AddComponent<PinBallButonScript>();
        if (pinBallID == 5) CreateSelectedItemHighlight(temp, temp.transform.Find("6 PinBall Button").transform.gameObject);
    }

    /// <summary>
    /// Here we create shop screen canvas
    /// </summary>
    private void CreateBouncyShopScreenCanvas()
    {
        bouncyCanvas = new GameObject("Bouncy shop screen");

        bouncyCanvas.transform.SetParent(menuCanvas.transform.Find("Shop screen"), gameObject.transform);

        bouncyCanvas.AddComponent<Canvas>();
        bouncyCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        bouncyCanvas.AddComponent<GraphicRaycaster>();
        bouncyCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        bouncyCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        bouncyCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        bouncyCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);
        bouncyCanvas.transform.SetParent(menuCanvas.transform);

        CreateBouncyShopScreenButtons(bouncyCanvas);

        bouncyCanvas.SetActive(false);
    }

    /// <summary>
    /// Here we create main shop screen buttons
    /// </summary>
    private void CreateBouncyShopScreenButtons(GameObject temp)
    {
        CreateShopScreenButton("Price", ShopButton, "Back to shop screen", new Vector3((originalWidth / -2) + (originalWidth / 5 / 2), (originalHeight / 2) - (originalHeight / 8 / 2), 0), backButtonSprite, temp.transform, square, new bool[0]);
        temp.transform.Find("Back to shop screen").transform.gameObject.AddComponent<BackToMainShopScreenScript>();

        CreateShopScreenButton("Price", ShopButton, "1 Bouncy Button", new Vector2(originalWidth / -3, originalHeight / 7), bouncySprites[0], temp.transform, square, boughtBouncy);
        temp.transform.Find("1 Bouncy Button").transform.gameObject.AddComponent<BouncyButonScript>();
        if (bouncyID == 0) CreateSelectedItemHighlight(temp, temp.transform.Find("1 Bouncy Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "2 Bouncy Button", new Vector2(0, originalHeight / 7), bouncySprites[1], temp.transform, square, boughtBouncy);
        temp.transform.Find("2 Bouncy Button").transform.gameObject.AddComponent<BouncyButonScript>();
        if (bouncyID == 1) CreateSelectedItemHighlight(temp, temp.transform.Find("2 Bouncy Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "3 Bouncy Button", new Vector2(originalWidth / 3, originalHeight / 7), bouncySprites[2], temp.transform, square, boughtBouncy);
        temp.transform.Find("3 Bouncy Button").transform.gameObject.AddComponent<BouncyButonScript>();
        if (bouncyID == 2) CreateSelectedItemHighlight(temp, temp.transform.Find("3 Bouncy Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "4 Bouncy Button", new Vector2(originalWidth / -3, originalHeight / -7), bouncySprites[3], temp.transform, square, boughtBouncy);
        temp.transform.Find("4 Bouncy Button").transform.gameObject.AddComponent<BouncyButonScript>();
        if (bouncyID == 3) CreateSelectedItemHighlight(temp, temp.transform.Find("4 Bouncy Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "5 Bouncy Button", new Vector2(0, originalHeight / -7), bouncySprites[4], temp.transform, square, boughtBouncy);
        temp.transform.Find("5 Bouncy Button").transform.gameObject.AddComponent<BouncyButonScript>();
        if (bouncyID == 4) CreateSelectedItemHighlight(temp, temp.transform.Find("5 Bouncy Button").transform.gameObject);

        CreateShopScreenButton("Price", ShopButton, "6 Bouncy Button", new Vector2(originalWidth / 3, originalHeight / -7), bouncySprites[5], temp.transform, square, boughtBouncy);
        temp.transform.Find("6 Bouncy Button").transform.gameObject.AddComponent<BouncyButonScript>();
        if (bouncyID == 5) CreateSelectedItemHighlight(temp, temp.transform.Find("6 Bouncy Button").transform.gameObject);
    }

    /// <summary>
    /// Here we create a canvas to keep our buttons and texts
    /// </summary>
    public void CreateCanvas()
    {
        CreateMenuCanvas();
        CreateMainScreenCanvas();
        CreateShopScreenCanvas();
        CreatePinBallShopScreenCanvas();
        CreateBackgroundShopScreenCanvas();
        CreateBouncyShopScreenCanvas();
        CreateCoinShopScreenCanvas();
        CreateYesOrNoCanvas();
        CreateAdsCanvas();
    }

    /// <summary>
    /// Creates our canvas to show ads
    /// </summary>
    private void CreateAdsCanvas()
    {
        GameObject adsCanvas = new GameObject("Ads canvas")
        {
            tag = "AdsCanvas"
        };

        adsCanvas.transform.SetParent(menuCanvas.transform);

        adsCanvas.AddComponent<Canvas>();
        adsCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        adsCanvas.AddComponent<GraphicRaycaster>();
        adsCanvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        adsCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        adsCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        adsCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);

        CreateBlackScreen(adsCanvas);
        CreateAdsButtons(adsCanvas);
        CreateYesorNoText(adsCanvas, new Vector3(0, originalHeight / 7, 0), "Do you want to watch an ad and get " + adsReward + " coins?");

        adsCanvas.SetActive(false);
    }

    /// <summary>
    /// Here we create main shop screen buttons
    /// </summary>
    private void CreateMainShopScreenButtons(GameObject temp)
    {
        CreateShopScreenButton("", ShopButton, "Back to main shop screen", new Vector3((originalWidth / -2) + (originalWidth / 5 / 2), (originalHeight / 2) - (originalHeight / 8 / 2), 0), backButtonSprite, temp.transform, square, new bool[0]);
        temp.transform.Find("Back to main shop screen").transform.gameObject.AddComponent<BackToMainMenuScript>();

        CreateShopScreenButton("Name", ShopButton, "Pinball", new Vector2(originalWidth / -4, originalHeight / 7), pinBallSprites[pinBallID], temp.transform, square, boughtPinBall);
        temp.transform.Find("Pinball").transform.gameObject.AddComponent<PinBallShopScript>();

        CreateShopScreenButton("Name", ShopButton, "Background", new Vector2(originalWidth / 4, originalHeight / 7), backgroundSprites[backgroundID], temp.transform, square, boughtPinBall);
        temp.transform.Find("Background").transform.gameObject.AddComponent<BackgroundShopScript>();

        CreateShopScreenButton("Name", ShopButton, "Flipper", new Vector2(originalWidth / -4, originalHeight / -7), bouncySprites[bouncyID], temp.transform, square, boughtPinBall);
        temp.transform.Find("Flipper").transform.gameObject.AddComponent<BouncyShopScript>();

        CreateShopScreenButton("Name", ShopButton, "Coin", new Vector2(originalWidth / 4, originalHeight / -7), coinSprites[coinID].sprites[0], temp.transform, square, boughtPinBall);
        temp.transform.Find("Coin").transform.gameObject.AddComponent<CoinShopScript>();
    }

    /// <summary>
    /// Here we create main screen buttons
    /// </summary>
    private void CreateMainScreenButtons(GameObject temp)
    {
        CreateShopScreenButton("", ShopButton, "Play", new Vector3(0, originalHeight / 7, 0), playButtonSprite, temp.transform, rectangle, new bool[0]);
        temp.transform.Find("Play").transform.gameObject.AddComponent<PlayButtonScript>();
        CreateShopScreenButton("", ShopButton, "Shop", new Vector3(0, originalHeight / -7, 0), shopButtonSprite, temp.transform, rectangle, new bool[0]);
        temp.transform.Find("Shop").transform.gameObject.AddComponent<ShopButtonScript>();
    }

    /// <summary>
    /// Here we create a canvas to keep our buttons and texts
    /// </summary>
    private void CreateGameCanvas()
    {
        canvas = new GameObject("Canvas");

        canvas.AddComponent<Canvas>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        canvas.AddComponent<CanvasScaler>();
        canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(originalWidth, originalHeight);

        canvas.AddComponent<GraphicRaycaster>();
        canvas.GetComponent<GraphicRaycaster>().ignoreReversedGraphics = true;

        canvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        canvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth, originalHeight);

        canvas.tag = "BouncyButtons";

        CreateButtons();
        CreateScoreText(canvas, new Vector3(originalWidth / 6, originalHeight / 2.25f, 0));
        CreateWalletText(canvas, new Vector3(originalWidth / -2.5f, originalHeight / 2.25f, 0));
        CreateTutorialScreenSpit(canvas);
    }

    /// <summary>
    /// Creates our coins wallet text
    /// </summary>
    private void CreateWalletText(GameObject canvas, Vector3 position)
    {
        wallet = new GameObject("Wallet");
        wallet.transform.parent = canvas.transform;

        wallet.AddComponent<Text>();

        wallet.AddComponent<ShowWalletScript>();

        wallet.GetComponent<Text>().fontSize = scoreTextSize;
        wallet.GetComponent<Text>().raycastTarget = true;
        wallet.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        wallet.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        wallet.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        wallet.GetComponent<RectTransform>().position = position;
    }

    /// <summary>
    /// Creates our Highscore text
    /// </summary>
    private void CreateHighscoreText(GameObject canvas, Vector3 position)
    {
        highScore = new GameObject("Highscore");
        highScore.transform.parent = canvas.transform;

        highScore.AddComponent<Text>();

        highScore.AddComponent<ShowHighScoreScript>();

        highScore.GetComponent<Text>().fontSize = scoreTextSize;
        highScore.GetComponent<Text>().raycastTarget = true;
        highScore.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        highScore.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        highScore.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        highScore.GetComponent<RectTransform>().position = position;
    }

    /// <summary>
    /// Creates our Creates our last score text text
    /// </summary>
    private void CreateLastScoreText(GameObject canvas, Vector3 position)
    {
        highScore = new GameObject("Last score");
        highScore.transform.parent = canvas.transform;

        highScore.AddComponent<Text>();

        highScore.AddComponent<LastScore>();

        highScore.GetComponent<Text>().fontSize = scoreTextSize;
        highScore.GetComponent<Text>().raycastTarget = true;
        highScore.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        highScore.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        highScore.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        highScore.GetComponent<RectTransform>().position = position;
    }

    /// <summary>
    /// Creates a text field for our current score
    /// </summary>
    private void CreateScoreText(GameObject canvas, Vector3 position)
    {
        currentScore = new GameObject("Score");
        currentScore.transform.parent = canvas.transform;

        currentScore.AddComponent<Text>();

        currentScore.AddComponent<ShowScoreScript>();

        currentScore.GetComponent<Text>().fontSize = scoreTextSize;
        currentScore.GetComponent<Text>().raycastTarget = true;
        currentScore.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        currentScore.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        currentScore.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        currentScore.GetComponent<RectTransform>().position = position;
    }

    /// <summary>
    /// Adds coins to current coins in game wallet
    /// </summary>
    /// <param name="coins">The ammount added</param>
    public void AddToCurrentCoins(int coins)
    {
        currentCoins += coins;
    }

/// <summary>
/// Adds text to a button
/// </summary>
/// <param name="forWho">Wich button is it added to</param>
/// <param name="name">What the button says</param>
    private void AddText(GameObject forWho, string name)
    {
        forWho.AddComponent<Text>();

        forWho.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        forWho.GetComponent<Text>().fontSize = 30;
        forWho.GetComponent<Text>().raycastTarget = true;
        forWho.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        forWho.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
        forWho.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        forWho.GetComponent<Text>().text = name;
    }

    /// <summary>
    /// Here we create left and right buttons
    /// </summary>
    private void CreateButtons()
    {
        leftButton = new GameObject("Left Button");
        leftButton.transform.parent = canvas.transform;
        AddText(leftButton, "LEFT FLIPPER");
        leftButton.AddComponent<Button>();

        leftButton.GetComponent<RectTransform>().position = new Vector3(originalWidth / -4, 0, 0);
        leftButton.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth / 2, originalHeight);

        leftButton.AddComponent<ButtonsScript>();




        rightButton = new GameObject("Right Button");
        rightButton.transform.parent = canvas.transform;
        AddText(rightButton, "RIGHT FLIPPER");
        rightButton.AddComponent<Button>();

        rightButton.GetComponent<RectTransform>().position = new Vector3(originalWidth / 4, 0, 0);
        rightButton.GetComponent<RectTransform>().sizeDelta = new Vector2(originalWidth / 2, originalHeight);

        rightButton.AddComponent<ButtonsScript>();

    }

    /// <summary>
    /// Allows other scripts to set the interval in wich the ccoins spawn
    /// </summary>
    /// <param name="interval">Coin spawn interval</param>
    public void SetCoinSpawnInterval(float interval)
    {
        coinSpawnInterval = interval;
    }

    /// <summary>
    /// This is our coin spawner
    /// </summary>
    /// <param name="interval">interval in wich our coins spawn</param>
    private void CoinSpawner(float interval)
    {
        InvokeRepeating("CreateCoin", interval, interval);
    }

    /// <summary>
    /// Sets our current score to 0 for a new game
    /// </summary>
    private void SetScoreToZero()
    {
        score = 0;
    }

    /// <summary>
    /// Here we can call our score increase from other scripts
    /// </summary>
    /// <param name="value">the value we increase the score by</param>
    public void IncreaseScore(int value)
    {
        score += value;
    }

    /// <summary>
    /// Meant for other scripts to check if we beat our previous highscore
    /// </summary>
    public void CheckForHighscore()
    {
        if (score > highscore)
        {
            highscore = score;
        }
    }

    /// <summary>
    /// This is our array of coins
    /// </summary>
    [System.Serializable]                                           // Mandatory for struct to appear in inspector
    public struct CoinSprites
    {
        public List<Sprite> sprites;                                // Our array of sprites
    }

    /// <summary>
    /// Here we set the coin scale to match our designateed size
    /// </summary>
    /// <param name="bouncy">Our bouncy gameobject</param>
    private void ChangeCoinTranformationScale(GameObject coin, int coinValue)
    {
        SpriteRenderer spriteRenderer = coin.GetComponent<SpriteRenderer>();        // This is our spriterenderer component

        spriteRenderer.drawMode = SpriteDrawMode.Sliced;                            // We set the sprite deawmode to sliced because it cannot scale properly otherwise

        float spriteHeight = spriteRenderer.sprite.texture.height;                  // We get the sprites height to calculate how to scale it
        float spriteWidth = spriteRenderer.sprite.texture.width;                    // We get the sprites width to calculate how to scale it

        // Here we set the new scale vector, we get the screen size and sprite size and divide them to calculate the scale needed to scale the sprite to match screensize
        coin.GetComponent<Transform>().localScale = new Vector3(coinPxSizeX / spriteWidth * scaleX, coinPxSizeY / spriteHeight * scaleY, 1f);

        coin.AddComponent<CircleCollider2D>();

        coin.GetComponent<CircleCollider2D>().isTrigger = true;

        coin.AddComponent<CoinScript>();

        coin.tag = coinValue + "coin";
    }

    /// <summary>
    /// Sets the max speed of pinBall gameobject so we dont break the speed of sound
    /// </summary>
    /// <param name="pinBall"></param>
    private void SetPinBallMaxSpeed(GameObject pinBall)
    {
        if (pinBall)
        {
            pinBall.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(pinBall.GetComponent<Rigidbody2D>().velocity, pinBallMaxSpeed);
        }
    }

    /// <summary>
    /// Chooses a random coin value 1-10
    /// </summary>
    /// <returns>returns a random value of 1-10</returns>
    private int ChooseCoinValue()
    {
        int id = Random.Range(1, 11);
        return Mathf.RoundToInt(id);
    }

    /// <summary>
    /// Coin X random spawn position
    /// </summary>
    /// <returns></returns>
    private float CoinPositionX()
    {
        return Random.Range(coinStartPosX, coinFinishPosX);
    }

    /// <summary>
    /// Coin Y random spawn position
    /// </summary>
    /// <returns></returns>
    private float CoinPositionY()
    {
        return Random.Range(coinStartPosY, coinFinishPosY);
    }

    /// <summary>
    /// Sets coin sprite
    /// </summary>
    /// <param name="coin">Coin gameobject that we set the parameters to</param>
    /// <param name="coinValue">We choose wich coin sprites we use</param>
    void SetCoinSprite(GameObject coin, int coinValue)
    {
        coin.AddComponent<SpriteRenderer>();
        coin.GetComponent<SpriteRenderer>().sprite = coinSprites[coinID].sprites[coinValue - 1]; //Selects and adds a sprite to the coin gameobject

        ChangeCoinTranformationScale(coin, coinValue);
    }

    /// <summary>
    /// Creates the bouncy gameobject at the specified position and rotation
    /// </summary>
    /// <param name="coin">The gameobject we are going to use</param>
    /// <param name="position">The position of the boucy on the screen</param>
    /// <param name="rotation">The rotation of the bouncy on the screen</param>
    private void SetCoinPosition(GameObject coin, Vector3 position)
    {
        coin.transform.position = position;                   // Here we set the position of the bouncy

        SetCoinSprite(coin, ChooseCoinValue());
    }

    /// <summary>
    /// Adds a sound to a coin on collision
    /// </summary>
    /// <param name="coin">our coin gameobject</param>
    private void SetCoinSound(GameObject coin)
    {
        coin.AddComponent<AudioSource>();
        coin.GetComponent<AudioSource>().clip = coinSound;
        coin.GetComponent<AudioSource>().playOnAwake = false;
    }

    /// <summary>
    /// A void dedicated to creating the bouncy knock ups
    /// </summary>
    private void CreateCoin()
    {
        coin = new GameObject("Coin");
        float coinX = CoinPositionX();                          // Our right bouncy position relative to the X axis
        float coinY = CoinPositionY();                          // Our right bouncy position relative to the Y axis
        float coinZ = 0F;                                       // Our right bouncy position relative to the Z axis

        SetCoinPosition(coin, new Vector3(coinX, coinY, coinZ));
    }

    /// <summary>
    /// A void dedicated to creating the bouncy knock ups
    /// </summary>
    private void CreateBouncy()
    {
        float bouncyRightX = bouncyWidth;                           // Our right bouncy position relative to the X axis
        float bouncyRightY = bouncyHeight;                          // Our right bouncy position relative to the Y axis
        float bouncyRightZ = 0F;                                    // Our right bouncy position relative to the Z axis

        float bouncyLeftX = -bouncyWidth;                           // Our left bouncy position relative to the X axis
        float bouncyLeftY = bouncyHeight;                           // Our left bouncy position relative to the Y axis
        float bouncyLeftZ = 0F;                                     // Our left bouncy position relative to the Z axis

        float bouncyRotationRightX = 0f;                            // Our right bouncy rotation relative to the X axis
        float bouncyRotationRightY = 0f;                            // Our right bouncy rotation relative to the Y axis
        float bouncyRotationRightZ = bouncyRotation;                // Our right bouncy rotation relative to the Z axis (default rotation axis)

        float bouncyRotationLeftX = 0f;                             // Our left bouncy rotation relative to the X axis
        float bouncyRotationLeftY = 0f;                             // Our left bouncy rotation relative to the Y axis
        float bouncyRotationLeftZ = -bouncyRotation;                // Our left bouncy rotation relative to the Z axis (default rotation axis) 


        SetBouncyPosition(bounceRight, new Vector3(bouncyRightX, bouncyRightY, bouncyRightZ), new Vector3(bouncyRotationRightX, bouncyRotationRightY, bouncyRotationRightZ));
        SetBouncyPosition(bounceLeft, new Vector3(bouncyLeftX, bouncyLeftY, bouncyLeftZ), new Vector3(bouncyRotationLeftX, bouncyRotationLeftY, bouncyRotationLeftZ));
    }

    /// <summary>
    /// Creates the bouncy gameobject at the specified position and rotation
    /// </summary>
    /// <param name="bouncy">The gameobject we are going to use</param>
    /// <param name="position">The position of the boucy on the screen</param>
    /// <param name="rotation">The rotation of the bouncy on the screen</param>
    private void SetBouncyPosition(GameObject bouncy, Vector3 position, Vector3 rotation)
    {
        bouncy.transform.position = position;                   // Here we set the position of the bouncy
        bouncy.transform.eulerAngles = rotation;                // Here we set the rotation of the object

        SetBouncySprite(bouncy);
    }

    /// <summary>
    /// Here we set the sprite to the bouncy
    /// </summary>
    /// <param name="bouncy">Our bouncy gameobject</param>
    void SetBouncySprite(GameObject bouncy)
    {
        bouncy.GetComponent<SpriteRenderer>().sprite = bouncySprites[bouncyID]; //Selects and adds a sprite to the boouncy gameobject

        ChangeBouncyTranformationScale(bouncy);
    }

    /// <summary>
    /// Here we set the bouncy scale to match our designateed size
    /// </summary>
    /// <param name="bouncy">Our bouncy gameobject</param>
    private void ChangeBouncyTranformationScale(GameObject bouncy)
    {
        SpriteRenderer spriteRenderer = bouncy.GetComponent<SpriteRenderer>();  // This is our spriterenderer component

        spriteRenderer.drawMode = SpriteDrawMode.Sliced;                            // We set the sprite deawmode to sliced because it cannot scale properly otherwise

        float spriteHeight = spriteRenderer.sprite.texture.height;                  // We get the sprites height to calculate how to scale it
        float spriteWidth = spriteRenderer.sprite.texture.width;                    // We get the sprites width to calculate how to scale it

        // Here we set the new scale vector, we get the screen size and sprite size and divide them to calculate the scale needed to scale the sprite to match screensize
        bouncy.GetComponent<Transform>().localScale = new Vector3(1.5f, bouncyPxSizeY / spriteHeight * scaleY, 1f);

        Instantiate(bouncy);                                                        // Here we spawn the object
    }

    /// <summary>
    /// Here we the the screen size and screen size of the default size the game was created on and scale all the objects to it 
    /// </summary>
    private void ScreenScale()
    {
        //scaleX = Screen.width / originalWidth;                      // Sets the X axis scale
        //scaleY = Screen.height / originalHeight;                    // Sets the Y axis scale

        //ScaleDistancesAndObjects(scaleX, scaleY);
    }

    /// <summary>
    /// Scales All the gameobjects to screensize
    /// </summary>
    /// <param name="scaleX">The scale parameter for X axis</param>
    /// <param name="sclaeY">The scale parameter for Y axis</param>
    private void ScaleDistancesAndObjects(float scaleX, float sclaeY)
    {
        wallWidth *= scaleX;
        wallRadius *= scaleY;

        pinBallPxSizeX *= scaleX;
        pinBallPxSizeY *= sclaeY;
        pinBallStartPosX *= scaleX;
        pinBallStartPosY *= sclaeY;

        bouncyPxSizeX *= scaleX;
        bouncyPxSizeY *= sclaeY;

        bouncyWidth *= scaleX;
        bouncyHeight *= sclaeY;
    }

    /// <summary>
    /// Creates the game walls
    /// </summary>
    private void CreateWalls()
    {
        wallLeft = new GameObject("Wall Left");
        wallRight = new GameObject("Wall Right");
        wallTop = new GameObject("Wall top");
        platform = new GameObject("Platform");
        deathWall = new GameObject("Death wall");

        wallLeft.AddComponent<SpriteRenderer>();                    // Adds the sprirte renderer ccomponent so we can see the size of the wall and measure colliders
        wallRight.AddComponent<SpriteRenderer>();                   // Adds the sprirte renderer ccomponent so we can see the size of the wall and measure colliders
        wallTop.AddComponent<SpriteRenderer>();                     // Adds the sprirte renderer ccomponent so we can see the size of the wall and measure colliders
        platform.AddComponent<SpriteRenderer>();                    // Adds the sprirte renderer ccomponent so we can see the size of the wall and measure colliders
        deathWall.AddComponent<SpriteRenderer>();                   // Adds the sprirte renderer ccomponent so we can see the size of the wall and measure colliders

        SetSpriteToWall(wallLeft, new Vector3(0, 0, 90));
        SetSpriteToWall(wallRight, new Vector3(0, 0, 90));
        SetSpriteToWall(wallTop, new Vector3(0, 0, 0));
        SetSpriteToWall(platform, new Vector3(0, 0, 0));
        SetSpriteToWall(deathWall, new Vector3(0, 0, 0));

        deathWall.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        deathWall.AddComponent<DeathWallScript>();

        deathWall.AddComponent<AudioSource>();
        deathWall.GetComponent<AudioSource>().clip = gameOverSound;
        deathWall.GetComponent<AudioSource>().playOnAwake = false;

        SetWallPosition(wallTop, new Vector3(0, originalHeight / 2 * scaleY / 100));
        SetWallPosition(wallRight, new Vector3(originalWidth / 2 * scaleY / 100, 0));
        SetWallPosition(wallLeft, new Vector3(originalWidth / 2 * scaleY / 100 * -1, 0));
        SetWallPosition(platform, new Vector3(0, originalHeight / 2 * scaleY / 100 * -1));
        SetWallPosition(deathWall, new Vector3(0, originalHeight / 2 * scaleY / 105 * -1));

        wallLeft.AddComponent<EdgeCollider2D>();                    // Adds the edge collider component so the gameobject has a collider
        wallRight.AddComponent<EdgeCollider2D>();                   // Adds the edge collider component so the gameobject has a collider
        wallTop.AddComponent<EdgeCollider2D>();                     // Adds the edge collider component so the gameobject has a collider
        platform.AddComponent<EdgeCollider2D>();                    // Adds the edge collider component so the gameobject has a collider
        deathWall.AddComponent<EdgeCollider2D>();                   // Adds the edge collider component so the gameobject has a collider

        deathWall.GetComponent<EdgeCollider2D>().isTrigger = true;  // Makes the deathWall a trigger so we can detect when ball is colliding with the deathWall

        SetWallRadius(wallLeft);
        SetWallRadius(wallRight);
        SetWallRadius(wallTop);
        SetWallRadius(platform);
        SetWallRadius(deathWall);
    }

    /// <summary>
    /// Sets the wall radius for the wall gameobject
    /// </summary>
    /// <param name="wall">gameobject for witch we set the edgecollider radius</param>
    private void SetWallRadius(GameObject wall)
    {
        wall.GetComponent<EdgeCollider2D>().edgeRadius = wallRadius;
    }

    /// <summary>
    /// Sets a sprite texture to a wall gameobject
    /// </summary>
    /// <param name="wall">gameobject to set the sprite on</param>
    /// <param name="rotation">rotation of the set sprite(Edge collider set didint work se we rotate the left and right walls by 90 degrees)</param>
    private void SetSpriteToWall(GameObject wall, Vector3 rotation)
    {
        SpriteRenderer spriterenderer = wall.GetComponent<SpriteRenderer>();

        spriterenderer.sprite = standart;                                       // We set the standrat sprite used for invisible walls

        spriterenderer.drawMode = SpriteDrawMode.Sliced;                        // Without this the scaling of the object wouldnt work

        float spriteHeight = spriterenderer.sprite.texture.height;
        float spriteWidth = spriterenderer.sprite.texture.width;

        // We select the longer screen size of the 2 and use that for scaling
        if (Screen.height >= Screen.width)
        {
            wall.GetComponent<Transform>().localScale = new Vector3(originalHeight / spriteHeight * scaleY, wallWidth / spriteWidth * scaleX);  // Here we scale the gameobject
        }
        else if (Screen.width > Screen.height)
        {
            wall.GetComponent<Transform>().localScale = new Vector3(wallWidth / spriteWidth * scaleX, originalHeight / spriteHeight * scaleY);  // Here we scale the gameobject
        }

        wall.GetComponent<Transform>().Rotate(rotation);                        // Here we rotate the gameobjectobject
    }

    /// <summary>
    /// Here we set the wall position
    /// </summary>
    /// <param name="wall">The wall gameobject for witch we change the position</param>
    /// <param name="position">The position of the gameobject</param>
    private void SetWallPosition(GameObject wall, Vector3 position)
    {
        wall.transform.position = position;
    }

    /// <summary>
    /// Creates the pinBall gameobject
    /// </summary>
    /// <param name="coordinates">coordinates where to spawn the pinBall</param>
    private void CreatepinBall(Vector3 coordinates)
    {
        pinBall = new GameObject("Pin Ball");                       // Creates the pinBall gameobject
        pinBall.AddComponent<SpriteRenderer>();                     // Adds the sprite renderer component to pinBall gameobject to to modify sprites
        pinBall.AddComponent<CircleCollider2D>();                   // Adds the circle collider component to pinBall gameobject to add circle collisions so the balls is shaped like a ball
        pinBall.AddComponent<Rigidbody2D>();                        // Adds the rigidbody component to pinBall gameobject to make the ball have a solid form
        pinBall.transform.position = coordinates;                   // Sets the ball spawn point to the given coordinates
        pinBall.tag = "Player";                                     // Sets the ball tag as "Player"

        pinBall.GetComponent<CircleCollider2D>().sharedMaterial = pinBallMaterial;  // Sets the pinBall material so the ball can bounce
        pinBall.GetComponent<CircleCollider2D>().radius = 1;        // Matches the collider radius to the texture size

        SetCoinSound(pinBall);

        SetSpriteToPinBall(pinBallID);
    }

    /// <summary>
    /// Adds a sprite to pinBall
    /// </summary>
    /// <param name="pinBallID">The sprite ID we use from pinBall sprites list to add a sprite to pinBall</param>
    void SetSpriteToPinBall(int pinBallId)
    {
        pinBall.GetComponent<SpriteRenderer>().sprite = pinBallSprites[pinBallId];  // Adds the srpite ti pinBall gameobject

        ScalePinBallSpriteToGameobject();
    }

    /// <summary>
    /// Scales pinBall sprite to the pinBallgameobject
    /// </summary>
    private void ScalePinBallSpriteToGameobject()
    {
        SpriteRenderer spriterenderer = pinBall.GetComponent<SpriteRenderer>(); // We get the spriterenderrer component so we can edit the sprites attached to this gameobjetc

        spriterenderer.drawMode = SpriteDrawMode.Sliced;            // Set the texture to match the gameobject

        float spriteHeight = spriterenderer.sprite.texture.height;
        float spriteWidth = spriterenderer.sprite.texture.width;

        pinBall.GetComponent<Transform>().localScale = new Vector3(pinBallPxSizeX / spriteWidth * scaleX, pinBallPxSizeY / spriteHeight * scaleY); // Scales the gameobject to match game requirements
    }

    /// <summary>
    /// Creates the background
    /// </summary>
    private void CreateBackground()
    {
        background = new GameObject("Background");     // Creates background gameobject
        background.AddComponent<SpriteRenderer>();      // Adds a spriterenderer component to the background gameobject witch is used to manipulate sprites
        background.transform.position = new Vector3(0, 0, 1);

        SetBackgroundSprite(backgroundID);              // Sets the background on launch
    }

    /// <summary>
    /// Sets the background gameobject sprite
    /// </summary>
    /// <param name="backgroundID">Background ID to know witch sprite to use from background sprites list</param>
    void SetBackgroundSprite(int backgroundId)
    {
        background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[backgroundId]; //Selects and adds a sprite to the gbackground gameobject

        ChangeBackgroundTranformationScale();
    }

    /// <summary>
    /// Sets the background sprite to match screensize
    /// </summary>
    private void ChangeBackgroundTranformationScale()
    {
        SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();  // This is our spriterenderer component

        spriteRenderer.drawMode = SpriteDrawMode.Sliced;                            // We set the sprite deawmode to sliced because it cannot scale properly otherwise

        float spriteHeight = spriteRenderer.sprite.texture.height;                  // We get the sprites height to calculate how to scale it
        float spriteWidth = spriteRenderer.sprite.texture.width;                    // We get the sprites width to calculate how to scale it

        // Here we set the new scale vector, we get the screen size and sprite size and divide them to calculate the scale needed to scale the sprite to match screensize
        background.GetComponent<Transform>().localScale = new Vector3(originalWidth / spriteWidth * scaleX, originalHeight / spriteHeight * scaleY, 1);
    }

    // Update is called once per frame
    void Update()
    {
        SetPinBallMaxSpeed(pinBall);                    // Sets our pinBall max speed
    }
}
