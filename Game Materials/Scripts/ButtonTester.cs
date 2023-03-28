using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTester : MonoBehaviour
{
    public Text errorText;

    public RegistrationScript inst;

    private void Start()
    {
        inst = GetComponent<RegistrationScript>();
    }


    public void typeErrorMessage()
    {
        errorText.text = "Registration button work";
    }

}
