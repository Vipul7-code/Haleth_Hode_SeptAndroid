using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopIntegeration : MonoBehaviour
{
    DatabaseManager db;
    [SerializeField]
    GameObject weaponPanel, shieldPanel, armourPanel, itemPanel,NoGold,noItemText;
    [SerializeField]
    Text goldText, swordText, draggerText, shortAxeText, clubText, shortBowText, longSwordText, longBowText, maceText, warHammerText, spearText, longAxeText, doubleHeadedAxeText, flairText, maulText, compositeBowText, crossBowText, woodenBText, woodenSText, metalBText, metalSText, woodenMText, metalMText, wwodenKiteText, woodenTowerText, metalKiteText, clothAText, paddedAText, leatherCapText, hideAText, leatherAText, kettleHatText, brigadineAText, scaleAText, chainAText, nesalHText, avainTailText, mailCoifText, breastPlateText, ringmailText, splintMailText, bandedMailText, AleText, cureText, healText, foodText, meatText, rumText;
    float tempScore, timeStarted;
    bool isUpdatingScore = false;
    [SerializeField]
    GameObject[] BuyButtons, sellButtons;
    [SerializeField]
    GameObject sell, sellSelected, buy, buySelected;
    // Start is called before the first frame update
    void Start()
    {
        db = FindObjectOfType<DatabaseManager>();
        // WeaponPanelSetting();
        ReadDB();
        goldText.text = Globals.shopMerchant.Gold.ToString();
    }
    void MerchantButtonSetting(bool b,bool bs, bool s, bool ss)
    {
        buy.SetActive(b);
        buySelected.SetActive(bs);
        sell.SetActive(s);
        sellSelected.SetActive(ss);
    }
    public void ClickOnButton(string btn_name)
    {
        switch (btn_name)
        {
            case "Sword":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.ShortSword += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    swordText.text = Globals.shopMerchant.ShortSword.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Mace":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.Mace += 1;
                    Globals.shopMerchant.Gold -= 50;
                    maceText.text = Globals.shopMerchant.Mace.ToString();
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "woodenShield":
                if (Globals.shopMerchant.Gold >= 15)
                {
                    Globals.shopMerchant.WoodenBuckler += 1;
                    Globals.shopMerchant.Gold -= 15;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenSText.text = Globals.shopMerchant.WoodenBuckler.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "pddedArmour":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.PaddedArmour += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    paddedAText.text = Globals.shopMerchant.PaddedArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "chainArmour":
                if (Globals.shopMerchant.Gold >= 250)
                {
                    Globals.shopMerchant.ChainArmour += 1;
                    Globals.shopMerchant.Gold -= 250;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    chainAText.text = Globals.shopMerchant.ChainArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "leatherArmour":
                if (Globals.shopMerchant.Gold >= 75)
                {
                    Globals.shopMerchant.LeatherArmour += 1;
                    Globals.shopMerchant.Gold -= 75;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    leatherAText.text = Globals.shopMerchant.LeatherArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Ale":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.Ale += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    AleText.text = Globals.shopMerchant.Ale.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "curePotion":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.CurePotion += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    cureText.text = Globals.shopMerchant.CurePotion.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Food":
                if (Globals.shopMerchant.Gold >= 10)
                {
                    Globals.shopMerchant.Food += 1;
                    Globals.shopMerchant.Gold -= 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    foodText.text = Globals.shopMerchant.Food.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Meat":
                if (Globals.shopMerchant.Gold >= 10)
                {
                    Globals.shopMerchant.Meat += 1;
                    Globals.shopMerchant.Gold -= 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    meatText.text = Globals.shopMerchant.Meat.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "HealPotion":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.HealPotion += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    healText.text = Globals.shopMerchant.HealPotion.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Rum":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.Rum += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    rumText.text = Globals.shopMerchant.Rum.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Okay":
                NoGold.SetActive(false);
                break;
            case "merchantWeapon":
                MerchantShopSetting(true, false, false, false);
               ReadDB();
                break;
            case "merchantShield":
                MerchantShopSetting(false, true, false, false);
                ReadDB();
                break;
            case "merchantArmour":
                MerchantShopSetting(false, false, true, false);
                ReadDB();
                break;
            case "merchantItems":
                MerchantShopSetting(false, false, false, true);
                ReadDB();
                break;
            case "SwordShort":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.ShortSword += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    swordText.text = Globals.shopMerchant.ShortSword.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "ShortAxe":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.ShortAxe += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    shortAxeText.text = Globals.shopMerchant.ShortAxe.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "ShortBow":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.ShortBow += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    shortBowText.text = Globals.shopMerchant.ShortBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "WoodenBuckler":
                if (Globals.shopMerchant.Gold >= 15)
                {
                    Globals.shopMerchant.WoodenBuckler += 1;
                    Globals.shopMerchant.Gold -= 15;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenBText.text = Globals.shopMerchant.WoodenBuckler.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "woodenSmallRoundShield":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.WoodenSmallRounded += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenSText.text = Globals.shopMerchant.WoodenSmallRounded.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "LeatherCap":
                if (Globals.shopMerchant.Gold >= 10)
                {
                    Globals.shopMerchant.LeatherCap += 1;
                    Globals.shopMerchant.Gold -= 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    leatherCapText.text = Globals.shopMerchant.LeatherCap.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Dragger":
                if (Globals.shopMerchant.Gold >= 10)
                {
                    Globals.shopMerchant.Dragger += 1;
                    Globals.shopMerchant.Gold -= 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    draggerText.text = Globals.shopMerchant.Dragger.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "MetalBuckler":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.MetalBuckler += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalBText.text = Globals.shopMerchant.MetalBuckler.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "MetalBucklerRound":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.MetalSmallRounded += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalSText.text = Globals.shopMerchant.MetalSmallRounded.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "HideArmour":
                if (Globals.shopMerchant.Gold >= 65)
                {
                    Globals.shopMerchant.HideArmour += 1;
                    Globals.shopMerchant.Gold -= 65;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    hideAText.text = Globals.shopMerchant.HideArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "KetalHat":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.KettleHat += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    kettleHatText.text = Globals.shopMerchant.KettleHat.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "longSword":
                if (Globals.shopMerchant.Gold >= 100)
                {
                    Globals.shopMerchant.LongSword += 1;
                    Globals.shopMerchant.Gold -= 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    longSwordText.text = Globals.shopMerchant.LongSword.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "longAxe":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.LongAxe += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    longAxeText.text = Globals.shopMerchant.LongAxe.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Club":
                if (Globals.shopMerchant.Gold >= 10)
                {
                    Globals.shopMerchant.Club += 1;
                    Globals.shopMerchant.Gold -= 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    clubText.text = Globals.shopMerchant.Club.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "warHammer":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.Warhammer += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    warHammerText.text = Globals.shopMerchant.Warhammer.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Spear":
                if (Globals.shopMerchant.Gold >= 75)
                {
                    Globals.shopMerchant.Spear += 1;
                    Globals.shopMerchant.Gold -= 75;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    spearText.text = Globals.shopMerchant.Spear.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "longBow":
                if (Globals.shopMerchant.Gold >= 75)
                {
                    Globals.shopMerchant.LongBow += 1;
                    Globals.shopMerchant.Gold -= 75;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                   longBowText.text = Globals.shopMerchant.LongBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "woodenMediumRoundShield":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.WoodenMediumShield += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenMText.text = Globals.shopMerchant.WoodenMediumShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "metalBuckler":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.MetalBuckler += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalBText.text = Globals.shopMerchant.MetalBuckler.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "metalSmallRound":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.MetalSmallRounded += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalSText.text = Globals.shopMerchant.MetalSmallRounded.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "metalMediumRound":
                if (Globals.shopMerchant.Gold >= 100)
                {
                    Globals.shopMerchant.MetalMediumShield += 1;
                    Globals.shopMerchant.Gold -= 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalMText.text = Globals.shopMerchant.MetalMediumShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "DoubleHeadedAxe":
                if (Globals.shopMerchant.Gold >= 250)
                {
                    Globals.shopMerchant.DoubleHeadedAxe += 1;
                    Globals.shopMerchant.Gold -= 250;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    doubleHeadedAxeText.text = Globals.shopMerchant.DoubleHeadedAxe.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Flair":
                if (Globals.shopMerchant.Gold >= 100)
                {
                    Globals.shopMerchant.Flair += 1;
                    Globals.shopMerchant.Gold -= 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    flairText.text = Globals.shopMerchant.Flair.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Maul":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.Maul += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    maulText.text = Globals.shopMerchant.Maul.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "CompositeBow":
                if (Globals.shopMerchant.Gold >= 150)
                {
                    Globals.shopMerchant.CompositeBow += 1;
                    Globals.shopMerchant.Gold -= 150;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    compositeBowText.text = Globals.shopMerchant.CompositeBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "CrossBow":
                if (Globals.shopMerchant.Gold >= 250)
                {
                    Globals.shopMerchant.CrossBow += 1;
                    Globals.shopMerchant.Gold -= 250;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    crossBowText.text = Globals.shopMerchant.CrossBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "woodenKiteShield":
                if (Globals.shopMerchant.Gold >= 50)
                {
                    Globals.shopMerchant.WoodenKiteShield += 1;
                    Globals.shopMerchant.Gold -= 50;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    wwodenKiteText.text = Globals.shopMerchant.WoodenKiteShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "woodenTowerShield":
                if (Globals.shopMerchant.Gold >= 100)
                {
                    Globals.shopMerchant.WoodenTowerShield += 1;
                    Globals.shopMerchant.Gold -= 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenTowerText.text = Globals.shopMerchant.WoodenTowerShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "metalKiteShield":
                if (Globals.shopMerchant.Gold >= 100)
                {
                    Globals.shopMerchant.MetalKiteShield += 1;
                    Globals.shopMerchant.Gold -= 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalKiteText.text = Globals.shopMerchant.MetalKiteShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "ClothArmour":
                if (Globals.shopMerchant.Gold >= 25)
                {
                    Globals.shopMerchant.ClothArmour += 1;
                    Globals.shopMerchant.Gold -= 25;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    clothAText.text = Globals.shopMerchant.ClothArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "BrigadineArmor":
                if (Globals.shopMerchant.Gold >= 100)
                {
                    Globals.shopMerchant.BrigadineArmor += 1;
                    Globals.shopMerchant.Gold -= 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    brigadineAText.text = Globals.shopMerchant.BrigadineArmor.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "ScaleArmour":
                if (Globals.shopMerchant.Gold >= 150)
                {
                    Globals.shopMerchant.ScaleArmour += 1;
                    Globals.shopMerchant.Gold -= 150;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    scaleAText.text = Globals.shopMerchant.ScaleArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "BreastPlateArmour":
                if (Globals.shopMerchant.Gold >= 300)
                {
                    Globals.shopMerchant.BreastPlateArmour += 1;
                    Globals.shopMerchant.Gold -= 300;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    breastPlateText.text = Globals.shopMerchant.BreastPlateArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "ringMailArmour":
                if (Globals.shopMerchant.Gold >= 350)
                {
                    Globals.shopMerchant.RingMailArmour += 1;
                    Globals.shopMerchant.Gold -= 350;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    ringmailText.text = Globals.shopMerchant.RingMailArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "SplintMailArmour":
                if (Globals.shopMerchant.Gold >= 400)
                {
                    Globals.shopMerchant.SplintmailArmor += 1;
                    Globals.shopMerchant.Gold -= 400;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    splintMailText.text = Globals.shopMerchant.SplintmailArmor.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "BandedMailArmour":
                if (Globals.shopMerchant.Gold >= 500)
                {
                    Globals.shopMerchant.BandedMailArmour += 1;
                    Globals.shopMerchant.Gold -= 500;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    bandedMailText.text = Globals.shopMerchant.BandedMailArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "NasalHelmet":
                if (Globals.shopMerchant.Gold >= 75)
                {
                    Globals.shopMerchant.NesalHelmet += 1;
                    Globals.shopMerchant.Gold -= 75;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    nesalHText.text = Globals.shopMerchant.NesalHelmet.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "Aventail":
                if (Globals.shopMerchant.Gold >= 100)
                {
                    Globals.shopMerchant.Aventail += 1;
                    Globals.shopMerchant.Gold -= 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    avainTailText.text = Globals.shopMerchant.Aventail.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "MailCoif":
                if (Globals.shopMerchant.Gold >= 150)
                {
                    Globals.shopMerchant.MailCoif += 1;
                    Globals.shopMerchant.Gold -= 150;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    mailCoifText.text = Globals.shopMerchant.MailCoif.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "You Don't have enough coins";
                }
                break;
            case "DraggerSell":
                if (Globals.shopMerchant.Dragger >= 1)
                {
                    Globals.shopMerchant.Dragger -= 1;
                    Globals.shopMerchant.Gold += 4;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    draggerText.text = Globals.shopMerchant.Dragger.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "ShortSwordS":
                if (Globals.shopMerchant.ShortSword >= 1)
                {
                    Globals.shopMerchant.ShortSword -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    swordText.text = Globals.shopMerchant.ShortSword.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "ShortAxeS":
                if (Globals.shopMerchant.ShortAxe >= 1)
                {
                    Globals.shopMerchant.ShortAxe -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    shortAxeText.text = Globals.shopMerchant.ShortAxe.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "ClubS":
                if (Globals.shopMerchant.Club >= 1)
                {
                    Globals.shopMerchant.Club -= 1;
                    Globals.shopMerchant.Gold += 4;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    clubText.text = Globals.shopMerchant.Club.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "ShortBowS":
                if (Globals.shopMerchant.ShortBow >= 1)
                {
                    Globals.shopMerchant.ShortBow -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    shortBowText.text = Globals.shopMerchant.ShortBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "WoodenBucklerS":
                if (Globals.shopMerchant.WoodenBuckler >= 1)
                {
                    Globals.shopMerchant.WoodenBuckler -= 1;
                    Globals.shopMerchant.Gold += 6;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenBText.text = Globals.shopMerchant.WoodenBuckler.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "WoodenSmallRoundS":
                if (Globals.shopMerchant.WoodenSmallRounded >= 1)
                {
                    Globals.shopMerchant.WoodenSmallRounded -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenSText.text = Globals.shopMerchant.WoodenSmallRounded.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "ClothArmourS":
                if (Globals.shopMerchant.ClothArmour >= 1)
                {
                    Globals.shopMerchant.ClothArmour -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    clothAText.text = Globals.shopMerchant.ClothArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "PaddedArmourS":
                if (Globals.shopMerchant.PaddedArmour >= 1)
                {
                    Globals.shopMerchant.PaddedArmour -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    paddedAText.text = Globals.shopMerchant.PaddedArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "LeatherCapS":
                if (Globals.shopMerchant.LeatherCap >= 1)
                {
                    Globals.shopMerchant.LeatherCap -= 1;
                    Globals.shopMerchant.Gold += 4;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    leatherCapText.text = Globals.shopMerchant.LeatherCap.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MetalBucklerS":
                if (Globals.shopMerchant.MetalBuckler >= 1)
                {
                    Globals.shopMerchant.MetalBuckler -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalBText.text = Globals.shopMerchant.MetalBuckler.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MetalSmallRoundS":
                if (Globals.shopMerchant.MetalSmallRounded >= 1)
                {
                    Globals.shopMerchant.MetalSmallRounded -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalSText.text = Globals.shopMerchant.MetalSmallRounded.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "HideArmourS":
                if (Globals.shopMerchant.HideArmour >= 1)
                {
                    Globals.shopMerchant.HideArmour -= 1;
                    Globals.shopMerchant.Gold += 26;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    hideAText.text = Globals.shopMerchant.HideArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "LeatherArmourS":
                if (Globals.shopMerchant.LeatherArmour >= 1)
                {
                    Globals.shopMerchant.LeatherArmour -= 1;
                    Globals.shopMerchant.Gold += 30;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    leatherAText.text = Globals.shopMerchant.LeatherArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "KettleHatS":
                if (Globals.shopMerchant.KettleHat >= 1)
                {
                    Globals.shopMerchant.KettleHat -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    kettleHatText.text = Globals.shopMerchant.KettleHat.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "LongSwordS":
                if (Globals.shopMerchant.LongSword >= 1)
                {
                    Globals.shopMerchant.LongSword -= 1;
                    Globals.shopMerchant.Gold += 40;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    longSwordText.text = Globals.shopMerchant.LongSword.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "LongAxeS":
                if (Globals.shopMerchant.LongAxe >= 1)
                {
                    Globals.shopMerchant.LongAxe -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    longAxeText.text = Globals.shopMerchant.LongAxe.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MaceS":
                if (Globals.shopMerchant.Mace >= 1)
                {
                    Globals.shopMerchant.Mace -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    maceText.text = Globals.shopMerchant.Mace.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "WarHammerS":
                if (Globals.shopMerchant.Warhammer >= 1)
                {
                    Globals.shopMerchant.Warhammer -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    warHammerText.text = Globals.shopMerchant.Warhammer.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "SpearS":
                if (Globals.shopMerchant.Spear >= 1)
                {
                    Globals.shopMerchant.Spear -= 1;
                    Globals.shopMerchant.Gold += 30;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    spearText.text = Globals.shopMerchant.Spear.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "LongBowS":
                if (Globals.shopMerchant.LongBow >= 1)
                {
                    Globals.shopMerchant.LongBow -= 1;
                    Globals.shopMerchant.Gold += 30;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    longBowText.text = Globals.shopMerchant.LongBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "WoodenMediumS":
                if (Globals.shopMerchant.WoodenMediumShield >= 1)
                {
                    Globals.shopMerchant.WoodenMediumShield -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                   woodenMText.text = Globals.shopMerchant.WoodenMediumShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MetalMediumS":
                if (Globals.shopMerchant.MetalMediumShield >= 1)
                {
                    Globals.shopMerchant.MetalMediumShield -= 1;
                    Globals.shopMerchant.Gold += 40;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalMText.text = Globals.shopMerchant.MetalMediumShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "BrigadineArmourS":
                if (Globals.shopMerchant.BreastPlateArmour >= 1)
                {
                    Globals.shopMerchant.BrigadineArmor -= 1;
                    Globals.shopMerchant.Gold += 40;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    brigadineAText.text = Globals.shopMerchant.BrigadineArmor.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "ScaleArmourS":
                if (Globals.shopMerchant.ScaleArmour >= 1)
                {
                    Globals.shopMerchant.ScaleArmour -= 1;
                    Globals.shopMerchant.Gold += 60;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    scaleAText.text = Globals.shopMerchant.ScaleArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "ChainArmourS":
                if (Globals.shopMerchant.ChainArmour >= 1)
                {
                    Globals.shopMerchant.ChainArmour -= 1;
                    Globals.shopMerchant.Gold += 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    chainAText.text = Globals.shopMerchant.ChainArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "NasalHelmetS":
                if (Globals.shopMerchant.NesalHelmet >= 1)
                {
                    Globals.shopMerchant.NesalHelmet -= 1;
                    Globals.shopMerchant.Gold += 30;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    nesalHText.text = Globals.shopMerchant.NesalHelmet.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "AvaintailS":
                if (Globals.shopMerchant.Aventail >= 1)
                {
                    Globals.shopMerchant.Aventail -= 1;
                    Globals.shopMerchant.Gold += 40;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    avainTailText.text = Globals.shopMerchant.Aventail.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MailCoifS":
                if (Globals.shopMerchant.MailCoif >= 1)
                {
                    Globals.shopMerchant.MailCoif -= 1;
                    Globals.shopMerchant.Gold += 60;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    mailCoifText.text = Globals.shopMerchant.MailCoif.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "DoubleHeadedAxeS":
                if (Globals.shopMerchant.DoubleHeadedAxe >= 1)
                {
                    Globals.shopMerchant.DoubleHeadedAxe -= 1;
                    Globals.shopMerchant.Gold += 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    doubleHeadedAxeText.text = Globals.shopMerchant.DoubleHeadedAxe.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "FlairS":
                if (Globals.shopMerchant.Flair >= 1)
                {
                    Globals.shopMerchant.Flair -= 1;
                    Globals.shopMerchant.Gold += 40;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    flairText.text = Globals.shopMerchant.Flair.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MaulS":
                if (Globals.shopMerchant.Maul >= 1)
                {
                    Globals.shopMerchant.Maul -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    maulText.text = Globals.shopMerchant.Maul.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "CompositeBowS":
                if (Globals.shopMerchant.CompositeBow >= 1)
                {
                    Globals.shopMerchant.CompositeBow -= 1;
                    Globals.shopMerchant.Gold += 60;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    compositeBowText.text = Globals.shopMerchant.CompositeBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "CrossBowS":
                if (Globals.shopMerchant.CrossBow >= 1)
                {
                    Globals.shopMerchant.CrossBow -= 1;
                    Globals.shopMerchant.Gold += 100;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    crossBowText.text = Globals.shopMerchant.CrossBow.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "WoodenKiteS":
                if (Globals.shopMerchant.WoodenKiteShield >= 1)
                {
                    Globals.shopMerchant.WoodenKiteShield -= 1;
                    Globals.shopMerchant.Gold += 20;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    wwodenKiteText.text = Globals.shopMerchant.WoodenKiteShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "WoodenTowerS":
                if (Globals.shopMerchant.Dragger >= 1)
                {
                    Globals.shopMerchant.Dragger -= 1;
                    Globals.shopMerchant.Gold += 40;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    woodenTowerText.text = Globals.shopMerchant.WoodenTowerShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MetalKiteS":
                if (Globals.shopMerchant.MetalKiteShield >= 1)
                {
                    Globals.shopMerchant.MetalKiteShield -= 1;
                    Globals.shopMerchant.Gold += 40;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    metalKiteText.text = Globals.shopMerchant.MetalKiteShield.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "BreastPlateArmourS":
                if (Globals.shopMerchant.BreastPlateArmour >= 1)
                {
                    Globals.shopMerchant.BreastPlateArmour -= 1;
                    Globals.shopMerchant.Gold += 120;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    breastPlateText.text = Globals.shopMerchant.BreastPlateArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "RingMailArmourS":
                if (Globals.shopMerchant.RingMailArmour >= 1)
                {
                    Globals.shopMerchant.RingMailArmour -= 1;
                    Globals.shopMerchant.Gold += 140;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    ringmailText.text = Globals.shopMerchant.RingMailArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "SplintmailArmourS":
                if (Globals.shopMerchant.SplintmailArmor >= 1)
                {
                    Globals.shopMerchant.SplintmailArmor -= 1;
                    Globals.shopMerchant.Gold += 160;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    splintMailText.text = Globals.shopMerchant.SplintmailArmor.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "BandedMailArmourS":
                if (Globals.shopMerchant.BandedMailArmour >= 1)
                {
                    Globals.shopMerchant.BandedMailArmour -= 1;
                    Globals.shopMerchant.Gold += 200;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    bandedMailText.text = Globals.shopMerchant.BandedMailArmour.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "AleS":
                if (Globals.shopMerchant.Ale >= 1)
                {
                    Globals.shopMerchant.Ale -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    AleText.text = Globals.shopMerchant.Ale.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "CurePotionS":
                if (Globals.shopMerchant.CurePotion >= 1)
                {
                    Globals.shopMerchant.CurePotion -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    cureText.text = Globals.shopMerchant.CurePotion.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "FoodS":
                if (Globals.shopMerchant.Food >= 1)
                {
                    Globals.shopMerchant.Food -= 1;
                    Globals.shopMerchant.Gold += 4;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    foodText.text = Globals.shopMerchant.Food.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "MeatS":
                if (Globals.shopMerchant.Meat >= 1)
                {
                    Globals.shopMerchant.Meat -= 1;
                    Globals.shopMerchant.Gold += 4;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    meatText.text = Globals.shopMerchant.Meat.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "HealPotionS":
                if (Globals.shopMerchant.HealPotion >= 1)
                {
                    Globals.shopMerchant.HealPotion -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    healText.text = Globals.shopMerchant.HealPotion.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "RumS":
                if (Globals.shopMerchant.Rum >= 1)
                {
                    Globals.shopMerchant.Rum -= 1;
                    Globals.shopMerchant.Gold += 10;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    rumText.text = Globals.shopMerchant.Rum.ToString();
                    isUpdatingScore = true;
                    StartCoroutine(increaseSize());
                }
                else
                {
                    NoGold.SetActive(true);
                    noItemText.GetComponent<Text>().text = "Item not available";
                }
                break;
            case "Sell":
                MerchantButtonSetting(true, false, false, true);
                ReadDB();
                foreach(var v in BuyButtons)
                {
                    v.SetActive(false);
                }
                foreach(var v in sellButtons)
                {
                    v.SetActive(true);
                }
                break;
            case "Buy":
                MerchantButtonSetting(false, true, true, false);
                ReadDB();
                foreach (var v in BuyButtons)
                {
                    v.SetActive(true);
                }
                foreach (var v in sellButtons)
                {
                    v.SetActive(false);
                }
                break;

        }
    }
    void PurchasingItems(int goldDeduct)
    {

    }
    IEnumerator increaseSize()
    {
        for (float i = goldText.fontSize; i <= 65; i += 1f)
        {
            goldText.fontSize = (int)i;
            yield return new WaitForSeconds(0.01f);
        }
        for (float i = goldText.fontSize; i >= 40; i -= 1f)
        {
            goldText.fontSize = (int)i;
            yield return new WaitForSeconds(0.01f); 
        }

    }
    private void Update()
    {
        if (isUpdatingScore)
            ShowScoreEffect();
    }
    void ShowScoreEffect()
    {
        int currentValue = int.Parse(goldText.text);
        goldText.text = ((int)Mathf.SmoothDamp(currentValue, Globals.shopMerchant.Gold, ref tempScore, 1)).ToString();
        if ((Time.time - timeStarted) >= 1)
        {
            goldText.text = Globals.shopMerchant.Gold.ToString();
            isUpdatingScore = false;
        }
    }
    void ReadDB()
    {
        if(swordText!=null)
           swordText.text = Globals.shopMerchant.ShortSword.ToString();
        if(draggerText!=null)
          draggerText.text = Globals.shopMerchant.Dragger.ToString();
        if(shortAxeText!=null)
          shortAxeText.text = Globals.shopMerchant.ShortAxe.ToString();
        if(wwodenKiteText!=null)
          wwodenKiteText.text = Globals.shopMerchant.WoodenKiteShield.ToString();
        if(clubText!=null)
          clubText.text = Globals.shopMerchant.Club.ToString();
        if(longBowText!=null)
          longBowText.text = Globals.shopMerchant.LongBow.ToString();
        if(shortBowText!=null)
          shortBowText.text = Globals.shopMerchant.ShortBow.ToString();
        if(longSwordText!=null)
           longSwordText.text = Globals.shopMerchant.LongSword.ToString();
        if(maceText!=null)
           maceText.text = Globals.shopMerchant.Mace.ToString();
        if(warHammerText!=null)
          warHammerText.text = Globals.shopMerchant.Warhammer.ToString();
        if(spearText!=null)
          spearText.text = Globals.shopMerchant.Spear.ToString();
        if(longAxeText!=null)
          longAxeText.text = Globals.shopMerchant.LongAxe.ToString();
        if(doubleHeadedAxeText!=null)
          doubleHeadedAxeText.text = Globals.shopMerchant.DoubleHeadedAxe.ToString();
        if(flairText!=null) 
          flairText.text = Globals.shopMerchant.Flair.ToString();
        if(maulText!=null)
          maulText.text = Globals.shopMerchant.Maul.ToString();
        if(compositeBowText!=null)
          compositeBowText.text = Globals.shopMerchant.CompositeBow.ToString();
        if(crossBowText!=null)
          crossBowText.text = Globals.shopMerchant.CrossBow.ToString();
        if(woodenBText!=null)
          woodenBText.text = Globals.shopMerchant.WoodenBuckler.ToString();
        if (woodenSText != null)
            woodenSText.text = Globals.shopMerchant.WoodenSmallRounded.ToString();
        if (metalBText!=null)
          metalBText.text = Globals.shopMerchant.MetalBuckler.ToString();
        if(metalSText!=null)
          metalSText.text = Globals.shopMerchant.MetalSmallRounded.ToString();
        if(woodenMText!=null)
          woodenMText.text = Globals.shopMerchant.WoodenMediumShield.ToString();
        if(metalMText!=null)
          metalMText.text = Globals.shopMerchant.MetalMediumShield.ToString();
        if(woodenTowerText!=null)
          woodenTowerText.text = Globals.shopMerchant.WoodenTowerShield.ToString();
        if(metalKiteText!=null)
          metalKiteText.text = Globals.shopMerchant.MetalKiteShield.ToString();
        if(clothAText!=null)
          clothAText.text = Globals.shopMerchant.ClothArmour.ToString();
        if(paddedAText!=null)
          paddedAText.text = Globals.shopMerchant.PaddedArmour.ToString();
        if(leatherCapText!=null)
          leatherCapText.text = Globals.shopMerchant.LeatherCap.ToString();
        if(hideAText!=null)
          hideAText.text = Globals.shopMerchant.HideArmour.ToString();
        if(leatherAText!=null)
          leatherAText.text = Globals.shopMerchant.LeatherArmour.ToString();
        if(kettleHatText!=null)
           kettleHatText.text = Globals.shopMerchant.KettleHat.ToString();
        if(brigadineAText!=null)
          brigadineAText.text = Globals.shopMerchant.BrigadineArmor.ToString();
        if(scaleAText!=null)
          scaleAText.text = Globals.shopMerchant.ScaleArmour.ToString();
        if(chainAText!=null)
          chainAText.text = Globals.shopMerchant.ChainArmour.ToString();
        if(nesalHText!=null)
          nesalHText.text = Globals.shopMerchant.NesalHelmet.ToString();
        if(avainTailText!=null)
           avainTailText.text = Globals.shopMerchant.Aventail.ToString();
        if(mailCoifText!=null)
          mailCoifText.text = Globals.shopMerchant.MailCoif.ToString();
        if(breastPlateText!=null)
          breastPlateText.text = Globals.shopMerchant.BreastPlateArmour.ToString();
        if(ringmailText!=null)
          ringmailText.text = Globals.shopMerchant.RingMailArmour.ToString();
        if(splintMailText!=null)
          splintMailText.text = Globals.shopMerchant.SplintmailArmor.ToString();
        if(bandedMailText!=null)
          bandedMailText.text = Globals.shopMerchant.BandedMailArmour.ToString();
        if(AleText!=null)
          AleText.text = Globals.shopMerchant.Ale.ToString();
        if(cureText!=null)
          cureText.text = Globals.shopMerchant.CurePotion.ToString();
        if(healText!=null)
          healText.text = Globals.shopMerchant.HealPotion.ToString();
        if(foodText!=null)
          foodText.text = Globals.shopMerchant.Food.ToString();
        if(meatText!=null)
          meatText.text = Globals.shopMerchant.Meat.ToString();
        if(rumText!=null)
          rumText.text = Globals.shopMerchant.Rum.ToString();
    }

    void MerchantShopSetting(bool w, bool s, bool a, bool i)
    {
        weaponPanel.SetActive(w);
        shieldPanel.SetActive(s);
        armourPanel.SetActive(a);
        itemPanel.SetActive(i);
    }
  
}
