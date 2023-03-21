using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AuthentificationManager : MonoBehaviour
{
    [SerializeField]
    private InputField Email;
    [SerializeField]
    private InputField Password;

    [SerializeField]
    private Text ErrorText;

    [SerializeField]
    private string nextScene;

    public void ExitFromAccount()
    {


        if(Email.text.ToString() != InfoForRegistraion.Email)
        {
            ErrorText.text = "Wrong Email";
            return;
        }
        if (Password.text.ToString() != InfoForRegistraion.Password)
        {
            ErrorText.text = "Wrong Password";
            return;
        }



        SceneManager.LoadScene(nextScene);
    }

}
