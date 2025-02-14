using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GPSPIN;
using static System.Net.Mime.MediaTypeNames;

public class UIHandler : MonoBehaviour
{
    public GameObject home;
    public GameObject pinUI;
    public GameObject pinUI2;
    public GameObject pinUI3;
    public RawImage pinUIImage;
    public RawImage pinUIImage2;
    public RawImage pinUIImage3;
    public GameObject tutorialOverlay;
    public GameObject tutorialPin;
    public Button activatePinButton;
    public GameHandler gameHandler;
    public GameObject gameOverScreen;
    public float minDistToPin;
    public float maxDistToPin;
    TMP_Text ActiveButtonText;

    private Sprite spaSprite;
    private Sprite arcadeSprite;
    private Sprite artsSprite;
    private Sprite gullSprite;
    private Sprite highRiseSprite;
    private Sprite parkingSprite;
    private Sprite busSprite;

    public TMP_Text gameOverScore;

    TMP_Text locationName;
    int currentPin = -1;

    // Start is called before the first frame update
    void Start()
    {
        home = GameObject.Find("Home");
        pinUI = GameObject.Find("PinUI");
        pinUI2 = GameObject.Find("PinUI (1)");
        pinUI3 = GameObject.Find("PinUI (2)");
        tutorialOverlay = GameObject.Find("TutorialOverlay");
        tutorialPin = GameObject.Find("TutorialPin");

        pinUIImage = pinUI.GetComponentInChildren<RawImage>();
        pinUIImage2 = pinUI2.GetComponentInChildren<RawImage>();
        pinUIImage3 = pinUI3.GetComponentInChildren<RawImage>();

        spaSprite = Resources.Load<Sprite>("Spa");
        arcadeSprite = Resources.Load<Sprite>("Arcade");
        artsSprite = Resources.Load<Sprite>("Arts");
        gullSprite = Resources.Load<Sprite>("Gull");
        highRiseSprite = Resources.Load<Sprite>("High Rise");
        parkingSprite = Resources.Load<Sprite>("Parking");
        busSprite = Resources.Load<Sprite>("Bus");

        activatePinButton = GameObject.Find("Canvas/Home/ActivatePinButton").GetComponent<Button>();
        locationName = activatePinButton.GetComponentInChildren<TMP_Text>();

        pinUI.SetActive(false);
        pinUI2.SetActive(false);
        pinUI3.SetActive(false);
        tutorialOverlay.SetActive(false);
        tutorialPin.SetActive(false);
    }
    private void Update()
    {
        if (this.currentPin >= 0 && gameHandler.closestDist > maxDistToPin)
        {
            this.currentPin = 0;
        } else if (gameHandler.closestDist < minDistToPin)
        {
            this.currentPin = gameHandler.closestPin;
        }

        bool active = this.currentPin >= 0;
        activatePinButton.interactable = active;
        locationName.text = active ? gameHandler.pins[this.currentPin].pinName : "No pin active";
    }
    public void Toggle_Tutor()
    {
        Debug.Log("Toggle_Tutor Called");
        tutorialOverlay.SetActive(!tutorialOverlay.activeSelf);
    }
    public void Toggle_Tutor_Pin()
    {
        tutorialOverlay.SetActive(!tutorialOverlay.activeSelf);
        tutorialPin.SetActive(!tutorialPin.activeSelf);
    }
    public void Toggle_Pin()
    {
        GPSPIN pin = gameHandler.pins[this.currentPin];
        switch (pin.pinType)
        {
            case PinType.Beach:
                pinUI.SetActive(!pinUI.activeSelf);
                gameHandler.Pin_Visited();
                gameHandler.scorePinsVisited = (gameHandler.scorePinsVisited + 20);
                break;
            case PinType.Commercial:
                pinUI2.SetActive(!pinUI2.activeSelf);
                gameHandler.Pin_Visited();
                gameHandler.scorePinsVisited = (gameHandler.scorePinsVisited + 10);
                break;
            case PinType.Residential:
                pinUI3.SetActive(!pinUI3.activeSelf);
                gameHandler.Pin_Visited();
                gameHandler.scorePinsVisited = (gameHandler.scorePinsVisited + 20);
                break;
        }
    }

    public void Close_Pin()
    {
        pinUI.SetActive(false);
        pinUI2.SetActive(false);
        pinUI3.SetActive(false);
        GameOver();
    }

    public void Change_Image(GPSPIN.PinType pinType)
    {
        
    }

    public void GameOver()
    {
        if (gameHandler.numPinsVisited == 8)
        {
            gameOverScreen.SetActive(true);
            gameOverScore.text = "Score: " + (gameHandler.numPinsVisited + gameHandler.scorePinsVisited * (gameHandler.economyHealth+gameHandler.environmentHealth - gameHandler.housingDemand) * 10).ToString();
        }
    }

}
