using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HelthHolde{

    [CreateAssetMenu(fileName = "ObjectiveConfiguration.asset", menuName = "Haleth/Objective/ObjectiveConfiguration", order = 1)]
    public class ObjectiveConfiguration : ScriptableObject
	{
        
        public string SceneName;
        public Globals.Objective currentObjective;
        public List<Globals.Objective> nextObjective;

    }
}