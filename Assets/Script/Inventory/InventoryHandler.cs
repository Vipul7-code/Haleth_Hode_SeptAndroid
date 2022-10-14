using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
public class InventoryHandler : MonoBehaviour
{
    [HideInInspector]
    public bool isCharacter, isEquipement, isStatistics, isArmor, isHelmet, isWeapon, isShield,isItem;
   
  public  Button magicSword, Sword,shortAxe,warHammer,longAxe,Spear,longSword,shortBow,longBow,Club,flair,Maul, Mace,Dragger, shield,woodenRound,metalBuckler,metalRound,woodenMedium,metalMedium, paddedArmor, leatherArmor, guardArmor, chainmailArmor,scaleArmour,hideArmour, clothArmour, leatherH, guardH, metalH, chainmailH,mailCoifH;
    [Space]
  public  GameObject magicSwordE, SwordE,shortAxeE,warHammerE,longAxeE,spearE,longSwordE,shortBowE,longBowE,clubE,flairE,maulE ,MaceE,draggerE, shieldE,woodenRoundE,metalBucklerE,metalRoundE,woodenMediumE,metalMediumE, woodenKiteE, metalKitE, clothArmorE, paddedArmorE, hideArmourE, leatherArmorE, brigadinArmorE, scaleArmourE, chainArmorE, ringMailArmorE, splintMailArmorE, bandedMailArmorE, guardArmorE, chainmailArmorE, leatherHE, kettleHE, nasalHE, aventailHE,mailCoifHE,mailCoifE, ale, curePotion, food, meat, healPotion, rum,bones,rottenFlesh,feather,barghestTooth,barghestHeart,deathWightMace,deathWightCloak,SoulGem,LordAlfredSword,soulEyeGem;
    public Toggle attackOn, attackOff, defenceOn, defenceOff;
    [SerializeField]
    GameObject characterPanel, equipmentPanel, staticsPanel,weaponPanel,helmetPanel,shieldPanel,armourPanel, itemPanel;
    [SerializeField]
    GameObject weaponList, armourList, helmetList, shieldList;
    DatabaseManager db;
    [SerializeField]
    Text noItem,noItem1,noItem2;
    [SerializeField]
    GameObject ProtagnistHighlight, johnHighlight, mariumHighlight, tuckerHighlight;
    public Text goldText;
    [Space]
    public GameObject shortSwordE, compositeBowE, crossBowE, doubleHeadedAxeE;

    public PlayerItemLibrary protagnist,john,marium,tucker;

    public Text avatarClass, level, health, xp, attack, defence;
    public PlayerItemLibrary protagonistItemLib, companionLib;
    public List<PlayerItem> player;

