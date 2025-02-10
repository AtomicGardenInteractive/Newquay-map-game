using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Pin
{
    public string name;
    public GPSLoc pos;
}

public class GameHandler : MonoBehaviour
{
    public float economyHealth = 0.4f;
    public float environmentHealth = 0.4f;
    public float housingDemand = 0.6f;

    public float decayRate = 0.01f;
    public float distanceThreshold = 0.01f;

    public Pin[] pins;
    public int closestPin;
    public double closestDist;

    Slider economySlider;
    Slider environmentSlider;
    Slider housingSlider;
    LocationStuff locationHandler;

    public int numPinsVisited;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        economySlider = GameObject.Find("EconomySlider").GetComponent<Slider>();
        environmentSlider = GameObject.Find("EnvironmentSlider").GetComponent<Slider>();
        housingSlider = GameObject.Find("HousingSlider").GetComponent<Slider>();
        locationHandler = GameObject.Find("LocationHandler").GetComponent<LocationStuff>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ClosestDIST   " + closestDist);
        // Update values
        float amount = Time.deltaTime * decayRate;
        economyHealth = Mathf.Clamp(economyHealth - amount, 0.0f, 1.0f);
        environmentHealth = Mathf.Clamp(environmentHealth + amount, 0.0f, 1.0f);
        housingDemand = Mathf.Clamp(housingDemand + amount, 0.0f, 1.0f);

        // Update slider
        economySlider.value = economyHealth;
        environmentSlider.value = environmentHealth;
        housingSlider.value = housingDemand;

        // Find closest pin
        this.closestDist = Double.PositiveInfinity;
        for (int i = 0; i < pins.Length; i++)
        {
            GPSLoc p = this.locationHandler.currLoc;
            double d = locationHandler.distance(p.lat, p.lon, this.pins[i].pos.lat, this.pins[i].pos.lon, this.locationHandler.unit);
            if (d < this.closestDist)
            {
                this.closestDist = d;
                this.closestPin = i;
            }
        }

        if (numPinsVisited == 8)
        {
            gameOverScreen.SetActive(true); 
        }
    }

    public void economy_increase() 
    {
        economyHealth = economyHealth + 0.01f;
    }

    public void economy_decrease()
    {
        economyHealth = economyHealth - 0.01f;
    }

    public void enviroment_decrease() 
    {
        environmentHealth = environmentHealth - 0.01f;
    }

    public void enviroment_increase()
    {
        environmentHealth = environmentHealth + 0.01f;
    }

    public void housing_increase()
    {
        housingDemand = housingDemand + 0.01f;
    }
    public void housing_decrease()
    {
        housingDemand = housingDemand - 0.01f;
    }

    public void Pin_Visited()
    {
        numPinsVisited = numPinsVisited + 1;
    }
    }

