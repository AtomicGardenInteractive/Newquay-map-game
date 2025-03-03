using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using static GPSPIN;
using static System.Net.Mime.MediaTypeNames;

public class UIHandler : MonoBehaviour
{
    public GameObject home;
    public GameObject pinUI;
    public GameObject pinUI2;
    public GameObject pinUI3;
    public PINUIHandler pinUIHandler1;
    public PINUIHandler pinUIHandler2;
    public PINUIHandler pinUIHandler3;
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
    public NotificationHandler notificationHandler;
    TMP_Text ActiveButtonText;
    public GameObject startScreen;
    

    public enum choices { Nothing, BeachBNS, BeachHNW, BeachEP, CommBNS, CommHNW, CommANC, ResdHR, ResdOTG, ResdPNR };
    public choices choiceChoosen;

    float gameTime;
    float maxGameLeng = 2400.0f;

    public TMP_Text gameOverScore;

    TMP_Text locationName;

    // Start is called before the first frame update
    void Start()
    {
        notificationHandler.RequestAuthorization();
        notificationHandler.RegisterNotificationChannel();

        home = GameObject.Find("Home");
        pinUI = GameObject.Find("PinUI");
        pinUI2 = GameObject.Find("PinUI (1)");
        pinUI3 = GameObject.Find("PinUI (2)");
        tutorialOverlay = GameObject.Find("TutorialOverlay");
        tutorialPin = GameObject.Find("TutorialPin");

        pinUIHandler1 = pinUI.GetComponent<PINUIHandler>();
        pinUIHandler2 = pinUI2.GetComponent<PINUIHandler>();
        pinUIHandler3 = pinUI3.GetComponent<PINUIHandler>();

        pinUIImage = pinUI.GetComponentInChildren<RawImage>();
        pinUIImage2 = pinUI2.GetComponentInChildren<RawImage>();
        pinUIImage3 = pinUI3.GetComponentInChildren<RawImage>();

        activatePinButton = GameObject.Find("Canvas/Home/ActivatePinButton").GetComponent<Button>();
        locationName = activatePinButton.GetComponentInChildren<TMP_Text>();

        pinUI.SetActive(false);
        pinUI2.SetActive(false);
        pinUI3.SetActive(false);
        tutorialOverlay.SetActive(false);
        tutorialPin.SetActive(false);
    }

    public void Game_Start()
    {
        startScreen.SetActive(false);
        gameHandler.economyHealth = 0.4f;
        gameHandler.environmentHealth = 0.4f;
        gameHandler.housingDemand = 0.4f;
    }
    private void Update()
    {
        bool active = gameHandler.closestDist <= gameHandler.distanceThreshold;
        activatePinButton.interactable = active;
        locationName.text = active ? gameHandler.pins[gameHandler.closestPin].pinName : "No pin active";

        for (int i = 0; i < gameHandler.pins.Length; i++)
        {
            gameHandler.pins[i].youAreHere.SetActive(false);
        }
        gameHandler.pins[gameHandler.closestPin].youAreHere.SetActive(active);

        gameTime = Mathf.Clamp(gameTime + Time.deltaTime, 0.0f, 2400.0f);
        if (gameTime >= maxGameLeng)
        {
            gameOverScreen.SetActive(true);
            gameOverScore.text = "Score: " + (gameHandler.numPinsVisited + gameHandler.scorePinsVisited * (gameHandler.economyHealth + gameHandler.environmentHealth - gameHandler.housingDemand) * 10).ToString();
        }

    }
    public void Toggle_Tutor()
    {
        tutorialOverlay.SetActive(!tutorialOverlay.activeSelf);
    }
    public void Toggle_Tutor_Pin()
    {
        tutorialOverlay.SetActive(!tutorialOverlay.activeSelf);
        tutorialPin.SetActive(!tutorialPin.activeSelf);
    }
    public void Toggle_Pin()
    {
        GPSPIN pin = gameHandler.pins[gameHandler.closestPin];
        if (pin.visited == false)
        {
            switch (pin.pinType)
            {
            case PinType.Beach:
                pinUIHandler1.imageDefault.SetActive(true);
                pinUI.SetActive(!pinUI.activeSelf);
                gameHandler.scorePinsVisited = (gameHandler.scorePinsVisited + 20);
                break;
            case PinType.Commercial:
                pinUIHandler2.imageDefault.SetActive(true);
                pinUI2.SetActive(!pinUI2.activeSelf);
                gameHandler.scorePinsVisited = (gameHandler.scorePinsVisited + 10);
                break;
            case PinType.Residential:
                pinUIHandler3.imageDefault.SetActive(true);
                pinUI3.SetActive(!pinUI3.activeSelf);
                gameHandler.scorePinsVisited = (gameHandler.scorePinsVisited + 20);
                break;
            }

        }
    }

