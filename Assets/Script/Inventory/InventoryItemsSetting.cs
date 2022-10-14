using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItemsSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button Sword, Mace, shield, paddedArmor, leatherArmor, guardArmor, chainmailArmor, leatherH, guardH, metalH, chainmailH;
    void Start()
    {
        if (Globals.inventoryHandler.isWeapon)
            WeaponsButtonSetting();
        else if (Globals.inventoryHandler.isShield)
            ShieldButtonSetting();
        else if (Globals.inventoryHandler.isHelmet)
            HelmetButtonSetting();
        else if (Globals.inventoryHandler.isArmor)
            ArmorButtonSetting();
    }
    void WeaponsButtonSetting()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (Globals.shopMerchant.ShortSword == 1)
                Sword.interactable = false;
            else
                Sword.interactable = true;
        }
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (Globals.shopMerchant.Mace == 1)
                Mace.interactable = false;
            else
                Mace.interactable = true;
        }
    }
    void ShieldButtonSetting()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (Globals.shopMerchant.WoodenBuckler == 1)
                shield.interactable = false;
            else
                shield.interactable = true;
        }
    }
    void HelmetButtonSetting()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (Globals.shopMerchant.LeatherCap == 1)
                leatherH.interactable = false;
            else
                leatherH.interactable = true;
            if (Globals.shopMerchant.KettleHat == 1)
                metalH.interactable = false;
            else
                metalH.interactable = true;
            if (Globals.shopMerchant.NesalHelmet == 1)
                guardH.interactable = false;
            else
                guardH.interactable = true;
            if (Globals.shopMerchant.NesalHelmet == 1)
                chainmailH.interactable = false;
            else
                chainmailH.interactable = true;
        }
    }
    void ArmorButtonSetting()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (Globals.shopMerchant.LeatherArmour == 1)
                leatherArmor.interactable = false;
            else
                leatherArmor.interactable = true;
            if (Globals.shopMerchant.PaddedArmour == 1)
                paddedArmor.interactable = false;
            else
                paddedArmor.interactable = true;
            if (Globals.shopMerchant.ClothArmour == 1)
                guardArmor.interactable = false;
            else
                guardArmor.interactable = true;
            if (Globals.shopMerchant.ChainArmour == 1)
                chainmailArmor.interactable = false;
            else
                chainmailArmor.interactable = true;
        }
    }
}
