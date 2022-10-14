using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
/// <summary>
/// BRIDGE BETWEEN Application and Database
/// </summary>
public class DatabaseManager : MonoBehaviour
{
    public DataService _ds = new DataService("HalethHode.db");
   public static DatabaseManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
}
    public void Enable()
    {
        _ds.connection.Execute("PRAGMA foreign_keys=ON");
	}

	public void InsertRecord<T>(T obj)
    {
		_ds.SaveRecord<T> (obj);
    }

	public void UpdateRecord<T>(T obj)
	{
        try
        {
            int res = _ds.connection.Update(obj);
            if (res == 0)
            {
                InsertRecord<T>(obj);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

	public void DeleteRecord<T>(object key)where T:new()
	{
		_ds.DeleteRecord(key);
    }

	public IEnumerable<T> ReadTable<T>() where T:new()
	{
        try
        {
            return _ds.GetRecords<T>();
        }
        catch(Exception e)
        {
            return _ds.GetRecords<T>();
        }
	}

    

}
