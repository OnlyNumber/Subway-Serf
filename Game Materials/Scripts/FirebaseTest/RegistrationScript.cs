using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;


public class RegistrationScript : MonoBehaviour
{
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

    private void Awake()
    {
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

            if(RegisterTask.Exception != null)
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
                        Debug.Log(user.DisplayName);
                        //user.DisplayName;


                    }

                }
            }
        }
        
    }






}
