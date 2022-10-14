using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelthHolde
{

    [CreateAssetMenu(fileName = "EnemyInBattle.asset", menuName = "Helth/Player/EnemyInBattle", order = 6)]
    public class EnemyInBattle : ScriptableObject
    {

        public Moveable enemyPrefab;
        public Vector3 spawnPoint;

        public Sprite missileIcon;

        public int damage = 0;
       // public float health = 0;
        public int range = 0;


        public AIType Type = AIType.Friend;

        public enum AIType { Friend, Enemy }
    }
}
