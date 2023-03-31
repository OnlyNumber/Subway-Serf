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

    private const string LOG_IN_MENU_SCENE = "LogIn menu";
    private const string MAIN_MENU_SCENE = "Main menu";

    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField nicknameField;
    [SerializeField]
    private InputField passwordField;
    [SerializeField]
    private InputField repeatPasswordField;

    [SerializeField] 
    private Text errorField;

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
        auth = FirebaseAuth.DefaultInstance;

    }

    public void RegisterButton()
    {


        StartCoroutine(Register(emailField.text, passwordField.text, nicknameField.text));
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {

        

        if(_username == "")
        {
            errorField.text = "Missing name";
        }
        else if(passwordField.text != repeatPasswordField.text)
        {
            errorField.text = "Passwords does not match";
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


                errorField.text = message;
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

                        errorField.text = "_username set failed";
                    }
                    else
                    {
                        playerData.userId = user.UserId;
                        playerData.userName = nicknameField.text;

                        SaveData();

                        SceneManager.LoadScene(MAIN_MENU_SCENE);


                    }

                }
            }
        }
        
    }

    private void SaveData()
    {
        UserData userData = new UserData(nicknameField.text, 0);

        string json = JsonUtility.ToJson(userData);

        dbRef.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);
    }

    public void GoToLogInMenu()
    {
        SceneManager.LoadScene(LOG_IN_MENU_SCENE);
    }
}
