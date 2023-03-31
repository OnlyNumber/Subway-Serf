using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

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


}

