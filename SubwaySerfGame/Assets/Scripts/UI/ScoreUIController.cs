using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUIController : MonoBehaviour
{
    [SerializeField]
    private Text playerScore;

    private float score;

    private void Update()
    {
        
        playerScore.text = ((int)score).ToString();

    }

    private void FixedUpdate()
    {
        if (SpawnManager.instance.ISGAME)
        {
            score += Time.deltaTime * 1;
        }
    }

    public void RestartScore()
    {
        score = 0;
    }    







}