    public void Beach_BNS() 
    {
        if (choiceChoosen != choices.BeachBNS)
        {
            pinUIHandler1.imageDefault.SetActive(false);
            pinUIHandler1.imageOptionOne.SetActive(true);
            pinUIHandler1.imageOptionTwo.SetActive(false);
            pinUIHandler1.imageOptionThree.SetActive(false);
            choiceChoosen = choices.BeachBNS;
        }
        else { choiceChoosen = choices.Nothing; clear_pinUI(); }   
    }
    public void Beach_HNW() 
    {
        if (choiceChoosen != choices.BeachHNW)
        {
            pinUIHandler1.imageDefault.SetActive(false);
            pinUIHandler1.imageOptionOne.SetActive(false);
            pinUIHandler1.imageOptionTwo.SetActive(true);
            pinUIHandler1.imageOptionThree.SetActive(false);
            choiceChoosen = choices.BeachHNW;
        }
        else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }
    public void Beach_EP()
    {
        if (choiceChoosen != choices.BeachEP) 
        {
            pinUIHandler1.imageDefault.SetActive(false);
            pinUIHandler1.imageOptionOne.SetActive(false);
            pinUIHandler1.imageOptionTwo.SetActive(false);
            pinUIHandler1.imageOptionThree.SetActive(true);
            choiceChoosen = choices.BeachEP;
        }
        else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }

    public void Commercial_BNS() 
    {
        if (choiceChoosen != choices.CommBNS)
        {
            pinUIHandler2.imageDefault.SetActive(false);
            pinUIHandler2.imageOptionOne.SetActive(true);
            pinUIHandler2.imageOptionTwo.SetActive(false);
            pinUIHandler2.imageOptionThree.SetActive(false);
            choiceChoosen = choices.CommBNS;
        }
        else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }
    public void Commercial_HNW() 
    {
        if (choiceChoosen != choices.CommHNW)
        {
            pinUIHandler2.imageDefault.SetActive(false);
            pinUIHandler2.imageOptionOne.SetActive(false);
            pinUIHandler2.imageOptionTwo.SetActive(true);
            pinUIHandler2.imageOptionThree.SetActive(false);
            choiceChoosen = choices.CommHNW;
        }
            else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }
    public void Commercial_ANC() 
    {
        if (choiceChoosen != choices.CommANC)
        {
            pinUIHandler2.imageDefault.SetActive(false);
            pinUIHandler2.imageOptionOne.SetActive(false);
            pinUIHandler2.imageOptionTwo.SetActive(false);
            pinUIHandler2.imageOptionThree.SetActive(true);
            choiceChoosen = choices.CommANC;
        }
            else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }

    public void Residential_HR() 
    {
        if (choiceChoosen != choices.ResdHR)
        {
            pinUIHandler3.imageDefault.SetActive(false);
            pinUIHandler3.imageOptionOne.SetActive(true);
            pinUIHandler3.imageOptionTwo.SetActive(false);
            pinUIHandler3.imageOptionThree.SetActive(false);
            choiceChoosen = choices.ResdHR;
        }
            else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }
    public void Residential_OTG() 
    {
        if (choiceChoosen != choices.ResdOTG)
        {
            pinUIHandler3.imageDefault.SetActive(false);
            pinUIHandler3.imageOptionOne.SetActive(false);
            pinUIHandler3.imageOptionTwo.SetActive(true);
            pinUIHandler3.imageOptionThree.SetActive(false);
            choiceChoosen = choices.ResdOTG;
        }
            else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }
    public void Residential_PNR() 
    {
        if (choiceChoosen != choices.ResdPNR)
        {
            pinUIHandler3.imageDefault.SetActive(true);
            pinUIHandler3.imageOptionOne.SetActive(false);
            pinUIHandler3.imageOptionTwo.SetActive(false);
            pinUIHandler3.imageOptionThree.SetActive(true);
            choiceChoosen = choices.ResdPNR;
        }
            else { choiceChoosen = choices.Nothing; clear_pinUI(); }
    }

    public void Choose_Option() 
    {
        switch (choiceChoosen)
        {
            case choices.Nothing:
                break;
            case choices.BeachBNS:
                gameHandler.Pin_Visited();
                gameHandler.Beach_Choice1();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                
                break;
            case choices.BeachHNW:
                gameHandler.Pin_Visited();
                gameHandler.Beach_Choice2();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;
            case choices.BeachEP:
                gameHandler.Pin_Visited();
                gameHandler.Beach_Choice3();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;
            case choices.CommBNS:
                gameHandler.Pin_Visited();
                gameHandler.Comm_Choice1();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;
            case choices.CommHNW:
                gameHandler.Pin_Visited();
                gameHandler.Comm_Choice2();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;
            case choices.CommANC:
                gameHandler.Pin_Visited();
                gameHandler.Comm_Choice3();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;
            case choices.ResdHR:
                gameHandler.Pin_Visited();
                gameHandler.Resd_Choice1();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;
            case choices.ResdOTG:
                gameHandler.Pin_Visited();
                gameHandler.Resd_Choice2();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;
            case choices.ResdPNR:
                gameHandler.Pin_Visited();
                gameHandler.Resd_Choice3();
                News();
                gameHandler.pins[gameHandler.closestPin].visited = true;
                break;


        }
        Close_Pin();
        clear_pinUI();
        GameOver();
        choiceChoosen = choices.Nothing;
        
    }

