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
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up");

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
                        Debug.Log(user.Email);

                        playerData.UserName = user.DisplayName;

                        SaveData();

                        /*UserData userData = new UserData(user.DisplayName, 0);

                        string json = JsonUtility.ToJson(user);

                        dbRef.Child("users").Child(user.DisplayName).SetRawJsonValueAsync(json);*/

                        //SceneManager.LoadScene("Main menu");


                    }

                }
            }
        }
        
    }

    private void SaveData()
    {
        UserData userData = new UserData(user.DisplayName, 0);

        string json = JsonUtility.ToJson(userData);

        dbRef.Child("users").Child(user.DisplayName).SetRawJsonValueAsync(json);

        

    }

    public void GoToLogInMenu()
    {
        SceneManager.LoadScene("Exit menu");
    }


    /*
     private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
        }
    }
     
     
     */






}
