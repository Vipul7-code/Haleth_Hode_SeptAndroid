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



    [CreateAssetMenu(fileName = "PlayerItemLibrary.asset", menuName = "Haleth/Player/PlayerItemLibray", order = 0)]
    public class PlayerItemLibrary : ScriptableObject
    {
        public List<PlayerConfiguration> PlayerCharacterLibrary;
    }
}