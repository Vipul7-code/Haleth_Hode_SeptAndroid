using HelthHolde;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

/// <summary>
/// Element describing a level
/// </summary>
[Serializable]
	public class InterationItem
    {
		/// <summary>
		/// The id - used in persistence
		/// </summary>
	//	public int id;

    /// <summary>
    /// The human readable Dialog List
    /// </summary>
       public List<DialogDataItem> dialogList;
   
   
		
}
