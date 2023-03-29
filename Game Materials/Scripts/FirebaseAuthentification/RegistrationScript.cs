using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class RegistrationScript : MonoBehaviour
{
    DatabaseReference dbRef;

    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    private const string LOG_IN_MENU_SCENE = "Exit menu";
    private const string MAIN_MENU_SCENE = "Main menu";

    [SerializeField]
    private InputField Email;
    [SerializeField]
    private InputField Nickname;
    [SerializeField]
    private InputField Password;
    [SerializeField]
    private InputField RepeatPassword;

    [SerializeField] 
    private Text ErrorField;

    [SerializeField]
    PlayerDataSO playerData;

    private int UserId;


    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Error:" + dependencyStatus);
            }

        });
        //StartCoroutine(LoadUserId());
    }

    private void InitializeFirebase()
    {
        //Debug.Log("Setting up");

        auth = FirebaseAuth.DefaultInstance;

    }

    public void RegisterButton()
    {


        StartCoroutine(Register(Email.text, Password.text, Nickname.text));
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {

        

        if(_username == "")
        {
            ErrorField.text = "Missing name";
        }
        else if(Password.text != RepeatPassword.text)
        {
            ErrorField.text = "Passwords does not match";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            Debug.Log(RegisterTask.Result.Email);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with{RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;

                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register failed";


                ErrorField.text = message;
            }
            else
            {
                user = RegisterTask.Result;


                if(user !=null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _username};

                    var profileTask = user.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);


                    if (profileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with{profileTask.Exception}");
                        FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;

                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                        ErrorField.text = "_username set failed";
                    }
                    else
                    {
                        playerData.UserId = user.UserId;
                        playerData.UserName = Nickname.text;

                        SaveData();

                        SceneManager.LoadScene(MAIN_MENU_SCENE);


                    }

                }
            }
        }
        
    }

    private void SaveData()
    {
        UserData userData = new UserData(Nickname.text, 0);

        string json = JsonUtility.ToJson(userData);

        dbRef.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);

        

    }

    public void GoToLogInMenu()
    {
        SceneManager.LoadScene(LOG_IN_MENU_SCENE);
    }

    /*private IEnumerator LoadUserId()
    {
        var user = dbRef.Child("users").OrderByKey().GetValueAsync();

        yield return new WaitUntil(predicate: () => user.IsCompleted);

        if (user.Exception != null)
        {
            Debug.LogError(user.Exception);
        }
        else if (user.Result.Value == null)
        {
            Debug.Log("Null");
            UserId = 0;

        }
        else
        {
            DataSnapshot snapshot = user.Result;

            List<DataSnapshot> reverseList = new List<DataSnapshot>();

            foreach (DataSnapshot clidSnapshot in snapshot.Children)
            {
                reverseList.Add(clidSnapshot);
            }


            UserId =int.Parse( reverseList[reverseList.Count-1].Key.ToString());
            UserId += 1;

            Debug.Log(UserId);

        }


    }*/



}
