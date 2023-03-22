using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour
{

    [SerializeField]
    private InputField Email;
    [SerializeField]
    private InputField Nickname;
    [SerializeField]
    private InputField Password;
    [SerializeField]
    private InputField RepeatPassword;

    [SerializeField] Text ErrorField;

    [SerializeField]
    private string nextScene;


    public void Registration()
    {
        if(Email.text == "")
        {
            ErrorField.text = "No Email";
            return;
        }
        
        if (Nickname.text == "")
        {
            ErrorField.text = "No Nickname";
            return;
        }
        
        if (Password.text == "")
        {
            ErrorField.text = "No Password";
            return;
        }
        if (RepeatPassword.text == "")
        {
            ErrorField.text = "No RepeatPassword";
            return;
        }

        if(Password.text != RepeatPassword.text)
        {
            ErrorField.text = "Passwords are not same";
            return;
        }

        InfoForRegistraion.Email = Email.text;
        InfoForRegistraion.Nickname = Nickname.text;
        InfoForRegistraion.Password = Password.text;

        SceneManager.LoadScene(nextScene);
    }


}
