using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuestHandler : MonoBehaviour
{
   public Text questText;
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        Globals.questHandler = this;
        currentScene = SceneManager.GetActiveScene();
    }

  public  void ShowQuest()
    {
        if (Globals.completeIntro)
        {
            if (Globals.conversationCount == 1 || Globals.conversationCount == 2)
                questText.text ="Well Dengeon"+"\n"+ "Find the Treasure";
            else if (Globals.conversationCount == 3)
                questText.text ="Huntsville Church"+"\n"+ "Talk to the Priest";
            else if (Globals.conversationCount == 4)
                questText.text = "Huntsville" + "\n"+ "Go to the Town Square";
            else if (Globals.conversationCount == 5)
                questText.text = "Huntsville Mayor's House"+"\n" + "Talk to the Mayor";
            else if(Globals.conversationCount==6)
                questText.text = "Huntsville Merchant Shop"+"\n"+"Talk to the Merchant";
            else if(Globals.conversationCount==7)
                questText.text = "Huntsville Inn" + "\n"+ "Talk to Innkeeper"+"\n"+"Talk to the Mercenary";
            else if(Globals.conversationCount>=8)
                questText.text = "Save at  Bed in Inn. Or exit Town";
        }
        else
        {
            Debug.Log("active scene" + Globals.activeScene);
            if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite)
                questText.text = "Go to the Soldier Campsite";
            else if (Globals.activeScene == Globals.CurrentScene.WagonCaravan)
                questText.text = "Go to the Wagon Caravan";
            else if (Globals.activeScene == Globals.CurrentScene.SecondSoldierCaravan)
                questText.text = "Go Second Soldier Campsite";
            else if (Globals.activeScene == Globals.CurrentScene.Huntsville)
            {
                if (currentScene.name == "Huntsville Chruch_int")
                    questText.text = "Pray at the Church (optional) to Save";
                else
                {
                    if (Globals.secondVisit == 0)
                    {
                        if (!Globals.isFirstCompleteStory)
                            questText.text = "Go to Huntsville Village Townsquare";
                        else
                            questText.text = "Pray at the Church (optional) to Save";
                    }
                    else if (Globals.secondVisit == 1)
                    {
                        //Debug.Log("back vill::" + Globals.backToVill + " complete::" + Globals.isChurchComplete);
                        if(Globals.backToVill || Globals.isChurchComplete)
                            questText.text = "Go to Campsite outside Monastary";
                        else
                           questText.text = "Go to Huntsville" + "\n" + "Speak to Huntsville Priest";
                    }
                    else if (Globals.secondVisit == 2)
                    {
                        if (!Globals.isChurchComplete)
                            questText.text = "Huntsville Village" + "\n" + "Speak to the Abbott";
                        else
                            questText.text = "Huntsville Village" + "\n" + "Speak to the Townsfolk";
                    }
                }

            }
            else if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage)
            {
                if (Globals.atWaterCount == 0)
                {
                    if (Globals.InnVisit == 0)
                        questText.text = "Go to Atwater Village" + "\n" + "Speak to Sargant-at-Arms";
                    else
                        questText.text = "Atwater Village" + "\n" + "Destroy Enemy Patrols and Supplies";

                }
                else
                {
                    if (Globals.InnVisit == 0)
                        questText.text = "Go to Atwater Village" + "\n" + "Speak to Sage";
                    else
                        questText.text = "Go to Sacred Place";
                }
            }
            else if (Globals.activeScene == Globals.CurrentScene.RandomAttack)
            {
                if(Globals.atWaterCount<6)
                  questText.text = "Atwater Village" + "\n" + "Destroy Enemy Patrols and Supplies";
                else
                    questText.text = "Motte and Bailey Castle" + "\n" + "Destroy Enemy Patrols and Supply Wagons";
            }
            else if (Globals.activeScene == Globals.CurrentScene.SacredPlace)
            {
                Debug.Log("scene name::" + currentScene.name);
                if (currentScene.name == "Sacred Place Exterior_New" || currentScene.name == "World Map")
                    questText.text = "Go to Sacred Place";
                else
                    questText.text = "Find the Treasure";
            }
            else if (Globals.activeScene == Globals.CurrentScene.MonkCampsite)
                questText.text = "Go to Campsite near Monastery";
            else if (Globals.activeScene == Globals.CurrentScene.monastery)
            {
                if (currentScene.name == "World Map")
                    questText.text = "Go to the Monastery";
                else if (currentScene.name == "Monastery_ext")
                    questText.text = "Enter through cellar";
            }
            else if (Globals.activeScene == Globals.CurrentScene.CellarTucker || Globals.activeScene==Globals.CurrentScene.CellarInt)
                questText.text = "Monastery" + "\n" + "Speak to Abbott";
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
                questText.text = "Motte & Bailey Castle" + "\n" + "Defeat Lord Alfred the Tax Collector";
            else if (Globals.activeScene == Globals.CurrentScene.BarghestVillage)
            {
                if (currentScene.name == "World Map" || currentScene.name == "Barghest Village")
                {
                    if (Globals.againVisit == 0)
                    {
                        if (!Globals.isShopDialog)
                        {
                            if (Globals.conversationCount == 0)
                            {
                                if (!Globals.secondVisitMon)
                                    questText.text = "Go to the Village in the North"+ "\n"+"Go to Merchant" /*"Barghest Village Go to Merchant" + "\n" + "Go to the Village in the South" + "\n" + "Go to the Village in the East"*/; //"\n" + "Go to the Village in the South" + "\n" + "Go to the Village in the East"
                                else
                                    questText.text = "Barghest Village" + "\n" + "Speak to Merchant";
                            }
                            else
                                questText.text = "Go to Merchant";
                        }
                        else
                            questText.text = "Defeat Barghest";
                    }
                    else
                        questText.text = "Barghest Village"+ "\n"+"Speak to Merchant";
                }
                else if (currentScene.name == "Barghest Lair-Dungeon")
                {
                    if (!Globals.secondVisitMon)
                    {
                        if (!Globals.isPart1Battle)
                            questText.text = "Barghest Lair" + "\n" + "Defeat Barghest";
                        else
                            questText.text = "Go to the Village in the East";
                    }
                    else
                        questText.text = "Go to the Village and Speak to Merchant";
                }
                else
                {
                    if (!Globals.secondVisitMon)
                    {
                        if (!Globals.isPart1Battle)
                            questText.text = "Defeat Barghest";
                        else
                            questText.text = "Go to the Village in the South";
                    }
                    else
                        questText.text = "Go to the Village and Speak to Merchant";
                }
            }
            else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight)
            {
                if (currentScene.name == "World Map" || currentScene.name == "Death Wight Village")
                {
                    if (Globals.againVisit==0)
                    {
                        if (!Globals.isFirstCompleteStory)
                        {
                            if (Globals.conversationCount == 0)
                                questText.text = "Go to Village in the East" + "\n" + "Speak to Old Man";
                            else
                                questText.text = "Death Wight Village" + "\n" + "Speak to Old Man";
                        }
                        else
                            questText.text = "Death Weight Liar" + "\n" + "Defeat Death Wight";
                    }
                    else
                        questText.text = "Death Wight Village" + "\n" + "Speak to Old Man";
                }
                else
                {
                    if (!Globals.secondVisitMon)
                        questText.text = "Death Weight Liar" + "\n" + "Defeat Death Wight";
                    else
                        questText.text = "Go to Village and Speak to Old Man";
                }
            }
            else if (Globals.activeScene == Globals.CurrentScene.TheBrigand)
            {
                if (!Globals.secondVisitMon)
                {
                    if (Globals.againVisit == 0)
                    {
                        if (!Globals.isPart1Battle)
                        {
                            if (Globals.conversationCount == 0)
                                questText.text = "Go to the Village in the South" + "\n" + "Speak with Villagers";
                            else
                                questText.text = "Bandit Village" + "\n" + "Speak with Villagers";
                        }
                        else
                            questText.text = "Find the Brigand Lair, Defeat Brigand Chief";
                    }
                    else
                        questText.text = "Bandit Village" + "\n" + "Speak with Rescued hostage";
                }
                else
                    questText.text = "Bandit Village" + "\n" + "Speak with Rescued hostage";
            }
            else if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
            {
                if (!Globals.secondVisitMon)
                    questText.text = "Bandit Liar" + "\n" + "Defeat Rochester";
                else
                    questText.text = "Bandit Village" + "\n" + "Speak with Rescued hostage";
            }
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonVillage)
            {
                if (currentScene.name == "World Map")
                    questText.text = "Huntington Village" + "\n" + "Enter Castle" + "\n" + "Caved-in Tunnel" + "\n" + "Drunken Guard Alley way" + "\n" + "Grapping Hook";
                else if (currentScene.name == "Huntington Town_Alley Scenes")
                {
                    if (Globals.grappingHook && !Globals.drunkenGuy && !Globals.isExploringTunnel)
                        questText.text = "Huntington Village" + "\n" + "Caved -in Tunnel" + "\n" + "Drunken Guard Alley way";
                    else if (Globals.drunkenGuy && !Globals.grappingHook && !Globals.isExploringTunnel)
                        questText.text = "Huntington Village" + "\n" + "Caved -in Tunnel" + "\n" + "Grapping Hook";
                    else if(Globals.isExploringTunnel && !Globals.drunkenGuy && !Globals.grappingHook)
                        questText.text = "Huntington Village" + "\n" + "Drunken Guard Alley way" + "\n" + "Grapping Hook";
                    else if (Globals.grappingHook && Globals.drunkenGuy && !Globals.isExploringTunnel)
                        questText.text = "Huntington Village" + "\n" + "Caved -in Tunnel";
                    else if(Globals.grappingHook && Globals.isExploringTunnel && !Globals.drunkenGuy)
                        questText.text = "Huntington Village" + "\n" + "Drunken Guard Alley way";
                    else if(Globals.isExploringTunnel && Globals.drunkenGuy && !Globals.grappingHook)
                        questText.text = "Huntington Village" + "\n" + "Grapping Hook";
                    else if(!Globals.grappingHook && !Globals.drunkenGuy && !Globals.isExploringTunnel)
                        questText.text = "Huntington Village" + "\n" + "Enter Castle" + "\n" + "Caved-in Tunnel" + "\n" + "Drunken Guard Alley way" + "\n" + "Grapping Hook";
                    else
                        questText.text = "Go to Huntington Inn and Find Secret Tunnel Entrance";

                }
                else if (currentScene.name == "Huntington_Inn_1stFloor")
                {
                    Debug.Log("complete effort::" + Globals.completeEfforts);
                    if (!Globals.completeEfforts)
                        questText.text = "Regroup at the Huntington Inn" + "\n" + "Talk to the Innkeeper";
                    else
                        questText.text = "Huntington Inn" + "\n" + "Enter Huntington Castle Secret Tunnel";
                }
            }
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonCastle)
            {
                if (!Globals.isPart1Battle)
                    questText.text = "Huntington Castle" + "\n" + "Explore the Castle";
                else
                    questText.text = "Huntington Castle" + "\n" + "Enter the Throne Room";
            }

        }


    }
}
