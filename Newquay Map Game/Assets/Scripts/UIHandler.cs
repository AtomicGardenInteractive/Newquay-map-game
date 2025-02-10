using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    TMP_Text ActiveButtonText;

    private Sprite spaSprite;
    private Sprite arcadeSprite;
    private Sprite artsSprite;
    private Sprite gullSprite;
    private Sprite highRiseSprite;
    private Sprite parkingSprite;
    private Sprite busSprite;

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

        //ActiveButtonText = activatePinButton.GetComponentInChildren<TextMeshPro>();

        pinUI.SetActive(false);
        pinUI2.SetActive(false);
        pinUI3.SetActive(false);
        tutorialOverlay.SetActive(false);
        tutorialPin.SetActive(false);

        //ActivatePinButton.GetComponentInChildren<TextMeshPro>().text = "";

    }
    private void Update()
    {

        //if (GameHandler.closestDist < MinDistToPin)
        {
            //ActivatePinButton.GetComponent<Button>().interactable = true;
            //ActivatePinButton.GetComponentInChildren<TextMeshPro>().text = "Beans";
        }
        //else 
        {
            //ActivatePinButton.GetComponent<Button>().interactable = false;
            //ActivatePinButton.GetComponentInChildren<TextMeshPro>().text = "";
        }

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
        pinUI.SetActive(!pinUI.activeSelf);
        GameOver();
    }
    public void Toggle_Pin2()
    {
        pinUI2.SetActive(!pinUI2.activeSelf);
        GameOver();
    }
    public void Toggle_Pin3()
    {
        pinUI3.SetActive(!pinUI3.activeSelf);
        GameOver();
    }
    public void Change_Image()
    {

    }

    public void GameOver()
    {
        if (gameHandler.numPinsVisited == 8)
        {
            gameOverScreen.SetActive(true);
        }
    }

}
