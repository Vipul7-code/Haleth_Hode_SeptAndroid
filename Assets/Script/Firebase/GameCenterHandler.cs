using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google;
using System.Threading.Tasks;
using Firebase.Auth;
using Proyecto26;
using Firebase.Extensions;

public class GameCenterHandler : MonoBehaviour
{
    // Start is called before the first frame update
    #region GAME_CENTER    
    Firebase.Auth.FirebaseAuth auth;
    string  NAME,USERID;
    DataController DController;
    DatabaseManager db;
   [HideInInspector]
  public  bool Loggedin = false;
    [HideInInspector]
   public string webClientId,googleId,googleEmail,googleName,token;
    GoogleSignInConfiguration configuration;
    Credential credential;
    public Text welcomeText;
    public Button googleButton, fbButton,LogoutButton;
    GameObject profilePic;
    public static string uniquiId, uniquiName, email, gameCenterName, gameCenterId;
    public static string armour1 , armour2, armour3, weapon1, weapon2, weapon3,savePoint;
   static GameCenterHandler instance;
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
    void Start()
    {
        Globals.gameCenterHandler = this;
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        DController = FindObjectOfType<DataController>();
        db = FindObjectOfType<DatabaseManager>();
        AuthenticateToGameCenter();
    }
    void AuthenticateToGameCenter()
    {
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Authentication successful");
            }
            else
            {
                Debug.Log("Authentication failed");
            }
        });
    }
    public void GameCenterInfo()
    {
        gameCenterName = Social.localUser.userName;
        gameCenterId = Social.localUser.id;
        Globals.id = gameCenterId;
        SaveInfoToDb();
    }

    public  void SaveInfoToDb()
    {
        Globals.userData user = new Globals.userData();
        RestApiDb.PostUser(user, Globals.id);
        Globals.playerState.GameCenterId = Social.localUser.id;
        Globals.playerState.GameCenterName = Social.localUser.userName;
        db.UpdateRecord<Globals.Player>(Globals.playerState);
    }
    void SaveGameCenter()
    {
        Globals.playerState.GameCenterId = Globals.id;
        Globals.playerState.GameCenterName = NAME;
        db.UpdateRecord<Globals.Player>(Globals.playerState);
    }

    public void OnGoogleLogin()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(
          OnAuthenticationFinished);
    }
    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;

                    Debug.LogError("Got Error: " + error.Status + " " + error.Message);

                }
                else
                {
                    Debug.LogError("Got Unexpected Exception?!?" + task.Exception.Message);

                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Sign-In Canceled by user!");
        }
        else
        {
            Globals.accessToken = task.Result.IdToken;
            googleId = task.Result.UserId;
            googleName = task.Result.DisplayName;
            googleEmail = task.Result.Email;
            Loggedin = true;
            SaveData();
        }
    }
 public void SaveData()
    {
        Globals.userData user = new Globals.userData();
        Globals.playerState.AccessToken = Globals.accessToken;
        Globals.playerState.GoogleId = googleId;
        Globals.playerState.GoogleName = googleName;
        Globals.playerState.GoogleEmail = googleEmail;
        db.UpdateRecord<Globals.Player>(Globals.playerState);
        RestApiDb.ForMergeAccount(Globals.playerState.GameCenterId, user, googleId, googleName, googleEmail);
      // welcomeText.text = "Welcome " + uniquiName;
        Globals.uiManager.LoadingSetting();
    }

    public void OnGuestLogin()
    {
        Globals.accessToken = SystemInfo.deviceModel;
        googleId = SystemInfo.deviceUniqueIdentifier;
        googleName = "GuestUser";
        googleEmail = "guest@abc.com";
        Loggedin = true;
        SaveData();
    }

    public void OnLogOut()
    {
        Globals.userData user = new Globals.userData();
        RestApiDb.OnLogout(Globals.playerState.GameCenterId,user);
        Globals.playerState.GoogleId = "";
        Globals.playerState.GoogleEmail = "";
        Globals.playerState.GoogleName = "";
        Globals.playerState.AccessToken = "";
        db.UpdateRecord<Globals.Player>(Globals.playerState);
      //  ShowLogin(true, true, false);
    }
    #endregion
}