using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
public class RestApiDb 
{
    private static readonly string databaseURL = $"https://haleth-hode.firebaseio.com/";
    public delegate void PostUserCallback();
    public delegate void GetUserCallback(Globals.userData user);
    public delegate void GetUsersCallback(Dictionary<string, Globals.userData> users);
    public static void PostUser(Globals.userData user, string userId)
    {
        RestClient.Put<Globals.userData>($"{databaseURL}users/{userId}.json", user);
    }
    public static void GetUser( string userId,Globals.userData user)//, GetUserCallback callback)
    {
        RestClient.Get<Globals.userData>($"{databaseURL}users/{userId}.json").Then(response =>
        {
            user= response;
        });
    }
    public static void ForMergeAccount(string userId,Globals.userData user,string mergeId,string mergeName,string mergeEmail)
    {
        RestClient.Get<Globals.userData>($"{databaseURL}users/{userId}.json").Then(response =>
        {
            user = response;
            if(user.uniquId.Equals(""))
            {
                user.uniquId = mergeId;
                user.uniquname = mergeName;
                user.uniquemail = mergeEmail;
                RestClient.Put<Globals.userData>($"{databaseURL}users/{userId}.json", user);
            }
        });

    }
    public static void UpdateArmor(string userId,Globals.userData user,string weapon)
    {
        RestClient.Get<Globals.userData>($"{databaseURL}users/{userId}.json").Then(response =>
        {
            user = response;
            if (weapon.Equals("armour1"))
                user.armour1 = "1";
            else if (weapon .Equals("armour2"))
                user.armour2 = "1";
            else if (weapon .Equals("armour3"))
                user.armour3 = "1";
            else if (weapon.Equals("weapon1"))
                user.weapon1 = "1";
            else if (weapon.Equals("weapon2"))
                user.weapon2 = "1";
            RestClient.Put<Globals.userData>($"{databaseURL}users/{userId}.json", user);
        });
    }
    public static void OnLogout(string userId,Globals.userData user)
    {
        RestClient.Get<Globals.userData>($"{databaseURL}users/{userId}.json").Then(response =>
        {
            user = response;
            user.uniquId = "";
            user.uniquname = "";
            user.uniquemail = "";
            user.armour1 = "";
            user.armour2 = "";
            user.armour3 = "";
            user.weapon1 = "";
            user.weapon2 = "";
            RestClient.Put<Globals.userData>($"{databaseURL}users/{userId}.json",user);
        });
    }
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}users.json").Then(response =>
        {
            var responseJson = response.Text;
            // to serialize more complex types (a Dictionary, in this case)
          //  var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            // serializer.TryDeserialize(data, typeof(Dictionary<string, Globals.userData>), ref deserialized);
            Dictionary<string, Globals.userData> _obt = JsonConvert.DeserializeObject<Dictionary<string, Globals.userData>>(responseJson);
            var users = deserialized as Dictionary<string, Globals.userData>;
            callback(users);
            Debug.Log("user::" + users.Count);
        });
    }
}
