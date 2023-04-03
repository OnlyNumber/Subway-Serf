using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using Firebase;
using UnityEngine.SceneManagement;

public class LogInScript : MonoBehaviour
{
    private const string REGISTRATION_MENU_SCENE = "Registration menu";
    private const string MAIN_MENU_SCENE = "Main menu";


    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField passwordField;
    [SerializeField]
    private Text errorField;

    public DependencyStatus dependencyStatus;
    public static FirebaseAuth auth;
    public FirebaseUser user;

    //[SerializeField]
    //PlayerDataSO playerData;

    //DatabaseReference dbRef;

    private void Start()
    {
        StartCoroutine(CheckAndFixDependenciesAsync());

        /*if(playerData.userName != "")
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE);
        }*/
        

    }

   

    private IEnumerator CheckAndFixDependenciesAsync()
    {
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(() => dependencyTask.IsCompleted);

        dependencyStatus = dependencyTask.Result;

        if (dependencyStatus == DependencyStatus.Available)
        {
            InitializeFirebase();

            yield return new WaitForEndOfFrame();

            Debug.Log("Try in");
            StartCoroutine(CheckForAutoLogin());

        }
        else
        {
            Debug.LogError("Error:" + dependencyStatus);
        }

    }

    

    private IEnumerator CheckForAutoLogin()
    {
        if(user != null)
        {
            var reloadUser = user.ReloadAsync();

            yield return new WaitUntil(() => reloadUser.IsCompleted);

            AutoLogin();
        }
        else
        {
            Debug.Log("No User");
        }
    }

    private void AutoLogin()
    {
        if(user != null)
        {
            DataHolder.name = user.DisplayName;
            DataHolder.id = user.UserId;

            //playerData.userId = user.UserId;
            //playerData.userName = user.DisplayName;

            Debug.Log(user.UserId);

            SceneManager.LoadScene(MAIN_MENU_SCENE);
        }
        else
        {

        }
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up");

        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;

        AuthStateChanged(this, null);


    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }


    public void LogInButton()
    {
        StartCoroutine(Login(emailField.text, passwordField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            
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
            errorField.text = message;
        }
        else
        {
            user = LoginTask.Result;

            DataHolder.name = user.DisplayName;
            DataHolder.id = user.UserId;


            //playerData.userId = user.UserId;
            //playerData.userName = user.DisplayName;

            Debug.Log(user.UserId);

            SceneManager.LoadScene(MAIN_MENU_SCENE);
            
        }
    }

    public static void SignOut()
    {
        auth.SignOut();
    }


    public void GoToRegistration()
    {
        SceneManager.LoadScene(REGISTRATION_MENU_SCENE);
    }

}
