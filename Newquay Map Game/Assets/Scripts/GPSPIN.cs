using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class EmojiImages
{
    public string imageName;
    public Sprite emojiImage;
}

[Serializable]
public class GPSPIN : MonoBehaviour
{
    public string pinName;
    public GPSLoc pos;
    public enum PinType { Beach, Commercial, Residential }
    public PinType pinType;
    public GameObject youAreHere;

    //Component refrences
    private Image mainIcon;
    public Button debugButton;

    public GameObject iconGameObject;

    public EmojiImages[] emojiImages;

    public bool visited;

    // Start is called before the first frame update
    void Start()
    {
        //debugButton = GetComponentInChildren<Button>();
        //set images
        mainIcon = iconGameObject.GetComponent<Image>();

        switch (pinType)
        {
            case PinType.Beach:
                mainIcon.sprite = emojiImages[0].emojiImage;
                break;
            case PinType.Commercial:
                mainIcon.sprite = emojiImages[1].emojiImage;
                break;
            case PinType.Residential:
                mainIcon.sprite = emojiImages[2].emojiImage;
                break;
        }

        //set interactivity


    }

    public void Change_Icon(int num)
    {
        mainIcon.sprite = emojiImages[num].emojiImage;
    }

    public void disable_button()
    {
        debugButton.interactable = false;
    }
}
