using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class CharacterSlot : MonoBehaviour
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
    public string mace;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string axe;
    [SpineAttachment(currentSkinOnly: true, slotField: "B_Weapon")]
    public string hammer;
    [SpineSlot]
    public string R_weapon;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string sword1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string mace1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string axe1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string hammer1;
    [SpineAttachment(currentSkinOnly: true, slotField: "R_Weapon")]
    public string spear;
    [SpineSlot]
    public string shield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string smallShield;
    [SpineAttachment(currentSkinOnly: true, slotField: "Shield")]
    public string mediumShield;
    [SpineSlot]
    public string bow;
    [SpineAttachment(currentSkinOnly: true, slotField: "L_Weapon")]
    public string bow1;

    [SpineSlot]
    public string Arrow;
    [SpineAttachment(currentSkinOnly: true, slotField: "Arrow")]
    public string arrow;
    DatabaseManager db;
    Slot[] slots;
    Scene currentScene;
    string sceneName;
    void Start()
    {

        Globals.characterSlot = this;
        slots = this.GetComponent<SkeletonMecanim>().skeleton.Slots.Items;
        db = FindObjectOfType<DatabaseManager>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
    private void Update()
    {
        UpdateCharacter();
    }
    public void UpdateCharacter()
    {
        if (sceneName == "BattleScene" || sceneName == "Battle Scene_Barghest  Lair" || sceneName == "Battle Scene_Brigand Lair" || sceneName == "Battle Scene_Castle" || sceneName == "Battle Scene_CobbleStone" || sceneName == "Battle Scene_Death Wight Lair" || sceneName == "BattleScene_Cave" || sceneName == "Motte and Baley Battle Scene")
        {
            if (Globals.inventoryProtagnist.Armour == "padded")
            {
                SetSkin("Padded Armor");
                //  Debug.Log("padded armor.......");

                SkeletonSlot("Arrow");
                Debug.Log("Arrow && shield....... rmove");
            }
            else if (Globals.inventoryProtagnist.Armour == "Chainmail" || Globals.inventoryProtagnist.Armour == "Scale")
            {
                SetSkin("Chainmail Armor");
                SkeletonSlot("Arrow");
            }


            else if (Globals.inventoryProtagnist.Armour == "Leahter" || Globals.inventoryProtagnist.Armour == "Hide" || Globals.inventoryProtagnist.Armour == "Brigadine")
            {
                SetSkin("Leather Armor");
                SkeletonSlot("Arrow");
                Debug.Log("Leather Armor.......");

            }

            if (Globals.inventoryProtagnist.WoodenBuckler == 1 || Globals.inventoryProtagnist.WoodenSmallRounded == 1 || Globals.inventoryProtagnist.MetalBuckler == 1 || Globals.inventoryProtagnist.MetalSmallRounded == 1)
            {
                if (this.name == "perR" || this.name == "perL")
                    ShowUpgrade(shield, smallShield, "small round shield persp");
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
            if (Globals.avatarState.AvatarName == "WarriorMale" || Globals.avatarState.AvatarName == "WarriorFemale")
            {
                if (Globals.inventoryProtagnist.Helmet == "LeatherHelmet")
                    ShowUpgrade(halmet, leatherHelmet, "Leather Helmet");
                else if (Globals.inventoryProtagnist.Helmet == "KettleHelmet")
                    ShowUpgrade(halmet, metalHelmet, "Metal Helmet");
                else if (Globals.inventoryProtagnist.Helmet == "NasalHelmet")
                    ShowUpgrade(halmet, metalHelmet, "Metal Helmet");
                else if (Globals.inventoryProtagnist.Helmet == "AvainTail" || Globals.inventoryProtagnist.Helmet == "MailCoif")
                {
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                    {
                        if (this.name == "perR" || this.name == "perL")
                            ShowUpgrade(halmet, chainmailHelmet, "Chainmail Helmet");
                    }
                    else
                        ShowUpgrade(halmet, chainmailHelmet, "Chainmail Helmet");
                }
                if (Globals.inventoryProtagnist.AttackWeapon == "Dragger" || Globals.inventoryProtagnist.AttackWeapon == "ShortSword" || Globals.inventoryProtagnist.AttackWeapon == "longSword")
                {
                    if (this.name == "perR" || this.name == "perL")
                    {
                        ShowUpgrade(weapon, sword, "warrior sword");
                        ShowUpgrade(R_weapon, sword1, "warrior sword");
                    }
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "ShortAxe" || Globals.inventoryProtagnist.AttackWeapon == "longAxe")
                {
                    if (this.name == "perR" || this.name == "perL")
                    {
                        ShowUpgrade(weapon, axe, "single side axe2");
                        ShowUpgrade(R_weapon, axe1, "single side axe");
                    }
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "warHammer")//|| Globals.inventoryProtagnist.Spear == 1)
                {
                    if (this.name == "perR" || this.name == "perL")
                    {
                        ShowUpgrade(weapon, axe, "Hammer2");
                        ShowUpgrade(R_weapon, axe1, "Hammer");
                    }
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "Mace")
                {
                    if (this.name == "perR" || this.name == "perL")
                    {
                        ShowUpgrade(weapon, axe, "Mace2");
                        ShowUpgrade(R_weapon, axe1, "Mace");
                    }
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
                {
                    if (this.name == "perR" || this.name == "perL")
                        ShowUpgrade(R_weapon, spear, "spear");
                }
                else
                {
                    if (this.name == "perR" || this.name == "perL")
                    {
                        ShowUpgrade(weapon, sword, "warrior sword");
                        ShowUpgrade(R_weapon, sword1, "warrior sword");
                    }
                }
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 || Globals.inventoryProtagnist.WoodenSmallRounded == 1 || Globals.inventoryProtagnist.MetalBuckler == 1 || Globals.inventoryProtagnist.MetalSmallRounded == 1)
                {
                    //  Debug.Log("shield1111111111");
                    if (this.name == "perR" || this.name == "perL")
                        ShowUpgrade(shield, smallShield, "small round shield persp");
                }
                else if (Globals.inventoryProtagnist.WoodenMediumShield == 1 || Globals.inventoryProtagnist.MetalMediumShield == 1)
                {
                    Debug.Log("shield222222222222");
                    if (this.name == "perR" || this.name == "perL")
                        ShowUpgrade(shield, mediumShield, "medium round shield side");
                }
                else
                {
                    // Debug.Log("shield3333333333");
                    foreach (Slot item in slots)
                    {
                        // Debug.Log("inside....."+item+"............");
                        if (item.ToString() == "Shield")
                        {
                            //  Debug.Log("inside..... shield");
                            item.Attachment = null;
                            item.A = 0f;
                            //   this.GetComponent<SkeletonMecanim>().Skeleton.SetSlotsToSetupPose();
                        }
                    }
                }

            }
            else if (Globals.avatarState.AvatarName == "ArcherMale" || Globals.avatarState.AvatarName == "ArcherFemale")
            {
                if (this.name == "perR" || this.name == "perL")
                {
                    if (Globals.inventoryProtagnist.Helmet == "LeatherHelmet")
                    {
                        if (Globals.avatarState.AvatarName == "ArcherMale")
                            ShowUpgrade(halmet, leatherHelmet, "Leather Helmet");
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            ShowUpgrade(halmet, leatherHelmet, "leather capback persp");
                    }
                    else if (Globals.inventoryProtagnist.Helmet == "KettleHelmet" || Globals.inventoryProtagnist.Helmet == "NasalHelmet")
                    {
                        if (Globals.avatarState.AvatarName == "ArcherMale")
                            ShowUpgrade(halmet, metalHelmet, "Metal Helmet");
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            ShowUpgrade(halmet, metalHelmet, "metal helmet persp");
                    }
                    else if (Globals.inventoryProtagnist.Helmet == "AvainTail" || Globals.inventoryProtagnist.Helmet == "MailCoif")
                    {
                        if (Globals.avatarState.AvatarName == "ArcherMale")
                            ShowUpgrade(halmet, chainmailHelmet, "Chainmail Helmet");
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            ShowUpgrade(halmet, chainmailHelmet, "chainmail helmet");
                    }

                }
                if ((Globals.inventoryProtagnist.AttackWeapon == "Dragger" || Globals.inventoryProtagnist.AttackWeapon == "ShortSword" || Globals.inventoryProtagnist.AttackWeapon == "longSword") && (Globals.inventoryProtagnist.Dragger >= 1 || Globals.inventoryProtagnist.ShortSword >= 1 || Globals.inventoryProtagnist.LongSword >= 1))
                {
                    ShowUpgrade(weapon, sword, "Warrior Sword");
                    ShowUpgrade(R_weapon, sword, "warrior sword");
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "ShortAxe" || Globals.inventoryProtagnist.AttackWeapon == "longAxe")
                {
                    ShowUpgrade(weapon, axe, "single side axe2");
                    ShowUpgrade(R_weapon, axe1, "single side axe");
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "warHammer")
                {
                    ShowUpgrade(weapon, hammer, "Hammer2");
                    ShowUpgrade(R_weapon, hammer1, "Hammer");
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
                {
                   // ShowUpgrade(weapon, hammer, "Hammer2");
                    ShowUpgrade(R_weapon, spear, "spear");
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "Mace")
                {
                    ShowUpgrade(R_weapon, mace1, "Mace");
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "shortBow" || Globals.inventoryProtagnist.AttackWeapon == "longBow")
                {
                    ShowUpgrade(bow, bow1, "bow");
                    //ShowUpgrade(R_weapon, none, "");
                    ShowUpgrade(Arrow, arrow, "Arrow");

                    SkeletonSlot("shield");
                    //SkeletonSlot("R_weapon");


                    foreach (Slot item in slots)
                    {
                        if (item.ToString().Contains("R_Weapon"))
                        {
                            item.Attachment = null;
                            item.A = 0f;
                            Debug.Log("R_weapon romve");

                        }
                    }

                    Debug.Log("Bow enable");


                }
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 || Globals.inventoryProtagnist.WoodenSmallRounded == 1 || Globals.inventoryProtagnist.MetalBuckler == 1 || Globals.inventoryProtagnist.MetalSmallRounded == 1)
                {
                    ShowUpgrade(shield, smallShield, "small round shield persp");
                }
            }
            else if (Globals.avatarState.AvatarName == "PriestMale" || Globals.avatarState.AvatarName == "PriestFemale")
            {
                if (this.name == "PerR" || this.name == "PerL")
                {
                    if (Globals.inventoryProtagnist.Helmet == "LeatherHelmet")
                    {
                        if (Globals.avatarState.AvatarName == "PriestFemale")
                            ShowUpgrade(halmet, leatherHelmet, "leather capback persp");
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            ShowUpgrade(halmet, leatherHelmet, "Leather Helmet");
                    }
                    else if (Globals.inventoryProtagnist.Helmet == "KettleHelmet" || Globals.inventoryProtagnist.Helmet == "NasalHelmet")
                    {
                        if (Globals.avatarState.AvatarName == "PriestFemale")
                            ShowUpgrade(halmet, metalHelmet, "metal helmet persp");
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            ShowUpgrade(halmet, metalHelmet, "Metal Helmet");
                    }
                    else if (Globals.inventoryProtagnist.Helmet == "AvainTail" || Globals.inventoryProtagnist.Helmet == "MailCoif")
                    {
                        if (Globals.avatarState.AvatarName == "PriestFemale")
                            ShowUpgrade(halmet, chainmailHelmet, "chainmail helmet persp copy");
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            ShowUpgrade(halmet, chainmailHelmet, "Metal Helmet");
                    }
                }
                if (Globals.inventoryProtagnist.AttackWeapon == "Dragger" || Globals.inventoryProtagnist.AttackWeapon == "ShortSword" || Globals.inventoryProtagnist.AttackWeapon == "longSword")
                {
                    ShowUpgrade(weapon, sword, "Warrior Sword");
                    ShowUpgrade(R_weapon, sword1, "warrior sword");
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "ShortAxe" || Globals.inventoryProtagnist.AttackWeapon == "longAxe")
                {
                    ShowUpgrade(weapon, axe, "single side axe2");
                    // ShowUpgrade(R_weapon,axe1,)
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "warHammer" || Globals.inventoryProtagnist.AttackWeapon == "Spear")
                {
                    ShowUpgrade(weapon, hammer, "Hammer2");
                    ShowUpgrade(R_weapon, hammer1, "Hammer");
                }
                else if (Globals.inventoryProtagnist.AttackWeapon == "Mace")
                {
                    ShowUpgrade(weapon, mace, "Mace2");
                    ShowUpgrade(R_weapon, mace1, "Mace");

                }
                else
                {
                    ShowUpgrade(weapon, mace, "Mace2");
                    ShowUpgrade(R_weapon, mace1, "Mace");
                }

            }


            // Debug.Log("charcter update skin.........");

        }
        else
        {
            if (Globals.avatarState.AvatarName == "WarriorMale" || Globals.avatarState.AvatarName == "WarriorFemale")
            {
                if (this.name == "perR" || this.name == "perL")
                {
                    ShowUpgrade(weapon, sword, "warrior sword");
                    ShowUpgrade(R_weapon, sword1, "warrior sword");
                }
                else if (this.name == "Back")
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
                    if (item.ToString().Contains("Shield"))
                    {
                        item.Attachment = null;
                        item.A = 0f;
                    }
                }
            }
            else if (Globals.avatarState.AvatarName == "PriestMale" || Globals.avatarState.AvatarName == "PriestFemale")
            {
                if (this.name == "Back" || this.name == "perR" || this.name == "perL")
                    ShowUpgrade(weapon, sword, "warrior sword");
            }
            foreach (Slot item in slots)
            {
                if (item.ToString().Contains("Shield"))
                {
                    item.Attachment = null;
                    item.A = 0f;
                }
            }
        }
    }
    public void SetSkin(string skinName)
    {
        this.GetComponent<SkeletonMecanim>().Skeleton.SetSkin(skinName);
        this.GetComponent<SkeletonMecanim>().Skeleton.SetSlotsToSetupPose();

    }
    public void SetHelmet(string slot, string attachment)
    {
        this.GetComponent<SkeletonMecanim>().Skeleton.SetAttachment(slot, attachment);
    }
    public void ShowUpgrade(string slot, string attachment, string skinName)
    {
        this.GetComponent<SkeletonMecanim>().Skeleton.SetAttachment(slot, attachment);
    }
    public void SkeletonSlot(string _spine)
    {
        if (_spine != null)
        {
            Spine.Slot slot = GetComponent<SkeletonMecanim>().Skeleton.FindSlot(_spine);
            if (slot != null)
            {
                slot.Attachment = null;
            }
        }
    }
}
