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



    [CreateAssetMenu(fileName = "NPCLibrary.asset", menuName = "Haleth/Player/NPCLibray", order = 1)]
    public class NPCItemLibrary : ScriptableObject, IList<NpcConfiguration>, IDictionary<string, NpcConfiguration>
    {
        public NpcConfiguration[] PlayerCharacterLibrary;
       

        IDictionary<string, NpcConfiguration> m_LevelDictionary;
        public int Count
        {
            get { return PlayerCharacterLibrary.Length; }
        }
        public bool IsReadOnly
        {
            get { return true; }
        }
        public NpcConfiguration this[int i]
        {
            get { return PlayerCharacterLibrary[i]; }
        }
        public NpcConfiguration this[string key]
        {
            get { return m_LevelDictionary[key]; }
        }
        public ICollection<string> Keys
        {
            get { return m_LevelDictionary.Keys; }
        }
        public int IndexOf(NpcConfiguration item)
        {
            if (item == null)
            {
                return -1;
            }

            for (int i = 0; i < PlayerCharacterLibrary.Length; ++i)
            {
                if (PlayerCharacterLibrary[i] == item)
                {
                    return i;
                }
            }

            return -1;
        }
        public bool Contains(NpcConfiguration item)
        {
            return IndexOf(item) >= 0;
        }

        /// <summary>
        /// Gets whether a level of the given id exists
        /// </summary>
        public bool ContainsKey(string key)
        {
            return m_LevelDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Try get a level with the given key
        /// </summary>
        public bool TryGetValue(string key, out NpcConfiguration value)
        {
            return m_LevelDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the <see cref="LevelList"/> associated with the given scene
        /// </summary>


        // Explicit interface implementations
        // Serialization listeners to create dictionary


        ICollection<NpcConfiguration> IDictionary<string, NpcConfiguration>.Values
        {
            get { return m_LevelDictionary.Values; }
        }

        NpcConfiguration IList<NpcConfiguration>.this[int i]
        {
            get { return PlayerCharacterLibrary[i]; }
            set { throw new NotSupportedException("Level List is read only"); }
        }

        NpcConfiguration IDictionary<string, NpcConfiguration>.this[string key]
        {
            get { return m_LevelDictionary[key]; }
            set { throw new NotSupportedException("Level List is read only"); }
        }

        void IList<NpcConfiguration>.Insert(int index, NpcConfiguration item)
        {
            throw new NotSupportedException("Level List is read only");
        }

        void IList<NpcConfiguration>.RemoveAt(int index)
        {
            throw new NotSupportedException("Level List is read only");
        }

        void ICollection<NpcConfiguration>.Add(NpcConfiguration item)
        {
            throw new NotSupportedException("Level List is read only");
        }

        void ICollection<KeyValuePair<string, NpcConfiguration>>.Add(KeyValuePair<string, NpcConfiguration> item)
        {
            throw new NotSupportedException("Level List is read only");
        }

        void ICollection<KeyValuePair<string, NpcConfiguration>>.Clear()
        {
            throw new NotSupportedException("Level List is read only");
        }

        bool ICollection<KeyValuePair<string, NpcConfiguration>>.Contains(KeyValuePair<string, NpcConfiguration> item)
        {
            return m_LevelDictionary.Contains(item);
        }

        void ICollection<KeyValuePair<string, NpcConfiguration>>.CopyTo(KeyValuePair<string, NpcConfiguration>[] array, int arrayIndex)
        {
            m_LevelDictionary.CopyTo(array, arrayIndex);
        }

        void ICollection<NpcConfiguration>.Clear()
        {
            throw new NotSupportedException("Level List is read only");
        }

        void ICollection<NpcConfiguration>.CopyTo(NpcConfiguration[] array, int arrayIndex)
        {
            PlayerCharacterLibrary.CopyTo(array, arrayIndex);
        }

        bool ICollection<NpcConfiguration>.Remove(NpcConfiguration item)
        {
            throw new NotSupportedException("Level List is read only");
        }

        public IEnumerator<NpcConfiguration> GetEnumerator()
        {
            return ((IList<NpcConfiguration>)PlayerCharacterLibrary).GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, NpcConfiguration>> IEnumerable<KeyValuePair<string, NpcConfiguration>>.GetEnumerator()
        {
            return m_LevelDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return PlayerCharacterLibrary.GetEnumerator();
        }

        void IDictionary<string, NpcConfiguration>.Add(string key, NpcConfiguration value)
        {
            throw new NotSupportedException("Level List is read only");
        }

        bool ICollection<KeyValuePair<string, NpcConfiguration>>.Remove(KeyValuePair<string, NpcConfiguration> item)
        {
            throw new NotSupportedException("Level List is read only");
        }

        bool IDictionary<string, NpcConfiguration>.Remove(string key)
        {
            throw new NotSupportedException("Level List is read only");
        }
    }
}