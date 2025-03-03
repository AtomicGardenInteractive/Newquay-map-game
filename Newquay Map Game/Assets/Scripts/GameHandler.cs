using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public float economyHealth = 0.4f;
    public float environmentHealth = 0.4f;
    public float housingDemand = 0.6f;

    public float decayRate = 0.01f;
    public float distanceThreshold = 0.01f;

    int numBeachBNS = 1 ;
    int numBeachHNW = 1 ;
    int numEP = 1 ;
    int numTownBNS = 1 ;
    int numTownHNW = 1 ;
    int numTownAC = 1 ;
    public GameObject parkNRide;


    public GameObject pinHolder;
    public GPSPIN[] pins;
    public int closestPin;
    public double closestDist;

    Slider economySlider;
    Slider environmentSlider;
    Slider housingSlider;
    GPSHandler locationHandler;

    public int numPinsVisited;
    public int scorePinsVisited;

    public TMP_Text econText;
    public TMP_Text enviroText;
    public TMP_Text houseText;
    public TMP_Text choicesText;

    TMP_Text distDebug;

    // Start is called before the first frame update
    void Start()
    {
        economySlider = GameObject.Find("EconomySlider").GetComponent<Slider>();
        environmentSlider = GameObject.Find("EnvironmentSlider").GetComponent<Slider>();
        housingSlider = GameObject.Find("HousingSlider").GetComponent<Slider>();
        locationHandler = GameObject.Find("GPSHandler").GetComponent<GPSHandler>();
        pins = pinHolder.GetComponentsInChildren<GPSPIN>();

        distDebug = GameObject.Find("Canvas/Home/DistDebug").GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // Update values
        float amount = Time.deltaTime * decayRate;
        economyHealth = Mathf.Clamp(economyHealth - amount, 0.0f, 1.0f);
        environmentHealth = Mathf.Clamp(environmentHealth + amount, 0.0f, 1.0f);
        housingDemand = Mathf.Clamp(housingDemand + amount, 0.0f, 1.0f);

        econText.text = (economyHealth * 100).ToString("00.00\\%");
        enviroText.text = (environmentHealth * 100).ToString("00.00\\%");
        houseText.text = (housingDemand * 100).ToString("00.00\\%");

        // Update slider
        economySlider.value = economyHealth;
        environmentSlider.value = environmentHealth;
        housingSlider.value = housingDemand;

        Debug.Log("Phone at " + this.locationHandler.currLoc.lat + ", " + this.locationHandler.currLoc.lon);

        // Find closest pin
        this.closestDist = Double.PositiveInfinity;
        distDebug.text = "";
        for (int i = 0; i < pins.Length; i++)
        {
            GPSLoc p = this.locationHandler.currLoc;
            double d = locationHandler.distance(p.lat, p.lon, this.pins[i].pos.lat, this.pins[i].pos.lon, this.locationHandler.unit);
            //Debug.Log("Pin " + i + " at " + this.pins[i].pos.lat + ", " + this.pins[i].pos.lon + " dist = " + d);
            if (d < this.closestDist)
            {
                this.closestDist = d;
                this.closestPin = i;
            }

            distDebug.text += "\n" + this.pins[i].pinName + ": " + d;
        }
        distDebug.text += "\nClosest pin: " + this.pins[this.closestPin].pinName;

        //            "\nLocation: \nLat: " + Input.location.lastData.latitude
        //+ " \nLon: " + Input.location.lastData.longitude
        //+ " \nAlt: " + Input.location.lastData.altitude
        //+ " \nH_Acc: " + Input.location.lastData.horizontalAccuracy
        //+ " \nTime: " + Input.location.lastData.timestamp;
    }

    public void economy_increase() 
    {
        economyHealth = economyHealth + 0.05f;
    }

    public void economy_decrease()
    {
        economyHealth = economyHealth - 0.02f;
    }

    public void enviroment_decrease() 
    {
        environmentHealth = environmentHealth - 0.03f;
    }

    public void enviroment_increase()
    {
        environmentHealth = environmentHealth + 0.03f;
    }

    public void housing_increase()
    {
        housingDemand = housingDemand + 0.03f;
    }
    public void housing_decrease()
    {
        housingDemand = housingDemand - 0.05f;
    }

    public void Beach_Choice1()
    {
        economyHealth = economyHealth + 0.1f;
        environmentHealth = environmentHealth - 0.06f;
        housingDemand = housingDemand + 0.03f;
        numBeachBNS = numBeachBNS + 1;
        pins[closestPin].Change_Icon(3);
    }

    public void Beach_Choice2()
    {
        economyHealth = economyHealth + 0.05f;
        environmentHealth = environmentHealth - 0.03f;
        housingDemand = housingDemand + 0.03f;
        numBeachHNW = numBeachHNW + 1;
        pins[closestPin].Change_Icon(10);
    }

    public void Beach_Choice3()
    {
        environmentHealth = environmentHealth + 0.06f;
        numEP = numEP + 1;
        pins[closestPin].Change_Icon(7);
    }

    public void Comm_Choice1()
    {
        economyHealth = economyHealth + (0.05f * numBeachBNS * numTownAC) / (numEP * 2) / (numTownBNS);
        housingDemand = housingDemand + 0.03f;
        numTownBNS = numTownBNS + 1;
        pins[closestPin].Change_Icon(3);
    }
    public void Comm_Choice2()
    {
        economyHealth = economyHealth + (0.05f * numBeachHNW * numTownAC) / (numEP * 2) / (numTownHNW);
        housingDemand = housingDemand + 0.03f;
        numTownHNW = numTownHNW + 1;
        pins[closestPin].Change_Icon(10);
    }
    public void Comm_Choice3()
    {
        economyHealth = economyHealth + 0.01f;
        housingDemand = housingDemand + 0.01f;
        numTownAC = Mathf.Clamp(numTownAC + 1 ,0, 2);
        pins[closestPin].Change_Icon(5);
    }

    public void Resd_Choice1()
    {
        housingDemand = housingDemand - 0.15f;
        pins[closestPin].Change_Icon(8);
    }
    public void Resd_Choice2()
    {
        housingDemand = housingDemand - 0.07f;
        environmentHealth = environmentHealth - 0.06f;
        pins[closestPin].Change_Icon(6);
    }
    public void Resd_Choice3()
    {
        environmentHealth = environmentHealth + 0.1f;
        parkNRide.SetActive(false);
    }

    public void Pin_Visited()
    {
        numPinsVisited = numPinsVisited + 1;
        choicesText.text = numPinsVisited.ToString("#" + "/8");
    }

    }

