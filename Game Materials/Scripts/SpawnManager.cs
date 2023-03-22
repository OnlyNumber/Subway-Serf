using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpawnManager : MonoBehaviour
{
    public delegate void OnGameChange();

    public OnGameChange onIsGameStart;
    public OnGameChange onIsGameFinish;

    [SerializeField]
    private bool IsGame;

    public bool ISGAME
    {
        get
        {
            return IsGame;
        }
        set
        {
            
            IsGame = value;
            if (IsGame == true)
            {
                onIsGameStart?.Invoke();
            }
            else
            {
                onIsGameFinish?.Invoke();
            }
        }
    }



    [SerializeField]
    private float WaitTimeBeforeSpawn;
    [SerializeField]
    private float TimeForSpawnObstacle;

    [SerializeField]
    private GameObject[] obstacles;

    [SerializeField]
    private Transform[] spawnPoints;

    private void Start()
    {
        onIsGameStart += GameStart;

        StartCoroutine(SpawnObstacles());


    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(WaitTimeBeforeSpawn);

        StartCoroutine(SpawnObstacles());

    }
    
    IEnumerator SpawnObstacles()
    {
        while(IsGame)
        {
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);

            yield return new WaitForSeconds(TimeForSpawnObstacle);

        }
    }

    private void GameStart()
    {
        StartCoroutine(WaitTime());
    }



}
