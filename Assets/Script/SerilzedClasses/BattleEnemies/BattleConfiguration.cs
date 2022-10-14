using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelthHolde
{
    [CreateAssetMenu(fileName = "BattleLibrary.asset", menuName = "Haleth/Battle/BattleLibrary", order = 1)]
    public class BattleConfiguration : ScriptableObject
    {
        public string partName;
        public List<GameObject> enemies = new List<GameObject>();
        public List<GameObject> companion = new List<GameObject>();
    }
}
