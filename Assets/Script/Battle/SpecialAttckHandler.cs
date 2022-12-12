using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttckHandler : MonoBehaviour
{
    public Button criticalStrike, stealthAttack, lightningBolt, bandage, deadEyeShot, normalAttack;
    // Start is called before the first frame update
    void Start()
    {
        Globals.specialAttack = this;
        SettingOfButtons();
    }

   public void SettingOfButtons()
    {
        if (Globals.afterPromotion)
        {
            GameObject battlePlayer = Globals.battleManager.tempList[Globals.battleManager.OtherIndex].gameObject;
            if (battlePlayer.name == "WarriorMale(Clone)" || battlePlayer.name == "WarriorFemale(Clone)")
            {
                if (battlePlayer.GetComponent<EntityGroup>().criticalStrike == 0  && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0)
                    EnableButtons(true, false, true, false, false, true);
                else if(battlePlayer.GetComponent<EntityGroup>().criticalStrike == 1 && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0)
                    EnableButtons(false, false, true, false, false, true);
                else if (battlePlayer.GetComponent<EntityGroup>().criticalStrike == 0 && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 1)
                    EnableButtons(true, false, false, false, false, true);
                else
                    EnableButtons(false, false, false, false, false, true);

                if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight && battlePlayer.tag == "Player" && Globals.isBarghest && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0 && Globals.battleManager.waveCount == 2)
                {
                    Debug.Log("here lightning bolt true ya false........... wave count TheDeathWeight " + Globals.battleManager.waveCount);


                    if (battlePlayer.GetComponent<EntityGroup>().lighteningBoltDeathWight == 1)
                    {
                        Debug.Log("lightning bolt true");
                        lightningBolt.interactable = true;
                    }
                    else if(battlePlayer.GetComponent<EntityGroup>().lighteningBoltDeathWight == 0)
                    {
                        Debug.Log("lightning bolt false");
                        lightningBolt.interactable = false;
                    }
               
                }
            }
            else if(battlePlayer.name == "JohnCompanion(Clone)")
            {
                if (battlePlayer.GetComponent<EntityGroup>().criticalStrike == 0)
                    EnableButtons(true, false, false, false, false, true);
                else if (battlePlayer.GetComponent<EntityGroup>().criticalStrike == 1)
                    EnableButtons(false, false, false, false, false, true);
                else
                    EnableButtons(false, false, false, false, false, true);
            }
            else if (battlePlayer.name == "ArcherMale(Clone)" || battlePlayer.name == "ArcherFemale(Clone)")
            {
                if (battlePlayer.GetComponent<EntityGroup>().deadEye == 0  && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0)
                    EnableButtons(false, true, true, false, false, true);
                else if (battlePlayer.GetComponent<EntityGroup>().deadEye == 0  && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 1)
                    EnableButtons(false, true, false, false, false, true);
                else if (battlePlayer.GetComponent<EntityGroup>().deadEye == 1 && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0)
                    EnableButtons(false, false, true, false, false, true);
                else
                    EnableButtons(false, false, false, false, false, true);


                if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight && battlePlayer.tag == "Player" && Globals.isBarghest && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0 && Globals.battleManager.waveCount == 2)
                {
                    Debug.Log("here lightning bolt true ya false........... wave count TheDeathWeight " + Globals.battleManager.waveCount);


                    if (battlePlayer.GetComponent<EntityGroup>().lighteningBoltDeathWight == 1)
                    {
                        Debug.Log("lightning bolt true");
                        lightningBolt.interactable = true;
                    }
                    else if (battlePlayer.GetComponent<EntityGroup>().lighteningBoltDeathWight == 0)
                    {
                        Debug.Log("lightning bolt false");
                        lightningBolt.interactable = false;
                    }

                }
            }
            else if (battlePlayer.name == "PriestMale(Clone)" || battlePlayer.name == "PriestFemale(Clone)")
            {
                if(battlePlayer.GetComponent<EntityGroup>().bandageProperty==0 && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0)
                     EnableButtons(false, false, true, true, false, true);
                else if (battlePlayer.GetComponent<EntityGroup>().bandageProperty == 1 &&  battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0)
                    EnableButtons(false, false, true, false, false, true);
                else if (battlePlayer.GetComponent<EntityGroup>().bandageProperty == 0 && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 1)
                    EnableButtons(false, false, false, true, false, true);
                else
                    EnableButtons(false, false, false, false, false, true);


                if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight && battlePlayer.tag == "Player" && Globals.isBarghest && battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0 && Globals.battleManager.waveCount == 2)
                {
                    Debug.Log("here lightning bolt true ya false........... wave count TheDeathWeight " + Globals.battleManager.waveCount);

                    if (battlePlayer.GetComponent<EntityGroup>().lighteningBoltDeathWight == 1)
                    {
                        Debug.Log("lightning bolt true");
                        lightningBolt.interactable = true;
                    }
                    else if (battlePlayer.GetComponent<EntityGroup>().lighteningBoltDeathWight == 0)
                    {
                        Debug.Log("lightning bolt false");
                        lightningBolt.interactable = false;
                    }

                }
            }
            else if(battlePlayer.name == "Tucker(Clone)")
            {
             //   Debug.Log("here::" + battlePlayer.GetComponent<EntityGroup>().bandageProperty);
                if (battlePlayer.GetComponent<EntityGroup>().bandageProperty == 0)
                    EnableButtons(false, false, false, true, false, true);
                else if (battlePlayer.GetComponent<EntityGroup>().bandageProperty == 1)
                    EnableButtons(false, false, false, false, false, true);
                else
                    EnableButtons(false, false, false, false, false, true);
            }
            else if (battlePlayer.name == "Marium(Clone)")
            {
                if (battlePlayer.GetComponent<EntityGroup>().deadEye == 0)
                    EnableButtons(false, false, false, false, true, true);
                else if (battlePlayer.GetComponent<EntityGroup>().deadEye == 1)
                    EnableButtons(false, false, false, false, false, true);
                else
                    EnableButtons(false, false, false, false, false, true);
            }
            if(battlePlayer.GetComponent<EntityGroup>().noAttack==1)
                EnableButtons(false, false, false, false, false, false);
        }
        else
        {
            GameObject battlePlayer = Globals.battleManager.tempList[Globals.battleManager.OtherIndex].gameObject;
            if (!Globals.isLightening)
                EnableButtons(false, false, false, false, false, true);
            else if(battlePlayer.tag=="Player")
            {
                if (battlePlayer.GetComponent<EntityGroup>().lighteningBolt == 0 && Globals.shopMerchant.MagicSword==1)
                    EnableButtons(false, false, true, false, false, true);
                else
                    EnableButtons(false, false, false, false, false, true);
            }
            else
                EnableButtons(false, false, false, false, false, true);
        }
    }

    void EnableButtons(bool critical,bool stealth,bool light,bool band,bool deadEye,bool normal)
    {
        criticalStrike.interactable = critical;
        stealthAttack.interactable = stealth;
        lightningBolt.interactable = light;
        bandage.interactable = band;
        deadEyeShot.interactable = deadEye;
        normalAttack.interactable = normal;
    }
}
