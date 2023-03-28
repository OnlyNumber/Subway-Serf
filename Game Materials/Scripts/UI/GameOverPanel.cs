using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    SpawnManager spMan;

    private void Start()
    {

        SpawnManager.instance.onIsGameFinish += SetActiveTrue;
        SpawnManager.instance.onIsGameStart += SetActiveFalse;
    }

    public void SetActiveFalse()
    {
        panel.SetActive(false);
    }

    public void SetActiveTrue()
    {
        panel.SetActive(true);
    }


    [ContextMenu("stop")]
    public void TimeStop()
    {
        Time.timeScale = 0;

    }

    [ContextMenu("Go")]
    public void TimeGo()
    {
        Time.timeScale = 5f;
    }

}

