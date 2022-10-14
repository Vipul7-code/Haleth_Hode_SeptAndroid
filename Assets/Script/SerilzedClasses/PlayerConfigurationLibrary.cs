using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HelthHolde
{
    /// <summary>
    /// Scriptable object for Level configuration
    /// </summary>
    /// 



    [CreateAssetMenu(fileName = "PlayerConfigurationLibrary.asset", menuName = "Helth/Player/PlayerConfigurationLibray", order = 0)]
    public class PlayerConfigurationLibrary : ScriptableObject
    {
        public PlayerItemLibrary playerConfigurations;
        //public PlayerItemLibrary companionConfigurations;
        public PlayerItemLibrary enemyConfigurations;
        public PlayerItemLibrary soldierCampsiteConfigurations;
        public PlayerItemLibrary wagonCaravanConfigurations;
        public PlayerItemLibrary secondSoldierConfigurations;
        public PlayerItemLibrary atwaterConfigurations;
        public PlayerItemLibrary monestryCellarConfigurations;
        public PlayerItemLibrary monestryTuckerConfigurations;
        public PlayerItemLibrary motteyAndBailey1Configurations;
        public PlayerItemLibrary motteyAndBailey2Configurations;
        public PlayerItemLibrary barghestConfigurations;
        public PlayerItemLibrary deathWightConfigurations;
        public PlayerItemLibrary brigandConfigurations;

        public PlayerItemLibrary sargentConfiguration;
        public PlayerItemLibrary captainConfiguration;
        //public PlayerItemLibrary NPCConfigurations;
        public NPCItemLibrary npcConfigurations;

        //public PlayerConfiguration[] MyCharacter;
        public PlayerItemLibrary CompanionAI;
        //public EnemyInBattle[] battleEnemy;

        //public NpcConfiguration[] npcs;
        
    }
}