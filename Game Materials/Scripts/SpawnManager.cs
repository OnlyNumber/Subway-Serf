using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;


    public delegate void OnGameChange();

    public OnGameChange onIsGameStart;
    public OnGameChange onIsGameFinish;

    [SerializeField]
    private int sizeOfObstaclePool;

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
                //Debug.Log("Change on true");

                onIsGameStart?.Invoke();
            }
            else
            {

                //Debug.Log("Change on false");
                onIsGameFinish?.Invoke();
            }
        }
    }

    [SerializeField]
    private float WaitTimeBeforeSpawn;
    [SerializeField]
    private float TimeForSpawnObstacle;

    //[SerializeField]
    private GameObject[] obstaclesPool;

    [SerializeField]
    private GameObject[] obstaclePrefab;



    [SerializeField]
    private Transform[] spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Debug.Log("OBGAMESTART");

        onIsGameStart += GameStart;

        onIsGameFinish += StopSpawning;

        

        //StartCoroutine(SpawnObstacles());

        obstaclesPool = new GameObject[sizeOfObstaclePool];

        for (int i = 0; i < sizeOfObstaclePool; i++)
        {

            if (i %2 == 0)
            {
                obstaclesPool[i] = Instantiate(obstaclePrefab[0]);
            }
            else
            {
                obstaclesPool[i] = Instantiate(obstaclePrefab[1]);
                
            }

            obstaclesPool[i].SetActive(false);


        }





    }

    private void Update()
    {
        //Debug.Log("EXIST");
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(WaitTimeBeforeSpawn);

        
        StartCoroutine(SpawnObstacles());

    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }



    
    IEnumerator SpawnObstacles()
    {
        //Debug.Log("SPAWN OBSTACLES");

        int randRange;

        while (IsGame)
        {

            while(true)
            {
                randRange = Random.Range(0, obstaclesPool.Length);

                //Debug.Log(randRange);

                if(obstaclesPool[randRange].activeInHierarchy == false)
                {


                    break;
                }


            }
            obstaclesPool[randRange].SetActive(true);

            obstaclesPool[randRange].transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            obstaclesPool[randRange].transform.rotation = spawnPoints[0].rotation;


            yield return new WaitForSeconds(TimeForSpawnObstacle);

        }
    }

    private void GameStart()
    {
        StartCoroutine(WaitTime());
    }

    [ContextMenu("Remove all obstacles")]
    public void SetOffAllObstacles()
    {

        for (int i = 0; i < obstaclesPool.Length; i++)
        {
            obstaclesPool[i].gameObject.SetActive(false);

        
        }

    }


}
