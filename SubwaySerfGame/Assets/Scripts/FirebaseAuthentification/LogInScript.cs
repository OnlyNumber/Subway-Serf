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
    public FirebaseAuth auth;
    public FirebaseUser user;

    [SerializeField]
    PlayerDataSO playerData;

    //DatabaseReference dbRef;

    private void Start()
    {
        StartCoroutine(CheckAndFixDependenciesAsync());

        if(playerData.userName != "")
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE);
        }
        

    }

   

    private IEnumerator CheckAndFixDependenciesAsync()
    {
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(() => dependencyTask.IsCompleted);

        dependencyStatus = dependencyTask.Result;

        if (dependencyStatus == DependencyStatus.Available)
        {
            InitializeFirebase();
        }
        else
        {
            Debug.LogError("Error:" + dependencyStatus);
        }

    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up");

        auth = FirebaseAuth.DefaultInstance;

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

            playerData.userId = user.UserId;
            playerData.userName = user.DisplayName;

            Debug.Log(user.UserId);

            SceneManager.LoadScene(MAIN_MENU_SCENE);
            
        }
    }

    


    public void GoToRegistration()
    {
        SceneManager.LoadScene(REGISTRATION_MENU_SCENE);
    }

}
