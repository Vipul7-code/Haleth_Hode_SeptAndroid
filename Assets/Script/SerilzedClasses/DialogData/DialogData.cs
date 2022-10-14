using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Scriptable object for Level configuration
/// </summary>
[CreateAssetMenu(fileName = "DialogLibrary", menuName = "Helth / Dialog / DialogLibrary", order = 0)]
public class DialogData : ScriptableObject
    //, IList<InterationItem>,
    //                     IDictionary<int, InterationItem>,
    //                     ISerializationCallbackReceiver
{
    public InterationItem interationData;
    /// <summary>
    /// Cached dictionary of levels by their IDs
    /// </summary>
    IDictionary<int, InterationItem> interationDataDictionary;

    /// <summary>
    /// Gets the number of levels
    /// </summary>
    //public int Count
    //{
    //    get { return interationData.Length; }
    //}

    /// <summary>
    /// Level list is always read-only
    /// </summary>
    public bool IsReadOnly
    {
        get { return true; }
    }

    /// <summary>
    /// Gets a level by index
    /// </summary>
    //public InterationItem this
    //{
    //    get
    //    {
    //        return interationDataDictionary[i];
    //    }
    //}

    /// <summary>
    /// Gets a level by id
    /// </summary>
    /*public InterationItem this[int key]
    {
        get { return interationDataDictionary[key]; }
    }*/

    /// <summary>
    /// Gets a collection of all level keys
    /// </summary>
    public ICollection<int> Keys
    {
        get { return interationDataDictionary.Keys; }
    }

    /// <summary>
    /// Gets the index of a given level
    /// </summary>
  //  public int IndexOf(InterationItem item)
  //  {
        
        //if (item == null)
        //{
        //    return -1;
        //}

        //for (int i = 0; i < interationData.Length; ++i)
        //{
        //    if (interationData[i] == item)
        //    {
        //        return i;
        //    }
        //}

        //return -1;
   // }

    /// <summary>
    /// Gets whether this level exists in the list
    /// </summary>
    //public bool Contains(InterationItem item)
    //{
    //    return IndexOf(item) >= 0;
    //}

    ///// <summary>
    ///// Gets whether a level of the given id exists
    ///// </summary>
    //public bool ContainsKey(int key)
    //{
    //    return interationDataDictionary.ContainsKey(key);
    //}

    ///// <summary>
    ///// Try get a level with the given key
    ///// </summary>
    //public bool TryGetValue(int key, out InterationItem value)
    //{
    //    return interationDataDictionary.TryGetValue(key, out value);
    //}

    /// <summary>
    /// Gets the <see cref="InterationItem"/> associated with the given scene
    /// </summary>
   

    // Explicit interface implementations
    // Serialization listeners to create dictionary
    //void ISerializationCallbackReceiver.OnBeforeSerialize()
    //{
    //}

    //void ISerializationCallbackReceiver.OnAfterDeserialize()
    //{
    //    interationDataDictionary = interationData.ToDictionary(l => l.id);
    //}

    //ICollection<InterationItem> IDictionary<int, InterationItem>.Values
    //{
    //    get { return interationDataDictionary.Values; }
    //}

    //InterationItem IList<InterationItem>.this[int i]
    //{
    //    get { return interationData[i]; }
    //    set { throw new NotSupportedException("Level List is read only"); }
    //}

    //InterationItem IDictionary<int, InterationItem>.this[int key]
    //{
    //    get { return interationDataDictionary[key]; }
    //    set { throw new NotSupportedException("Level List is read only"); }
    //}

    //void IList<InterationItem>.Insert(int index, InterationItem item)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //void IList<InterationItem>.RemoveAt(int index)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //void ICollection<InterationItem>.Add(InterationItem item)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //void ICollection<KeyValuePair<int, InterationItem>>.Add(KeyValuePair<int, InterationItem> item)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //void ICollection<KeyValuePair<int, InterationItem>>.Clear()
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //bool ICollection<KeyValuePair<int, InterationItem>>.Contains(KeyValuePair<int, InterationItem> item)
    //{
    //    return interationDataDictionary.Contains(item);
    //}

    //void ICollection<KeyValuePair<int, InterationItem>>.CopyTo(KeyValuePair<int, InterationItem>[] array, int arrayIndex)
    //{
    //    interationDataDictionary.CopyTo(array, arrayIndex);
    //}

    //void ICollection<InterationItem>.Clear()
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //void ICollection<InterationItem>.CopyTo(InterationItem[] array, int arrayIndex)
    //{
    //    interationData.CopyTo(array, arrayIndex);
    //}

    //bool ICollection<InterationItem>.Remove(InterationItem item)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //public IEnumerator<InterationItem> GetEnumerator()
    //{
    //    return ((IList<InterationItem>)interationData).GetEnumerator();
    //}

    //IEnumerator<KeyValuePair<int, InterationItem>> IEnumerable<KeyValuePair<int, InterationItem>>.GetEnumerator()
    //{
    //    return interationDataDictionary.GetEnumerator();
    //}

    //IEnumerator IEnumerable.GetEnumerator()
    //{
    //    return interationData.GetEnumerator();
    //}

    //void IDictionary<int, InterationItem>.Add(int key, InterationItem value)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //bool ICollection<KeyValuePair<int, InterationItem>>.Remove(KeyValuePair<int, InterationItem> item)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}

    //bool IDictionary<int, InterationItem>.Remove(int key)
    //{
    //    throw new NotSupportedException("Level List is read only");
    //}
}