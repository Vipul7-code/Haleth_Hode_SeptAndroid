using HelthHolde;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

/// <summary>
/// Element describing a level
/// </summary>
[CreateAssetMenu(fileName = "CharacterItem", menuName = "Helth / Character / CharacterItem", order = 0)]
public class CharacterItem : ScriptableObject
{
		/// <summary>
		/// The id - used in persistence
		/// </summary>
        
        [HideInInspector]
		public int id;

    /// <summary>
    /// The human readable character name
    /// </summary>
    public string characterName;

    public Sprite characterImage;

    

    public Gender gender;

		
		
}
