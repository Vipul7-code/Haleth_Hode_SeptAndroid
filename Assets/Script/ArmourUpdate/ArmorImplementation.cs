using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;
public class ArmorImplementation : MonoBehaviour
{
    [SpineSlot]
    public string halmet;
    [SpineAttachment(currentSkinOnly: true, slotField: "Helmet")]
    public string hair;
    [SpineAttachment(currentSkinOnly: true, slotField: "Helmet")]
    public string leatherHelmet;
    [SpineAttachment(currentSkinOnly: true, slotField: "Helmet")]
    public string metalHelmet;
    [SpineAttachment(currentSkinOnly: true, slotField: "Helmet")]
    public string guardHelmet;
    [SpineAttachment(currentSkinOnly: true, slotField: "Helmet")]
    public string chainmailHelmet;
    [SpineSlot]
    public string weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string sword;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string axe;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string mace;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string hammer;
    [SpineSlot]
    public string weapon1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string sword1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string axe1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string mace1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string hammer1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string spear;
    [SpineSlot]
    public string arrow;
    [SpineAttachment(currentSkinOnly: true, slotField: "Arrow")]
    public string Arrow;
    [SpineSlot]
    public string L_Weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "L_Weapon")]
    public string bow;
    [SpineSlot]
    public string shield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string smallShield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string mediumShield;
    Slot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        Globals.armourImplimentation = this;
        slots = this.GetComponent<SkeletonAnimation>().skeleton.Slots.Items;
    }
    private void Update()
    {
        UpdateCharacter();
    }
    public void UpdateCharacter()
    {
        foreach (Slot item in slots)
        {
            if (item.ToString().Contains("Shield"))
            {
                item.Attachment = null;
                item.A = 0f;
            }
        }
        if(Globals.isGuard)
            SetSkin("Padded Armor");
        if (Globals.avatarState.AvatarName == "WarriorMale" || Globals.avatarState.AvatarName == "WarriorFemale")
        {
           // Debug.Log("here........");
            if (this.name == "Worror_Persp" || this.name == "Smith_F_Persp")
            {
                ShowUpgrade(weapon, sword, "warrior sword");
                ShowUpgrade(weapon1, sword1, "warrior sword");
            }
            else if (this.name == "Worrior_Back" || this.name== "Smith_F_Back")
                ShowUpgrade(weapon, sword, "Warrior Sword");
            foreach (Slot item in slots)
            {
                if (item.ToString().Contains("Shield"))
                {
                    item.Attachment = null;
                    item.A = 0f;
                }
            }

        }
        else if (Globals.avatarState.AvatarName == "ArcherMale" || Globals.avatarState.AvatarName == "ArcherFemale")
        {
            foreach (Slot item in slots)
            {
                if (item.ToString().Contains("Shield") || item.ToString().Contains("B_Weapon") || item.ToString().Contains("R_Weapon") || item.ToString().Contains("Arrow") || item.ToString().Contains("L_Weapon"))
                {
                    item.Attachment = null;
                    item.A = 0f;
                }
            }
        }
        else if (Globals.avatarState.AvatarName == "PriestMale" || Globals.avatarState.AvatarName == "PriestFemale")
        {
            if (this.name == "M_Acolyte_Back" || this.name == "M_Acolyte_Persp" || this.name == "F_Acolyte_Persp" || this.name== "F_Acolyte_Back")
                ShowUpgrade(weapon, sword, "warrior sword");
        }
    }
    public void SetSkin(string skinName)
    {
      this.GetComponent<SkeletonAnimation>().Skeleton.SetSkin(skinName);
    }

    public void ShowUpgrade(string slot, string attachment, string skinName)
    {
        GetComponent<SkeletonAnimation>().Skeleton.SetAttachment(slot, attachment);
    }
    public void SkeletonSlot(string _spine)
    {
        if (_spine != null)
        {
            Spine.Slot slot = GetComponent<SkeletonAnimation>().Skeleton.FindSlot(_spine);
            if (slot != null)
            {
                slot.Attachment = null;
            }
        }
    }
}
