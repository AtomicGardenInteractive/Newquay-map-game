using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject Home;
    public GameObject Pin;
    public GameObject TutorialOverlay;
    public GameObject TutorialPin;

    // Start is called before the first frame update
    void Start()
    {
        Home = GameObject.Find("Home");
        Pin = GameObject.Find("Pin");
        TutorialOverlay = GameObject.Find("TutorialOverlay");
        TutorialPin = GameObject.Find("TutorialPin");

        Pin.SetActive(false);
        TutorialOverlay.SetActive(false);
        TutorialPin.SetActive(false);
    }

    public void Toggle_Tutor() 
    {
        Debug.Log("Toggle_Tutor Called");
        TutorialOverlay.SetActive(!TutorialOverlay.activeSelf);
    }
     public void Toggle_Tutor_Pin() 
    { 
        TutorialPin.SetActive(!TutorialPin.activeSelf);
    }
    public void Toggle_Pin() 
    { 
    
    }
}
