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
    private InputField Email;
    [SerializeField]
    private InputField Password;
    [SerializeField]
    private Text ErrorField;

    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    [SerializeField]
    PlayerDataSO playerData;

    //DatabaseReference dbRef;

    private void Start()
    {
        StartCoroutine(CheckAndFixDependenciesAsync());

        if(playerData.UserName != "")
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

    private IEnumerator CheckForAutoLogin()
    {

        if(user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            yield return new WaitUntil(() => reloadUserTask.IsCompleted);

            //CheckForAutoLogin()
        }
        else
        {
            //Refere
        }

    }




    public void LogInButton()
    {
        StartCoroutine(Login(Email.text, Password.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //Debug.Log("Not Work");

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
            ErrorField.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            user = LoginTask.Result;

            playerData.UserId = user.UserId;
            playerData.UserName = user.DisplayName;

            Debug.Log(user.UserId);

            SceneManager.LoadScene(MAIN_MENU_SCENE);
            
            //FirebaseAuth.DefaultInstance.SignOut();
            
        }
    }

    


    public void GoToRegistration()
    {
        SceneManager.LoadScene(REGISTRATION_MENU_SCENE);
    }

}
