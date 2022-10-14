using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.UI;
public class StatisticsRecords : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PlayerConfiguration smithMale, smithFemale, acolyteMale, acolyteFemale,priestMale, priestFemale,john,Marium,helena,tucker,activeConfiguration;
    [SerializeField]
    Text characterName, AttackText,defenceText,damageText,healthText;
    //void Start()
    //{
    //    Globals.staticRecord = this;
    //    CharacterRecords();

    //}
    private void Awake()
    {
        Globals.staticRecord = this;
        CharacterRecords();
    }
    public void CharacterRecords()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale")
        {
            activeConfiguration = smithMale;
            //   characterName.text = "Robyn";
            characterName.text = Globals.characterName;
            if (Globals.afterPromotion)
                defenceText.text = "Squire";
            else
                defenceText.text = "Smith";
          //  ProtagnistSetup();
           // Setup(10, 5,Globals.avatarState.Level);
        }
        if (Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale")
        {
            activeConfiguration = acolyteMale;
            //characterName.text = "Scout";
            characterName.text = Globals.characterName;
            if (Globals.afterPromotion)
                defenceText.text = "Hunter";
            else
                defenceText.text = "Scout";
          //  ProtagnistSetup();
           // Setup(5, 5, Globals.avatarState.Level);
        }
        if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            activeConfiguration = priestMale;
            characterName.text = Globals.characterName;
            if (Globals.afterPromotion)
            {
                if (Globals.selectedInventoryCharacter == "PriestMale")
                       defenceText.text = "Monk";
                else if(Globals.selectedInventoryCharacter== "PriestFemale")
                    defenceText.text = "Nun";
            }
            else
                defenceText.text = "Acolyte";
           // ProtagnistSetup();
           // Setup(7, 5, Globals.avatarState.Level);
        }
        if (Globals.selectedInventoryCharacter == "John")
        {
            activeConfiguration = john;
            characterName.text = "John";
            if (Globals.afterPromotion)
                defenceText.text = "Squire";
            else
               defenceText.text = "Smith";
          //  JohnSetup();
           // Setup(4, 5, Globals.avatarState.Level);
        }
        if (Globals.selectedInventoryCharacter == "Marium")
        {
            activeConfiguration = Marium;
            characterName.text = "Mariam";
            if(Globals.afterPromotion)
              defenceText.text = "Hunter";
            else
                defenceText.text = "Scout";
           // MariumSetup();
           // Setup(4, 4, Globals.avatarState.Level); 
        }
        if (Globals.selectedInventoryCharacter == "Tucker")
        {
            activeConfiguration = tucker;
            characterName.text = "Tucker";
            if(Globals.afterPromotion)
                defenceText.text = "Monk";
            else
                defenceText.text = "Acolyte";
           // TuckerSetup();
           // Setup(3, 6, Globals.avatarState.Level);
        }
    }
    void ProtagnistSetup()
    {
        Debug.Log("attack weapon::" + Globals.inventoryProtagnist.AttackWeapon);
        if (Globals.inventoryProtagnist.AttackWeapon == "Mace")
            AttackText.text = "25";
        else if (Globals.inventoryProtagnist.AttackWeapon == "Dragger")
            AttackText.text = "25";
        else if (Globals.inventoryProtagnist.AttackWeapon == "Maul")
            AttackText.text = "26";
        else if (Globals.inventoryProtagnist.AttackWeapon == "Flair")
            AttackText.text = "35";
        else if (Globals.inventoryProtagnist.AttackWeapon == "Club")
            AttackText.text = "29";
        else if (Globals.inventoryProtagnist.AttackWeapon == "longBow")
            AttackText.text = "20";
        else if (Globals.inventoryProtagnist.AttackWeapon == "shortBow")
            AttackText.text = "18";
        else if (Globals.inventoryProtagnist.AttackWeapon == "longSword")
            AttackText.text = "23";
        else if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
            AttackText.text = "20";
        else if (Globals.inventoryProtagnist.AttackWeapon == "longAxe")
            AttackText.text = "27";
        else if (Globals.inventoryProtagnist.AttackWeapon == "warHammer")
            AttackText.text = "15";
        else if (Globals.inventoryProtagnist.AttackWeapon == "ShortAxe")
            AttackText.text = "14";
        else if (Globals.inventoryProtagnist.AttackWeapon == "ShortSword")
            AttackText.text = "21";
        healthText.text = Globals.avatarState.Level.ToString();


    }
    void JohnSetup()
    {
        if (Globals.inventoryJohn.WeaponAttack == "Mace")
            AttackText.text = "25";
        else if (Globals.inventoryJohn.WeaponAttack == "Dragger")
            AttackText.text = "25";
        else if (Globals.inventoryJohn.WeaponAttack == "longSword")
            AttackText.text = "23";
        else if (Globals.inventoryJohn.WeaponAttack == "Spear")
            AttackText.text = "20";
        else if (Globals.inventoryJohn.WeaponAttack == "longAxe")
            AttackText.text = "27";
        else if (Globals.inventoryJohn.WeaponAttack == "warHammer")
            AttackText.text = "15";
        else if (Globals.inventoryJohn.WeaponAttack == "ShortAxe")
            AttackText.text = "14";
        else if (Globals.inventoryJohn.WeaponAttack == "ShortSword")
            AttackText.text = "21";
        healthText.text = Globals.avatarState.Level.ToString();
    }
    void MariumSetup()
    {
        if (Globals.inventoryMarium.WeaponAttack == "Dragger")
            AttackText.text = "25";
        else if (Globals.inventoryMarium.WeaponAttack == "longBow")
            AttackText.text = "20";
        else if (Globals.inventoryMarium.WeaponAttack == "shortBow")
            AttackText.text = "18";
        else if (Globals.inventoryMarium.WeaponAttack == "Spear")
            AttackText.text = "20";
        else if (Globals.inventoryMarium.WeaponAttack == "warHammer")
            AttackText.text = "15";
        else if (Globals.inventoryMarium.WeaponAttack == "ShortAxe")
            AttackText.text = "14";
        else if (Globals.inventoryMarium.WeaponAttack == "ShortSword")
            AttackText.text = "21";
        healthText.text = Globals.avatarState.Level.ToString();
    }
    void TuckerSetup()
    {
        if (Globals.inventoryTucker.WeaponAttack == "Mace")
            AttackText.text = "25";
        else if (Globals.inventoryTucker.WeaponAttack == "Dragger")
            AttackText.text = "25";
        else if (Globals.inventoryTucker.WeaponAttack == "Maul")
            AttackText.text = "26";
        else if (Globals.inventoryTucker.WeaponAttack == "Flair")
            AttackText.text = "35";
        else if (Globals.inventoryTucker.WeaponAttack == "warHammer")
            AttackText.text = "15";
        healthText.text = Globals.avatarState.Level.ToString();
    }
    void Setup(int attack,int damage,int Health)
    {
        if (Globals.shopMerchant.ShortSword == 1)
            AttackText.text = (attack + 20).ToString();
       else if (Globals.shopMerchant.Mace == 1)
            AttackText.text = (attack + 15).ToString();
        else
            AttackText.text = attack.ToString();
        if (Globals.shopMerchant.WoodenBuckler == 1)
            damageText.text = (attack + 10).ToString();
        else
            damageText.text = attack.ToString();
        if (Globals.shopMerchant.PaddedArmour == 1)
            defenceText.text = (damage + 10).ToString();
       else if (Globals.shopMerchant.LeatherArmour == 1)
            defenceText.text = (damage + 20).ToString();
       else if (Globals.shopMerchant.ClothArmour == 1)
            defenceText.text = (damage + 8).ToString();
       else if (Globals.shopMerchant.ChainArmour == 1)
            defenceText.text = (damage + 25).ToString();
        else
            defenceText.text = damage.ToString();
        
            healthText.text = Health.ToString();
    }
    void WeaponRecord(int w1, int w2)
    {
        activeConfiguration.weapon1 = w1;
        activeConfiguration.weapon2 = w2;

    }
    void DefenceRecord(int d1)
    {

    }
    void SettingOfStatisticsRecord()
    {
        characterName.text = activeConfiguration.characterName;
        AttackText.text = activeConfiguration.weaponId.ToString();
       
    }
    void AttackPower()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
