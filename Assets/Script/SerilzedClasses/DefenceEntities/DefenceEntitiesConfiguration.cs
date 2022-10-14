using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HelthHolde{

    [CreateAssetMenu(fileName = "DefenceEntitiesConfiguration.asset", menuName = "Helth/Entities/DefenceEntitiesItem", order = 3)]
    public class DefenceEntitiesConfiguration : ScriptableObject
	{
        
        public int ID = 0;
        public string entityName;
        public int defence = 0;
        public Sprite icon;
    }
}