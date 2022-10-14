using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;
public class ArmourForTucker : MonoBehaviour
{
    [SpineSlot]
    public string halmet;
    [SpineAttachment(currentSkinOnly: true, slotField: "Helmet")]
    public string leatherHelmet;
    [SpineAttachment(currentSkinOnly: true, slotField: "Helmet")]
    public string metalHelmet;
    [SpineSlot]
    public string B_weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string sword;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string mace;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string hammer;
    [SpineSlot]
    public string R_weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string sword1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string mace1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string hammer1;
    [SpineSlot]
    public string shield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string smallShield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string mediumShield;
    Scene currentScene;
    string sceneName;
    Slot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "Tucker_Back"||this.name== "Tucker_Front" || this.name== "Tucker_Persp")
            slots = this.GetComponent<SkeletonAnimation>().skeleton.Slots.Items;
        else
            slots = this.GetComponent<SkeletonMecanim>().skeleton.Slots.Items;
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
    private void Update()
    {
        UpdateForTucker();
    }

    void UpdateForTucker()
    {
        if (sceneName == "BattleScene" || sceneName == "Battle Scene_Barghest  Lair" || sceneName == "Battle Scene_Brigand Lair" || sceneName == "Battle Scene_Castle" || sceneName == "Battle Scene_CobbleStone" || sceneName == "Battle Scene_Death Wight Lair" || sceneName == "BattleScene_Cave" || sceneName == "Motte and Baley Battle Scene")
        {
            if (Globals.inventoryTucker.Armour == "Leahter" || Globals.inventoryTucker.Armour == "Hide")
            {
                if (this.name == "Tucker_Back" || this.name == "Tucker_Front" || this.name == "Tucker_Persp")
                    SkinName("Leather Armor");
                else
                    SetSkin("Leather Armor");
            }
            else if (Globals.inventoryTucker.Armour == "padded")
            {
                if (this.name == "Tucker_Back" || this.name == "Tucker_Front" || this.name == "Tucker_Persp")
                    SkinName("Padded Armor");
                else
                    SetSkin("Padded Armor");
            }
            else
                SetSkin("Padded Armor");
            if (Globals.inventoryTucker.Helmet == "LeatherHelmet")
            {
                if (this.name == "Back")
                    ShowUpgrade(halmet, leatherHelmet, "leather cap with haior");
                else if (this.name == "Front")
                    ShowUpgrade(halmet, leatherHelmet, "leather cap with hairs");
                else if (this.name == "Tucker_Back")
                    ShowUpgrade1(halmet, leatherHelmet, "leather cap with haior");
                else if (this.name == "Tucker_Front")
                    ShowUpgrade1(halmet, leatherHelmet, "leather cap with hairs");
                else if (this.name == "Tucker_Persp")
                    ShowUpgrade1(halmet, leatherHelmet, "leather cap with hair");
                else
                    ShowUpgrade(halmet, leatherHelmet, "leather cap with hair");
            }
            else if (Globals.inventoryTucker.Helmet == "KettleHelmet")
            {
                if (this.name == "Back")
                    ShowUpgrade(halmet, leatherHelmet, "metal helmet with hair");
                else if (this.name == "Front")
                    ShowUpgrade(halmet, leatherHelmet, "metal helmet with hairs");
                else if (this.name == "Tucker_Back")
                    ShowUpgrade1(halmet, leatherHelmet, "metal helmet with hair");
                else if (this.name == "Tucker_Front")
                    ShowUpgrade1(halmet, leatherHelmet, "metal helmet with hairs");
                else if (this.name == "Tucker_Persp")
                    ShowUpgrade1(halmet, leatherHelmet, "metal helmet wit hair");
                else
                    ShowUpgrade(halmet, leatherHelmet, "metal helmet wit hair");
            }
            else
            {
                foreach (Slot item in slots)
                {
                    if (item.ToString().Contains("Helmet"))
                    {
                        item.Attachment = null;
                        item.A = 0f;
                    }
                }
            }
            if (Globals.inventoryTucker.WeaponAttack == "Dragger")
            {
                ShowUpgrade(B_weapon, sword, "warrior sword2");
                ShowUpgrade(R_weapon, sword1, "warrior sword");
                    
            }
            else if (Globals.inventoryTucker.WeaponAttack == "Mace" || Globals.inventoryTucker.WeaponAttack == "Flair")
            {
                ShowUpgrade(B_weapon, mace, "Mace2");
                ShowUpgrade(R_weapon, mace1, "Mace");
            }
            else if (Globals.inventoryTucker.WeaponAttack == "warHammer" || Globals.inventoryTucker.WeaponAttack == "Maul")
            {
                ShowUpgrade(B_weapon, hammer, "Hammer2");
                ShowUpgrade(R_weapon, hammer1, "Hammer");

            }
            else
            {
                ShowUpgrade(B_weapon, mace, "Mace2");
                ShowUpgrade(R_weapon, mace1, "Mace");
            }
            if (Globals.inventoryTucker.MetalBuckler == 1 || Globals.inventoryTucker.MetalSmall == 1 || Globals.inventoryTucker.WoodenBuckler == 1 || Globals.inventoryTucker.WoodenSmall == 1)
            {
                if (this.name == "perR" || this.name == "perL")
                    ShowUpgrade(shield, smallShield, "small round shield persp");
                else if (this.name == "Tucker_Persp")
                    ShowUpgrade1(shield, smallShield, "small round shield persp");
            }
            else if (Globals.inventoryTucker.MetalMedium == 1)
            {
                if (this.name == "perR" || this.name == "perL")
                    ShowUpgrade(shield, mediumShield, "medium round shield side");
                else if (this.name == "Tucker_Persp")
                    ShowUpgrade1(shield, mediumShield, "medium round shield side");
            }
            else
            {
                foreach (Slot item in slots)
                {
                    if (item.ToString().Contains("Shield"))
                    {
                        item.Attachment = null;
                        item.A = 0f;
                    }
                }
            }
            //if (Globals.inventoryTucker.Armour == "Leahter" || Globals.inventoryTucker.Armour == "Hide")
            //{
            //    if (this.name == "Tucker_Back" || this.name == "Tucker_Front" || this.name == "Tucker_Persp")
            //        SkinName("Leather Armor");
            //    else
            //        SetSkin("Leather Armor");
            //}
            //else if (Globals.inventoryTucker.Armour == "padded")
            //{
            //    if (this.name == "Tucker_Back" || this.name == "Tucker_Front" || this.name == "Tucker_Persp")
            //        SkinName("Padded Armor");
            //    else
            //        SetSkin("Padded Armor");
            //}
            //else
            //    SetSkin("Padded Armor");
        }
    }
    public void ShowUpgrade(string slot, string attachment, string skinName)
    {
        this.GetComponent<SkeletonMecanim>().Skeleton.SetAttachment(slot, attachment);
    }
    void ShowUpgrade1(string slot,string attachment,string skinName)
    {
        this.GetComponent<SkeletonAnimation>().Skeleton.SetAttachment(slot, attachment);
    }
    void SkinName(string skinName)
    {
        this.GetComponent<SkeletonAnimation>().Skeleton.SetSkin(skinName);
    }
    public void SetSkin(string skinName)
    {
        this.GetComponent<SkeletonMecanim>().Skeleton.SetSkin(skinName);
        this.GetComponent<SkeletonMecanim>().Skeleton.SetSlotsToSetupPose();
    }
}
