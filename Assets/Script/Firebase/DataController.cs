using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class DataController : MonoBehaviour
{
    DatabaseReference userInfoRef;
    string _name, userId,uniquId,uniqueName,uniquemail;
    // Start is called before the first frame update
    private void OnEnable()
    {
        userInfoRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
    }
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://haleth-hode.firebaseio.com");
        GameObject.DontDestroyOnLoad(this);
    }
    public void writeNewUser(string id, string name)
    {
        if(userInfoRef.Child("gameCenter").Child(Globals.id).Child("USERID")==null)
        _name = name;
        userId = id;
        UserData user = new UserData(name);
        string json = JsonUtility.ToJson(user);
        userInfoRef.Child("gameCenter").Child(Globals.id).SetRawJsonValueAsync(json);
    }
    public void WriteGoogleUser(string id,string name,string email,string gameCenterName)
    {
        _name = gameCenterName;
        uniquId = id;
        uniqueName = name;
        uniquemail = email;
        LinkData link = new LinkData(id, name, email,_name);
        string json = JsonUtility.ToJson(link);
        userInfoRef.Child("gameCenter").Child(Globals.id).SetRawJsonValueAsync(json);
    }
    public class UserData
    {
        public string Name;


        public UserData()
        {
            Name = null;
        }

        public UserData(string name)
        {

            Name = name;
        }
    }
    public List<UserData> playerData = new List<UserData>();
   public class LinkData
    {
        public string uniquId;
        public string uniquname;
        public string uniquemail;
        public string Name;
        public LinkData()
        {
            uniquId = null;
            uniquname = null;
            uniquemail = null;
            Name = null;
        }
        public LinkData(string uniqueId,string name,string email,string gameCenterName)
        {
            uniquId = uniqueId;
            uniquname = name;
            uniquemail = email;
            Name = gameCenterName;
        }
    }
    public List<LinkData> linkData = new List<LinkData>();
}
