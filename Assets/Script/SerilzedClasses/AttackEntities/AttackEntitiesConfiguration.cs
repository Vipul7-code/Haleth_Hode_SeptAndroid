using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HelthHolde{

    [CreateAssetMenu(fileName = "AttackEntitiesConfiguration.asset", menuName = "Helth/Entities/AttackEntitiesItem", order = 1)]
    public class AttackEntitiesConfiguration : ScriptableObject
	{
        
        public int ID = 0;
        public string entityName;
        public int BaseWeaponAttack = 0;
        public int DefenceValue = 0;
        public int BaseWeaponDamage = 0;
        public Sprite weaponIcon;
    }
}