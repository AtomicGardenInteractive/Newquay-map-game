using UnityEngine;
using UnityEngine.UI;
using System;

public class GameHandler : MonoBehaviour


{
    public float economyHealth = 0.4f;
    public float environmentHealth = 0.4f;
    public float housingDemand = 0.6f;

    public float decayRate = 0.01f;
    public float distanceThreshold = 0.01f;

    public Pos[] pins;
    public int closestPin;
    public double closestDist;

    Pos currentPos;

    Slider economySlider;
    Slider environmentSlider;
    Slider housingSlider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        economySlider = GameObject.Find("EconomySlider").GetComponent<Slider>();
        environmentSlider = GameObject.Find("EnvironmentSlider").GetComponent<Slider>();
        housingSlider = GameObject.Find("HousingSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update values
        float amount = Time.deltaTime * decayRate;
        economyHealth = Mathf.Clamp(economyHealth - amount, 0.0f, 1.0f);
        environmentHealth = Mathf.Clamp(environmentHealth + amount, 0.0f, 1.0f);
        housingDemand = Mathf.Clamp(housingDemand + amount, 0.0f, 1.0f);

        // Update slider
        economySlider.value = economyHealth;
        environmentSlider.value = environmentHealth;
        housingSlider.value = housingDemand;

        // Update position
        this.currentPos.lat = Input.location.lastData.latitude;
        this.currentPos.lon = Input.location.lastData.longitude;

        // Find closest pin
        this.closestDist = Double.PositiveInfinity;
        for (int i = 0; i < pins.Length; i++)
        {
            double d = this.currentPos.Distance(this.pins[i]);
            if (d < this.closestDist)
            {
                this.closestDist = d;
                this.closestPin = i;
            }
        }
    }

    public void economy_increase() 
    {
        economyHealth = economyHealth + 0.1f;
    }

    public void enviroment_decrease() 
    {
        environmentHealth = environmentHealth - 0.1f;
    }
}

[Serializable]
public class Pos
{
    public string name;
    public double lon;
    public double lat;

    public Pos(double lon, double lat)
    {
        this.lon = lon;
        this.lat = lat;
    }

    public double Distance(Pos to)
    {
        double r = 6371;
        double p = Math.PI / 180;
        double a = 0.5 - Math.Cos((to.lat - this.lat) * p) / 2
                      + Math.Cos(this.lat * p) * Math.Cos(to.lat * p) *
                        (1 - Math.Cos((to.lon - this.lon) * p)) / 2;
        return 2 * r * Math.Asin(Math.Sqrt(a));
    }
}