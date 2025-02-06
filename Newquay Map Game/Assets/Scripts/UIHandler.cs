using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIHandler : MonoBehaviour
{
    public GameObject Home;
    public GameObject PinUI;
    public GameObject TutorialOverlay;
    public GameObject TutorialPin;
    public Button ActivatePinButton;
    public GameHandler GameHandler;
    public float MinDistToPin;
    TMP_Text ActiveButtonText;

    // Start is called before the first frame update
    void Start()
    {
        Home = GameObject.Find("Home");
        PinUI = GameObject.Find("PinUI");
        TutorialOverlay = GameObject.Find("TutorialOverlay");
        TutorialPin = GameObject.Find("TutorialPin");

        ActiveButtonText = ActivatePinButton.GetComponentInChildren<TextMeshPro>();

        PinUI.SetActive(false);
        TutorialOverlay.SetActive(false);
        TutorialPin.SetActive(false);
        
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
