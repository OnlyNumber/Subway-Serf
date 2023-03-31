using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    SpawnManager spawnManager;

    [SerializeField]
    private PlayerDataSO playerData;

    private const string LOG_IN_MENU_SCENE = "LogIn menu";

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private Button exitFromAccoutButton;

    [SerializeField]
    private Text startTouchText;

    [SerializeField]
    private Button startButton;


    private void Start()
    {

        SpawnManager.instance.onIsGameStart += PutOffButtons;
    }

    public void PutOffButtons()
    {
        startButton.gameObject.SetActive(false);
        startTouchText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        exitFromAccoutButton.gameObject.SetActive(false);
    }

    public void PutOnButtons()
    {
        startButton.gameObject.SetActive(true);
        startTouchText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        exitFromAccoutButton.gameObject.SetActive(true);
    }



    public void ExitFromGame()
    {
        Application.Quit();
    }

    public void ExitFromAccount()
    {
        playerData.userId = null;
        playerData.userName = "";
        
        SceneManager.LoadScene(LOG_IN_MENU_SCENE);
    }

    public void StartGame()
    {
        SpawnManager.instance.ISGAME = true;

        Debug.Log("StartGame");
    }

}
