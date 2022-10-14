using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HelthHolde{

    [CreateAssetMenu(fileName = "PlayerConfiguration.asset", menuName = "Helth/Player/PlayerItem", order = 1)]
    public class PlayerConfiguration : ScriptableObject
	{
        [HideInInspector]
        public GameObject playerPrefab;
        public int damageValue = 0;
        public string characterName;
        public int shield = 0;
        public float health = 0;
        public int armour = 0;
        public int helmet = 0;
        public int defence = 0;
        public int weapon = 0;
        public int level = 1;
        public int xpPoints = 0;
        public int HealthFactor;
        public PlayerType Type = PlayerType.Player;
        public Globals.Gender gender = Globals.Gender.Male;
        public int weaponId = 0;
        public int defenceId = 0;
        public Sprite icon;
        public Sprite weaponIcon;
        public string stringId;
        public int weapon1 = 0;
        public int weapon2 = 0;
        public int weapon3 = 0;
        public int weapon4 = 0;
        public int weapon5 = 0;
        public int weapon6 = 0;
        public enum PlayerType { Player, AI,Companion}
        public PlayerItem playerItemPrefab;
    }
}