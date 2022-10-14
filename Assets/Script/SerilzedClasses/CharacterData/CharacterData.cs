using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Scriptable object for Level configuration
/// </summary>
[CreateAssetMenu(fileName = "CharacterLibrary", menuName = "Helth / Character / CharacterLibrary", order = 0)]
public class CharacterData : ScriptableObject, IList<CharacterItem>,
                         IDictionary<int, CharacterItem>,
                         ISerializationCallbackReceiver
{


    public CharacterItem[] characterList;


    /// <summary>
    /// Cached dictionary of levels by their IDs
    /// </summary>
    IDictionary<int, CharacterItem> characterListDictionary;

    /// <summary>
    /// Gets the number of levels
    /// </summary>
    public int Count
    {
        get { return characterList.Length; }
    }

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
    public CharacterItem this[int i]
    {
        get { return characterList[i]; }
    }

    /// <summary>
    /// Gets a level by id
    /// </summary>
    /*public CharacterItem this[int key]
    {
        get { return characterListDictionary[key]; }
    }*/

    /// <summary>
    /// Gets a collection of all level keys
    /// </summary>
    public ICollection<int> Keys
    {
        get { return characterListDictionary.Keys; }
    }

    /// <summary>
    /// Gets the index of a given level
    /// </summary>
    public int IndexOf(CharacterItem item)
    {
        if (item == null)
        {
            return -1;
        }

        for (int i = 0; i < characterList.Length; ++i)
        {
            if (characterList[i] == item)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Gets whether this level exists in the list
    /// </summary>
    public bool Contains(CharacterItem item)
    {
        return IndexOf(item) >= 0;
    }

    /// <summary>
    /// Gets whether a level of the given id exists
    /// </summary>
    public bool ContainsKey(int key)
    {
        return characterListDictionary.ContainsKey(key);
    }

    /// <summary>
    /// Try get a level with the given key
    /// </summary>
    public bool TryGetValue(int key, out CharacterItem value)
    {
        return characterListDictionary.TryGetValue(key, out value);
    }

    /// <summary>
    /// Gets the <see cref="CharacterItem"/> associated with the given scene
    /// </summary>
   

    // Explicit interface implementations
    // Serialization listeners to create dictionary
    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        characterListDictionary = characterList.ToDictionary(l => l.id);
    }

    ICollection<CharacterItem> IDictionary<int, CharacterItem>.Values
    {
        get { return characterListDictionary.Values; }
    }

    CharacterItem IList<CharacterItem>.this[int i]
    {
        get { return characterList[i]; }
        set { throw new NotSupportedException("Level List is read only"); }
    }

    CharacterItem IDictionary<int, CharacterItem>.this[int key]
    {
        get { return characterListDictionary[key]; }
        set { throw new NotSupportedException("Level List is read only"); }
    }

    void IList<CharacterItem>.Insert(int index, CharacterItem item)
    {
        throw new NotSupportedException("Level List is read only");
    }

    void IList<CharacterItem>.RemoveAt(int index)
    {
        throw new NotSupportedException("Level List is read only");
    }

    void ICollection<CharacterItem>.Add(CharacterItem item)
    {
        throw new NotSupportedException("Level List is read only");
    }

    void ICollection<KeyValuePair<int, CharacterItem>>.Add(KeyValuePair<int, CharacterItem> item)
    {
        throw new NotSupportedException("Level List is read only");
    }

    void ICollection<KeyValuePair<int, CharacterItem>>.Clear()
    {
        throw new NotSupportedException("Level List is read only");
    }

    bool ICollection<KeyValuePair<int, CharacterItem>>.Contains(KeyValuePair<int, CharacterItem> item)
    {
        return characterListDictionary.Contains(item);
    }

    void ICollection<KeyValuePair<int, CharacterItem>>.CopyTo(KeyValuePair<int, CharacterItem>[] array, int arrayIndex)
    {
        characterListDictionary.CopyTo(array, arrayIndex);
    }

    void ICollection<CharacterItem>.Clear()
    {
        throw new NotSupportedException("Level List is read only");
    }

    void ICollection<CharacterItem>.CopyTo(CharacterItem[] array, int arrayIndex)
    {
        characterList.CopyTo(array, arrayIndex);
    }

    bool ICollection<CharacterItem>.Remove(CharacterItem item)
    {
        throw new NotSupportedException("Level List is read only");
    }

    public IEnumerator<CharacterItem> GetEnumerator()
    {
        return ((IList<CharacterItem>)characterList).GetEnumerator();
    }

    IEnumerator<KeyValuePair<int, CharacterItem>> IEnumerable<KeyValuePair<int, CharacterItem>>.GetEnumerator()
    {
        return characterListDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return characterList.GetEnumerator();
    }

    void IDictionary<int, CharacterItem>.Add(int key, CharacterItem value)
    {
        throw new NotSupportedException("Level List is read only");
    }

    bool ICollection<KeyValuePair<int, CharacterItem>>.Remove(KeyValuePair<int, CharacterItem> item)
    {
        throw new NotSupportedException("Level List is read only");
    }

    bool IDictionary<int, CharacterItem>.Remove(int key)
    {
        throw new NotSupportedException("Level List is read only");
    }
}