using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelthHolde
{

    [CreateAssetMenu(fileName = "NpcConfiguration.asset", menuName = "Helth/Player/NpcItem", order = 4)]
    public class NpcConfiguration : ScriptableObject
    {

        public Moveable npcPrefab;
        public Vector3 spawnPoint;
        public int HealthFactor;
        public int damage = 0;
        public AIType Type = AIType.Friend;

        public enum AIType { Friend, Enemy }
    }
}
