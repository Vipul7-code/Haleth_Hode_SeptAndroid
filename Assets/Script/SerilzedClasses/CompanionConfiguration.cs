using System.Collections.Generic;
using UnityEngine;

namespace HelthHolde{

    [CreateAssetMenu(fileName = "CompanionConfiguration.asset", menuName = "Helth/Player/CompanionItem", order = 2)]
    public class CompanionConfiguration : ScriptableObject
	{
        
        public Moveable companionPrefab;
        public Vector3 spawnPoint;
        public int HealthFactor;
        public int damage = 0;
        public AIType Type = AIType.Friend;
        public enum AIType { Friend, Enemy}
    }
}