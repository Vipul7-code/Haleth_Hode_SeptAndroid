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



    [CreateAssetMenu(fileName = "AttackEntitiesLibrary.asset", menuName = "Helth/Entities/AttackEntitiesLibrary", order = 1)]
    public class AttackEntitiesLibrary : ScriptableObject
    {
        public  List<AttackEntitiesConfiguration> AttackEntityLibrary;

    }
}