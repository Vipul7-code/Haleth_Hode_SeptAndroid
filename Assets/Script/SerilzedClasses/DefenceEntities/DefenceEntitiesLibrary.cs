using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HelthHolde
{
  



    [CreateAssetMenu(fileName = "DefenceEntitiesLibrary.asset", menuName = "Helth/Entities/DefenceEntitiesLibrary", order = 2)]
    public class DefenceEntitiesLibrary : ScriptableObject
    {
        public  List<DefenceEntitiesConfiguration> DefenceEntityLibrary;

    }
}