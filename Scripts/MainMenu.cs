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



    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();

        spawnManager.onIsGameStart += PutOffButtons;

        spawnManager.onIsGameFinish += PutOnButtons;
    }

    private void Update()
    {

    }

    public void PutOffButtons()
    {
        ExitButton.gameObject.SetActive(false);
        ExitFromAccountButton.gameObject.SetActive(false);
    }

    public void PutOnButtons()
    {
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


}
