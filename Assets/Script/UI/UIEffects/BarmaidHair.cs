using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class BarmaidHair : MonoBehaviour
{
    [SpineSlot]
    public string hair;
    [SpineAttachment(currentSkinOnly: true, slotField: "hairs")]
    public string blondeHair;
    [SpineAttachment(currentSkinOnly: true, slotField: "hairs")]
    public string darkHair;
    [SpineAttachment(currentSkinOnly: true, slotField: "hairs")]
    public string redHair;
    [SpineSlot]
    public string hairs;
    [SpineAttachment(currentSkinOnly: true, slotField: "Hairs")]
    public string blonde_Hair;
    [SpineAttachment(currentSkinOnly: true, slotField: "Hairs")]
    public string dark_Hair;
    [SpineAttachment(currentSkinOnly: true, slotField: "Hairs")]
    public string red_Hair;
    // Start is called before the first frame update
    void Start()
    {
        if(this.transform.parent.name== "GuardBarmaidRed" || this.transform.parent.name== "GuardBarmaidRed(Clone)1")
        {
            if(this.name== "perR")
                ShowUpgrade(hair, redHair, "Red");
            else if(this.name== "back" || this.name== "front")
                ShowUpgrade(hairs, red_Hair, "Red");
        }
        else if (this.transform.parent.name == "GuardBarmaidBlonde(Clone)" || this.transform.parent.name== "GuardBarmaidBlonde(Clone)2")
        {
            if (this.name == "perR")
                ShowUpgrade(hair, blondeHair, "Blonde");
            else if (this.name == "back" || this.name == "front")
                ShowUpgrade(hairs, blonde_Hair, "Blonde");
        }
        else if (this.transform.parent.name == "GuardBarmaidDark(Clone)" || this.transform.parent.name== "GuardBarmaidDark(Clone)0")
        {
            if (this.name == "perR")
                ShowUpgrade(hair, darkHair, "Dark");
            else if (this.name == "back" || this.name == "front")
                ShowUpgrade(hairs, dark_Hair, "Dark");
        }

    }
    public void ShowUpgrade(string slot, string attachment, string skinName)
    {
        this.GetComponent<SkeletonMecanim>().Skeleton.SetAttachment(slot, attachment);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
