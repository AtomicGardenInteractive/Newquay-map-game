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

public class GPSPIN : MonoBehaviour
{
        public string pinName;
        public GPSLoc pos;
        public enum PinType { Beach, Commercial, Residential}
        public PinType pinType;
    //Component refrences
    private Image mainIcon;
    private Image thoughtIcon;
    private GameObject thoughtBubble;
    private Button debugButton;

    public GameObject iconGameObject;
    public GameObject thoughtIconGameObject;

    public EmojiImages[] emojiImages; 

    // Start is called before the first frame update
    void Start()
    {
        //set images
        mainIcon = iconGameObject.GetComponent<Image>();
        thoughtIcon = thoughtIconGameObject.GetComponent<Image>();

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
