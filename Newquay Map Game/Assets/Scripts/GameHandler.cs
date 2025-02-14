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

        econText.text = (economyHealth).ToString();
        enviroText.text = environmentHealth.ToString();
        houseText.text = housingDemand.ToString();

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

    public void Pin_Visited()
    {
        numPinsVisited = numPinsVisited + 1;
    }
    }