    public void News()
    {
        notificationHandler.SendNotification("News", "", 2);
        int randNum;
        
        randNum = UnityEngine.Random.Range(1,2);

        switch (choiceChoosen)
        {
            case choices.Nothing:
                break;
            case choices.BeachBNS:
               if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Business leaders celebrate new planning permissions for tourist attractions on the coast: ‘With more ‘staycations’ there’s a big market for the traditional seaside holiday experience’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Public outcry at new planning permissions for tourist attractions on the coast: ‘Our unique natural heritage should not be sacrificed for the sake of profit’. ", 2);
                }
                break;
            case choices.BeachHNW:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Politicians announce new planning permissions for health spas on the coast: ‘Promoting new ‘lifestyle’ businesses is a key step in shifting beyond a seasonal economy’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Public outcry at new planning permissions for health retreats on the coast: ‘Supporting business and jobs is fine, but the seaside should be for everyone, not just people who are well off’. ", 2);
                }
                break;
            case choices.BeachEP:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Environmentalists celebrate the protection of natural heritage on the coast: ‘This is a big step towards preserving the wonder of this place for generations to come’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Business leaders criticise ‘blockers’ hampering economic development on the coast: ‘Excessive regulation means we’re wasting our most precious economic asset’. ", 2);
                }
                break;
            case choices.CommBNS:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Business leaders celebrate new planning permissions for tourist attractions in the town centre: ‘The traditional seaside holiday experience is key to growth and jobs here’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Public outcry at new planning permissions for tourist attractions in the town centre: ‘More seasonal tourism business means more pressure on housing and services. And do we really need more sweet shops, and amusement arcades? ", 2);
                }
                break;
            case choices.CommHNW:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Politicians announce new planning permissions for leisure complex in the town centre: ‘A sustainable year-round economy needs these new types of enterprise, like yoga studios and health food stores’ ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Public outcry at new planning permission for leisure complex in the town centre: ‘I can’t see yoga studios and health food shops providing many jobs for local people’. ", 2);
                }
                break;
            case choices.CommANC:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Business Leaders welcome new arts and cultural venues in the town centre: ‘This will increase visitor numbers, particularly in the evening, and support other local enterprises’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Political figures criticise arts investment: ‘It’s all very well for people to talk about culture, but public money should go into essential things – the arts is a luxury’. ", 2);
                }
                break;
            case choices.ResdHR:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Environmental groups celebrate planning permission for new high-rise apartment blocks: ‘This will make a big reduction on housing demand and protect green-belt land at the same time’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Public outcry at planning permission for new high-rise apartment blocks: ‘This will ruin the sea view that many people come here to enjoy’. ", 2);
                }
                break;
            case choices.ResdOTG:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Politicians announce new planning permissions for out-of-town housing developments: ‘These new communities will meet housing demand and help grow our economy’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Environmental groups condemn new planning permissions for out-of-town housing developments: ‘This is reckless destruction of the countryside and how many of these houses will really be affordable anyway?’ ", 2);
                }
                break;
            case choices.ResdPNR:
                if (randNum == 1)
                {
                    notificationHandler.SendNotification("News", "Business Leaders celebrate the announcement of a new Park & Ride scheme: ‘This will make it much easier for visitors to come into the town, boosting the economy and also supporting a healthy environment by reducing traffic congestion’. ", 2);
                }
                else
                {
                    notificationHandler.SendNotification("News", "Public outcry at the introduction of resident parking permits: ‘We’re happy for tourists to come in on a Park & Ride but why should we have to pay for it with these parking permits?’ ", 2);
                }
                break;


        }
    }

    void clear_pinUI()
    {
        pinUIHandler1.imageDefault.SetActive(true);
        pinUIHandler1.imageOptionOne.SetActive(false);
        pinUIHandler1.imageOptionTwo.SetActive(false);
        pinUIHandler1.imageOptionThree.SetActive(false);

        pinUIHandler2.imageDefault.SetActive(true);
        pinUIHandler2.imageOptionOne.SetActive(false);
        pinUIHandler2.imageOptionTwo.SetActive(false);
        pinUIHandler2.imageOptionThree.SetActive(false);

        pinUIHandler3.imageDefault.SetActive(true);
        pinUIHandler3.imageOptionOne.SetActive(false);
        pinUIHandler3.imageOptionTwo.SetActive(false);
        pinUIHandler3.imageOptionThree.SetActive(false);
    }

    public void Close_Pin()
    {
        pinUI.SetActive(false);
        pinUI2.SetActive(false);
        pinUI3.SetActive(false);
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
