using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    SpawnManager spawnManager;

    [SerializeField]
    private string nextScene;

    [SerializeField]
    private Button ExitButton;

    [SerializeField]
    private Button ExitFromAccountButton;

    [SerializeField]
    private Text startTouch;

    [SerializeField]
    private Button startButton;


    private void Start()
    {

        SpawnManager.instance.onIsGameStart += PutOffButtons;
    }

    public void PutOffButtons()
    {
        startButton.gameObject.SetActive(false);
        startTouch.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        ExitFromAccountButton.gameObject.SetActive(false);
    }

    public void PutOnButtons()
    {
        startButton.gameObject.SetActive(true);
        startTouch.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        ExitFromAccountButton.gameObject.SetActive(true);
    }



    public void ExitFromGame()
    {
        Application.Quit();
    }

    public void ExitFromAccount()
    {
        
        
        SceneManager.LoadScene(nextScene);
    }

    public void StartGame()
    {
        SpawnManager.instance.ISGAME = true;

        Debug.Log("StartGame");
    }

}
