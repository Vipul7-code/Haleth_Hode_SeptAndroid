using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSetting : MonoBehaviour
{
    // Start is called before the first frame updat
    [SerializeField]
    GameObject[] armourChild;
    [SerializeField]
    GameObject[] helmetChild;
    [SerializeField]
    GameObject lockedLeather, lockedPadded, lockedGuard, lockedChainmail;
    [SerializeField]
    GameObject lockedLeatherH, lockedMetalH, lockedGuardH, lockedChainmailH;
    void Start()
    {
        if (Globals.inventoryHandler.isArmor)
            ArmourSettings();
        if (Globals.inventoryHandler.isHelmet)
            HelmetSetting();
    }

   void ArmourSettings()
    {
        Debug.Log("scene::" + Globals.activeScene);
        if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            armourChild[3].gameObject.SetActive(false);
        if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite || Globals.activeScene == Globals.CurrentScene.WagonCaravan || Globals.activeScene == Globals.CurrentScene.SecondSoldierCaravan)
            SettingOfLockedArmour(true, true, true, true);
        else if (Globals.activeScene == Globals.CurrentScene.Huntsville || Globals.activeScene == Globals.CurrentScene.AtwaterVillage || Globals.activeScene==Globals.CurrentScene.SacredPlace)
        {
            if (Globals.activeScene == Globals.CurrentScene.Huntsville)
            {
                if (Globals.secondVisit == 0 || Globals.secondVisit == 1)
                    SettingOfLockedArmour(true, true, false, true);
                else if (Globals.secondVisit == 2)
                    SettingOfLockedArmour(false, false, false, true);
            }
            else
                SettingOfLockedArmour(true, true, false, true);
        }
        else if(Globals.activeScene==Globals.CurrentScene.MonkCampsite || Globals.activeScene==Globals.CurrentScene.monastery)
            SettingOfLockedArmour(false, true, false, true);
        else if(Globals.activeScene==Globals.CurrentScene.MotteAndBaileyCastle || Globals.activeScene==Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon|| Globals.activeScene == Globals.CurrentScene.TheDeathWeight|| Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
            SettingOfLockedArmour(false, false, false, true);
        else if(Globals.activeScene==Globals.CurrentScene.TheBrigand|| Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon || Globals.activeScene == Globals.CurrentScene.HuntingtonVillage)
        {
            if(Globals.selectedInventoryCharacter != "PriestMale"|| Globals.selectedInventoryCharacter != "PriestFemale")
                SettingOfLockedArmour(false, false, false, false);
        }


    }
    void SettingOfLockedArmour(bool l,bool p,bool g,bool c)
    {
        lockedLeather.SetActive(l);
        lockedPadded.SetActive(p);
        lockedGuard.SetActive(g);
        lockedChainmail.SetActive(c);
    }
    void SettingOfLockedHelmet(bool l, bool p, bool g, bool c)
    {
        lockedLeatherH.SetActive(l);
        lockedMetalH.SetActive(p);
        lockedGuardH.SetActive(g);
        lockedChainmailH.SetActive(c);
    }
    void HelmetSetting()
    {
        if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
            helmetChild[3].gameObject.SetActive(false);
        if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite || Globals.activeScene == Globals.CurrentScene.WagonCaravan || Globals.activeScene == Globals.CurrentScene.SecondSoldierCaravan)
            SettingOfLockedHelmet(true, true, true, true);
        else if (Globals.activeScene == Globals.CurrentScene.Huntsville || Globals.activeScene == Globals.CurrentScene.AtwaterVillage || Globals.activeScene == Globals.CurrentScene.SacredPlace)
        {
            if (Globals.activeScene == Globals.CurrentScene.Huntsville)
            {
                if (Globals.secondVisit == 0 || Globals.secondVisit == 1)
                    SettingOfLockedHelmet(true, true, false, true);
                else if (Globals.secondVisit == 2)
                    SettingOfLockedHelmet(false, false, false, true);
            }
            else
                SettingOfLockedHelmet(true, true, false, true);
        }
        else if (Globals.activeScene == Globals.CurrentScene.MonkCampsite || Globals.activeScene == Globals.CurrentScene.monastery)
            SettingOfLockedHelmet(false, true, false, true);
        else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle || Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
            SettingOfLockedHelmet(false, false, false, true);
        else if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon || Globals.activeScene == Globals.CurrentScene.HuntingtonVillage)
        {
            if (Globals.selectedInventoryCharacter != "PriestMale" || Globals.selectedInventoryCharacter != "PriestFemale")
                SettingOfLockedHelmet(false, false, false, false);
        }
    }
}
