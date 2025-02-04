using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour


{
    public float economyHealth = 0.4f;
    public float environmentHealth = 0.4f;
    public float housingDemand = 0.6f;

    public float decayRate = 0.01f;
    public float distanceThreshold = 0.01f;

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
        environmentHealth = Mathf.Clamp(environmentHealth - amount, 0.0f, 1.0f);
        housingDemand = Mathf.Clamp(housingDemand + amount, 0.0f, 1.0f);

        // Update slider
        economySlider.value = economyHealth;
        environmentSlider.value = environmentHealth;
        housingSlider.value = housingDemand;
    }
}
