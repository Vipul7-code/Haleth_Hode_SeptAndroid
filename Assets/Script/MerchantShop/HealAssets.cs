using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealAssets : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button food, meat, rum, ale, healPotion, curePotion;
    DatabaseManager db;
    void Start()
    {
        db = FindObjectOfType<DatabaseManager>();
        //SettingOfHealAsstes();
    }
    void Update()
    {
        SettingOfHealAsstes();
    }
    void SettingOfHealAsstes()
    {
        if (Globals.shopMerchant.Ale >= 1)
            ale.interactable = true;
        else
            ale.interactable = false;
        if (Globals.shopMerchant.HealPotion >= 1)
            healPotion.interactable = true;
        else
            healPotion.interactable = false;
        if (Globals.shopMerchant.CurePotion >= 1)
            curePotion.interactable = true;
        else
            curePotion.interactable = false;
        if (Globals.shopMerchant.Meat >= 1)
            meat.interactable = true;
        else
            meat.interactable = false;
        if (Globals.shopMerchant.Food >= 1)
            food.interactable = true;
        else
            food.interactable = false;
        if (Globals.shopMerchant.Rum >= 1)
            rum.interactable = true;
        else
            rum.interactable = false;
    }
    public void ClickOnButton(string btn_name)
    {
        switch(btn_name)
        {
            case "Food":
                Globals.battleManager.HealFunctionality();
                Globals.shopMerchant.Food -= 1;
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                break;
            case "Meat":
                Globals.battleManager.HealFunctionality();
                Globals.shopMerchant.Meat -= 1;
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                break;
            case "Rum":
                Globals.battleManager.HealFunctionality();
                Globals.shopMerchant.Rum -= 1;
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                break;
            case "Ale":
                Globals.battleManager.HealFunctionality();
                Globals.shopMerchant.Ale -= 1;
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                break;
            case "HealPotion":
                Globals.battleManager.HealFunctionality();
                Globals.shopMerchant.HealPotion -= 1;
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                break;
            case "CurePotion":
                Globals.usedCurePotion = true;
                Globals.battleManager.HealFunctionality();
                Globals.shopMerchant.CurePotion -= 1;
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                break;
        }
    }

    // Update is called once per frame
    
}