    // Start is called before the first frame update
    void Start()
    {
      //  Globals.inventoryHandler = this;
        db = FindObjectOfType<DatabaseManager>();
        goldText.text = Globals.shopMerchant.Gold.ToString();
        isCharacter = true;
        InventoryPArtsSetting(false, false, false, false, false);
        // isWeapon = true;
        xp.text = Globals.avatarState.TotalXp.ToString();
    }
    private void Awake()
    {
        Globals.inventoryHandler = this;
    }
    public void ClickOnButton(string btn_name)
    {
        switch (btn_name)
        {
            case "Character":
                InventoryTopSetting(true, false, false);
                InventoryPanelSetting(true, false, false);
                InventoryPArtsSetting(false, false, false, false, false);
                EquipmentSecondList(true, false, false, false);
                noItem.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "Equipment":
                InventoryTopSetting(false, true, false);
                InventoryPanelSetting(false, true, false);
                isWeapon = true;
                InventorySetupForCompanion();
                break;
            case "Statics":
                InventoryTopSetting(false, false, true);
                InventoryPanelSetting(false, false, true);
                Debug.Log("inventory avatar:: "+Globals.selectedInventoryCharacter);
                Globals.staticRecord.CharacterRecords();
                PrintValues();
                break;
            case "Weapon":
                InventoryPArtsSetting(false, false, true, false,false);
                EquipmentPanelSetting(true, false, false, false,false);
                noItem1.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "Defence":
                InventoryPArtsSetting(false, false, false, true,false);
                EquipmentPanelSetting(false, true, false, false,false);
                noItem1.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "Helmet":
                InventoryPArtsSetting(false, true, false, false,false);
                EquipmentPanelSetting(false, false, true, false,false);
                noItem1.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "Armour":
                InventoryPArtsSetting(true, false, false, false,false);
                EquipmentPanelSetting(false, false, false, true,false);
                noItem1.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "OtherItems":
                InventoryPArtsSetting(false, false, false, false, true);
                EquipmentPanelSetting(false, false, false, false, true);
                noItem1.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "Armour1":
                InventoryPArtsSetting(true, false, false, false,false);
                EquipmentSecondList(false, true, false, false);
                noItem.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "Helmet1":
                InventoryPArtsSetting(false, true, false, false,false);
                EquipmentSecondList(false, false, true, false);
                noItem.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "weapon1":
                InventoryPArtsSetting(false, false, true, false,false);
                EquipmentSecondList(true, false, false, false);
                noItem.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
            case "shield1":
                InventoryPArtsSetting(false, false, false, true,false);
                EquipmentSecondList(false, false, false, true);
                noItem.gameObject.SetActive(false);
                InventorySetupForCompanion();
                break;
        }
    }
    void ItemSetting()
    {
        if (Globals.shopMerchant.Ale >= 1)
        {
            ale.SetActive(true);
            ale.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Ale;
        }
        else
            ale.SetActive(false);
        if (Globals.shopMerchant.CurePotion >= 1)
        {
            curePotion.SetActive(true);
            curePotion.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.CurePotion;
        }
        else
            curePotion.SetActive(false);
        if (Globals.shopMerchant.Food >= 1)
        {
            food.SetActive(true);
            food.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Food;
        }
        else
            food.SetActive(false);
        if (Globals.shopMerchant.Meat >= 1)
        {
            meat.SetActive(true);
            meat.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Meat;
        }
        else
            meat.SetActive(false);
        if (Globals.shopMerchant.HealPotion >= 1)
        {
            healPotion.SetActive(true);
            healPotion.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.HealPotion;
        }
        else
            healPotion.SetActive(false);
        if (Globals.shopMerchant.Rum >= 1)
        {
            rum.SetActive(true);
            rum.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Rum;
        }
        else
            rum.SetActive(false);
        if (Globals.shopMerchant.DeathWightCloak >= 1)
        {
            deathWightCloak.SetActive(true);
            deathWightCloak.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.DeathWightCloak;
        }
        else
            deathWightCloak.SetActive(false);
        //if (Globals.shopMerchant.DeathWightMace >= 1)
        //{
        //    deathWightMace.SetActive(true);
        //    deathWightMace.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.DeathWightMace;
        //}
        //else
        //    rottenFlesh.SetActive(false);
        if (Globals.shopMerchant.SoulEyeGems >= 1)
        {
            soulEyeGem.SetActive(true);
            soulEyeGem.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.SoulEyeGems;
        }
        else
            soulEyeGem.SetActive(false);
        if (Globals.shopMerchant.SoulGem >= 1)
        {
            SoulGem.SetActive(true);
            SoulGem.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.SoulGem;
        }
        else
            SoulGem.SetActive(false);
        if (Globals.shopMerchant.BarghestHeart >= 1)
        {
            barghestHeart.SetActive(true);
            barghestHeart.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.BarghestHeart;
        }
   
        else
            barghestHeart.SetActive(false);
        if (Globals.shopMerchant.Bones >= 1)
        {
            bones.SetActive(true);
            bones.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Bones;
        }     
        else
            bones.SetActive(false);
        if (Globals.shopMerchant.RottenFlesh >= 1)
        {
            rottenFlesh.SetActive(true);
            rottenFlesh.transform.GetChild(2).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.RottenFlesh;
        }     
        else
            rottenFlesh.SetActive(false);
        if (Globals.shopMerchant.Ale == 0 && Globals.shopMerchant.CurePotion == 0 && Globals.shopMerchant.Food == 0 && Globals.shopMerchant.Meat == 0 && Globals.shopMerchant.HealPotion == 0 && Globals.shopMerchant.Rum == 0 && Globals.shopMerchant.DeathWightCloak==0 && Globals.shopMerchant.SoulEyeGems==0 && Globals.shopMerchant.SoulGem==0 && Globals.shopMerchant.BarghestHeart==0 && Globals.shopMerchant.Bones==0 && Globals.shopMerchant.RottenFlesh==0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Item available";
        }
        else
            noItem1.gameObject.SetActive(false);
    }
    public void PurchaseItems(string itemName)
    {
        if (itemName == "Ale")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Ale = 1;
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Ale = 1;
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.Ale = 1;
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
            }
            else if(Globals.selectedInventoryCharacter== "Tucker")
            {
                Globals.inventoryTucker.Ale = 1;
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
            }
            Globals.shopMerchant.Ale -= 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (itemName == "CurePotion")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.CurePotion = 1;
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.CurePotion = 1;
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.CurePotion = 1;
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.CurePotion = 1;
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
            }
            Globals.shopMerchant.CurePotion -= 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else  if (itemName == "Food")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Food = 1;
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Food = 1;
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.Food = 1;
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Food = 1;
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
            }
            Globals.shopMerchant.Food -= 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (itemName == "Meat")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Meat = 1;
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Meat = 1;
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.Meat = 1;
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Meat = 1;
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
            }
            Globals.shopMerchant.Meat -= 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (itemName == "HealPotion")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.HealPotion = 1;
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.HealPotion = 1;
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.HealPotion = 1;
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.HealPotion = 1;
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
            }
            Globals.shopMerchant.HealPotion -= 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (itemName == "Rum")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Rum = 1;
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Rum = 1;
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.Rum = 1;
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Rum = 1;
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
            }
            Globals.shopMerchant.Rum -= 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
    }
    void ArmourSettingForProtagnist()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale")
        {
            if (isCharacter)
                ArmourForSmithCharacter();
            else if (isEquipement)
                ArmourForSmithEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale")
        {
            if (isCharacter)
                ArmourForArcherCharacter();
            else if (isEquipement)
                ArmourForArcherEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (isCharacter)
                ArmourForPriestCharacter();
            else if (isEquipement)
                ArmourForPriestEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "John")
        {
            if (isCharacter)
                ArmourForJohnCharacter();
            else if (isEquipement)
                ArmourForJohnCharacterEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "Marium")
        {
            if (isCharacter)
                ArmourForMarium();
            else if (isEquipement)
                ArmourForMariumEquipement();
               // ArmourForArcherEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "Tucker")
        {
            if (isCharacter)
                ArmourForTucker();
            else if (isEquipement)
                ArmourForTuckerEquipement();
                //ArmourForPriestEquipement();
        }
    }

    void ArmourShowTucker()
    {
        if (Globals.shopMerchant.PaddedArmour <= 3 && Globals.shopMerchant.PaddedArmour > 0)
        {
            if (Globals.shopMerchant.PaddedArmour == 1)
            {
                if (Globals.inventoryJohn.PaddedArmour == 1 || Globals.inventoryProtagnist.PaddedArmour == 1 || Globals.inventoryMarium.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 2)
            {
                if ((Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryProtagnist.PaddedArmour == 1) || (Globals.inventoryProtagnist.PaddedArmour == 1 && Globals.inventoryMarium.PaddedArmour == 1) || (Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryMarium.PaddedArmour == 1))
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 3)
            {
                if (Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryProtagnist.PaddedArmour == 1 && Globals.inventoryMarium.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.LeatherArmour <= 3 && Globals.shopMerchant.LeatherArmour > 0)
        {
            if (Globals.shopMerchant.LeatherArmour == 1)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 || Globals.inventoryProtagnist.LeatherArmour == 1 || Globals.inventoryMarium.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 2)
            {
                if ((Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryProtagnist.LeatherArmour == 1) || (Globals.inventoryProtagnist.LeatherArmour == 1 && Globals.inventoryMarium.LeatherArmour == 1) || (Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryMarium.LeatherArmour == 1))
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryProtagnist.LeatherArmour == 1 && Globals.inventoryMarium.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
        }


        if (Globals.shopMerchant.HideArmour <= 3 && Globals.shopMerchant.HideArmour > 0)
        {
            if (Globals.shopMerchant.HideArmour == 1)
            {
                if (Globals.inventoryJohn.HideArmour == 1 || Globals.inventoryProtagnist.HideArmour == 1 || Globals.inventoryMarium.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.HideArmour == 2)
            {
                if ((Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryProtagnist.HideArmour == 1) || (Globals.inventoryProtagnist.HideArmour == 1 && Globals.inventoryMarium.HideArmour == 1) || (Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryMarium.HideArmour == 1))
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryProtagnist.HideArmour == 1 && Globals.inventoryMarium.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
        }
    }
    void ArmourForTucker()
    {
        AllArmour();
        if (Globals.shopMerchant.PaddedArmour >= 1)
        {
            paddedArmor.gameObject.SetActive(true);
            if (Globals.inventoryTucker.Armour == "padded")
                paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            paddedArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1)
        {
            leatherArmor.gameObject.SetActive(true);
            if (Globals.inventoryTucker.Armour == "Leahter")
                leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1)
        {
            hideArmour.gameObject.SetActive(true);
            if (Globals.inventoryTucker.Armour == "Hide")
                hideArmour.transform.GetChild(2).gameObject.SetActive(true);
            else
                hideArmour.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            hideArmour.gameObject.SetActive(false);

        ArmourShowTucker();
        if (!paddedArmor.gameObject.activeInHierarchy && !leatherArmor.gameObject.activeInHierarchy && !hideArmour.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Armour available";
        }
        else
            noItem.gameObject.SetActive(false);
        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.ScaleArmour == 0 && Globals.shopMerchant.HideArmour == 0)
        //{
        //    if (Globals.inventoryTucker.PaddedArmour ==0 && Globals.inventoryTucker.LeatherArmour == 0 && Globals.inventoryTucker.HideArmour == 0)
        //    {
        //        //noItem.gameObject.SetActive(true);
        //        //noItem.text = "No Armour available";
        //        ////paddedArmor.gameObject.SetActive(true);
        //        ////paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ArmourShowPriest()
    {
        if (Globals.shopMerchant.PaddedArmour <= 3 && Globals.shopMerchant.PaddedArmour > 0)
        {
            if (Globals.shopMerchant.PaddedArmour == 1)
            {
                if (Globals.inventoryMarium.PaddedArmour == 1 || Globals.inventoryJohn.PaddedArmour == 1 || Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 2)
            {
                if ((Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryJohn.PaddedArmour == 1) || (Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1) || (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1))
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 3)
            {
                if (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.LeatherArmour <= 3 && Globals.shopMerchant.LeatherArmour > 0)
        {
            if (Globals.shopMerchant.LeatherArmour == 1)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 || Globals.inventoryJohn.LeatherArmour == 1 || Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 2)
            {
                if ((Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryJohn.LeatherArmour == 1) || (Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1) || (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1))
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.HideArmour <= 3 && Globals.shopMerchant.HideArmour > 0)
        {
            if (Globals.shopMerchant.HideArmour == 1)
            {
                if (Globals.inventoryMarium.HideArmour == 1 || Globals.inventoryJohn.HideArmour == 1 || Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.HideArmour == 2)
            {
                if ((Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryJohn.HideArmour == 1) || (Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1) || (Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1))
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.HideArmour == 3)
            {
                if (Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
        }
    }
    void ArmourForPriestCharacter()
    {
        AllArmour();
        if (Globals.shopMerchant.PaddedArmour >= 1 || Globals.inventoryProtagnist.PaddedArmour >= 1)
        {
            paddedArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "padded")
                paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            paddedArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 || Globals.inventoryProtagnist.LeatherArmour >= 1)
        {
            leatherArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Leahter")
                leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1 || Globals.inventoryProtagnist.HideArmour >= 1)
        {
            hideArmour.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Hide")
                hideArmour.transform.GetChild(2).gameObject.SetActive(true);
            else
                hideArmour.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            hideArmour.gameObject.SetActive(false);

        ArmourShowPriest();
        if (!paddedArmor.gameObject.activeInHierarchy && !leatherArmor.gameObject.activeInHierarchy && !hideArmour.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Armour available";
        }
        else
            noItem.gameObject.SetActive(false);
        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.ScaleArmour == 0 && Globals.shopMerchant.HideArmour == 0)
        //{
        //    if (Globals.inventoryProtagnist.PaddedArmour == 0 && Globals.inventoryProtagnist.LeatherArmour == 0 && Globals.inventoryProtagnist.ScaleArmour == 0 && Globals.inventoryProtagnist.HideArmour == 0)
        //    {
        //        //noItem.gameObject.SetActive(true);
        //        //noItem.text = "No Armour available";
        //        ////clothArmour.gameObject.SetActive(true);
        //        ////clothArmour.transform.GetChild(2).gameObject.SetActive(true);

        //    }
        //    else
        //        noItem.text = "No Armour available";
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }

    void ArmourForPriestEquipement()
    {
        HideAllArmor();
        if (Globals.shopMerchant.PaddedArmour >= 1)
        {
            paddedArmorE.gameObject.SetActive(true);
            paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        }
        else
            paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1)
        {
            leatherArmorE.gameObject.SetActive(true);
            leatherArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour;
        }
        else
            leatherArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1)
        {
            hideArmourE.gameObject.SetActive(true);
            hideArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour + Globals.shopMerchant.HideArmour;
        }
        else
            hideArmourE.gameObject.SetActive(false);
        //int total = Globals.shopMerchant.ClothArmour + 1;
        //clothArmorE.SetActive(true);
        //clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.ScaleArmour == 0 && Globals.shopMerchant.HideArmour == 0)
        //{
        //    //noItem1.gameObject.SetActive(true);
        //    //noItem1.text = "No Armour available";
        //    clothArmorE.SetActive(true);
        //    clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ClothArmour;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void ArmourForTuckerEquipement()
    {
        HideAllArmor();
        if (Globals.shopMerchant.PaddedArmour >= 1)
        {
            paddedArmorE.gameObject.SetActive(true);
            paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        }
        else
            paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1)
        {
            leatherArmorE.gameObject.SetActive(true);
            leatherArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour;
        }
        else
            leatherArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1)
        {
            hideArmourE.gameObject.SetActive(true);
            hideArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour + Globals.shopMerchant.HideArmour;
        }
        else
            hideArmourE.gameObject.SetActive(false);
        //int total = Globals.shopMerchant.PaddedArmour + 1;
        //paddedArmorE.SetActive(true);
        //paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.ScaleArmour == 0 && Globals.shopMerchant.HideArmour == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Armour available";
            //paddedArmorE.gameObject.SetActive(true);
            //paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        }
        else
            noItem1.gameObject.SetActive(false);
    }
    void HideAllArmor()
    {
        clothArmorE.SetActive(false);
        paddedArmorE.SetActive(false);
        leatherArmorE.SetActive(false);
        hideArmourE.SetActive(false);
        scaleArmourE.SetActive(false);
    }
    void ArmourForArcherEquipement()
    {
        HideAllArmor();
        if (Globals.shopMerchant.ClothArmour >= 1)
        {
            clothArmorE.gameObject.SetActive(true);
            clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ClothArmour;
        }
        else
            paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.PaddedArmour >= 1)
        {
            paddedArmorE.gameObject.SetActive(true);
            paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        }
        else
            paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 )
        {
            leatherArmorE.gameObject.SetActive(true);
            leatherArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour;
        }
        else
            leatherArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1)
        {
            hideArmourE.gameObject.SetActive(true);
            hideArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.HideArmour;
        }
        else
            hideArmourE.gameObject.SetActive(false);
        int total = Globals.shopMerchant.ClothArmour + 1;
        clothArmorE.SetActive(true);
        clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.HideArmour == 0)
        //{
        //    //noItem1.gameObject.SetActive(true);
        //    //noItem1.text = "No Armour available";
        //    clothArmorE.SetActive(true);
        //    clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ClothArmour;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void ArmourForMariumEquipement()
    {
        HideAllArmor();
        //if (Globals.shopMerchant.ClothArmour >= 1)
        //{
        //    clothArmorE.gameObject.SetActive(true);
        //    clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ClothArmour;
        //}
        //else
        //    paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.PaddedArmour >= 1)
        {
            paddedArmorE.gameObject.SetActive(true);
            paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        }
        else
            paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1)
        {
            leatherArmorE.gameObject.SetActive(true);
            leatherArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour;
        }
        else
            leatherArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1)
        {
            hideArmourE.gameObject.SetActive(true);
            hideArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.HideArmour;
        }
        else
            hideArmourE.gameObject.SetActive(false);

        if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.HideArmour == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Armour available";

        }
        else
            noItem1.gameObject.SetActive(false);
        //int total = Globals.shopMerchant.PaddedArmour + 1;
        //paddedArmorE.SetActive(true);
        //paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;

        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.HideArmour == 0)
        //{
        //    paddedArmorE.gameObject.SetActive(true);
        //    paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void ArmourShowMarium()
    {
        if (Globals.shopMerchant.PaddedArmour <= 3 && Globals.shopMerchant.PaddedArmour > 0)
        {
            if (Globals.shopMerchant.PaddedArmour == 1)
            {
                if (Globals.inventoryJohn.PaddedArmour == 1 || Globals.inventoryProtagnist.PaddedArmour == 1 || Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 2)
            {
                if ((Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryProtagnist.PaddedArmour == 1) || (Globals.inventoryProtagnist.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1) || (Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1))
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 3)
            {
                if (Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryProtagnist.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.LeatherArmour <= 3 && Globals.shopMerchant.LeatherArmour > 0)
        {
            if (Globals.shopMerchant.LeatherArmour == 1)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 || Globals.inventoryProtagnist.LeatherArmour == 1 || Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 2)
            {
                if ((Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryProtagnist.LeatherArmour == 1) || (Globals.inventoryProtagnist.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1) || (Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1))
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryProtagnist.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.BrigadineArmor <= 2 && Globals.shopMerchant.BrigadineArmor > 0)
        {
            if (Globals.shopMerchant.BrigadineArmor == 1)
            {
                if (Globals.inventoryJohn.BrigadineArmour == 1 || Globals.inventoryProtagnist.BrigadineArmor == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryJohn.BrigadineArmour == 1 && Globals.inventoryProtagnist.BrigadineArmor == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
        }


        if (Globals.shopMerchant.HideArmour <= 3 && Globals.shopMerchant.HideArmour > 0)
        {
            if (Globals.shopMerchant.HideArmour == 1)
            {
                if (Globals.inventoryJohn.HideArmour == 1 || Globals.inventoryProtagnist.HideArmour == 1 || Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.HideArmour == 2)
            {
                if ((Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryProtagnist.HideArmour == 1) || (Globals.inventoryProtagnist.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1) || (Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1))
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryProtagnist.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
        }
    }
    void ArmourForMarium()
    {
        AllArmour();
        if (Globals.shopMerchant.PaddedArmour >= 1|| Globals.inventoryMarium.PaddedArmour >= 1)
        {
            paddedArmor.gameObject.SetActive(true);
            if (Globals.inventoryMarium.Armour == "padded")
            {
                paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
                paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            paddedArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 || Globals.inventoryMarium.LeatherArmour >= 1)
        {
            leatherArmor.gameObject.SetActive(true);
            if (Globals.inventoryMarium.Armour == "Leahter")
                leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.BrigadineArmor >= 1 || Globals.inventoryMarium.BrigadineArmour >= 1)
        {
            guardArmor.gameObject.SetActive(true);
            if (Globals.inventoryMarium.Armour == "Brigadine")
                guardArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1 || Globals.inventoryMarium.HideArmour >= 1)
        {
            hideArmour.gameObject.SetActive(true);
            if (Globals.inventoryMarium.Armour == "Hide")
                hideArmour.transform.GetChild(2).gameObject.SetActive(true);
            else
                hideArmour.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            hideArmour.gameObject.SetActive(false);

        ArmourShowMarium();

        if(!paddedArmor.gameObject.activeInHierarchy && !leatherArmor.gameObject.activeInHierarchy && !guardArmor.gameObject.activeInHierarchy && !hideArmour.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Armour available";
        }
        else
            noItem.gameObject.SetActive(false);

        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.HideArmour == 0)
        //{
        //    if (Globals.inventoryMarium.PaddedArmour ==0 && Globals.inventoryMarium.LeatherArmour == 0 && Globals.inventoryMarium.BrigadineArmour == 0 && Globals.inventoryMarium.HideArmour == 0)
        //    {
        //        //noItem.gameObject.SetActive(true);
        //        //noItem.text = "No Armour available";
        //        //////paddedArmor.gameObject.SetActive(true);
        //        //////paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }

    void ArmourShowArcher()
    {
        if (Globals.shopMerchant.PaddedArmour <= 3 && Globals.shopMerchant.PaddedArmour > 0)
        {
            if (Globals.shopMerchant.PaddedArmour == 1)
            {
                if (Globals.inventoryMarium.PaddedArmour == 1 || Globals.inventoryJohn.PaddedArmour == 1 || Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 2)
            {
                if ((Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryJohn.PaddedArmour == 1) || (Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1) || (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1))
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 3)
            {
                if (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.LeatherArmour <= 3 && Globals.shopMerchant.LeatherArmour > 0)
        {
            if (Globals.shopMerchant.LeatherArmour == 1)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 || Globals.inventoryJohn.LeatherArmour == 1 || Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 2)
            {
                if ((Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryJohn.LeatherArmour == 1) || (Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1) || (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1))
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.BrigadineArmor <= 2 && Globals.shopMerchant.BrigadineArmor > 0)
        {
            if (Globals.shopMerchant.BrigadineArmor == 1)
            {
                if (Globals.inventoryMarium.BrigadineArmour == 1 || Globals.inventoryJohn.BrigadineArmour == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 2)
            {
                if (Globals.inventoryMarium.BrigadineArmour == 1 && Globals.inventoryJohn.BrigadineArmour == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.HideArmour <= 3 && Globals.shopMerchant.HideArmour > 0)
        {
            if (Globals.shopMerchant.HideArmour == 1)
            {
                if (Globals.inventoryMarium.HideArmour == 1 || Globals.inventoryJohn.HideArmour == 1 || Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.HideArmour == 2)
            {
                if ((Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryJohn.HideArmour == 1) || (Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1) || (Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1))
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.HideArmour == 3)
            {
                if (Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryJohn.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
        }
    }
    void ArmourForArcherCharacter()
    {
        AllArmour();
        if (Globals.shopMerchant.PaddedArmour >= 1 || Globals.inventoryProtagnist.PaddedArmour >= 1)
        {
            paddedArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour== "padded")
                paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            paddedArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 || Globals.inventoryProtagnist.LeatherArmour >= 1)
        {
            leatherArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Leahter")
                leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.BrigadineArmor >= 1 || Globals.inventoryProtagnist.BrigadineArmor >= 1)
        {
            guardArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Brigadine")
                guardArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1 || Globals.inventoryProtagnist.HideArmour >= 1)
        {
            hideArmour.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Hide")
                hideArmour.transform.GetChild(2).gameObject.SetActive(true);
            else
                hideArmour.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            hideArmour.gameObject.SetActive(false);

        ArmourShowArcher();
        if (!paddedArmor.gameObject.activeInHierarchy && !leatherArmor.gameObject.activeInHierarchy && !guardArmor.gameObject.activeInHierarchy && !hideArmour.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Armour available";
        }
        else
            noItem.gameObject.SetActive(false);

        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.HideArmour == 0)
        //{
        //    if (Globals.inventoryProtagnist.PaddedArmour == 0 && Globals.inventoryProtagnist.LeatherArmour == 0 && Globals.inventoryProtagnist.BrigadineArmor == 0 && Globals.inventoryProtagnist.HideArmour == 0 )
        //    {
        //        //noItem.gameObject.SetActive(true);
        //        //noItem.text = "No Armour available";
        //        clothArmour.gameObject.SetActive(true);
        //        clothArmour.transform.GetChild(2).gameObject.SetActive(true);
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ArmourForSmithEquipement()
    {
        HideAllArmor();
        //if (Globals.shopMerchant.ClothArmour >= 1)
        //{
        //    clothArmorE.gameObject.SetActive(true);
        //    clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ClothArmour;
        //}
        //else
        //    clothArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.PaddedArmour >= 1)
        {
            paddedArmorE.gameObject.SetActive(true);
            paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        }
        else
            paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1)
        {
            hideArmourE.gameObject.SetActive(true);
            hideArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.HideArmour;
        }
        else
            hideArmourE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1)
        {
            leatherArmorE.gameObject.SetActive(true);
            leatherArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour;
        }
        else
            leatherArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.BrigadineArmor >= 1)
        {
            brigadinArmorE.gameObject.SetActive(true);
            brigadinArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.BrigadineArmor;
        }
        else
            brigadinArmorE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.ScaleArmour >= 1)
        //{
        //    scaleArmourE.gameObject.SetActive(true);
        //    scaleArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ScaleArmour;
        //}
        //else
        //    scaleArmourE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.ChainArmour >= 1)
        //{
        //    chainArmorE.gameObject.SetActive(true);
        //    chainArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ChainArmour;
        //}
        //else
        //    chainmailArmorE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.RingMailArmour >= 1)
        //{
        //    ringMailArmorE.gameObject.SetActive(true);
        //    ringMailArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.RingMailArmour;
        //}
        //else
        //    ringMailArmorE.gameObject.SetActive(false);

        if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.ChainArmour == 0 && Globals.shopMerchant.ScaleArmour == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Armour available";
            //clothArmorE.SetActive(true);
            //clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ClothArmour;
        }
        else
            noItem1.gameObject.SetActive(false);
    }
    void AllArmour()
    {
        paddedArmor.gameObject.SetActive(false);
        leatherArmor.gameObject.SetActive(false);
        guardArmor.gameObject.SetActive(false);
        chainmailArmor.gameObject.SetActive(false);
        scaleArmour.gameObject.SetActive(false);
        hideArmour.gameObject.SetActive(false);
        clothArmour.gameObject.SetActive(false);
    }
    void ArmourShowSmith()
    {
        if(Globals.shopMerchant.PaddedArmour <= 3 && Globals.shopMerchant.PaddedArmour > 0)
        {
            if(Globals.shopMerchant.PaddedArmour == 1)
            {
                if(Globals.inventoryMarium.PaddedArmour == 1 || Globals.inventoryJohn.PaddedArmour == 1 || Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 2)
            {
                if ((Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryJohn.PaddedArmour == 1) || (Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1) || (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1))
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 3)
            {
                if (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryJohn.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.LeatherArmour <= 3 && Globals.shopMerchant.LeatherArmour > 0)
        {
            if (Globals.shopMerchant.LeatherArmour == 1)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 || Globals.inventoryJohn.LeatherArmour == 1 || Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 2)
            {
                if ((Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryJohn.LeatherArmour == 1) || (Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1) || (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1))
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryJohn.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.BrigadineArmor <= 2 && Globals.shopMerchant.BrigadineArmor > 0)
        {
            if (Globals.shopMerchant.BrigadineArmor == 1)
            {
                if (Globals.inventoryMarium.BrigadineArmour == 1 || Globals.inventoryJohn.BrigadineArmour == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.BrigadineArmor == 2)
            {
                if (Globals.inventoryMarium.BrigadineArmour == 1 && Globals.inventoryJohn.BrigadineArmour == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.ChainArmour <= 1 && Globals.shopMerchant.ChainArmour > 0)
        {
            if (Globals.shopMerchant.ChainArmour == 1)
            {
                if (Globals.inventoryJohn.ChainArmour == 1)
                    chainmailArmor.gameObject.SetActive(false);
                else
                    chainmailArmor.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.ScaleArmour <= 1 && Globals.shopMerchant.ScaleArmour > 0)
        {
            if (Globals.shopMerchant.ScaleArmour == 1)
            {
                if (Globals.inventoryJohn.ScaleArmour == 1)
                    scaleArmour.gameObject.SetActive(false);
                else
                    scaleArmour.gameObject.SetActive(true);
            }
        }
    }
    void ArmourForSmithCharacter()
    {
        AllArmour();
        if (Globals.shopMerchant.PaddedArmour >= 1 || Globals.inventoryProtagnist.PaddedArmour>=1)
        {
            paddedArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour== "padded")
                paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            paddedArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 || Globals.inventoryProtagnist.LeatherArmour >= 1)
        {
            leatherArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Leahter")
                leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.BrigadineArmor >= 1 || Globals.inventoryProtagnist.BrigadineArmor >= 1)
        {
            guardArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Brigadine")
                guardArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.ChainArmour >= 1 || Globals.inventoryProtagnist.ChainArmour >= 1)
        {
            chainmailArmor.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Chainmail")
                chainmailArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                chainmailArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            chainmailArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.ScaleArmour >= 1 || Globals.inventoryProtagnist.ScaleArmour >= 1)
        {
            scaleArmour.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Armour == "Scale")
                scaleArmour.transform.GetChild(2).gameObject.SetActive(true);
            else
                scaleArmour.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            scaleArmour.gameObject.SetActive(false);

        ArmourShowSmith();

        if (!paddedArmor.gameObject.activeInHierarchy && !leatherArmor.gameObject.activeInHierarchy && !guardArmor.gameObject.activeInHierarchy && !chainmailArmor.gameObject.activeInHierarchy && !scaleArmour.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Armour available";
        }
        else
            noItem.gameObject.SetActive(false);
        //if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.ChainArmour == 0 && Globals.shopMerchant.ScaleArmour == 0)
        //{
        //    noItem.gameObject.SetActive(true);
        //    noItem.text = "No Armour available";
        //    //if (Globals.inventoryProtagnist.PaddedArmour == 0 && Globals.inventoryProtagnist.LeatherArmour == 0 && Globals.inventoryProtagnist.BrigadineArmor == 0 && Globals.inventoryProtagnist.ChainArmour == 0 && Globals.inventoryProtagnist.ScaleArmour == 0)
        //    //{
        //    //    //clothArmour.gameObject.SetActive(true);
        //    //    //clothArmour.transform.GetChild(2).gameObject.SetActive(true);
        //    //    noItem.gameObject.SetActive(true);
        //    //    noItem.text = "No Armour available";
        //    //}
        //    //else
        //    //    noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }

    void ArmourShowJohn()
    {
        if (Globals.shopMerchant.PaddedArmour <= 3 && Globals.shopMerchant.PaddedArmour > 0)
        {
            if (Globals.shopMerchant.PaddedArmour == 1)
            {
                if (Globals.inventoryMarium.PaddedArmour == 1 || Globals.inventoryProtagnist.PaddedArmour == 1 || Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 2)
            {
                if ((Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryProtagnist.PaddedArmour == 1) || (Globals.inventoryProtagnist.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1) || (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1))
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.PaddedArmour == 3)
            {
                if (Globals.inventoryMarium.PaddedArmour == 1 && Globals.inventoryProtagnist.PaddedArmour == 1 && Globals.inventoryTucker.PaddedArmour == 1)
                    paddedArmor.gameObject.SetActive(false);
                else
                    paddedArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.LeatherArmour <= 3 && Globals.shopMerchant.LeatherArmour > 0)
        {
            if (Globals.shopMerchant.LeatherArmour == 1)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 || Globals.inventoryProtagnist.LeatherArmour == 1 || Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 2)
            {
                if ((Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryProtagnist.LeatherArmour == 1) || (Globals.inventoryProtagnist.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1) || (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1))
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryMarium.LeatherArmour == 1 && Globals.inventoryProtagnist.LeatherArmour == 1 && Globals.inventoryTucker.LeatherArmour == 1)
                    leatherArmor.gameObject.SetActive(false);
                else
                    leatherArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.BrigadineArmor <= 2 && Globals.shopMerchant.BrigadineArmor > 0)
        {
            if (Globals.shopMerchant.BrigadineArmor == 1)
            {
                if (Globals.inventoryMarium.BrigadineArmour == 1 || Globals.inventoryProtagnist.BrigadineArmor == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.BrigadineArmor == 2)
            {
                if (Globals.inventoryMarium.BrigadineArmour == 1 && Globals.inventoryProtagnist.BrigadineArmor == 1)
                    guardArmor.gameObject.SetActive(false);
                else
                    guardArmor.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.ChainArmour <= 1 && Globals.shopMerchant.ChainArmour > 0)
        {
            if (Globals.shopMerchant.ChainArmour == 1)
            {
                if (Globals.inventoryJohn.ChainArmour == 1)
                    chainmailArmor.gameObject.SetActive(false);
                else
                    chainmailArmor.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.ScaleArmour <= 1 && Globals.shopMerchant.ScaleArmour > 0)
        {
            if (Globals.shopMerchant.ScaleArmour == 1)
            {
                if (Globals.inventoryProtagnist.ScaleArmour == 1)
                    scaleArmour.gameObject.SetActive(false);
                else
                    scaleArmour.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.HideArmour <= 3 && Globals.shopMerchant.HideArmour > 0)
        {
            if (Globals.shopMerchant.HideArmour == 1)
            {
                if (Globals.inventoryMarium.HideArmour == 1 || Globals.inventoryProtagnist.HideArmour == 1 || Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.HideArmour == 2)
            {
                if ((Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryProtagnist.HideArmour == 1) || (Globals.inventoryProtagnist.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1) || (Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1))
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherArmour == 3)
            {
                if (Globals.inventoryMarium.HideArmour == 1 && Globals.inventoryProtagnist.HideArmour == 1 && Globals.inventoryTucker.HideArmour == 1)
                    hideArmour.gameObject.SetActive(false);
                else
                    hideArmour.gameObject.SetActive(true);
            }
        }
    }
    void  ArmourForJohnCharacter()
    {
        AllArmour();
        if (Globals.shopMerchant.PaddedArmour >= 1 || Globals.inventoryJohn.PaddedArmour>=1)
        {
            paddedArmor.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Armour == "padded")
                paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            paddedArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 || Globals.inventoryJohn.LeatherArmour >= 1)
        {
            leatherArmor.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Armour == "Leahter")
                leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.BrigadineArmor >= 1 || Globals.inventoryJohn.BrigadineArmour >= 1)
        {
            guardArmor.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Armour == "Brigadine")
                guardArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.ChainArmour >= 1 || Globals.inventoryJohn.ChainArmour >= 1)
        {
            chainmailArmor.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Armour == "Chainmail")
                chainmailArmor.transform.GetChild(2).gameObject.SetActive(true);
            else
                chainmailArmor.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            chainmailArmor.gameObject.SetActive(false);
        if (Globals.shopMerchant.ScaleArmour >= 1 || Globals.inventoryJohn.ScaleArmour >= 1)
        {
            scaleArmour.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Armour == "Scale")
                scaleArmour.transform.GetChild(2).gameObject.SetActive(true);
            else
                scaleArmour.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            scaleArmour.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1 || Globals.inventoryJohn.HideArmour >= 1)
        {
            hideArmour.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Armour == "Hide")
                hideArmour.transform.GetChild(2).gameObject.SetActive(true);
            else
                hideArmour.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            hideArmour.gameObject.SetActive(false);

        ArmourShowJohn();
        if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.ChainArmour == 0 && Globals.shopMerchant.ScaleArmour == 0 && Globals.shopMerchant.HideArmour == 0)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Armour available";
            if (Globals.inventoryJohn.PaddedArmour == 0 && Globals.inventoryJohn.LeatherArmour == 0 && Globals.inventoryJohn.BrigadineArmour == 0 && Globals.inventoryJohn.ChainArmour == 0 && Globals.inventoryJohn.ScaleArmour == 0 && Globals.inventoryJohn.HideArmour == 0)
            {
                //noItem.gameObject.SetActive(true);  
                //noItem.text = "No Armour available";
                //paddedArmor.gameObject.SetActive(true);
                //paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
                noItem.gameObject.SetActive(false);
        }
        else
            noItem.gameObject.SetActive(false);
    }
    void ArmourForJohnCharacterEquipement()
    {
        HideAllArmor();
        //if (Globals.shopMerchant.ClothArmour >= 1)
        //{
        //    clothArmorE.gameObject.SetActive(true);
        //    clothArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ClothArmour;
        //}
        //else
        //    clothArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.PaddedArmour >= 1)
        {
            paddedArmorE.gameObject.SetActive(true);
            paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;
        }
        else
            paddedArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.HideArmour >= 1 )
        {
            hideArmourE.gameObject.SetActive(true);
            hideArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.HideArmour;
        }
        else
            hideArmourE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 )
        {
            leatherArmorE.gameObject.SetActive(true);
            leatherArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherArmour;
        }
        else
            leatherArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherArmour >= 1 || Globals.shopMerchant.BrigadineArmor >= 1)
        {
            brigadinArmorE.gameObject.SetActive(true);
            brigadinArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.BrigadineArmor;
        }
        else
            brigadinArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.ChainArmour >= 1 )
        {
            chainmailArmorE.gameObject.SetActive(true);
            chainmailArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ChainArmour;
        }
        else
            chainmailArmorE.gameObject.SetActive(false);
        if (Globals.shopMerchant.ScaleArmour >= 1)
        {
            scaleArmourE.gameObject.SetActive(true);
            scaleArmourE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ScaleArmour;
        }
        else
            scaleArmourE.gameObject.SetActive(false);
        //int total = Globals.shopMerchant.PaddedArmour + 1;
        //paddedArmorE.SetActive(true);
        //paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;

        if (Globals.shopMerchant.PaddedArmour == 0 && Globals.shopMerchant.LeatherArmour == 0 && Globals.shopMerchant.BrigadineArmor == 0 && Globals.shopMerchant.ChainArmour == 0 && Globals.shopMerchant.ScaleArmour == 0 && Globals.shopMerchant.HideArmour == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Armour available";
            //paddedArmorE.gameObject.SetActive(true);
            //paddedArmorE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.PaddedArmour;

        }
        else
            noItem1.gameObject.SetActive(false);
    }
    void AllHelmet()
    {
        leatherH.gameObject.SetActive(false);
        guardH.gameObject.SetActive(false);
        metalH.gameObject.SetActive(false);
        chainmailH.gameObject.SetActive(false);
        mailCoifH.gameObject.SetActive(false);
    }
    void HelmetSettingForProtagnist()
    {
        AllHelmet();
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale")
        {
            if (isCharacter)
                SmithHalmet();
            else if (isEquipement)
                JohnHelmetEquipment();
        }
        else if (Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale")
        {
            if (isCharacter)
                ArcherHelmet();
            else if (isEquipement)
                MariumHelmetEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (isCharacter)
                PriestHemletCharacter();
            else if (isEquipement)
                PriesttHelmetEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "John")
        {
            if (isCharacter)
                JohnHelmetCharacter();
            else if (isEquipement)
                JohnHelmetEquipment();
        }
        else if (Globals.selectedInventoryCharacter == "Marium")
        {
            if (isCharacter)
                MariumHelmetCharacter();
            else if (isEquipement)
                MariumHelmetEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "Tucker")
        {
            if (isCharacter)
                TuckerHelmet();
            else if (isEquipement)
                MariumHelmetEquipement();
        }
    }
    void MariumHelmetEquipement()
    {
        if (Globals.shopMerchant.LeatherCap >= 1)
        {
            leatherHE.gameObject.SetActive(true);
            leatherHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherCap;
        }
        else
            leatherHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 )
        {
            kettleHE.gameObject.SetActive(true);
            kettleHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.KettleHat;
        }
        else
            kettleHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.NesalHelmet >= 1)
        {
            nasalHE.gameObject.SetActive(true);
            nasalHE.transform.GetChild(3).GetComponent<Text>().text = "Total: "  + Globals.shopMerchant.NesalHelmet;
        }
        else
            nasalHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0 && Globals.shopMerchant.NesalHelmet == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Helmet available";
        }
        else
            noItem1.gameObject.SetActive(false);
    }
    void JohnHelmetEquipment()
    {
        if (Globals.shopMerchant.LeatherCap >= 1)
        {
            leatherHE.gameObject.SetActive(true);
            leatherHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherCap;
        }
        else
            leatherHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 )
        {
            kettleHE.gameObject.SetActive(true);
            kettleHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.KettleHat;
        }
        else
            kettleHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.NesalHelmet >= 1)
        {
            nasalHE.gameObject.SetActive(true);
            nasalHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.NesalHelmet;
        }
        else
            nasalHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Aventail >= 1)
        {
            aventailHE.gameObject.SetActive(true);
            aventailHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Aventail;
        }
        else
            aventailHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MailCoif >= 1)
        {
            mailCoifHE.gameObject.SetActive(true);
            mailCoifHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MailCoif;
        }
        else
            mailCoifHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0 && Globals.shopMerchant.NesalHelmet == 0 && Globals.shopMerchant.Aventail == 0 && Globals.shopMerchant.MailCoif == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Helmet available";
        }
        else
            noItem1.gameObject.SetActive(false);
    }
    void ShowSmithHelmet()
    {
        if(Globals.shopMerchant.LeatherCap <= 3 && Globals.shopMerchant.LeatherCap > 0)
        {
            if(Globals.shopMerchant.LeatherCap == 1)
            {
                if(Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 2)
            {
                if ((Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryMarium.LeatherCap == 1) || (Globals.inventoryMarium.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1))
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 3)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.KettleHat <= 3 && Globals.shopMerchant.KettleHat > 0)
        {
            if (Globals.shopMerchant.KettleHat == 1)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 2)
            {
                if ((Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryMarium.KettleHat == 1) || (Globals.inventoryMarium.KettleHat == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryTucker.KettleHat == 1))
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 3)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.NesalHelmet <= 2 && Globals.shopMerchant.NesalHelmet > 0)
        {
            if (Globals.shopMerchant.NesalHelmet == 1)
            {
                if (Globals.inventoryJohn.NasalHelmet == 1 || Globals.inventoryMarium.NasalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.NesalHelmet == 3)
            {
                if (Globals.inventoryJohn.NasalHelmet == 1 && Globals.inventoryMarium.NasalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
        }


        if (Globals.shopMerchant.Aventail <= 1 && Globals.shopMerchant.Aventail > 0)
        {
            if (Globals.shopMerchant.Aventail == 1)
            {
                if (Globals.inventoryJohn.Avaintail == 1)
                    chainmailH.gameObject.SetActive(false);
                else
                    chainmailH.gameObject.SetActive(true);
            }

        }
        if (Globals.shopMerchant.MailCoif <= 1 && Globals.shopMerchant.MailCoif > 0)
        {
            if (Globals.shopMerchant.MailCoif == 1)
            {
                if (Globals.inventoryJohn.MailCoif == 1)
                    mailCoifH.gameObject.SetActive(false);
                else
                    mailCoifH.gameObject.SetActive(true);
            }

        }
    }
    void SmithHalmet()
    {
        if (Globals.shopMerchant.LeatherCap >= 1 || Globals.inventoryProtagnist.LeatherCap>=1)
        {
            leatherH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet== "LeatherHelmet")
                leatherH.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherH.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 || Globals.inventoryProtagnist.KettleHat >= 1)
        {
            guardH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "KettleHelmet")
                guardH.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardH.gameObject.SetActive(false);
        if (Globals.shopMerchant.NesalHelmet >= 1 || Globals.inventoryProtagnist.NesalHelmet >= 1)
        {
            metalH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "NasalHelmet")
                metalH.transform.GetChild(2).gameObject.SetActive(true);
            else
                metalH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            metalH.gameObject.SetActive(false);
        if (Globals.shopMerchant.Aventail >= 1 || Globals.inventoryProtagnist.Aventail >= 1)
        {
            chainmailH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "AvainTail")
                chainmailH.transform.GetChild(2).gameObject.SetActive(true);
            else
                chainmailH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            chainmailH.gameObject.SetActive(false);
        if (Globals.shopMerchant.MailCoif >= 1 || Globals.inventoryProtagnist.MailCoif >= 1)
        {
            mailCoifH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "MailCoif")
                mailCoifH.transform.GetChild(2).gameObject.SetActive(true);
            else
                mailCoifH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            mailCoifH.gameObject.SetActive(false);

        ShowSmithHelmet();
        if (!leatherH.gameObject.activeInHierarchy && !guardH.gameObject.activeInHierarchy && !metalH.gameObject.activeInHierarchy && !chainmailH.gameObject.activeInHierarchy && !mailCoifH.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Helmet available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }
        //if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0 && Globals.shopMerchant.NesalHelmet == 0 && Globals.shopMerchant.Aventail == 0 && Globals.shopMerchant.MailCoif == 0)
        //{
        //    if (Globals.inventoryProtagnist.LeatherCap == 0 && Globals.inventoryProtagnist.KettleHat == 0 && Globals.inventoryProtagnist.NesalHelmet == 0 && Globals.inventoryProtagnist.Aventail == 0 && Globals.inventoryProtagnist.MailCoif == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Helmet available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }

    void ShowJohnHelmet()
    {
        if (Globals.shopMerchant.LeatherCap <= 3 && Globals.shopMerchant.LeatherCap > 0)
        {
            if (Globals.shopMerchant.LeatherCap == 1)
            {
                if (Globals.inventoryProtagnist.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 2)
            {
                if ((Globals.inventoryProtagnist.LeatherCap == 1 && Globals.inventoryMarium.LeatherCap == 1) || (Globals.inventoryMarium.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryProtagnist.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1))
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 3)
            {
                if (Globals.inventoryProtagnist.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.KettleHat <= 3 && Globals.shopMerchant.KettleHat > 0)
        {
            if (Globals.shopMerchant.KettleHat == 1)
            {
                if (Globals.inventoryProtagnist.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 2)
            {
                if ((Globals.inventoryProtagnist.KettleHat == 1 && Globals.inventoryMarium.KettleHat == 1) || (Globals.inventoryMarium.KettleHat == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryProtagnist.KettleHat == 1 && Globals.inventoryTucker.KettleHat == 1))
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 3)
            {
                if (Globals.inventoryProtagnist.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.NesalHelmet <= 2 && Globals.shopMerchant.NesalHelmet > 0)
        {
            if (Globals.shopMerchant.NesalHelmet == 1)
            {
                if (Globals.inventoryProtagnist.NesalHelmet == 1 || Globals.inventoryMarium.NasalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.NesalHelmet == 3)
            {
                if (Globals.inventoryProtagnist.NesalHelmet == 1 && Globals.inventoryMarium.NasalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
        }


        if (Globals.shopMerchant.Aventail <= 1 && Globals.shopMerchant.Aventail > 0)
        {
            if (Globals.shopMerchant.Aventail == 1)
            {
                if (Globals.inventoryProtagnist.Aventail == 1)
                    chainmailH.gameObject.SetActive(false);
                else
                    chainmailH.gameObject.SetActive(true);
            }

        }
        if (Globals.shopMerchant.MailCoif <= 1 && Globals.shopMerchant.MailCoif > 0)
        {
            if (Globals.shopMerchant.MailCoif == 1)
            {
                if (Globals.inventoryProtagnist.MailCoif == 1)
                    mailCoifH.gameObject.SetActive(false);
                else
                    mailCoifH.gameObject.SetActive(true);
            }

        }
    }
    void JohnHelmetCharacter()
    {
        if (Globals.shopMerchant.LeatherCap >= 1 || Globals.inventoryJohn.LeatherCap>=1)
        {
            leatherH.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Helmet== "LeatherHelmet")
                leatherH.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherH.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 || Globals.inventoryJohn.KettleHat >= 1)
        {
            guardH.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Helmet == "KettleHelmet")
                guardH.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardH.gameObject.SetActive(false);
        if (Globals.shopMerchant.NesalHelmet >= 1 || Globals.inventoryJohn.NasalHelmet >= 1)
        {
            metalH.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Helmet == "NasalHelmet")
                metalH.transform.GetChild(2).gameObject.SetActive(true);
            else
                metalH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            metalH.gameObject.SetActive(false);
        if (Globals.shopMerchant.Aventail >= 1 || Globals.inventoryJohn.Avaintail >= 1)
        {
            chainmailH.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Helmet == "AvainTail")
                chainmailH.transform.GetChild(2).gameObject.SetActive(true);
            else
                chainmailH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            chainmailH.gameObject.SetActive(false);
        if (Globals.shopMerchant.MailCoif >= 1 || Globals.inventoryJohn.MailCoif >= 1)
        {
            mailCoifH.gameObject.SetActive(true);
            if (Globals.inventoryJohn.Helmet == "MailCoif")
                mailCoifH.transform.GetChild(2).gameObject.SetActive(true);
            else
                mailCoifH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            mailCoifH.gameObject.SetActive(false);

        ShowJohnHelmet();
        if (!leatherH.gameObject.activeInHierarchy && !guardH.gameObject.activeInHierarchy && !metalH.gameObject.activeInHierarchy && !chainmailH.gameObject.activeInHierarchy && !mailCoifH.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Helmet available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }
        //if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0 && Globals.shopMerchant.NesalHelmet == 0 && Globals.shopMerchant.Aventail == 0 && Globals.shopMerchant.MailCoif == 0)
        //{
        //    if (Globals.inventoryJohn.LeatherCap == 0 && Globals.inventoryJohn.KettleHat == 0 && Globals.inventoryJohn.NasalHelmet == 0 && Globals.inventoryJohn.Avaintail == 0 && Globals.inventoryJohn.MailCoif == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Helmet available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ShowArcherHelmet()
    {
        if (Globals.shopMerchant.LeatherCap <= 3 && Globals.shopMerchant.LeatherCap > 0)
        {
            if (Globals.shopMerchant.LeatherCap == 1)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 2)
            {
                if ((Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryMarium.LeatherCap == 1) || (Globals.inventoryMarium.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1))
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 3)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.KettleHat <= 3 && Globals.shopMerchant.KettleHat > 0)
        {
            if (Globals.shopMerchant.KettleHat == 1)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 2)
            {
                if ((Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryMarium.KettleHat == 1) || (Globals.inventoryMarium.KettleHat == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryTucker.KettleHat == 1))
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 3)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.NesalHelmet <= 2 && Globals.shopMerchant.NesalHelmet > 0)
        {
            if (Globals.shopMerchant.NesalHelmet == 1)
            {
                if (Globals.inventoryJohn.NasalHelmet == 1 || Globals.inventoryMarium.NasalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.NesalHelmet == 3)
            {
                if (Globals.inventoryJohn.NasalHelmet == 1 && Globals.inventoryMarium.NasalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
        }



    }

    void ArcherHelmet()
    {
        if (Globals.shopMerchant.LeatherCap >= 1 || Globals.inventoryProtagnist.LeatherCap >= 1)
        {
            leatherH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "LeatherHelmet")
                leatherH.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherH.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 || Globals.inventoryProtagnist.KettleHat >= 1)
        {
            guardH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "KettleHelmet")
                guardH.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardH.gameObject.SetActive(false);
        if (Globals.shopMerchant.NesalHelmet >= 1 || Globals.inventoryProtagnist.NesalHelmet >= 1)
        {
            metalH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "NasalHelmet")
                metalH.transform.GetChild(2).gameObject.SetActive(true);
            else
                metalH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            metalH.gameObject.SetActive(false);
        ShowArcherHelmet();

        if (!leatherH.gameObject.activeInHierarchy && !guardH.gameObject.activeInHierarchy && !metalH.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Helmet available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
            //if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0 && Globals.shopMerchant.NesalHelmet == 0)
            //{
            //    if (Globals.inventoryProtagnist.LeatherCap == 0 && Globals.inventoryProtagnist.KettleHat == 0 && Globals.inventoryProtagnist.NesalHelmet == 0)
            //    {
            //        noItem.gameObject.SetActive(true);
            //        noItem.text = "No Helmet available";
            //    }
            //    else
            //        noItem.gameObject.SetActive(false);
            //}
            //else
            //    noItem.gameObject.SetActive(false);
        }
    }
    void ShowTuckerHelmet()
    {
        if (Globals.shopMerchant.LeatherCap <= 3 && Globals.shopMerchant.LeatherCap > 0)
        {
            if (Globals.shopMerchant.LeatherCap == 1)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryProtagnist.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 2)
            {
                if ((Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryProtagnist.LeatherCap == 1) || (Globals.inventoryProtagnist.LeatherCap == 1 && Globals.inventoryMarium.LeatherCap == 1) || (Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryMarium.LeatherCap == 1))
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 3)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryProtagnist.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.KettleHat <= 3 && Globals.shopMerchant.KettleHat > 0)
        {
            if (Globals.shopMerchant.KettleHat == 1)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryProtagnist.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 2)
            {
                if ((Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryProtagnist.KettleHat == 1) || (Globals.inventoryProtagnist.KettleHat == 1 && Globals.inventoryMarium.LeatherCap == 1) || (Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryMarium.KettleHat == 1))
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 3)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryProtagnist.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
        }


    }
    void TuckerHelmet()
    {
        if (Globals.shopMerchant.LeatherCap >= 1 || Globals.inventoryTucker.LeatherCap >= 1)
        {
            leatherH.gameObject.SetActive(true);
            if (Globals.inventoryTucker.Helmet== "LeatherHelmet")
                leatherH.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherH.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 || Globals.inventoryTucker.KettleHat >= 1)
        {
            guardH.gameObject.SetActive(true);
            if (Globals.inventoryTucker.Helmet == "KettleHelmet")
                guardH.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardH.gameObject.SetActive(false);

        ShowTuckerHelmet();
        if (!leatherH.gameObject.activeInHierarchy && !guardH.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Helmet available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }
        //if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0 && Globals.shopMerchant.NesalHelmet == 0)
        //{
        //    if (Globals.inventoryTucker.LeatherCap == 0 && Globals.inventoryTucker.KettleHat == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Helmet available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ShowMariumHelmet()
    {
        if (Globals.shopMerchant.LeatherCap <= 3 && Globals.shopMerchant.LeatherCap > 0)
        {
            if (Globals.shopMerchant.LeatherCap == 1)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryProtagnist.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 2)
            {
                if ((Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryProtagnist.LeatherCap == 1) || (Globals.inventoryProtagnist.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1))
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 3)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryProtagnist.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.KettleHat <= 3 && Globals.shopMerchant.KettleHat > 0)
        {
            if (Globals.shopMerchant.KettleHat == 1)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryProtagnist.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 2)
            {
                if ((Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryProtagnist.KettleHat == 1) || (Globals.inventoryProtagnist.KettleHat == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryTucker.KettleHat == 1))
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 3)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryProtagnist.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.NesalHelmet <= 2 && Globals.shopMerchant.NesalHelmet > 0)
        {
            if (Globals.shopMerchant.NesalHelmet == 1)
            {
                if (Globals.inventoryJohn.NasalHelmet == 1 || Globals.inventoryProtagnist.NesalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.NesalHelmet == 3)
            {
                if (Globals.inventoryJohn.NasalHelmet == 1 && Globals.inventoryProtagnist.NesalHelmet == 1)
                    metalH.gameObject.SetActive(false);
                else
                    metalH.gameObject.SetActive(true);
            }
        }   
    }
    void MariumHelmetCharacter()
    {
        if (Globals.shopMerchant.LeatherCap >= 1 || Globals.inventoryMarium.LeatherCap >= 1)
        {
            leatherH.gameObject.SetActive(true);
            if (Globals.inventoryMarium.Helmet== "LeatherHelmet")
                leatherH.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherH.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 || Globals.inventoryMarium.KettleHat >= 1)
        {
            guardH.gameObject.SetActive(true);
            if (Globals.inventoryMarium.Helmet == "KettleHelmet")
                guardH.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardH.gameObject.SetActive(false);
        if (Globals.shopMerchant.NesalHelmet >= 1 || Globals.inventoryMarium.NasalHelmet >= 1)
        {
            metalH.gameObject.SetActive(true);
            if (Globals.inventoryMarium.Helmet == "NasalHelmet")
                metalH.transform.GetChild(2).gameObject.SetActive(true);
            else
                metalH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            metalH.gameObject.SetActive(false);

        ShowMariumHelmet();
        if (!leatherH.gameObject.activeInHierarchy && !guardH.gameObject.activeInHierarchy && !metalH.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Helmet available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }
        //if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0 && Globals.shopMerchant.NesalHelmet == 0)
        //{
        //    if (Globals.inventoryMarium.LeatherCap == 0 && Globals.inventoryMarium.KettleHat == 0 && Globals.inventoryMarium.NasalHelmet == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Helmet available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ShowPriestHelmet()
    {
        if (Globals.shopMerchant.LeatherCap <= 3 && Globals.shopMerchant.LeatherCap > 0)
        {
            if (Globals.shopMerchant.LeatherCap == 1)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 2)
            {
                if ((Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryMarium.LeatherCap == 1) || (Globals.inventoryMarium.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.LeatherCap == 1 && Globals.inventoryTucker.LeatherCap == 1))
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.LeatherCap == 3)
            {
                if (Globals.inventoryJohn.LeatherCap == 1 || Globals.inventoryMarium.LeatherCap == 1 || Globals.inventoryTucker.LeatherCap == 1)
                    leatherH.gameObject.SetActive(false);
                else
                    leatherH.gameObject.SetActive(true);
            }
        }
        if (Globals.shopMerchant.KettleHat <= 3 && Globals.shopMerchant.KettleHat > 0)
        {
            if (Globals.shopMerchant.KettleHat == 1)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 2)
            {
                if ((Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryMarium.KettleHat == 1) || (Globals.inventoryMarium.KettleHat == 1 && Globals.inventoryTucker.LeatherCap == 1) || (Globals.inventoryJohn.KettleHat == 1 && Globals.inventoryTucker.KettleHat == 1))
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.KettleHat == 3)
            {
                if (Globals.inventoryJohn.KettleHat == 1 || Globals.inventoryMarium.KettleHat == 1 || Globals.inventoryTucker.KettleHat == 1)
                    guardH.gameObject.SetActive(false);
                else
                    guardH.gameObject.SetActive(true);
            }
        }


    }
    void PriestHemletCharacter()
    {
        if (Globals.shopMerchant.LeatherCap >= 1 || Globals.inventoryProtagnist.LeatherCap>=1)
        {
            leatherH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet== "LeatherHelmet")
                leatherH.transform.GetChild(2).gameObject.SetActive(true);
            else
                leatherH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            leatherH.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1 || Globals.inventoryProtagnist.KettleHat >= 1)
        {
            guardH.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.Helmet == "KettleHelmet")
                guardH.transform.GetChild(2).gameObject.SetActive(true);
            else
                guardH.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            guardH.gameObject.SetActive(false);

        ShowPriestHelmet();
        if (!leatherH.gameObject.activeInHierarchy && !guardH.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Helmet available";
        }
        else
            noItem.gameObject.SetActive(false);

        //if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0)
        //{
        //    if (Globals.inventoryProtagnist.LeatherCap == 0 && Globals.inventoryProtagnist.KettleHat == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Helmet available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void PriesttHelmetEquipement()
    {
        if (Globals.shopMerchant.LeatherCap >= 1)
        {
            leatherHE.gameObject.SetActive(true);
            leatherHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LeatherCap;
        }
        else
            leatherHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.KettleHat >= 1)
        {
            kettleHE.gameObject.SetActive(true);
            kettleHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.KettleHat;
        }
        else
            kettleHE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.NesalHelmet >= 1)
        //{
        //    nasalHE.gameObject.SetActive(true);
        //    nasalHE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.NesalHelmet;
        //}
        //else
        //    nasalHE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LeatherCap == 0 && Globals.shopMerchant.KettleHat == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Helmet available";
        }
        else
            noItem1.gameObject.SetActive(false);
    }
    void AllShield()
    {
        shield.gameObject.SetActive(false);
        woodenRound.gameObject.SetActive(false);
        metalBuckler.gameObject.SetActive(false);
        metalRound.gameObject.SetActive(false);
        woodenMedium.gameObject.SetActive(false);
        metalMedium.gameObject.SetActive(false);
    }
    void ShieldSettingForProtagnist()
    {
        AllShield();
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale")
        {
            if (isCharacter)
                ShieldForSmithCharacter();
            else if (isEquipement)
                ShieldForSmithEquipment();
        }
        else if (Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale")
        {
            if (isCharacter)
                ShieldForArcherCharacter();
            else if (isEquipement)
                ShieldForArcherEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (isCharacter)
                ShieldForSmithCharacter();
            else if(isEquipement)
                ShieldForSmithEquipment();
        }
        else if (Globals.selectedInventoryCharacter == "John")
        {
            if (isCharacter)
                JohnShield();
            else if (isEquipement)
                ShieldForJohnEquipment();
              //  ShieldForSmithEquipment();
        }
        else if (Globals.selectedInventoryCharacter == "Marium")
        {
            if (isCharacter)
                ShieldForMarium();
            else if (isEquipement)
                ShieldForArcherEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "Tucker")
        {
            if (isCharacter)
                ShieldForTucker();
            else if (isEquipement)
                ShieldForSmithEquipment();
        }
    }
    void ShieldForArcherEquipement()
    {
        if (Globals.shopMerchant.WoodenBuckler >= 1)
        {
            shieldE.gameObject.SetActive(true);
            shieldE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenBuckler;
        }
        else
            shieldE.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1)
        {
            woodenRoundE.gameObject.SetActive(true);
            woodenRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenSmallRounded;
        }
        else
            woodenRoundE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1)
        {
            metalBucklerE.gameObject.SetActive(true);
            metalBucklerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalBuckler;
        }
        else
            metalBucklerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalSmallRounded >= 1)
        {
            metalRoundE.gameObject.SetActive(true);
            metalRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalSmallRounded;
        }
        else
            metalRoundE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.WoodenBuckler >= 1 || Globals.shopMerchant.WoodenSmallRounded >= 1 || Globals.shopMerchant.WoodenMediumShield >= 1)
        //{
        //    shieldE.gameObject.SetActive(true);
        //    shieldE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenBuckler + Globals.shopMerchant.WoodenSmallRounded + Globals.shopMerchant.WoodenMediumShield;
        //}
        //else
        //    shieldE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.MetalBuckler >= 1 || Globals.shopMerchant.MetalSmallRounded >= 1 || Globals.shopMerchant.MetalMediumShield >= 1)
        //{
        //    woodenRoundE.gameObject.SetActive(true);
        //    woodenRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalBuckler + Globals.shopMerchant.MetalSmallRounded + Globals.shopMerchant.MetalMediumShield;
        //}
        //else
        //    woodenRoundE.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0)
        {
            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Shield available";
        }
        else
            noItem1.gameObject.SetActive(false);
    }

    void ShieldShowMarium()
    {
        if (Globals.shopMerchant.WoodenBuckler <= 3 && Globals.shopMerchant.WoodenBuckler > 0)
        {
            if (Globals.shopMerchant.WoodenBuckler == 1)
            {
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 || Globals.inventoryJohn.WoodenBuckler == 1 || Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 2)
            {
                if ((Globals.inventoryProtagnist.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryProtagnist.WoodenBuckler == 1))
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 3)
            {
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1 && Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.WoodenSmallRounded <= 3 && Globals.shopMerchant.WoodenSmallRounded > 0)
        {
            if (Globals.shopMerchant.WoodenSmallRounded == 1)
            {
                if (Globals.inventoryProtagnist.WoodenSmallRounded == 1 || Globals.inventoryJohn.WoodenSmallRound == 1 || Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 2)
            {
                if ((Globals.inventoryProtagnist.WoodenSmallRounded == 1 && Globals.inventoryJohn.WoodenSmallRound == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryJohn.WoodenSmallRound == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryProtagnist.WoodenSmallRounded == 1))
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 3)
            {
                if (Globals.inventoryProtagnist.WoodenSmallRounded == 1 && Globals.inventoryJohn.WoodenSmallRound == 1 && Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalBuckler <= 3 && Globals.shopMerchant.MetalBuckler > 0)
        {
            if (Globals.shopMerchant.MetalBuckler == 1)
            {
                if (Globals.inventoryProtagnist.MetalBuckler == 1 || Globals.inventoryJohn.metalBuckler == 1 || Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 2)
            {
                if ((Globals.inventoryProtagnist.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryProtagnist.MetalBuckler == 1))
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 3)
            {
                if (Globals.inventoryProtagnist.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1 && Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalSmallRounded <= 3 && Globals.shopMerchant.MetalSmallRounded > 0)
        {
            if (Globals.shopMerchant.MetalSmallRounded == 1)
            {
                if (Globals.inventoryProtagnist.MetalSmallRounded == 1 || Globals.inventoryJohn.metalSmallRound == 1 || Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 2)
            {
                if ((Globals.inventoryProtagnist.MetalSmallRounded == 1 && Globals.inventoryJohn.metalSmallRound == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryJohn.metalSmallRound == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryProtagnist.MetalSmallRounded == 1))
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 3)
            {
                if (Globals.inventoryProtagnist.MetalSmallRounded == 1 && Globals.inventoryJohn.metalSmallRound == 1 && Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
        }

       
    }
    void ShieldForMarium()
    {
        if (Globals.shopMerchant.WoodenBuckler >= 1 || Globals.inventoryMarium.WoodenBuckler >= 1)
        {
            shield.gameObject.SetActive(true);
            if (Globals.inventoryMarium.WoodenBuckler >= 1)
                shield.transform.GetChild(1).gameObject.SetActive(true);
            else
                shield.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shield.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1 || Globals.inventoryMarium.woodenSmall >= 1)
        {
            woodenRound.gameObject.SetActive(true);
            if (Globals.inventoryMarium.woodenSmall >= 1)
                woodenRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            woodenRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1 || Globals.inventoryMarium.MetalBuckler >= 1)
        {
            metalBuckler.gameObject.SetActive(true);
            if (Globals.inventoryMarium.MetalBuckler >= 1)
                metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalBuckler.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalSmallRounded >= 1 || Globals.inventoryMarium.MetalSmall >= 1)
        {
            metalRound.gameObject.SetActive(true);
            if (Globals.inventoryMarium.MetalSmall >= 1)
                metalRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalRound.gameObject.SetActive(false);

        ShieldShowMarium();
        if (!shield.gameObject.activeInHierarchy && !woodenRound.gameObject.activeInHierarchy && !metalBuckler.gameObject.activeInHierarchy && !metalRound.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Shield available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }
        //if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0)
        //{
        //    if (Globals.inventoryMarium.WoodenBuckler == 0 && Globals.inventoryMarium.woodenSmall == 0 && Globals.inventoryMarium.MetalBuckler == 0 && Globals.inventoryMarium.MetalSmall == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Shield available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ShieldShowArcher()
    {
        if (Globals.shopMerchant.WoodenBuckler <= 3 && Globals.shopMerchant.WoodenBuckler > 0)
        {
            if (Globals.shopMerchant.WoodenBuckler == 1)
            {
                if (Globals.inventoryJohn.WoodenBuckler == 1 || Globals.inventoryMarium.WoodenBuckler == 1 || Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 2)
            {
                if ((Globals.inventoryJohn.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1))
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 3)
            {
                if (Globals.inventoryJohn.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1 && Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.WoodenSmallRounded <= 3 && Globals.shopMerchant.WoodenSmallRounded > 0)
        {
            if (Globals.shopMerchant.WoodenSmallRounded == 1)
            {
                if (Globals.inventoryJohn.WoodenSmallRound == 1 || Globals.inventoryMarium.woodenSmall == 1 || Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 2)
            {
                if ((Globals.inventoryJohn.WoodenSmallRound == 1 && Globals.inventoryMarium.woodenSmall == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryMarium.woodenSmall == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryJohn.WoodenSmallRound == 1))
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 3)
            {
                if (Globals.inventoryJohn.WoodenSmallRound == 1 && Globals.inventoryMarium.woodenSmall == 1 && Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalBuckler <= 3 && Globals.shopMerchant.MetalBuckler > 0)
        {
            if (Globals.shopMerchant.MetalBuckler == 1)
            {
                if (Globals.inventoryJohn.metalBuckler == 1 || Globals.inventoryMarium.MetalBuckler == 1 || Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 2)
            {
                if ((Globals.inventoryJohn.metalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1))
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 3)
            {
                if (Globals.inventoryJohn.metalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1 && Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalSmallRounded <= 3 && Globals.shopMerchant.MetalSmallRounded > 0)
        {
            if (Globals.shopMerchant.MetalSmallRounded == 1)
            {
                if (Globals.inventoryJohn.metalSmallRound == 1 || Globals.inventoryMarium.MetalSmall == 1 || Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 2)
            {
                if ((Globals.inventoryJohn.metalSmallRound == 1 && Globals.inventoryMarium.MetalSmall == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryMarium.MetalSmall == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryJohn.metalSmallRound == 1))
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 3)
            {
                if (Globals.inventoryJohn.metalSmallRound == 1 && Globals.inventoryMarium.MetalSmall == 1 && Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
        }

       
    }
    void ShieldForArcherCharacter()
    {
        if (Globals.shopMerchant.WoodenBuckler >= 1 || Globals.inventoryProtagnist.WoodenBuckler >= 1)
        {
            shield.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.WoodenBuckler >= 1)
                shield.transform.GetChild(1).gameObject.SetActive(true);
            else
                shield.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shield.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1 || Globals.inventoryProtagnist.WoodenSmallRounded >= 1)
        {
            woodenRound.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.WoodenSmallRounded >= 1)
                woodenRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            woodenRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1 || Globals.inventoryProtagnist.MetalBuckler >= 1)
        {
            metalBuckler.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.MetalBuckler >= 1)
                metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalBuckler.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalSmallRounded >= 1 || Globals.inventoryProtagnist.MetalSmallRounded >= 1)
        {
            metalRound.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.MetalSmallRounded >= 1)
                metalRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalRound.gameObject.SetActive(false);

        ShieldShowArcher();

        if (!shield.gameObject.activeInHierarchy && !woodenRound.gameObject.activeInHierarchy && !metalBuckler.gameObject.activeInHierarchy && !metalRound.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Shield available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }

        //if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0)
        //{
        //    if (Globals.inventoryProtagnist.WoodenBuckler == 0 && Globals.inventoryProtagnist.WoodenSmallRounded == 0 && Globals.inventoryProtagnist.MetalBuckler == 0 && Globals.inventoryProtagnist.MetalSmallRounded == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Shield available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ShieldForSmithEquipment()
    {
        if (Globals.shopMerchant.WoodenBuckler >= 1 )
        {
            shieldE.gameObject.SetActive(true);
            shieldE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenBuckler ;
        }
        else
            shieldE.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1)
        {
            woodenRoundE.gameObject.SetActive(true);
            woodenRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenSmallRounded;
        }
        else
            woodenRoundE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1)
        {
            metalBucklerE.gameObject.SetActive(true);
            metalBucklerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalBuckler;
        }
        else
            metalBucklerE.gameObject.SetActive(false);
        if ( Globals.shopMerchant.MetalSmallRounded >= 1)
        {
            metalRoundE.gameObject.SetActive(true);
            metalRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalSmallRounded ;
        }
        else
            metalRoundE.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenMediumShield >= 1)
        {
            woodenMediumE.gameObject.SetActive(true);
            woodenMediumE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenMediumShield;
        }
        else
            woodenMediumE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalMediumShield >= 1)
        {
            metalMediumE.gameObject.SetActive(true);
            metalMediumE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalMediumShield;
        }
        else
            metalMediumE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.WoodenBuckler >= 1 || Globals.shopMerchant.WoodenSmallRounded>=1|| Globals.shopMerchant.WoodenMediumShield>=1)
        //{
        //    shieldE.gameObject.SetActive(true);
        //    shieldE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenBuckler + Globals.shopMerchant.WoodenSmallRounded + Globals.shopMerchant.WoodenMediumShield;
        //}
        //else
        //    shieldE.gameObject.SetActive(false);
        //if (Globals.shopMerchant.MetalBuckler >= 1 || Globals.shopMerchant.MetalSmallRounded>=1|| Globals.shopMerchant.MetalMediumShield>=1)
        //{
        //    woodenRoundE.gameObject.SetActive(true);
        //    woodenRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalBuckler + Globals.shopMerchant.MetalSmallRounded + Globals.shopMerchant.MetalMediumShield;
        //}
        //else
        //    woodenRoundE.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0 && Globals.shopMerchant.WoodenMediumShield == 0 && Globals.shopMerchant.MetalMediumShield == 0)
        {

            noItem1.gameObject.SetActive(true);
            noItem1.text = "No Shield available";
        }    
        else
            noItem1.gameObject.SetActive(false);
    }
    void ShieldForJohnEquipment()
    {
        if (Globals.shopMerchant.WoodenBuckler >= 1)
        {
            shieldE.gameObject.SetActive(true);
            shieldE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenBuckler;
        }
        else
            shieldE.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1)
        {
            woodenRoundE.gameObject.SetActive(true);
            woodenRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenSmallRounded;
        }
        else
            woodenRoundE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1)
        {
            metalBucklerE.gameObject.SetActive(true);
            metalBucklerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalBuckler;
        }
        else
            metalBucklerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalSmallRounded >= 1)
        {
            metalRoundE.gameObject.SetActive(true);
            metalRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalSmallRounded;
        }
        else
            metalRoundE.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenMediumShield >= 1)
        {
            woodenMediumE.gameObject.SetActive(true);
            woodenMediumE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.WoodenMediumShield;
        }
        else
            woodenMediumE.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalMediumShield >= 1)
        {
            metalMediumE.gameObject.SetActive(true);
            metalMediumE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.MetalMediumShield;
        }
        else
            metalMediumE.gameObject.SetActive(false);

        //if (Globals.selectedInventoryCharacter == "John")
        //{
        //    int total = Globals.shopMerchant.WoodenSmallRounded;
        //    woodenRoundE.gameObject.SetActive(true);
        //    woodenRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //}

        //if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0 && Globals.shopMerchant.WoodenMediumShield == 0 && Globals.shopMerchant.MetalMediumShield == 0)
        //{
        //    if (Globals.selectedInventoryCharacter == "John")
        //    {
        //        int total = Globals.shopMerchant.WoodenSmallRounded;
        //        woodenRoundE.gameObject.SetActive(true);
        //        woodenRoundE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //    }


        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }

    void ShieldShowJohn()
    {
        if (Globals.shopMerchant.WoodenBuckler <= 3 && Globals.shopMerchant.WoodenBuckler > 0)
        {
            if (Globals.shopMerchant.WoodenBuckler == 1)
            {
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 || Globals.inventoryMarium.WoodenBuckler == 1 || Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 2)
            {
                if ((Globals.inventoryProtagnist.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryProtagnist.WoodenBuckler == 1))
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 3)
            {
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1 && Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.WoodenSmallRounded <= 3 && Globals.shopMerchant.WoodenSmallRounded > 0)
        {
            if (Globals.shopMerchant.WoodenSmallRounded == 1)
            {
                if (Globals.inventoryProtagnist.WoodenSmallRounded == 1 || Globals.inventoryMarium.woodenSmall == 1 || Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 2)
            {
                if ((Globals.inventoryProtagnist.WoodenSmallRounded == 1 && Globals.inventoryMarium.woodenSmall == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryMarium.woodenSmall == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryProtagnist.WoodenSmallRounded == 1))
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 3)
            {
                if (Globals.inventoryProtagnist.WoodenSmallRounded == 1 && Globals.inventoryMarium.woodenSmall == 1 && Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalBuckler <= 3 && Globals.shopMerchant.MetalBuckler > 0)
        {
            if (Globals.shopMerchant.MetalBuckler == 1)
            {
                if (Globals.inventoryProtagnist.MetalBuckler == 1 || Globals.inventoryMarium.MetalBuckler == 1 || Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 2)
            {
                if ((Globals.inventoryProtagnist.MetalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryProtagnist.MetalBuckler == 1))
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 3)
            {
                if (Globals.inventoryProtagnist.MetalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1 && Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalSmallRounded <= 3 && Globals.shopMerchant.MetalSmallRounded > 0)
        {
            if (Globals.shopMerchant.MetalSmallRounded == 1)
            {
                if (Globals.inventoryProtagnist.MetalSmallRounded == 1 || Globals.inventoryMarium.MetalSmall == 1 || Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 2)
            {
                if ((Globals.inventoryProtagnist.MetalSmallRounded == 1 && Globals.inventoryMarium.MetalSmall == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryMarium.MetalSmall == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryProtagnist.MetalSmallRounded == 1))
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 3)
            {
                if (Globals.inventoryProtagnist.MetalSmallRounded == 1 && Globals.inventoryMarium.MetalSmall == 1 && Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.WoodenMediumShield <= 1 && Globals.shopMerchant.WoodenMediumShield > 0)
        {
            if (Globals.shopMerchant.WoodenMediumShield == 1)
            {
                if (Globals.inventoryProtagnist.WoodenMediumShield == 1)
                    woodenMedium.gameObject.SetActive(false);
                else
                    woodenMedium.gameObject.SetActive(true);
            }

        }
        if (Globals.shopMerchant.MetalMediumShield <= 2 && Globals.shopMerchant.MetalMediumShield > 0)
        {
            if (Globals.shopMerchant.MetalMediumShield == 1)
            {
                if (Globals.inventoryProtagnist.MetalMediumShield == 1 || Globals.inventoryTucker.MetalMedium == 1)
                    metalMedium.gameObject.SetActive(false);
                else
                    metalMedium.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalMediumShield == 2)
            {
                if (Globals.inventoryProtagnist.MetalMediumShield == 1 && Globals.inventoryTucker.MetalMedium == 1)
                    metalMedium.gameObject.SetActive(false);
                else
                    metalMedium.gameObject.SetActive(true);
            }

        }
    }
    void JohnShield()
    {
        //if (Globals.inventoryProtagnist.AttackWeapon == "ShortSword")
            if (Globals.shopMerchant.WoodenBuckler >= 1 || Globals.inventoryJohn.WoodenBuckler >= 1)
        {
            shield.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WoodenBuckler >= 1)
                shield.transform.GetChild(1).gameObject.SetActive(true);
            else
                shield.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shield.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1 || Globals.inventoryJohn.WoodenSmallRound >= 1 || Globals.inventoryJohn.Shield == "WoodenRound")
        {
            woodenRound.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WoodenSmallRound >= 1 || Globals.inventoryJohn.Shield == "WoodenRound")
                woodenRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            woodenRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1 || Globals.inventoryJohn.metalBuckler >= 1)
        {
            metalBuckler.gameObject.SetActive(true);
            if (Globals.inventoryJohn.metalBuckler >= 1)
                metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalBuckler.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalSmallRounded >= 1 || Globals.inventoryJohn.metalSmallRound >= 1)
        {
            metalRound.gameObject.SetActive(true);
            if (Globals.inventoryJohn.metalSmallRound >= 1)
                metalRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenMediumShield >= 1 || Globals.inventoryJohn.WoodenMedium >= 1)
        {
            woodenMedium.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WoodenMedium >= 1)
                woodenMedium.transform.GetChild(1).gameObject.SetActive(true);
            else
                woodenMedium.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            woodenMedium.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalMediumShield >= 1 || Globals.inventoryJohn.metalMedium >= 1)
        {
            metalMedium.gameObject.SetActive(true);
            if (Globals.inventoryJohn.metalMedium >= 1)
                metalMedium.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalMedium.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalMedium.gameObject.SetActive(false);

        ShieldShowJohn();
        if (!shield.gameObject.activeInHierarchy && !woodenRound.gameObject.activeInHierarchy && !metalBuckler.gameObject.activeInHierarchy && !metalRound.gameObject.activeInHierarchy && !metalMedium.gameObject.activeInHierarchy && !woodenMedium.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Shield available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }
        //if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0 && Globals.shopMerchant.WoodenMediumShield == 0 && Globals.shopMerchant.MetalMediumShield == 0)
        //{
        //    if (Globals.inventoryJohn.WoodenBuckler == 0 && Globals.inventoryJohn.WoodenSmallRound == 0 && Globals.inventoryJohn.metalBuckler == 0 && Globals.inventoryJohn.metalSmallRound == 0 && Globals.inventoryJohn.WoodenSmallRound == 0 && Globals.inventoryJohn.metalMedium == 0)
        //    {
        //        //noItem.gameObject.SetActive(true);
        //        //noItem.text = "No Shield available";
        //        woodenRound.gameObject.SetActive(true);
        //        woodenRound.transform.GetChild(1).gameObject.SetActive(true);
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ShieldShowTucker()
    {
        if (Globals.shopMerchant.WoodenBuckler <= 3 && Globals.shopMerchant.WoodenBuckler > 0)
        {
            if (Globals.shopMerchant.WoodenBuckler == 1)
            {
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 || Globals.inventoryJohn.WoodenBuckler == 1 || Globals.inventoryMarium.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 2)
            {
                if ((Globals.inventoryProtagnist.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1) || (Globals.inventoryMarium.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1) || (Globals.inventoryMarium.WoodenBuckler == 1 && Globals.inventoryProtagnist.WoodenBuckler == 1))
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 3)
            {
                if (Globals.inventoryProtagnist.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.WoodenSmallRounded <= 3 && Globals.shopMerchant.WoodenSmallRounded > 0)
        {
            if (Globals.shopMerchant.WoodenSmallRounded == 1)
            {
                if (Globals.inventoryProtagnist.WoodenSmallRounded == 1 || Globals.inventoryJohn.WoodenSmallRound == 1 || Globals.inventoryMarium.woodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 2)
            {
                if ((Globals.inventoryProtagnist.WoodenSmallRounded == 1 && Globals.inventoryJohn.WoodenSmallRound == 1) || (Globals.inventoryMarium.woodenSmall == 1 && Globals.inventoryJohn.WoodenSmallRound == 1) || (Globals.inventoryMarium.woodenSmall == 1 && Globals.inventoryProtagnist.WoodenSmallRounded == 1))
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 3)
            {
                if (Globals.inventoryProtagnist.WoodenSmallRounded == 1 && Globals.inventoryJohn.WoodenSmallRound == 1 && Globals.inventoryMarium.woodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalBuckler <= 3 && Globals.shopMerchant.MetalBuckler > 0)
        {
            if (Globals.shopMerchant.MetalBuckler == 1)
            {
                if (Globals.inventoryProtagnist.MetalBuckler == 1 || Globals.inventoryJohn.metalBuckler == 1 || Globals.inventoryMarium.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 2)
            {
                if ((Globals.inventoryProtagnist.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1) || (Globals.inventoryMarium.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1) || (Globals.inventoryMarium.MetalBuckler == 1 && Globals.inventoryProtagnist.MetalBuckler == 1))
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 3)
            {
                if (Globals.inventoryProtagnist.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalSmallRounded <= 3 && Globals.shopMerchant.MetalSmallRounded > 0)
        {
            if (Globals.shopMerchant.MetalSmallRounded == 1)
            {
                if (Globals.inventoryProtagnist.MetalSmallRounded == 1 || Globals.inventoryJohn.metalSmallRound == 1 || Globals.inventoryMarium.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 2)
            {
                if ((Globals.inventoryProtagnist.MetalSmallRounded == 1 && Globals.inventoryJohn.metalSmallRound == 1) || (Globals.inventoryMarium.MetalSmall == 1 && Globals.inventoryJohn.metalSmallRound == 1) || (Globals.inventoryMarium.MetalSmall == 1 && Globals.inventoryProtagnist.MetalSmallRounded == 1))
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 3)
            {
                if (Globals.inventoryProtagnist.MetalSmallRounded == 1 && Globals.inventoryJohn.metalSmallRound == 1 && Globals.inventoryMarium.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
        }


    }
    void ShieldForTucker()
    {
        if (Globals.shopMerchant.WoodenBuckler >= 1 || Globals.inventoryTucker.WoodenBuckler>=1)
        {
            shield.gameObject.SetActive(true);
            if (Globals.inventoryTucker.WoodenBuckler >= 1)
                shield.transform.GetChild(1).gameObject.SetActive(true);
            else
                shield.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shield.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1 || Globals.inventoryTucker.WoodenSmall >= 1)
        {
            woodenRound.gameObject.SetActive(true);
            if (Globals.inventoryTucker.WoodenSmall >= 1)
                woodenRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            woodenRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1 || Globals.inventoryTucker.MetalBuckler >= 1)
        {
            metalBuckler.gameObject.SetActive(true);
            if (Globals.inventoryTucker.MetalBuckler >= 1)
                metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalBuckler.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalSmallRounded >= 1 || Globals.inventoryTucker.MetalSmall >= 1)
        {
            metalRound.gameObject.SetActive(true);
            if (Globals.inventoryTucker.MetalSmall >= 1)
                metalRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalMediumShield >= 1 || Globals.inventoryTucker.MetalMedium >= 1)
        {
            metalMedium.gameObject.SetActive(true);
            if (Globals.inventoryTucker.MetalMedium >= 1)
                metalMedium.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalMedium.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalMedium.gameObject.SetActive(false);

        ShieldShowTucker();
        if (!shield.gameObject.activeInHierarchy && !woodenRound.gameObject.activeInHierarchy && !metalBuckler.gameObject.activeInHierarchy && !metalRound.gameObject.activeInHierarchy && !metalMedium.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Shield available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }
        //if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0 && Globals.shopMerchant.WoodenMediumShield == 0 && Globals.shopMerchant.MetalMediumShield == 0)
        //{
        //    if (Globals.inventoryTucker.WoodenBuckler == 0 && Globals.inventoryTucker.WoodenSmall == 0 && Globals.inventoryTucker.MetalBuckler == 0 && Globals.inventoryTucker.MetalSmall == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Shield available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
    void ShieldShowSmith()
    {
        if(Globals.shopMerchant.WoodenBuckler <= 3 && Globals.shopMerchant.WoodenBuckler > 0)
        {
            if(Globals.shopMerchant.WoodenBuckler == 1)
            {
                if(Globals.inventoryJohn.WoodenBuckler == 1 || Globals.inventoryMarium.WoodenBuckler == 1 || Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 2)
            {
                if ((Globals.inventoryJohn.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1) || (Globals.inventoryTucker.WoodenBuckler == 1 && Globals.inventoryJohn.WoodenBuckler == 1))
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenBuckler == 3)
            {
                if (Globals.inventoryJohn.WoodenBuckler == 1 && Globals.inventoryMarium.WoodenBuckler == 1 && Globals.inventoryTucker.WoodenBuckler == 1)
                    shield.gameObject.SetActive(false);
                else
                    shield.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.WoodenSmallRounded <= 3 && Globals.shopMerchant.WoodenSmallRounded > 0)
        {
            if (Globals.shopMerchant.WoodenSmallRounded == 1)
            {
                if (Globals.inventoryJohn.WoodenSmallRound == 1 || Globals.inventoryMarium.woodenSmall == 1 || Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 2)
            {
                if ((Globals.inventoryJohn.WoodenSmallRound == 1 && Globals.inventoryMarium.woodenSmall == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryMarium.woodenSmall == 1) || (Globals.inventoryTucker.WoodenSmall == 1 && Globals.inventoryJohn.WoodenSmallRound == 1))
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.WoodenSmallRounded == 3)
            {
                if (Globals.inventoryJohn.WoodenSmallRound == 1 && Globals.inventoryMarium.woodenSmall == 1 && Globals.inventoryTucker.WoodenSmall == 1)
                    woodenRound.gameObject.SetActive(false);
                else
                    woodenRound.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalBuckler <= 3 && Globals.shopMerchant.MetalBuckler > 0)
        {
            if (Globals.shopMerchant.MetalBuckler == 1)
            {
                if (Globals.inventoryJohn.metalBuckler == 1 || Globals.inventoryMarium.MetalBuckler == 1 || Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 2)
            {
                if ((Globals.inventoryJohn.metalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1) || (Globals.inventoryTucker.MetalBuckler == 1 && Globals.inventoryJohn.metalBuckler == 1))
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalBuckler == 3)
            {
                if (Globals.inventoryJohn.metalBuckler == 1 && Globals.inventoryMarium.MetalBuckler == 1 && Globals.inventoryTucker.MetalBuckler == 1)
                    metalBuckler.gameObject.SetActive(false);
                else
                    metalBuckler.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.MetalSmallRounded <= 3 && Globals.shopMerchant.MetalSmallRounded > 0)
        {
            if (Globals.shopMerchant.MetalSmallRounded == 1)
            {
                if (Globals.inventoryJohn.metalSmallRound == 1 || Globals.inventoryMarium.MetalSmall == 1 || Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 2)
            {
                if ((Globals.inventoryJohn.metalSmallRound == 1 && Globals.inventoryMarium.MetalSmall == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryMarium.MetalSmall == 1) || (Globals.inventoryTucker.MetalSmall == 1 && Globals.inventoryJohn.metalSmallRound == 1))
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalSmallRounded == 3)
            {
                if (Globals.inventoryJohn.metalSmallRound == 1 && Globals.inventoryMarium.MetalSmall == 1 && Globals.inventoryTucker.MetalSmall == 1)
                    metalRound.gameObject.SetActive(false);
                else
                    metalRound.gameObject.SetActive(true);
            }
        }

        if (Globals.shopMerchant.WoodenMediumShield <= 1 && Globals.shopMerchant.WoodenMediumShield > 0)
        {
            if (Globals.shopMerchant.WoodenMediumShield == 1)
            {
                if (Globals.inventoryJohn.WoodenMedium == 1 )
                    woodenMedium.gameObject.SetActive(false);
                else
                    woodenMedium.gameObject.SetActive(true);
            }

        }
        if (Globals.shopMerchant.MetalMediumShield <= 2 && Globals.shopMerchant.MetalMediumShield > 0)
        {
            if (Globals.shopMerchant.MetalMediumShield == 1)
            {
                if (Globals.inventoryJohn.metalMedium == 1 || Globals.inventoryTucker.MetalMedium == 1)
                    metalMedium.gameObject.SetActive(false);
                else
                    metalMedium.gameObject.SetActive(true);
            }
            if (Globals.shopMerchant.MetalMediumShield == 2)
            {
                if (Globals.inventoryJohn.metalMedium == 1 && Globals.inventoryTucker.MetalMedium == 1)
                    metalMedium.gameObject.SetActive(false);
                else
                    metalMedium.gameObject.SetActive(true);
            }

        }
    }
    void ShieldForSmithCharacter()
    {
        if (Globals.shopMerchant.WoodenBuckler >= 1 || Globals.inventoryProtagnist.WoodenBuckler >= 1)
        {
            shield.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.WoodenBuckler >= 1)
                shield.transform.GetChild(1).gameObject.SetActive(true);
            else
                shield.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shield.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenSmallRounded >= 1 || Globals.inventoryProtagnist.WoodenSmallRounded >= 1)
        {
            woodenRound.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.WoodenSmallRounded >= 1)
                woodenRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            woodenRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalBuckler >= 1 || Globals.inventoryProtagnist.MetalBuckler >= 1)
        {
            metalBuckler.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.MetalBuckler >= 1)
                metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalBuckler.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalSmallRounded >= 1 || Globals.inventoryProtagnist.MetalSmallRounded >= 1)
        {
            metalRound.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.MetalSmallRounded >= 1)
                metalRound.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalRound.gameObject.SetActive(false);
        if (Globals.shopMerchant.WoodenMediumShield >= 1 || Globals.inventoryProtagnist.WoodenMediumShield >= 1)
        {
            woodenMedium.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.WoodenMediumShield >= 1)
                woodenMedium.transform.GetChild(1).gameObject.SetActive(true);
            else
                woodenMedium.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            woodenMedium.gameObject.SetActive(false);
        if (Globals.shopMerchant.MetalMediumShield >= 1 || Globals.inventoryProtagnist.MetalMediumShield >= 1)
        {
            metalMedium.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.MetalMediumShield >= 1)
                metalMedium.transform.GetChild(1).gameObject.SetActive(true);
            else
                metalMedium.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            metalMedium.gameObject.SetActive(false);

        ShieldShowSmith();
        if(!shield.gameObject.activeInHierarchy && !woodenRound.gameObject.activeInHierarchy && !metalBuckler.gameObject.activeInHierarchy && !metalRound.gameObject.activeInHierarchy && !metalMedium.gameObject.activeInHierarchy && !woodenMedium.gameObject.activeInHierarchy)
        {
            noItem.gameObject.SetActive(true);
            noItem.text = "No Shield available";
        }
        else
        {
            noItem.gameObject.SetActive(false);
        }

        //if (Globals.shopMerchant.WoodenBuckler == 0 && Globals.shopMerchant.WoodenSmallRounded == 0 && Globals.shopMerchant.MetalBuckler == 0 && Globals.shopMerchant.MetalSmallRounded == 0 && Globals.shopMerchant.WoodenMediumShield == 0 && Globals.shopMerchant.MetalMediumShield == 0)
        //{
        //    if (Globals.inventoryProtagnist.WoodenBuckler == 0 && Globals.inventoryProtagnist.WoodenSmallRounded == 0 && Globals.inventoryProtagnist.MetalBuckler == 0 && Globals.inventoryProtagnist.MetalSmallRounded == 0 && Globals.inventoryProtagnist.WoodenMediumShield == 0 && Globals.inventoryProtagnist.MetalMediumShield == 0)
        //    {
        //        noItem.gameObject.SetActive(true);
        //        noItem.text = "No Shield available";
        //    }
        //    else
        //        noItem.gameObject.SetActive(false);
        //}
        //else
        //    noItem.gameObject.SetActive(false);
    }
  void  WeaponSettingForProtagnist()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale")
        {
            if (isCharacter)
                WeaponForSmithCharacter();
            else if (isEquipement)
                WeaponForSmithEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale")
        {
            if (isCharacter)
                WeaponForArcherCharacter();
            else if (isEquipement)
                WeaponForArcherEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            if (isCharacter)
                WeaponForPriestCharacter();
            else if (isEquipement)
                WeaponForPriestEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "John")
        {
            if (isCharacter)
                WeaponForJohnCharacter();
            else if (isEquipement)
                WeaponForJohnEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "Marium")
        {
            if (isCharacter)
                WeaponForMariumCharacter();
            else if (isEquipement)
                WeaponForMariumEquipement();
        }
        else if (Globals.selectedInventoryCharacter == "Tucker")
        {
            if (isCharacter)
                WeaponForTuckerCharacter();
            else if (isEquipement)
                WeaponForTuckerEquipement();
        }
    }
    void WeaponForTuckerEquipement()
    {
        AllEquipementWeapon();
        if (Globals.shopMerchant.Dragger >= 1)
        {
            draggerE.gameObject.SetActive(true);
            draggerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Dragger;
        }
        else
            draggerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Mace >= 1)
        {
            MaceE.gameObject.SetActive(true);
            MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Mace;
        }
        else
            MaceE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Flair >= 1)
        {
            flairE.gameObject.SetActive(true);
            totalClub = 0;
            totalClub = Globals.shopMerchant.Flair;
            flairE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + totalClub;
        }
        else
            flairE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1)
        {
            warHammerE.gameObject.SetActive(true);
            totalHammer = 0;
            totalHammer = Globals.shopMerchant.Warhammer + Globals.shopMerchant.Maul;
            warHammerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " +totalHammer ;
        }
        else
            warHammerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Maul >= 1)
        {
            maulE.gameObject.SetActive(true);
            totalHammer = 0;
            totalHammer = Globals.shopMerchant.Maul;
            maulE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + totalHammer;
        }
        else
            maulE.gameObject.SetActive(false);

        if (Globals.shopMerchant.Club >= 1)
        {
            clubE.gameObject.SetActive(true);
            totalClub = 0;
            totalClub =  Globals.shopMerchant.Club ;
            clubE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + totalClub;
        }
        else
            clubE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Mace == 0)
        {
            Globals.shopMerchant.Mace = 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        int total = Globals.shopMerchant.Mace + 1;
        MaceE.gameObject.SetActive(true);
        MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //if (Globals.shopMerchant.Mace == 0 && Globals.shopMerchant.Warhammer == 0 && Globals.shopMerchant.Dragger == 0 && Globals.shopMerchant.Flair == 0 && Globals.shopMerchant.Maul == 0 && Globals.shopMerchant.Club == 0)
        //{
        //    MaceE.gameObject.SetActive(true);
        //    MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Mace;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void AllWeapon()
    {
        Sword.gameObject.SetActive(false);
        shortAxe.gameObject.SetActive(false);
        warHammer.gameObject.SetActive(false);
        longAxe.gameObject.SetActive(false);
        Spear.gameObject.SetActive(false);
        longSword.gameObject.SetActive(false);
        shortBow.gameObject.SetActive(false);
        longBow.gameObject.SetActive(false);
        Club.gameObject.SetActive(false);
        flair.gameObject.SetActive(false);
        Maul.gameObject.SetActive(false);
        Mace.gameObject.SetActive(false);
        Dragger.gameObject.SetActive(false);
        magicSword.gameObject.SetActive(false);
    }
    void WeaponShowTucker()
    {

        if (Globals.shopMerchant.Mace <= 2 && Globals.shopMerchant.Mace > 0)
        {
            if (Globals.shopMerchant.Mace == 1)
            {
                if (Globals.inventoryJohn.Mace == 1 || Globals.inventoryProtagnist.Mace == 1)
                {
                    Mace.gameObject.SetActive(false);
                }
                else
                {
                    Mace.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Mace == 2)
            {
                if (Globals.inventoryJohn.Mace == 1 && Globals.inventoryProtagnist.Mace == 1)
                {
                    Mace.gameObject.SetActive(false);
                }
                else
                {
                    Mace.gameObject.SetActive(true);
                }
            }

        }

        if (Globals.shopMerchant.Warhammer <= 3 && Globals.shopMerchant.Warhammer > 0)
        {
            if (Globals.shopMerchant.Warhammer == 1)
            {
                if (Globals.inventoryJohn.Warhammer == 1 || Globals.inventoryProtagnist.Warhammer == 1 || Globals.inventoryMarium.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1) || (Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryMarium.Warhammer == 1) || (Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1))
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 3)
            {
                if (Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1 && Globals.inventoryMarium.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
        }



      
        if (Globals.shopMerchant.Maul <= 1 && Globals.shopMerchant.Maul > 0)
        {
            if (Globals.shopMerchant.Maul == 1)
            {
                if (Globals.inventoryProtagnist.Maul == 1)
                {
                    Maul.gameObject.SetActive(false);
                }
                else
                {
                    Maul.gameObject.SetActive(true);
                }
            }

        }

        if (Globals.shopMerchant.Flair <= 1 && Globals.shopMerchant.Flair > 0)
        {
            if (Globals.shopMerchant.Flair == 1)
            {
                if (Globals.inventoryProtagnist.Flair == 1)
                {
                    flair.gameObject.SetActive(false);
                }
                else
                {
                    flair.gameObject.SetActive(true);
                }
            }

        }

        if (Globals.shopMerchant.Dragger <= 3 && Globals.shopMerchant.Dragger > 0) // dagger
        {
            if (Globals.shopMerchant.Dragger == 1)
            {
                if (Globals.inventoryJohn.Dragger == 1 || Globals.inventoryProtagnist.Dragger == 1 || Globals.inventoryMarium.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryJohn.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1) || (Globals.inventoryJohn.Dragger == 1 && Globals.inventoryMarium.Dragger == 1) || (Globals.inventoryMarium.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1))
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Dragger == 3)
            {
                if (Globals.inventoryJohn.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1 && Globals.inventoryMarium.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
        }

    }
    void WeaponForTuckerCharacter()
    {
        AllWeapon();
        if (Globals.inventoryTucker.WeaponAttack == "Mace")
        {
            Mace.gameObject.SetActive(true);
            Mace.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            Mace.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1 || Globals.inventoryTucker.Warhammer>=1)
        {
            warHammer.gameObject.SetActive(true);
            Mace.gameObject.SetActive(true);
            if (Globals.inventoryTucker.WeaponAttack == "warHammer")
                warHammer.transform.GetChild(1).gameObject.SetActive(true);
            else
                warHammer.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            warHammer.gameObject.SetActive(false);
        if (Globals.shopMerchant.Flair >= 1 || Globals.inventoryTucker.Flair >= 1)
        {
            flair.gameObject.SetActive(true);
            Mace.gameObject.SetActive(true);
            if (Globals.inventoryTucker.WeaponAttack == "Flair")
                flair.transform.GetChild(1).gameObject.SetActive(true);
            else
                flair.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            flair.gameObject.SetActive(false);
        if (Globals.shopMerchant.Maul >= 1 || Globals.inventoryTucker.Maul >= 1)
        {
            Maul.gameObject.SetActive(true);
            Mace.gameObject.SetActive(true);
            if (Globals.inventoryTucker.WeaponAttack == "Maul")
                Maul.transform.GetChild(1).gameObject.SetActive(true);
            else
                Maul.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Maul.gameObject.SetActive(false);
        if (Globals.shopMerchant.Dragger >= 1 || Globals.inventoryTucker.Dragger >= 1)
        {
            Dragger.gameObject.SetActive(true);
            Mace.gameObject.SetActive(true);
            if (Globals.inventoryTucker.WeaponAttack == "Dragger")
                Dragger.transform.GetChild(1).gameObject.SetActive(true);
            else
                Dragger.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Dragger.gameObject.SetActive(false);

        WeaponShowTucker();
        //if (Globals.inventoryTucker.Warhammer == 0 && Globals.inventoryTucker.Dragger == 0 && Globals.inventoryTucker.Flair == 0 && Globals.inventoryTucker.Maul == 0)
        //{
        //    Mace.gameObject.SetActive(true);
        //    Mace.transform.GetChild(1).gameObject.SetActive(true);
        //}
    }
    void WeaponForMariumEquipement()
    {
        AllEquipementWeapon();
        if (Globals.shopMerchant.ShortSword >= 1)
        {
            shortSwordE.gameObject.SetActive(true);
            shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortSword;
        }
        else
            shortSwordE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Dragger >= 1 )
        {
            draggerE.gameObject.SetActive(true);

            draggerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Dragger;
        }
        else
            draggerE.gameObject.SetActive(false);

        if (Globals.shopMerchant.ShortAxe >= 1)
        {
            shortAxeE.gameObject.SetActive(true);
            shortAxeE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortAxe;
        }
        else
            shortAxeE.gameObject.SetActive(false);

        if (Globals.shopMerchant.Warhammer >= 1 )
        {
            warHammerE.gameObject.SetActive(true);
            warHammerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Warhammer;
        }
        else
            warHammerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Spear >= 1)
        {
            spearE.gameObject.SetActive(true);
            spearE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Spear;
        }
        else
            spearE.gameObject.SetActive(false);

        if (Globals.shopMerchant.ShortBow >= 1)
        {
            shortBowE.gameObject.SetActive(true);
            shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortBow;
        }
        else
            shortBowE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongBow >= 1)
        {
            longBowE.gameObject.SetActive(true);
            longBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LongBow;
        }
        else
            longBowE.gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortBow == 0)
        {
            Globals.shopMerchant.ShortBow = 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        //int total = Globals.shopMerchant.ShortBow + 1;
        //shortBowE.gameObject.SetActive(true);
        //shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;

        //if (Globals.shopMerchant.Dragger == 0 && Globals.shopMerchant.ShortSword == 0 && Globals.shopMerchant.ShortAxe == 0 && Globals.shopMerchant.ShortBow == 0 && Globals.shopMerchant.Warhammer == 0 && Globals.shopMerchant.Spear == 0 && Globals.shopMerchant.LongBow == 0)
        //{
        //    shortBowE.gameObject.SetActive(true);
        //    shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortBow;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void WeaponShowMarium()
    {
        if (Globals.shopMerchant.ShortAxe <= 2 && Globals.shopMerchant.ShortAxe > 0)
        {
            if (Globals.shopMerchant.ShortAxe == 1)
            {
                if (Globals.inventoryJohn.ShortAxe == 1 || Globals.inventoryProtagnist.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.ShortAxe == 2)
            {
                if (Globals.inventoryJohn.ShortAxe == 1 && Globals.inventoryProtagnist.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }

        }
        if (Globals.shopMerchant.ShortSword <= 2 && Globals.shopMerchant.ShortSword > 0)
        {
            if (Globals.shopMerchant.ShortSword == 1)
            {
                if (Globals.inventoryJohn.ShortSword == 1 || Globals.inventoryProtagnist.ShortSword == 1)
                {
                    Sword.gameObject.SetActive(false);
                }
                else
                {
                    Sword.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.ShortSword == 2)
            {
                if (Globals.inventoryJohn.ShortSword == 1 && Globals.inventoryProtagnist.ShortSword == 1)
                {
                    Sword.gameObject.SetActive(false);
                }
                else
                {
                    Sword.gameObject.SetActive(true);
                }
            }

        }

        if (Globals.shopMerchant.Warhammer <= 3 && Globals.shopMerchant.Warhammer > 0)
        {
            if (Globals.shopMerchant.Warhammer == 1)
            {
                if (Globals.inventoryJohn.Warhammer == 1 || Globals.inventoryProtagnist.Warhammer == 1 || Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1) || (Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1) || (Globals.inventoryTucker.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1))
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 3)
            {
                if (Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
        }

        if (Globals.shopMerchant.ShortBow <= 1 && Globals.shopMerchant.ShortBow > 0)
        {
            if (Globals.shopMerchant.ShortBow == 1)
            {
                if (Globals.inventoryProtagnist.ShortBow == 1)
                {
                    shortBow.gameObject.SetActive(false);
                }
                else
                {
                    shortBow.gameObject.SetActive(true);
                }
            }

        }

        if (Globals.shopMerchant.Spear <= 2 && Globals.shopMerchant.Spear > 0)
        {
            if (Globals.shopMerchant.Spear == 1)
            {
                if (Globals.inventoryJohn.Spear == 1 || Globals.inventoryProtagnist.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Spear == 2)
            {
                if (Globals.inventoryJohn.Spear == 1 && Globals.inventoryProtagnist.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }

        }

        if (Globals.shopMerchant.LongBow <= 1 && Globals.shopMerchant.LongBow > 0)
        {
            if (Globals.shopMerchant.LongBow == 1)
            {
                if (Globals.inventoryProtagnist.LongBow == 1)
                {
                    longBow.gameObject.SetActive(false);
                }
                else
                {
                    longBow.gameObject.SetActive(true);
                }
            }

        }


       

        if (Globals.shopMerchant.Dragger <= 3 && Globals.shopMerchant.Dragger > 0) // dagger
        {
            if (Globals.shopMerchant.Dragger == 1)
            {
                if (Globals.inventoryJohn.Dragger == 1 || Globals.inventoryProtagnist.Dragger == 1 || Globals.inventoryTucker.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryJohn.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1) || (Globals.inventoryJohn.Dragger == 1 && Globals.inventoryTucker.Dragger == 1) || (Globals.inventoryTucker.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1))
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Dragger == 3)
            {
                if (Globals.inventoryJohn.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1 && Globals.inventoryTucker.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
        }

    }
    void WeaponForMariumCharacter()
    {
        AllWeapon();
        if (Globals.inventoryMarium.ShortBow >= 1) //Globals.inventoryMarium.WeaponAttack == "shortBow"
        {
            shortBow.gameObject.SetActive(true);
            if(Globals.inventoryMarium.WeaponAttack == "shortBow")
                shortBow.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            shortBow.gameObject.SetActive(true);
            shortBow.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (Globals.shopMerchant.ShortSword >= 1 || Globals.inventoryMarium.ShortSword >= 1)
        {
            Sword.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryMarium.WeaponAttack == "ShortSword")
                Sword.transform.GetChild(1).gameObject.SetActive(true);
            else
                Sword.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Sword.gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortAxe >= 1 || Globals.inventoryMarium.ShortAxe >= 1)
        {
            shortAxe.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryMarium.WeaponAttack == "ShortAxe")
                shortAxe.transform.GetChild(1).gameObject.SetActive(true);
            else
                shortAxe.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shortAxe.gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1 || Globals.inventoryMarium.Warhammer >= 1)
        {
            warHammer.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryMarium.WeaponAttack == "warHammer")
                warHammer.transform.GetChild(1).gameObject.SetActive(true);
            else
                warHammer.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            warHammer.gameObject.SetActive(false);
        if (Globals.shopMerchant.Spear >= 1 || Globals.inventoryMarium.Spear >= 1)
        {
            Spear.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryMarium.WeaponAttack == "Spear")
                Spear.transform.GetChild(1).gameObject.SetActive(true);
            else
                Spear.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Spear.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongBow >= 1 || Globals.inventoryMarium.LongBow >= 1)
        {
            longBow.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryMarium.WeaponAttack == "longBow")
                longBow.transform.GetChild(1).gameObject.SetActive(true);
            else
                longBow.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            longBow.gameObject.SetActive(false);
        if (Globals.shopMerchant.Dragger >= 1 || Globals.inventoryMarium.Dragger >= 1)
        {
            Dragger.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryMarium.WeaponAttack == "Dragger")
                Dragger.transform.GetChild(1).gameObject.SetActive(true);
            else
                Dragger.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Dragger.gameObject.SetActive(false);

        WeaponShowMarium();
        //if (Globals.inventoryMarium.Dragger == 0 && Globals.inventoryMarium.ShortSword == 0 && Globals.inventoryMarium.ShortBow == 0 && Globals.inventoryMarium.Warhammer == 0 && Globals.inventoryMarium.Spear == 0 && Globals.inventoryMarium.LongBow == 0)
        //{
        //    shortBow.gameObject.SetActive(true);
        //    shortBow.transform.GetChild(1).gameObject.SetActive(true);
        //}
    }
    void WeaponForPriestEquipement()
    {
        AllEquipementWeapon();
        if (Globals.shopMerchant.Dragger >= 1)
        {
            draggerE.gameObject.SetActive(true);
            draggerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Dragger;
        }
        else
            draggerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1 )
        {
            warHammerE.gameObject.SetActive(true);
            warHammerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Warhammer;
        }
        else
            warHammerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Mace >= 1)
        {
            MaceE.gameObject.SetActive(true);
            MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Mace;
        }
        else
            MaceE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Flair >= 1)
        {
            flairE.gameObject.SetActive(true);
            flairE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Flair;
        }
        else
            flairE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Maul >= 1)
        {
            maulE.gameObject.SetActive(true);
            maulE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Maul;
        }
        else
            maulE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Club >= 1)
        {
            clubE.gameObject.SetActive(true);
            clubE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Club;
        }
        else
            clubE.gameObject.SetActive(false);
        if (Globals.isLightening)
        {
            magicSwordE.gameObject.SetActive(true);
            magicSwordE.transform.GetChild(0).GetComponent<Text>().text = "Magic Mace";
        }
        else
            magicSwordE.gameObject.SetActive(false);

        if (Globals.shopMerchant.Mace == 0)
        {
            Globals.shopMerchant.Mace = 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        int total = Globals.shopMerchant.Mace + 1;
        MaceE.gameObject.SetActive(true);
        MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //if (Globals.shopMerchant.Warhammer == 0 && Globals.shopMerchant.Mace == 0 && Globals.shopMerchant.Flair == 0 && Globals.shopMerchant.Maul == 0 && Globals.shopMerchant.Dragger == 0)
        //{
        //    MaceE.gameObject.SetActive(true);
        //    MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Mace;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void WeaponForJohnEquipement()
    {
        AllEquipementWeapon();
        if (Globals.shopMerchant.Dragger >= 1)
        {
            draggerE.gameObject.SetActive(true);
            draggerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Dragger;
        }
        else
            draggerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortSword >= 1)
        {
            shortSwordE.gameObject.SetActive(true);
            shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortSword;
        }
        else
            shortSwordE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongSword >= 1)
        {
            longSwordE.gameObject.SetActive(true);
            longSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LongSword;
        }
        else
            longSwordE.gameObject.SetActive(false);

        if (Globals.shopMerchant.ShortAxe >= 1 )
        {
            shortAxeE.gameObject.SetActive(true);
            shortAxeE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortAxe;
        }
        else
            shortAxeE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongAxe >= 1)
        {
            longAxeE.gameObject.SetActive(true);
            longAxeE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LongAxe;
        }
        else
            longAxeE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Mace >= 1)
        {
            MaceE.gameObject.SetActive(true);
            MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Mace;
        }
        else
            MaceE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Club >= 1)
        {
            clubE.gameObject.SetActive(true);
            clubE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Club;
        }
        else
            clubE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1)
        {
            warHammerE.gameObject.SetActive(true);
            warHammerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Warhammer;
        }
        else
            warHammerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Spear >= 1)
        {
            spearE.gameObject.SetActive(true);
            spearE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Spear;
        }
        else
            spearE.gameObject.SetActive(false);

        if (Globals.shopMerchant.ShortSword == 0)
        {
            Globals.shopMerchant.ShortSword = 1;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        //int total = Globals.shopMerchant.ShortSword; //+ 1
        //shortSwordE.gameObject.SetActive(true);
        //shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //if (Globals.shopMerchant.ShortSword == 0 && Globals.shopMerchant.Dragger == 0 && Globals.shopMerchant.LongSword == 0 && Globals.shopMerchant.ShortAxe == 0 && Globals.shopMerchant.Warhammer == 0 && Globals.shopMerchant.LongAxe == 0 && Globals.shopMerchant.Mace == 0 && Globals.shopMerchant.Spear == 0)
        //{
        //    shortSwordE.gameObject.SetActive(true);
        //    shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortSword;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void WeaponShowJohn()
    {
        if (Globals.shopMerchant.ShortAxe <= 2 && Globals.shopMerchant.ShortAxe > 0)
        {
            if (Globals.shopMerchant.ShortAxe == 1)
            {
                if (Globals.inventoryMarium.ShortAxe == 1 || Globals.inventoryProtagnist.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.ShortAxe == 2)
            {
                if (Globals.inventoryMarium.ShortAxe == 1 && Globals.inventoryProtagnist.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }

        }

        if (Globals.shopMerchant.Warhammer <= 3 && Globals.shopMerchant.Warhammer > 0)
        {
            if (Globals.shopMerchant.Warhammer == 1)
            {
                if (Globals.inventoryMarium.Warhammer == 1 || Globals.inventoryProtagnist.Warhammer == 1 || Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1) || (Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1) || (Globals.inventoryTucker.Warhammer == 1 && Globals.inventoryJohn.Warhammer == 1))
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 3)
            {
                if (Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryProtagnist.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
        }


        if (Globals.shopMerchant.LongAxe <= 1 && Globals.shopMerchant.LongAxe > 0)
        {
            if (Globals.shopMerchant.LongAxe == 1)
            {
                if (Globals.inventoryProtagnist.LongAxe == 1)
                {
                    longAxe.gameObject.SetActive(false);
                }
                else
                {
                    longAxe.gameObject.SetActive(true);
                }
            }

        }


        if (Globals.shopMerchant.Spear <= 2 && Globals.shopMerchant.Spear > 0)
        {
            if (Globals.shopMerchant.Spear == 1)
            {
                if (Globals.inventoryMarium.Spear == 1 || Globals.inventoryProtagnist.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Spear == 2)
            {
                if (Globals.inventoryMarium.Spear == 1 && Globals.inventoryProtagnist.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }

        }


        if (Globals.shopMerchant.LongSword <= 1 && Globals.shopMerchant.LongSword > 0)
        {
            if (Globals.shopMerchant.LongSword == 1)
            {
                if (Globals.inventoryProtagnist.LongSword == 1)
                {
                    longSword.gameObject.SetActive(false);
                }
                else
                {
                    longSword.gameObject.SetActive(true);
                }
            }

        }


        if (Globals.shopMerchant.Mace <= 2 && Globals.shopMerchant.Mace > 0)  //mace
        {
            if (Globals.shopMerchant.Mace == 1)
            {
                if (Globals.inventoryProtagnist.Mace == 1 || Globals.inventoryTucker.Mace == 1)
                {
                    Mace.gameObject.SetActive(false);
                }
                else
                {
                    Mace.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Mace == 2)
            {
                if (Globals.inventoryTucker.Mace == 1 && Globals.inventoryProtagnist.Mace == 1)
                {
                    Mace.gameObject.SetActive(false);
                }
                else
                {
                    Mace.gameObject.SetActive(true);
                }
            }

        }


        if (Globals.shopMerchant.Dragger <= 3 && Globals.shopMerchant.Dragger > 0) // dagger
        {
            if (Globals.shopMerchant.Dragger == 1)
            {
                if (Globals.inventoryMarium.Dragger == 1 || Globals.inventoryProtagnist.Dragger == 1 || Globals.inventoryTucker.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryMarium.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1) || (Globals.inventoryMarium.Dragger == 1 && Globals.inventoryTucker.Dragger == 1) || (Globals.inventoryTucker.Dragger == 1 && Globals.inventoryJohn.Dragger == 1))
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Dragger == 3)
            {
                if (Globals.inventoryMarium.Dragger == 1 && Globals.inventoryProtagnist.Dragger == 1 && Globals.inventoryTucker.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
        }



        if (Globals.shopMerchant.ShortSword <= 2 && Globals.shopMerchant.ShortSword > 0)  //SHORT SWORD
        {
            if (Globals.shopMerchant.ShortSword == 1)
            {
                if (Globals.inventoryProtagnist.ShortSword == 1 || Globals.inventoryMarium.ShortSword == 1)
                {
                    Sword.gameObject.SetActive(false);
                }
                else
                {
                    Sword.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.ShortSword == 2)
            {
                if (Globals.inventoryMarium.ShortSword == 1 && Globals.inventoryProtagnist.ShortSword == 1)
                {
                    Sword.gameObject.SetActive(false);
                }
                else
                {
                    Sword.gameObject.SetActive(true);
                }
            }

        }

    }
void WeaponForJohnCharacter()
    {
        AllWeapon();
        if (Globals.shopMerchant.ShortSword > 0)
        {
            Sword.gameObject.SetActive(true);
            if(Globals.inventoryJohn.WeaponAttack == "ShortSword")
                Sword.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            Sword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortAxe >= 1 || Globals.inventoryJohn.ShortAxe>=1)
        {
            shortAxe.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WeaponAttack == "ShortAxe")
                shortAxe.transform.GetChild(1).gameObject.SetActive(true);
            else
                shortAxe.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shortAxe.gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1 || Globals.inventoryJohn.Warhammer >= 1)
        {
            warHammer.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WeaponAttack == "warHammer")
                warHammer.transform.GetChild(1).gameObject.SetActive(true);
            else
                warHammer.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            warHammer.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongAxe >= 1 || Globals.inventoryJohn.LongAxe >= 1)
        {
            longAxe.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WeaponAttack == "longAxe")
                longAxe.transform.GetChild(1).gameObject.SetActive(true);
            else
                longAxe.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            longAxe.gameObject.SetActive(false);
        if (Globals.shopMerchant.Spear >= 1 || Globals.inventoryJohn.Spear >= 1)
        {
            Spear.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WeaponAttack == "Spear")
                Spear.transform.GetChild(1).gameObject.SetActive(true);
            else
                Spear.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Spear.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongSword >= 1 || Globals.inventoryJohn.LongSword >= 1)
        {
            longSword.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WeaponAttack == "longSword")
                longSword.transform.GetChild(1).gameObject.SetActive(true);
            else
                longSword.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            longSword.gameObject.SetActive(false);
        if (Globals.shopMerchant.Mace >= 1 || Globals.inventoryJohn.Mace >= 1)
        {
            Mace.gameObject.SetActive(true);
        //    Sword.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WeaponAttack == "Mace")
                Mace.transform.GetChild(1).gameObject.SetActive(true);
            else
                Mace.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Mace.gameObject.SetActive(false);
        if (Globals.shopMerchant.Dragger >= 1 || Globals.inventoryJohn.Dragger >= 1)
        {
            Dragger.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryJohn.WeaponAttack == "Dragger")
                Dragger.transform.GetChild(1).gameObject.SetActive(true);
            else
                Dragger.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Dragger.gameObject.SetActive(false);

        WeaponShowJohn();
        //if (Globals.inventoryJohn.Dragger == 0 && Globals.inventoryJohn.LongSword == 0 && Globals.inventoryJohn.ShortAxe == 0 && Globals.inventoryJohn.Warhammer == 0 && Globals.inventoryJohn.LongAxe == 0 && Globals.inventoryJohn.Mace == 0 && Globals.inventoryJohn.Spear == 0)
        //{
        //    Sword.gameObject.SetActive(true);
        //    Sword.transform.GetChild(1).gameObject.SetActive(true);
        //} 
    }

    void weaponShowPriest()
    {
        if (Globals.shopMerchant.Mace <= 2 && Globals.shopMerchant.Mace > 0)
        {
            if (Globals.shopMerchant.Mace == 1)
            {
                if (Globals.inventoryTucker.Mace == 1 || Globals.inventoryJohn.Mace == 1)
                {
                    Mace.gameObject.SetActive(false);
                }
                else
                {
                    Mace.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Mace == 2)
            {
                if (Globals.inventoryTucker.Mace == 1 && Globals.inventoryJohn.Mace == 1)
                {
                    Mace.gameObject.SetActive(false);
                }
                else
                {
                    Mace.gameObject.SetActive(true);
                }
            }

        }
        if (Globals.shopMerchant.Warhammer <= 3 && Globals.shopMerchant.Warhammer > 0)
        {
            if (Globals.shopMerchant.Warhammer == 1)
            {
                if (Globals.inventoryMarium.Warhammer == 1 || Globals.inventoryJohn.Warhammer == 1 || Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryJohn.Warhammer == 1) || (Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1) || (Globals.inventoryTucker.Warhammer == 1 && Globals.inventoryJohn.Warhammer == 1))
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 3)
            {
                if (Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
        }

        if (Globals.shopMerchant.Dragger <= 3 && Globals.shopMerchant.Dragger > 0)
        {
            if (Globals.shopMerchant.Dragger == 1)
            {
                if (Globals.inventoryMarium.Dragger == 1 || Globals.inventoryJohn.Dragger == 1 || Globals.inventoryTucker.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Dragger == 2)
            {
                if ((Globals.inventoryMarium.Dragger == 1 && Globals.inventoryJohn.Dragger == 1) || (Globals.inventoryMarium.Dragger == 1 && Globals.inventoryTucker.Dragger == 1) || (Globals.inventoryTucker.Dragger == 1 && Globals.inventoryJohn.Dragger == 1))
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Dragger == 3)
            {
                if (Globals.inventoryMarium.Dragger == 1 && Globals.inventoryJohn.Dragger == 1 && Globals.inventoryTucker.Dragger == 1)
                {
                    Dragger.gameObject.SetActive(false);
                }
                else
                {
                    Dragger.gameObject.SetActive(true);
                }
            }
        }
        if (Globals.shopMerchant.Maul <= 1 && Globals.shopMerchant.Maul > 0)
        {
            if (Globals.shopMerchant.Maul == 1)
            {
                if (Globals.inventoryTucker.Maul == 1)
                {
                    Maul.gameObject.SetActive(false);
                }
                else
                {
                    Maul.gameObject.SetActive(true);
                }
            }

        }

       
        if (Globals.shopMerchant.Flair <= 1 && Globals.shopMerchant.Flair > 0)
        {
            if (Globals.shopMerchant.Flair == 1)
            {
                if (Globals.inventoryTucker.Flair == 1)
                {
                    flair.gameObject.SetActive(false);
                }
                else
                {
                    flair.gameObject.SetActive(true);
                }
            }

        }
    }
    void WeaponForPriestCharacter()
    {
        AllWeapon();
        if (Globals.inventoryProtagnist.AttackWeapon == "Mace")
        {
            Mace.gameObject.SetActive(true);
            Mace.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            Mace.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1 || Globals.inventoryProtagnist.Warhammer>=1)
        {
            warHammer.gameObject.SetActive(true);
           // Mace.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "warHammer")
                warHammer.transform.GetChild(1).gameObject.SetActive(true);
            else
                warHammer.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            warHammer.gameObject.SetActive(false);
        if (Globals.shopMerchant.Club >= 1 || Globals.inventoryProtagnist.Club >= 1)
        {
            Club.gameObject.SetActive(true);
          //  Mace.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "Club")
                Club.transform.GetChild(1).gameObject.SetActive(true);
            else
                Club.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Club.gameObject.SetActive(false);
        if (Globals.shopMerchant.Flair >= 1 || Globals.inventoryProtagnist.Flair >= 1)
        {
            flair.gameObject.SetActive(true);
          //  Mace.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "Flair")
                flair.transform.GetChild(1).gameObject.SetActive(true);
            else
                flair.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            flair.gameObject.SetActive(false);
        if (Globals.shopMerchant.Maul >= 1 || Globals.inventoryProtagnist.Maul >= 1)
        {
            Maul.gameObject.SetActive(true);
          //  Mace.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "Maul")
                Maul.transform.GetChild(1).gameObject.SetActive(true);
            else
                Maul.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Maul.gameObject.SetActive(false);
        if (Globals.shopMerchant.Dragger >= 1 || Globals.inventoryProtagnist.Dragger >= 1)
        {
            Dragger.gameObject.SetActive(true);
          //  Mace.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "Dragger")
                Dragger.transform.GetChild(1).gameObject.SetActive(true);
            else
                Dragger.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Dragger.gameObject.SetActive(false);
        if (Globals.isLightening || Globals.inventoryProtagnist.AttackWeapon == "MagicSword")
        {
            magicSword.gameObject.SetActive(true);
            magicSword.transform.GetChild(0).GetComponent<Text>().text = "Magic Mace";
            if (Globals.inventoryProtagnist.AttackWeapon == "MagicSword")
                magicSword.transform.GetChild(1).gameObject.SetActive(true);
            else
                magicSword.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            magicSword.gameObject.SetActive(false);
        weaponShowPriest();
        //if (Globals.inventoryProtagnist.Warhammer == 0 && Globals.inventoryProtagnist.Club == 0 && Globals.inventoryProtagnist.Mace == 0 && Globals.inventoryProtagnist.Flair == 0 && Globals.inventoryProtagnist.Maul == 0 && Globals.inventoryProtagnist.Dragger == 0)
        //{
        //    Mace.gameObject.SetActive(true);
        //    Mace.transform.GetChild(1).gameObject.SetActive(true);
        //}
    }
    void weaponShowArcher()
    {
        if (Globals.shopMerchant.ShortBow <= 1 && Globals.shopMerchant.ShortBow > 0)
        {
            if (Globals.shopMerchant.ShortBow == 1)
            {
                if (Globals.inventoryMarium.ShortBow == 1)
                {
                    shortBow.gameObject.SetActive(false);
                }
                else
                {
                    shortBow.gameObject.SetActive(true);
                }
            }
          
        }
        if (Globals.shopMerchant.ShortAxe <= 2 && Globals.shopMerchant.ShortAxe > 0)
        {
            if (Globals.shopMerchant.ShortAxe == 1)
            {
                if (Globals.inventoryJohn.ShortAxe == 1 || Globals.inventoryMarium.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.ShortAxe == 2)
            {
                if (Globals.inventoryJohn.ShortAxe == 1 && Globals.inventoryMarium.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }
        }
        if (Globals.shopMerchant.Spear <= 2 && Globals.shopMerchant.Spear > 0)
        {
            if (Globals.shopMerchant.Spear == 1)
            {
                if (Globals.inventoryMarium.Spear == 1 || Globals.inventoryJohn.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Spear == 2)
            {
                if (Globals.inventoryMarium.Spear == 1 && Globals.inventoryJohn.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }

        }
        if (Globals.shopMerchant.LongBow <= 1 && Globals.shopMerchant.LongBow > 0)
        {
            if (Globals.shopMerchant.LongBow == 1)
            {
                if (Globals.inventoryMarium.LongBow == 1)
                {
                    longBow.gameObject.SetActive(false);
                }
                else
                {
                    longBow.gameObject.SetActive(true);
                }
            }

        }
    }
    void WeaponForArcherCharacter()
    {
        AllWeapon();
        if (Globals.inventoryProtagnist.AttackWeapon == "shortBow")
        {
            shortBow.gameObject.SetActive(true);
            shortBow.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            shortBow.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortAxe >= 1 || Globals.inventoryProtagnist.ShortAxe>=1)
        {
            shortAxe.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "ShortAxe")
                shortAxe.transform.GetChild(1).gameObject.SetActive(true);
            else
                shortAxe.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shortAxe.gameObject.SetActive(false);
        if (Globals.shopMerchant.Spear >= 1 || Globals.inventoryProtagnist.Spear >= 1)
        {
            Spear.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
                Spear.transform.GetChild(1).gameObject.SetActive(true);
            else
                Spear.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Spear.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongBow >= 1 || Globals.inventoryProtagnist.LongBow >= 1)
        {
            longBow.gameObject.SetActive(true);
            shortBow.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "longBow")
                longBow.transform.GetChild(1).gameObject.SetActive(true);
            else
                longBow.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            longBow.gameObject.SetActive(false);
        Debug.Log("bool::" + Globals.isLightening);
        if (Globals.isLightening || Globals.inventoryProtagnist.AttackWeapon == "MagicSword")
        {
            magicSword.gameObject.SetActive(true);
            magicSword.transform.GetChild(0).GetComponent<Text>().text = "Magic Bow";
            if (Globals.inventoryProtagnist.AttackWeapon == "MagicSword")
                magicSword.transform.GetChild(1).gameObject.SetActive(true);
            else
                magicSword.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            magicSword.gameObject.SetActive(false);

        weaponShowArcher();
        //if (Globals.inventoryProtagnist.ShortAxe == 0 && Globals.inventoryProtagnist.Spear == 0 && Globals.inventoryProtagnist.ShortBow == 0 && Globals.inventoryProtagnist.LongBow == 0)
        //{
        //    shortBow.gameObject.SetActive(true);
        //    shortBow.transform.GetChild(1).gameObject.SetActive(true);
        //}
    }
    void WeaponForArcherEquipement()
    {
        AllEquipementWeapon();
        if (Globals.shopMerchant.Dragger >= 1)
        {
            draggerE.gameObject.SetActive(true);
            draggerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Dragger;
        }
        else
            draggerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortSword >= 1)
        {
            shortSwordE.gameObject.SetActive(true);
            shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortSword;
        }
        else
            shortSwordE.gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortAxe >= 1)
        {
            shortAxeE.gameObject.SetActive(true);
            totalAxe = 0;
            totalAxe = Globals.shopMerchant.ShortAxe;
            shortAxeE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + totalAxe;
        }
        else
            shortAxeE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1)
        {
            warHammerE.gameObject.SetActive(true);
            warHammerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Warhammer;
        }
        else
            warHammerE.gameObject.SetActive(false);
        if (Globals.shopMerchant.Spear >= 1)
        {
            spearE.gameObject.SetActive(true);
            spearE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Spear;
        }
        else
            spearE.gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortBow >= 1)
        {
            shortBowE.gameObject.SetActive(true);
            totalBow = 0;
            totalBow = Globals.shopMerchant.ShortBow ;
            shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " +totalBow;
        }
        else
            shortBowE.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongBow >= 1)
        {
            longBowE.gameObject.SetActive(true);
            totalBow = 0;
            totalBow = Globals.shopMerchant.LongBow;
            longBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + totalBow;
        }
        else
            longBowE.gameObject.SetActive(false);
        if (Globals.isLightening)
        {
            magicSwordE.gameObject.SetActive(true);
            magicSwordE.transform.GetChild(0).GetComponent<Text>().text = "Magic Bow";
        }
        else
            magicSwordE.gameObject.SetActive(false);

        int total = Globals.shopMerchant.ShortBow + 1;
        shortBowE.gameObject.SetActive(true);
        shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //if (Globals.shopMerchant.ShortBow == 0)
        //{
        //    shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;
        //}
        //else
        //{

        //    shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortBow++;
        //}

     
        //if (Globals.shopMerchant.ShortAxe == 0 && Globals.shopMerchant.Dragger==0 && Globals.shopMerchant.ShortSword==0 && Globals.shopMerchant.Spear == 0 && Globals.shopMerchant.ShortBow == 0 && Globals.shopMerchant.LongBow == 0)
        //{
        //    shortBowE.gameObject.SetActive(true);
        //    shortBowE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortBow;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void AllEquipementWeapon()
    {
        magicSwordE.gameObject.SetActive(false);
        SwordE.gameObject.SetActive(false);
        shortAxeE.gameObject.SetActive(false);
        warHammerE.gameObject.SetActive(false);
        spearE.gameObject.SetActive(false);
        shortBowE.gameObject.SetActive(false);
        clubE.gameObject.SetActive(false);
        MaceE.gameObject.SetActive(false);
        shortSwordE.SetActive(false);
        draggerE.SetActive(false);
        longAxeE.SetActive(false);
        doubleHeadedAxeE.SetActive(false);
        flairE.SetActive(false);
        longBowE.SetActive(false);
        compositeBowE.SetActive(false);
        crossBowE.SetActive(false);
    }
    int totalSword,totalAxe,totalHammer,totalBow,totalClub;
    void WeaponForSmithEquipement()
    {
        AllEquipementWeapon();
        Debug.Log("..........");
        if (Globals.shopMerchant.ShortSword >= 1)
        {
            Debug.Log("here is inside if");
            shortSwordE.SetActive(true);
            shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortSword;
        }
        if (Globals.shopMerchant.LongSword >= 1)
        {
            shortSwordE.SetActive(true);
            longSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.LongSword;
        }
        if (Globals.shopMerchant.Dragger >= 1)
        {
            draggerE.SetActive(true);
            draggerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Dragger;
        }
        else
            draggerE.SetActive(false);

        if (Globals.shopMerchant.ShortAxe >= 1)
        {
            shortAxeE.gameObject.SetActive(true);
             int shortAxe = 0;
            shortAxe = Globals.shopMerchant.ShortAxe;
            shortAxeE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + shortAxe;
        }
        else
        {
            shortAxeE.gameObject.SetActive(false);
        }
        if (Globals.shopMerchant.LongAxe >= 1)
        {
            longAxeE.gameObject.SetActive(true);
            int LongAxe = 0;
            LongAxe = Globals.shopMerchant.LongAxe;
            longAxeE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + LongAxe;
        }
        else
        {
            longAxeE.gameObject.SetActive(false);
        }
        if (Globals.shopMerchant.Mace >= 1)
        {
            MaceE.gameObject.SetActive(true);
            MaceE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Mace;
        }
        else
            MaceE.gameObject.SetActive(false);

        if (Globals.shopMerchant.Warhammer >= 1)
        {
            warHammerE.gameObject.SetActive(true);
            totalHammer = 0;
            totalHammer = Globals.shopMerchant.Warhammer;
            warHammerE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.Warhammer;
        }
        else
            warHammerE.gameObject.SetActive(false);

        if (Globals.shopMerchant.Spear >= 1)
        {
            spearE.gameObject.SetActive(true);
            spearE.transform.GetChild(3).GetComponent<Text>().text = "Total:" + Globals.shopMerchant.Spear;
        }
        else
            spearE.gameObject.SetActive(false);

        int total = Globals.shopMerchant.ShortSword; // + 1
        shortSwordE.gameObject.SetActive(true);
        shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + total;

        //if (Globals.shopMerchant.ShortSword == 0 && Globals.shopMerchant.Dragger==0 && Globals.shopMerchant.ShortAxe == 0 && Globals.shopMerchant.Warhammer == 0 && Globals.shopMerchant.LongAxe == 0 && Globals.shopMerchant.Mace == 0 && Globals.shopMerchant.LongSword == 0 )
        //{
        //    shortSwordE.gameObject.SetActive(true);
        //    shortSwordE.transform.GetChild(3).GetComponent<Text>().text = "Total: " + Globals.shopMerchant.ShortSword;
        //}
        //else
        //    noItem1.gameObject.SetActive(false);
    }
    void weaponShowSmith()
    {
        if (Globals.shopMerchant.ShortAxe <= 2 && Globals.shopMerchant.ShortAxe > 0)
        {
            if(Globals.shopMerchant.ShortAxe == 1){
                if (Globals.inventoryMarium.ShortAxe == 1 || Globals.inventoryJohn.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.ShortAxe == 2)
            {
                if (Globals.inventoryMarium.ShortAxe == 1 && Globals.inventoryJohn.ShortAxe == 1)
                {
                    shortAxe.gameObject.SetActive(false);
                }
                else
                {
                    shortAxe.gameObject.SetActive(true);
                }
            }

        }
        //else
        //{
        //    shortAxe.gameObject.SetActive(true);
        //}

        if (Globals.shopMerchant.Warhammer <= 3 && Globals.shopMerchant.Warhammer > 0)
        {
            if (Globals.shopMerchant.Warhammer == 1)
            {
                if (Globals.inventoryMarium.Warhammer == 1 || Globals.inventoryJohn.Warhammer == 1 || Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 2)
            {
                if ((Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryJohn.Warhammer == 1) ||(Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1) ||(Globals.inventoryTucker.Warhammer == 1 && Globals.inventoryJohn.Warhammer == 1))
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Warhammer == 3)
            {
                if (Globals.inventoryMarium.Warhammer == 1 && Globals.inventoryJohn.Warhammer == 1 && Globals.inventoryTucker.Warhammer == 1)
                {
                    warHammer.gameObject.SetActive(false);
                }
                else
                {
                    warHammer.gameObject.SetActive(true);
                }
            }
        }
        //else
        //{
        //    warHammer.gameObject.SetActive(true);
        //}

        if (Globals.shopMerchant.LongAxe <= 1 && Globals.shopMerchant.LongAxe > 0)
        {
            if (Globals.shopMerchant.LongAxe == 1)
            {
                if (Globals.inventoryJohn.LongAxe == 1)
                {
                    longAxe.gameObject.SetActive(false);
                }
                else
                {
                    longAxe.gameObject.SetActive(true);
                }
            }
           
        }
        //else
        //{
        //    longAxe.gameObject.SetActive(true);
        //}

        if (Globals.shopMerchant.Spear <= 2 && Globals.shopMerchant.Spear > 0)
        {
            if (Globals.shopMerchant.Spear == 1)
            {
                if (Globals.inventoryMarium.Spear == 1 || Globals.inventoryJohn.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }
            if (Globals.shopMerchant.Spear == 2)
            {
                if (Globals.inventoryMarium.Spear == 1 && Globals.inventoryJohn.Spear == 1)
                {
                    Spear.gameObject.SetActive(false);
                }
                else
                {
                    Spear.gameObject.SetActive(true);
                }
            }

        }
        //else
        //{
        //    Spear.gameObject.SetActive(true);
        //}

        if (Globals.shopMerchant.LongSword <= 1 && Globals.shopMerchant.LongSword > 0)
        {
            if (Globals.shopMerchant.LongSword == 1)
            {
                if (Globals.inventoryJohn.LongSword == 1)
                {
                    longSword.gameObject.SetActive(false);
                }
                else
                {
                    longSword.gameObject.SetActive(true);
                }
            }

        }
        //else
        //{
        //    longSword.gameObject.SetActive(true);
        //}
    }
    void WeaponForSmithCharacter()
    {
        AllWeapon();
        if (Globals.shopMerchant.ShortSword > 0)
        {
            Sword.gameObject.SetActive(true);
            if(Globals.inventoryProtagnist.AttackWeapon == "ShortSword")
                Sword.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            Sword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.shopMerchant.ShortAxe >= 1 || Globals.inventoryProtagnist.ShortAxe>=1)
        {
            shortAxe.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "ShortAxe")
                shortAxe.transform.GetChild(1).gameObject.SetActive(true);
            else
                shortAxe.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            shortAxe.gameObject.SetActive(false);
        if (Globals.shopMerchant.Warhammer >= 1||Globals.inventoryProtagnist.Warhammer>=1)
        {
            warHammer.gameObject.SetActive(true);
           // Sword.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "warHammer")
                warHammer.transform.GetChild(1).gameObject.SetActive(true);
            else
                warHammer.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            warHammer.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongAxe >= 1 || Globals.inventoryProtagnist.LongAxe >= 1)
        {
            longAxe.gameObject.SetActive(true);
           // Sword.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "longAxe")
                longAxe.transform.GetChild(1).gameObject.SetActive(true);
            else
                longAxe.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            longAxe.gameObject.SetActive(false);
        if (Globals.shopMerchant.Spear >= 1 || Globals.inventoryProtagnist.Spear >= 1)
        {
            Spear.gameObject.SetActive(true);
          //  Sword.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
                Spear.transform.GetChild(1).gameObject.SetActive(true);
            else
                Spear.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            Spear.gameObject.SetActive(false);
        if (Globals.shopMerchant.LongSword >= 1 || Globals.inventoryProtagnist.LongSword >= 1)
        {
            longSword.gameObject.SetActive(true);
           // Sword.gameObject.SetActive(true);
            if (Globals.inventoryProtagnist.AttackWeapon == "longSword")
                longSword.transform.GetChild(1).gameObject.SetActive(true);
            else
                longSword.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            longSword.gameObject.SetActive(false);

        weaponShowSmith();

        Debug.Log("bool::" + Globals.isLightening);
        if (Globals.isLightening || Globals.inventoryProtagnist.AttackWeapon== "MagicSword")
        {
            magicSword.gameObject.SetActive(true);
           // Sword.gameObject.SetActive(true);
            magicSword.transform.GetChild(0).GetComponent<Text>().text = "Magic Sword";
            if (Globals.inventoryProtagnist.AttackWeapon == "MagicSword")
                magicSword.transform.GetChild(1).gameObject.SetActive(true);
            else
                magicSword.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            magicSword.gameObject.SetActive(false);

        //if (Globals.inventoryProtagnist.ShortAxe == 0 && Globals.inventoryProtagnist.Warhammer == 0 && Globals.inventoryProtagnist.LongAxe == 0 && Globals.inventoryProtagnist.Mace == 0 && Globals.inventoryProtagnist.LongSword == 0 && Globals.inventoryProtagnist.magicSword == 0)
        //{
        //    Sword.gameObject.SetActive(true);
        //    Sword.transform.GetChild(1).gameObject.SetActive(true);
        //}
    }
    public void BackToInventory()
    {
        InventoryTopSetting(true, false, false);
        InventoryPanelSetting(true, false, false);
        Globals.selectedInventoryCharacter = Globals.avatarState.AvatarName;
        HighlightImages(true, false, false, false);
        //isCharacter = true;
        //isWeapon = false;
        //isStatistics = false;
        InventorySetupForCompanion();
    }
    public void InventoryCharacter(string NameOfCharacter)
    {
        if (NameOfCharacter.Equals("character"))
            Globals.selectedInventoryCharacter = Globals.avatarState.AvatarName;
        else
            Globals.selectedInventoryCharacter = NameOfCharacter;
        if (!isStatistics)
          InventorySetupForCompanion();
        else
            Globals.staticRecord.CharacterRecords();
        if (NameOfCharacter.Equals("character"))
            HighlightImages(true, false, false, false);
        else
        {
            if (NameOfCharacter.Equals("John"))
                HighlightImages(false, true, false, false);
            else if (NameOfCharacter.Equals("Marium"))
                HighlightImages(false, false, true, false);
            else if (NameOfCharacter.Equals("Tucker"))
                HighlightImages(false, false, false, true);
        }
        if (isCharacter)
            FindObjectOfType<DataInInventory>().SetImage();
        PrintValues();
    }
    public void HighlightImages(bool prot,bool john,bool marium,bool tucker)
    {
        ProtagnistHighlight.SetActive(prot);
        johnHighlight.SetActive(john);
        mariumHighlight.SetActive(marium);
        tuckerHighlight.SetActive(tucker);
    }

    public void HelmetForProtagnist(int l,int k,int n,int a,int m)
    {
        Globals.inventoryProtagnist.LeatherCap = l;
        Globals.inventoryProtagnist.KettleHat = k;
        Globals.inventoryProtagnist.NesalHelmet = n;
        Globals.inventoryProtagnist.Aventail = a;
        Globals.inventoryProtagnist.MailCoif = m;
    }
    void HelmetForJohn(int l, int k, int n, int a, int m)
    {
      //  Debug.Log("l::" + Globals.inventoryJohn.LeatherCap + " k::" + Globals.inventoryJohn.KettleHat + "  n::" + Globals.inventoryJohn.NasalHelmet);
        Globals.inventoryJohn.LeatherCap = l;
        Globals.inventoryJohn.KettleHat = k;
        Globals.inventoryJohn.NasalHelmet = n;
        Globals.inventoryJohn.Avaintail = a;
        Globals.inventoryJohn.MailCoif = m;
    }
    public void HelmetForMarium(int l, int k, int n)
    {
        Globals.inventoryMarium.LeatherCap = l;
        Globals.inventoryMarium.KettleHat = k;
        Globals.inventoryMarium.NasalHelmet = n;
    }
    void HelmetForTucker(int l, int k)
    {
        Globals.inventoryTucker.LeatherCap = l;
        Globals.inventoryTucker.KettleHat = k;
    }
    void CheckMarkForHelmet()
    {
        if (Globals.inventoryProtagnist.Helmet== "LeatherHelmet")
            leatherH.transform.GetChild(2).gameObject.SetActive(true);
        else
            leatherH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Helmet == "KettleHelmet")
            guardH.transform.GetChild(2).gameObject.SetActive(true);
        else
            guardH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Helmet == "NasalHelmet")
            metalH.transform.GetChild(2).gameObject.SetActive(true);
        else
            metalH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Helmet == "AvainTail")
            chainmailH.transform.GetChild(2).gameObject.SetActive(true);
        else
            chainmailH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Helmet == "MailCoif")
            mailCoifH.transform.GetChild(2).gameObject.SetActive(true);
        else
            mailCoifH.transform.GetChild(2).gameObject.SetActive(false);
    }
    void CheckMarkForMariumHelmet()
    {
        if (Globals.inventoryMarium.Helmet== "LeatherHelmet")
            leatherH.transform.GetChild(2).gameObject.SetActive(true);
        else
            leatherH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryMarium.Helmet == "KettleHelmet")
            guardH.transform.GetChild(2).gameObject.SetActive(true);
        else
            guardH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryMarium.Helmet == "NasalHelmet")
            metalH.transform.GetChild(2).gameObject.SetActive(true);
        else
            metalH.transform.GetChild(2).gameObject.SetActive(false);
    }
    void CheckMarkForTuckerHelmet()
    {
        if (Globals.inventoryTucker.Helmet== "LeatherHelmet")
            leatherH.transform.GetChild(2).gameObject.SetActive(true);
        else
            leatherH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryTucker.Helmet == "KettleHelmet")
            guardH.transform.GetChild(2).gameObject.SetActive(true);
        else
            guardH.transform.GetChild(2).gameObject.SetActive(false);
    }
    void CheckMarkForJohn()
    {
        if(Globals.inventoryJohn.Helmet== "LeatherHelmet")
            leatherH.transform.GetChild(2).gameObject.SetActive(true);
        else
            leatherH.transform.GetChild(2).gameObject.SetActive(true);
        if (Globals.inventoryJohn.Helmet == "NasalHelmet")
            metalH.transform.GetChild(2).gameObject.SetActive(true);
        else
            metalH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryJohn.Helmet == "AvainTail")
            chainmailH.transform.GetChild(2).gameObject.SetActive(true);
        else
            chainmailH.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryJohn.Helmet == "KettleHelmet")
            guardH.transform.GetChild(2).gameObject.SetActive(true);
        else
            guardH.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void HelmetPurchase(string name)
    {
        if (name == "LeatherHelmet")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.LeatherCap == 1)
                {
                    leatherH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.LeatherCap = 0;
                    Globals.inventoryProtagnist.Helmet = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                Globals.inventoryProtagnist.LeatherCap = 1;
                Globals.inventoryProtagnist.Helmet = "LeatherHelmet";
                 HelmetForProtagnist(1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForHelmet();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.LeatherCap == 1)
                {
                    leatherH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryJohn.LeatherCap = 0;
                    Globals.inventoryJohn.Helmet = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                Globals.inventoryJohn.LeatherCap = 1;
                Globals.inventoryJohn.Helmet = "LeatherHelmet";
                 HelmetForJohn(1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohn();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.LeatherCap == 1)
                {
                    leatherH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryMarium.LeatherCap = 0;
                    Globals.inventoryMarium.Helmet = "";
                    db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                    return;
                }
                Globals.inventoryMarium.LeatherCap = 1;
                Globals.inventoryMarium.Helmet = "LeatherHelmet";
                 HelmetForMarium(1, 0, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForMariumHelmet();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.LeatherCap == 1)
                {
                    leatherH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryTucker.LeatherCap = 0;
                    Globals.inventoryTucker.Helmet = "";
                    db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                    return;
                }
                Globals.inventoryTucker.LeatherCap = 1;
                Globals.inventoryTucker.Helmet = "LeatherHelmet";
                 HelmetForTucker(1, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForTuckerHelmet();
            }
          //  Globals.shopMerchant.LeatherCap -= 1;
            if (Globals.shopMerchant.LeatherCap < 0)
                Globals.shopMerchant.LeatherCap = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "KettleHelmet")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.KettleHat == 1)
                {
                    guardH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.KettleHat = 0;
                    Globals.inventoryProtagnist.Helmet = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                Globals.inventoryProtagnist.KettleHat = 1;
                Globals.inventoryProtagnist.Helmet = "KettleHelmet";
                  HelmetForProtagnist(0, 1, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForHelmet();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.KettleHat == 1)
                {
                    guardH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryJohn.KettleHat = 0;
                    Globals.inventoryJohn.Helmet = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                Globals.inventoryJohn.KettleHat = 1;
                Globals.inventoryJohn.Helmet = "KettleHelmet";
                 HelmetForJohn(0, 1, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohn();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.KettleHat == 1)
                {
                    guardH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryMarium.KettleHat = 0;
                    Globals.inventoryMarium.Helmet = "";
                    db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                    return;
                }
                Globals.inventoryMarium.KettleHat = 1;
                Globals.inventoryMarium.Helmet = "KettleHelmet";
                  HelmetForMarium(0, 1, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForMariumHelmet();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.KettleHat == 1)
                {
                    guardH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryTucker.KettleHat = 0;
                    Globals.inventoryTucker.Helmet = "";
                    db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                    return;
                }
                Globals.inventoryTucker.KettleHat = 1;
                Globals.inventoryTucker.Helmet = "KettleHelmet";
                 HelmetForTucker(0, 1);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForTuckerHelmet();
            }
          //  Globals.shopMerchant.KettleHat -= 1;
            if (Globals.shopMerchant.KettleHat < 0)
                Globals.shopMerchant.KettleHat = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "NasalHelmet")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.NesalHelmet == 1)
                {
                    metalH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.NesalHelmet = 0;
                    Globals.inventoryProtagnist.Helmet = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                Globals.inventoryProtagnist.NesalHelmet = 1;
                Globals.inventoryProtagnist.Helmet = "NasalHelmet";
                 HelmetForProtagnist(0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForHelmet();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.NasalHelmet == 1)
                {
                    metalH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryJohn.NasalHelmet = 0;
                    Globals.inventoryJohn.Helmet = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                Globals.inventoryJohn.NasalHelmet = 1;
                Globals.inventoryJohn.Helmet = "NasalHelmet";
                 HelmetForJohn(0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohn();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.NasalHelmet == 1)
                {
                    metalH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryMarium.NasalHelmet = 0;
                    Globals.inventoryMarium.Helmet = "";
                    db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                    return;
                }
                Globals.inventoryMarium.NasalHelmet = 1;
                Globals.inventoryMarium.Helmet = "NasalHelmet";
                 HelmetForMarium(0, 0,1);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForMariumHelmet();
            }
           // Globals.shopMerchant.NesalHelmet -= 1;
            if (Globals.shopMerchant.NesalHelmet < 0)
                Globals.shopMerchant.NesalHelmet = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "AvainTail")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.Aventail == 1)
                {
                    chainmailH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.Aventail = 0;
                    Globals.inventoryProtagnist.Helmet = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                Globals.inventoryProtagnist.Aventail = 1;
                Globals.inventoryProtagnist.Helmet = "AvainTail";
                 HelmetForProtagnist(0, 0, 0, 1, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForHelmet();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.Avaintail == 1)
                {
                    chainmailH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryJohn.Avaintail = 0;
                    Globals.inventoryJohn.Helmet = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                Globals.inventoryJohn.Avaintail = 1;
                Globals.inventoryJohn.Helmet = "AvainTail";
                  HelmetForJohn(0, 0,0, 1, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohn();
            }
           // Globals.shopMerchant.Aventail -= 1;
            if (Globals.shopMerchant.Aventail < 0)
                Globals.shopMerchant.Aventail = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "MailCoif")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.MailCoif == 1)
                {
                    mailCoifH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.MailCoif = 0;
                    Globals.inventoryProtagnist.Helmet = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                Globals.inventoryProtagnist.MailCoif = 1;
                Globals.inventoryProtagnist.Helmet = "MailCoif";
                  HelmetForProtagnist(0, 0, 0, 0, 1);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForHelmet();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.MailCoif == 1)
                {
                    mailCoifH.transform.GetChild(2).gameObject.SetActive(false);
                    Globals.inventoryJohn.MailCoif = 0;
                    Globals.inventoryJohn.Helmet = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                Globals.inventoryJohn.MailCoif = 1;
                Globals.inventoryJohn.Helmet = "MailCoif";
                 HelmetForJohn(0, 0, 0, 0, 1);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohn();
            }
           // Globals.shopMerchant.MailCoif -= 1;
            if (Globals.shopMerchant.MailCoif < 0)
                Globals.shopMerchant.MailCoif = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }

    }
    void CharacterTabHelmetSetting(bool l, bool p, bool g, bool c)
    {
        chainmailH.interactable = c;
        leatherH.interactable = l;
        metalH.interactable = p;
        guardH.interactable = g;
    }
    public void ArmourForProtagnist(int l,int b,int p,int c,int s,int h)
    {
        Globals.inventoryProtagnist.LeatherArmour = l;
        Globals.inventoryProtagnist.BrigadineArmor = b;
        Globals.inventoryProtagnist.PaddedArmour = p;
        Globals.inventoryProtagnist.ChainArmour = c;
        Globals.inventoryProtagnist.ScaleArmour = s;
        Globals.inventoryProtagnist.HideArmour = h;
    }
    void ArmourForJohn(int l, int b, int p, int c, int s, int h)
    {
        Globals.inventoryJohn.LeatherArmour = l;
        Globals.inventoryJohn.BrigadineArmour = b;
        Globals.inventoryJohn.PaddedArmour = p;
        Globals.inventoryJohn.ChainArmour = c;
        Globals.inventoryJohn.ScaleArmour = s;
        Globals.inventoryJohn.HideArmour = h;
    }
    public void ArmourForMarium(int l, int b, int p, int h)
    {
        Globals.inventoryMarium.LeatherArmour = l;
        Globals.inventoryMarium.BrigadineArmour = b;
        Globals.inventoryMarium.PaddedArmour = p;
        Globals.inventoryMarium.HideArmour = h;
    }
    void ArmourForTucker(int l, int p, int h)
    {
        Globals.inventoryTucker.LeatherArmour = l;
        Globals.inventoryTucker.PaddedArmour = p;
        Globals.inventoryTucker.HideArmour = h;
    }
    void CheckMarkForArmour()
    {
        if (Globals.inventoryProtagnist.Armour== "Leahter")
            leatherArmor.transform.GetChild(2).gameObject.SetActive(true);        
        else
            leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Armour == "Brigadine")
            guardArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            guardArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Armour == "padded")
        {
            paddedArmor.transform.GetChild(2).gameObject.SetActive(true); 
        }   
        else
            paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Armour == "Chainmail")
            chainmailArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            chainmailArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Armour == "Scale")
            scaleArmour.transform.GetChild(2).gameObject.SetActive(true);
        else
            scaleArmour.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.Armour == "Hide")
            hideArmour.transform.GetChild(2).gameObject.SetActive(true);
        else
            hideArmour.transform.GetChild(2).gameObject.SetActive(false);
    }
    void CheckMarkForArmourJohn()
    {
        if (Globals.inventoryJohn.Armour== "Leahter")
            leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryJohn.Armour == "padded")
            paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryJohn.Armour == "Chainmail")
            chainmailArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            chainmailArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryJohn.Armour == "Scale")
            scaleArmour.transform.GetChild(2).gameObject.SetActive(true);
        else
            scaleArmour.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryJohn.Armour == "Hide")
            hideArmour.transform.GetChild(2).gameObject.SetActive(true);
        else
            hideArmour.transform.GetChild(2).gameObject.SetActive(false);
    }
    void CheckMarkForArmourMarium()
    {
        if (Globals.inventoryMarium.Armour== "Leahter")
            leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryMarium.Armour == "Brigadine")
            guardArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            guardArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryMarium.Armour == "padded")
            paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryMarium.Armour == "Hide")
            hideArmour.transform.GetChild(2).gameObject.SetActive(true);
        else
            hideArmour.transform.GetChild(2).gameObject.SetActive(false);
    }
    void CheckMarkForArmourTucker()
    {
        if (Globals.inventoryTucker.Armour== "Leahter")
            leatherArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            leatherArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryTucker.Armour == "padded")
            paddedArmor.transform.GetChild(2).gameObject.SetActive(true);
        else
            paddedArmor.transform.GetChild(2).gameObject.SetActive(false);
        if (Globals.inventoryTucker.Armour == "Hide")
            hideArmour.transform.GetChild(2).gameObject.SetActive(true);
        else
            hideArmour.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void ArmourPurchase(string name)
    {
        Debug.Log("armor purchase :: "+name+" "+ Globals.selectedInventoryCharacter);
    //    ResetMerchantShopArmour();
        if (name == "Leahter")
        {
          
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.Armour == "Leahter")
                {
                    armorResetProtagnist(leatherArmor.gameObject);
                    return;
                }
                Globals.inventoryProtagnist.LeatherArmour = 1;
                Globals.inventoryProtagnist.Armour = "Leahter";
                ArmourForProtagnist(1, 0, 0, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForArmour();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.Armour == "Leahter")
                {
                    armorResetJohn(leatherArmor.gameObject);
                    return;
                }
                Globals.inventoryJohn.LeatherArmour = 1;
                Globals.inventoryJohn.Armour = "Leahter";
                 ArmourForJohn(1, 0, 0, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForArmourJohn();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryJohn.Armour == "Leahter")
                {
                    armorResetMarium(leatherArmor.gameObject);
                    return;
                }
                Globals.inventoryMarium.LeatherArmour = 1;
                Globals.inventoryMarium.Armour = "Leahter";
                 ArmourForMarium(1, 0, 0, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForArmourMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryJohn.Armour == "Leahter")
                {
                    armorResetTucker(leatherArmor.gameObject);
                    return;
                }
                Globals.inventoryTucker.LeatherArmour = 1;
                Globals.inventoryTucker.Armour = "Leahter";
                 ArmourForTucker(1, 0, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForArmourTucker();
            }
            //Globals.shopMerchant.LeatherArmour -= 1;
            if (Globals.shopMerchant.LeatherArmour < 0)
                Globals.shopMerchant.LeatherArmour = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "Brigadine")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryJohn.Armour == "Brigadine")
                {
                    armorResetProtagnist(guardArmor.gameObject);
                    return;
                }
                Globals.inventoryProtagnist.BrigadineArmor = 1;
                Globals.inventoryProtagnist.Armour = "Brigadine";
                ArmourForProtagnist(0, 1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForArmour();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.Armour == "Brigadine")
                {
                    armorResetJohn(guardArmor.gameObject);
                    return;
                }
                Globals.inventoryJohn.BrigadineArmour = 1;
                Globals.inventoryJohn.Armour = "Brigadine";
                ArmourForJohn(0, 1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForArmourJohn();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.Armour == "Brigadine")
                {
                    armorResetMarium(guardArmor.gameObject);
                    return;
                }
                Globals.inventoryMarium.BrigadineArmour = 1;
                Globals.inventoryMarium.Armour = "Brigadine";
                ArmourForMarium(0, 1, 0, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForArmourMarium();
            }
            //Globals.shopMerchant.BrigadineArmor -= 1;
            if (Globals.shopMerchant.BrigadineArmor < 0)
                Globals.shopMerchant.BrigadineArmor = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "padded")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.Armour == "padded")
                {
                    armorResetProtagnist(paddedArmor.gameObject);
                    return;
                }
                Globals.inventoryProtagnist.PaddedArmour = 1;
                Globals.inventoryProtagnist.Armour = "padded";
                ArmourForProtagnist(0, 0, 1, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForArmour();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.Armour == "padded")
                {
                    armorResetJohn(paddedArmor.gameObject);
                    return;
                }
                Globals.inventoryJohn.PaddedArmour = 1;
                Globals.inventoryJohn.Armour = "padded";
                ArmourForJohn(0, 0,1, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForArmourJohn();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.Armour == "padded")
                {
                    armorResetMarium(paddedArmor.gameObject);
                    return;
                }
                Globals.inventoryMarium.PaddedArmour = 1;
                Globals.inventoryMarium.Armour = "padded";
                ArmourForMarium(0, 0, 1, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForArmourMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.Armour == "padded")
                {
                    armorResetTucker(paddedArmor.gameObject);
                    return;
                }
                Globals.inventoryTucker.PaddedArmour = 1;
                Globals.inventoryTucker.Armour = "padded";
                 ArmourForTucker(0, 1, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForArmourTucker();
            }
            //Globals.shopMerchant.PaddedArmour -= 1;
            if (Globals.shopMerchant.PaddedArmour < 0)
                Globals.shopMerchant.PaddedArmour = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "Chainmail")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.Armour == "Chainmail")
                {
                    armorResetProtagnist(chainmailArmor.gameObject);
                    return;
                }
                Globals.inventoryProtagnist.ChainArmour = 1;
                Globals.inventoryProtagnist.Armour = "Chainmail";
                 ArmourForProtagnist(0, 0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForArmour();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.Armour == "Chainmail")
                {
                    armorResetJohn(chainmailArmor.gameObject);
                    return;
                }
                Globals.inventoryJohn.ChainArmour = 1;
                Globals.inventoryJohn.Armour = "Chainmail";
                 ArmourForJohn(0, 0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForArmourJohn();
            }
            //Globals.shopMerchant.ChainArmour -= 1;
            if (Globals.shopMerchant.ChainArmour < 0)
                Globals.shopMerchant.ChainArmour = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "Scale")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.Armour == "Scale")
                {
                    armorResetProtagnist(scaleArmour.gameObject);
                    return;
                }
                Globals.inventoryProtagnist.ScaleArmour = 1;
                Globals.inventoryProtagnist.Armour = "Scale";
                 ArmourForProtagnist(0, 0, 0, 0, 1, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForArmour();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.Armour == "Scale")
                {
                    armorResetJohn(scaleArmour.gameObject);
                    return;
                }
                Globals.inventoryJohn.ScaleArmour = 1;
                Globals.inventoryJohn.Armour = "Scale";
                 ArmourForJohn(0, 0, 0, 0, 1, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForArmourJohn();
            }
            //Globals.shopMerchant.ScaleArmour -= 1;
            if (Globals.shopMerchant.ScaleArmour < 0)
                Globals.shopMerchant.ScaleArmour = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (name == "Hide")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.Armour == "Hide")
                {
                    armorResetProtagnist(hideArmour.gameObject);
                    return;
                }
                Globals.inventoryProtagnist.HideArmour = 1;
                Globals.inventoryProtagnist.Armour = "Hide";
                ArmourForProtagnist(0, 0, 0, 0, 0, 1);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForArmour();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.Armour == "Hide")
                {
                    armorResetJohn(hideArmour.gameObject);
                    return;
                }
                Globals.inventoryJohn.HideArmour = 1;
                Globals.inventoryJohn.Armour = "Hide";
                ArmourForJohn(0, 0, 0, 0, 0, 1);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForArmourJohn();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.Armour == "Hide")
                {
                    armorResetMarium(hideArmour.gameObject);
                    return;
                }
                Globals.inventoryMarium.HideArmour = 1;
                Globals.inventoryMarium.Armour = "Hide";
                 ArmourForMarium(0, 0, 0, 1);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForArmourMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.Armour == "Hide")
                {
                    armorResetMarium(hideArmour.gameObject);
                    return;
                }
                Globals.inventoryTucker.HideArmour = 1;
                Globals.inventoryTucker.Armour = "Hide";
                 ArmourForTucker(0, 0, 1);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForArmourTucker();
            }
            //Globals.shopMerchant.HideArmour -= 1;
            if (Globals.shopMerchant.HideArmour < 0)
                Globals.shopMerchant.HideArmour = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }

    }
    void CharacterArmourSetting(bool l, bool g, bool p, bool c)
    {
        leatherArmor.interactable = l;
        guardArmor.interactable = g;
        paddedArmor.interactable = p;
        chainmailArmor.interactable = c;
    }
    public void ShieldForProtagnist(int wB,int wS,int wM,int mB,int mS,int mM)
    {
        Globals.inventoryProtagnist.WoodenBuckler = wB;
        Globals.inventoryProtagnist.WoodenSmallRounded = wS;
        Globals.inventoryProtagnist.WoodenMediumShield = wM;
        Globals.inventoryProtagnist.MetalBuckler = mB;
        Globals.inventoryProtagnist.MetalSmallRounded = mS;
        Globals.inventoryProtagnist.MetalMediumShield = mM;
    }
    void ShieldForJohn(int wB, int wS, int wM, int mB, int mS, int mM)
    {
        Globals.inventoryJohn.WoodenBuckler = wB;
        Globals.inventoryJohn.WoodenSmallRound = wS;
        Globals.inventoryJohn.WoodenMedium = wM;
        Globals.inventoryJohn.metalBuckler = mB;
        Globals.inventoryJohn.metalSmallRound = mS;
        Globals.inventoryJohn.metalMedium = mM;
    }
    void ShieldForTucker(int wB, int wS, int mB, int mS, int mM)
    {
        Globals.inventoryTucker.WoodenBuckler = wB;
        Globals.inventoryTucker.WoodenSmall = wS;
        Globals.inventoryTucker.MetalBuckler = mB;
        Globals.inventoryTucker.MetalSmall = mS;
        Globals.inventoryTucker.MetalMedium = mM;
    }
    public void ShieldForMarium(int wB, int wS, int mB, int mS)
    {
        Globals.inventoryMarium.WoodenBuckler = wB;
        Globals.inventoryMarium.woodenSmall = wS;
        Globals.inventoryMarium.MetalBuckler = mB;
        Globals.inventoryMarium.MetalSmall = mS;
    }
    void CheckMarkForShield()
    {
        if (Globals.inventoryProtagnist.WoodenBuckler >= 1)
            shield.transform.GetChild(1).gameObject.SetActive(true);
        else
            shield.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.WoodenSmallRounded >= 1)
            woodenRound.transform.GetChild(1).gameObject.SetActive(true);
        else
            woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.WoodenMediumShield >= 1)
            woodenMedium.transform.GetChild(1).gameObject.SetActive(true);
        else
            woodenMedium.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.MetalBuckler >= 1)
            metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.MetalSmallRounded >= 1)
            metalRound.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalRound.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.MetalMediumShield >= 1)
            metalMedium.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalMedium.transform.GetChild(1).gameObject.SetActive(false);
    }
    void CheckMarkForJohnShield()
    {
        if (Globals.inventoryJohn.WoodenBuckler >= 1)
            shield.transform.GetChild(1).gameObject.SetActive(true);
        else
            shield.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WoodenSmallRound >= 1)
            woodenRound.transform.GetChild(1).gameObject.SetActive(true);
        else
            woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WoodenMedium >= 1)
            woodenMedium.transform.GetChild(1).gameObject.SetActive(true);
        else
            woodenMedium.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.metalBuckler >= 1)
            metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.metalSmallRound >= 1)
            metalRound.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalRound.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.metalMedium >= 1)
            metalMedium.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalMedium.transform.GetChild(1).gameObject.SetActive(false);
    }
    void CheckMarkForMarium()
    {
        if (Globals.inventoryMarium.WoodenBuckler >= 1)
            shield.transform.GetChild(1).gameObject.SetActive(true);
        else
            shield.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.woodenSmall >= 1)
        {
            Debug.Log("inside this");
            woodenRound.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("else");
            woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (Globals.inventoryMarium.MetalBuckler >= 1)
            metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.MetalSmall >= 1)
            metalRound.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalRound.transform.GetChild(1).gameObject.SetActive(false);
    }
    void CheckMarkForTucker()
    {
        if (Globals.inventoryTucker.WoodenBuckler >= 1)
            shield.transform.GetChild(1).gameObject.SetActive(true);
        else
            shield.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.WoodenSmall >= 1)
            woodenRound.transform.GetChild(1).gameObject.SetActive(true);
        else
            woodenRound.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.MetalBuckler >= 1)
            metalBuckler.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.MetalSmall >= 1)
            metalRound.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalRound.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.MetalMedium >= 1)
            metalMedium.transform.GetChild(1).gameObject.SetActive(true);
        else
            metalMedium.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void ShieldPurchase(string shieldName)
    {
        if (shieldName == "woodenBuckler")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if(Globals.inventoryProtagnist.WoodenBuckler == 1)
                {
                    shield.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.WoodenBuckler = 0;
                    Globals.inventoryProtagnist.Shield = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                HideProtagonistShield();
                Globals.inventoryProtagnist.WoodenBuckler = 1;
                Globals.inventoryProtagnist.Shield = "woodenBuckler";
                //Globals.inventoryProtagnist. = 1;
                 ShieldForProtagnist(1, 0, 0, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForShield();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.WoodenBuckler == 1)
                {
                    shield.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.WoodenBuckler = 0;
                    Globals.inventoryJohn.Shield = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                HideJohnShield();
                Globals.inventoryJohn.WoodenBuckler = 1;
                Globals.inventoryJohn.Shield = "woodenBuckler";
                ShieldForJohn(1, 0, 0, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnShield();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.WoodenBuckler == 1)
                {
                    shield.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryMarium.WoodenBuckler = 0;
                    Globals.inventoryMarium.Shield = "";
                    db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                    return;
                }
                HideMariumShield();
                Globals.inventoryMarium.WoodenBuckler = 1;
                Globals.inventoryMarium.Shield = "woodenBuckler";
                ShieldForMarium(1, 0, 0, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.WoodenBuckler == 1)
                {
                    shield.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryTucker.WoodenBuckler = 0;
                    Globals.inventoryTucker.Shield = "";
                    db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                    return;
                }
                HideTuckerShield();
                Globals.inventoryTucker.WoodenBuckler = 1;
                Globals.inventoryTucker.Shield = "woodenBuckler";
                ShieldForTucker(1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForTucker();
            }
            //Globals.shopMerchant.WoodenBuckler -= 1;
            //if (Globals.shopMerchant.WoodenBuckler < 0)
            //    Globals.shopMerchant.WoodenBuckler = 0;
            //db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (shieldName == "WoodenRound")
        {
            Debug.Log(Globals.selectedInventoryCharacter);
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.WoodenSmallRounded == 1)
                {
                    woodenRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.WoodenSmallRounded = 0;
                    Globals.inventoryProtagnist.Shield = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                HideProtagonistShield();
                Globals.inventoryProtagnist.WoodenSmallRounded = 1;
                Globals.inventoryProtagnist.Shield = "WoodenRound";
                ShieldForProtagnist(0, 1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForShield();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.WoodenSmallRound == 1)
                {
                    woodenRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryJohn.WoodenSmallRound = 0;
                    Globals.inventoryJohn.Shield = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                HideJohnShield();
                Globals.inventoryJohn.WoodenSmallRound = 1;
                Globals.inventoryJohn.Shield = "WoodenRound";
                ShieldForJohn(0, 1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnShield();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.woodenSmall == 1)
                {
                    woodenRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryMarium.woodenSmall = 0;
                    Globals.inventoryMarium.Shield = "";
                    db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                    return;
                }
                Debug.Log("wooden round");
                HideMariumShield();
                Globals.inventoryMarium.woodenSmall = 1;
                Globals.inventoryMarium.Shield = "WoodenRound";
                ShieldForMarium(0, 1, 0, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.WoodenSmall == 1)
                {
                    woodenRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryTucker.WoodenSmall = 0;
                    Globals.inventoryTucker.Shield = "WoodenRound";
                    db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                    return;
                }
                HideTuckerShield();
                Globals.inventoryTucker.WoodenSmall = 1;
                Globals.inventoryTucker.Shield = "WoodenRound";
                ShieldForTucker(0,1, 0, 0, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForTucker();

            }
            //Globals.shopMerchant.WoodenSmallRounded -= 1;
            //if (Globals.shopMerchant.WoodenSmallRounded < 0)
            //    Globals.shopMerchant.WoodenSmallRounded = 0;
            //db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (shieldName == "WoodenRoundMed")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.WoodenMediumShield == 1)
                {
                    woodenMedium.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.WoodenMediumShield = 0;
                    Globals.inventoryProtagnist.Shield = "WoodenRoundMed";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                HideProtagonistShield();
                Globals.inventoryProtagnist.WoodenMediumShield = 1;
                Globals.inventoryProtagnist.Shield = "";
                ShieldForProtagnist(0, 0, 1, 0, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForShield();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.WoodenMedium == 1)
                {
                    woodenMedium.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryJohn.WoodenMedium = 0;
                    Globals.inventoryJohn.Shield = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                HideJohnShield();
                Globals.inventoryJohn.WoodenMedium = 1;
                Globals.inventoryJohn.Shield = "WoodenRoundMed";
                  ShieldForJohn(0, 0, 1, 0, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnShield();
            }
            //Globals.shopMerchant.WoodenMediumShield -= 1;
            //if (Globals.shopMerchant.WoodenMediumShield < 0)
            //    Globals.shopMerchant.WoodenMediumShield = 0;
            //db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (shieldName == "MetalBuckler")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.MetalBuckler == 1)
                {
                    metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.MetalBuckler = 0;
                    Globals.inventoryProtagnist.Shield = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                HideProtagonistShield();
                Globals.inventoryProtagnist.MetalBuckler = 1;
                Globals.inventoryProtagnist.Shield = "MetalBuckler";
                ShieldForProtagnist(0, 0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForShield();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.metalBuckler == 1)
                {
                    metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryJohn.metalBuckler = 1;
                    Globals.inventoryJohn.Shield = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                HideJohnShield();
                Globals.inventoryJohn.metalBuckler = 1;
                Globals.inventoryJohn.Shield = "MetalBuckler";
                  ShieldForJohn(0, 0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnShield();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.MetalBuckler == 1)
                {
                    metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryMarium.MetalBuckler = 0;
                    Globals.inventoryMarium.Shield = "";
                    db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                    return;
                }
                HideMariumShield();
                Globals.inventoryMarium.MetalBuckler = 1;
                Globals.inventoryMarium.Shield = "MetalBuckler";
                ShieldForMarium(0,0, 1, 0);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.MetalBuckler == 1)
                {
                    metalBuckler.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryTucker.MetalBuckler = 0;
                    Globals.inventoryTucker.Shield = "";
                    db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                    return;
                }
                HideTuckerShield();
                Globals.inventoryTucker.MetalBuckler = 1;
                Globals.inventoryTucker.Shield = "MetalBuckler";
                ShieldForTucker(0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForTucker();
            }
            //Globals.shopMerchant.MetalBuckler -= 1;
            //if (Globals.shopMerchant.MetalBuckler < 0)
            //    Globals.shopMerchant.MetalBuckler = 0;
            //db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (shieldName == "MetalSmall")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.MetalSmallRounded == 1)
                {
                    metalRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.MetalSmallRounded = 0;
                    Globals.inventoryProtagnist.Shield = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                HideProtagonistShield();
                Globals.inventoryProtagnist.MetalSmallRounded = 1;
                Globals.inventoryProtagnist.Shield = "MetalSmall";
                ShieldForProtagnist(0, 0, 0,0, 1, 0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForShield();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.metalSmallRound == 1)
                {
                    metalRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryJohn.metalSmallRound = 0;
                    Globals.inventoryJohn.Shield = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                HideJohnShield();
                Globals.inventoryJohn.metalSmallRound = 1;
                Globals.inventoryJohn.Shield = "MetalSmall";
                 ShieldForJohn(0, 0, 0, 0, 1, 0);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnShield();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                if (Globals.inventoryMarium.MetalSmall == 1)
                {
                    metalRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryMarium.MetalSmall = 0;
                    Globals.inventoryMarium.Shield = "";
                    db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                    return;
                }
                HideMariumShield();
                Globals.inventoryMarium.MetalSmall = 1;
                Globals.inventoryMarium.Shield = "MetalSmall";
                ShieldForMarium(0, 0, 0, 1);
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkForMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.MetalSmall == 1)
                {
                    metalRound.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryTucker.MetalSmall = 0;
                    Globals.inventoryTucker.Shield = "";
                    db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                    return;
                }
                HideTuckerShield();
                Globals.inventoryTucker.MetalSmall = 1;
                ShieldForTucker(0, 0,0, 1, 0);
                Globals.inventoryTucker.Shield = "MetalSmall";
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForTucker();
            }
            //Globals.shopMerchant.MetalSmallRounded -= 1;
            //if (Globals.shopMerchant.MetalSmallRounded < 0)
            //    Globals.shopMerchant.MetalSmallRounded = 0;
            //db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (shieldName == "MetalMedium")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                if (Globals.inventoryProtagnist.MetalMediumShield == 1)
                {
                    metalMedium.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryProtagnist.MetalMediumShield = 0;
                    Globals.inventoryTucker.Shield = "";
                    db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                    return;
                }
                HideProtagonistShield();
                Globals.inventoryProtagnist.MetalMediumShield = 1;
                Globals.inventoryProtagnist.Shield = "MetalMedium";
                ShieldForProtagnist(0, 0, 0, 0, 0, 1);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForShield();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                if (Globals.inventoryJohn.metalMedium == 1)
                {
                    metalMedium.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryJohn.metalMedium = 0;
                    Globals.inventoryJohn.Shield = "";
                    db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                    return;
                }
                HideJohnShield();
                Globals.inventoryJohn.metalMedium = 1;
                Globals.inventoryJohn.Shield = "MetalMedium";
                 ShieldForJohn(0, 0, 0, 0,0,1);
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnShield();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                if (Globals.inventoryTucker.MetalMedium == 1)
                {
                    metalMedium.transform.GetChild(1).gameObject.SetActive(false);
                    Globals.inventoryTucker.MetalMedium = 0;
                    Globals.inventoryTucker.Shield = "";
                    db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                    return;
                }
                HideTuckerShield();
                Globals.inventoryTucker.MetalMedium = 1;
                Globals.inventoryTucker.Shield = "MetalMedium";
                ShieldForTucker(0, 0,0, 0, 1);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkForTucker();
            }
            //Globals.shopMerchant.MetalMediumShield -= 1;
            //if (Globals.shopMerchant.MetalMediumShield < 0)
            //    Globals.shopMerchant.MetalMediumShield = 0;
            //db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
    }
    void HideProtagonistShield()
    {
        Globals.inventoryProtagnist.WoodenBuckler = 0;
        Globals.inventoryProtagnist.WoodenMediumShield = 0;
        Globals.inventoryProtagnist.WoodenSmallRounded = 0;
        Globals.inventoryProtagnist.MetalBuckler = 0;
        Globals.inventoryProtagnist.MetalMediumShield = 0;
        Globals.inventoryProtagnist.MetalSmallRounded = 0;
        db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);

    }
    void HideTuckerShield()
    {
        Globals.inventoryTucker.WoodenBuckler = 0;
        Globals.inventoryTucker.WoodenSmall = 0;
       // Globals.inventoryTucker.woo = 0;
        Globals.inventoryTucker.MetalBuckler = 0;
        Globals.inventoryTucker.MetalSmall = 0;
        Globals.inventoryTucker.MetalMedium = 0;
        db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);

    }
    void HideMariumShield()
    {
        Globals.inventoryMarium.WoodenBuckler = 0;
        Globals.inventoryMarium.woodenSmall = 0;
        Globals.inventoryMarium.MetalBuckler = 0;
        Globals.inventoryMarium.MetalSmall = 0;
        db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
    }
    void HideJohnShield()
    {
        Globals.inventoryJohn.WoodenBuckler = 0;
        Globals.inventoryJohn.WoodenSmallRound = 0;
        Globals.inventoryJohn.WoodenMedium = 0;
        Globals.inventoryJohn.metalBuckler = 0;
        Globals.inventoryJohn.metalSmallRound = 0;
        Globals.inventoryJohn.metalMedium = 0;
        db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
    }
    void ProtagnistSettingForWeapon(bool s)
    {
        Sword.transform.GetChild(1).gameObject.SetActive(s);
        Sword.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ProtagnistWeaponSetting(int d,int s,int sa,int c,int sb,int ls,int lb,int m,int w,int sp,int la,int da,int f,int ma,int co,int cb,int ms)
    {
        Globals.inventoryProtagnist.Dragger = d;
        Globals.inventoryProtagnist.ShortSword = s;
        Globals.inventoryProtagnist.ShortAxe = sa;
        Globals.inventoryProtagnist.Club = c;
        Globals.inventoryProtagnist.ShortBow = sb;
        Globals.inventoryProtagnist.LongSword = ls;
        Globals.inventoryProtagnist.LongBow = lb;
        Globals.inventoryProtagnist.Mace = m;
        Globals.inventoryProtagnist.Warhammer = w;
        Globals.inventoryProtagnist.Spear = sp;
        Globals.inventoryProtagnist.LongAxe = la;
        Globals.inventoryProtagnist.DoubleHeadedAxe = da;
        Globals.inventoryProtagnist.Flair = f;
        Globals.inventoryProtagnist.Maul = ma;
        Globals.inventoryProtagnist.CompositeBow = co;
        Globals.inventoryProtagnist.CrossBow = cb;
        Globals.inventoryProtagnist.magicSword = ms;
    }
    void WeaponForJohn(int sSw,int sA,int w,int lA,int sp,int lSw,int d,int m)
    {
        Globals.inventoryJohn.ShortSword = sSw;
        Globals.inventoryJohn.ShortAxe = sA;
        Globals.inventoryJohn.Warhammer = w;
        Globals.inventoryJohn.LongAxe = lA;
        Globals.inventoryJohn.Spear = sp;
        Globals.inventoryJohn.LongSword = lSw;
        Globals.inventoryJohn.Dragger = d;
        Globals.inventoryJohn.Mace = m;
    }
    public void WeaponForMarium(int sSW,int sA,int w,int s,int sB,int lB,int d)
    {
        Globals.inventoryMarium.ShortSword = sSW;
        Globals.inventoryMarium.ShortAxe = sA;
        Globals.inventoryMarium.Warhammer = w;
        Globals.inventoryMarium.Spear = s;
        Globals.inventoryMarium.ShortBow = sB;
        Globals.inventoryMarium.LongBow = lB;
        Globals.inventoryMarium.Dragger = d;
    }
    void WeaponForTucker(int w,int f,int m,int d,int ma)
    {
        Globals.inventoryTucker.Warhammer = w;
        Globals.inventoryTucker.Flair = f;
        Globals.inventoryTucker.Maul = m;
        Globals.inventoryTucker.Dragger = d;
        Globals.inventoryTucker.Mace = ma;
    }
    void CheckMarkForWeapon()
    {
        if (Globals.inventoryProtagnist.AttackWeapon == "ShortSword")
            Sword.transform.GetChild(1).gameObject.SetActive(true);
        else
            Sword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "MagicSword")
        {
            Debug.Log("..........helo.............");
            magicSword.transform.GetChild(1).gameObject.SetActive(true);
        }
            
        else
            magicSword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "ShortAxe")
            shortAxe.transform.GetChild(1).gameObject.SetActive(true);
        else
            shortAxe.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "warHammer")
            warHammer.transform.GetChild(1).gameObject.SetActive(true);
        else
            warHammer.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "longAxe")
            longAxe.transform.GetChild(1).gameObject.SetActive(true);
        else
            longAxe.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
            Spear.transform.GetChild(1).gameObject.SetActive(true);
        else
            Spear.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "longSword")
            longSword.transform.GetChild(1).gameObject.SetActive(true);
        else
            longSword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "shortBow")
            shortBow.transform.GetChild(1).gameObject.SetActive(true);
        else
            shortBow.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "longBow")
            longBow.transform.GetChild(1).gameObject.SetActive(true);
        else
            longBow.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "Club")
            Club.transform.GetChild(1).gameObject.SetActive(true);
        else
            Club.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "Flair")
            flair.transform.GetChild(1).gameObject.SetActive(true);
        else
            flair.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "Maul")
            Maul.transform.GetChild(1).gameObject.SetActive(true);
        else
            Maul.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "Dragger")
            Dragger.transform.GetChild(1).gameObject.SetActive(true);
        else
            Dragger.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "ShortSword")
            Sword.transform.GetChild(1).gameObject.SetActive(true);
        else
            Sword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryProtagnist.AttackWeapon == "Mace")
            Mace.transform.GetChild(1).gameObject.SetActive(true);
        else
            Mace.transform.GetChild(1).gameObject.SetActive(false);
    }
    void CheckMarkForJohnWeapon()
    {
        if (Globals.inventoryJohn.WeaponAttack== "ShortSword")
            Sword.transform.GetChild(1).gameObject.SetActive(true);
        else
            Sword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WeaponAttack == "ShortAxe")
            shortAxe.transform.GetChild(1).gameObject.SetActive(true);
        else
            shortAxe.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WeaponAttack == "warHammer")
            warHammer.transform.GetChild(1).gameObject.SetActive(true);
        else
            warHammer.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WeaponAttack == "longAxe")
            longAxe.transform.GetChild(1).gameObject.SetActive(true);
        else
            longAxe.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WeaponAttack == "Spear")
            Spear.transform.GetChild(1).gameObject.SetActive(true);
        else
            Spear.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WeaponAttack == "longSword")
            longSword.transform.GetChild(1).gameObject.SetActive(true);
        else
            longSword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WeaponAttack == "Dragger")
            Dragger.transform.GetChild(1).gameObject.SetActive(true);
        else
            Dragger.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryJohn.WeaponAttack == "Mace")
            Mace.transform.GetChild(1).gameObject.SetActive(true);
        else
            Mace.transform.GetChild(1).gameObject.SetActive(false);
    }
    void CheckMarkWeaponForMarium()
    {
        if (Globals.inventoryMarium.WeaponAttack== "ShortSword")
            Sword.transform.GetChild(1).gameObject.SetActive(true);
        else
            Sword.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.WeaponAttack == "ShortAxe")
            shortAxe.transform.GetChild(1).gameObject.SetActive(true);
        else
            shortAxe.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.WeaponAttack == "warHammer")
            warHammer.transform.GetChild(1).gameObject.SetActive(true);
        else
            warHammer.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.WeaponAttack == "Spear")
            Spear.transform.GetChild(1).gameObject.SetActive(true);
        else
            Spear.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.WeaponAttack == "shortBow")
            shortBow.transform.GetChild(1).gameObject.SetActive(true);
        else
            shortBow.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.WeaponAttack == "longBow")
            longBow.transform.GetChild(1).gameObject.SetActive(true);
        else
            longBow.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryMarium.WeaponAttack == "Dragger")
            Dragger.transform.GetChild(1).gameObject.SetActive(true);
        else
            Dragger.transform.GetChild(1).gameObject.SetActive(false);
    }
    void CheckMarkWeaponForTucker()
    {
        if (Globals.inventoryTucker.WeaponAttack== "warHammer")
            warHammer.transform.GetChild(1).gameObject.SetActive(true);
        else
            warHammer.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.WeaponAttack == "Flair")
            flair.transform.GetChild(1).gameObject.SetActive(true);
        else
            flair.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.WeaponAttack == "Maul")
            Maul.transform.GetChild(1).gameObject.SetActive(true);
        else
            Maul.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.WeaponAttack == "Dragger")
            Dragger.transform.GetChild(1).gameObject.SetActive(true);
        else
            Dragger.transform.GetChild(1).gameObject.SetActive(false);
        if (Globals.inventoryTucker.WeaponAttack == "Mace")
            Mace.transform.GetChild(1).gameObject.SetActive(true);
        else
            Mace.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void WeaponPurchase(string weaponName)
    {
        Debug.Log("wepaon selected");
        if (weaponName == "ShortSword")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.ShortSword = 1;
                Globals.inventoryProtagnist.AttackWeapon = "ShortSword";
                 ProtagnistWeaponSetting(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.ShortSword = 1;
                WeaponForJohn(1, 0, 0, 0, 0, 0, 0, 0);
                Globals.inventoryJohn.WeaponAttack = "ShortSword";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.ShortSword = 1;
                WeaponForMarium(1, 0, 0, 0, 0, 0, 0);
                Globals.inventoryMarium.WeaponAttack = "ShortSword";
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkWeaponForMarium();
            }
          //  Globals.shopMerchant.ShortSword -= 1;
            if (Globals.shopMerchant.ShortSword < 0)
                Globals.shopMerchant.ShortSword = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName== "MagicSword")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Debug.Log("wepaon selected selected magic sword");
                Globals.inventoryProtagnist.magicSword = 1;
                Globals.inventoryProtagnist.AttackWeapon = "MagicSword";
                 ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                Globals.shopMerchant.MagicSword = 1;
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                CheckMarkForWeapon();
            }
        }
        else if (weaponName == "ShortAxe")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.ShortAxe = 1;
                Globals.inventoryProtagnist.AttackWeapon = "ShortAxe";
                 ProtagnistWeaponSetting(0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.ShortAxe = 1;
                WeaponForJohn(0, 1, 0, 0, 0, 0, 0, 0);
                Globals.inventoryJohn.WeaponAttack = "ShortAxe";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.ShortAxe = 1;
               WeaponForMarium(0, 1, 0, 0, 0, 0, 0);
                Globals.inventoryMarium.WeaponAttack = "ShortAxe";
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkWeaponForMarium();
            }
          //  Globals.shopMerchant.ShortAxe -= 1;
            if (Globals.shopMerchant.ShortAxe < 0)
                Globals.shopMerchant.ShortAxe = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "warHammer")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Warhammer = 1;
                Globals.inventoryProtagnist.AttackWeapon = "warHammer";
                 ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Warhammer = 1;
                WeaponForJohn(0, 0, 1, 0, 0, 0, 0, 0);
                Globals.inventoryJohn.WeaponAttack = "warHammer";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.Warhammer = 1;
                WeaponForMarium(0,0,1, 0, 0, 0, 0);
                Globals.inventoryMarium.WeaponAttack = "warHammer";
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkWeaponForMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Warhammer = 1;
                Globals.inventoryTucker.WeaponAttack = "warHammer";
                  WeaponForTucker(1, 0, 0, 0, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkWeaponForTucker();
            }
         //   Globals.shopMerchant.Warhammer -= 1;
            if (Globals.shopMerchant.Warhammer < 0)
                Globals.shopMerchant.Warhammer = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "longAxe")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.LongAxe = 1;
                Globals.inventoryProtagnist.AttackWeapon = "longAxe";
                 ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0,1, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.LongAxe = 1;
                WeaponForJohn(0, 0, 0, 1, 0, 0, 0, 0);
                Globals.inventoryJohn.WeaponAttack = "longAxe";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
          //  Globals.shopMerchant.LongAxe -= 1;
            if (Globals.shopMerchant.LongAxe < 0)
                Globals.shopMerchant.ShortSword = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "Spear")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Spear = 1;
                Globals.inventoryProtagnist.AttackWeapon = "Spear";
                 ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Spear = 1;
                WeaponForJohn(0, 0, 0, 0, 1, 0, 0, 0);
                Globals.inventoryJohn.WeaponAttack = "Spear";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.Spear = 1;
                WeaponForMarium(0, 0, 0, 1, 0, 0, 0);
                Globals.inventoryMarium.WeaponAttack = "Spear";
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkWeaponForMarium();
            }
         //   Globals.shopMerchant.Spear -= 1;
            if (Globals.shopMerchant.Spear < 0)
                Globals.shopMerchant.Spear = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "longSword")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.LongSword = 1;
                Globals.inventoryProtagnist.AttackWeapon = "longSword";
                  ProtagnistWeaponSetting(0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.LongSword = 1;
                WeaponForJohn(0, 0, 0, 0, 0,1, 0, 0);
                Globals.inventoryJohn.WeaponAttack = "longSword";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
         //   Globals.shopMerchant.LongSword -= 1;
            if (Globals.shopMerchant.LongSword < 0)
                Globals.shopMerchant.LongSword = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "shortBow")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.ShortBow = 1;
                Globals.inventoryProtagnist.AttackWeapon = "shortBow";
                 ProtagnistWeaponSetting(0,0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.ShortBow = 1;
                WeaponForMarium(0, 0, 0, 0,1, 0, 0);
                Globals.inventoryMarium.WeaponAttack = "shortBow";
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkWeaponForMarium();
            }
          //  Globals.shopMerchant.ShortBow -= 1;
            if (Globals.shopMerchant.ShortBow < 0)
                Globals.shopMerchant.ShortBow = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "longBow")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.LongBow = 1;
                Globals.inventoryProtagnist.AttackWeapon = "longBow";
                 ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.LongBow = 1;
                WeaponForMarium(0, 0, 0, 0, 0, 1, 0);
                Globals.inventoryMarium.WeaponAttack = "longBow";
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkWeaponForMarium();
            }
          //  Globals.shopMerchant.LongBow -= 1;
            if (Globals.shopMerchant.LongBow < 0)
                Globals.shopMerchant.LongBow = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "Club")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Club = 1;
                Globals.inventoryProtagnist.AttackWeapon = "Club";
                 ProtagnistWeaponSetting(0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
         //   Globals.shopMerchant.Club -= 1;
            if (Globals.shopMerchant.Club < 0)
                Globals.shopMerchant.Club = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "Flair")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Flair = 1;
                Globals.inventoryProtagnist.AttackWeapon = "Flair";
                 ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Flair = 1;
                Globals.inventoryTucker.WeaponAttack = "Flair";
                 WeaponForTucker(0, 1, 0, 0, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkWeaponForTucker();
            }
          //  Globals.shopMerchant.Flair -= 1;
            if (Globals.shopMerchant.Flair < 0)
                Globals.shopMerchant.Flair = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "Maul")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Maul = 1;
                Globals.inventoryProtagnist.AttackWeapon = "Maul";
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,1, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Maul = 1;
                Globals.inventoryTucker.WeaponAttack = "Maul";
                WeaponForTucker(0, 0, 1, 0, 0);
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkWeaponForTucker();
            }
           // Globals.shopMerchant.Maul -= 1;
            if (Globals.shopMerchant.Maul < 0)
                Globals.shopMerchant.Maul = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "Dragger")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Dragger = 1;
                Globals.inventoryProtagnist.AttackWeapon = "Dragger";
                ProtagnistWeaponSetting(1,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Dragger = 1;
                WeaponForJohn(0, 0, 0, 0, 0, 0, 1, 0);
                Globals.inventoryJohn.WeaponAttack = "Dragger";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Marium")
            {
                Globals.inventoryMarium.Dragger = 1;
                WeaponForMarium(0, 0, 0, 0, 0, 0, 1);
                Globals.inventoryMarium.WeaponAttack = "Dragger";
                db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                CheckMarkWeaponForMarium();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Dragger = 1;
                  WeaponForTucker(0, 0, 0,1, 0);
                Globals.inventoryTucker.WeaponAttack = "Dragger";
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkWeaponForTucker();
            }
         //   Globals.shopMerchant.Dragger -= 1;
            if (Globals.shopMerchant.Dragger < 0)
                Globals.shopMerchant.Dragger = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
        else if (weaponName == "Mace")
        {
            if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            {
                Globals.inventoryProtagnist.Mace = 1;
                Globals.inventoryProtagnist.AttackWeapon = "Mace";
                  ProtagnistWeaponSetting(0,0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0,0);
                db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
                CheckMarkForWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "John")
            {
                Globals.inventoryJohn.Mace = 1;
                WeaponForJohn(0, 0, 0, 0, 0, 0,0,1);
                Globals.inventoryJohn.WeaponAttack = "Mace";
                db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
                CheckMarkForJohnWeapon();
            }
            else if (Globals.selectedInventoryCharacter == "Tucker")
            {
                Globals.inventoryTucker.Mace = 1;
                 WeaponForTucker(0, 0, 0, 0, 1);
                Globals.inventoryTucker.WeaponAttack = "Mace";
                db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
                CheckMarkWeaponForTucker();
            }
       //     Globals.shopMerchant.Mace -= 1;
            if (Globals.shopMerchant.Mace < 0)
                Globals.shopMerchant.Mace = 0;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
    }
    void EquipmentSecondList(bool w, bool a, bool h, bool s)
    {
        weaponList.SetActive(w);
        armourList.SetActive(a);
        helmetList.SetActive(h);
        shieldList.SetActive(s);
    }
    void EquipmentPanelSetting(bool w, bool d, bool h, bool a,bool l)
    {
        weaponPanel.SetActive(w);
        shieldPanel.SetActive(d);
        helmetPanel.SetActive(h);
        armourPanel.SetActive(a);
        itemPanel.SetActive(l);
    }
    void InventoryPArtsSetting(bool a, bool h, bool w, bool s,bool o)
    {
        isArmor = a;
        isHelmet = h;
        isWeapon = w;
        isShield = s;
        isItem = o;
    }
    void InventorySetupForCompanion()
    {
        Debug.Log("is character :: "+isCharacter+" is weapon :: "+isWeapon + " is shield :: "+isShield+" is armour :: "+isArmor + " is helmet :: "+ isHelmet);
        if (isCharacter)
            FindObjectOfType<DataInInventory>().SetImage();
        if (isWeapon)
            WeaponSettingForProtagnist();
        else if (isShield)
            ShieldSettingForProtagnist();
        else if (isArmor)
            ArmourSettingForProtagnist();
        else if (isHelmet)
            HelmetSettingForProtagnist();
        else if (isItem) 
            ItemSetting();
    }
    void InventoryTopSetting(bool c, bool e, bool s)
    {
        isCharacter = c;
        isEquipement = e;
        isStatistics = s;
    }
    void InventoryPanelSetting(bool c, bool e, bool s)
    {
        characterPanel.SetActive(c);
        equipmentPanel.SetActive(e);
        staticsPanel.SetActive(s);
    }


    void PrintValues()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale" || Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale" || Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            AttackCalculator.instance.AttckEntityForInventory();
            Debug.Log("health  :: " + player[0].health );
            if (Globals.selectedInventoryCharacter == "WarriorMale")
            {
               player[0].GetComponent<PlayerItem>().InitializePlayerItem(protagonistItemLib.PlayerCharacterLibrary[0]);
            }
            else if (Globals.selectedInventoryCharacter == "WarriorFemale")
            {
                player[0].GetComponent<PlayerItem>().InitializePlayerItem(protagonistItemLib.PlayerCharacterLibrary[3]);
            }
            else if (Globals.selectedInventoryCharacter == "ArcherFemale")
            {
                player[0].GetComponent<PlayerItem>().InitializePlayerItem(protagonistItemLib.PlayerCharacterLibrary[0]);
            }
            else if (Globals.selectedInventoryCharacter == "ArcherMale")
            {
                Debug.Log("archer male.......");
                player[0].GetComponent<PlayerItem>().InitializePlayerItem(protagonistItemLib.PlayerCharacterLibrary[1]);
            }

            Debug.Log("health after :: "+ player[0].health + "player weapon id :: "+player[0].weaponId);
            float attackvalue = AttackCalculator.instance.GetTotalWeaponDamage(player[0]);
            Debug.Log("attack value : "+ attackvalue);
            float defenceValue = AttackCalculator.instance.GetPlayerDefenceValue(player[0]);
            level.text = player[0].level.ToString();
            PrintStatisticsValue(attackvalue, defenceValue,player[0].health);
        }
        if (Globals.selectedInventoryCharacter == "John")
        {
            Debug.Log("john.......");
            AttackCalculator.instance.AttckEntityForInventory();
            player[1].GetComponent<PlayerItem>().InitializePlayerItem(companionLib.PlayerCharacterLibrary[0]);

            float attackvalue = AttackCalculator.instance.GetTotalWeaponDamage(player[1]);
            Debug.Log("attack value : " + attackvalue);
            float defenceValue = AttackCalculator.instance.GetPlayerDefenceValue(player[1]);
            level.text = player[1].level.ToString();
            PrintStatisticsValue(attackvalue, defenceValue, player[1].health);
        }
        if (Globals.selectedInventoryCharacter == "Marium")
        {
            Debug.Log("marium.......");
            AttackCalculator.instance.AttckEntityForInventory();
            player[2].GetComponent<PlayerItem>().InitializePlayerItem(companionLib.PlayerCharacterLibrary[1]);

            float attackvalue = AttackCalculator.instance.GetTotalWeaponDamage(player[2]);
            Debug.Log("attack value : " + attackvalue);
            float defenceValue = AttackCalculator.instance.GetPlayerDefenceValue(player[2]);
            level.text = player[2].level.ToString();
            PrintStatisticsValue(attackvalue, defenceValue, player[2].health);
        }
        if (Globals.selectedInventoryCharacter == "Tucker")
        {
            Debug.Log("tucker.......");
            AttackCalculator.instance.AttckEntityForInventory();
            player[3].GetComponent<PlayerItem>().InitializePlayerItem(companionLib.PlayerCharacterLibrary[2]);

            float attackvalue = AttackCalculator.instance.GetTotalWeaponDamage(player[3]);
            Debug.Log("attack value : " + attackvalue);
            float defenceValue = AttackCalculator.instance.GetPlayerDefenceValue(player[3]);
            level.text = player[3].level.ToString();
            PrintStatisticsValue(attackvalue, defenceValue, player[3].health);
        }
    }

    void PrintStatisticsValue(float attackValue, float defenceValue, float h)
    {
        attack.text = attackValue.ToString();
        defence.text = defenceValue.ToString();
        health.text = h.ToString();
    }

    void ResetMerchantShopArmour()
    {
        if (Globals.inventoryProtagnist.LeatherArmour == 1 || Globals.inventoryJohn.LeatherArmour == 1 || Globals.inventoryMarium.LeatherArmour == 1 || Globals.inventoryTucker.LeatherArmour == 1)
        {
            Globals.shopMerchant.LeatherArmour += 1; 
        }
        else if (Globals.inventoryProtagnist.PaddedArmour == 1 /*|| Globals.inventoryJohn.PaddedArmour == 1 || Globals.inventoryMarium.PaddedArmour == 1 || Globals.inventoryTucker.PaddedArmour == 1*/)
        {
            Globals.shopMerchant.PaddedArmour += 1;
        }
        else if (Globals.inventoryProtagnist.ScaleArmour == 1 || Globals.inventoryJohn.ScaleArmour == 1)
        {
            Globals.shopMerchant.ScaleArmour += 1;
        }
        else if (Globals.inventoryProtagnist.HideArmour == 1 || Globals.inventoryJohn.HideArmour == 1 || Globals.inventoryMarium.HideArmour == 1 || Globals.inventoryTucker.HideArmour == 1)
        {
            Globals.shopMerchant.HideArmour += 1;
        }
        else if (Globals.inventoryProtagnist.ChainArmour == 1 || Globals.inventoryJohn.ChainArmour == 1)
        {
            Globals.shopMerchant.ChainArmour += 1;
        }
        else if (Globals.inventoryProtagnist.BrigadineArmor == 1 || Globals.inventoryJohn.BrigadineArmour == 1 || Globals.inventoryMarium.BrigadineArmour == 1)
        {
            Globals.shopMerchant.BrigadineArmor += 1;
        }
        db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
    }

    void armorResetProtagnist(GameObject Armor)
    {
        Armor.transform.GetChild(2).gameObject.SetActive(false);
        ArmourForProtagnist(0, 0, 0, 0, 0, 0);
        Globals.inventoryProtagnist.Armour = "";
        db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
    }
    void armorResetJohn(GameObject armor)
    {
        armor.transform.GetChild(2).gameObject.SetActive(false);
        ArmourForJohn(0, 0, 0, 0, 0, 0);
        Globals.inventoryJohn.Armour = "";
        db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
    }
    void armorResetMarium(GameObject armor)
    {
        armor.transform.GetChild(2).gameObject.SetActive(false);
        ArmourForMarium(0, 0, 0, 0);
        Globals.inventoryMarium.Armour = "";
        db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
    }
    void armorResetTucker(GameObject armor)
    {
        armor.transform.GetChild(2).gameObject.SetActive(false);
        ArmourForTucker(0, 0, 0);
        Globals.inventoryTucker.Armour = "";
        db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
    }


}
