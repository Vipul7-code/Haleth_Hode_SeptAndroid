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



    [CreateAssetMenu(fileName = "BattleLibrary.asset", menuName = "Haleth/Battle/BattleLibrary", order = 0)]
    public class BattleLibrary : ScriptableObject
    {
        public List<BattleConfiguration> BattleConfigurationLibrary;
    }
}