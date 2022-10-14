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



    [CreateAssetMenu(fileName = "ObjectiveLibrary.asset", menuName = "Haleth/Objective/ObjectiveLibrary", order = 0)]
    public class ObjectiveLibrary : ScriptableObject
    {
        public List<ObjectiveConfiguration> ObjectiveConfigurationLibrary;
    }
}