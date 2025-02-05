using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject Home;
    public GameObject PinUI;
    public GameObject TutorialOverlay;
    public GameObject TutorialPin;
    public GameObject ActivatePinButton;

    // Start is called before the first frame update
    void Start()
    {
        Home = GameObject.Find("Home");
        PinUI = GameObject.Find("PinUI");
        TutorialOverlay = GameObject.Find("TutorialOverlay");
        TutorialPin = GameObject.Find("TutorialPin");
        ActivatePinButton = GameObject.Find("ActivatePinButton");

        PinUI.SetActive(false);
        TutorialOverlay.SetActive(false);
        TutorialPin.SetActive(false);
        //ActivatePinButton.SetActive(false);
    }

    public void Toggle_Tutor() 
    {
        Debug.Log("Toggle_Tutor Called");
        TutorialOverlay.SetActive(!TutorialOverlay.activeSelf);
    }
     public void Toggle_Tutor_Pin() 
    {
        TutorialOverlay.SetActive(!TutorialOverlay.activeSelf);
        TutorialPin.SetActive(!TutorialPin.activeSelf);
    }
    public void Toggle_Pin() 
    { 
        PinUI.SetActive(!PinUI.activeSelf);
    }
}
