using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;
public class ArmourForMarium : MonoBehaviour
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
    public string B_weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string sword;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string axe;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string hammer;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string mace;

    [SpineSlot]
    public string weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "Weapon")]
    public string sword1;
    [SpineAttachment(currentSkinOnly: true, slotField: "Weapon")]
    public string axe1;
    [SpineAttachment(currentSkinOnly: true, slotField: "Weapon")]
    public string mace1;
    [SpineAttachment(currentSkinOnly: true, slotField: "Weapon")]
    public string spear1;

    [SpineSlot]
    public string R_weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string sword2;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string axe2;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string mace2;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string spear2;

    [SpineSlot]
    public string shield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string smallShield;
    [SpineSlot]
    public string Arrow;
    [SpineAttachment(currentSkinOnly: true, slotField: "Arrow")]
    public string bow;


    DatabaseManager db;
    string wName;
    Slot[] slots;
    Scene currentScene;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "Mariam_Front" || this.name == "Mariam_Back" || this.name == "Mariam_Persp")
            slots = this.GetComponent<SkeletonAnimation>().skeleton.Slots.Items;
        else
            slots = this.GetComponent<SkeletonMecanim>().Skeleton.Slots.Items;
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
    private void Update()
    {
        UpdateMariumArmour();
    }

    public void UpdateMariumArmour()
    {
        if (sceneName == "BattleScene" || sceneName == "Battle Scene_Barghest  Lair" || sceneName == "Battle Scene_Brigand Lair" || sceneName == "Battle Scene_Castle" || sceneName == "Battle Scene_CobbleStone" || sceneName == "Battle Scene_Death Wight Lair" || sceneName == "BattleScene_Cave" || sceneName == "Motte and Baley Battle Scene")
        {
            if (Globals.inventoryMarium.Helmet == "LeatherHelmet")
            {
                if (this.name == "Back")
                    ShowUpgrade(halmet, leatherHelmet, "leather cap back");
                else if (this.name == "front")
                    ShowUpgrade(halmet, leatherHelmet, "leather cap front");
                else if (this.name == "Mariam_Back")
                    ShowUpgrade1(halmet, leatherHelmet, "leather cap back");
                else if (this.name == "Mariam_Front")
                    ShowUpgrade1(halmet, leatherHelmet, "leather cap front");
                else if (this.name == "Mariam_Persp")
                    ShowUpgrade1(halmet, leatherHelmet, "leather capback persp");
                else
                    ShowUpgrade(halmet, leatherHelmet, "leather capback persp");
            }
            else if (Globals.inventoryMarium.Helmet == "KettleHelmet" || Globals.inventoryMarium.Helmet == "NasalHelmet")
            {
                if (this.name == "Back")
                    ShowUpgrade(halmet, leatherHelmet, "metal helmet back");
                else if (this.name == "front")
                    ShowUpgrade(halmet, leatherHelmet, "metal helmet front");
                else if (this.name == "Mariam_Back")
                    ShowUpgrade1(halmet, leatherHelmet, "metal helmet back");
                else if (this.name == "Mariam_Front")
                    ShowUpgrade1(halmet, leatherHelmet, "metal helmet front");
                else if (this.name == "Mariam_Persp")
                    ShowUpgrade1(halmet, leatherHelmet, "metal helmet persp");
                else
                    ShowUpgrade(halmet, leatherHelmet, "metal helmet persp");
            }
            else
            {
                if (this.name == "Mariam_Back")
                    ShowUpgrade1(halmet, hair, "back hairstyle");
                else if (this.name == "Back")
                    ShowUpgrade(halmet, hair, "back hairstyle");
                else if (this.name == "Mariam_Front")
                    ShowUpgrade1(halmet, hair, "front hairstyle");
                else if (this.name == "front")
                    ShowUpgrade(halmet, hair, "front hairstyle");
            }
            if (Globals.inventoryMarium.WoodenBuckler == 1 || Globals.inventoryMarium.MetalBuckler == 1 || Globals.inventoryMarium.woodenSmall == 1 || Globals.inventoryMarium.MetalSmall == 1)
            {
                if (this.name == "perR" || this.name == "perL")
                    ShowUpgrade(shield, smallShield, "small round shield persp");
                else if (this.name == "Mariam_Persp")
                    ShowUpgrade1(shield, smallShield, "small round shield persp");
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
            //  Debug.Log("dragger::" + Globals.inventoryMarium.Dragger + "  sword::" + Globals.inventoryMarium.ShortSword + "  shortAxe::" + Globals.inventoryMarium.ShortAxe + "Warhammer::" + Globals.inventoryMarium.Warhammer + "  spear::" + Globals.inventoryMarium.Spear+" short bow::"+Globals.inventoryMarium.ShortBow+"  long bow::"+Globals.inventoryMarium.LongBow);
            if (Globals.inventoryMarium.WeaponAttack == "Dragger" || Globals.inventoryMarium.WeaponAttack == "ShortSword")
            {
                ShowUpgrade(B_weapon, sword, "warrior sword");
                ShowUpgrade(R_weapon, sword2, "Warrior Sword");
            }
            else if (Globals.inventoryMarium.WeaponAttack == "ShortAxe")
            {
               // ShowUpgrade(B_weapon, axe, "single side axe");
                ShowUpgrade(R_weapon, axe2, "single side axe");
            }
            else if (Globals.inventoryMarium.WeaponAttack == "warHammer")
            {
                ShowUpgrade(B_weapon, mace, "Mace");
                ShowUpgrade(R_weapon, mace2, "Mace");
                    
            }
            else if(Globals.inventoryMarium.WeaponAttack == "Spear")
            {
                //ShowUpgrade(B_weapon, spear1, "spear");
                ShowUpgrade(R_weapon, spear2, "spear");
                Debug.Log("spear----------------------------");
                foreach (Slot item in slots)
                {
                    if (item.ToString().Contains("R_weapon"))
                    {
                        item.Attachment = null;
                        item.A = 0f;
                    }
                }
            }
            else if (Globals.inventoryMarium.WeaponAttack == "shortBow" || Globals.inventoryMarium.WeaponAttack == "longBow")
            {
                ShowUpgrade(Arrow, bow, "Arrow");
                foreach (Slot item in slots)
                {
                    if (item.ToString().Contains("Shield"))
                    {
                        item.Attachment = null;
                        item.A = 0f;
                    }
                }
            }
            else
            {
                if (this.name == "perR" || this.name == "perL")
                {
                    wName = "R_Weapon";
                    ShowUpgrade(Arrow, bow, "Arrow");
                }
                else if (this.name == "Mariam_Persp")
                {
                    wName = "R_Weapon";
                    ShowUpgrade1(Arrow, bow, "Arrow");
                }
                else if (this.name == "front" || this.name == "Mariam_Front")
                    wName = "B_Weapon";
                else if (this.name == "Back" || this.name == "Mariam_Back")
                    wName = "Weapon";
                foreach (Slot item in slots)
                {
                    if (item.ToString().Contains("B_Weapon"))
                    {
                        item.Attachment = null;
                        item.A = 0f;
                    }
                }
            }
            if (Globals.inventoryMarium.Armour == "Leahter" || Globals.inventoryMarium.Armour == "Hide" || Globals.inventoryMarium.Armour == "Brigadine")
            {
                if (this.name == "Mariam_Front" || this.name == "Mariam_Back" || this.name == "Mariam_Persp")
                    SkinName("Leather Armor");
                else
                    SetSkin("Leather Armor");
            }
            else if (Globals.inventoryMarium.Armour == "padded" || Globals.inventoryMarium.Armour == "padded")
            {
                if (this.name == "Mariam_Front" || this.name == "Mariam_Back" || this.name == "Mariam_Persp")
                    SkinName("Padded Armor");
                else
                    SetSkin("Padded Armor");
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
