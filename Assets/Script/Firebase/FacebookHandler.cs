using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Firebase.Auth;
using Firebase.Unity.Editor;
using Firebase;
public class FacebookHandler : MonoBehaviour
{
    DatabaseManager db;
    string FacebookID, FacebookEmail;
    string id, firstname, lastname, email,accessToken;
    private void Start()
    {
        db = FindObjectOfType<DatabaseManager>();
    }
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }
    public void FacebookLogin()
    {
        if (!FB.IsLoggedIn)
        {
            var permissions = new List<string>() { "public_profile", "email", "user_friends" };
            FB.LogInWithReadPermissions(permissions, AuthCallback);
        }
        else
        {
            SaveData();
        }
    }
    private void AuthCallback(ILoginResult result)
    {

        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            FB.API("/me?fields=id,first_name,last_name,email,birthday", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string>() { });
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
    public void GetFacebookInfo(IResult result1)
    {
        if (result1.Error == null)
        {
            id = result1.ResultDictionary["id"].ToString();
            firstname = result1.ResultDictionary["first_name"].ToString();
            lastname = result1.ResultDictionary["last_name"].ToString();
           accessToken= Facebook.Unity.AccessToken.CurrentAccessToken.TokenString.ToString();
            email = "facebook@gmail.com";
            //if (result1.ResultDictionary["email"].ToString() != null)
            //    email = result1.ResultDictionary["email"].ToString();
            //else
            //    email = null;
            Dictionary<string, string> _obt = JsonConvert.DeserializeObject<Dictionary<string, string>>(result1.RawResult);
            SaveData();
        }
        else
        {
            Debug.Log("Data not recived>>>>>>>>>>>>>>>>>");
        }
    }
    public void SaveData()
    {
        Globals.userData user = new Globals.userData();
        Globals.playerState.GoogleId = id;
        Globals.playerState.GoogleName = firstname +lastname;
        Globals.playerState.GoogleEmail = email;
        Globals.playerState.AccessToken = accessToken;
        db.UpdateRecord<Globals.Player>(Globals.playerState);
        GameCenterHandler.uniquiName = firstname + lastname;
        RestApiDb.ForMergeAccount(Globals.playerState.GameCenterId, user,id,GameCenterHandler.uniquiName,email);
        Globals.uiManager.LoadingSetting();
    }
}
