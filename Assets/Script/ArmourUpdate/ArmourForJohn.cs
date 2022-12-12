using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;
public class ArmourForJohn : MonoBehaviour
{
    [SpineSlot]
    public string halmet;
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
    public string hammer;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string spear;
    [SpineSlot]
    public string R_weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string sword1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string axe1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string hammer1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string spear1;
    [SpineSlot]
    public string shield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string smallShield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string mediumShield;
    DatabaseManager db;
    Slot[] slots;
    Scene currentScene;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "John_Back" || this.name == "John_Front" || this.name == "John_Persp")
            slots = this.GetComponent<SkeletonAnimation>().skeleton.Slots.Items;
        else
            slots = this.GetComponent<SkeletonMecanim>().Skeleton.Slots.Items;
        db = FindObjectOfType<DatabaseManager>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
    private void Update()
    {
        UpdateJohnArmour();
    }
    public void UpdateJohnArmour()
    {
        if (sceneName == "BattleScene" || sceneName == "Battle Scene_Barghest  Lair" || sceneName == "Battle Scene_Brigand Lair" || sceneName == "Battle Scene_Castle" || sceneName == "Battle Scene_CobbleStone" || sceneName == "Battle Scene_Death Wight Lair" || sceneName == "BattleScene_Cave" || sceneName == "Motte and Baley Battle Scene")
        {
            if (Globals.inventoryJohn.Helmet== "LeatherHelmet")
            {
                if (this.name == "back")
                    ShowUpgrade(halmet, leatherHelmet, "leather cap back");
                else if (this.name == "front")
                    ShowUpgrade(halmet, leatherHelmet, "leather cap front");
                else if (this.name == "John_Back")
                    ShowUpgrade1(halmet, leatherHelmet, "leather cap back");
                else if (this.name == "John_Front")
                    ShowUpgrade1(halmet, leatherHelmet, "leather cap front");
                else if (this.name == "John_Persp")
                    ShowUpgrade1(halmet, leatherHelmet, "Leather Helmet");
                else
                    ShowUpgrade(halmet, leatherHelmet, "Leather Helmet");
            }
            else if (Globals.inventoryJohn.Helmet == "KettleHelmet" || Globals.inventoryJohn.Helmet == "NasalHelmet")
            {
                if (this.name == "back")
                    ShowUpgrade(halmet, metalHelmet, "metal helmet back");
                else if (this.name == "front")
                    ShowUpgrade(halmet, metalHelmet, "metal helmet copy");
                else if (this.name == "John_Back")
                    ShowUpgrade1(halmet, metalHelmet, "metal helmet back");
                else if (this.name == "John_Front")
                    ShowUpgrade1(halmet, metalHelmet, "metal helmet copy");
                else if (this.name == "John_Persp")
                    ShowUpgrade1(halmet, metalHelmet, "Metal Helmet");
                else
                    ShowUpgrade(halmet, leatherHelmet, "Metal Helmet");
            }
            else if (Globals.inventoryProtagnist.Helmet== "AvainTail" || Globals.inventoryProtagnist.Helmet == "MailCoif")
            {
                if (this.name == "back")
                    ShowUpgrade(halmet, chainmailHelmet, "chainmail helmet back");
                else if (this.name == "front")
                    ShowUpgrade(halmet, chainmailHelmet, "chain mail helmet");
                else if (this.name == "John_Back")
                    ShowUpgrade1(halmet, chainmailHelmet, "chainmail helmet back");
                else if (this.name == "John_Front")
                    ShowUpgrade1(halmet, chainmailHelmet, "chain mail helmet");
                else if (this.name == "John_Persp")
                    ShowUpgrade1(halmet, chainmailHelmet, "Chainmail Helmet");
                else
                    ShowUpgrade(halmet, chainmailHelmet, "Chainmail Helmet");
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
            if (Globals.inventoryJohn.WoodenBuckler == 1 || Globals.inventoryJohn.metalBuckler == 1 || Globals.inventoryJohn.WoodenSmallRound == 1 || Globals.inventoryJohn.metalSmallRound == 1)
            {
                if (this.name == "perR" || this.name == "perL")
                    ShowUpgrade(shield, smallShield, "small round shield persp");
                else if (this.name == "John_Persp")
                    ShowUpgrade1(shield, smallShield, "small round shield persp");

            }
            else if (Globals.inventoryJohn.WoodenMedium == 1 || Globals.inventoryJohn.metalMedium == 1)
            {
                if (this.name == "perR" || this.name == "perL")
                    ShowUpgrade(shield, mediumShield, "small round shield persp");
                else if (this.name == "John_Persp")
                    ShowUpgrade1(shield, mediumShield, "small round shield persp");
            }
            else
            {
                if (this.name == "perR" || this.name == "perL")
                    //ShowUpgrade(shield, smallShield, "small round shield persp");

                    foreach (Slot item in slots)
                    {
                        if (item.ToString().Contains("Shield"))
                        {
                            item.Attachment = null;
                            item.A = 0f;
                        }
                    }
            }
            if (Globals.inventoryJohn.WeaponAttack == "Dragger" || Globals.inventoryJohn.WeaponAttack == "ShortSword" || Globals.inventoryJohn.WeaponAttack == "longSword")
            {
                ShowUpgrade(weapon, sword, "warrior sword");
                ShowUpgrade(R_weapon, sword1, "warrior sword");
            }
            else if (Globals.inventoryJohn.WeaponAttack == "ShortAxe" || Globals.inventoryJohn.WeaponAttack == "longAxe")
            {
                ShowUpgrade(weapon, axe, "single side axe2");
                ShowUpgrade(R_weapon, axe1, "single side axe");
            }
            else if (Globals.inventoryJohn.WeaponAttack == "warHammer" )
            {
                ShowUpgrade(weapon, hammer, "Hammer");
                ShowUpgrade(R_weapon, hammer1, "Hammer");
            }
            else if(Globals.inventoryJohn.WeaponAttack == "Spear")
            {
                ShowUpgrade(weapon, spear, "Hammer");
                ShowUpgrade(R_weapon, spear1, "spear");
            }
            else
            {
                ShowUpgrade(weapon, sword, "warrior sword");
                ShowUpgrade(R_weapon, sword1, "warrior sword");
            }
            if (Globals.inventoryJohn.Armour == "Leahter" || Globals.inventoryJohn.Armour == "Hide" || Globals.inventoryJohn.Armour == "Brigadine")
            {
                if (this.name == "John_Back" || this.name == "John_Front" || this.name == "John_Persp")
                    SkinName("Leather Armor");
                else
                    SetSkin("Leather Armor");
            }
            else if (Globals.inventoryJohn.Armour == "padded" || Globals.inventoryJohn.Armour == "padded")
            {
                if (this.name == "John_Back" || this.name == "John_Front" || this.name == "John_Persp")
                    SkinName("Padded Armor");
                else
                    SetSkin("Padded Armor");
            }
            else if (Globals.inventoryJohn.Armour == "Chainmail" || Globals.inventoryJohn.Armour == "Scale")
            {
                if (this.name == "John_Back" || this.name == "John_Front" || this.name == "John_Persp")
                    SkinName("Chainmail Armor");
                else
                    SetSkin("Chainmail Armor");
            }
            else
                SetSkin("Normal");
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
    }
}
