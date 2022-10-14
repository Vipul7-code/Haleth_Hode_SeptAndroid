using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;
public class ArmourForArcher : MonoBehaviour
{
    [SpineSlot]
    public string Arrow;
    [SpineAttachment(currentSkinOnly: true, slotField: "Arrow")]
    public string bow;
    Slot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ArmourForArcher");
        slots = this.GetComponent<SkeletonAnimation>().skeleton.Slots.Items;
        if (Globals.avatarState.AvatarName == "PriestMale" || Globals.avatarState.AvatarName == "PriestFemale")
        {
            foreach (Slot item in slots)
            {
                if (item.ToString().Contains("Shield"))
                {
                    Debug.Log("for priest");
                    item.Attachment = null;
                    item.A = 0f;
                }
            }
        }
        else if(Globals.avatarState.AvatarName == "ArcherMale" || Globals.avatarState.AvatarName == "ArcherFemale")
        {
            Debug.Log("for archer___________________________________________________________________________________________");
            foreach (Slot item in slots)
            {
               if (item.ToString().Contains("Arrow"))
                {
                    Debug.Log("for archer");
                    item.Attachment = null;
                   item.A = 0f;
                }

                if (item.ToString().Contains("Shield"))
                {
                    Debug.Log("for archer");
                    item.Attachment = null;
                    item.A = 0f;
                }
            }
        }
    }

    public void ShowUpgrade(string slot, string attachment, string skinName)
    {
        this.GetComponent<SkeletonMecanim>().Skeleton.SetAttachment(slot, attachment);
    }
}
